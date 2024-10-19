using Microsoft.AspNetCore.SignalR;

namespace BooksChanger.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessageToGroup(string chatId, string user, string message, string messageId)
        {
            await Clients.Group(chatId).SendAsync("ReceiveMessage", user, message, messageId);
        }

        public async Task AddToGroup(string chatId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
        }

        public async Task RemoveFromGroup(string chatId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, chatId);
        }
    }
}
