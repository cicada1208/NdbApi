using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Mc_mic2Repository : SYB1BaseRepository<Mc_mic2>
    {
        /// <summary>
        /// 查詢 mc_mic2
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依病歷號，查詢血液感染</para>
        /// </summary>
        public async Task<ApiResult<List<Mc_mic2>>> GetMc_mic2(Mc_mic2 param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 * 
                    from mc_mic2
                    where hcic2_ptno = @hcic2_ptno
                    and hcic2_id2no in ('042','044')
                    and hcic2_status<>'D'";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Mc_mic2>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<Mc_mic2>>(queryList);
        }

    }
}
