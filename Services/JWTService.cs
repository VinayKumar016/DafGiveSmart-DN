using CDTApi.DTOs;
using CDTApi.Models;
using CDTApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 
using Microsoft.IdentityModel.Tokens; 

namespace CDTApi.Services
{
    public class JWTService{
        public string CreateJWTToken(User user)
        {
            var jwtHandler = new JwtSecurityTokenHandler(); // Correct variable name
            var key = Encoding.ASCII.GetBytes("a-string-secret-at-least-256-bits-long");
            var identity = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            });
        
            var credentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = identity,
                Expires = DateTime.UtcNow.AddMinutes(1),
                SigningCredentials = credentials
            };
        
            var token = jwtHandler.CreateToken(tokenDescriptor); // Use jwtHandler
            return jwtHandler.WriteToken(token); // Use jwtHandler
        }
    }
}