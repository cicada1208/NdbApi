using Lib;
using Lib.Api.Routes;
using Models;
using MoreLinq;
using Params;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys
{
    public class OperationRepository : BaseRepository<PatientOPListExt>
    {
        /// <summary>
        /// 查詢護理站手術
        /// </summary>
        public async Task<ApiResult<List<PatientOPListExt>>> GetUnitPatientOPListExt(string clinicalUnitId, int iOpdates, int iOpdatee)
        {
            List<PatientOPListExt> result = new();

            if (clinicalUnitId.IsNullOrWhiteSpace())
                goto exit;

            List<PtEncounter> ptEncs = (await DB.PtEncounterRepository.Get(new PtEncounter
            {
                ClinicalNo = clinicalUnitId,
                statusId = PtEncounterParam.StatusId.Hospitalization
            })).Data;

            List<PatientOPListExt> ops;
            foreach (var p in ptEncs)
            {
                ops = (await ApiUtil.HttpClientExAsync<List<PatientOPListExt>>(
                     OPStateRoute.Service(),
                     OPStateRoute.PatientLocation.PatientOPList,
                     queryParams: new { iOpdates, iOpdatee, Patno = p.ptHisId },
                     method: "GET"));
                if (ops != null)
                    result.AddRange(ops);
            }

            result = result.DistinctBy(op => op.NOTNO).ToList();

            string patno = string.Empty;
            OPNSChkRec oPNSChkRec;
            PtEncounter ptEnc;
            VitalSign vs;
            Gas102 gas102;
            foreach (PatientOPListExt op in result)
            {
                patno = op.PATNO.NullableToStr().PadLeft(8, '0');
                if (!patno.IsNullOrWhiteSpace())
                {
                    ptEnc = ptEncs.Find(p => p.ptHisId == patno);
                    op.ptEncounterId = ptEnc?.ptEncounterId ?? string.Empty;
                    op.BedNo = ptEnc?.BedNo ?? string.Empty;
                    op.PatName = Utils.Medical.AnonymizeName((await DB.HisPatientRepository.Get(new HisPatient { PatNo = patno })).Data.FirstOrDefault()?.PatName);

                    oPNSChkRec = (await DB.OPNSChkRecRepository.GetOPNSChkRec(
                        new OPNSChkRec
                        {
                            PT_NO = patno,
                            OPDT = DateTimeUtil.ConvertROC(op.iOPdate.NullableToStr()),
                            OPNO = $"%{op.NOTNO}%"
                        }, 2)).Data.FirstOrDefault();
                    op.CHK_Consent1 = oPNSChkRec?.CHK_Consent1 ?? string.Empty;
                    op.CHK_Consent2 = oPNSChkRec?.CHK_Consent2 ?? string.Empty;
                    op.AnesthesiaAse = oPNSChkRec?.Anesthesia ?? string.Empty;
                    op.OPSiteMark = oPNSChkRec?.OPSiteMark ?? string.Empty;

                    gas102 = (await DB.Gas102Repository.GetPatTransfer(
                        patno, $"{op.OPStartDate} 00:00:00", $"{op.OPStartDate} 23:59:59")).Data.FirstOrDefault();
                    if (gas102 != null)
                    {
                        op.TransferDispatchTime = gas102.gas102_12.NullableToStr("yyyy-MM-dd HH:mm:ss");
                        //op.TransferCompletedTime = gas102.gas102_14.NullableToStr("yyyy-MM-dd HH:mm:ss");

                        if (gas102.gas102_03 != null)
                            op.TransferSite = (await DB.Gas002Repository.Get(
                                new Gas002
                                {
                                    gas002_00 = "00001",
                                    gas002_01 = gas102.gas102_03
                                }
                                )).Data.FirstOrDefault()?.gas002_02;
                        op.TransferSite += op.TransferSite.IsNullOrWhiteSpace() ? "" : "，" + gas102.gas102_09;
                    }
                }

                if (!op.ptEncounterId.IsNullOrWhiteSpace())
                {
                    op.TransferTool = (await DB.KDNrNewRepository.Get(
                        new KDNrNew { ptEncounterID = op.ptEncounterId, Item_Name = "TFT", Item_Seq = "1" })
                        ).Data.FirstOrDefault(kdn => kdn.REC_STATUS != "D" && kdn.NrObject != "")?.NrObject ?? string.Empty;

                    vs = (await DB.VitalSignRepository.GetVitalSign(
                        new VitalSign { ptEncounterID = op.ptEncounterId }, 2)).Data.FirstOrDefault();
                    if (vs != null)
                    {
                        if (!vs.OT.IsNullOrWhiteSpace())
                            op.TransferTool += $"{(op.TransferTool.IsNullOrWhiteSpace() ? "" : ",")}氧氣";
                        if (!vs.Ventilator.IsNullOrWhiteSpace())
                            op.TransferTool += $"{(op.TransferTool.IsNullOrWhiteSpace() ? "" : ",")}呼吸器";
                    }
                }
            }

        exit:
            return new ApiResult<List<PatientOPListExt>>(result);
        }

    }
}
