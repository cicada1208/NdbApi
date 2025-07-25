using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.PeriPhery
{
    public class APACHE1Repository : PeriPheryBaseRepository<APACHE1>
    {
        public async Task<ApiResult<List<APACHE1>>> GetLatestAP2(string PATNO, string INDATE_Begin, string INDATE_End)
        {
            string sql = @"
            select top 1 AP1.*
            from PERIPHERY.dbo.APACHE0 as AP0
            left join PERIPHERY.dbo.APACHE1 as AP1
            on AP0.APAUNIKEY = AP1.APAUNIKEY
            where FORMAT(CAST(AP0.PATNO as int),'00000000') = @PATNO
            and (
                AP0.INDATE between @INDATE_Begin and @INDATE_End
                or AP0.OUTDATE = ''
            )
            and AP1.STATUS in ('1','2')
            order by AP0.INDATE desc, AP0.INTIME desc";

            var query = (await DBUtil.QueryAsync<APACHE1>(sql, new { PATNO, INDATE_Begin, INDATE_End })).ToList();
            return new ApiResult<List<APACHE1>>(query);
        }

    }
}
