namespace Lib.Api.Routes
{
    public class HrRoute : BaseRoute
    {
        /// <summary>
        /// API Service Url
        /// </summary>
        public static string Service() => Service(ApiName.Hr);

        public class SearchEmp
        {
            public const string UserInfos = "SearchEmp/UserInfos/";
        }

    }
}
