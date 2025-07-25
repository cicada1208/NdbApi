using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class VSPulseRepository : NISDBBaseRepository<VSPulse>
    {
        /// <summary>
        ///  查詢脈博強度
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依單號多筆</para>
        /// </summary>
        public async Task<ApiResult<List<VSPulse>>> GetVSPulse(VSPulse param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select *
                    from ni_VSPulse
                    where REC_NO in ({param.REC_NO})
                    and isnull(REC_STATUS,'') <>'D'";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VSPulse>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<VSPulse>>(queryList);
        }

    }
}
