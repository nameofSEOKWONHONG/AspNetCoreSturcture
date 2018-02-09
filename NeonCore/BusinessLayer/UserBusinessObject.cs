using NeonCore.BusinessLayer.Contract;
using NeonCore.Library;
using NeonCore.Library.Response;
using NeonCore.Models;
using SharpRepository.EfCoreRepository;
using SharpRepository.Repository.Queries;

namespace NeonCore.BusinessLayer
{
    public class UserBusinessObject : BusinessObject, IUserBusinessObject
    {
        EfCoreRepository<TaUser> _userRepo;

        public UserBusinessObject(IUserInfo userInfo, JWDBContext dbcontext) : base(userInfo, dbcontext)
        {
            _userRepo = new EfCoreRepository<TaUser>(dbcontext);
        }

        public IListModelResponse<TaUser> GetUsers(int pageNo, int pageSize)
        {
            var pagingOptions = new PagingOptions<TaUser, int>(pageNo, pageSize, x => x.UserIdx, isDescending: true);
            var pagingUsers = _userRepo.GetAll(pagingOptions);
            var totalItems = pagingOptions.TotalItems;

            return new ListModelResponse<TaUser>()
            {
                ErrorMessage = "",
                HasError = false,
                Message = "OK",
                Model = pagingUsers,
                PageNumber = pageNo,
                PageSize = pageSize,
                TotalSize = totalItems
            };           
        }

        public ISingleModelResponse<TaUser> GetUser(int userIdx)
        {
            var user = _userRepo.Find(u => u.UserIdx == userIdx);

            return new SingleModelResponse<TaUser>
            {
                ErrorMessage = "",
                HasError = false,
                Message = "OK",
                Model = user
            };
        }
    }
}
