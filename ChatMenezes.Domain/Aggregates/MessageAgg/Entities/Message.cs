using ChatMenezes.Domain.Aggregates.RoomAgg.Entities;
using ChatMenezes.Domain.Aggregates.UserAgg.Entities;
using System.ComponentModel.DataAnnotations;

namespace ChatMenezes.Domain.Aggregates.MessageAgg.Entities
{
    public class Message
    {
        [Key]
        [StringLength(255)]
        public string Id { get; set; }

        [StringLength(255)]
        public string UserId { get; set; }
        public User? User { get; set; }
        public int RoomId { get; set; }
        public Room? Room { get; set; }
        
        [StringLength(2000)]
        public string MessageText { get; set; }
        public DateTime RegiterDate { get; set; }

        public Message(string userId, int roomId, string messageText)
        {
            Id = Guid.NewGuid().ToString();
            UserId = userId;
            RoomId = roomId;
            MessageText = messageText;
            RegiterDate = DateTime.Now.ToUniversalTime();
        }
    }
}
