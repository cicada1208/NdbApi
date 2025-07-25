using Lib;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class AllergyRepository : NISDBBaseRepository<Allergy>
    {
        /// <summary>
        /// 查詢過敏內容
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依NIS住院序號，查詢病人過敏藥物及入院表單其他藥物過敏</para>
        /// </summary>
        public async Task<ApiResult<List<Allergy>>> GetAllergy(Allergy param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select * 
                    from ni_Allergy
                    where ptEncounterID = @ptEncounterID";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Allergy>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            switch (option)
            {
                case 2:
                    queryList.ForEach(a =>
                    {
                        switch (a.AllergyType)
                        {
                            case "AllergyNote":
                                a.AllergyTypeName = "過敏備註";
                                break;
                            case "AllergyEffect":
                                a.AllergyTypeName = "藥物不良反應";
                                break;
                            case "AllergyDrug":
                            case "AllergyEftDrug":
                                a.AllergyTypeName = "藥物過敏";
                                break;
                            case "AllergyPhar":
                                a.AllergyTypeName = "藥理過敏";
                                break;
                        }
                    });

                    var baseInfo = (await DB.ASE_BaseInfoRepository.GetASE_BaseInfo(
                        new ASE_BaseInfo { ptEncounterID = param.ptEncounterID },
                        2)).Data.FirstOrDefault();

                    if (baseInfo != null && !baseInfo.OtherAllergy.IsNullOrWhiteSpace())
                    {
                        queryList.Add(new Allergy
                        {
                            ptEncounterID = baseInfo.ptEncounterID,
                            PT_NO = baseInfo.PT_NO,
                            AllergyTypeName = "其他藥物過敏",
                            PRSNAME = baseInfo.OtherAllergy
                        });
                    }
                    break;
            }

            return new ApiResult<List<Allergy>>(queryList);
        }

    }
}
