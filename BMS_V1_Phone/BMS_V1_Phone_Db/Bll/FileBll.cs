using BMS_V1_Phone.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Ys.Tools.Config;
using Ys.Tools.Extra;
using Ys.Tools.Interface;
using Ys.Tools.Response;

namespace BMS_V1_Phone.Bll;

public class FileBll : IBll
{
    private readonly BmsV2DbContext _dbContext;
    public FileBll(BmsV2DbContext dbContext)
    {
        _dbContext = dbContext;
    }


    /// <summary>
    /// 上传
    /// </summary>
    /// <param name="file"></param>
    /// <param name="userCode"></param>
    /// <param name="userName"></param>
    public async Task<ApiResult> Upload(IFormFile file, string userCode, string userName)
    {
        var dateTime = DateTime.Now.ToString("yyyy-MM-dd");
        var fileFullPath = Path.Combine(BMS_V1_Phone.Config.ProjectConfig.Instance.UploadFileFolder, dateTime);
        if (!Directory.Exists(fileFullPath)) Directory.CreateDirectory(fileFullPath);
        var fileName = Guid.NewGuid() + "-" + file.FileName;
        //绝对路径
        var filePath = fileFullPath + "\\" + fileName;
        //相对路径
        var relativePath = dateTime + "\\" + fileName;

        await using var stream = System.IO.File.Create(filePath);
        await file.CopyToAsync(stream);
        var files = new FileUpload()
        {
            UserCode = userCode,
            UserName = userName,
            Code = Guid.NewGuid().ToString(),
            CreateDate = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss"),
            FullName = file.FileName,
            Location = filePath,
            RelativePath = relativePath
        };
        _dbContext.FileUpload.Add(files);
        await _dbContext.SaveChangesAsync();    
        return ApiResult.True(new { code = files.Code, location = files.RelativePath, });
    }


    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<FileUpload>> GetFileUpload()
    {
        var listAsync = await _dbContext.FileUpload.OrderByDescending(x => x.CreateDate).Take(20)
            .AsNoTracking().ToListAsync();
        return listAsync;
    }

    /// <summary>
    /// 获取列表
    /// </summary>
    /// <returns></returns>
    public async Task<List<FileUpload>> GetHasUploadList(string[] args)
    {
        var listAsync = await _dbContext.FileUpload.Where(x => args.Contains(x.Code))
            .OrderByDescending(x => x.CreateDate).AsNoTracking().ToListAsync();
        return listAsync;
    }
    /// <summary>
    /// 删除
    /// </summary>
    /// <param name="file"></param>
    /// <returns></returns>
    public void Delete(FileUpload file)
    {
        _dbContext.FileUpload.Remove(file);
    }

    /// <summary>
    /// 获取模块
    /// </summary>
    /// <returns></returns>
    public FileUpload GetFileUploadEntityByCode(string code)
    {
        code.NotNull("传入编号为空");
        var module = _dbContext.FileUpload.AsNoTracking().FirstOrDefault(x => x.Code.Equals(code));
        module = module.NotNull("当前数据不存在");
        return module;
    }

}