using NeonCore.Library.Response;
using NeonCore.Models;

namespace NeonCore.BusinessLayer.Contract
{
    public interface IUserBusinessObject : IBusinessObject
    {
        IListModelResponse<TaUser> GetUsers(int pageNo, int pageSize);
        ISingleModelResponse<TaUser> GetUser(int userIdx);
    }
}
