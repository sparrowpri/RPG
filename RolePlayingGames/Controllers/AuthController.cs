using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using RolePlayingGames.Data;
using RolePlayingGames.DTO.Users;
using RolePlayingGames.Models;

namespace RolePlayingGames.Controllers
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
        [HttpPost("Register")]
        public async Task<ActionResult<ServiceResponse<int>>> RegisterUser(UserRegistrationDTO userRegister)
        {
            var response =await _authRepository.Register(new User(){ UserName=userRegister.UserName },userRegister.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<ServiceResponse<string>>> LoginUser(UserLoginDTO user)
        {
            var response = await _authRepository.Login(user.UserName, user.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
