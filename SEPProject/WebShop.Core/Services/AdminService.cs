using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;
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
            if (String.IsNullOrEmpty(admin.Password) || String.IsNullOrEmpty(admin.Username) 
                || String.IsNullOrEmpty(admin.Name))
            {
                return Result.Failure("Username, password or name can't be empty!");
            }
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            admin.Salt = Convert.ToBase64String(salt);
            admin.Password = GetHashCode(admin.Password, salt);
            _adminRepository.Save(admin);
            return Result.Success(admin);
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
