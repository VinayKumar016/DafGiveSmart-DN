using CDTApi.DTOs;
using CDTApi.Models;
using CDTApi.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IdentityModel.Tokens.Jwt; 
using System.Security.Claims; 
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;
using CDTApi.Services; 
namespace CDTApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly IUserRepository _userRepo;
        private readonly IDAFRepository _dafRepo;
        private readonly EmailService _emailService;
        private readonly JWTService _jwtService;

        public UsersController(IUserRepository userRepo, IDAFRepository dafRepo, EmailService emailService, JWTService jwtService)
        {
            _userRepo = userRepo;
            _dafRepo = dafRepo;
            _emailService = emailService;
            _jwtService = jwtService;
        }
        


        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterUserDTO dto)
        {
            if (_userRepo.GetByEmail(dto.Email) is not null)
                return BadRequest("Email already exists.");

            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                Name = dto.Name,
                Email = dto.Email,
                PasswordHash = hashedPassword,
                RegistrationSource = dto.RegistrationSource,
                Mobile = dto.Mobile,
                Location = dto.Location
            };

            _userRepo.AddUser(user); // UserId generated inside

            // If the registration is for a DAF account, create the DAF record
            if (dto.RegistrationSource == "DAF")
            {
                int newId = _dafRepo.GetByUserId(user.UserId) == null
                    ? _userRepo.GetAllUsers().Count() + 1
                    : _dafRepo.GetByUserId(user.UserId)!.DAFAccountId;

                string accountNumber = $"DAF-{newId:D3}";

                var dafAccount = new DAFAccount
                {
                    DAFAccountId = newId,
                    UserId = user.UserId,
                    AccountNumber = accountNumber,
                    DAFBalance = 0,
                    TotalDonated = 0
                };

                // After adding User + DAF account successfully
                _emailService.SendEmailAsync(
                    user.Email,
                    "Welcome to Donor Advisor Fund!",
                    $"<h3>Hi {user.Name},</h3><p>Thank you for registering with us.</p><p>Your DAF Account Number is <b>{accountNumber}</b>.</p><p>Happy Giving!</p>"
                );

                _dafRepo.AddDAFAccount(dafAccount);

                return Ok(new
                {
                    message = "DAF user registered successfully",
                    dafAccountNumber = accountNumber,
                    userId = user.UserId
                });
            }

            return Ok(new { message = "User registered successfully", userId = user.UserId });
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDTO dto)
        {
            var user = _userRepo.GetByEmail(dto.Email);

            if (user == null || !BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash))
                return Unauthorized("Invalid email or password.");

            user.Token = _jwtService.CreateJWTToken(user); // Create JWT token

            return Ok(new
            {
                token = user.Token,
                message = "Login successful",
                userId = user.UserId,
                name = user.Name,
                registrationSource = user.RegistrationSource
            });
        }

        


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            return Ok(_userRepo.GetAllUsers());
        }
        
    }
}
