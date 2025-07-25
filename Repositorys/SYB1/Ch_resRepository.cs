using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Ch_resRepository : SYB1BaseRepository<Ch_res>
    {
        /// <summary>
        /// 查詢住等來源的病人需求床位等級 (來源：住等)
        /// </summary>
        public async Task<ApiResult<List<Ch_res>>> GetBedClassFromOPD(Ch_res param)
        {
            string sql = @"
            select *
            from ch_res
            where chres_pat_no = @chres_pat_no --來源病歷號
            and (chres_del_mark = 'S'  or (chres_del_mark = 'T' and chres_ipd_no = @chres_ipd_no)) --來源住院序號
            and chres_wait_bed = @chres_wait_bed --預約床位)";

            var query = (await DBUtil.QueryAsync<Ch_res>(sql, param)).ToList();
            return new ApiResult<List<Ch_res>>(query);
        }

    }
}
