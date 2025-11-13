
using GymManagmentBLL.BusinessServices.View_Models.MembershipVM;

namespace GymManagmentBLL.BusinessServices.Interfaces
{
    public interface IMembershipService
    {
        IEnumerable<MembershipViewModel> GetAllMemberships();
        IEnumerable<PlanSelectListViewModel> GetPlansForDropDown();
        IEnumerable<MemberSelectListViewModel> GetMembersForDropDown();
        bool CreateMembership(CreateMembershipViewModel model);
        bool DeleteMembership(int MemberId);
    }
}
