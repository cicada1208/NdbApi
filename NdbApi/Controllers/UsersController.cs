using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class UsersController : BaseController
    {
        public UsersController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet("{option}")]
        public Task<ApiResult<List<Users>>> GetUsers([FromQuery] Users param, int option = 0) =>
            DB.UsersRepository.GetUsers(param, option);

        [HttpGet("[action]")]
        public Task<ApiResult<Users>> GetNdbUser() =>
            DB.UsersRepository.GetNdbUser(User.Identity.Name);

    }
}
