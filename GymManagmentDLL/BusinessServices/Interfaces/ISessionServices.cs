using GymManagementSystemBLL.View_Models.SessionVm;
using GymManagmentBLL.BusinessServices.View_Models.SessionVM;

namespace GymManagmentBLL.BusinessServices.Interfaces
{
    public interface ISessionServices
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionDetails (int sessionId); 
        bool CreateSession(CreateSessionViewModel createSession);

        UpdateSessionViewModel? GetSessionToUpdate(int sessionId);
        bool UpdateSession(int sessionId,UpdateSessionViewModel updateSession);

        bool DeleteSession(int sessionId);

        IEnumerable<TrainerSelectViewModel> GetTrainersForDropdown();
        IEnumerable<CategorySelectViewModel> GetCategoriesForDropdown();
    }
}
