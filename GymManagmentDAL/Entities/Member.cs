using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    internal class Member: GymUser
    {
        //JoinDate==CreatedAt
        public string? Photo { get; set; }

    }
}
