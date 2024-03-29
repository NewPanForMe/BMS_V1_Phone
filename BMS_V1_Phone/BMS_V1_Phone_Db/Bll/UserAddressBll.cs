﻿using BMS_V1_Phone.Enum;
using BMS_V1_Phone.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Ys.Tools.Extra;
using Ys.Tools.Interface;
using Ys.Tools.Response;

namespace BMS_V1_Phone.Bll;

public class UserAddressBll:IBll
{

    private readonly BmsV1PhoneDbContext _dbContext;
    private readonly ILogger<UserAddress> _logger;

    public UserAddressBll(BmsV1PhoneDbContext dbContext, ILogger<UserAddress> logger)
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
            .FirstOrDefault(x => x.UserCode == userCode);
        return ApiResult.True(userAddress);
    }


}