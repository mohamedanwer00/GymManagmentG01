using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Plan: BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string DurationDays { get; set; }
        public bool IsActive { get; set; }

        #region Relationships
        #region Plan has Membership
        public ICollection<Membership> Memberships { get; set; }
        #endregion
        #endregion

    }
}
