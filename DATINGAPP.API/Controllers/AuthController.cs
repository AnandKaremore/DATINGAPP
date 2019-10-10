using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DATINGAPP.API.Data;
using Microsoft.AspNetCore.Mvc;
using DATINGAPP.API.Models;
using System.Web.Http;
namespace DATINGAPP.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository _authRepository;
        public AuthController(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }
        
        public async Task<IActionResult> Register(string username,string password)
        {
            //validate request
            username = username.ToLower();
            if(await _authRepository.UserExists(username))
                return BadRequest("Username already exists");
            
            var userToCreate = new User(){
                Username = username
            };

            var createdUser = await _authRepository.Register(userToCreate,password);

            return StatusCode(201);
        }
    }
}