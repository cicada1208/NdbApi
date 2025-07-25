using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class RestraintMagController : BaseController
    {
        public RestraintMagController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<RestraintMag>>> GetRestraintMag([FromQuery] RestraintMag param, int option = 0) =>
            DB.RestraintMagRepository.GetRestraintMag(param, option);

        [HttpGet("[action]/{option}")]
        public Task<ApiResult<List<RestraintMag>>> GetRestraintMagRpt([FromQuery] RestraintMag param, int option = 0) =>
            DB.RestraintMagRepository.GetRestraintMagRpt(param, option);

    }
}
