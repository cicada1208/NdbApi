using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB2
{
    public class Ch_tabRepository : SYB2BaseRepository<Ch_tab>
    {
        /// <summary>
        /// 查詢值班班表
        /// </summary>
        public async Task<ApiResult<List<Ch_tab_dhid>>> GetDrDutyScheduleTab(string clinicalUnitId)
        {
            string sql = string.Empty;
            List<Ch_tab_dhid> query = new();

            if (clinicalUnitId.IsNullOrWhiteSpace())
                goto exit;

            var dhids = (await DB.Ch_dhidRepository.GetDrDutyScheduleDhid(clinicalUnitId)).Data;
            var schcodes = string.Join(",", dhids.Select(d => "'" + d.dhid_schcode + "'"));

            if (schcodes.IsNullOrWhiteSpace())
                goto exit;

            DateTime now = DateTime.Now;
            string tab_ym = DateTimeUtil.ConvertAD(now.ToString("yyyyMMdd"), outFormat: "yyyMM");
            string tab_date = $"'{DateTimeUtil.ConvertAD(now.ToString("yyyyMMdd"), outFormat: "yyyMMdd")}','{DateTimeUtil.ConvertAD(now.AddDays(-1).ToString("yyyyMMdd"), outFormat: "yyyMMdd")}'";

            sql = $@"
            select tab.*, dhid_dr_type
            from ch_tab as tab
            left join ch_dhid as dhid
            on (dhid_schcode=tab_schcode)
            where tab_ym='{tab_ym}'
            and tab_schcode in ({schcodes})
            and tab_date in ({tab_date})
            and tab_id in ('4','5','6')";

            query = (await DBUtil.QueryAsync<Ch_tab_dhid>(sql)).ToList();

            long begDtm, endDtm, nowDtm;
            string endDt = string.Empty;
            query = query.Where(t =>
            {
                if (t.tab_type == "1")
                {
                    begDtm = (t.tab_date + t.tab_time_beg.NullableToStr().PadLeft(6, '0')).ToLong();
                    if (t.tab_time_end <= t.tab_time_beg)
                        endDt = Utils.DateTime.ROCNextDay(t.tab_date, 1);
                    else
                        endDt = t.tab_date;
                    endDtm = (endDt + t.tab_time_end.NullableToStr().PadLeft(6, '0')).ToLong();
                    nowDtm = DateTimeUtil.ConvertAD(now.ToString("yyyyMMddHHmmss"),
                        inFormat: "yyyyMMddHHmmss",
                        outFormat: "yyyMMddHHmmss").ToLong();
                    return nowDtm >= begDtm && nowDtm <= endDtm;
                }
                else return true;
            }).ToList();

        exit:
            return new ApiResult<List<Ch_tab_dhid>>(query);
        }

    }
}
