using BMS_V2_Db.Enum;
using BMS_V2_Db.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ys.Tools.Extra;
using Ys.Tools.Interface;
using Ys.Tools.Response;

namespace BMS_V2_Db.Bll;

public class UserAddressBll:IBll
{

    private readonly BmsV2DbContext _dbContext;
    private readonly ILogger<UserAddress> _logger;

    public UserAddressBll(BmsV2DbContext dbContext, ILogger<UserAddress> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="userAddress"></param>
    /// <param name="userCode"></param>
    /// <returns></returns>
    public async Task<ApiResult> Add(UserAddress userAddress,string userCode)
    {
        userAddress.Code = Guid.NewGuid().ToString();
        userAddress.UserCode = userCode;
        _dbContext.UserAddress.Add(userAddress);
        await _dbContext.SaveChangesAsync();
        return ApiResult.True("成功");
    }

    /// <summary>
    /// 获得列表
    /// </summary>
    /// <param name="userCode"></param>
    /// <returns></returns>
    public ApiResult UserAddressList(string userCode)
    {
        var list = _dbContext.UserAddress.AsNoTracking().Where(x => x.UserCode == userCode)
            .OrderByDescending(x => x.Id).ToList();
        return ApiResult.True(new { list, list.Count });
    }


    /// <summary>
    /// 获得一条
    /// </summary>
    /// <param name="userCode"></param>
    /// <returns></returns>
    public ApiResult UserAddressByUserCode(string userCode)
    {
        var userAddress = _dbContext.UserAddress.AsNoTracking().OrderByDescending(x => x.Id)
            .FirstOrDefault(x => x.UserCode == userCode).NotNull($"UserAddress【{userCode}】未查询到数据");
        return ApiResult.True(userAddress);
    }
}