using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class SysParameterRepository : NISDBBaseRepository<SysParameter>
    {
        /// <summary>
        /// 查詢參數設定檔
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依參數查詢(多筆)</para>
        /// </summary>
        public async Task<ApiResult<List<SysParameter>>> GetSysParameter(SysParameter param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select * from ni_SysParameter
                    where parameterName in ({param.parameterName})";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<SysParameter>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<SysParameter>>(queryList);
        }

    }
}
