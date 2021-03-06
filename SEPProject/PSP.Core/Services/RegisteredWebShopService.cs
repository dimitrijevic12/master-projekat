using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PSP.Core.DTOs;
using PSP.Core.Interface.Repository;
using PSP.Core.Model;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PSP.Core.Services
{
    public class RegisteredWebShopService
    {
        private readonly IRegisteredWebShopRepository _registeredWebShopRepository;
        private IConfiguration _config;

        public RegisteredWebShopService(IRegisteredWebShopRepository registeredWebShopRepository,
            IConfiguration config)
        {
            _registeredWebShopRepository = registeredWebShopRepository;
            _config = config;
        }

        public Result Save(RegisteredWebShopDTO registeredWebShopDTO)
        {
            if (_registeredWebShopRepository.GetByEmail(registeredWebShopDTO.EmailAddress) != null) return Result.Failure("WebShop with that email already exists!");
            if (registeredWebShopDTO.Password == "") return Result.Failure("Password can't be empty!");
            if (registeredWebShopDTO.WebShopName == "") return Result.Failure("Name can't be empty!");
            registeredWebShopDTO.WebShopId = _registeredWebShopRepository.GetAll().ToList().Max(shop => shop.WebShopId) + 1;

            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }

            RegisteredWebShop webShop = _registeredWebShopRepository.Save(new RegisteredWebShop(registeredWebShopDTO.Id, registeredWebShopDTO.WebShopId, registeredWebShopDTO.WebShopName,
                GetHashCode(registeredWebShopDTO.Password, salt), Convert.ToBase64String(salt), registeredWebShopDTO.EmailAddress, registeredWebShopDTO.SuccessUrl, registeredWebShopDTO.FailedUrl, registeredWebShopDTO.ErrorUrl));
            return Result.Success(webShop);
        }

        public RegisteredWebShop GetWebShopByEmail(String email)
        {
            return _registeredWebShopRepository.GetByEmail(email);
        }

        public RegisteredWebShop FindWebShop(String email, String password)
        {
            var webShop = _registeredWebShopRepository.GetByEmail(email);
            return webShop == null || !webShop.Password.ToString().
                Equals(GetHashCode(password, Convert.FromBase64String(webShop.Salt))) ? 
                null : webShop;
        }

        public string GenerateJSONWebToken(RegisteredWebShop webShopInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


            var test = webShopInfo.GetType().Name;
            var claims = new[] {
                new Claim("webshop_id", webShopInfo.Id.ToString()),
                new Claim("email", webShopInfo.EmailAddress),
                new Claim("name", webShopInfo.WebShopName),
                new Claim("role", webShopInfo.GetType().Name),
                new Claim (ClaimTypes.Role, webShopInfo.GetType().Name)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Issuer"],
                claims,
                expires: DateTime.Now.AddMinutes(120),
                signingCredentials: credentials);
            return new JwtSecurityTokenHandler().WriteToken(token);
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
