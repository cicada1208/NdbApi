using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class VitalSignController : BaseController
    {
        public VitalSignController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<VitalSign>>> GetVitalSign([FromQuery] VitalSign param, int option = 0) => DB.VitalSignRepository.GetVitalSign(param, option);

        [HttpGet("[action]/{option}")]
        public Task<ApiResult<VitalSignRpt>> GetVitalSignRpt([FromQuery] VitalSign param, int option = 0) => DB.VitalSignRepository.GetVitalSignRpt(param, option);

    }
}
