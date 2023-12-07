namespace BMS_V2_Db.Models;

/// <summary>
/// 文件上传记录表
/// </summary>
public record FileUpload
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
  /// 用户编号 
   /// </summary>  
   public  string UserCode   { get; set; }   =string.Empty;
  /// <summary>  
  /// 用户名称 
   /// </summary>  
   public  string UserName   { get; set; }   =string.Empty;
  /// <summary>  
  /// 文件名称 
   /// </summary>  
   public  string FullName   { get; set; }   =string.Empty;
  /// <summary>  
  /// 绝对路径 
   /// </summary>  
   public  string Location   { get; set; }   =string.Empty;
  /// <summary>  
  /// 上传时间 
   /// </summary>  
   public  string CreateDate   { get; set; }   =string.Empty;
  /// <summary>  
  /// 相对路径 
   /// </summary>  
   public  string RelativePath   { get; set; }   =string.Empty;

}