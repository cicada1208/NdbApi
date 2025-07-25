using Lib;
using Models;
using MoreLinq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class BedSoresASERepository : NISDBBaseRepository<BedSoresASE>
    {
        /// <summary>
        /// 查詢壓力性損傷資料
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢最新壓力性損傷資料</para>
        /// </summary>
        public async Task<ApiResult<List<BedSoresASE>>> GetBedSoresASE(BedSoresASE param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select top 1 *
                    from ni_BedSoresASE
                    where ptEncounterID=@ptEncounterID 
                    and isnull(REC_STATUS,'')<>'D'
                    order by REC_DTM desc";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<BedSoresASE>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<BedSoresASE>>(queryList);
        }

        /// <summary>
        /// 依NIS住院序號，查詢最新壓力性損傷資料
        /// </summary>
        public async Task<ApiResult<List<ASEItem>>> GetLatestBedSoresASEItem(BedSoresASE param)
        {
            List<ASEItem> result = new List<ASEItem>();

            var bedSoresRst = await GetBedSoresASE(
                new BedSoresASE { ptEncounterID = param.ptEncounterID },
                2);

            if (bedSoresRst.Data.Count == 0) goto exit;

            var bedSoresAse = bedSoresRst.Data.FirstOrDefault();
            string RECMODEL = $"{bedSoresAse?.DOC_CODE}ASE";
            string CODEVER = bedSoresAse?.ASEItem_VER;

            var codeRst = await DB.RecShortCodeRepository.GetRecShortCode(
                new RecShortCode { RECMODEL = RECMODEL, CODEVER = CODEVER },
                2);

            codeRst.Data.DistinctBy(c => new { c.GROUPCODE, c.CONTEXT01, c.CONTEXT03 }).ForEach(c =>
            {
                result.Add(new ASEItem
                {
                    Title = c.CONTEXT01,
                    Value = bedSoresAse.GetPropertyValue(c.GROUPCODE).NullableToStr(),
                    Unit = c.CONTEXT03
                });
            });

            result.Add(new ASEItem
            {
                Title = "總分",
                Value = bedSoresAse.ASETotalGrade,
                Unit = codeRst.Data.FirstOrDefault()?.CONTEXT03
            });

            result.Add(new ASEItem
            {
                Title = "其他",
                Value = bedSoresAse.ASE_Others,
                Unit = string.Empty
            });

        exit:
            return new ApiResult<List<ASEItem>>(result);
        }

    }
}
