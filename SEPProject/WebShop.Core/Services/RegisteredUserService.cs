using CSharpFunctionalExtensions;
using System;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class RegisteredUserService
    {
        private readonly IRegisteredUserRepository _registeredUserRepository;
        private readonly IUserRepository _userRepository;

        public RegisteredUserService(IRegisteredUserRepository registeredUserRepository, 
            IUserRepository userRepository)
        {
            _registeredUserRepository = registeredUserRepository;
            _userRepository = userRepository;
        }

        public Result Register(RegisteredUser registeredUser)
        {
            if (_userRepository.GetByUsername(registeredUser.Username) != null)
            {
                return Result.Failure("User with that username already exists!");
            }
            if (String.IsNullOrEmpty(registeredUser.Password) || 
                String.IsNullOrEmpty(registeredUser.Username))
            {
                return Result.Failure("Username or password can't be empty!");
            }
            _registeredUserRepository.Save(registeredUser);
            return Result.Success(registeredUser);
        }
    }
}
