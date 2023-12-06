using System.Security.Claims;
using BMS_V2_Db.Models;
using Microsoft.Extensions.Logging;
using Ys.Base.Tools.xTool;
using Ys.Tools.Extra;
using Ys.Tools.Interface;
using Ys.Tools.MoreTool;
using Ys.Tools.Response;

namespace BMS_V2_Db.Bll;

public class LoginBll : IBll
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
    public ApiResult CustomerCheckLogin(string account, string password)
    {
        var user = _dbContext.UserCustomer.FirstOrDefault(x => x.Account == account).NotNull($"用户【{account}】不存在");
        var md532 = Md5Tools.MD5_32(password + user.PasswordSalt);
        if (md532.Equals(user.Password))
        {
            var listClaims = new List<Claim>()
            {
                new (ClaimTypes.Name, user.Account),
                new (ClaimTypes.NameIdentifier,user.Id.ToString()),
                new ("UserCode",user.Code),
                new ("UserType","Customer"),
            };
            var token = TokenTools.Create(listClaims);
            return ApiResult.True(new { account, token, });
        }
        return ApiResult.False("登录失败，请检查账号/密码");
    }






    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="account"></param>
    /// <param name="password"></param>
    /// <returns>ApiResult</returns>
    public async Task<ApiResult> CustomerRegister(string account, string password)
    {
        _dbContext.UserCustomer.Any(x => x.Account == account).IsBool($"用户{account}已存在");
        var passwordSalt = Guid.NewGuid().ToString();
        var passMd5 = Md5Tools.MD5_32(password + passwordSalt);
        var user = new UserCustomer()
        {
            Account = account,
            Password = passMd5,
            PasswordSalt = passwordSalt,
            Code = Guid.NewGuid().ToString(),
        };
        _dbContext.UserCustomer.Add(user);
        await _dbContext.SaveChangesAsync();

        var listClaims = new List<Claim>()
        {
            new (ClaimTypes.Name, user.Account),
            new (ClaimTypes.NameIdentifier,user.Id.ToString()),
            new ("UserCode",user.Code),
            new ("UserType","Customer"),
        };
        var token = TokenTools.Create(listClaims);
        return ApiResult.True(new { account, token, });
    }


    /// <summary>
    /// 登录
    /// </summary>
    /// <param name="account">账号</param>
    /// <param name="password">密码</param>
    /// <returns>ApiResult</returns>
    public ApiResult EngineerCheckLogin(string account, string password)
    {
        var user = _dbContext.UserEngineer.FirstOrDefault(x => x.Account == account).NotNull($"用户【{account}】不存在");
        var md532 = Md5Tools.MD5_32(password + user.PasswordSalt);
        if (md532.Equals(user.Password))
        {
            var listClaims = new List<Claim>()
            {
                new (ClaimTypes.Name, user.Name),
                new (ClaimTypes.NameIdentifier,user.Id.ToString()),
                new ("UserCode",user.Code),
                new ("UserType","Engineer"),
            };
            var token = TokenTools.Create(listClaims);
            return ApiResult.True(new { account, token, });
        }
        return ApiResult.False("登录失败，请检查账号/密码");
    }

    /// <summary>
    /// 注册
    /// </summary>
    /// <param name="account"></param>
    /// <param name="password"></param>
    /// <returns>ApiResult</returns>
    public async Task<ApiResult> EngineerRegister(string account, string password)
    {
        _dbContext.UserEngineer.Any(x => x.Account == account).IsBool($"用户{account}已存在");
        var passwordSalt = Guid.NewGuid().ToString();
        var passMd5 = Md5Tools.MD5_32(password + passwordSalt);
        var user = new UserEngineer()
        {
            Account = account,
            Password = passMd5,
            PasswordSalt = passwordSalt,
            Code = Guid.NewGuid().ToString(),
            Name = "",
            Phone = ""
        };
        _dbContext.UserEngineer.Add(user);
        await _dbContext.SaveChangesAsync();

        var listClaims = new List<Claim>()
        {
            new (ClaimTypes.Name, user.Name),
            new (ClaimTypes.NameIdentifier,user.Id.ToString()),
            new ("UserCode",user.Code),
            new ("UserType","Engineer"),
        };
        var token = TokenTools.Create(listClaims);
        return ApiResult.True(new { account, token, });
    }
}