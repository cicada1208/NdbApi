using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Mr_tsmitRepository : SYB1BaseRepository<Mr_tsmit>
    {
        /// <summary>
        /// 查詢運送等級
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依住院序號、病歷號，取得最新運送等級</para>
        /// </summary>
        public async Task<ApiResult<List<Mr_tsmit>>> GetMr_tsmit(Mr_tsmit param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 *, substring(tsmit_data,1,1) as tsmit_data_grade_id
                    from mr_tsmit
                    where tsmit_key = @tsmit_key
                    order by tsmit_mod_dt desc,tsmit_mod_time desc, tsmit_seq desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Mr_tsmit>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<Mr_tsmit>>(queryList);
        }

    }
}
