using Lib;
using Lib.Api;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NdbApi.Controllers
{
    public class TeamNoteController : BaseController
    {
        public TeamNoteController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet]
        public Task<ApiResult<List<TeamNote>>> GetTeamNote([FromQuery] TeamNote param) =>
            DB.TeamNoteRepository.Get(param);

        [HttpPost]
        public Task<ApiResult<TeamNote>> InsertTeamNote(TeamNote param)
        {
            if (param.REC_NO.IsNullOrWhiteSpace())
                param.REC_NO = Guid.NewGuid().ToString();
            param.MD_PC = RemoteIpAddress;
            DateTime now = DateTime.Now;
            param.MD_DT = now.ToString("yyyy/MM/dd");
            param.MD_TIME = now.ToString("HH:mm:ss");
            return DB.TeamNoteRepository.Insert(param);
        }

        [HttpPut]
        public Task<ApiResult<TeamNote>> UpdateTeamNote(TeamNote param)
        {
            param.MD_PC = RemoteIpAddress;
            DateTime now = DateTime.Now;
            param.MD_DT = now.ToString("yyyy/MM/dd");
            param.MD_TIME = now.ToString("HH:mm:ss");
            return DB.TeamNoteRepository.Update(param);
        }

        [HttpPatch]
        public Task<ApiResult<TeamNote>> PatchTeamNote(object param)
        {
            param.GetModelAndProps(out TeamNote model, out HashSet<string> props);
            model.MD_PC = RemoteIpAddress;
            DateTime now = DateTime.Now;
            model.MD_DT = now.ToString("yyyy/MM/dd");
            model.MD_TIME = now.ToString("HH:mm:ss");
            props.AddProps<TeamNote>(m => new { m.MD_PC, m.MD_DT, m.MD_TIME });
            return DB.TeamNoteRepository.Patch(model, props);
        }

    }
}
