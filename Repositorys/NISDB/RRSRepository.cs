using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class RRSRepository : NISDBBaseRepository<RRS>
    {
        /// <summary>
        /// 查詢 CAS
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢24hr內最新CAS及轉介資訊</para>
        /// </summary>
        public async Task<ApiResult<List<RRS>>> GetRRS(RRS param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    string REC_DTM = DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss");
                    sql = $@"
                    select top 1 *
                    from ni_RRS
                    where ptEncounterID=@ptEncounterID 
                    and REC_DTM > '{REC_DTM}'
                    and isnull(REC_STATUS,'')<>'D'
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<RRS>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            switch (option)
            {
                case 2:
                    queryList.ForEach(r =>
                    {
                        switch (r.SendType)
                        {
                            case "1":
                                r.Info = $"項目：{r.Condition}，已通知Leader";
                                break;
                            case "2":
                                r.Info = $"項目：{r.Condition}，{r.SendTime} 已通知醫師 {r.SendToName}";
                                break;
                            case "3":
                                r.Info = $"項目：{r.Condition}，醫師已知";
                                break;
                            case "4":
                                r.Info = $"項目：{r.Condition}，通知醫療團隊";
                                break;
                        }
                    });
                    break;
            }

            return new ApiResult<List<RRS>>(queryList);
        }

    }
}
