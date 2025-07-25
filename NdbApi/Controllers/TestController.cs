using Lib;
using Lib.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Models;
using Params;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NdbApi.Controllers
{
    public class TestController : BaseController
    {
        private readonly ILogger<TestController> logger;

        public TestController(IOptionsMonitor<AppSettings> settings,
            ApiUtilLocator apiUtils,
            ILogger<TestController> logger) : base(settings.CurrentValue, apiUtils)
        {
            this.logger = logger;
        }

    [HttpGet]
        public IEnumerable<Test> Get()
        {
            string[] Summaries = new[]
            {"Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"};

            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new Test
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            }).ToArray();
        }

        [HttpGet("[action]")]
        public string GetConnStr()
        {
            return $@"
            NIS ConnStr：{DBUtil.GetConnString(DBParam.DBName.NIS)}
            SYB1 ConnStr：{DBUtil.GetConnString(DBParam.DBName.SYB1)}
            SYB2 ConnStr：{DBUtil.GetConnString(DBParam.DBName.SYB2)}";
        }

        // https://localhost:44313/Test/GetSettings?option=5 OK
        // https://localhost:44313/Test/GetSettings OK
        [HttpGet("[action]")]
        public string GetSettings(int option = 0)
        {
            return $"option={option}. {nameof(Settings.Jwt.Issuer)}={Settings.Jwt.Issuer}.";
        }

        // https://localhost:44313/Test/GetTest/999?option=5 OK
        // https://localhost:44313/Test/GetTest?option=5 OK
        // https://localhost:44313/Test/GetTest/999 OK
        // https://localhost:44313/Test/GetTest OK
        [HttpGet("[action]/{id?}")]
        public string GetTest(int? id, int option = 0)
        {
            return $"option={option}. id={id}.";
        }

        // https://localhost:44313/Test/GetTest2?title=abc&msg=123 OK
        // https://localhost:44313/Test/GetTest2?title=abc OK
        // https://localhost:44313/Test/GetTest2 OK
        [HttpGet("[action]")]
        public string GetTest2(string title, string msg) =>
             $"title={title}. msg={msg}.";

        [ApiExplorerSettings(IgnoreApi = true)]
        public string GetTest3() =>
            "不會加入路由 404 NotFound";

        [HttpPost("[action]")]
        public string PostException(Test param, int option = 0)
        {
            throw new Exception("test");
            //return $"PostTest option={option}. param.Summary={param.Summary}.";
        }

        [HttpGet("[action]")]
        public IEnumerable<string> GetJwt()
        {
            return new string[] { "JWTToken", "Get ( no Authorize )" };
        }

        [HttpPost("[action]")]
        [Authorize]
        public IEnumerable<string> PostJwt()
        {
            var identityName = User.Identity.Name;
            var claims = User.Claims.Select(p => $"{p.Type}:{ p.Value}");
            return new string[] { "JWTToken", "Post ( has Authorize )" };
        }

        [HttpPut("[action]")]
        [Authorize(Roles = "Admin")]
        public IEnumerable<string> PutJwt()
        {
            return new string[] { "JWTToken", "Put ( has Authorize and Roles=Admin )" };
        }

        [HttpDelete("[action]")]
        [Authorize(Roles = "User")]
        public IEnumerable<string> DeleteJwt()
        {
            return new string[] { "JWTToken", "Post ( has Authorize and Roles=User )" };
        }

    }

    public class Test
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        public string Summary { get; set; }
    }

}
