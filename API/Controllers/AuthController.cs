using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Interfaces;
using Domain.Entities;
namespace API.Controllers

{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IUserService _userService;

        public AuthController(ILogger<AuthController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<UserResponse> CreateUser(UserRequest userRequest)
        {
            try
            {
                var response = await _userService.CreateUser(userRequest);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserRequest user)
        {
            bool loginSuccess = await _userService.Login(user);
            if (!loginSuccess){return Unauthorized("Usuario o contraseña incorrectos");}
            return Ok("Usuario logeado satisfactoriamente");
        }

    }
}
