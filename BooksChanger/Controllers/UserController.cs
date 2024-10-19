using BusinessLogic.Services.UserService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using BusinessLogic.ModelsDTO.UserDTO;

namespace BooksChanger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            this._userService = userService;
        }
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var user = await _userService.UserGetById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }       

        [HttpPut("update")]
        public async Task<IActionResult> UpdateUser([FromBody] AddUserInfoDTO updateUserDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized("User ID is missing from the token claims.");                                                                                                                                                                                       
            }                                                             
                                                                                                                                                                                                                                                                                            
            if (!int.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("Invalid user ID claim.");
            }

            var result = await _userService.UpdateUser(userId, updateUserDTO);
            return result ? Ok() : BadRequest("Can't update user.");
        }
        [HttpPost("changePassword")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDTO changePasswordDTO)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (userIdClaim == null)
            {
                return Unauthorized("User ID is missing from the token claims.");
            }

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return BadRequest("Invalid user ID claim.");
            }

            var result = await _userService.ChangePassword(userId, changePasswordDTO.CurrentPassword, changePasswordDTO.NewPassword);
            if (result)
            {
                return Ok("Password changed successfully.");
            }
            else
            {
                return BadRequest("Failed to change password.");
            }
        }
        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var result = await _userService.DeleteUser(id);
            if (result)
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

    }
}
