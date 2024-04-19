using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class BedController : BaseController
    {
        public BedController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils) 
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<Bed>>> GetBed([FromQuery] Bed param, int option = 0) =>
            DB.BedRepository.GetBed(param, option);

        [HttpGet("[action]/{option}")]
        public Task<ApiResult<BedInfo>> GetBedInfo([FromQuery] Bed param, int option = 0) =>
            DB.BedRepository.GetBedInfo(param, option);

    }
}
