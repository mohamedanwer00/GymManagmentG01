using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Implementations
{
    internal class TrainerRepository : ITrainerRepository
    {
        private readonly GymDbContext _dbContext = new GymDbContext();
        public int Add(Trainer trainer)
        {
            _dbContext.Trainers.Add(trainer);
            return _dbContext.SaveChanges();

        }

        public IEnumerable<Trainer> GetAllTrainers()=>_dbContext.Trainers.ToList();


        public Trainer? GetTrainerById(int id)=> _dbContext.Trainers.Find(id);


        public int Remove(int id)
        {
            var trainer = _dbContext.Trainers.Find(id);
            if (trainer == null)
                return 0;
            _dbContext.Trainers.Remove(trainer);
            return _dbContext.SaveChanges();

        }

        public int Update(Trainer trainer)
        {
            _dbContext.Trainers.Update(trainer);
            return _dbContext.SaveChanges();
        }
    }
}
