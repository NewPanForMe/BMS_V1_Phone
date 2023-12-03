using BMS_V2_Db.Enum;
using BMS_V2_Db.Models;
using Microsoft.Extensions.Logging;
using Ys.Tools.Extra;
using Ys.Tools.Interface;
using Ys.Tools.Models;
using Ys.Tools.Response;

namespace BMS_V2_Db.Bll;

public class PcbOrderBll : IBll
{
    private readonly BmsV2DbContext _dbContext;
    private readonly ILogger<PcbOrderBll> _logger;

    public PcbOrderBll(BmsV2DbContext dbContext, ILogger<PcbOrderBll> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    /// <summary>
    /// 新增
    /// </summary>
    /// <param name="pcbOrder"></param>
    /// <param name="userCode"></param>
    /// <returns></returns>
    public async Task<ApiResult> Add(PcbOrder pcbOrder, string userCode)
    {
        pcbOrder.Code = Guid.NewGuid().ToString();
        pcbOrder.CreateDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        pcbOrder.CreateUser = userCode;
        pcbOrder.OrderStatus = OrderStatus.未接单.ToString();
        _dbContext.PcbOrder.Add(pcbOrder);
        await _dbContext.SaveChangesAsync();
        return ApiResult.True("成功");
    }


    /// <summary>
    /// 接单功能
    /// </summary>
    /// <param name="pcbOrder"></param>
    /// <param name="userCode"></param>
    /// <returns></returns>
    public ApiResult ReceiveOrder(PcbOrder pcbOrder, string userCode)
    {
        var pcbOrderDb = _dbContext.PcbOrder.FirstOrDefault(x=>x.Code== pcbOrder.Code).NotNull($"ReceiveOrder【{pcbOrder.Code}】未查询到数据");
        pcbOrderDb.AcceptDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        pcbOrderDb.AcceptEngineer = userCode;
        pcbOrderDb.OrderStatus = OrderStatus.已接单.ToString();
        _dbContext.SaveChanges();
        return ApiResult.True("成功");
    }



    /// <summary>
    /// 估价备注功能
    /// </summary>
    /// <param name="pcbOrder"></param>
    /// <returns></returns>
    public ApiResult Evaluate(PcbOrder pcbOrder)
    {
        var pcbOrderDb = _dbContext.PcbOrder.FirstOrDefault(x => x.Code == pcbOrder.Code).NotNull($"Evaluate【{pcbOrder.Code}】未查询到数据");
        pcbOrderDb.Evaluate = pcbOrder.Evaluate;
        pcbOrderDb.Remark = pcbOrder.Remark;
        pcbOrderDb.OrderStatus = OrderStatus.已估价.ToString();
        _dbContext.SaveChanges();
        return ApiResult.True("成功");
    }





}