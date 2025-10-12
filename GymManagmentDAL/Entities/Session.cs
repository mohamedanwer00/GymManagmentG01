using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    internal class Session : BaseEntity
    {
        public string Discription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }

        #region Relationships
        #region Session belongs to Category
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        #endregion

        #region Session-Trainer
        public int TrainerId { get; set; }
        public Trainer Trainer { get; set; }
        #endregion

        #region Session has many Members
        public ICollection<MemberSession> MemberSessions { get; set; }
        #endregion


        #endregion


    }
}
