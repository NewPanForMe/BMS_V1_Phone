namespace BMS_V2_Db.Models;

/// <summary>
/// 订单拒绝表
/// </summary>
public record OrderRefuse
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
    /// 订单编号 
    /// </summary>  
    public string OrderCode { get; set; } = string.Empty;
    /// <summary>  
    /// 估价 
    /// </summary>  
    public string OrderEvaluate { get; set; } = string.Empty;
    /// <summary>  
    /// 工程师备注 
    /// </summary>  
    public string OrderRemark { get; set; } = string.Empty;
    /// <summary>  
    /// 接单工程师 
    /// </summary>  
    public string OrderAcceptEngineer { get; set; } = string.Empty;
    /// <summary>  
    /// 接单时间 
    /// </summary>  
    public string OrderAcceptDateTime { get; set; } = string.Empty;
    /// <summary>  
    /// 退单时间 
    /// </summary>  
    public string CreateDateTime { get; set; } = string.Empty;

}