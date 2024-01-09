using System.Text.Json;
using BMS_V1_Phone.Bll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Extra;
using Ys.Tools.Response;

namespace BMS_V2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CustomerLoginController : ControllerBase
    {
        public CustomerLoginController(LoginBll loginBll, ILogger<CustomerLoginController> logger)
        {
            _loginBll = loginBll;
            _logger = logger;
        }

        private readonly LoginBll _loginBll;
        private readonly ILogger<CustomerLoginController> _logger;


        [HttpPost]
        public ApiResult Login(JsonElement req)
        {
            var account = req.GetJsonString("account").HasValueNoNameOrPwd("账户为空");
            var password = req.GetJsonString("password").HasValueNoNameOrPwd("密码为空");
            _logger.LogWarning("用户{0}登录", account);
            return _loginBll.CustomerCheckLogin(account, password);
        }

        [HttpPost]
        public async Task<ApiResult> Register(JsonElement req)
        {
            var account = req.GetJsonString("account").HasValueNoNameOrPwd("账户为空");
            var password = req.GetJsonString("password").HasValueNoNameOrPwd("密码为空");
            _logger.LogWarning("用户{0}注册", account);
            return await _loginBll.CustomerRegister(account, password);
        }
    }
}
