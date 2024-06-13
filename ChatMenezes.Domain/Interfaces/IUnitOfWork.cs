using ChatMenezes.Domain.Aggregates.MessageAgg.Entities;
using ChatMenezes.Domain.Aggregates.RoomAgg.Entities;
using ChatMenezes.Domain.Aggregates.UserAgg.Entities;

namespace ChatMenezes.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        IBaseEFRepository<User> UsuarioRepository { get; }
        IBaseEFRepository<Room> RoomRepository { get; }
        IBaseEFRepository<Message> MessageRepository { get; }

        Task<int> Commit();
        void Rollback();
    }
}
