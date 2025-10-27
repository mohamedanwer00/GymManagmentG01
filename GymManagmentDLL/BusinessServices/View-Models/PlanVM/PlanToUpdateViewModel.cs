namespace GymManagmentBLL.BusinessServices.View_Models
{
    internal class PlanToUpdateViewModel
    {
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Description is required.")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "Description must be between 5 and 50 characters.")]
        public string Description { get; set; } = null!;

        [Required(ErrorMessage = "Duration in days is required.")]
        [Range(1, 365, ErrorMessage = "Duration must be between 1 and 365 days.")]
        public int DurationDays { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(200, 10000, ErrorMessage = "Price must be between 200 and 10000.")]
        public decimal Price { get; set; }
    }
}
