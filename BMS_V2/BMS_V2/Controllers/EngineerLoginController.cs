using BMS_V2_Db.Bll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Ys.Tools.Controllers;
using Ys.Tools.Extra;
using Ys.Tools.Response;

namespace BMS_V2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EngineerLoginController : BaseController
    {
        public EngineerLoginController(LoginBll loginBll, ILogger<EngineerLoginController> logger)
        {
            _loginBll = loginBll;
            _logger = logger;
        }

        private readonly LoginBll _loginBll;
        private readonly ILogger<EngineerLoginController> _logger;



        [HttpPost]
        public ApiResult Login(JsonElement req)
        {
            var account = req.GetJsonString("account").HasValueNoNameOrPwd("账户为空");
            var password = req.GetJsonString("password").HasValueNoNameOrPwd("密码为空");
            _logger.LogWarning("用户{0}登录", account);
            return _loginBll.EngineerCheckLogin(account, password);
        }

        [HttpPost]
        public async Task<ApiResult> Register(JsonElement req)
        {
            var account = req.GetJsonString("account").HasValueNoNameOrPwd("账户为空");
            var password = req.GetJsonString("password").HasValueNoNameOrPwd("密码为空");
            _logger.LogWarning("用户{0}注册", account);
            return await _loginBll.EngineerRegister(account, password);
        }




    }
}
