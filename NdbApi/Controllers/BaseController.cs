using Lib.Api;
using Lib.Api.Attributes;
using Microsoft.AspNetCore.Mvc;
using Models;
using Repositorys;

namespace NdbApi.Controllers
{
    [Route("[controller]")]
    [Produces("application/json")]
    [ApiController]
    [ApiActionFilter]
    [ApiExceptionFilter]
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public abstract class BaseController : ControllerBase
    {
        public BaseController(AppSettings settings, ApiUtilLocator apiUtils)
        {
            Settings = settings;
            ApiUtils = apiUtils;
        }

        protected AppSettings Settings { get; }

        private DBContext _DB;
        protected DBContext DB =>
            _DB ??= new DBContext();

        protected ApiUtilLocator ApiUtils { get; }

        protected string RemoteIpAddress =>
            HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;

    }
}
