

namespace GymManagmentBLL.BusinessServices.View_Models
{
    public class BaseMemberViewModel
    {
        [Required(ErrorMessage = "Email is Required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]//hint for the view
        [StringLength(100, MinimumLength = 5, ErrorMessage = "Email must be between 5 And 100.")]
        public string Email { get; set; } = null!;
        [Required(ErrorMessage = "Phone is Required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"^(010|011|015|012)\d{8}$", ErrorMessage = "Must Be valid Egyption Phone Number")]
        public string Phone { get; set; } = null!;
        [Required(ErrorMessage = "Building Number is Required")]
        [Range(1, int.MaxValue, ErrorMessage = "Building Number must be greater than 0.")]
        public int BuildingNumber { get; set; }

        [Required(ErrorMessage = "Street is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "Street must be between 2 And 100.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Street can only contain letters and spaces.")]

        public string Street { get; set; } = null!;


        [Required(ErrorMessage = "City is Required")]
        [StringLength(30, MinimumLength = 2, ErrorMessage = "City must be between 2 And 100.")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "City can only contain letters and spaces.")]

        public string City { get; set; } = null!;

    }
}
