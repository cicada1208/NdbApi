using Lib;
using Lib.Api.Routes;
using Models;
using MoreLinq;
using Params;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys
{
    public class MedicalTeamRepository : BaseRepository<MedicalTeam>
    {
        /// <summary>
        /// 查詢護理站醫療團隊
        /// </summary>
        public async Task<ApiResult<List<MedicalTeam>>> GetMedicalTeam(string clinicalUnitId)
        {
            List<MedicalTeam> result = new();
            List<PtEncounter> ptEnc = null;

            if (clinicalUnitId.IsNullOrWhiteSpace())
                goto exit;

            ptEnc = (await DB.PtEncounterRepository.Get(new PtEncounter
            {
                ClinicalNo = clinicalUnitId,
                statusId = PtEncounterParam.StatusId.Hospitalization
            })).Data;

            result = ptEnc.Select(enc => new MedicalTeam
            {
                VsNo = enc.Vs1Name.Split("-").First(),
                VsName = enc.Vs1Name.Split("-").Last(),
                PatDept = enc.PatDept.Split("-").First(),
                PatDeptName = enc.PatDept.Split("-").Last(),
            }).ToList();

            result = result.Concat(
                ptEnc.Where(enc => !enc.Vs2Name.IsNullOrWhiteSpace())
                .Select(enc => new MedicalTeam
                {
                    VsNo = enc.Vs2Name.Split("-").First(),
                    VsName = enc.Vs2Name.Split("-").Last(),
                    PatDept = enc.PatDept.Split("-").First(),
                    PatDeptName = enc.PatDept.Split("-").Last(),
                }))
                .DistinctBy(enc => enc.VsNo).OrderBy(enc => enc.PatDept).ToList();

            if (result.Count > 0)
            {
                int mgop_ym = DateTimeUtil.ConvertAD(DateTime.Now.ToString("yyyyMMdd"), outFormat: "yyyMM").ToInt();
                var gopList = (await DB.Mch_mgopRepository.Get(new Mch_mgop { mgop_ym = mgop_ym })).Data;
                Mch_mgop gop;
                MedicalTeamNPPGY nppgy;
                Mg_mnid hisUser;
                string type = string.Empty;

                foreach (var mt in result)
                {
                    if (!mt.VsNo.IsNullOrWhiteSpace())
                    {
                        mt.Mvpn = (await ApiUtil.HttpClientExAsync<List<UserInfo>>(
                        HrRoute.Service(),
                        HrRoute.SearchEmp.UserInfos + mt.VsNo,
                        method: "GET")).FirstOrDefault()?.mvpn ?? string.Empty;
                    }

                    gop = gopList.FirstOrDefault(vs => vs.mgop_dr_no == mt.VsNo.SubStr(1));
                    if (gop != null)
                    {
                        for (int i = 1; i <= 10; i++)
                        {
                            type = gop.GetPropertyValue("mgop_gop_type_" + i).NullableToStr();
                            if (type.IsNullOrWhiteSpace()) continue;
                            nppgy = new();
                            nppgy.Type = type == "1" ? "PGY" : (type == "2" ? "NP" : "");
                            nppgy.No = gop.GetPropertyValue("mgop_gop_no_" + i).NullableToStr().PadLeft(5, '0');
                            hisUser = (await DB.Mg_mnidRepository.GetUser(new Mg_mnid()
                            {
                                UserId = nppgy.No
                            }, 1)).Data?.FirstOrDefault();
                            nppgy.Name = hisUser?.UserName ?? string.Empty;
                            nppgy.Mvpn = gop.GetPropertyValue("mgop_mb_no_" + i).NullableToStr();
                            mt.NPPGY.Add(nppgy);
                        }
                    }
                }
            }

        exit:
            return new ApiResult<List<MedicalTeam>>(result);
        }

        /// <summary>
        /// 查詢值班
        /// </summary>
        public async Task<ApiResult<List<DrDutySchedule>>> GetDrDutySchedule(string clinicalUnitId)
        {
            var tabs = (await DB.Ch_tabRepository.GetDrDutyScheduleTab(clinicalUnitId)).Data;

            Mg_mnid_Dr mg_mnid = new();
            string empNo = string.Empty;
            string mvpn = string.Empty;

            var result = tabs.Select(t =>
            {
                empNo = string.Empty;
                mvpn = string.Empty;

                if (!t.tab_dr_no.IsNullOrWhiteSpace())
                {
                    mg_mnid.nid_code = t.tab_dr_no;
                    empNo = DB.Mg_mnidRepository.GetDr(mg_mnid).Result.Data.FirstOrDefault()?.EmpNo ?? string.Empty;
                    if (empNo != string.Empty)
                    {
                        mvpn = ApiUtil.HttpClientExAsync<List<UserInfo>>(
                           HrRoute.Service(),
                           HrRoute.SearchEmp.UserInfos + empNo,
                           method: "GET").Result.FirstOrDefault()?.mvpn ?? string.Empty;
                    }
                }

                return new DrDutySchedule
                {
                    SchCode = t.tab_schcode,
                    SchName = t.tab_schname,
                    DrType = t.dhid_dr_type_name,
                    DrEmpNo = empNo,
                    DrName = t.tab_dr_name,
                    Mvpn = mvpn
                };
            }).ToList();

            return new ApiResult<List<DrDutySchedule>>(result);
        }

        /// <summary>
        /// 查詢災難協助
        /// </summary>
        public async Task<ApiResult<List<DisasterAssistance>>> GetDisasterAssistance(string clinicalUnitId)
        {
            List<DisasterAssistance> disasterAssistance = new();

            if (clinicalUnitId.IsNullOrWhiteSpace())
                goto exit;

            disasterAssistance = (await DB.KDNrNewRepository.GetUnitTransferTool(clinicalUnitId)).Data;

            List<PtEncounter> ptEnc = (await DB.PtEncounterRepository.Get(new PtEncounter
            {
                ClinicalNo = clinicalUnitId,
                statusId = PtEncounterParam.StatusId.Hospitalization
            })).Data;

            VitalSign vs;
            foreach (var p in ptEnc)
            {
                vs = (await DB.VitalSignRepository.GetVitalSign(new VitalSign { ptEncounterID = p.ptEncounterId }, 2)).Data.FirstOrDefault();

                if (vs == null) continue;
                if (!vs.OT.IsNullOrWhiteSpace())
                    disasterAssistance.Add(new DisasterAssistance { ADL = "氧氣", Beds = p.BedNo });
                if (!vs.Ventilator.IsNullOrWhiteSpace())
                    disasterAssistance.Add(new DisasterAssistance { ADL = "呼吸器", Beds = p.BedNo });
            }

            disasterAssistance = disasterAssistance.GroupBy(da => da.ADL)
                .Select(g => new DisasterAssistance { ADL = g.Key, Beds = string.Join(",", g.Select(da => da.Beds)) }).ToList();

        exit:
            return new ApiResult<List<DisasterAssistance>>(disasterAssistance);
        }

    }
}
