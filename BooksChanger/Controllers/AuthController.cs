using BusinessLogic.ModelsDTO.UserDTO;
using BusinessLogic.Services.AuthService;
using BusinessLogic.Services.UserService;
using Database.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Linq;

namespace BooksChanger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthService _authService;
        private readonly IConfiguration _configuration;


        public AuthController(IUserService userService, IAuthService authService, IConfiguration configuration)
        {
            _userService = userService;
            _authService = authService;
            _configuration = configuration;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register(RegisterDTO request)
        {
            var user = await _userService.CreateUser(request);
            var token = _authService.CreateToken(user); 
            return Ok(new { token, userId = user.Id });
        }

        //[HttpPost("register-admin")]
        //public async Task<ActionResult<User>> RegisterAdmin(RegisterDTO request, string adminPassword)
        //{
        //    if (adminPassword != _configuration["AdminRegistrationPassword"])
        //    {
        //        return Unauthorized();
        //    }

        //    var user = await _userService.CreateUser(request, 2);

        //    return Ok(user);
        //}

        [HttpPost("login")]
        public async Task<ActionResult<UserLoginResponseDTO>> Login(LoginDTO request)
        {
            var loginResponse = await _authService.Login(request);

            if (loginResponse == null)
            {
                return BadRequest("Wrong username or password");
            }

            var response = new UserLoginResponseDTO
            {
                Token = loginResponse.Token,
                UserId = loginResponse.UserId
            };

            return Ok(response);
        }


    }
}
