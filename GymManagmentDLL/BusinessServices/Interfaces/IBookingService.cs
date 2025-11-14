using GymManagmentBLL.BusinessServices.View_Models.BookingVM;
using GymManagmentBLL.BusinessServices.View_Models.MembershipVM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.BusinessServices.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<SessionViewModel> GetAllSessionsWithTrainerAndCategory();
        IEnumerable<MemberForSessionViewModel> GetAllMembersForUpcomingSession(int id);
        IEnumerable<MemberForSessionViewModel> GetAllMembersForOngoingSession(int id);
        bool CreateBooking(CreateBookingViewModel createBookingViewModel);
        IEnumerable<MemberSelectListViewModel> GetMembersForDropdown(int id);
        bool MemberAttended(MemberAttendOrCancelViewModel model);
        bool CancelBooking(MemberAttendOrCancelViewModel model);
    }
}
