using Models;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class FocusRecordRepository : NISDBBaseRepository<FocusRecord>
    {
        /// <summary>
        /// 查詢護理焦點
        /// <para>1: 依參數自動組建</para>
        /// </summary>
        public async Task<ApiResult<List<FocusRecord>>> GetFocusRecord(FocusRecord param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<FocusRecord>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<FocusRecord>>(queryList);
        }

        /// <summary>
        /// 查詢病人焦點最新狀態
        /// <para>1: 依NIS住院序號，查詢病人全部焦點最新狀態</para>
        /// <para>2: 依NIS住院序號，查詢病人未結焦點最新狀態</para>
        /// </summary>
        public async Task<ApiResult<List<FocusRecord>>> GetFocusRecordLatest(FocusRecord param, int option = 0)
        {
            string sql = @"
                    select frec.*, fitem.focusNo 
                    from (
                        select A1.focusItemId, A1.focusName, max(A1.recordDate) as recordDate
                        from (
                            select A11.focusItemId, A11.focusName, A11.recordDate
                            from ni_FocusRecord as A11 with (index = ni_FocusRecord_2)
                            where A11.ptEncounterId=@ptEncounterId and A11.isActive=1
                        ) As A1
                        group by A1.focusItemId, A1.focusName
                    ) As frecg
                    inner join ni_FocusRecord as frec with (index = ni_FocusRecord_2)
                    on ((frecg.focusItemId=frec.focusItemId or frecg.focusItemId is null) 
                    and frecg.focusName=frec.focusName and frecg.recordDate=frec.recordDate)
                    left join ni_FocusItem as fitem
                    on (frecg.focusItemId=fitem.focusItemId and fitem.isActive=1) 
                    where frec.ptEncounterId=@ptEncounterId
                    and frec.isActive=1";

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql += @"
                    and frec.focusStatus not in (
                        select convert(smallint, termlib.value)
                        from ni_TermLibCatalog as termlibc
                        left join ni_TermLib as termlib with (index = ni_TermLib_1)
                        on termlib.termLibCatalogId = termlibc.termLibCatalogId
                        where termlibc.label='FocusStatusOver'
                        and termlibc.isActive=1
                        and termlib.isActive=1
                    )";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<FocusRecord>(sql, param, schemaOnly: true);
            var queryList = query.DistinctBy(fr => new { fr.focusItemId, fr.focusName }).ToList();

            return new ApiResult<List<FocusRecord>>(queryList);
        }

    }
}
