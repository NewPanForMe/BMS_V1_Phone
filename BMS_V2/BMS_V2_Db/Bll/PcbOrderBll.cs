using System.Data.Entity;
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
        return ApiResult.True("新增成功");
    }


    /// <summary>
    /// 接单功能
    /// </summary>
    /// <param name="code"></param>
    /// <param name="userCode"></param>
    /// <param name="userName"></param>
    /// <returns></returns>
    public ApiResult ReceiveOrder(string code, string userCode, string userName)
    {
        var pcbOrderDb = _dbContext.PcbOrder.FirstOrDefault(x => x.Code == code).NotNull($"ReceiveOrder【{code}】未查询到数据");

        if(!string.IsNullOrWhiteSpace(pcbOrderDb.AcceptEngineerCode)) return ApiResult.False("抢慢了！订单没了");
        pcbOrderDb.AcceptDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        pcbOrderDb.AcceptEngineer = userName;
        pcbOrderDb.AcceptEngineerCode = userCode;
        pcbOrderDb.OrderStatus = OrderStatus.已接单.ToString();
        _dbContext.SaveChanges();
        return ApiResult.True("抢单成功");
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
        return ApiResult.True("估价成功");
    }


    /// <summary>
    /// 拒绝订单
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public async Task<ApiResult> Refuse(string code)
    {
        var pcbOrderDb = _dbContext.PcbOrder.FirstOrDefault(x => x.Code == code).NotNull($"Evaluate【{code}】未查询到数据");
        //新增订单拒绝
        var refuse = new OrderRefuse()
        {
            Code = Guid.NewGuid().ToString(),
            OrderCode = pcbOrderDb.Code,
            OrderEvaluate = pcbOrderDb.Evaluate,
            OrderRemark = pcbOrderDb.Remark,
            OrderAcceptEngineer = pcbOrderDb.AcceptEngineer,
            OrderAcceptDateTime = pcbOrderDb.AcceptDateTime,
            CreateDateTime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
            OrderAcceptEngineerCode = pcbOrderDb.AcceptEngineerCode
        };
        //处理订单
        pcbOrderDb.AcceptDateTime = "";
        pcbOrderDb.AcceptEngineer = "";
        pcbOrderDb.AcceptEngineerCode = "";
        pcbOrderDb.Evaluate = "";
        pcbOrderDb.Remark = "";
        pcbOrderDb.OrderStatus = OrderStatus.订单拒绝.ToString();

        _dbContext.OrderRefuse.Add(refuse);
        await _dbContext.SaveChangesAsync();
        return ApiResult.True("订单已拒绝");
    }


    /// <summary>
    /// 作废订单
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public ApiResult Cancel(string code)
    {
        var pcbOrderDb = _dbContext.PcbOrder.FirstOrDefault(x => x.Code == code).NotNull($"Cancel【{code}】未查询到数据");
        if (pcbOrderDb.OrderStatus == OrderStatus.确认订单.ToString()) return ApiResult.False("订单已确认，无法作废");
        if (pcbOrderDb.OrderStatus == OrderStatus.已完成待发送快递.ToString()) return ApiResult.False("订单待发送快递，无法作废");
        if (pcbOrderDb.OrderStatus == OrderStatus.快递已发出.ToString()) return ApiResult.False("订单快递已发出，无法作废");
        if (pcbOrderDb.OrderStatus == OrderStatus.订单完成.ToString()) return ApiResult.False("订单已完成，无法作废");
        if (pcbOrderDb.OrderStatus == OrderStatus.订单作废.ToString()) return ApiResult.False("订单已作废，无需再次作废");
        pcbOrderDb.OrderStatus = OrderStatus.订单作废.ToString();
        _dbContext.SaveChanges();
        return ApiResult.True("作废成功");
    }


    /// <summary>
    /// 确认订单
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public ApiResult Confirm(string code)
    {
        var pcbOrderDb = _dbContext.PcbOrder.FirstOrDefault(x => x.Code == code).NotNull($"Cancel【{code}】未查询到数据");
        pcbOrderDb.OrderStatus = OrderStatus.确认订单.ToString();
        _dbContext.SaveChanges();
        return ApiResult.True("确认成功");
    }


    /// <summary>
    /// 寄送快递
    /// </summary>
    /// <param name="pcbOrder"></param>
    /// <returns></returns>
    public ApiResult Follow(PcbOrder pcbOrder)
    {
        var pcbOrderDb = _dbContext.PcbOrder.FirstOrDefault(x => x.Code == pcbOrder.Code).NotNull($"Cancel【{pcbOrder.Code}】未查询到数据");
        pcbOrderDb.FollowNum = pcbOrder.FollowNum;
        pcbOrderDb.OrderStatus = OrderStatus.快递已发出.ToString();
        _dbContext.SaveChanges();
        return ApiResult.True("填写成功,快递已发出");
    }



    /// <summary>
    /// 订单完成
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public ApiResult Finish(string code)
    {
        var pcbOrderDb = _dbContext.PcbOrder.FirstOrDefault(x => x.Code == code).NotNull($"Cancel【{code}】未查询到数据");
        pcbOrderDb.OrderStatus = OrderStatus.订单完成.ToString();
        _dbContext.SaveChanges();
        return ApiResult.True("订单已完成");
    }



    /// <summary>
    /// 获得列表
    /// </summary>
    /// <param name="user"></param>
    /// <param name="status"></param>
    /// <returns></returns>
    public ApiResult OrderList(string user, string status)
    {
        List<PcbOrder> list = _dbContext.PcbOrder.AsNoTracking().Where(x => x.CreateUser == user).OrderByDescending(x => x.CreateDateTime).ToList();
        if (!string.IsNullOrWhiteSpace(status))
        {
            list = list.Where(x => x.OrderStatus == status).ToList();
        }
        return ApiResult.True(new { list, list.Count });
    }





    /// <summary>
    /// 获得列表
    /// </summary>
    /// <param name="status"></param>
    /// <param name="userCode"></param>
    /// <returns></returns>
    public ApiResult EngineerOrderList(string status, string userCode)
    {
        List<PcbOrder> list = _dbContext.PcbOrder.AsNoTracking()
            .Where(x =>
                x.OrderStatus != OrderStatus.订单作废.ToString() ||
                x.AcceptEngineerCode == userCode ||
                x.AcceptEngineerCode == "")
            .OrderByDescending(x => x.CreateDateTime).ToList();
        if (!string.IsNullOrWhiteSpace(status))
        {
            list = list.Where(x => x.OrderStatus == status).ToList();
        }
        return ApiResult.True(new { list, list.Count });
    }

    /// <summary>
    /// 获得单挑
    /// </summary>
    /// <param name="code"></param>
    /// <returns></returns>
    public ApiResult Order(string code)
    {
        var pcbOrder = _dbContext.PcbOrder.AsNoTracking().FirstOrDefault(x => x.Code == code).NotNull($"Order【{code}】未查询到数据"); ;
        return ApiResult.True(new { pcbOrder });
    }
}