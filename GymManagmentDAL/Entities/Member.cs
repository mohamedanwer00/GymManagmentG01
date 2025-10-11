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

        #region Relationships

        #region Member has Health Record
        public HealthRecord HealthRecord { get; set; }

        #endregion

        #region Member has Membership

        public ICollection<Membership> Memberships { get; set; }

        #endregion

        #region Member has many Sessions

        public ICollection<Session> Sessions { get; set; }
         
        #endregion
        #endregion

    }
}
