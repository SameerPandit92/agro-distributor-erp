using AgroERP.Application.DTOs.Auth;
using AgroERP.Application.Interfaces;
using AgroERP.Domain.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Application.Services
{
    public class AuthService  : IAuthService
    {
        private readonly IAuthRepository _repository;
        private readonly IConfiguration _configuration;

        public AuthService(IAuthRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetByUsernameAsync(dto.UserName);
            if (user == null)
            {
                throw new Exception("Invalid username");
            }

            bool isPasswordValid =BCrypt.Net.BCrypt.Verify(dto.Password,user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid password");
            }
            var claims = new[]
            {
              new Claim(ClaimTypes.Name, user.UserName)
              //new Claim(ClaimTypes.Role,user.Role)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _configuration["Jwt:Key"]!));
            var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],

                    audience: _configuration["Jwt:Audience"],
                    claims:claims,
                    expires: DateTime.Now.AddHours(5),
                    signingCredentials:creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public async Task<string> RegisterAsync( RegisterDto dto)
        {
            var existingUser = await _repository.GetByUsernameAsync(dto.UserName);
            if (existingUser != null)
            {
                throw new Exception("Username already exists");
            }
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email,
                PasswordHash = hashedPassword, 
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                DateOfBirth = dto.DateOfBirth,
                PhoneNumber = dto.PhoneNumber
            };


            await _repository.AddUserAsync(user);
            return "User Registered Successfully";
        }

    }
}
