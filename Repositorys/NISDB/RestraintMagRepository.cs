using Lib;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class RestraintMagRepository : NISDBBaseRepository<RestraintMag>
    {
        /// <summary>
        /// 查詢約束管理
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢是否有未結約束</para>
        /// </summary>
        public async Task<ApiResult<List<RestraintMag>>> GetRestraintMag(RestraintMag param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 * 
                    from ni_RestraintMag
                    where ptEncounterID = @ptEncounterID
                    and isnull(RTEndDTM,'') = ''
                    and isnull(REC_STATUS,'') <> 'D'";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<RestraintMag>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<RestraintMag>>(queryList);
        }

        /// <summary>
        /// 查詢約束管理報表
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號、記錄日期起迄、結案狀態</para>
        /// </summary>
        public async Task<ApiResult<List<RestraintMag>>> GetRestraintMagRpt(RestraintMag param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select * 
                    from ni_RestraintMag
                    where ptEncounterID = @ptEncounterID
                    and REC_DTM between @REC_DTM_begin and @REC_DTM_end
                    {(param.RTEndDTM == "ON" ?
                    "and isnull(RTEndDTM,'') = ''" :
                    param.RTEndDTM == "CLOSE" ?
                    "and isnull(RTEndDTM,'') <> ''"
                    : string.Empty)}
                    and isnull(REC_STATUS,'') <> 'D'
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<RestraintMag>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            queryList.ForEach(rm =>
            {
                rm.REC_DTM = DateTimeUtil.ConvertAD(rm.REC_DTM, false,
                    "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
                rm.RTEndDTM = DateTimeUtil.ConvertAD(rm.RTEndDTM, false,
                    "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
                rm.RTReason = Regex.Replace(rm.RTReason, "///", "、");
                rm.RTRegion = Regex.Replace(rm.RTRegion, "///", "、");
                rm.RTTool = Regex.Replace(rm.RTTool, "///", "、");
                rm.RTMedicine = Regex.Replace(rm.RTMedicine, "///", "、");
                rm.RTComplication = Regex.Replace(rm.RTComplication, "///", "、");
            });

            return new ApiResult<List<RestraintMag>>(queryList);
        }

    }
}
