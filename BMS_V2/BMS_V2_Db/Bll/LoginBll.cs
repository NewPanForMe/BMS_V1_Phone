using BMS_V2_Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ys.Base.Tools.xTool;
using Ys.Tools.Extra;
using Ys.Tools.Interface;
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
        var canConnect = _dbContext.Database.CanConnect();


        User user = _dbContext.Users.FirstOrDefault(x=>x.Account== account).NotNull("用户不存在");
        user.IsLock.IsBool("当前账户已锁定，请联系管理员");
        if (Md5Tools.MD5_32(password + user.PasswordSalt).Equals(user.Password))
        {



            return ApiResult.True(new { account, });
        }
        return  ApiResult.False("登录失败，请检查用户名/密码");
    }







}