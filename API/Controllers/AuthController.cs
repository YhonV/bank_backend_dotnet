using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using Application.Interfaces;
namespace API.Controllers

{
    [Route("api/hello")]
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

        [HttpPost]
        public async Task<UserResponse> CreateUser(UserRequest userRequest)
        {
            try
            {
                var response = await _userService.createUser(userRequest);
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating user");
                throw;
            }
            
        }

    }
}
