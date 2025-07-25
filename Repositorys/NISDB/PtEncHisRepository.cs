using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class PtEncHisRepository : NISDBBaseRepository<PtEncHis>
    {
        /// <summary>
        /// 查詢住院序號歷程
        /// <para>1: 依參數自動組建</para>
        /// </summary>
        public async Task<ApiResult<List<PtEncHis>>> GetPtEncHis(PtEncHis param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<PtEncHis>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<PtEncHis>>(queryList);
        }

    }
}
