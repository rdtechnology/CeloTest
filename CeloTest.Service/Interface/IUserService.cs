using CeloTest.Service.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CeloTest.Service.IService
{
    public interface IUserService
    {
        Task<CommonResult<UserModel>> GetUserAsync(int id);

        Task<CommonResult<IEnumerable<UserModel>>> GetAllUserAsync(string firstName, string lasName);

        Task<CommonResult<UserModel>> UpdateUserAsync(UserModel model);

        Task<CommonResult<UserModel>> DeleteUserAsync(int id);

    }
}
