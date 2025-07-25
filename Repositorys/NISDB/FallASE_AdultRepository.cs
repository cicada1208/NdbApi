using Lib;
using Models;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class FallASE_AdultRepository : NISDBBaseRepository<FallASE_Adult>
    {
        /// <summary>
        /// 查詢跌倒資料
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢最新跌倒資料</para>
        /// </summary>
        public async Task<ApiResult<List<FallASE_Adult>>> GetFallASE_Adult(FallASE_Adult param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 *
                    from ( 
                        select *
                        from ni_FallASE_Adult
                        where ptEncounterID=@ptEncounterID 
                        and isnull(REC_STATUS,'')<>'D'
                        union
                        select *,
                        '' as ASEItem1C,'' as ASEItem2C,'' as ASEItem3C,'' as ASEItem4C,
                        '' as ASEItem5C,'' as ASEItem6C,'' as ASEItem7C,'' as ASEItem8C,
                        '' as ASEItem9C,'' as ASEItem10C,'' as ASEItem11C,'' as ASEItem12C,
                        '' as ASEItem13C,'' as ASEItem14C,'' as ASEItem15C
                        from ni_FallASE_Child
                        where ptEncounterID=@ptEncounterID 
                        and isnull(REC_STATUS,'')<>'D'
                    ) as fall
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<FallASE_Adult>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<FallASE_Adult>>(queryList);
        }

        /// <summary>
        /// 依NIS住院序號，查詢最新跌倒資料
        /// </summary>
        public async Task<ApiResult<List<ASEItem>>> GetLatestFallASEItem(FallASE_Adult param)
        {
            List<ASEItem> result = new List<ASEItem>();

            var fallRst = await GetFallASE_Adult(
                new FallASE_Adult { ptEncounterID = param.ptEncounterID },
                2);

            if (fallRst.Data.Count == 0) goto exit;

            var fallAse = fallRst.Data.FirstOrDefault();
            string RECMODEL = $"{fallAse?.DOC_CODE}ASE";
            string CODEVER = fallAse?.ASEItem_VER;

            var codeRst = await DB.RecShortCodeRepository.GetRecShortCode(
                new RecShortCode { RECMODEL = RECMODEL, CODEVER = CODEVER },
                2);

            codeRst.Data.DistinctBy(c => new { c.GROUPCODE, c.CONTEXT01, c.CONTEXT03 }).ForEach(c =>
            {
                result.Add(new ASEItem
                {
                    Title = c.CONTEXT01,
                    Value = fallAse.GetPropertyValue(c.GROUPCODE).NullableToStr(),
                    Unit = c.CONTEXT03
                });
            });

            result.Add(new ASEItem
            {
                Title = "總分",
                Value = fallAse.ASETotalGrade,
                Unit = codeRst.Data.FirstOrDefault()?.CONTEXT03
            });

            result.Add(new ASEItem
            {
                Title = "其他",
                Value = fallAse.ASE_Others,
                Unit = string.Empty
            });

        exit:
            return new ApiResult<List<ASEItem>>(result);
        }

    }
}
