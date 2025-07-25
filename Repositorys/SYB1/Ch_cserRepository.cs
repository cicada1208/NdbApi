using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB1
{
    public class Ch_cserRepository : SYB1BaseRepository<Ch_cser>
    {
        /// <summary>
        /// 查詢轉介
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依轉介類別(多筆)、病歷號、>=住院日期、轉介日期</para>
        /// </summary>
        public async Task<ApiResult<List<Ch_cser>>> GetCh_cser(Ch_cser param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select * from ch_cser
                    where cser_t_class in ({param.cser_t_class})
                    and cser_pat_no = @cser_pat_no
                    and cser_ipd_dt >= @cser_ipd_dt
                    and cser_t_dt = @cser_t_dt
                    and cser_mark <> 'D'";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Ch_cser>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<Ch_cser>>(queryList);
        }

    }
}
