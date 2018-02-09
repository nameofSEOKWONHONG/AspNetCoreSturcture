using NeonCore.Library.Response;
using NeonCore.Models;

namespace NeonCore.Repositories.Contract
{
    public interface IUserRepository
    {
        IListModelResponse<TaUser> GetUsers(int pageNo, int pageSize);
    }
}
