using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class CSAssignDetailRepository : NISDBBaseRepository<CSAssignDetail>
    {
        public async Task<ApiResult<List<CSAssignDetail>>> GetUnitCSAssignDetail(string clinicalUnitId, string assignDate)
        {
            string sql = @"
            select * from ni_CSAssignDetail
            where csassignMainId in (   
                select csassignMainId 
                from ni_CSAssignMain
                where clinicalUnitId = @clinicalUnitId 
                and assignDate = @assignDate
                and delDate is null
                and loginId in (
                    select distinct loginId 
                    from ni_CSDetail
                    where clinicalUnitId = @clinicalUnitId 
                    and csDate = @assignDate
                    and delDate is null
                ) 
            )
            and delDate is null";

            var query = (await DBUtil.QueryAsync<CSAssignDetail>(sql, new { clinicalUnitId, assignDate })).ToList();

            return new ApiResult<List<CSAssignDetail>>(query);
        }
    }
}
