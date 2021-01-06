using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using API.Entities;
using API.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace API.Services
{
    public class TokenService : ITokenService
    {
        private readonly SymmetricSecurityKey _key;
        public TokenService(IConfiguration config)  // Đây chính là Constructor
        {
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]));
        }

        public string CreateToken(AppUser user)
        {
            var claims = new List<Claim>
           {
               new Claim(JwtRegisteredClaimNames.NameId, user.UserName )
           };

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature); // HmacSha512Signature: Chọn thuật toán mạnh nhất

            var TokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7), // Thời gian hết hạn của Token
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(TokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
