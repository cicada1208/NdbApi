using Lib;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class TubeMagRepository : NISDBBaseRepository<TubeMag>
    {
        /// <summary>
        /// 查詢管路管理
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號、三管種類，查詢未結管路</para>
        /// <para>3: 依NIS住院序號，查詢未結Chest tube</para>
        /// <para>4: 依NIS住院序號、三管種類，查詢是否有未結管路</para>
        /// <para>5: 依NIS住院序號，查詢是否有未結DL./Hickman</para>
        /// </summary>
        public async Task<ApiResult<List<TubeMag>>> GetTubeMag(TubeMag param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select tubemag.* 
                    from ni_TubeMag as tubemag
                    left join ni_Tube as tube
                    on (tube.TBID = tubemag.TBID)
                    where tubemag.ptEncounterID = @ptEncounterID
                    and isnull(tubemag.TreatType,'') not in ('2','3','4')
                    and isnull(tubemag.REC_STATUS,'') <> 'D'
                    and tube.TBKind = @TBKind
                    and tube.isActive = '1'";
                    break;
                case 3:
                    sql = @"
                    select * 
                    from ni_TubeMag
                    where ptEncounterID = @ptEncounterID
                    and TBType like 'Chest tube%'
                    and isnull(TreatType,'') not in ('2','3','4')
                    and isnull(REC_STATUS,'') <> 'D'";
                    break;
                case 4:
                    sql = @"
                    select top 1 tubemag.* 
                    from ni_TubeMag as tubemag
                    left join ni_Tube as tube
                    on (tube.TBID = tubemag.TBID)
                    where tubemag.ptEncounterID = @ptEncounterID
                    and isnull(tubemag.TreatType,'') not in ('2','3','4')
                    and isnull(tubemag.REC_STATUS,'') <> 'D'
                    and tube.TBKind = @TBKind
                    and tube.isActive = '1'";
                    break;
                case 5:
                    sql = @"
                    select top 1 * 
                    from ni_TubeMag
                    where ptEncounterID = @ptEncounterID
                    and (TBType like 'Hickman%' or TBType like 'Double-Lumen%' or TBType like 'Double J%')
                    and isnull(TreatType,'') not in ('2','3','4')
                    and isnull(REC_STATUS,'') <> 'D'";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<TubeMag>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<TubeMag>>(queryList);
        }

        /// <summary>
        /// 查詢管路管理報表
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號、記錄日期起迄、管路或三管種類、結案狀態</para>
        /// </summary>
        public async Task<ApiResult<List<TubeMag>>> GetTubeMagRpt(TubeMag param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = $@"
                    select tubemag.*,
                    case when tubemag.TreatType = '1' then '續用'
                    when tubemag.TreatType = '2' then '換管'
                    when tubemag.TreatType = '3' then '拔管'
                    when tubemag.TreatType = '4' then '已出院'
                    else '' end as TreatTypeName
                    from ni_TubeMag as tubemag
                    {(!param.TBKind.IsNullOrWhiteSpace() ?
                    @"left join ni_Tube as tube
                    on (tube.TBID = tubemag.TBID)" : string.Empty)}
                    where tubemag.ptEncounterID = @ptEncounterID
                    and tubemag.REC_DTM between @REC_DTM_begin and @REC_DTM_end
                    {(!param.TBKind.IsNullOrWhiteSpace() ?
                    @"and tube.TBKind = @TBKind
                    and tube.isActive = '1'" : string.Empty)}
                    {(!param.TBType.IsNullOrWhiteSpace() ? "and tubemag.TBType like @TBType" : string.Empty)}
                    {(param.TreatType == "ON" ?
                    "and tubemag.TreatType in (null, '1')" :
                    param.TreatType == "CLOSE" ?
                    "and tubemag.TreatType in ('2', '3', '4')"
                    : string.Empty)}
                    and isnull(tubemag.REC_STATUS,'') <> 'D'
                    order by tubemag.REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<TubeMag>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            queryList.ForEach(tm =>
            {
                tm.REC_DTM = DateTimeUtil.ConvertAD(tm.REC_DTM, false,
                    "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
                tm.TBRegion = tm.TBRegion.Split("-").LastOrDefault() ?? string.Empty;
                tm.NextChangeDTM = DateTimeUtil.ConvertAD(tm.NextChangeDTM, false,
                    "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
                tm.TBTreatDTM = DateTimeUtil.ConvertAD(tm.TBTreatDTM, false,
                    "yyyyMMddHHmmss", "yyyy-MM-dd HH:mm:ss");
            });

            return new ApiResult<List<TubeMag>>(queryList);
        }

    }
}
