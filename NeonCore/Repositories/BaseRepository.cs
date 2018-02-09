using Microsoft.Extensions.Options;
using NeonCore.Library;
using NeonCore.Repositories.Contract;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace NeonCore.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        protected IDbConnection Connection { get; }
        protected bool Disposed = false;

        public BaseRepository(IOptions<AppSettings> appSettings)
        {
            Connection = new SqlConnection(appSettings.Value.JWDBInstance);
        }

        public void Dispose()
        {
            if(!Disposed)
            {
                if(Connection != null)
                {
                    Connection.Close();
                    Connection.Dispose();

                    Disposed = true;
                }
            }
        }
    }
}
