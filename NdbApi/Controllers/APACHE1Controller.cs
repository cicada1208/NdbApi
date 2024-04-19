using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class APACHE1Controller : BaseController
    {
        public APACHE1Controller(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("[action]")]
        public Task<ApiResult<List<APACHE1>>> GetLatestAP2(string PATNO, string INDATE_Begin, string INDATE_End) =>
            DB.APACHE1Repository.GetLatestAP2(PATNO, INDATE_Begin, INDATE_End);

    }
}
