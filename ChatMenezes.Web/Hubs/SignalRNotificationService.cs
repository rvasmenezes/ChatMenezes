using ChatMenezes.Domain.Aggregates.MessageAgg.Interfaces;
using ChatMenezes.Domain.Aggregates.StockAgg.Dtos;
using ChatMenezes.Domain.Aggregates.StockAgg.Entities;
using ChatMenezes.Domain.Aggregates.StockAgg.Interfaces;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace ChatMenezes.Web.Hubs
{
    public class SignalRNotificationService : INotificationService
    {
        private readonly IHubContext<ChatHub> _chatHubContext;
        private readonly IMessageServices _messageServices;

        public SignalRNotificationService(
            IHubContext<ChatHub> chatHubContext,
            IMessageServices messageServices)
        {
            _chatHubContext = chatHubContext;
            _messageServices = messageServices;
        }

        public async Task NotifyAsync(FindStockDto findStockDto)
        {
            var url = $"https://stooq.com/q/l/?s={findStockDto.StockCode}&f=sd2t2ohlcv&h&e=json";
            var stockData = await GetStockDataAsync(url);

            var quoteMessage = $"{findStockDto.StockCode.ToUpper()} quote is not found";

            if (stockData != null && stockData.Symbols.Count > 0 && stockData.Symbols[0].Open > 0)
            {
                var symbol = stockData.Symbols[0];
                quoteMessage = $"{symbol.Symbol} quote is ${symbol.Open} per share";                
            }

            var message = await _messageServices.Add(findStockDto.RoomId, quoteMessage, false);

            await _chatHubContext.Clients.Group(findStockDto.RoomId.ToString()).SendAsync(
                "ReceiveMessage",
                "Job",
                message.RegiterDate.ToUniversalTime().ToString("yyyy-MM-dd HH:mm"),
                message.MessageText);
        }

        public static async Task<StockData> GetStockDataAsync(string url)
        {
            using HttpClient client = new();
            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                return JsonSerializer.Deserialize<StockData>(jsonString.Replace("\\", ""));
            }
            return null;
        }
    }
}
