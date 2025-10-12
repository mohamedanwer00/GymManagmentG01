using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Implementations
{
    internal class SessoinRepository : ISessoinRepository
    {
        private readonly GymDbContext _dbContext = new GymDbContext();
        public int Add(Session session)
        {
            _dbContext.Sessions.Add(session);
            return _dbContext.SaveChanges();
        }

        public IEnumerable<Session> GetAllSessions()=>_dbContext.Sessions.ToList();

        public Session? GetSessionById(int id)=> _dbContext.Sessions.Find (id);


        public int Remove(int id)
        {
            var session = _dbContext.Sessions.Find (id);
            if(session is null) 
                return 0;
            _dbContext.Sessions.Remove(session);
            return _dbContext.SaveChanges();
        }

        public int Update(Session session)
        {
            _dbContext.Sessions.Update(session);
            return _dbContext.SaveChanges();
        }
    }
}
