using BMS_V2_Db.Bll;
using BMS_V2_Db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ys.Tools.Controllers;
using Ys.Tools.Models;
using Ys.Tools.Response;

namespace BMS_V2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class PcbOrderController : BaseController
    {
        public PcbOrderController(PcbOrderBll pcbBll, ILogger<PcbOrderController> logger)
        {
            _pcbBll = pcbBll;
            _logger = logger;
        }

        private readonly PcbOrderBll _pcbBll;
        private readonly ILogger<PcbOrderController> _logger;

        [HttpPost]
        public  async Task<ApiResult> Add(PcbOrder pcbOrder)
        {
           return await _pcbBll.Add(pcbOrder,CurrentUser.Name);
        }


        [HttpPost]
        public  ApiResult ReceiveOrder(PcbOrder pcbOrder)
        {
            return  _pcbBll.ReceiveOrder(pcbOrder, CurrentUser.Name);
        }

        [HttpPost]
        public ApiResult Evaluate(PcbOrder pcbOrder)
        {
            return _pcbBll.Evaluate(pcbOrder);
        }


        
    }
}
