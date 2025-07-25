using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class DHRLRepository : NISDBBaseRepository<DHRL>
    {
        /// <summary>
        /// 查詢出院準備服務高危險群篩選表
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢最新出院準備服務高危險群篩選表及轉介資訊</para>
        /// </summary>
        public async Task<ApiResult<List<DHRL>>> GetDHRL(DHRL param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 *
                    from ni_DHRL
                    where ptEncounterID=@ptEncounterID 
                    and isnull(REC_STATUS,'')<>'D'
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<DHRL>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            switch (option)
            {
                case 2:
                    Ch_tor ch_tor = null;
                    string date = string.Empty;

                    // 因此 case 只有單筆，不需非同步，故改為.Result
                    // 若在 queryList.ForEach(async d =>) 使用非同步，因未等待，在各個非同步執行完前，
                    // 可能就已經執行最下面的 return，可使用 Task.WhenAll 處理此狀況；
                    // 或改用 foreach( var d in queryList){}。
                    foreach (var d in queryList)
                    {
                        ch_tor = null;
                        if (!d.PT_NO.IsNullOrWhiteSpace() && !d.ptEncounterID.IsNullOrWhiteSpace())
                        {
                            ch_tor = (await DB.Ch_torRepository.GetCh_tor(new Ch_tor
                            {
                                chtor_pat_no = d.PT_NO.ToNullableInt(),
                                ptEncounterId = d.ptEncounterID
                            }, 4)).Data.FirstOrDefault(c => c.chtor_value_str == "Y");
                        }
                        if (ch_tor == null)
                        {
                            date = DateTimeUtil.ConvertAD(d.REC_DTM, false,
                               "yyyyMMddHHmmss", "yyyy-MM-dd");
                            d.Info = $"{date} " + (d.Referral == "Y" ?
                            "已轉介" : date.CompareTo(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")) <= 0 ?
                            "已評，大於7天須重評" : "已評，不需轉介");
                        }
                        else if (ch_tor.chtor_value_str == "Y")
                            d.Info = $"{DateTimeUtil.ConvertROC(ch_tor.chtor_cre_dt.ToString(), outFormat: "yyyy-MM-dd")} 已收案"; // 已收案: red
                    }
                    break;
            }

            return new ApiResult<List<DHRL>>(queryList);
        }

    }
}
