using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class BedSoresASEController : BaseController
    {
        public BedSoresASEController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<BedSoresASE>>> GetBedSoresASE([FromQuery] BedSoresASE param, int option = 0) =>
            DB.BedSoresASERepository.GetBedSoresASE(param, option);

        [HttpGet("[action]")]
        public Task<ApiResult<List<ASEItem>>> GetLatestBedSoresASEItem([FromQuery] BedSoresASE param) =>
            DB.BedSoresASERepository.GetLatestBedSoresASEItem(param);

    }
}
