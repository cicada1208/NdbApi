using Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class OPNSChkRecRepository : NISDBBaseRepository<OPNSChkRec>
    {
        /// <summary>
        /// 查詢手術護理查核單
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依病歷號、手術日期、手術單號，查詢最新手術護理查核單</para>
        /// </summary>
        public async Task<ApiResult<List<OPNSChkRec>>> GetOPNSChkRec(OPNSChkRec param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    string OPDTBegin = param.OPDT;
                    string OPDTEnd = param.OPDT;
                    if (DateTime.TryParseExact(param.OPDT, "yyyy/MM/dd", null, DateTimeStyles.None, out DateTime OPDT))
                    {
                        OPDTBegin = OPDT.AddDays(-10).ToString("yyyy/MM/dd");
                        OPDTEnd = OPDT.ToString("yyyy/MM/dd");
                    }
                    sql = $@"
                    select top 1* 
                    from ni_OPNSChkRec
                    where PT_NO = @PT_NO
                    and OPDT between '{OPDTBegin}' and '{OPDTEnd}'
                    and OPNO like '{param.OPNO}'
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<OPNSChkRec>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<OPNSChkRec>>(queryList);
        }
    }
}
