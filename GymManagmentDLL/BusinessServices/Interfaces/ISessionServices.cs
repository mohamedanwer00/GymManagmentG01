using GymManagementSystemBLL.View_Models.SessionVm;

namespace GymManagmentBLL.BusinessServices.Interfaces
{
    internal interface ISessionServices
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionDetails (int sessionId); 
        bool CreateSession(CreateSessionViewModel createSession);

        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);
        bool UpdateSession(int sessionId,UpdateSessionViewModel updateSession);

        bool DeleteSession(int sessionId);
    }
}
