using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class NisPatInfoController : BaseController
    {
        public NisPatInfoController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<NisPatInfo>>> GetNisPatInfo([FromQuery] NisPatInfo param, int option = 0) =>
            DB.NisPatInfoRepository.GetNisPatInfo(param, option);

    }
}
