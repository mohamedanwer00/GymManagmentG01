using AutoMapper;
using GymManagementSystemBLL.View_Models.SessionVm;

namespace GymManagmentBLL.BusinessServices.Implememtation
{
    internal class SessionServices : ISessionServices
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public SessionServices(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel createSession)
        {
            try
            {
                if (!IsTrainerExists(createSession.TrainerId) || !IsCategoryExists(createSession.CategoryId))
                    return false;


                if (!IsDateTimeValid(createSession.StartDate, createSession.EndDate))
                    return false;

                if (createSession.Capacity > 25 || createSession.Capacity < 0)
                    return false;


                var sessionToCreate = _mapper.Map<CreateSessionViewModel, Session>(createSession);
                _unitOfWork.GetRepository<Session>().Add(sessionToCreate);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }

        public IEnumerable<SessionViewModel> GetAllSessions()
        {
            var sessionRepo = _unitOfWork.SessionRepository;
            var sessions = sessionRepo.GetAllWithCategoryAndTrainer();

            if (!sessions.Any())
                return [];

            var mappedSessions = _mapper.Map<IEnumerable<Session>, IEnumerable<SessionViewModel>>(sessions);

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



        #region helper method
        private bool IsTrainerExists(int trainerTd)
        {
            return _unitOfWork.GetRepository<Trainer>().GetById(trainerTd) is not null;
        }

        private bool IsCategoryExists(int categoryId)
        {
            return _unitOfWork.GetRepository<Category>().GetById(categoryId) is not null;
        }

        private bool IsDateTimeValid(DateTime startDate, DateTime endDate)
        {
            return startDate < endDate;
        }

        private bool IsSessionAvailableForUpdate(Session session)
        {
            if (session == null)
                return false;

            if (session.EndDate < DateTime.Now)
                return false;

            if (session.StartDate < DateTime.Now)
                return false;

            var hasActiveBookings = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (hasActiveBookings)
                return false;
            return true;
        }


        private bool IsSessionAvailableForRemoved(Session session)
        {
            if (session == null)
                return false;



            if (session.StartDate < DateTime.Now && session.EndDate > DateTime.Now)
                return false;

            if (session.StartDate > DateTime.Now)
                return false;

            var hasActiveBookings = _unitOfWork.SessionRepository.GetCountOfBookedSlots(session.Id) > 0;
            if (hasActiveBookings)
                return false;
            return true;
        }

        #endregion

        #region update
        public UpdateSessionViewModel? GetSessionToUpdate(int sessionId)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(sessionId);
            if (!IsSessionAvailableForUpdate(session!))
                return null;

            return _mapper.Map<UpdateSessionViewModel>(session);
        }

        public bool UpdateSession(int sessionId, UpdateSessionViewModel updateSession)
        {

            try
            {
                var sessionRepo = _unitOfWork.GetRepository<Session>();
                var session = sessionRepo.GetById(sessionId);


                if (!IsSessionAvailableForUpdate(session!))
                    return false;

                if (!IsTrainerExists(updateSession.TrainerId))
                    return false;
                if (!IsDateTimeValid(updateSession.StartDate, updateSession.EndDate))
                    return false;

                _mapper.Map(updateSession, session);

                _unitOfWork.GetRepository<Session>().Update(session!);

                session!.UpdatedAt = DateTime.Now;
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }

        }

        #endregion
        public bool DeleteSession(int sessionId)
        {
            try
            {
                var sessionRepo = _unitOfWork.GetRepository<Session>();
                var session = sessionRepo.GetById(sessionId);

                if (!IsSessionAvailableForRemoved(session!))
                    return false;

                _unitOfWork.GetRepository<Session>().Delete(session!);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }
}
