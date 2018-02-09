using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NeonCore.BusinessLayer.Contract;
using NeonCore.WebAPI.Extensions;

namespace NeonCore.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/UserManage")]
    public class UserManageController : Controller
    {
        IUserBusinessObject _userBizObj;
        public UserManageController(IUserBusinessObject userBizObj)
        {
            _userBizObj = userBizObj;
        }

        [Route("GetUsers")]
        public async Task<IActionResult> GetUsers()
        {
            var response = await Task.Run(() =>
            {
                return _userBizObj.GetUsers(1, 20);
            });
            return response.ToHttpResponse();
        }

        [Route("GetUser")]
        public async Task<IActionResult> GetUser(int userIdx = 0)
        {
            var response = await Task.Run(() =>
            {
                return _userBizObj.GetUser(userIdx);
            });

            return response.ToHttpResponse();
        }

        protected override void Dispose(bool disposing)
        {
            _userBizObj.Dispose();

            base.Dispose(disposing);
        }
    }
}