

using GymManagmentDAL.Entities;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface IMembershipRepository:IGenericRepository<Membership>
    {
        IEnumerable<Membership> GetAllMembershipsWithMembersAndPlans(Func<Membership, bool>? filter = null);
        Membership? GetFirstOrDefult(Func<Membership, bool>? filter = null);
    }
}
