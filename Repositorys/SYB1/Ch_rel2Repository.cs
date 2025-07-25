using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Ch_rel2Repository : SYB1BaseRepository<Ch_rel2>
    {
        /// <summary>
        /// 查詢 ch_rel2
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依病歷號、報告日期7天內，查詢最新SCr</para>
        /// <para>3: 依病歷號、報告日期7天內，查詢最新K</para>
        /// <para>4: 依病歷號，查詢血液感染</para>
        /// </summary>
        public async Task<ApiResult<List<Ch_rel2>>> GetCh_rel2(Ch_rel2 param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 *, substring(chrel2_filler,6,10) as chrel2_ctm_unit
                    from ch_rel2
                    where chrel2_pat_no = @chrel2_pat_no
                    and chrel2_itm_cd in ('0421B','0421Y')
                    and chrel2_rp_date >= convert(int,convert(varchar, dateadd(dd, -7, getdate()), 112))-19110000
                    order by chrel2_rp_date desc";
                    break;
                case 3:
                    sql = @"
                    select top 1 *, substring(chrel2_filler,6,10) as chrel2_ctm_unit
                    from ch_rel2
                    where chrel2_pat_no = @chrel2_pat_no
                    and chrel2_itm_cd in ('0427B','0427Y')
                    and chrel2_rp_date >= convert(int,convert(varchar, dateadd(dd, -7, getdate()), 112))-19110000
                    order by chrel2_rp_date desc";
                    break;
                case 4:
                    sql = @"
                    select top 1 *
                    from ch_rel2
                    where chrel2_pat_no = @chrel2_pat_no
                    and chrel2_itm_cd in ('0801','0827','0834')
                    and upper(substring(chrel2_ctm_value,1,8)) in ('REACTIVE','POSITIVE')
                    union
                    select top 1 rel2.*
                    from ch_rel2 as rel2
                    left join mt_mchk as chk
                    on chrel2_dpt_no = htchk_dpt_cd and chrel2_cls_cd = htchk_cls_cd 
                    and chrel2_pr_seq = htchk_pr_seq and chrel2_ip_date = htchk_ip_date 
                    where chrel2_pat_no = @chrel2_pat_no
                    and chrel2_itm_cd in ('0835','0835A')
                    and (substring(htchk_ip_data,12,7) in ('B02','Z21')
                    or substring(htchk_ip_data,19,7) in ('B02','Z21')
                    or substring(htchk_ip_data,26,7) in ('B02','Z21')
                    or substring(htchk_ip_data,12,7) in ('042','V08')
                    or substring(htchk_ip_data,19,7) in ('042','V08')
                    or substring(htchk_ip_data,16,7) in ('042','V08'))
                    union
                    select top 1 rel2.*
                    from ch_rel2 as rel2
                    left join mt_mchk as chk
                    on chrel2_dpt_no = htchk_dpt_cd and chrel2_cls_cd = htchk_cls_cd 
                    and chrel2_pr_seq = htchk_pr_seq and chrel2_ip_date = htchk_ip_date
                    where chrel2_pat_no = @chrel2_pat_no
                    and chrel2_itm_cd in ('0835','0835A')
                    and chrel2_ctm_value <> 'TND'
                    and (substring(htchk_ip_data,12,7) not in ('B02','Z21')
                    or substring(htchk_ip_data,19,7) not in ('B02','Z21')
                    or substring(htchk_ip_data,26,7) not in ('B02','Z21'))";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Ch_rel2>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<Ch_rel2>>(queryList);
        }
    }
}
