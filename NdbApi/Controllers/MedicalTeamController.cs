using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class MedicalTeamController : BaseController
    {
        public MedicalTeamController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("[action]")]
        public Task<ApiResult<List<MedicalTeam>>> GetMedicalTeam(string clinicalUnitId) =>
            DB.MedicalTeamRepository.GetMedicalTeam(clinicalUnitId);

        [HttpGet("[action]")]
        public Task<ApiResult<List<DrDutySchedule>>> GetDrDutySchedule(string clinicalUnitId) =>
            DB.MedicalTeamRepository.GetDrDutySchedule(clinicalUnitId);

        [HttpGet("[action]")]
        public Task<ApiResult<List<DisasterAssistance>>> GetDisasterAssistance(string clinicalUnitId) =>
            DB.MedicalTeamRepository.GetDisasterAssistance(clinicalUnitId);

    }
}
