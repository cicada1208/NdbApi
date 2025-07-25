using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.Inf
{
    public class Gas102Repository : InfBaseRepository<Gas102>
    {
        public async Task<ApiResult<List<Gas102>>> GetPatTransfer(string patno, string dispatchTimeBegin, string dispatchTimeEnd)
        {
            string sql = @"
            select * from gas102
            where right(replicate('0', 8) + gas102_22, 8) = @patno
            and gas102_12 between @dispatchTimeBegin and @dispatchTimeEnd
            and GETDATE() >= gas102_12   
            and (gas102_14 is null or (gas102_14 is not null and GETDATE() <= gas102_14))
            and del='N'
            order by gas102_12 desc";

            var query = (await DBUtil.QueryAsync<Gas102>(sql, new { patno, dispatchTimeBegin, dispatchTimeEnd })).ToList();
            return new ApiResult<List<Gas102>>(query);
        }

    }
}
