using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ChatMenezes.Domain.Aggregates.UserAgg.Entities
{
    public class User : IdentityUser
    {
        [StringLength(100, MinimumLength = 2)]
        public string Name { get; set; }

        public User(string email, string name)
        {
            UserName = email;
            Email = email;
            Name = name;
            EmailConfirmed = true;
        }
    }
}
