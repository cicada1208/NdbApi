using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class ClinicalUnitRepository : NISDBBaseRepository<ClinicalUnit>
    {
        /// <summary>
        /// 查詢護理站
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 查詢臨床單位：病房 、加護、急診</para>
        /// </summary>
        public async Task<ApiResult<List<ClinicalUnit>>> GetClinicalUnit(ClinicalUnit param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select *
                    from ni_ClinicalUnit 
                    where cuTypeId in ('034beb84-01ea-4914-9e82-b1219879bd39','fe7914fc-13ca-42cf-a24c-b1219879bd39','f2d263c0-4ea9-4c15-9206-b1219879bd39')
                    and clinicalUnitId <> 'ER'
                    and isActive = 1
                    order by label";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<ClinicalUnit>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<ClinicalUnit>>(queryList);
        }

        /// <summary>
        /// 查詢我的臨床單位
        /// <para>1: 我的臨床單位：病房 、加護、急診</para>
        /// <para>2: 我的臨床單位置頂及其他單位：病房 、加護、急診</para>
        /// </summary>
        public async Task<ApiResult<List<ClinicalUnit>>> GetMyClinicalUnit(string userId, int option = 1)
        {
            string sql = @"
            select *
            from ni_ClinicalUnit
            where clinicalUnitId in (
                select targetId 
                from ni_UserFavorite 
                where userId = @userId
                and favoriteTypeId='69fe243d-5236-4cfc-a361-b1219879bd39'
                and isDefault = 1
            )
            and cuTypeId in ('034beb84-01ea-4914-9e82-b1219879bd39','fe7914fc-13ca-42cf-a24c-b1219879bd39','f2d263c0-4ea9-4c15-9206-b1219879bd39')
            and isActive = 1
            union
            select *
            from ni_ClinicalUnit
            where clinicalUnitId in (
                select targetId 
                from ni_UserFavorite 
                where userId = @userId
                and favoriteTypeId='69fe243d-5236-4cfc-a361-b1219879bd39'
                and isDefault = 0
            )
            and cuTypeId in ('034beb84-01ea-4914-9e82-b1219879bd39','fe7914fc-13ca-42cf-a24c-b1219879bd39','f2d263c0-4ea9-4c15-9206-b1219879bd39')
            and isActive = 1 
            order by label";

            var query = await DBUtil.QueryAsync<ClinicalUnit>(sql, new { userId });
            var queryList = query.ToList();

            switch (option)
            {
                case 2:
                    var restUnit = (await GetClinicalUnit(null, 2)).Data.Where(u => !queryList.Exists(q =>
                    q.clinicalUnitId == u.clinicalUnitId)).ToList();
                    queryList = queryList.Concat(restUnit).ToList();
                    break;
            }

            return new ApiResult<List<ClinicalUnit>>(queryList);
        }

    }
}
