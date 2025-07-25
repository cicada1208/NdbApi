using Lib;
using Models;
using Params;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Ch_torRepository : SYB1BaseRepository<Ch_tor>
    {
        /// <summary>
        /// 查詢 ch_tor
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依病歷號、住院序號，查詢最新身高</para>
        /// <para>3: 依病歷號、住院序號，查詢最新體重</para>
        /// <para>4: 依病歷號、NIS住院序號</para>
        /// </summary>
        public async Task<ApiResult<List<Ch_tor>>> GetCh_tor(Ch_tor param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 * from (
                        select ai_ctlmf_update_da as chtor_cre_dt, 0 as chtor_cre_ti, 
                        ai_ctlmf_body_height as chtor_value_num
                        from ai_ctlmf
                        where ai_ctlmf_pat_no = @chtor_pat_no
                        union
                        select chtor_cre_dt, chtor_cre_ti, chtor_value_num
                        from ch_tor
                        where chtor_pat_no = @chtor_pat_no
                        and chtor_item = 'BH'
                        and chtor_del_mark =''
                        and chtor_from_sys_1 = 'I'
                        and chtor_from_sys_2 = 'NIS'
                        union
                        select 0 as chtor_cre_dt, 0 as chtor_cre_ti,
                        case when isnumeric(chpre_value2)=1 then convert(decimal,chpre_value2)
                        else 0 end as chtor_value_num
                        from ch_pre
                        where chpre_sys_id = '01'
                        and chpre_ipd_no = @chtor_ipd_no
                        and chpre_type = 'Admission'
                        and chpre_item = 'Physical'
                        and chpre_id = '101'
                    ) as ch_tor
                    order by chtor_cre_dt desc, chtor_cre_ti desc";
                    break;
                case 3:
                    sql = @"
                    select top 1 * from (
                        select ai_ctlmf_update_da as chtor_cre_dt, 0 as chtor_cre_ti, 
                        ai_ctlmf_body_weight as chtor_value_num
                        from ai_ctlmf
                        where ai_ctlmf_pat_no = @chtor_pat_no
                        union
                        select chtor_cre_dt, chtor_cre_ti, chtor_value_num
                        from ch_tor
                        where chtor_pat_no = @chtor_pat_no
                        and chtor_item = 'BW'
                        and chtor_del_mark =''
                        and chtor_from_sys_1 = 'I'
                        and chtor_from_sys_2 = 'NIS'
                        union
                        select 0 as chtor_cre_dt, 0 as chtor_cre_ti, 
                        case when isnumeric(chpre_value2)=1 then convert(decimal,chpre_value2)
                        else 0 end as chtor_value_num
                        from ch_pre
                        where chpre_sys_id = '01'
                        and chpre_ipd_no = @chtor_ipd_no
                        and chpre_type = 'Admission'
                        and chpre_item = 'Physical'
                        and chpre_id = '102'
                    ) as ch_tor
                    order by chtor_cre_dt desc, chtor_cre_ti desc";
                    break;
                case 4:
                    string chtor_from_sys_1 = string.Empty;
                    switch (param.ptEncounterId.SubStr(0, 3))
                    {
                        case PatParam.FromType.NIS:
                            chtor_from_sys_1 = PatParam.FromType.I;
                            break;
                        case PatParam.FromType.EMG:
                            chtor_from_sys_1 = PatParam.FromType.E;
                            break;
                        case PatParam.FromType.OPD:
                            chtor_from_sys_1 = PatParam.FromType.O;
                            break;
                    }

                    string chtor_ipd_no = string.Empty;
                    if (!param.ptEncounterId.IsNullOrWhiteSpace())
                    {
                        var ptEncHisList = await DB.PtEncHisRepository.GetPtEncHis(
                            new PtEncHis() { ptEncounterId = param.ptEncounterId }, 1);
                        chtor_ipd_no = string.Join(",", ptEncHisList.Data.Select(p => p.ptEncHisId.SubStr(4, 11)));
                    }

                    sql = $@"
                    select * from ch_tor
                    where chtor_pat_no = @chtor_pat_no
                    and chtor_ipd_no in ({chtor_ipd_no})
                    and chtor_from_sys_1 = '{chtor_from_sys_1}'
                    and chtor_from_sys_2 = 'CSER' 
                    and chtor_item = 'AcceptCase'
                    and isnull(chtor_del_mark,'') = ''";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Ch_tor>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<Ch_tor>>(queryList);
        }

    }
}
