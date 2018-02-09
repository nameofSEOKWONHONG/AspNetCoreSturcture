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

namespace NeonCore.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Payment")]
    public class PaymentController : Controller
    {
        IUserInfo _userinfo;
        IOrderBusinessObject _orderBizObj;

        public PaymentController(IUserInfo userinfo, IOrderBusinessObject orderBizObj)
        {
            userinfo = _userinfo;
            _orderBizObj = orderBizObj;
        }

        public IListModelResponse<TaOrders> GetOrders()
        {
            return _orderBizObj.GetOrders(1, 20);
        }
    }
}