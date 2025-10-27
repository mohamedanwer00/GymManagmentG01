using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;

namespace GymManagmentDAL.UnitOfWork
{
    public interface IUnitOfWork
    {
        public ISessionRepository SessionRepository { get; }
        IGenericRepository<TEntity> GetRepository<TEntity>()
            where TEntity : BaseEntity, new();

        int SaveChanges();
    }
}
