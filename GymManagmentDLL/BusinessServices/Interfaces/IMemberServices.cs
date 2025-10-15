using GymManagmentBLL.BusinessServices.View_Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.BusinessServices.Interfaces
{
    internal interface IMemberServices
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel createMember);

    }
}
