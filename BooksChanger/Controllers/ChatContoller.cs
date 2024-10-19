using BusinessLogic.ModelsDTO.ChatDTO;
using BusinessLogic.Services.ChatService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooksChanger.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chatService)
        {
            _chatService = chatService;
        }

        [HttpGet("{chatId}")]
        public async Task<IActionResult> GetChat(int chatId)
        {
            var chat = await _chatService.GetChatByIdAsync(chatId);
            if (chat == null)
            {
                return NotFound();
            }   
            return Ok(chat);
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateChat([FromBody] CreateChatDTO request)
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return Unauthorized("Invalid user ID.");
            }

            if (request.UserTwoId == 0)
            {
                return BadRequest("Invalid userTwoId.");
            }

            var chat = await _chatService.CreateChatAsync(userId, request.UserTwoId);
            if (chat == null)
            {
                return BadRequest("Unable to create chat.");
            }
            return Ok(chat);
        }



        [HttpGet("mychats")]
        public async Task<IActionResult> GetUserChats() 
        {
            if (!int.TryParse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value, out int userId))
            {
                return Unauthorized("Invalid user ID.");
            }

            var chats = await _chatService.GetUserChatsAsync(userId);
            return Ok(chats);
        }
    }
}
