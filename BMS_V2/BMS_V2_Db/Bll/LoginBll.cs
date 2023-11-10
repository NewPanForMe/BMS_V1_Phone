using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ys.Base.Tools.xTool;
using Ys.Tools.Config;
using Ys.Tools.Extra;
using Ys.Tools.Interface;
using Ys.Tools.MoreTool;
using Ys.Tools.Response;

namespace BMS_V2_Db.Bll;

public class LoginBll:IBll
{
    private readonly BmsV2DbContext _dbContext;
    private readonly ILogger<LoginBll> _logger;

    public LoginBll(BmsV2DbContext dbContext, ILogger<LoginBll> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="account">账号</param>
    /// <param name="password">密码</param>
    /// <returns>ApiResult</returns>
    public ApiResult CheckLogin(string account,string password)
    {
        var user = _dbContext.User.FirstOrDefault(x=>x.Account== account).NotNull($"用户【{account}】不存在");
        user.IsLock.IsBool($"用户【{account}】已锁定，请联系管理员");
        user.IsDelete.IsBool($"用户【{account}】不存在，请联系管理员");
        var md532 = Md5Tools.MD5_32(password + user.PasswordSalt);
        if (md532.Equals(user.Password))
        {
            var listClaims = new List<Claim>()
            {
                new (ClaimTypes.Name,user.UserName ?? user.Account),
                new (ClaimTypes.NameIdentifier,user.Id.ToString()),
                new ("UserCode",user.Code),
            };
            var token = TokenTools.Create(listClaims);
            return ApiResult.True(new { account,user.UserName, token });
        }
        return  ApiResult.False("登录失败，请检查用户名/密码");
    }
}