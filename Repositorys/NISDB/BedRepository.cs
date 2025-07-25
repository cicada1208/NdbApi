using Lib;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class BedRepository : NISDBBaseRepository<Bed>
    {
        /// <summary>
        /// 查詢床位
        /// <para>1: 依參數自動組建</para>
        /// <para>2: 依護理站，查詢床位資訊</para>
        /// </summary>
        public async Task<ApiResult<List<Bed>>> GetBed(Bed param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
                case 2:
                    sql = @"
                    select stay.ptEncounterId, bed.*
                    from ni_Bed as bed
                    left join ni_PtStay stay
                    on (stay.clinicalUnitId = bed.clinicalUnitId 
                    and stay.bedId = bed.bedId and stay.endDt is null)
                    where bed.clinicalUnitId = @clinicalUnitId
                    and bed.isActive = 1";
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Bed>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<Bed>>(queryList);
        }

        /// <summary>
        /// 查詢床位資訊
        /// <para>2: 依護理站，查詢床位資訊</para>
        /// </summary>
        public async Task<ApiResult<BedInfo>> GetBedInfo(Bed param, int option = 0)
        {
            var bedList = (await GetBed(param, option)).Data;

            BedInfo bedInfo = new BedInfo
            {
                clinicalUnitId = param.clinicalUnitId,
                all = bedList.Count,
                empty = bedList.Count(b => b.ptEncounterId.IsNullOrWhiteSpace()),
                add = bedList.Count(b => b.isAdd.HasValue && b.isAdd.Value)
            };
            bedInfo.inbed = bedInfo.all - bedInfo.empty;

            return new ApiResult<BedInfo>(bedInfo);
        }

    }
}
