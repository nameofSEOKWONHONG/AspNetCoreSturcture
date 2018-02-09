using Neon.Payment.BusinessLayer.Contract;
using Neon.Payment.Models;
using NeonCore.Library;
using NeonCore.Library.Response;
using NeonCore.Payment.BusinessLayer;
using SharpRepository.EfCoreRepository;
using SharpRepository.Repository.Queries;
using System;
using System.Collections.Generic;
using System.Text;

namespace Neon.Payment.BusinessLayer
{
    public class OrderBusinessObject : BusinessObject, IOrderBusinessObject
    {
        EfCoreRepository<TaOrders> _orders;

        public OrderBusinessObject(IUserInfo userInfo, PaymentContext dbcontext) : base(userInfo, dbcontext)
        {
            _orders = new EfCoreRepository<TaOrders>(dbcontext);
        }

        public ISingleModelResponse<TaOrders> GetOrder(int orderId)
        {
            var response = new SingleModelResponse<TaOrders>();

            try
            {
                var order = _orders.Find(x => x.OrdId == orderId);

                if(order != null)
                {
                    response.Message = "OK";
                    response.Model = order;
                }
            }
            catch(Exception e)
            {
                response.ErrorMessage = e.Message;
                response.HasError = true;
            }

            return response;
        }

        public IListModelResponse<TaOrders> GetOrders(int pageNo, int pageSize)
        {
            var response = new ListModelResponse<TaOrders>();

            try
            {
                var pagingOptions = new PagingOptions<TaOrders, int>(pageNo, pageSize, x => x.OrdId, isDescending: true);

                var orders = _orders.GetAll(pagingOptions);
                var totalItems = pagingOptions.TotalItems;

                if (orders != null)
                {
                    response.Message = "OK";
                    response.Model = orders;
                    response.TotalSize = totalItems;
                }
            }
            catch (Exception e)
            {
                response.ErrorMessage = e.Message;
                response.HasError = true;
            }

            return response;
        }
    }
}
