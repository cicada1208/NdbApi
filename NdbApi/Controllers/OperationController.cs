using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class OperationController : BaseController
    {
        public OperationController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("[action]")]
        public Task<ApiResult<List<PatientOPListExt>>> GetUnitPatientOPListExt(string clinicalUnitId, int iOpdates, int iOpdatee) =>
            DB.OperationRepository.GetUnitPatientOPListExt(clinicalUnitId, iOpdates, iOpdatee);

    }
}
