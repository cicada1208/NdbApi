using Lib;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class VSBPRepository : NISDBBaseRepository<VSBP>
    {
        /// <summary>
        ///  查詢血壓
        /// <para>1: 依參數自動組建</para>
        /// </summary>
        public async Task<ApiResult<List<VSBP>>> GetVSBP(VSBP param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VSBP>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<VSBP>>(queryList);
        }

        /// <summary>
        ///  查詢血壓報表
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依單號多筆</para>
        /// </summary>
        public async Task<ApiResult<List<VSBP>>> GetVSBPRpt(VSBP param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select VS.REC_DTM, BP.*
                    from ni_VSBP as BP
                    left join ni_VitalSign as VS
                    on (BP.REC_NO=VS.REC_NO)
                    where BP.REC_NO in ({param.REC_NO})";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VSBP>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            queryList.ForEach(bt =>
            {
                bt.REC_DTM = DateTimeUtil.ConvertAD(bt.REC_DTM, false,
                    "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
            });

            return new ApiResult<List<VSBP>>(queryList);
        }

    }
}
