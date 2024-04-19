using Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Lib.Api;

namespace NdbApi.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpPost]
        [AllowAnonymous]
        public async Task<JsonResult> PostLogin()
        {
            var result = await DB.LoginRepository.PostLogin(Request, ApiUtils.Jwt);
            return new JsonResult(result) { StatusCode = (int)result.Code };
        }

    }
}
