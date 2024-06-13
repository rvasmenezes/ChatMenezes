namespace ChatMenezes.Domain.Aggregates.StockAgg.Dtos
{
    public class FindStockDto
    {
        public int RoomId { get; set; }
        public string StockCode { get; set; }

        public FindStockDto(int roomId, string stockCode)
        {
            RoomId = roomId;
            StockCode = stockCode;
        }
    }
}
