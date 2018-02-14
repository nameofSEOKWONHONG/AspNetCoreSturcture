using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Neon.Payment.BusinessLayer.Contract;
using Neon.Payment.Models;
using NeonCore.Library;
using NeonCore.Library.Response;
using NeonCore.WebAPI.Extensions;
using NLog;

namespace NeonCore.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class PaymentController : Controller
    {
        IUserInfo _userinfo;
        IOrderBusinessObject _orderBizObj;

        public PaymentController(IUserInfo userinfo, IOrderBusinessObject orderBizObj)
        {
            userinfo = _userinfo;
            _orderBizObj = orderBizObj;
        }

        [HttpGet]
        [Route("GetOrders")]
        public IActionResult GetOrders()
        {

            //Logger.Trace("payment.getorders");
            //DbLogger.Trace("payment.getorders");
            return _orderBizObj.GetOrders(1, 20).ToHttpResponse();
        }
    }
}