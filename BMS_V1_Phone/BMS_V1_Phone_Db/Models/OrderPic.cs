namespace BMS_V1_Phone.Models;

/// <summary>
/// Order的订单图片上传。最多上传5张
/// </summary>
public record OrderPic
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
    /// 图片1 
    /// </summary>  
    public string PicCode { get; set; } = string.Empty;
   
}