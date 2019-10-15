using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATINGAPP.API.Data;
using Microsoft.AspNetCore.Mvc;
using DATINGAPP.API.Models;
using System.Web.Http;
using DATINGAPP.API.DTO;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace DATINGAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        private readonly IConfiguration _config;
        public AuthController(IAuthRepository authRepository,IConfiguration config)
        {
            _authRepository = authRepository;
            _config = config;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody]UserForRegisterDto dto)
        {
            //validate request
            try
            {
                dto.Username = dto.Username.ToLower();
                var Result = await _authRepository.UserExists(dto.Username);
                if (Result)
                    return BadRequest("Username already exists");

                var userToCreate = new User()
                {
                    Username = dto.Username
                };

                var createdUser = await _authRepository.Register(userToCreate, dto.Password);
            }
            catch(Exception ex)
            {

            }

            return StatusCode(201);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody]UserForRegisterDto dto)
        {
            var userFromRepo = await _authRepository.Login(dto.Username.ToLower(), dto.Password);
            if (userFromRepo == null)
                return Unauthorized();

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,userFromRepo.Id.ToString()),
                new Claim(ClaimTypes.Name,userFromRepo.Username)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}