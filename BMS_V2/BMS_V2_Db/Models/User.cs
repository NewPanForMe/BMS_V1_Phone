namespace BMS_V2_Db.Models;

/// <summary>
/// 用户信息
/// </summary>
public record User
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
    /// 登录名 
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
    /// <summary>  
    /// 手机号 
    /// </summary>  
    public string Phone { get; set; } = string.Empty;
    /// <summary>  
    /// 邮箱 
    /// </summary>  
    public string Mail { get; set; } = string.Empty;
    /// <summary>  
    /// 是否锁定 
    /// </summary>  
    public bool IsLock { get; set; } = false;
    /// <summary>  
    /// 用户名 
    /// </summary>  
    public string UserName { get; set; } = string.Empty;
    /// <summary>  
    /// 是否删除 
    /// </summary>  
    public bool IsDelete { get; set; } = false;

}