using GymManagmentDAL.Entities.Enums;

namespace GymManagmentDAL.Entities
{
    public class Trainer : GymUser
    {
        //HireDate==CeartedAt
        public Specialties Specialties { get; set; }

        #region Relationships
        #region Trainer-Sessions
        public ICollection<Session> Sessions { get; set; }
        #endregion
        #endregion

    }
}
