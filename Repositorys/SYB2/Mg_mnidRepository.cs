using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB2
{
    public class Mg_mnidRepository : SYB2BaseRepository<Mg_mnid>
    {
        /// <summary>
        /// 查詢設定檔
        /// <para>1: 依參數自動組建</para>
        /// </summary>
        public async Task<ApiResult<List<Mg_mnid>>> QueryMg_mnid(Mg_mnid param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Mg_mnid>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<Mg_mnid>>(queryList);
        }

        /// <summary>
        /// 查詢人員資料
        /// <para>1: 依員編，查詢人員資料</para>
        /// </summary>
        public async Task<ApiResult<List<Mg_mnid>>> GetUser(Mg_mnid param, int option = 0)
        {
            string sql = string.Empty;
            List<Mg_mnid> queryList = null;

            switch (option)
            {
                case 1:
                    sql = @"
                    select nid_code as UserId, nid_name as UserName, 
                    case when substring(nid_rec,35,2)='Z0' then 'Y'
                    else 'N' end as Dimission
                    from mg_mnid
                    where nid_id='5100' 
                    and nid_code=@UserId";

                    var query = await DBUtil.QueryIntgrAsync<Mg_mnid>(sql, param);
                    queryList = query.ToList();
                    break;
            }

            return new ApiResult<List<Mg_mnid>>(queryList);
        }

        /// <summary>
        /// 查詢醫師
        /// </summary>
        public async Task<ApiResult<List<Mg_mnid_Dr>>> GetDr(Mg_mnid_Dr param)
        {
            param.nid_id = "0503";
            var query = (await DBUtil.QueryAsync<Mg_mnid_Dr>(param)).ToList();
            return new ApiResult<List<Mg_mnid_Dr>>(query);
        }

    }
}
