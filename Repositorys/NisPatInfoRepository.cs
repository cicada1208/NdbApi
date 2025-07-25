using Lib;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Repositorys
{
    public class NisPatInfoRepository : BaseRepository<NisPatInfo>
    {
        /// <summary>
        /// 查詢病人資料卡
        /// <para>1: 依護理站</para>
        /// <para>2: 依床號</para>
        /// </summary>
        public async Task<ApiResult<List<NisPatInfo>>> GetNisPatInfo(NisPatInfo param, int option = 0)
        {
            string sql = string.Empty;
            string select = @"
                    case when termlib.label is null then 0
                    else 1 end as Icu,
                    bed.label as bedLabel, bed.bedId, bed.clinicalUnitId, bed.clinicalHisId,
                    pat.PatNo, pat.PatIdNo, pat.PatName, pat.PatBirth, 
                    pat.PatGender, pat.PatBloodType, pat.PatBloodRh,
                    enc.ptEncounterId, enc.BeginDt, enc.EndDt, enc.Vs1Name, enc.Vs2Name,
                    enc.encDiagNo1, enc.encDiagCh1, enc.encDiagEn1,
                    enc.encDiagNo2, enc.encDiagCh2, enc.encDiagEn2,
                    enc.encDiagNo3, enc.encDiagCh3, enc.encDiagEn3,
                    enc.encDiagNo4, enc.encDiagCh4, enc.encDiagEn4,
                    enc.encDiagNo5, enc.encDiagCh5, enc.encDiagEn5,
                    enc.odrDiag, enchis.hisIpdNo";
            bool icu = false;

            switch (option)
            {
                case 1:
                    sql = $@"
                    select {select}
                    from ni_Bed as bed
                    left join ni_ClinicalUnit as clinical
                    on (clinical.clinicalUnitId = bed.clinicalUnitId and clinical.isActive=1)
                    left join ni_TermLib as termlib
                    on (termlib.termLibId = clinical.cuTypeId and termlib.label='加護病房' and termlib.isActive=1)
                    left join ni_PtStay as stay
                    on (stay.clinicalUnitId = bed.clinicalUnitId and stay.bedId = bed.bedId and stay.endDt is null)
                    left join ni_PtEncounter as enc 
                    on (enc.ptEncounterId = stay.ptEncounterId)
                    left join ni_HisPatient as pat 
                    on (pat.PatNo = enc.ptHisId)
                    left join ni_PtEncHis as enchis
                    on (enchis.ptEncounterId = enc.ptEncounterId and enchis.isActive = 1)
                    where bed.clinicalUnitId = @clinicalUnitId
                    and bed.isActive = 1
                    order by bedId";
                    break;
                case 2:
                    sql = $@"
                    select {select}
                    from ni_Bed as bed
                    left join ni_ClinicalUnit as clinical
                    on (clinical.clinicalUnitId = bed.clinicalUnitId and clinical.isActive=1)
                    left join ni_TermLib as termlib
                    on (termlib.termLibId = clinical.cuTypeId and termlib.label='加護病房' and termlib.isActive=1)
                    left join ni_PtStay as stay
                    on (stay.clinicalUnitId = bed.clinicalUnitId and stay.bedId = bed.bedId and stay.endDt is null)
                    left join ni_PtEncounter as enc 
                    on (enc.ptEncounterId = stay.ptEncounterId)
                    left join ni_HisPatient as pat 
                    on (pat.PatNo = enc.ptHisId)
                    left join ni_PtEncHis as enchis
                    on (enchis.ptEncounterId = enc.ptEncounterId and enchis.isActive = 1)
                    where bed.bedId = @bedId
                    and bed.isActive = 1";
                    break;
            }

            Stopwatch sw1 = new Stopwatch();
            sw1.Start();
            var query = await DB.NISDB.QueryIntgrAsync<NisPatInfo>(sql, param);
            var queryList = query.ToList();
            icu = queryList.Exists(pt => pt.Icu.HasValue && pt.Icu.Value);
            sw1.Stop();
            Console.WriteLine("GetNisPatInfo.query: {0:N0}ms", sw1.ElapsedMilliseconds);

            Stopwatch sw2 = new Stopwatch();
            sw2.Start();
            string endDt;
            string clinicalHisId = queryList.FirstOrDefault()?.clinicalHisId;
            queryList.ForEach(pt =>
            {
                if (pt.hisIpdNo.Length > 11) pt.hisIpdNo = pt.hisIpdNo.SubStr(1);
                pt.BeginDt = DateTimeUtil.ConvertAD(pt.BeginDt, false, "yyyyMMdd", "yyyy-MM-dd");
                pt.EndDt = DateTimeUtil.ConvertAD(pt.EndDt, false, "yyyyMMdd", "yyyy-MM-dd");
                if (!pt.BeginDt.IsNullOrWhiteSpace())
                {
                    endDt = pt.EndDt.IsNullOrWhiteSpace() ? DateTime.Now.ToString("yyyy-MM-dd") : pt.EndDt;
                    pt.Days = Math.Ceiling(Utils.DateTime.DateTimeDiffTS(pt.BeginDt, endDt, "yyyy-MM-dd", "yyyy-MM-dd").TotalDays + 1).ToInt();
                }
                pt.PatBirth = DateTimeUtil.ConvertAD(pt.PatBirth, false, "yyyy/MM/dd", "yyyy-MM-dd");
                pt.PatAge = DateTimeUtil.GetAge(pt.PatBirth, pt.BeginDt, "yyyy-MM-dd", "yyyy-MM-dd");
                pt.Vs1No = pt.Vs1Name.Split('-').First();
                pt.Vs1Name = pt.Vs1Name.Split('-').Last();
                pt.Vs2No = pt.Vs2Name.Split('-').First();
                pt.Vs2Name = pt.Vs2Name.Split('-').Last();
                SetBedLable(pt);
                pt.PatName = Utils.Medical.AnonymizeName(pt.PatName);
            });
            sw2.Stop();
            Console.WriteLine("GetNisPatInfo.format: {0:N0}ms", sw2.ElapsedMilliseconds);

            Stopwatch sw3 = new Stopwatch();
            sw3.Start();
            var tsmitGradeTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.PatNo.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<Mr_tsmit>>(null));
                else
                {
                    return DB.Mr_tsmitRepository.GetMr_tsmit(new Mr_tsmit
                    {
                        tsmit_key = $"GRADE{pt.hisIpdNo}{pt.PatNo}"
                    }, 2);
                }
            }));

            var dnrTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.PatIdNo.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<Ch_dnr>>(null));
                else
                {
                    return DB.Ch_dnrRepository.GetCh_dnr(new Ch_dnr
                    {
                        dnr_pat_idno = pt.PatIdNo
                    }, 2);
                }
            }));

            var bloodTasks = Task.WhenAll(queryList.Select(pt => GetBlood(pt)));

            var allergyTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<Allergy>>(null));
                else
                {
                    //allergy.ptEncounterID = pt.ptEncounterId; // 此段寫法會錯亂，改為如下
                    return DB.AllergyRepository.GetAllergy(new Allergy
                    {
                        ptEncounterID = pt.ptEncounterId
                    }, 2);
                }
            }));

            var fallTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<FallASE_Adult>>(null));
                else
                {
                    return DB.FallASE_AdultRepository.GetFallASE_Adult(new FallASE_Adult
                    {
                        ptEncounterID = pt.ptEncounterId
                    }, 2);
                }
            }));

            var bedSoresTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<BedSoresASE>>(null));
                else
                {
                    return DB.BedSoresASERepository.GetBedSoresASE(new BedSoresASE
                    {
                        ptEncounterID = pt.ptEncounterId
                    }, 2);
                }
            }));

            var focusRecordTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<FocusRecord>>(null));
                else
                {
                    return DB.FocusRecordRepository.GetFocusRecordLatest(new FocusRecord
                    {
                        ptEncounterId = pt.ptEncounterId
                    }, 2);
                }
            }));

            var dhrlTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<DHRL>>(null));
                else
                {
                    return DB.DHRLRepository.GetDHRL(new DHRL
                    {
                        ptEncounterID = pt.ptEncounterId
                    }, 2);
                }
            }));

            var essTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<ESS>>(null));
                else
                {
                    return DB.ESSRepository.GetESS(new ESS
                    {
                        ptEncounterID = pt.ptEncounterId
                    }, 2);
                }
            }));

            var endoTrTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<TubeMag>>(null));
                else
                {
                    return DB.TubeMagRepository.GetTubeMag(new TubeMag
                    {
                        ptEncounterID = pt.ptEncounterId,
                        TBKind = "A"
                    }, 4);
                }
            }));

            var cvcTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<TubeMag>>(null));
                else
                {
                    return DB.TubeMagRepository.GetTubeMag(new TubeMag
                    {
                        ptEncounterID = pt.ptEncounterId,
                        TBKind = "B"
                    }, 2);
                }
            }));

            var foleyTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<TubeMag>>(null));
                else
                {
                    return DB.TubeMagRepository.GetTubeMag(new TubeMag
                    {
                        ptEncounterID = pt.ptEncounterId,
                        TBKind = "C"
                    }, 2);
                }
            }));

            var dlHickTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<TubeMag>>(null));
                else
                {
                    return DB.TubeMagRepository.GetTubeMag(new TubeMag
                    {
                        ptEncounterID = pt.ptEncounterId
                    }, 5);
                }
            }));

            var o2Tasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<VitalSign>>(null));
                else
                {
                    return DB.VitalSignRepository.GetVitalSign(new VitalSign
                    {
                        ptEncounterID = pt.ptEncounterId
                    }, 2);
                }
            }));

            var restraintTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.ptEncounterId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<RestraintMag>>(null));
                else
                {
                    return DB.RestraintMagRepository.GetRestraintMag(new RestraintMag
                    {
                        ptEncounterID = pt.ptEncounterId
                    }, 2);
                }
            }));

            var transferTasks = Task.WhenAll(queryList.Select(pt =>
            {
                if (pt.bedId.IsNullOrWhiteSpace())
                    return Task.Run(() => new ApiResult<List<Mi_mbed_PatInfo>>(null));
                else
                {
                    return DB.Mi_mbedRepository.GetTranPatInfo(new Mi_mbed
                    {
                        bed_bed = pt.bedId
                    });
                }
            }));

            Task<ApiResult<List<RRS>>[]> casTasks = null;
            Task<ApiResult<List<TubeMag>>[]> chestTubeTasks = null;
            Task<ApiResult<List<KDNrNew>>[]> transferToolTasks = null;
            Task<ApiResult<List<VitalSign>>[]> rassTasks = null;
            Task<ApiResult<List<VitalSign>>[]> camTasks = null;
            Task<ApiResult<List<VSPain>>[]> painTasks = null;
            Task<ApiResult<List<VitalSign>>[]> hrTasks = null;
            Task<ApiResult<List<APACHE1>>[]> ap2Tasks = null;
            Task<ApiResult<List<IOByDay>>[]> ioTasks = null;
            if (!icu)
            {
                casTasks = Task.WhenAll(queryList.Select(pt =>
                {
                    if (pt.ptEncounterId.IsNullOrWhiteSpace())
                        return Task.Run(() => new ApiResult<List<RRS>>(null));
                    else
                    {
                        return DB.RRSRepository.GetRRS(new RRS
                        {
                            ptEncounterID = pt.ptEncounterId
                        }, 2);
                    }
                }));

                chestTubeTasks = Task.WhenAll(queryList.Select(pt =>
                {
                    if (pt.ptEncounterId.IsNullOrWhiteSpace())
                        return Task.Run(() => new ApiResult<List<TubeMag>>(null));
                    else
                    {
                        return DB.TubeMagRepository.GetTubeMag(new TubeMag
                        {
                            ptEncounterID = pt.ptEncounterId
                        }, 3);
                    }
                }));

                transferToolTasks = Task.WhenAll(queryList.Select(pt =>
                {
                    if (pt.ptEncounterId.IsNullOrWhiteSpace())
                        return Task.Run(() => new ApiResult<List<KDNrNew>>(null));
                    else
                    {
                        return DB.KDNrNewRepository.Get(new KDNrNew
                        {
                            ptEncounterID = pt.ptEncounterId,
                            Item_Name = "TFT",
                            Item_Seq = "1"
                        });
                    }
                }));
            }
            else
            {
                rassTasks = Task.WhenAll(queryList.Select(pt =>
                {
                    if (pt.ptEncounterId.IsNullOrWhiteSpace())
                        return Task.Run(() => new ApiResult<List<VitalSign>>(null));
                    else
                    {
                        return DB.VitalSignRepository.GetRASS(new VitalSign
                        {
                            ptEncounterID = pt.ptEncounterId
                        }, 2);
                    }
                }));

                camTasks = Task.WhenAll(queryList.Select(pt =>
                {
                    if (pt.ptEncounterId.IsNullOrWhiteSpace())
                        return Task.Run(() => new ApiResult<List<VitalSign>>(null));
                    else
                    {
                        return DB.VitalSignRepository.GetVitalSign(new VitalSign
                        {
                            ptEncounterID = pt.ptEncounterId
                        }, 3);
                    }
                }));

                painTasks = Task.WhenAll(queryList.Select(pt =>
                {
                    if (pt.ptEncounterId.IsNullOrWhiteSpace())
                        return Task.Run(() => new ApiResult<List<VSPain>>(null));
                    else
                    {
                        return DB.VSPainRepository.GetVSPainRpt(new VSPain
                        {
                            ptEncounterID = pt.ptEncounterId
                        }, 3);
                    }
                }));

                hrTasks = Task.WhenAll(queryList.Select(pt =>
                {
                    if (pt.ptEncounterId.IsNullOrWhiteSpace())
                        return Task.Run(() => new ApiResult<List<VitalSign>>(null));
                    else
                    {
                        return DB.VitalSignRepository.GetVitalSign(new VitalSign
                        {
                            ptEncounterID = pt.ptEncounterId
                        }, 4);
                    }
                }));

                ap2Tasks = Task.WhenAll(queryList.Select(pt =>
                {
                    if (pt.PatNo.IsNullOrWhiteSpace())
                        return Task.Run(() => new ApiResult<List<APACHE1>>(null));
                    else
                    {
                        return DB.APACHE1Repository.GetLatestAP2(pt.PatNo,
                            DateTimeUtil.ConvertAD(pt.BeginDt, false, "yyyy-MM-dd", "yyyyMMdd"),
                            DateTime.Now.ToString("yyyyMMdd"));
                    }
                }));

                ioTasks = Task.WhenAll(queryList.Select(pt =>
                {
                    if (pt.ptEncounterId.IsNullOrWhiteSpace())
                        return Task.Run(() => new ApiResult<List<IOByDay>>(null));
                    else
                    {
                        return DB.IOByDayRepository.Get(new IOByDay
                        {
                            ptEncounterID = pt.ptEncounterId,
                            REC_DATE = DateTime.Now.AddDays(-1).ToString("yyyy/MM/dd")
                        });
                    }
                }));
            }

            //int iOpdate = DateTimeUtil.ConvertAD(DateTime.Now.ToString("yyyyMMdd"), outFormat: "yyyMMdd").ToInt();
            //var opTasks = Task.WhenAll(queryList.Select(pt =>
            //{
            //    if (pt.PatNo.IsNullOrWhiteSpace())
            //        return Task.Run(() => new List<PatientOPList>());
            //    else
            //    {
            //        return ApiUtil.HttpClientExAsync<List<PatientOPList>>(
            //            OPStateRoute.Service(),
            //            OPStateRoute.PatientLocation.PatientOPList,
            //            queryParams: new { iOpdates = iOpdate, iOpdatee = iOpdate, Patno = pt.PatNo },
            //            method: "GET");
            //    }
            //}));

            //await Task.WhenAll(dnrTasks, bloodTasks); // 改成下列
            var tsmitGradeRsts = await tsmitGradeTasks;
            var dnrRsts = await dnrTasks;
            var bloodRsts = await bloodTasks;
            var allergyRsts = await allergyTasks;
            var fallRsts = await fallTasks;
            var bedSoresRsts = await bedSoresTasks;
            var focusRecordRsts = await focusRecordTasks;
            var dhrlRsts = await dhrlTasks;
            var essRsts = await essTasks;
            var endoTrRsts = await endoTrTasks;
            var cvcRsts = await cvcTasks;
            var foleyRsts = await foleyTasks;
            var dlHickRsts = await dlHickTasks;
            var o2Rsts = await o2Tasks;
            var restraintRsts = await restraintTasks;
            var transferRsts = await transferTasks;

            ApiResult<List<RRS>>[] casRsts = null;
            ApiResult<List<TubeMag>>[] chestTubeRsts = null;
            ApiResult<List<KDNrNew>>[] transferToolRsts = null;
            ApiResult<List<VitalSign>>[] rassRsts = null;
            ApiResult<List<VitalSign>>[] camRsts = null;
            ApiResult<List<VSPain>>[] painRsts = null;
            ApiResult<List<VitalSign>>[] hrRsts = null;
            ApiResult<List<APACHE1>>[] ap2Rsts = null;
            ApiResult<List<IOByDay>>[] ioRsts = null;
            if (!icu)
            {
                casRsts = await casTasks;
                chestTubeRsts = await chestTubeTasks;
                transferToolRsts = await transferToolTasks;
            }
            else
            {
                rassRsts = await rassTasks;
                camRsts = await camTasks;
                painRsts = await painTasks;
                hrRsts = await hrTasks;
                ap2Rsts = await ap2Tasks;
                ioRsts = await ioTasks;
            }

            //var opRsts = await opTasks;

            var infectiousFilter = new HashSet<string> { "64", "67", "68" }; // 觸64、沫67、空68
            for (int i = 0; i < queryList.Count; i++)
            {
                queryList[i].TsmitGrade = tsmitGradeRsts[i].Data?.FirstOrDefault()?.tsmit_data_grade_id ?? string.Empty;
                queryList[i].DNR = dnrRsts[i].Data;
                queryList[i].Blood = bloodRsts[i].Data;
                queryList[i].Allergy = allergyRsts[i].Data;
                queryList[i].FallTotalGrade = fallRsts[i].Data?.FirstOrDefault()?.ASETotalGrade.ToNullableInt();
                queryList[i].BedSoresTotalGrade = bedSoresRsts[i].Data?.FirstOrDefault()?.ASETotalGrade.ToNullableInt();
                queryList[i].Cough = focusRecordRsts[i].Data?.Exists(fr => fr.focusNo == "71");
                queryList[i].Fever = focusRecordRsts[i].Data?.Exists(fr => fr.focusNo == "112");
                queryList[i].Infectious = focusRecordRsts[i].Data?.Exists(fr => infectiousFilter.Contains(fr.focusNo));
                queryList[i].DHRL = dhrlRsts[i].Data?.FirstOrDefault()?.Info ?? string.Empty;
                queryList[i].ESS = (essRsts[i].Data?.FirstOrDefault() != null) ? essRsts[i].Data?.FirstOrDefault()?.Info :
                    (DB.ESSRepository.ESSDTFilter(queryList[i]) ? "入院24-72小時未評估" : string.Empty);
                queryList[i].CAS = casRsts?[i].Data?.FirstOrDefault()?.Info ?? string.Empty;
                queryList[i].EndoTr = endoTrRsts[i].Data?.Count > 0;
                queryList[i].CVC = SetTubeInfo(cvcRsts[i].Data);
                queryList[i].Foley = SetTubeInfo(foleyRsts[i].Data);
                queryList[i].ChestTube = SetTubeInfo(chestTubeRsts?[i].Data);
                queryList[i].DLHick = dlHickRsts[i].Data?.Count > 0;
                queryList[i].O2 = SetO2Info(o2Rsts[i].Data?.FirstOrDefault());
                queryList[i].Restraint = restraintRsts[i].Data?.Count > 0;
                SetTransferInfo(queryList[i], transferRsts[i].Data?.FirstOrDefault());
                queryList[i].TransferTool = transferToolRsts?[i].Data?.FirstOrDefault(
                    kdn => kdn.REC_STATUS != "D" && kdn.NrObject != "")?.NrObject ?? string.Empty;

                queryList[i].RASS = rassRsts?[i].Data?.FirstOrDefault()?.RASS.ToNullableInt();
                queryList[i].CAM = camRsts?[i].Data?.FirstOrDefault()?.CAM_ICU_RST;
                queryList[i].Pain = painRsts?[i].Data?.FirstOrDefault()?.PainVal ?? string.Empty;
                queryList[i].HR = hrRsts?[i].Data?.FirstOrDefault()?.HBFreq.ToNullableInt();
                queryList[i].AP2 = ap2Rsts?[i].Data?.FirstOrDefault()?.TOTALSUM.NullableToStr() ?? string.Empty;
                queryList[i].T_IODif_T_Yesterday = ioRsts?[i].Data?.FirstOrDefault()?.T_IODif_T ?? string.Empty;

                //queryList[i].OP = opRsts[i]?.FirstOrDefault() ?? new PatientOPList();
            }
            sw3.Stop();
            Console.WriteLine("GetNisPatInfo.task: {0:N0}ms", sw3.ElapsedMilliseconds);

            return new ApiResult<List<NisPatInfo>>(queryList);
        }

        /// <summary>
        /// 設置未結管路資訊
        /// </summary>
        /// <returns></returns>
        private string SetTubeInfo(List<TubeMag> tubeMags)
        {
            string result = string.Empty;
            //string days = string.Empty;

            //tubeMags?.ForEach(t =>
            //{
            //    days = Math.Ceiling(Utils.DateTime.DateTimeDiffTS(t.REC_DTM,
            //        DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
            //        str1Format: "yyyyMMddHHmmss").TotalDays).ToString();
            //    result += (result != string.Empty ? Environment.NewLine : "") + $"({days}){t.TBType}";
            //});

            TubeMag tubeMag = tubeMags?.FirstOrDefault();
            if (tubeMag != null)
            {
                result = Math.Ceiling(Utils.DateTime.DateTimeDiffTS(tubeMag.REC_DTM,
                    DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
                    str1Format: "yyyyMMddHHmmss").TotalDays).ToString();
            }

            return result;
        }

        /// <summary>
        /// 設置O2資訊
        /// </summary>
        private string SetO2Info(VitalSign vs)
        {
            string result = string.Empty;

            if (vs != null)
            {
                if (!vs.SPO2.IsNullOrWhiteSpace())
                    result += (result != string.Empty ? Environment.NewLine : "") + $"SPO2：{vs.SPO2}";
                if (!vs.OT.IsNullOrWhiteSpace())
                    result += (result != string.Empty ? Environment.NewLine : "") + $"使用氧氣療法：{vs.OT}";
                if (!vs.Ventilator.IsNullOrWhiteSpace())
                    result += (result != string.Empty ? Environment.NewLine : "") + $"使用呼吸器：{vs.Ventilator}";
            }

            return result;
        }

        /// <summary>
        /// 設置轉入轉出資訊
        /// </summary>
        private void SetTransferInfo(NisPatInfo pat, Mi_mbed_PatInfo bed)
        {
            if (bed == null) return;
            if (bed.bed_pat_no.IsNullOrWhiteSpace())
            { // 床上無病人，取轉入欄資訊
                if ((bed.bed_i_mark == "1" || bed.bed_i_mark == "4"))
                {
                    pat.TransferType = "轉入";
                    pat.TransferBedLabel = bed.bed_i_bed;
                    if (pat.TransferBedLabel.Length > 5)
                        pat.TransferBedLabel = $"{pat.TransferBedLabel.SubStr(0, 5)}-{pat.TransferBedLabel.SubStr(5)}";
                    if (pat.ptEncounterId.IsNullOrWhiteSpace())
                    { // NIS該床也為無病人，再帶入資訊
                        pat.PatName = Utils.Medical.AnonymizeName(bed.bed_i_pat_name);
                        pat.PatNo = bed.bed_i_pat_no;
                        pat.PatGender = bed.bed_i_pat_sex;
                    }
                }
                else if (bed.bed_i_mark == "2")
                    pat.TransferType = "鎖床";
            }
            else
            {  // 床上有病人，取轉出欄資訊
                if ((bed.bed_o_mark == "1" || bed.bed_o_mark == "4"))
                {
                    pat.TransferType = "轉出";
                    pat.TransferBedLabel = bed.bed_o_bed;
                    if (pat.TransferBedLabel.Length > 5)
                        pat.TransferBedLabel = $"{pat.TransferBedLabel.SubStr(0, 5)}-{pat.TransferBedLabel.SubStr(5)}";
                }
                else if (bed.bed_o_mark == "A")
                    pat.TransferType = "上午出院";
                else if (bed.bed_o_mark == "P")
                    pat.TransferType = "下午出院";
            }
        }

        private void SetBedLable(NisPatInfo pat)
        {
            if (pat.clinicalUnitId.StartsWith("10A"))
            {
                pat.bedLabelClinical = pat.clinicalHisId;
                pat.bedLabelNo = pat.bedLabel.Split(pat.bedLabelClinical).Last();
            }
            else if (!pat.bedLabel.Contains(pat.clinicalUnitId))
            {
                Regex r = new Regex(@"\d*\D+");
                pat.bedLabelClinical = r.Match(pat.bedLabel).Value;
                pat.bedLabelNo = pat.bedLabel.Split(pat.bedLabelClinical).Last();
            }
            else
            {
                pat.bedLabelClinical = pat.bedLabel.Split(pat.clinicalUnitId).First() + pat.clinicalUnitId;
                pat.bedLabelNo = pat.bedLabel.Split(pat.bedLabelClinical).Last();
            }
        }

        public async Task<ApiResult<bool>> GetBlood(NisPatInfo param)
        {
            bool blood = false; // 血液感染

            if (param.PatNo.IsNullOrWhiteSpace())
                goto exit;

            var hivcaRst = await DB.Ch_hivcaRepository.GetCh_hivca(
                new Ch_hivca { hivca_cn_pat = param.PatNo.ToNullableInt() },
                2);

            if (hivcaRst.Data.Count() > 0)
            {
                blood = true;
                goto exit;
            }

            var mic2Rst = await DB.Mc_mic2Repository.GetMc_mic2(
                new Mc_mic2 { hcic2_ptno = param.PatNo.ToNullableInt() },
                2);

            if (mic2Rst.Data.Count() > 0)
            {
                blood = true;
                goto exit;
            }

            var rel2Rst = await DB.Ch_rel2Repository.GetCh_rel2(
                new Ch_rel2 { chrel2_pat_no = param.PatNo.ToNullableInt() },
                4);

            if (rel2Rst.Data.Count() > 0)
            {
                blood = true;
                goto exit;
            }

        exit:
            return new ApiResult<bool>(blood, MsgParam.ApiSuccess);
        }

    }
}
