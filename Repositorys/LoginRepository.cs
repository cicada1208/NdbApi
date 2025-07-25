using Lib;
using Lib.Api;
using Microsoft.AspNetCore.Http;
using Models;
using Params;
using Repositorys.NISDB;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Repositorys
{
    public class LoginRepository : NISDBBaseRepository<Users>
    {
        public async Task<ApiResult<object>> PostLogin(HttpRequest request, JwtUtil jwtUtil)
        {
            // 登入頁面 call api 時好時壞，報的錯誤訊息為 CORS，需確認 webf01、webf02 IIS基本驗證設定是否相同(若使用 HTTP Basic Auth，基本驗證需關閉)
            bool decodeOK = request.Headers.DecodeBasicCredentials("Basic", out string userId, out string password);
            string errorMsg = string.Empty;
            Users user = null;

            if (!decodeOK)
                errorMsg = MsgParam.LoginErrorFormat;
            else
            {
                var sysParam = (await DB.SysParameterRepository.GetSysParameter(
                     new SysParameter { parameterName = "'CloseAD','SysAdmKey'" },
                     2)).Data;

                string closeAD = sysParam?.Find(p => p.parameterName == "CloseAD")?.value;
                string sysAdmKey = sysParam?.Find(p => p.parameterName == "SysAdmKey")?.value;
                bool passAD = closeAD == "1" || sysAdmKey == password;
                bool loginSucc = passAD || ServiceUtil.ADClient.VerifyUser(userId);

                if (!loginSucc)
                    errorMsg = MsgParam.LoginErrorId;
                else
                {
                    loginSucc = passAD || ServiceUtil.ADClient.Verify(userId, password);
                    if (!loginSucc)
                        errorMsg = MsgParam.LoginErrorPw;
                    else
                    {
                        // AD=6碼、nis & mg_mnid=5碼
                        if (userId.Trim().Length == 6)
                            userId = userId.SubStr(0, 1) + userId.SubStr(2);

                        // nis user
                        user = (await DB.UsersRepository.GetUsers(new Users()
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

                        if (user == null)
                            errorMsg = MsgParam.LoginNoData;
                        else if (user.dimission == "Y")
                            errorMsg = MsgParam.LoginDimission;
                    }
                }
            }

            ApiResult<object> result;
            if (errorMsg != string.Empty)
                result = new ApiError(HttpStatusCode.Unauthorized, errorMsg);
            else
            {
                user.token = jwtUtil.GenerateToken(user.loginId, expireMinutes: 7 * 24 * 60);
                if (user.userTerseName.IsNullOrWhiteSpace()) user.userTerseName = user.userName.SubStr(1, 2);
                result = new ApiResult<object>(user);
            }

            return result;
        }

    }
}
