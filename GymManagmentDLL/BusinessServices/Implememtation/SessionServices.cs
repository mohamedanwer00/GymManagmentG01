using AutoMapper;

namespace GymManagmentBLL.BusinessServices.Implememtation
{
    internal class SessionServices : ISessionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionServices(IUnitOfWork unitOfWork,IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessionRepo = _unitOfWork.SessionRepository;
            var sessions = sessionRepo.GetAllWithCategoryAndTrainer();

            if (!sessions.Any())
                return [];

            var mappedSessions=_mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);

            foreach (var session in mappedSessions)
            {
                session.AvailableSlots = session.Capacity - sessionRepo.GetCountOfBookedSlots(session.Id);
            }

            return mappedSessions;

            #region Manual Mapping

            //return sessions.Select(session => new SessionViewModel
            //{
            //    Id = session.Id,
            //    Description = session.Description,
            //    StartDate = session.StartDate,
            //    EndDate = session.EndDate,
            //    Capacity = session.Capacity,
            //    TrainerName = session.Trainer.Name,
            //    CategoryName = session.Category.CategoryName,

            //    AvailableSlots = session.Capacity - sessionRepo.GetCountOfBookedSlots(session.Id),

            //}); 
            #endregion 
        }

        public SessionViewModel? GetSessionDetails(int sessionId)
        {
            var sessionRepo = _unitOfWork.SessionRepository;

            var session = sessionRepo.GetByIdWithTrainerAndCategory(sessionId);
            if (session == null) return null;

            var mappedSession = _mapper.Map<Session, SessionViewModel>(session);

            mappedSession.AvailableSlots = mappedSession.Capacity - sessionRepo.GetCountOfBookedSlots(session.Id);

            return mappedSession;

            #region Manual Mapping
            //return new SessionViewModel
            //{
            //    Id = session.Id,
            //    Description = session.Description,
            //    StartDate = session.StartDate,
            //    EndDate = session.EndDate,
            //    Capacity = session.Capacity,
            //    TrainerName = session.Trainer.Name,
            //    CategoryName = session.Category.CategoryName,

            //    AvailableSlots = session.Capacity - sessionRepo.GetCountOfBookedSlots(session.Id),
            //}; 
            #endregion
        }
    }
}
