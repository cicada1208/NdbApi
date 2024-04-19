using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class Ch_dnrController : BaseController
    {
        public Ch_dnrController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<Ch_dnr>>> GetCh_dnr([FromQuery] Ch_dnr param, int option = 0) =>
            DB.Ch_dnrRepository.GetCh_dnr(param, option);
    }
}
