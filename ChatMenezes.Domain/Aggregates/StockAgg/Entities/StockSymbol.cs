using System.Text.Json.Serialization;

namespace ChatMenezes.Domain.Aggregates.StockAgg.Entities
{
    public class StockSymbol
    {
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; }
        
        [JsonPropertyName("date")]
        public string Date { get; set; }

        [JsonPropertyName("time")]
        public string Time { get; set; }

        [JsonPropertyName("open")]
        public decimal Open { get; set; }

        [JsonPropertyName("high")]
        public decimal High { get; set; }

        [JsonPropertyName("low")]
        public decimal Low { get; set; }

        [JsonPropertyName("close")]
        public decimal Close { get; set; }

        [JsonPropertyName("volume")]
        public long Volume { get; set; }
    }

    public class StockData
    {
        [JsonPropertyName("symbols")]
        public List<StockSymbol> Symbols { get; set; }
    }
}
