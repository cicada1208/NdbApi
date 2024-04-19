using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class Ch_cserController : BaseController
    {
        public Ch_cserController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<Ch_cser>>> GetCh_cser([FromQuery] Ch_cser param, int option = 0) =>
            DB.Ch_cserRepository.GetCh_cser(param, option);

    }
}
