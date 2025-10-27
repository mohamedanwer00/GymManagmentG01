namespace GymManagmentDAL.Entities
{
    public class Member : GymUser
    {
        //JoinDate==CreatedAt
        public string? Photo { get; set; }

        #region Relationships

        #region Member has Health Record
        public HealthRecord HealthRecord { get; set; } = null!;

        #endregion

        #region Member has Membership

        public ICollection<Membership> Memberships { get; set; }

        #endregion

        #region Member has many Sessions

        public ICollection<MemberSession> MemberSessions { get; set; } = [];

        #endregion
        #endregion

    }
}
