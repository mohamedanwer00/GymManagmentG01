using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.BusinessServices.Implememtation
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnalyticsService(IUnitOfWork unitOfWork) 
        {
            _unitOfWork = unitOfWork;
        }
        public HomeAnalyticsViewModel GetHomeAnalyticsService()
        {
            var sessions = _unitOfWork.GetRepository<Session>().GetAll();
            return new HomeAnalyticsViewModel()
            {
                TotalMembers = _unitOfWork.GetRepository<Member>().GetAll().Count(),
                TotalTrainers = _unitOfWork.GetRepository<Trainer>().GetAll().Count(),
                ActiveMembers=_unitOfWork.GetRepository<Membership>().GetAll(X=>X.Status=="Active").Count(),
                UpComingSessions=sessions.Count(X=>X.StartDate>DateTime.Now),
                OngoingSessions= sessions.Count(X => X.StartDate <= DateTime.Now&&X.EndDate>=X.EndDate),
                CompletedSessions= sessions.Count(X => X.EndDate < DateTime.Now),
            };
        }
    }
}
