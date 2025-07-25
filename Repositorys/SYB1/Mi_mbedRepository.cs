using Lib;
using Models;
using MoreLinq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Mi_mbedRepository : SYB1BaseRepository<Mi_mbed>
    {
        /// <summary>
        /// 查詢轉入轉出及病人資訊
        /// </summary>
        public async Task<ApiResult<List<Mi_mbed_PatInfo>>> GetTranPatInfo(Mi_mbed param)
        {
            var query = (await DBUtil.QueryAsync<Mi_mbed_PatInfo>(param)).ToList();

            foreach (var b in query)
            {
                if (b.bed_pat_no.IsNullOrWhiteSpace())
                { // 床上無病人，取轉入欄資訊
                    if ((b.bed_i_mark == "1" || b.bed_i_mark == "4") && !b.bed_i_pat_no.IsNullOrWhiteSpace())
                    {
                        Mh_mpat mh_mpat = new Mh_mpat();
                        mh_mpat.pat_no = b.bed_i_pat_no.ToNullableInt();
                        mh_mpat =(await DB.Mh_mpatRepository.GetMh_mpat(mh_mpat, 1)).Data.FirstOrDefault();
                        b.bed_i_pat_name = mh_mpat?.pat_name ?? string.Empty;
                        b.bed_i_pat_sex = mh_mpat?.pat_sex ?? string.Empty;
                    }
                }
            }

            return new ApiResult<List<Mi_mbed_PatInfo>>(query);
        }

        public class BedPatDptComparer : IEqualityComparer<BedPatDpt>
        {
            public bool Equals(BedPatDpt x, BedPatDpt y)
            {
                return x.dpt == y.dpt;
            }

            public int GetHashCode(BedPatDpt obj)
            {
                return obj.dpt.GetHashCode();
            }
        }

        /// <summary>
        /// 查詢科別床動態
        /// </summary>
        public async Task<ApiResult<List<BedPatDpt>>> GetBedPatDpt(Mi_mbed param)
        {
            string sql = @"
            select mi_mbed.*,
            substring(ipd.ipd_group1 , 1 , 4 ) as 'bed_pat_dpt',
            case when rtrim(substring(ipdi.ipd_group1, 1, 4)) <> '' then substring(ipdi.ipd_group1, 1, 4) --入床者科別
            else pre2_dpt --預住科別
            end as 'bed_i_pat_dpt',
            ipd.ipd_out_dt
            from mi_mbed
            left join mi_mipd as ipd
            on ipd.ipd_no = substring(bed_group1,119,11)
            left join mi_mipd as ipdi
            on bed_i_mark in ('1','4') and ipdi.ipd_no = substring(bed_group1,75,11) 
            left join mi_mpre2 
            on bed_i_mark in ('1','4') and pre2_pat_no = convert(int,substring(bed_group1,67,8))
            where bed_unit = @bed_unit
            and substring(bed_group1,40,1) not in ('X','Y')"; // 停用：會報備到健保署(通常會藉由申報處理)

            var bedList = (await DBUtil.QueryAsync<Mi_mbed_PatDpt>(sql, param)).ToList();

            // 在床數
            //var pBeds = bedList.Where(b => !b.bed_pat_dpt.IsNullOrWhiteSpace() &&
            //((!b.ipd_out_dt.HasValue) || b.ipd_out_dt == 0))
            var pBeds = bedList.Where(b => !b.bed_pat_dpt.IsNullOrWhiteSpace() &&
                b.bed_group1.SybSubStr(119, 11).TrimEnd() != "") // 該床患者住院序號
                .GroupBy(b => b.bed_pat_dpt)
                .Select(g => new BedPatDpt { dpt = g.Key, beds = g.Count() });

            // 出床數
            var oBeds = bedList.Where(b => !b.bed_pat_dpt.IsNullOrWhiteSpace() &&
            new HashSet<string>() { "1", "4", "A", "P" }.Contains(b.bed_o_mark))
                .GroupBy(b => b.bed_pat_dpt)
                .Select(g => new BedPatDpt { dpt = g.Key, obeds = g.Count() });

            // 入床數
            var iBeds = bedList.Where(b => !b.bed_i_pat_dpt.IsNullOrWhiteSpace() &&
            new HashSet<string>() { "1", "4" }.Contains(b.bed_i_mark))
                .GroupBy(b => b.bed_i_pat_dpt)
                .Select(g => new BedPatDpt { dpt = g.Key, ibeds = g.Count() });

            //// left outer join: pBeds and oBeds
            //var poBeds =
            //    from p in pBeds
            //    join o in oBeds
            //    on p.dpt equals o.dpt
            //    into temp
            //    from o in temp.DefaultIfEmpty(new BedPatDpt())
            //    select new BedPatDpt { dpt = p.dpt, beds = p.beds, obeds = o.obeds };

            //// left outer join: poBeds and iBeds
            //var poiBeds =
            //    from po in poBeds
            //    join i in iBeds
            //    on po.dpt equals i.dpt
            //    into temp
            //    from i in temp.DefaultIfEmpty(new BedPatDpt())
            //    select new BedPatDpt { dpt = po.dpt, beds = po.beds, obeds = po.obeds, ibeds = i.ibeds };

            //// right outer join: iBeds and poBeds
            //var ipoBeds =
            //    from i in iBeds
            //    join po in poBeds
            //    on i.dpt equals po.dpt
            //    into temp
            //    from po in temp.DefaultIfEmpty(new BedPatDpt())
            //    select new BedPatDpt { dpt = i.dpt, beds = po.beds, obeds = po.obeds, ibeds = i.ibeds };

            //// full outer join: poiBeds and ipoBeds
            //var result = poiBeds.Union(ipoBeds, new BedPatDptComparer()).ToList();


            // using MoreLinq
            //// left outer join: pBeds and oBeds
            //var poBeds = pBeds.LeftJoin(oBeds, po => po.dpt,
            //    p => new BedPatDpt { dpt = p.dpt, beds = p.beds },
            //    (p, o) => new BedPatDpt { dpt = p.dpt, beds = p.beds, obeds = o.obeds });
            // full outer join: pBeds and oBeds
            var poBeds = pBeds.FullJoin(oBeds, po => po.dpt,
                p => new BedPatDpt { dpt = p.dpt, beds = p.beds },
                o => new BedPatDpt { dpt = o.dpt, obeds = o.obeds },
                (p, o) => new BedPatDpt { dpt = p.dpt, beds = p.beds, obeds = o.obeds });

            // full outer join: poBeds and iBeds
            var result = poBeds.FullJoin(iBeds, poi => poi.dpt,
                po => new BedPatDpt { dpt = po.dpt, beds = po.beds, obeds = po.obeds },
                i => new BedPatDpt { dpt = i.dpt, ibeds = i.ibeds },
                (po, i) => new BedPatDpt { dpt = po.dpt, beds = po.beds, obeds = po.obeds, ibeds = i.ibeds }).ToList();

            var dpts = (await DB.Mg_mnidRepository.QueryMg_mnid(new Mg_mnid { nid_id = "0501" }, 1)).Data;

            result = result.LeftJoin(dpts, r => r.dpt, d => d.nid_code,
               r => new BedPatDpt { dpt = r.dpt, beds = r.beds, obeds = r.obeds, ibeds = r.ibeds, dptName = string.Empty },
               (r, d) => new BedPatDpt { dpt = r.dpt, beds = r.beds, obeds = r.obeds, ibeds = r.ibeds, dptName = d.nid_name }).ToList();

            result.Add(new BedPatDpt
            {
                dpt = "total",
                dptName = "總計",
                beds = result.Sum(r => r.beds),
                obeds = result.Sum(r => r.obeds),
                ibeds = result.Sum(r => r.ibeds)
            });

            return new ApiResult<List<BedPatDpt>>(result);
        }

        /// <summary>
        /// 查詢不排床註記
        /// </summary>
        public async Task<ApiResult<List<Mi_mbed_Ext>>> GetLockBed(Mi_mbed param)
        {
            string sql = @"
            select *
            from mi_mbed
            where bed_unit = @bed_unit
            and substring(bed_group1,40,1) not in ('X','Y') 
            and bed_i_mark = '2'";

            var query = (await DBUtil.QueryAsync<Mi_mbed_Ext>(sql, param)).ToList();
            return new ApiResult<List<Mi_mbed_Ext>>(query);
        }

        /// <summary>
        /// 查詢預住/轉入
        /// </summary>
        public async Task<ApiResult<List<Mi_mbed_TranIn>>> GetTranIn(Mi_mbed param)
        {
            string sql = @"
            select bed.*
            from mi_mbed as bed
            left join mi_mbed as unit_i
            on (unit_i.bed_bed = substring(bed.bed_group1,61,6))
            where bed.bed_unit = @bed_unit
            and substring(bed.bed_group1,40,1) not in ('X','Y')
            and bed.bed_i_mark in ('1','4')
            and bed.bed_unit <> isnull(unit_i.bed_unit,'')";

            var bedList = (await DBUtil.QueryAsync<Mi_mbed_TranIn>(sql, param)).ToList();

            string bedCls = string.Empty;
            string patFrom = string.Empty;
            Ch_chgbed ch_chgbed;
            foreach (var b in bedList)
            {
                if (!b.bed_i_pat_no.IsNullOrWhiteSpace())
                {
                    Mh_mpat mh_mpat = new Mh_mpat();
                    mh_mpat.pat_no = b.bed_i_pat_no.ToNullableInt();
                    mh_mpat = (await DB.Mh_mpatRepository.GetMh_mpat(mh_mpat, 1)).Data.FirstOrDefault();
                    b.bed_i_pat_name = Utils.Medical.AnonymizeName(mh_mpat?.pat_name);
                    b.bed_i_pat_sex = mh_mpat?.pat_sex ?? string.Empty;
                }

                patFrom = b.bed_i_bed;

                if (!b.bed_i_pat_no.IsNullOrWhiteSpace() && !b.bed_bed.IsNullOrWhiteSpace())
                {
                    bedCls = (await DB.Ch_erresRepository.GetBedClassFromER(
                        new Ch_erres
                        {
                            erres_pat_no = b.bed_i_pat_no.ToNullableInt(),
                            erres_ipd_no = b.bed_i_ipd_no,
                            erres_given_bed = b.bed_bed
                        })).Data.FirstOrDefault()?.erres_res_lv;
                    if (!bedCls.IsNullOrWhiteSpace()) patFrom = "急診";

                    if (bedCls.IsNullOrWhiteSpace())
                    {
                        ch_chgbed = (await DB.Ch_chgbedRepository.GetBedClassFromInternal(
                            new Ch_chgbed
                            {
                                chgbed_pat_no = b.bed_i_pat_no.ToNullableInt(),
                                chgbed_bed_room = b.bed_bed.SubStr(0, 4),
                                chgbed_bed_no = b.bed_bed.SubStr(4, 2)
                            })).Data.FirstOrDefault();

                        bedCls = ch_chgbed?.chgbed_bed_cls;
                        if (!bedCls.IsNullOrWhiteSpace())
                        {
                            if (patFrom.IsNullOrWhiteSpace())
                                patFrom = ch_chgbed?.chgbed_original_bed;
                        }
                    }

                    if (bedCls.IsNullOrWhiteSpace())
                    {
                        bedCls = (await DB.Ch_resRepository.GetBedClassFromOPD(
                            new Ch_res
                            {
                                chres_pat_no = b.bed_i_pat_no.ToNullableInt(),
                                chres_ipd_no = b.bed_i_ipd_no.ToLong(),
                                chres_wait_bed = b.bed_bed
                            })).Data.FirstOrDefault()?.chres_grd_1;
                        if (!bedCls.IsNullOrWhiteSpace()) patFrom = "住等";
                    }

                    b.bed_i_need_bed_cls = SetBedCls(bedCls);
                }
                b.bed_i_pat_from = patFrom;
            }

            return new ApiResult<List<Mi_mbed_TranIn>>(bedList);
        }

        /// <summary>
        /// 設定病人等級名稱
        /// </summary>
        public string SetBedCls(string bedCls)
        {
            string result = string.Empty;
            switch (bedCls.SubStr(0, 1))
            {
                case "A":
                    result = "單人";
                    break;
                case "B":
                    result = "雙人";
                    break;
                case "C":
                    result = "健保";
                    break;
            }
            return result;
        }

        /// <summary>
        /// 查詢內轉床
        /// </summary>
        public async Task<ApiResult<List<Mi_mbed_TranInternal>>> GetTranInternal(Mi_mbed param)
        {
            string sql = @"
            select bed.*
            from mi_mbed as bed
            left join mi_mbed as unit_i
            on (unit_i.bed_bed = substring(bed.bed_group1,61,6))
            where bed.bed_unit = @bed_unit
            and substring(bed.bed_group1,40,1) not in ('X','Y')
            and bed.bed_i_mark in ('1','4')
            and bed.bed_unit = isnull(unit_i.bed_unit,'')";

            var bedList = (await DBUtil.QueryAsync<Mi_mbed_TranInternal>(sql, param)).ToList();

            foreach (var b in bedList)
            {
                b.bed_note = await DB.Ch_chgbedRepository.SetMessage(new Ch_chgbed
                {
                    chgbed_pat_no = b.bed_i_pat_no.ToNullableInt(),
                    chgbed_exe_status = "G",
                    chgbed_bed_room = b.bed_bed.SubStr(0, 4),
                    chgbed_bed_no = b.bed_bed.SubStr(4, 2)
                });
            }

            return new ApiResult<List<Mi_mbed_TranInternal>>(bedList);
        }

        /// <summary>
        /// 查詢預出/出院/轉出
        /// </summary>
        public async Task<ApiResult<List<Mi_mbed_TranOut>>> GetTranOut(Mi_mbed param)
        {
            // 狀態順序，顯示最後狀態即可：
            // 1. 預出院日期 ipd_preout_dt：1110325預出
            // 2. 轉出註記 bed_o_mark & bed_o_bed：A / P
            //     or 1 / 4 => 轉出+轉出註記床號
            // 3. 是否結帳 ipd_oc_end_yn：Y  => ★
            // 4. 清潔
            string sql = @"
            select bed.*, 
            substring(ipd.ipd_group2,212,7) as ipd_preout_dt,
            substring(ipd2_group2,151,1) as ipd2_out_hos_knd,
            substring(ipd.ipd_group2,15,1) as ipd_oc_end_yn
            from mi_mbed as bed
            left join mi_mipd as ipd
            on (ipd.ipd_no = substring(bed.bed_group1,119,11))
            left join mi_ipd2 as ipd2
            on (ipd2.ipd2_no = ipd.ipd_no)
            left join mi_mbed as unit_o
            on (unit_o.bed_bed = substring(bed.bed_group1,86,6))
            where bed.bed_unit = @bed_unit
            and substring(bed.bed_group1,40,1) not in ('X','Y')
            and (bed.bed_o_mark in ('1','4','A','P')
            or substring(ipd.ipd_group2,212,7) <> '')
            and bed.bed_unit <> isnull(unit_o.bed_unit,'')";

            var bedList = (await DBUtil.QueryAsync<Mi_mbed_TranOut>(sql, param)).ToList();

            // 清床清單
            var clearingList = (await DB.Mi_clrbedRepository.GetClearing(new Mi_clrbed { clrbed_unit = param.bed_unit })).Data;

            Mi_clrbed clearing;
            //List<string> tranoutSts = new();
            foreach (var b in bedList)
            {
                clearing = clearingList.FirstOrDefault(c => c.clrbed_bed == b.bed_bed);
                if (b.bed_pat_no.IsNullOrWhiteSpace())
                    b.bed_pat_no = clearing?.clrbed_pat_no.NullableToStr().PadLeft(8, '0') ?? string.Empty;

                // PatInfo
                if (!b.bed_pat_no.IsNullOrWhiteSpace())
                {
                    Mh_mpat mh_mpat = new Mh_mpat();
                    mh_mpat.pat_no = b.bed_pat_no.ToNullableInt();
                    mh_mpat = (await DB.Mh_mpatRepository.GetMh_mpat(mh_mpat, 1)).Data.FirstOrDefault();
                    b.bed_pat_name = Utils.Medical.AnonymizeName(mh_mpat?.pat_name);
                    b.bed_pat_sex = mh_mpat?.pat_sex ?? string.Empty;
                }

                // 狀態
                //tranoutSts.Clear();
                if (!b.ipd_preout_dt.IsNullOrWhiteSpace())
                    //tranoutSts.Add($"{DateTimeUtil.ConvertROC(b.ipd_preout_dt, outFormat: "yyyy-MM-dd")}預出");
                    b.bed_tranout_st = $"{DateTimeUtil.ConvertROC(b.ipd_preout_dt, outFormat: "yyyy-MM-dd")}{(b.ipd2_out_hos_knd == "A" ? "出準" : "預出")}";
                if (new HashSet<string>() { "1", "4" }.Contains(b.bed_o_mark))
                    //tranoutSts.Add($"轉出{b.bed_o_bed}");
                    b.bed_tranout_st = $"轉出{b.bed_o_bed}";
                else if (!b.bed_o_mark.IsNullOrWhiteSpace())
                    //tranoutSts.Add(b.bed_o_mark);
                    b.bed_tranout_st = b.bed_o_mark;
                if (b.ipd_oc_end_yn == "Y")
                    //tranoutSts.Add("★");
                    b.bed_tranout_st = "★";
                if (clearing != null)
                    //tranoutSts.Add(DB.Mi_clrbedRepository.SetSt(clearing.clrbed_status));
                    b.bed_tranout_st = DB.Mi_clrbedRepository.SetSt(clearing.clrbed_status);
                //b.bed_tranout_st = string.Join("、", tranoutSts);

                // 備註
                b.bed_note = await DB.Ch_chgbedRepository.SetMessage(new Ch_chgbed
                {
                    chgbed_pat_no = b.bed_pat_no.ToNullableInt(),
                    chgbed_exe_status = "G",
                    chgbed_bed_room = b.bed_bed.SubStr(0, 4),
                    chgbed_bed_no = b.bed_bed.SubStr(4, 2)
                });
                if (!string.IsNullOrWhiteSpace(clearing?.clrbed_memo))
                    b.bed_note += (b.bed_note == "" ? "" : Environment.NewLine) + clearing.clrbed_memo;
            }

            bedList = bedList.Where(b => !b.bed_pat_no.IsNullOrWhiteSpace()).ToList();

            return new ApiResult<List<Mi_mbed_TranOut>>(bedList);
        }

    }
}
