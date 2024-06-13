using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ChatMenezes.Domain.Interfaces
{
    public interface IBaseEFRepository<TEntity> where TEntity : class
    {
        Task<TEntity?> GetEntityByIdAsync(int Id);
        IQueryable<TEntity> GetAll();
        Task<EntityEntry<TEntity>> AddAsync(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity Entity);
        TEntity GetById(int Id);
    }
}
