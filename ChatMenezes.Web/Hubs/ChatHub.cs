using ChatMenezes.Core.ApplicationContext;
using ChatMenezes.Domain.Aggregates.MessageAgg.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ChatMenezes.Web.Hubs
{
    public class ChatHub : Hub
    {
        private readonly IChatWebContext _chatWebContext;
        private readonly IMessageServices _messageServices;

        public ChatHub(
            IChatWebContext chatWebContext,
            IMessageServices messageServices)
        {
            _chatWebContext = chatWebContext;
            _messageServices = messageServices;
        }

        public async Task SendMessage(string roomId, string messageText)
        {
            try
            {
                var message = await _messageServices.Add(Convert.ToInt32(roomId), messageText);

                await Clients.Group(roomId).SendAsync(
                    "ReceiveMessage",
                    _chatWebContext.UserLoggedName,
                    message.RegiterDate.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    messageText);
            }
            catch (Exception ex)
            {
                await Clients.Group(roomId).SendAsync(
                    "ReceiveMessage",
                    "Error",
                    DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                    ex.Message);
            }
        }

        public async Task JoinRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);

            var messages = await _messageServices.GetList(Convert.ToInt32(roomId));

            foreach (var message in messages.OrderBy(x => x.RegiterDate))
            {
                await Clients.Caller.SendAsync(
                    "ReceiveMessage",
                    message.User.Name,
                    message.RegiterDate.ToString("yyyy-MM-dd HH:mm:ss"),
                    message.MessageText);
            }

            await Clients.Group(roomId).SendAsync(
                "ReceiveMessage",
                _chatWebContext.UserLoggedName,
                DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd HH:mm:ss"),
                $"Has joined the room.");
        }
    }
}
