using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class VSPainController : BaseController
    {
        public VSPainController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<VSPain>>> GetVSPain([FromQuery] VSPain param, int option = 0) => DB.VSPainRepository.GetVSPain(param, option);

        [HttpGet("[action]/{option}")]
        public Task<ApiResult<List<VSPain>>> GetVSPainRpt([FromQuery] VSPain param, int option = 0) => DB.VSPainRepository.GetVSPainRpt(param, option);

    }
}
