using Lib;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class VSPainRepository : NISDBBaseRepository<VSPain>
    {
        /// <summary>
        ///  查詢疼痛
        /// <para>1: 依參數自動組建</para>
        /// </summary>
        public async Task<ApiResult<List<VSPain>>> GetVSPain(VSPain param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VSPain>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<VSPain>>(queryList);
        }

        /// <summary>
        ///  查詢疼痛報表
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依單號多筆</para>
        /// <para>3: 依NIS住院序號，查詢24hr內最新Pain(記錄中取最大分，無法形容於無分數時顯示)</para>
        /// </summary>
        public async Task<ApiResult<List<VSPain>>> GetVSPainRpt(VSPain param, int option = 0)
        {
            string sql = string.Empty;
            string REC_DTM = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select VS.REC_DTM, P.REC_NO, P.SEQ_NO, P.ptEncounterID, P.DOC_CODE, P.PT_NO,
                    P.PainRegion, P.PainType, P.PainScale, P.NonDoReason_Pain,
                    P.MD_MAN, P.MD_NAME, P.MD_PC, P.MD_Version, P.MD_DT, P.MD_TIME, P.EvaluationForm
                    from ni_VSPain as P
                    left join ni_VitalSign as VS
                    on (P.REC_NO=VS.REC_NO)
                    where P.REC_NO in ({param.REC_NO})
                    union
                    select REC_DTM, REC_NO, '' as SEQ_NO, ptEncounterID, DOC_CODE, PT_NO,
                    '' as PainRegion, '' as PainType, 
                    case when NoPain = 'Y' then '0分' 
                    when NoPain = 'A' then 'NA'
                    else NoPain end as PainScale, 
                    '' as NonDoReason_Pain,
                    MD_MAN, MD_NAME, MD_PC, MD_Version, MD_DT, MD_TIME, '' as EvaluationForm
                    from ni_VitalSign
                    where REC_NO in ({param.REC_NO})
                    and isnull(REC_STATUS,'') <> 'D'
                    and isnull(NoPain,'') not in ('','N')";
                    break;
                case 3:
                    REC_DTM = DateTime.Now.AddDays(-1).ToString("yyyyMMddHHmmss");
                    sql = $@"
                    select top 1 * from (
                        select VS.REC_DTM, P.REC_NO, P.SEQ_NO, P.ptEncounterID, P.DOC_CODE, P.PT_NO,
                        P.PainRegion, P.PainType, P.PainScale, P.NonDoReason_Pain,
                        P.MD_MAN, P.MD_NAME, P.MD_PC, P.MD_Version, P.MD_DT, P.MD_TIME, P.EvaluationForm
                        from ni_VSPain as P
                        left join ni_VitalSign as VS
                        on (P.REC_NO=VS.REC_NO)
                        inner join (
                            select MAXVS.ptEncounterID, max(MAXVS.REC_DTM) as REC_DTM
                            from ni_VSPain as MAXP
                            left join ni_VitalSign as MAXVS
                            on (MAXP.REC_NO=MAXVS.REC_NO)
                            where MAXVS.ptEncounterID = @ptEncounterID
                            and MAXVS.REC_DTM > '{REC_DTM}'
                            and isnull(MAXVS.REC_STATUS,'') <> 'D'
                            group by MAXVS.ptEncounterID
                        ) as MAXDTM
                        on (MAXDTM.ptEncounterID = VS.ptEncounterID and MAXDTM.REC_DTM = VS.REC_DTM)
                        where VS.ptEncounterID = @ptEncounterID
                        and VS.REC_DTM > '{REC_DTM}'
                        and isnull(VS.REC_STATUS,'') <> 'D'
                        union
                        select REC_DTM, REC_NO, '' as SEQ_NO, ptEncounterID, DOC_CODE, PT_NO,
                        '' as PainRegion, '' as PainType, 
                        case when NoPain = 'Y' then '0分' 
                        when NoPain = 'A' then 'NA'
                        else NoPain end as PainScale, 
                        '' as NonDoReason_Pain, MD_MAN, MD_NAME, MD_PC, MD_Version, MD_DT, MD_TIME, 
                        '' as EvaluationForm
                        from ni_VitalSign
                        where ptEncounterID = @ptEncounterID
                        and REC_DTM > '{REC_DTM}'
                        and isnull(REC_STATUS,'') <> 'D'
                        and isnull(NoPain,'') not in ('','N')
                    ) as VSF
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<VSPain>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            queryList.ForEach(p =>
            {
                p.REC_DTM = DateTimeUtil.ConvertAD(p.REC_DTM, false,
                    "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
                SetPainVal(p);
            });

            switch (option)
            {
                case 3:
                    VSPain maxPainVal = queryList.Where(pain => pain.PainVal.IsNumeric())
                        .OrderByDescending(pain => pain.PainVal.ToInt()).FirstOrDefault();
                    if (maxPainVal != null)
                        maxPainVal = queryList.FirstOrDefault();
                    if (maxPainVal != null)
                    {
                        queryList.Clear();
                        queryList.Add(maxPainVal);
                    }
                    break;
            }

            return new ApiResult<List<VSPain>>(queryList);
        }

        public void SetPainVal(VSPain pain)
        {
            switch (pain.PainScale)
            {
                case "不痛":
                    pain.PainVal = "0";
                    break;
                case "微痛":
                    pain.PainVal = "2";
                    break;
                case "稍微":
                    pain.PainVal = "4";
                    break;
                case "中痛":
                    pain.PainVal = "6";
                    break;
                case "大痛":
                    pain.PainVal = "8";
                    break;
                case "最痛":
                    pain.PainVal = "10";
                    break;
                case "無法形容":
                    pain.PainVal = pain.PainScale;
                    break;
                default:
                    pain.PainVal = pain.PainScale.Replace("分", "");
                    break;
            }
        }

    }
}
