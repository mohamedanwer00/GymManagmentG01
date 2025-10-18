using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Implementations;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.UnitOfWork
{
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly Dictionary<Type, object> _repositories = new();
        private readonly GymDbContixt _dbContixt;


        public UnitOfWork(GymDbContixt dbContixt)
        {
            _dbContixt = dbContixt;
        }



        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : BaseEntity, new()
        {
            var entityType = typeof(TEntity);

            if (_repositories.TryGetValue(entityType,out var repository))
                return (IGenericRepository<TEntity>)repository;

            var newRepository = new GenericRepository<TEntity>(_dbContixt);

            _repositories[entityType] = newRepository;

            return newRepository;

            

        }

        public int SaveChanges()
        {
            return _dbContixt.SaveChanges();
        }
    }
}
