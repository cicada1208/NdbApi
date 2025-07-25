using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Ch_hivcaRepository : SYB1BaseRepository<Ch_hivca>
    {
        /// <summary>
        /// 查詢 ch_hivca
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依病歷號，查詢血液感染</para>
        /// </summary>
        public async Task<ApiResult<List<Ch_hivca>>> GetCh_hivca(Ch_hivca param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 * 
                    from ch_hivca
                    where hivca_cn_pat = @hivca_cn_pat";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Ch_hivca>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<Ch_hivca>>(queryList);
        }

    }
}
