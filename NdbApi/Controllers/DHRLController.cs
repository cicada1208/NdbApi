using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class DHRLController : BaseController
    {
        public DHRLController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<DHRL>>> GetDHRL([FromQuery] DHRL param, int option = 0) =>
            DB.DHRLRepository.GetDHRL(param, option);

    }
}
