using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Mi_clrbedRepository : SYB1BaseRepository<Mi_clrbed>
    {
        /// <summary>
        /// 查詢清床中/待清床清單
        /// </summary>
        public async Task<ApiResult<List<Mi_clrbed>>> GetClearing(Mi_clrbed param)
        {
            string sql = @"
            select *
            from mi_clrbed
            where clrbed_unit = @clrbed_unit
            and clrbed_status in ('0','1','2') --床位狀態 0:清床中 1:待清床(急) 2:待清床(緩) 9:已清床
            order by clrbed_sort_odr";

            var query = (await DBUtil.QueryAsync<Mi_clrbed>(sql, param)).ToList();
            return new ApiResult<List<Mi_clrbed>>(query);
        }

        public string SetSt(string lrbed_status)
        {
            string result = string.Empty;

            switch (lrbed_status)
            {
                case "0":
                    result = "清床中";
                    break;
                case "1":
                case "2":
                    result = "待清床";
                    break;
                case "9":
                    result = "已清床";
                    break;
            }

            return result;
        }

    }
}
