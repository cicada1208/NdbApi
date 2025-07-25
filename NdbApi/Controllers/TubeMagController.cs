using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class TubeMagController : BaseController
    {
        public TubeMagController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<TubeMag>>> GetTubeMag([FromQuery] TubeMag param, int option = 0) =>
            DB.TubeMagRepository.GetTubeMag(param, option);

        [HttpGet("[action]/{option}")]
        public Task<ApiResult<List<TubeMag>>> GetTubeMagRpt([FromQuery] TubeMag param, int option = 0) =>
            DB.TubeMagRepository.GetTubeMagRpt(param, option);

    }
}
