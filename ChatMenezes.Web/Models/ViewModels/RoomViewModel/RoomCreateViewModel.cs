using System.ComponentModel.DataAnnotations;

namespace ChatMenezes.Web.Models.ViewModels.RoomViewModel
{
    public class RoomCreateViewModel
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
    }
}
