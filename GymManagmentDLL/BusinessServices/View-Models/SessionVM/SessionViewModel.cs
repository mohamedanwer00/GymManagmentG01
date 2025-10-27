namespace GymManagmentBLL.BusinessServices.View_Models
{
    internal class SessionViewModel
    {
        public int Id { get; set; }
        public string CategoryName { get; set; } = null!;
        public string Description { get; set; } = null!;
        public string TrainerName { get; set; } = null!;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Capacity { get; set; }
        public int AvailableSlots { get; set; }


        #region Computed Prop // Not Mapped/ Stored in Db//للعرضها فقط
        public string DateDisplay => $"{StartDate:MMM dd,yyyy}";
        public string TimeRangeDisplay => $"{StartDate:hh:mm tt}-{EndDate:hh:mm:tt}";
        public TimeSpan Duration => EndDate - StartDate;
        public string Status
        {
            get
            {
                if (StartDate > DateTime.Now)
                    return "Uncoming";
                else if (StartDate <= DateTime.Now && EndDate >= DateTime.Now)
                    return "Ongoing";
                else
                    return "Completed";
            }
        }

        #endregion
    }
}
