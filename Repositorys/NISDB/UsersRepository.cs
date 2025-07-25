using Lib;
using Models;
using Params;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Repositorys.NISDB
{
    public class UsersRepository : NISDBBaseRepository<Users>
    {
        /// <summary>
        /// 查詢人員資料
        /// <para> 1: 依參數自動組建</para>
        /// </summary>
        public async Task<ApiResult<List<Users>>> GetUsers(Users param, int option = 0)
        {
            string sql = string.Empty;

            switch (option)
            {
                case 1:
                    break;
            }

            var query = await DBUtil.QueryIntgrAsync<Users>(sql, param, schemaOnly: option != 1);
            var queryList = query.ToList();

            return new ApiResult<List<Users>>(queryList);
        }

        public async Task<ApiResult<Users>> GetNdbUser(string userId)
        {
            if (userId.IsNullOrWhiteSpace())
                return new ApiResult<Users>(false, msg: MsgParam.LoginNoData);

            // nis user
            Users user = (await GetUsers(new Users()
            {
                loginId = userId,
                isActive = true
            }, 1)).Data.FirstOrDefault();

            // his user
            if (user == null)
            {
                Mg_mnid hisUser = (await DB.Mg_mnidRepository.GetUser(new Mg_mnid()
                {
                    UserId = userId
                }, 1)).Data?.FirstOrDefault();

                if (hisUser != null)
                    user = new Users()
                    {
                        loginId = hisUser.UserId,
                        userId = hisUser.UserId,
                        userName = hisUser.UserName,
                        dimission = hisUser.Dimission
                    };
            }

            if (user.userTerseName.IsNullOrWhiteSpace())
                user.userTerseName = user.userName.SubStr(1, 2);

            return new ApiResult<Users>(user);
        }

    }
}
