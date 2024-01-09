using System.Text.Json;
using BMS_V1_Phone;
using BMS_V1_Phone.Bll;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Controllers;
using Ys.Tools.Extra;
using Ys.Tools.Response;

namespace BMS_V2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class FileController : BaseController
    {
        private readonly BmsV2DbContext _dbContext;
        private readonly FileBll _fileBll;
        public FileController(BmsV2DbContext dbContext, FileBll fileBll)
        {
            _dbContext = dbContext;
            _fileBll = fileBll;
        }


        [HttpPost]
        public async Task<ApiResult> Upload(IFormFile file)
        {
            return await _fileBll.Upload(file, CurrentUser.Code, CurrentUser.Name);
        }


    }
}
