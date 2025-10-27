using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Implementations;
using GymManagmentDAL.Repositories.Interfaces;

namespace GymManagmentDAL.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDbContext _dbContext;


        public UnitOfWork(GymDbContext dbContixt, ISessionRepository sessionRepository)
        {
            _dbContext = dbContixt;
            SessionRepository = sessionRepository;
        }

        public ISessionRepository SessionRepository { get; }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType, out var repository))
                return (IGenericRepository<TEntity>)repository;

            var newRepository = new GenericRepository<TEntity>(_dbContext);

            _repositories[entityType] = newRepository;

            return newRepository;



        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
