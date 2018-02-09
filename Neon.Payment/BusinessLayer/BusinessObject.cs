using Neon.Payment;
using NeonCore.Library;
using NeonCore.Payment.BusinessLayer.Contract;
using System;

namespace NeonCore.Payment.BusinessLayer
{
    public abstract class BusinessObject : IBusinessObject
    {
        protected IUserInfo UserInfo;
        protected Boolean Disposed;
        protected PaymentContext DbContext;

        public BusinessObject(IUserInfo userInfo, PaymentContext dbcontext)
        {
            UserInfo = userInfo;
            DbContext = dbcontext;
        }

        public void Dispose()
        {
            if (!Disposed)
            {
                if (DbContext != null)
                {
                    DbContext.Dispose();

                    Disposed = true;
                }
            }
        }
    }
}
