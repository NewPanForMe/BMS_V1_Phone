namespace BMS_V2_Db.Config;

public partial  record ProjectConfig:Ys.Tools.Config.ProjectConfig
{

    public static ProjectConfig Instance { get; set; } = new ProjectConfig();

    /// <summary>
    /// 文件上传位置
    /// </summary>
    public string UploadFileFolder { get; set; }=string.Empty;
}