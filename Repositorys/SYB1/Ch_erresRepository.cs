using Lib;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Ch_erresRepository : SYB1BaseRepository<Ch_erres>
    {
        /// <summary>
        /// 查詢急診來源的病人需求床位等級 (來源：急診)
        /// </summary>
        public async Task<ApiResult<List<Ch_erres>>> GetBedClassFromER(Ch_erres param)
        {
            string sql = @"
            select *
            from ch_erres
            where erres_pat_no = @erres_pat_no --來源病歷號
            and ( erres_status = ' '  or (erres_status = 'S' and erres_ipd_no = @erres_ipd_no )) --來源住院序號
            and erres_given_bed = @erres_given_bed --預約床位";

            var query = (await DBUtil.QueryAsync<Ch_erres>(sql, param)).ToList();
            return new ApiResult<List<Ch_erres>>(query);
        }

    }
}
