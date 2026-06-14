using AgroERP.Application.DTOs.AssignRoleDto;
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
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _repository.GetByUsernameAsync(dto.UserName);

            if (user == null)
            {
                throw new Exception("Invalid username");
            }
            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!isPasswordValid)
            {
                throw new Exception("Invalid password");
            }

            var role = await _repository.GetUserRoleAsync(user.Id);
            var claims = new[]
            {
             new Claim( ClaimTypes.Name,user.UserName),
             new Claim( ClaimTypes.Role, role ?? "User")
            };

            var key = new SymmetricSecurityKey( Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
            var creds =new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token =
                new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],
                    audience: _configuration["Jwt:Audience"],
                    claims:claims,
                    expires:DateTime.Now.AddMinutes(30),
                    signingCredentials:creds);

            // Refresh Token
            var refreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = refreshToken;

            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await _repository.UpdateUserAsync(user);

            return new AuthResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken = refreshToken
            };
        }

        //public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        //{
        //    var user = await _repository.GetByUsernameAsync(dto.UserName);

        //    if (user == null)
        //    {
        //        throw new Exception("Invalid username");
        //    }

        //    bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password,user.PasswordHash);
        //    if (!isPasswordValid)
        //    {
        //        throw new Exception("Invalid password");
        //    }
        //    var role = await _repository.GetUserRoleAsync(user.Id);
        //    var claims = new[]
        //    {
        //      new Claim( ClaimTypes.Name,user.UserName),
        //      new Claim(ClaimTypes.Role, role ?? "User")
        //    };

        //    var key =new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));
        //    var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);
        //    var token = new JwtSecurityToken(
        //            issuer: _configuration["Jwt:Issuer"],
        //            audience: _configuration["Jwt:Audience"],
        //            claims: claims,
        //            expires: DateTime.Now.AddHours(5),
        //            signingCredentials: creds);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        //public async Task<string> LoginAsync(LoginDto dto)
        //{
        //    var user = await _repository.GetByUsernameAsync(dto.UserName);
        //    if (user == null)
        //    {
        //        throw new Exception("Invalid username");
        //    }

        //    bool isPasswordValid =BCrypt.Net.BCrypt.Verify(dto.Password,user.PasswordHash);
        //    if (!isPasswordValid)
        //    {
        //        throw new Exception("Invalid password");
        //    }
        //    var claims = new[]
        //    {
        //      new Claim(ClaimTypes.Name, user.UserName)
        //      //new Claim(ClaimTypes.Role,user.Role)
        //    };

        //    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( _configuration["Jwt:Key"]!));
        //    var creds = new SigningCredentials(key,SecurityAlgorithms.HmacSha256);

        //    var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"],

        //            audience: _configuration["Jwt:Audience"],
        //            claims:claims,
        //            expires: DateTime.Now.AddHours(5),
        //            signingCredentials:creds);

        //    return new JwtSecurityTokenHandler().WriteToken(token);
        //}

        public async Task<string> RegisterAsync( RegisterDto dto)
        {
            try
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
                    PhoneNumber = dto.PhoneNumber,
                    AgreeToTerms= dto.AgreeToTerms,
                    IsActive = true,
                    CreatedDate = DateTime.UtcNow
                };


                await _repository.AddUserAsync(user);
                return "User Registered Successfully";
            }
            catch (Exception ex)
            {
                throw ex;
            }
            
        }
        public async Task<string> AssignRoleAsync(AssignRoleDto dto)
        {
            await _repository.AssignRoleAsync(dto.UserId,dto.RoleId);
            return "Role Assigned Successfully";
        }


        private string GenerateRefreshToken()
        {
            return Guid.NewGuid().ToString();
        }
        public async Task<AuthResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
        {
            var user = await _repository.GetByRefreshTokenAsync(dto.RefreshToken);

            if (user == null)
            {
                throw new Exception("Invalid refresh token");
            }

            if (user.RefreshTokenExpiryTime < DateTime.UtcNow)
            {
                throw new Exception("Refresh token expired");
            }

            var role = await _repository.GetUserRoleAsync(user.Id);

            var claims = new[]
            {
              new Claim( ClaimTypes.Name, user.UserName),
              new Claim( ClaimTypes.Role,role ?? "User")
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]!));

            var creds =new SigningCredentials( key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                    issuer: _configuration["Jwt:Issuer"],
                    audience:_configuration["Jwt:Audience"],
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(30),
                    signingCredentials:
                    creds);

            var newRefreshToken = Guid.NewGuid().ToString();
            user.RefreshToken = newRefreshToken;
            user.RefreshTokenExpiryTime =DateTime.UtcNow.AddDays(7);
            await _repository.UpdateUserAsync(user);
            return new AuthResponseDto
            {
                AccessToken = new JwtSecurityTokenHandler().WriteToken(token),
                RefreshToken =newRefreshToken
            };
        }
    }
}
