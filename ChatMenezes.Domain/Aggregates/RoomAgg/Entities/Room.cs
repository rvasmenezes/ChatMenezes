using System.ComponentModel.DataAnnotations;

namespace ChatMenezes.Domain.Aggregates.RoomAgg.Entities
{
    public class Room
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }

        public Room(string name) => Name = name;
    }
}
