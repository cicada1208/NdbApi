using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class FallASE_AdultController : BaseController
    {
        public FallASE_AdultController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<FallASE_Adult>>> GetFallASE_Adult([FromQuery] FallASE_Adult param, int option = 0) =>
            DB.FallASE_AdultRepository.GetFallASE_Adult(param, option);

        [HttpGet("[action]")]
        public Task<ApiResult<List<ASEItem>>> GetLatestFallASEItem([FromQuery] FallASE_Adult param) =>
            DB.FallASE_AdultRepository.GetLatestFallASEItem(param);

    }
}
