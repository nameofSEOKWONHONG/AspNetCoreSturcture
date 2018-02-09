using NeonCore.BusinessLayer.Contract;
using NeonCore.Library;
using System;

namespace NeonCore.BusinessLayer
{
    public abstract class BusinessObject : IBusinessObject
    {
        protected IUserInfo UserInfo;
        protected JWDBContext DbContext;
        protected Boolean Disposed;

        public BusinessObject(IUserInfo userInfo, JWDBContext dbcontext)
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
