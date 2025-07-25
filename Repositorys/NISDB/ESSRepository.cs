using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class ESSRepository : NISDBBaseRepository<ESS>
    {
        /// <summary>
        /// 查詢情緒壓力篩檢表
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢最新情緒壓力篩檢表及轉介資訊</para>
        /// </summary>
        public async Task<ApiResult<List<ESS>>> GetESS(ESS param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 *
                    from ni_ESS
                    where ptEncounterID=@ptEncounterID 
                    and isnull(REC_STATUS,'')<>'D'
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<ESS>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            switch (option)
            {
                case 2:
                    List<Ch_cser> ch_csers = null;
                    string date = string.Empty;
                    var dic = new Dictionary<string, string>();
                    dic.Add("12", "腫瘤心理師");
                    dic.Add("06", "社工室");
                    dic.Add("02", "院牧部");

                    foreach (var e in queryList)
                    {
                        ch_csers = null;
                        if (!e.PT_NO.IsNullOrWhiteSpace())
                        {
                            ch_csers =(await DB.Ch_cserRepository.GetCh_cser(new Ch_cser
                            {
                                cser_t_class = "'12','06','02'",
                                cser_pat_no = e.PT_NO.ToNullableInt(),
                                cser_ipd_dt = e.ptEncounterID.SubStr(4, 7).ToNullableInt(),
                                cser_t_dt = DateTimeUtil.ConvertAD(e.REC_DTM,
                                inFormat: "yyyyMMddHHmmss", outFormat: "yyyMMdd").ToNullableInt()
                            }, 2)).Data;
                        }
                        if (ch_csers != null && ch_csers.Exists(c => c.cser_r_dt.HasValue && c.cser_r_dt > 0))
                        { // 有回覆日期即代表收案
                            e.Info = string.Join(Environment.NewLine, ch_csers.Select(c =>
                            {
                                date = DateTimeUtil.ConvertROC(c.cser_r_dt.ToString(), outFormat: "yyyy-MM-dd");
                                return $"{date}{(!date.IsNullOrWhiteSpace() ? " " : "")}{dic.GetValueOrDefault(c.cser_t_class)} {(!date.IsNullOrWhiteSpace() ? "已收案" : "未收案")}"; // 已收案: red
                            }));
                        }
                        else if (ch_csers != null && ch_csers.Count > 0)
                        { // 已轉介
                            date = DateTimeUtil.ConvertAD(e.REC_DTM, false, "yyyyMMddHHmmss", "yyyy-MM-dd");
                            e.Info = $"{date} {string.Join("、", ch_csers.Select(c => dic.GetValueOrDefault(c.cser_t_class)))} 已轉介";
                        }
                        else
                        { // 未轉介
                            date = DateTimeUtil.ConvertAD(e.REC_DTM, false, "yyyyMMddHHmmss", "yyyy-MM-dd");
                            e.Info = $"{date} " + (date.CompareTo(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd")) <= 0 ?
                            "已評，大於7天須重評" : "已評，不需轉介");
                        }
                    }
                    break;
            }

            return new ApiResult<List<ESS>>(queryList);
        }

        /// <summary>
        /// 情緒壓力篩檢表腫瘤篩選
        /// </summary>
        public bool ESSDTFilter(dynamic param)
        {
            return param.encDiagNo1.CompareTo("140") >= 0 && param.encDiagNo1.CompareTo("209") <= 0 && (param.encDiagNo1 as string).SubStr(0, 3) != "176" ||
                param.encDiagNo2.CompareTo("140") >= 0 && param.encDiagNo2.CompareTo("209") <= 0 && (param.encDiagNo2 as string).SubStr(0, 3) != "176" ||
                param.encDiagNo3.CompareTo("140") >= 0 && param.encDiagNo3.CompareTo("209") <= 0 && (param.encDiagNo3 as string).SubStr(0, 3) != "176" ||
                param.encDiagNo4.CompareTo("140") >= 0 && param.encDiagNo4.CompareTo("209") <= 0 && (param.encDiagNo4 as string).SubStr(0, 3) != "176" ||
                param.encDiagNo5.CompareTo("140") >= 0 && param.encDiagNo5.CompareTo("209") <= 0 && (param.encDiagNo5 as string).SubStr(0, 3) != "176";
        }

    }
}
