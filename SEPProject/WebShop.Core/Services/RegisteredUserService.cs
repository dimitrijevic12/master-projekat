using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
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
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            registeredUser.Salt = Convert.ToBase64String(salt);
            registeredUser.Password = GetHashCode(registeredUser.Password, salt);
            _registeredUserRepository.Save(registeredUser);
            return Result.Success(registeredUser);
        }

        private static string GetHashCode(string password, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
