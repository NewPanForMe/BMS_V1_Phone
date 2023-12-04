namespace BMS_V2_Db.Models;

/// <summary>
/// 基础用户表 
/// </summary>
public record UserCustomer
{
    /// <summary>  
    /// 序号i 
    /// </summary>  
    public long Id { get; set; } = 0;
    /// <summary>  
    /// 主键 
    /// </summary>  
    public string Code { get; set; } = string.Empty;
    /// <summary>  
    /// 账号 
    /// </summary>  
    public string Account { get; set; } = string.Empty;
    /// <summary>  
    /// 密码 
    /// </summary>  
    public string Password { get; set; } = string.Empty;
    /// <summary>  
    /// 密码盐 
    /// </summary>  
    public string PasswordSalt { get; set; } = string.Empty;

}