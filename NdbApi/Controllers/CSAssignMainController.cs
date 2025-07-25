using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class CSAssignMainController : BaseController
    {
        public CSAssignMainController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("[action]")]
        public Task<ApiResult<List<CSAssignGroup>>> GetCSAssignGroup(string clinicalUnitId, string assignDate) =>
            DB.CSAssignMainRepository.GetCSAssignGroup(clinicalUnitId, assignDate);
    }
}
