using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class ASE_BaseInfoRepository : NISDBBaseRepository<ASE_BaseInfo>
    {
        /// <summary>
        /// 查詢入院表單基本資料
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢最新入院表單基本資料</para>
        /// </summary>
        public async Task<ApiResult<List<ASE_BaseInfo>>> GetASE_BaseInfo(ASE_BaseInfo param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1* 
                    from ni_ASE_BaseInfo as info
                    left join ni_ASE as ase
                    on (ase.ptEncounterID=info.ptEncounterID and ase.DOC_CODE=info.DOC_CODE)
                    where info.ptEncounterID = @ptEncounterID
                    order by ase.ASE_DT+ase.ASE_TIME desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<ASE_BaseInfo>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<ASE_BaseInfo>>(queryList);
        }

    }
}
