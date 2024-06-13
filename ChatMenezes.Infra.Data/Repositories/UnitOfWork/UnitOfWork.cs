using ChatMenezes.Domain.Aggregates.MessageAgg.Entities;
using ChatMenezes.Domain.Aggregates.RoomAgg.Entities;
using ChatMenezes.Domain.Aggregates.UserAgg.Entities;
using ChatMenezes.Domain.Interfaces;
using ChatMenezes.Infra.Data.Context;
using ChatMenezes.Infra.Data.Repositories.EntityFramework;

namespace ChatMenezes.Infra.Data.Repositories.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        public EFContext _context { get; }

        private BaseEFRepository<User>? _usuarioRepository;
        private BaseEFRepository<Room>? _roomRepository;
        private BaseEFRepository<Message>? _messageRepository;
        public UnitOfWork(EFContext context) => _context = context;

        public IBaseEFRepository<User> UsuarioRepository
            => _usuarioRepository ??= new BaseEFRepository<User>(_context);

        public IBaseEFRepository<Room> RoomRepository
            => _roomRepository ??= new BaseEFRepository<Room>(_context);

        public IBaseEFRepository<Message> MessageRepository
            => _messageRepository ??= new BaseEFRepository<Message>(_context);

        public Task<int> Commit() => _context.SaveChangesAsync();

        public void Rollback() { }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}