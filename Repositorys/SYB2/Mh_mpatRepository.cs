using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB2
{
    public class Mh_mpatRepository : SYB2BaseRepository<Mh_mpat>
    {
        /// <summary>
        /// 查詢病人基本資料
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依病歷號多筆，查詢測試病歷號</para>
        /// </summary>
        public async Task<ApiResult<List<Mh_mpat>>> GetMh_mpat(Mh_mpat param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select *
                    from mh_mpat
                    where pat_no in ({param.pat_no_m})
                    and substring(pat_data_4,89,1) = 'Y'";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Mh_mpat>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            switch (option)
            {
                case 2:
                    break;
                default:
                    queryList.ForEach(pat =>
                    {
                        pat.pat_sex =
                        pat.pat_data_1.SubStr(0, 1) == "1" ? "男" :
                        pat.pat_data_1.SubStr(0, 1) == "2" ? "女" : "?";

                        pat.pat_age = DateTimeUtil.GetAge(pat.pat_birth_dt.NullableToStr(),
                            DateTime.Now.ToString("yyyy/MM/dd"), "yyyMMdd");
                    });
                    break;
            }

            return new ApiResult<List<Mh_mpat>>(queryList);
        }

    }
}
