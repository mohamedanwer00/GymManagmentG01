namespace GymManagmentBLL.BusinessServices.View_Models
{
    internal class HealthRecordViewModel
    {
        [Required(ErrorMessage = "Height is required.")]
        [Range(1, 300, ErrorMessage = "Height must be between 1 and 300 cm.")]
        public decimal Height { get; set; }
        [Required(ErrorMessage = "Weight is required.")]
        [Range(1, 350, ErrorMessage = "Weight must be between 1 and 500 kg.")]
        public decimal Weight { get; set; }

        [Required(ErrorMessage = "blood is required.")]
        [StringLength(3, ErrorMessage = "Blood Conditions cannot exceed 3 characters.")]
        public string BloodTybe { get; set; } = null!;
        public string? Note { get; set; }
    }
}
