using CSharpFunctionalExtensions;
using System;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class AdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IUserRepository _userRepository;

        public AdminService(IAdminRepository adminRepository,
            IUserRepository userRepository)
        {
            _adminRepository = adminRepository;
            _userRepository = userRepository;
        }

        public Result Register(Admin admin)
        {
            if (_userRepository.GetByUsername(admin.Username) != null)
            {
                return Result.Failure("User with that username already exists!");
            }
            if (String.IsNullOrEmpty(admin.Password) || String.IsNullOrEmpty(admin.Username))
            {
                return Result.Failure("Username or password can't be empty!");
            }
            _adminRepository.Save(admin);
            return Result.Success(admin);
        }
    }
}
