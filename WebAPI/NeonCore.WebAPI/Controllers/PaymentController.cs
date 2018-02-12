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
using NLog;

namespace NeonCore.WebAPI.Controllers
{
    [Produces("application/json")]
    [Route("api/Payment")]
    public class PaymentController : BaseController
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

            Logger.Trace("payment.getorders");
            DbLogger.Trace("payment.getorders");
            return _orderBizObj.GetOrders(1, 20);
        }
    }
}