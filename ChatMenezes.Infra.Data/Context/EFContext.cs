using ChatMenezes.Domain.Aggregates.MessageAgg.Entities;
using ChatMenezes.Domain.Aggregates.RoomAgg.Entities;
using ChatMenezes.Domain.Aggregates.UserAgg.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ChatMenezes.Infra.Data.Context
{
    public class EFContext : IdentityDbContext<User>
    {
        public EFContext(DbContextOptions<EFContext> options) : base(options) { }

        public DbSet<User> User { get; set; }
        public DbSet<Room> Room { get; set; }
        public DbSet<Message> Message { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
