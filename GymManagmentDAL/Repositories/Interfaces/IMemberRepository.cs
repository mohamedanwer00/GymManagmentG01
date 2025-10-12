using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization.Metadata;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    internal interface IMemberRepository
    {
        //GetAllMembers
        IEnumerable<Member> GetAllMembers();

        //GetMemberById
        Member? GetMemberById(int id);

        //AddMember
        int Add(Member member);
        //UpdateMember
        int Update(Member member);
        //DeleteMember
        int Remove(int id);
    }
}
