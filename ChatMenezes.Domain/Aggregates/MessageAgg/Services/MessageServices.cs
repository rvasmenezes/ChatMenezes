using ChatMenezes.Core.ApplicationContext;
using ChatMenezes.Core.RabbitMQ;
using ChatMenezes.Domain.Aggregates.MessageAgg.Entities;
using ChatMenezes.Domain.Aggregates.MessageAgg.Interfaces;
using ChatMenezes.Domain.Aggregates.StockAgg.Dtos;
using ChatMenezes.Domain.Common.Validators;
using ChatMenezes.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ChatMenezes.Domain.Aggregates.MessageAgg.Services
{
    public class MessageServices : IMessageServices
    {
        protected readonly IUnitOfWork _unitOfWork;
        private readonly IChatWebContext _chatWebContext;
        private readonly RabbitMQServices _rabbitMQService;

        public MessageServices(
            IUnitOfWork unitOfWork, 
            IChatWebContext chatWebContext,
            RabbitMQServices rabbitMQService)
        {
            _unitOfWork = unitOfWork;
            _chatWebContext = chatWebContext;
            _rabbitMQService = rabbitMQService;
        }

        public async Task<Message> Add(int roomId, string messageText, bool validateStockCode = true)
        {
            var userId = validateStockCode ? _chatWebContext.UserLoggedId : ConstantMessages.USER_ID_JOB;

            var message = new Message(userId, roomId, messageText);

            await _unitOfWork.MessageRepository.AddAsync(message);
            await _unitOfWork.Commit();

            if (validateStockCode && messageText.Contains("/stock="))
            {
                string stockValue = GetStockValue(messageText);

                if(!string.IsNullOrEmpty(stockValue))
                    SendToQueue(new FindStockDto(roomId, stockValue));
            }
            
            return message;
        }

        public async Task<List<Message>> GetList(int roomId)
            => await _unitOfWork.MessageRepository.GetAll()
                .Include(x => x.User)
                .Where(x => x.RoomId == roomId)
                .OrderByDescending(x => x.RegiterDate)
                .Take(50).ToListAsync();

        private void SendToQueue(FindStockDto findStockDto)
        {
            var message = JsonSerializer.Serialize(findStockDto);
            _rabbitMQService.SendMessage("find_stock_code", message);
        }

        private static string GetStockValue(string input)
        {
            int stockIndex = input.IndexOf("/stock=");
            if (stockIndex == -1)
                return string.Empty;

            int startIndex = stockIndex + "/stock=".Length;

            int endIndex = input.IndexOf(' ', startIndex);
            if (endIndex == -1)
                endIndex = input.Length;

            string stockValue = input.Substring(startIndex, endIndex - startIndex);
            return stockValue;
        }
    }
}
