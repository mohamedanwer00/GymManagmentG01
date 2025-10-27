namespace GymManagmentDAL.Entities
{
    public class Membership : BaseEntity
    {
        public Member Member { get; set; }

        public int MemberId { get; set; }
        public int PlanId { get; set; }
        public Plan Plan { get; set; }
        public DateTime EndDate { get; set; }

        //Derived Property
        public string Status
        {
            get
            {
                if (EndDate <= DateTime.Now)
                    return "Expired";
                else
                    return "Active";
            }
        }

    }
}
