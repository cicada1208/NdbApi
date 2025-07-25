using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class ClinicalUnitController : BaseController
    {
        public ClinicalUnitController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<ClinicalUnit>>> GetClinicalUnit([FromQuery] ClinicalUnit param, int option = 0) =>
            DB.ClinicalUnitRepository.GetClinicalUnit(param, option);

        [HttpGet("[action]/{option}")]
        public Task<ApiResult<List<ClinicalUnit>>> GetMyClinicalUnit(string userId, int option = 1) =>
            DB.ClinicalUnitRepository.GetMyClinicalUnit(userId, option);

    }
}
