using Microsoft.IdentityModel.Tokens;
using MobileWarehouse.Common.Models;
using MobileWarehouse.Entity.Repository.Interface;
using MobileWarehouse.Repository.Interface;
using Serilog;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MobileWarehouse.Repository.Implementation
{
    public class TokenRepository : ITokenRepository
    {
        private readonly IUserRepository _userRepository;

        public TokenRepository(IUserRepository userRepository)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<object> GetToken(LoginModel model)
        {
            var identity = await GetIdentity(model);
            if (identity == null)
            {
                return "Invalid username or password.";
            }

            var now = DateTime.UtcNow;
            // create JWT-token
            var jwt = new JwtSecurityToken(
                    issuer: AuthOptions.ISSUER,
                    audience: AuthOptions.AUDIENCE,
                    notBefore: now,
                    claims: identity.Claims,
                    expires: now.Add(TimeSpan.FromMinutes(AuthOptions.LIFETIME)),
                    signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256));
            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            
            var response = new
            {
                access_token = encodedJwt,
                username = identity.Name
            };

            return response;
        }

        private async Task<ClaimsIdentity> GetIdentity(LoginModel model)
        {
            var user = await _userRepository.FindUserFromDbAsync(model);
            if (user != null)
            {
                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, user.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role.Name)
                };
                var claimsIdentity = new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }

            // if user is not found
            return null;
        }

        public bool ValidateToken(string token)
        {
            var tokenWithoutBearer = token.Replace("Bearer ", "");
            var tokenHandler = new JwtSecurityTokenHandler();
            try
            {
                tokenHandler.ValidateToken(tokenWithoutBearer, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidIssuer = AuthOptions.ISSUER,
                    ValidAudience = AuthOptions.AUDIENCE,
                    IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey()
                }, out SecurityToken validatedToken);
            }
            catch (Exception e)
            {
                Log.Error($"{nameof(ValidateToken)} | Message - {e.Message}");
                return false;
            }
            return true;
        }
    }
}
