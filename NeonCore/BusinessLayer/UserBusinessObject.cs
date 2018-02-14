using NeonCore.BusinessLayer.Contract;
using NeonCore.Library;
using NeonCore.Library.Response;
using NeonCore.Models;
using SharpRepository.EfCoreRepository;
using SharpRepository.Repository.Queries;
using System;

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

        /// <summary>
        /// 트랜젝션을 사용한 예
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ISingleModelResponse<Int32> AddUser(TaUser user)
        {
            var response = new SingleModelResponse<Int32>();

            var existObj = _userRepo.Find(u => u.UserIdx == user.UserIdx);

            if(!existObj.IsExist())
            {
                using (var tran = DbContext.CreateTransaction())
                {
                    try
                    {
                        var result = DbContext.Add<TaUser>(user);

                        DbContext.SaveChanges();

                        tran.Commit();

                        response.HasError = false;
                        response.Message = "정상처리 되었습니다.";
                        response.Model = user.UserIdx;
                    }
                    catch(Exception e)
                    {
                        tran.Rollback();

                        response.HasError = true;
                        response.Message = e.Message;
                    }
                }
            }
            else
            {
                response.HasError = true;
                response.ErrorMessage = "기존 내역이 존재 합니다.";
            }

            return response;
        }

        /// <summary>
        /// sharprepository를 사용한 예
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public ISingleModelResponse<Int32> UpdateUser(TaUser user)
        {
            var response = new SingleModelResponse<Int32>();
            
            var updateObj = _userRepo.Find(u => u.UserIdx == user.UserIdx);

            if (updateObj.IsExist())
            {
                try
                {
                    _userRepo.Update(user);
                }
                catch(Exception e)
                {
                    response.HasError = true;
                    response.Message = e.Message;
                }
            }
            else
            {
                response.ErrorMessage = "업데이트 할 내역이 없습니다.";
                response.HasError = true;
            }

            return response;
        }

        /// <summary>
        /// sharprepository를 사용한 예
        /// </summary>
        /// <param name="userIdx"></param>
        /// <returns></returns>
        public ISingleModelResponse<Int32> DeleteUser(int userIdx)
        {
            var response = new SingleModelResponse<Int32>();

            var removeObj = _userRepo.Find(u => u.UserIdx == userIdx);

            if (removeObj.IsExist())
            {
                try
                {
                    _userRepo.Delete(removeObj);
                }
                catch (Exception e)
                {
                    response.HasError = true;
                    response.Message = e.Message;
                }
            }
            else
            {
                response.ErrorMessage = "삭제 할 내역이 없습니다.";
                response.HasError = true;
            }

            return response;
        }
    }
}
