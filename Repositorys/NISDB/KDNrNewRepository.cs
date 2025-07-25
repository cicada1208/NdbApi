using Models;
using Params;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class KDNrNewRepository : NISDBBaseRepository<KDNrNew>
    {
        public async Task<ApiResult<List<DisasterAssistance>>> GetUnitTransferTool(string clinicalUnitId)
        {
            string sql = $@"
            select NrNew.NrObject as ADL, Enc.BedNo as Beds
            from ni_PtEncounter as Enc
            left join ni_KDNrNew as NrNew
            on (NrNew.ptEncounterID = Enc.ptEncounterId)
            where Enc.ClinicalNo = @ClinicalNo
            and Enc.statusId = '{PtEncounterParam.StatusId.Hospitalization}'
            and NrNew.Item_Name='TFT' and NrNew.Item_Seq='1' and isnull(NrNew.REC_STATUS,'') <> 'D'
            and isnull(NrNew.NrObject,'') <> ''";

            List<DisasterAssistance> disasterAssistance = (await DB.NISDB.QueryAsync<DisasterAssistance>(
                sql, new { ClinicalNo = clinicalUnitId })).ToList();

            return new ApiResult<List<DisasterAssistance>>(disasterAssistance);
        }
    }
}
