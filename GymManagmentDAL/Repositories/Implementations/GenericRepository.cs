using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GymManagmentDAL.Repositories.Implementations
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        private readonly GymDbContext _dbContext;
        private GymDbContext dbContixt;

        public GenericRepository(GymDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        //public GenericRepository(GymDbContext dbContixt)
        //{
        //    //this.dbContext = dbContixt;
        //}

        public void Add(TEntity entity)
        {
            _dbContext.Set<TEntity>().Add(entity);
        }

        public void Delete(TEntity entity)
        {
            _dbContext.Set<TEntity>().Remove(entity);
        }

        public IEnumerable<TEntity> GetAll(Func<TEntity, bool>? condition = null)
        {
            if (condition is null)
                return _dbContext.Set<TEntity>().AsNoTracking().ToList();
            else
                return _dbContext.Set<TEntity>().AsNoTracking().Where(condition).ToList();
        }

        //public IEnumerable<TEntity> GetAll()=> _dbContext.Set<TEntity>().ToList();

        public TEntity? GetById(int id) => _dbContext.Set<TEntity>().Find(id);


        public void Update(TEntity entity)
        {
            _dbContext.Set<TEntity>().Update(entity);
        }
    }
}
