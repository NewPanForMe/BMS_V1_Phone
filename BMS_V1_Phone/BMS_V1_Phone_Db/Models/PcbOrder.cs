namespace BMS_V1_Phone.Models;

/// <summary>
/// 订单表
/// </summary>
public record PcbOrder
{
    /// <summary>  
    /// 序号 
    /// </summary>  
    public long Id { get; set; } = 0;
    /// <summary>  
    /// 主键 
    /// </summary>  
    public string Code { get; set; } = string.Empty;
    /// <summary>  
    /// 标题 
    /// </summary>  
    public string Title { get; set; } = string.Empty;
    /// <summary>  
    /// 具体需求 
    /// </summary>  
    public string Comment { get; set; } = string.Empty;
    /// <summary>  
    /// 估价 
    /// </summary>  
    public string Evaluate { get; set; } = string.Empty;
    /// <summary>  
    /// 完成时间 
    /// </summary>  
    public string FinishTime { get; set; } = string.Empty;
    /// <summary>  
    /// 实际完成时间 
    /// </summary>  
    public string RealFinishTime { get; set; } = string.Empty;
    /// <summary>  
    /// 订单创建时间 
    /// </summary>  
    public string CreateDateTime { get; set; } = string.Empty;
    /// <summary>  
    /// 订单创建人 
    /// </summary>  
    public string CreateUser { get; set; } = string.Empty;
    /// <summary>  
    /// 工程师备注 
    /// </summary>  
    public string Remark { get; set; } = string.Empty;
    /// <summary>  
    /// 接单工程师 
    /// </summary>  
    public string AcceptEngineerCode { get; set; } = string.Empty;
    /// <summary>  
    /// 接单工程师名称 
    /// </summary>  
    public string AcceptEngineer { get; set; } = string.Empty;
    /// <summary>  
    /// 接单时间 
    /// </summary>  
    public string AcceptDateTime { get; set; } = string.Empty;
    /// <summary>  
    /// 订单状态
    /// 0-未接单
    /// 1-已接单
    /// 2-已估价
    /// 3-确认订单
    /// 4-已完成，待发送快递
    /// 5-快递已发出
    /// 6-订单完成
    /// -1 -订单拒绝
    /// -2 -订单作废 
    /// </summary>  
    public string OrderStatus { get; set; } = string.Empty;
    /// <summary>  
    /// 物流单号 
    /// </summary>  
    public string FollowNum { get; set; } = string.Empty;

}