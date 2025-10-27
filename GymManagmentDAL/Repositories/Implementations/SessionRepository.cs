using GymManagmentDAL.Data.Context;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Implementations
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext _context;

        public SessionRepository(GymDbContext context): base(context)
        {
            _context = context;
        }
        public IEnumerable<Session> GetAllWithCategoryAndTrainer()
        {
            return _context.Sessions
                .Include(X => X.Category)
                .Include(X => X.Trainer)
                .ToList();
        }

        public Session? GetByIdWithTrainerAndCategory(int sessionId)
        {
            return _context.Sessions
                .Include(X => X.Category)
                .Include(X => X.Trainer)
                .FirstOrDefault(X => X.Id == sessionId);
        }

        public int GetCountOfBookedSlots(int sessionId)
        {
            return _context.MemberSessions.Count(X => X.SessionId == sessionId);
        }
    }
}
