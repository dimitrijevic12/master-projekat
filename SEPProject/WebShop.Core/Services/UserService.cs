using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WebShop.Core.Interface.Repository;
using WebShop.Core.Model;

namespace WebShop.Core.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRegisteredUserRepository _registeredUserRepository;
        private IConfiguration _config;

        public UserService(IUserRepository userRepository,
            IConfiguration config, IRegisteredUserRepository registeredUserRepository)
        {
            _userRepository = userRepository;
            _config = config;
            _registeredUserRepository = registeredUserRepository;
        }

        public User FindUser(String username, String password)
        {
            var user = _userRepository.GetByUsername(username);
            return user == null || !user.Password.ToString().Equals(
                GetHashCode(password, Convert.FromBase64String(user.Salt))) ?
                null : user;
        }

        public string GenerateJSONWebToken(User userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            string itRole = "";
            if (userInfo.GetType().Name == "RegisteredUserProxy")
            {
                itRole = _registeredUserRepository.GetById(userInfo.Id).ITRole;
            }
            var claims = new[] {
                new Claim("user_id", userInfo.Id.ToString()),
                new Claim("username", userInfo.Username),
                new Claim("role", userInfo.GetType().Name),
                new Claim("itRole", itRole),
                new Claim (ClaimTypes.Role, userInfo.GetType().Name)
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
