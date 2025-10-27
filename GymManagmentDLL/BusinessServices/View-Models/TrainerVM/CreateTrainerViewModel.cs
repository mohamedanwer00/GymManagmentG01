using GymManagmentDAL.Entities.Enums;

namespace GymManagmentBLL.BusinessServices.View_Models
{
    public class CreateTrainerViewModel
    {
        [Required(ErrorMessage = "Name is required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name must be between 2 and 50 characters")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        [DataType(DataType.EmailAddress)]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 and 100 characters")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone number is required")]
        [Phone(ErrorMessage = "Invalid phone number format")]
        [RegularExpression(@"^(010|011|015|012)\d{8}$", ErrorMessage = "Phone number must start with 010, 011, 012, or 015 and be 11 digits long")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Date Of Birth is required")]
        [DataType(DataType.Date)]
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Building Number is Required")]
        [Range(1, 9000, ErrorMessage = "Building Number must be greater than 0.")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must be between 2 And 100.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Street can only contain letters and spaces.")]
        public string Street { get; set; } = null!;

        [Required(ErrorMessage = "City is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 And 100.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces.")]
        public string City { get; set; } = null!;

        [Required(ErrorMessage = "Specialities is required")]
        [DataType(DataType.Text)]
        public Specialties Specialities { get; set; }


    }
}
