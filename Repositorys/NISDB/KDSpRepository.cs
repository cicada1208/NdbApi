using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class KDSpRepository : NISDBBaseRepository<KDSp>
    {
        /// <summary>
        /// 查詢 Kardex 特殊註記
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢最新AP-II</para>
        /// </summary>
        public async Task<ApiResult<List<KDSp>>> GetKDSp(KDSp param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 *
                    from ni_KDSp
                    where ptEncounterID = @ptEncounterID 
                    and Sp_Item = 'AP2'
                    and isnull(REC_STATUS,'') <> 'D'
                    order by Sp_Time desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<KDSp>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<KDSp>>(queryList);
        }

    }
}
