using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class VSBTRepository : NISDBBaseRepository<VSBT>
    {
        /// <summary>
        ///  查詢體溫
        /// <para>1: 依參數自動組建</para>
        /// </summary>
        public async Task<ApiResult<List<VSBT>>> GetVSBT(VSBT param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VSBT>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<VSBT>>(queryList);
        }

        /// <summary>
        ///  查詢體溫報表
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依單號多筆</para>
        /// </summary>
        public async Task<ApiResult<List<VSBT>>> GetVSBTRpt(VSBT param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select VS.REC_DTM, BT.*
                    from ni_VSBT as BT
                    left join ni_VitalSign as VS
                    on (BT.REC_NO=VS.REC_NO)
                    where BT.REC_NO in ({param.REC_NO})";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VSBT>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            queryList.ForEach(bt =>
            {
                bt.REC_DTM = DateTimeUtil.ConvertAD(bt.REC_DTM, false,
                    "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");

                if (bt.BTUnit == "℉" && bt.BTNum.IsNumeric())
                {
                    bt.BTNum = Math.Round((bt.BTNum.ToDouble() - 32) * 5 / 9, 1, MidpointRounding.AwayFromZero).ToString();
                    bt.BTUnit = "℃";
                }
            });

            return new ApiResult<List<VSBT>>(queryList);
        }

    }
}
