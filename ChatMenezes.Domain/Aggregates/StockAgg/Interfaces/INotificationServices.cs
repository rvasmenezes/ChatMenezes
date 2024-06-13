using ChatMenezes.Domain.Aggregates.StockAgg.Dtos;

namespace ChatMenezes.Domain.Aggregates.StockAgg.Interfaces
{
    public interface INotificationService
    {
        Task NotifyAsync(FindStockDto findStockDto);
    }
}

