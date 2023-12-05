using BMS_V2_Db.Bll;
using BMS_V2_Db.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using Ys.Tools.Controllers;
using Ys.Tools.Extra;
using Ys.Tools.Response;

namespace BMS_V2.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize]
    public class UserAddressController : BaseController
    {


        public UserAddressController(UserAddressBll addressBll, ILogger<UserAddressController> logger)
        {
            _addressBll = addressBll;
            _logger = logger;
        }

        private readonly UserAddressBll _addressBll;
        private readonly ILogger<UserAddressController> _logger;



        [HttpPost]
        public async Task<ApiResult> Add(UserAddress address)
        {
            return await _addressBll.Add(address, CurrentUser.Code);
        }

        [HttpPost]
        public ApiResult UserAddressList()
        {
            return _addressBll.UserAddressList(CurrentUser.Code);
        }



    }
}
