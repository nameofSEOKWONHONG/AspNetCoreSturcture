using Dapper;
using Microsoft.Extensions.Options;
using NeonCore.Library;
using NeonCore.Library.Response;
using NeonCore.Models;
using NeonCore.Repositories.Contract;

namespace NeonCore.Repositories
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(IOptions<AppSettings> appSettings) : base(appSettings)
        {

        }

        public IListModelResponse<TaUser> GetUsers(int pageNo, int pageSize)
        {
            //return Connection.Query<TaUser>("SELECT * FROM TA_USER");

            return null;
        }
    }
}