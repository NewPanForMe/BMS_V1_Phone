using System.Text.Json;
using BMS_V2_Db.Bll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Extra;
using Ys.Tools.Response;

namespace BMS_V2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        public LoginController(LoginBll loginBll, ILogger<LoginController> logger)
        {
            _loginBll = loginBll;
            _logger = logger;
        }

        private readonly LoginBll _loginBll;
        private readonly ILogger<LoginController> _logger;


        [HttpPost]
        public ApiResult Login(JsonElement req)
        {
            var account = req.GetJsonString("account").HasValueNoNameOrPwd("用户名为空");
            var password = req.GetJsonString("password").HasValueNoNameOrPwd("密码为空");
            _logger.LogInformation($"用户{account}登录");
            return _loginBll.CheckLogin(account, password);
        }



    }
}
