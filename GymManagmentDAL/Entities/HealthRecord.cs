namespace GymManagmentDAL.Entities
{
    public class HealthRecord : BaseEntity
    {
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string BloodType { get; set; }
        public string? Note { get; set; }
        //LastUpdate == UpdatedAt



    }
}
