using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class RecShortCodeRepository : NISDBBaseRepository<RecShortCode>
    {
        /// <summary>
        /// 查詢設定檔
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依RECMODEL、CODEVER，查詢最新版本選項</para>
        /// </summary>
        public async Task<ApiResult<List<RecShortCode>>> GetRecShortCode(RecShortCode param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select code.* 
                    from ( 
                        select RECMODEL, GROUPCODE, SHORTCODE, Max(CODEVER) as CODEVER
                        from ni_RecShortCode
                        where RECMODEL = @RECMODEL
                        and CODEVER <= @CODEVER
                        group by RECMODEL, GROUPCODE, SHORTCODE
                    ) as maxcode
                    left join ni_RecShortCode as code
                    on (code.RECMODEL = maxcode.RECMODEL and code.GROUPCODE = maxcode.GROUPCODE
                    and code.SHORTCODE = maxcode.SHORTCODE and code.CODEVER = maxcode.CODEVER)
                    order by GROUPSEQ asc, CODESEQ asc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<RecShortCode>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<RecShortCode>>(queryList);
        }

    }
}
