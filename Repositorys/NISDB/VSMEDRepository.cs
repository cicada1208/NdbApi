using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class VSMEDRepository : NISDBBaseRepository<VSMED>
    {
        /// <summary>
        ///  查詢藥物
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依單號多筆</para>
        /// </summary>
        public async Task<ApiResult<List<VSMED>>> GetVSMED(VSMED param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select *
                    from ni_VSMED 
                    where REC_NO in ({param.REC_NO})
                    and isnull(REC_STATUS,'') <>'D'";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VSMED>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<VSMED>>(queryList);
        }

    }
}
