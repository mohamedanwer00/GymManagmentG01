namespace GymManagmentBLL.BusinessServices.Interfaces
{
    internal interface ISessionServices
    {
        IEnumerable<SessionViewModel> GetAllSessions();
        SessionViewModel? GetSessionDetails (int sessionId); 
    }
}
