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
    public class TeamContactController : BaseController
    {
        public TeamContactController(IOptionsMonitor<AppSettings> settings, ApiUtilLocator apiUtils)
            : base(settings.CurrentValue, apiUtils) { }

        [HttpGet]
        public Task<ApiResult<List<TeamContact>>> GetTeamContact([FromQuery] TeamContact param) =>
            DB.TeamContactRepository.Get(param);

        [HttpPost]
        public Task<ApiResult<TeamContact>> InsertTeamContact(TeamContact param)
        {
            if (param.REC_NO.IsNullOrWhiteSpace())
                param.REC_NO = Guid.NewGuid().ToString();
            param.MD_PC = RemoteIpAddress;
            DateTime now = DateTime.Now;
            param.MD_DT = now.ToString("yyyy/MM/dd");
            param.MD_TIME = now.ToString("HH:mm:ss");
            return DB.TeamContactRepository.Insert(param);
        }

        [HttpPut]
        public Task<ApiResult<TeamContact>> UpdateTeamContact(TeamContact param)
        {
            param.MD_PC = RemoteIpAddress;
            DateTime now = DateTime.Now;
            param.MD_DT = now.ToString("yyyy/MM/dd");
            param.MD_TIME = now.ToString("HH:mm:ss");
            return DB.TeamContactRepository.Update(param);
        }

        /// <summary>
        /// PatchTeamContact
        /// </summary>
        /// <param name="param">前端傳入匿名型別，先取得更新欄位再轉型</param>
        [HttpPatch]
        public Task<ApiResult<TeamContact>> PatchTeamContact(object param)
        {
            param.GetModelAndProps(out TeamContact model, out HashSet<string> props);
            model.MD_PC = RemoteIpAddress;
            DateTime now = DateTime.Now;
            model.MD_DT = now.ToString("yyyy/MM/dd");
            model.MD_TIME = now.ToString("HH:mm:ss");
            props.AddProps<TeamContact>(m => new { m.MD_PC, m.MD_DT, m.MD_TIME });
            return DB.TeamContactRepository.Patch(model, props);
        }

    }
}
