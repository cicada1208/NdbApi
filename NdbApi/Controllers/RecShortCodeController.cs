using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class RecShortCodeController : BaseController
    {
        public RecShortCodeController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<RecShortCode>>> GetRecShortCode([FromQuery] RecShortCode param, int option = 0) =>
            DB.RecShortCodeRepository.GetRecShortCode(param, option);

    }
}
