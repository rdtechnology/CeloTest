using CeloTest.Data.Models;
using CeloTest.Service.IService;
using CeloTest.Service.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CeloTest.Service
{
    public class UserService : IUserService
    {
        private readonly CeloTestContext _db;

        public UserService(CeloTestContext db)
        {
            _db = db;
        }

        public async Task<CommonResult<IEnumerable<UserModel>>> GetAllUserAsync(string firstName, string lasName)
        {
            var result = new CommonResult<IEnumerable<UserModel>>();

            // get first 20 user
            var users = await _db.User
                .Where(x => x.IsActive.Value
                    && (string.IsNullOrEmpty(firstName) ? true : x.FirstName.Contains(firstName) 
                        || string.IsNullOrEmpty(lasName) ? true : x.LastName.Contains(lasName))
                )
                .Select(x => new UserModel
                {
                    Id = x.Id,
                    Email = x.Email,
                    DOB = x.DateOfBirth.Value,
                    PhoneNumber = x.PhoneNumber,
                    Image = x.Image + "_thumbnail",
                    Name = x.Title + ' ' + x.FirstName + ' ' + x.LastName
                })
                .Take(20)
                .ToListAsync();

            result.Data = users;
            result.Success = true;
            return result;
        }

        public async Task<CommonResult<UserModel>> GetUserAsync(int id)
        {
            var result = new CommonResult<UserModel>();

            var user = await _db.User
                .Where(x => x.Id == id
                    && x.IsActive.Value
                )
                .Select(x => new UserModel { 
                    Id = x.Id,
                    Email = x.Email,
                    DOB = x.DateOfBirth.Value,
                    PhoneNumber = x.PhoneNumber,
                    Image = x.Image,
                    Name = x.Title + ' ' + x.FirstName + ' ' + x.LastName
                })
                .FirstOrDefaultAsync();

            result.Data = user;
            result.Success = true;
            return result;
        }

        public async Task<CommonResult<UserModel>> UpdateUserAsync(UserModel model)
        {
            var result = new CommonResult<UserModel>();
            User user;

            var timestamp = DateTime.UtcNow;

            // Create
            if (model.Id == 0)
            {
                var userExist = await _db.User
                    .AnyAsync(x => x.Email == model.Email
                        && x.IsActive.Value
                    );

                if (userExist)
                {
                    result.Code = "USER_EXIST";
                    return result;
                }

                user = new User
                {
                    Email = model.Email,
                    Title = model.Title,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    DateOfBirth = model.DOB,
                    Image = model.Image,

                    IsActive = true,
                };

                await _db.User.AddAsync(user);
            }
            // Update
            else
            {
                user = await _db.User
                    .Where(x => x.Id == model.Id
                        && x.IsActive.Value
                    )
                    .FirstOrDefaultAsync();

                if (user == null)
                {
                    result.Code = "EMPLOYEE_NOT_FOUND";
                    result.Message = "Employee Not Found";
                    return result;
                }

                user.Title = model.Title;
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;
                user.DateOfBirth = model.DOB;
                user.Image = model.Image;
            }

            await _db.SaveChangesAsync();

            if (user == null)
            {
                result.Code = "EMPLOYEE_NOT_FOUND";
                result.Message = "Employee Not Found";
                return result;
            }

            var tempuser = new UserModel
            {
                Id = user.Id,
                Email = user.Email,
                DOB = user.DateOfBirth.Value,
                PhoneNumber = user.PhoneNumber,
                Image = user.Image,
                Name = user.Title + ' ' + user.FirstName + ' ' + user.LastName
            };

            result.Data = tempuser;
            result.Success = true;
            return result;
        }

        public async Task<CommonResult<UserModel>> DeleteUserAsync(int id)
        {
            var result = new CommonResult<UserModel>();

            var user = await _db.User
                .Where(x => x.Id == id
                    && x.IsActive.Value
                )
                .FirstOrDefaultAsync();

            user.IsActive = false;

            // result.Data = user;
            result.Success = true;
            return result;
        }

    }
}
