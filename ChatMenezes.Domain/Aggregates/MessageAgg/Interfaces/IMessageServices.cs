using ChatMenezes.Domain.Aggregates.MessageAgg.Entities;

namespace ChatMenezes.Domain.Aggregates.MessageAgg.Interfaces
{
    public interface IMessageServices
    {
        Task<List<Message>> GetList(int roomId);
        Task<Message> Add(int roomId, string messageText, bool validateStockCode = true);
    }
}

