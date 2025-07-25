using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Ch_dnrRepository : SYB1BaseRepository<Ch_dnr>
    {
        /// <summary>
        /// 查詢 DNR
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依身分證字號</para>
        /// <para>3: 依病歷號</para>
        /// </summary>
        public async Task<ApiResult<List<Ch_dnr>>> GetCh_dnr(Ch_dnr param, int option = 0)
        {
            string sql = string.Empty;
            string select = @"
                    *, substring(dnr_tbl,143,1) as no143, substring(dnr_tbl,144,1) as no144,
                    substring(dnr_tbl,145,1) as no145, substring(dnr_tbl,146,1) as no146,
                    substring(dnr_tbl,147,1) as no147, substring(dnr_tbl,148,1) as no148,
                    substring(dnr_tbl,149,1) as no149, substring(dnr_tbl,150,1) as no150";

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select {select}
                    from ch_dnr
                    where dnr_pat_idno = @dnr_pat_idno";
                    break;
                case 3:
                    sql = $@"
                    select {select}
                    from ch_dnr
                    where dnr_pat_no = @dnr_pat_no";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Ch_dnr>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            switch (option)
            {
                case 2:
                case 3:
                    bool dnr = false, pre = false, agent = false, adexe = false, pread = false;
                    queryList.ForEach(d =>
                    {
                        d.dnr_sign_dt = DateTimeUtil.ConvertROC(d.dnr_tbl.SubStr(0, 7), outFormat: "yyyy-MM-dd");

                        switch (d.dnr_tbl_type)
                        {
                            case "P1":
                                d.dnr_info = "患者 - 預立選擇安寧緩和醫療意願書";
                                dnr = true;
                                break;
                            case "P2":
                                d.dnr_info = "患者 - 選擇安寧緩和醫療意願書";
                                dnr = true;
                                break;
                            case "P3":
                                d.dnr_info = "患者 - 預立不施行心肺復甦術意願書";
                                dnr = true;
                                break;
                            case "P4":
                                d.dnr_info = "患者 - 不施行心肺復甦術意願書";
                                dnr = true;
                                break;
                            case "P5":
                                d.dnr_info = "患者 - 預立醫療委任代理人委任書";
                                agent = true;
                                break;
                            case "P6":
                                d.dnr_info = "患者 - 預立選擇安寧緩和醫療意願書(非末期)";
                                if (d.no143 == "1")
                                {
                                    pre = true;
                                    d.dnr_chk_item.Add("同意安寧緩和醫療");
                                }
                                if (d.no144 == "1")
                                {
                                    pre = true;
                                    d.dnr_chk_item.Add("同意不施行維生醫療");
                                }
                                if (d.no145 == "1")
                                {
                                    pre = true;
                                    d.dnr_chk_item.Add("同意不施心肺復甦術");
                                }
                                if (d.no150 == "1")
                                    d.dnr_chk_item.Add("同意加註健保卡");
                                break;
                            case "P7":
                                d.dnr_info = "患者 - 預立選擇安寧緩和醫療意願書(末期)";
                                if (d.no145 == "1")
                                {
                                    dnr = true;
                                    d.dnr_chk_item.Add("同意不施心肺復甦術");
                                }
                                if (d.no146 == "1")
                                    d.dnr_chk_item.Add("同意不施心肺復甦術");
                                if (d.no143 == "1")
                                    d.dnr_chk_item.Add("同意安寧緩和醫療");
                                if (d.no144 == "1")
                                    d.dnr_chk_item.Add("同意不施行維生醫療");
                                if (d.no150 == "1")
                                    d.dnr_chk_item.Add("同意加註健保卡");
                                break;
                            case "R1":
                                d.dnr_info = "親屬 - 不施行心肺復甦術同意書(末期)";
                                if (d.no145 == "1")
                                {
                                    dnr = true;
                                    d.dnr_chk_item.Add("同意不施心肺復甦術");
                                }
                                if (d.no144 == "1")
                                    d.dnr_chk_item.Add("同意不施行維生醫療");
                                break;
                            case "R2":
                                d.dnr_info = "緩和醫療家庭諮詢會議紀錄1";
                                break;
                            case "R3":
                                d.dnr_info = "預立醫療照護諮商文件(ACP)記錄1";
                                pread = true;
                                break;
                            case "R4":
                                d.dnr_info = "預立醫療照護諮商文件(ACP)記錄2";
                                pread = true;
                                break;
                            case "R5":
                                d.dnr_info = "緩和醫療家庭諮詢會議紀錄2";
                                break;
                            case "R6":
                                d.dnr_info = "病人：生命末期病人臨終照護意願徵詢";
                                if (d.no144 == "1")
                                {
                                    dnr = true;
                                    d.dnr_chk_item.Add("持續目前醫療,直至生命終點");
                                }
                                if (d.no143 == "1")
                                    d.dnr_chk_item.Add("選擇安寧緩和醫療");
                                if (d.no145 == "1")
                                    d.dnr_chk_item.Add("於生命徵象不穩時,留一口氣回家");
                                if (d.no146 == "1")
                                    d.dnr_chk_item.Add("願意在腦死時做器官捐贈,遺愛人間");
                                if (d.no147 == "1")
                                    d.dnr_chk_item.Add("願意在離世後做組織捐贈,做有意義貢獻");
                                if (d.no148 == "1")
                                    d.dnr_chk_item.Add("遺體捐贈,願作大體老師,供醫學研究及教學");
                                break;
                            case "R8":
                                d.dnr_info = "預立醫療決議書(AD)簽署";
                                adexe = true;
                                break;
                            case "R9":
                                d.dnr_info = "預立醫療決議書(AD)更改";
                                adexe = true;
                                break;
                            case "IC":
                                d.dnr_info = d.dnr_tbl.SubStr(64, 80).Trim();
                                if (!d.dnr_info.IsNullOrWhiteSpace()) d.dnr_info = $"IC卡 - {d.dnr_info}";
                                HashSet<string> code = new HashSet<string>(new[] { "3", "4", "6", "7", "D", "E", "G", "H", "I", "J", "N", "O", "Q", "V", "W", "Y", "Z" });
                                if (code.Contains(d.dnr_tbl.SubStr(62, 1)))
                                    pre = true;
                                code = new HashSet<string>(new[] { "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" });
                                if (code.Contains(d.dnr_tbl.SubStr(62, 1)))
                                    pread = true;
                                break;
                        }

                        if (!d.dnr_sign_dt.IsNullOrWhiteSpace()) d.dnr_info += $"：於{d.dnr_sign_dt}簽署。";
                    });

                    string dnr_type = string.Empty;
                    if (dnr)
                        dnr_type = "DNR";
                    else if (pre)
                        dnr_type = "預立";
                    else if (agent)
                        dnr_type = "代理人";
                    else
                        dnr_type = string.Empty;
                    if (adexe)
                        dnr_type += $"{Environment.NewLine}AD執行";
                    else if (pread)
                        dnr_type += $"{Environment.NewLine}預立AD";
                    queryList.ForEach(d => d.dnr_type = dnr_type);
                    break;
            }

            return new ApiResult<List<Ch_dnr>>(queryList);
        }

    }
}
