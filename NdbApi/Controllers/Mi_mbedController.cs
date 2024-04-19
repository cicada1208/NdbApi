using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class Mi_mbedController : BaseController
    {
        public Mi_mbedController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet]
        public Task<ApiResult<List<Mi_mbed>>> GetMi_mbed([FromQuery] Mi_mbed param) =>
            DB.Mi_mbedRepository.Get(param);

        [HttpGet("[action]")]
        public Task<ApiResult<List<Mi_mbed_PatInfo>>> GetTranPatInfo([FromQuery] Mi_mbed param) =>
            DB.Mi_mbedRepository.GetTranPatInfo(param);

        [HttpGet("[action]")]
        public Task<ApiResult<List<BedPatDpt>>> GetBedPatDpt([FromQuery] Mi_mbed param) =>
            DB.Mi_mbedRepository.GetBedPatDpt(param);

        [HttpGet("[action]")]
        public Task<ApiResult<List<Mi_mbed_Ext>>> GetLockBed([FromQuery] Mi_mbed param) =>
            DB.Mi_mbedRepository.GetLockBed(param);

        [HttpGet("[action]")]
        public Task<ApiResult<List<Mi_mbed_TranIn>>> GetTranIn([FromQuery] Mi_mbed param) =>
            DB.Mi_mbedRepository.GetTranIn(param);

        [HttpGet("[action]")]
        public Task<ApiResult<List<Mi_mbed_TranInternal>>> GetTranInternal([FromQuery] Mi_mbed param) =>
            DB.Mi_mbedRepository.GetTranInternal(param);

        [HttpGet("[action]")]
        public Task<ApiResult<List<Mi_mbed_TranOut>>> GetTranOut([FromQuery] Mi_mbed param) =>
            DB.Mi_mbedRepository.GetTranOut(param);

    }
}
