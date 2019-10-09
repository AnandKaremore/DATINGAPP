using Microsoft.AspNetCore.Mvc;
using DATINGAPP.API.Data;
namespace DATINGAPP.API.Controllers
{
    [Rout("api/[controller]")]
    [ApiController]
    public class AuthController:ControllerBase
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