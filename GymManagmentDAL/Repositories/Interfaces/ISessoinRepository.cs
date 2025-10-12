using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface ISessoinRepository
    {
        //GetAllSessions
        IEnumerable<Session> GetAllSessions();
        //GetSessionById
        Session? GetSessionById(int id);
        //AddSession
        int Add(Session session);
        //UpdateSession
        int Update(Session session);
        //DeleteSession
        int Remove(int id);
    }
}
