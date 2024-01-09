using BMS_V1_Phone.Bll;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Config;
using Ys.Tools.Response;

namespace BMS_V2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProjectInfoController : ControllerBase
    {

        public ProjectInfoController( ILogger<ProjectInfoController> logger)
        {
            _logger = logger;
        }

        private readonly ILogger<ProjectInfoController> _logger;


        [HttpGet]
        public ApiResult GetProjectInfo()
        {
            return ApiResult.True(new { ProjectConfig.Instance.ProjectName });
        }

    }
}
