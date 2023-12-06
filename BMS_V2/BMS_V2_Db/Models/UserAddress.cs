namespace BMS_V2_Db.Models;

/// <summary>
/// 用户地址表
/// 用于确认客户的邮寄地址
/// </summary>
public record UserAddress
{
      /// <summary>  
  /// 序号i 
   /// </summary>  
   public  long Id   { get; set; }   =0;
  /// <summary>  
  /// 主键 
   /// </summary>  
   public  string Code   { get; set; }   =string.Empty;
  /// <summary>  
  /// 对应用户编号 
   /// </summary>  
   public  string UserCode   { get; set; }   =string.Empty;
  /// <summary>  
  /// 手机号 
   /// </summary>  
   public  string Phone   { get; set; }   =string.Empty;
  /// <summary>  
  /// 姓名 
   /// </summary>  
   public  string Name   { get; set; }   =string.Empty;
  /// <summary>  
  /// 地址信息 
   /// </summary>  
   public  string Address   { get; set; }   =string.Empty;

}