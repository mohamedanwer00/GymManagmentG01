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
    public class MembershipRepository :GenericRepository<Membership>, IMembershipRepository
    {
        private readonly GymDbContext _context;

        public MembershipRepository(GymDbContext context) : base(context)
        {
            _context = context;
        }
        public IEnumerable<Membership> GetAllMembershipsWithMembersAndPlans(Func<Membership, bool>? filter = null)
        {
            var memberships = _context.Memberships.Include(m => m.Member).Include(m => m.Plan)
                .Where(filter ?? (_ => true));

            return memberships;
        }

        public Membership? GetFirstOrDefult(Func<Membership, bool>? filter = null)
        {
            var membership = _context.Memberships.FirstOrDefault(filter ?? (_ => true));
            return membership;
        }
    }
}
