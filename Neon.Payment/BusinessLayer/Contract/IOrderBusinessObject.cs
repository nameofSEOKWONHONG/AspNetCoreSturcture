using Neon.Payment.Models;
using NeonCore.Library.Response;
using NeonCore.Payment.BusinessLayer.Contract;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neon.Payment.BusinessLayer.Contract
{
    public interface IOrderBusinessObject : IBusinessObject
    {
        IListModelResponse<TaOrders> GetOrders(int pageNo, int pageSize);
        ISingleModelResponse<TaOrders> GetOrder(int orderId);
    }
}
