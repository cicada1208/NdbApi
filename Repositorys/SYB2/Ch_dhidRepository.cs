using Lib;
using Models;
using MoreLinq;
using Params;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.SYB2
{
    public class Ch_dhidRepository : SYB2BaseRepository<Ch_dhid>
    {
        /// <summary>
        /// 查詢值班線別
        /// </summary>
        public async Task<ApiResult<List<Ch_dhid>>> GetDrDutyScheduleDhid(string clinicalUnitId)
        {
            string sql = string.Empty;
            List<Ch_dhid> query = new();

            if (clinicalUnitId.IsNullOrWhiteSpace())
                goto exit;

            List<string> depts = (await DB.PtEncounterRepository.Get(new PtEncounter
            {
                ClinicalNo = clinicalUnitId,
                statusId = PtEncounterParam.StatusId.Hospitalization
            })).Data.DistinctBy(enc => enc.PatDept).Select(enc => enc.PatDept.Split("-").First()).ToList();

            // 線別By科別
            depts.ForEach(d =>
            {
                sql += $@"
                {(sql != string.Empty ? "union" : string.Empty)}
                select * from ch_dhid
                where dhid_unit like '%{d}%'
                and dhid_type = 'D'";
            });

            // 線別By護理站
            sql += $@"
                {(sql != string.Empty ? "union" : string.Empty)}
                select * from ch_dhid
                where dhid_unit like '%{clinicalUnitId}%'
                and dhid_type = 'S'";

            // 線別By護理站+科別
            depts.ForEach(d =>
            {
                sql += $@"
                {(sql != string.Empty ? "union" : string.Empty)}
                select * from ch_dhid
                where dhid_unit like '%{clinicalUnitId}%' and dhid_unit like '%{d}%'
                and dhid_type = 'F'";
            });

            query = (await DBUtil.QueryAsync<Ch_dhid>(sql)).ToList();

        exit:
            return new ApiResult<List<Ch_dhid>>(query);
        }

    }
}
