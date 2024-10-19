using BusinessLogic.ModelsDTO.Message;
using BusinessLogic.Services.MessageService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BooksChanger.Controllers
{

    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class MessageController : ControllerBase
    {
        private readonly IMessageService _messageService;

        public MessageController(IMessageService messageService)
        {
            _messageService = messageService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMessage([FromBody] MessageDTO messageDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (messageDTO.SenderId == 0)
            {
                var senderIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!int.TryParse(senderIdString, out int senderId))
                {
                    return Unauthorized();
                }
                messageDTO.SenderId = senderId;
            }

            var message = await _messageService.CreateMessageAsync(messageDTO);
            if (message == null) return BadRequest();
            return Ok(message);
        }



        [HttpGet("chat/{chatId}")]
        public async Task<IActionResult> GetChatMessages(int chatId)
        {
            var messages = await _messageService.GetChatMessagesAsync(chatId);
            return Ok(messages);
        }
        [HttpPost("read/{chatId}")]
        public async Task<IActionResult> MarkMessagesAsRead(int chatId)
        {
            var recipientIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            // Перевірити, чи існує ID користувача
            if (string.IsNullOrEmpty(recipientIdString) || !int.TryParse(recipientIdString, out int recipientId))
            {
                return Unauthorized(); // Якщо не вдалося отримати ID користувача, повернути статус неавторизованого доступу
            }
            return Ok();
        }

    }
}
