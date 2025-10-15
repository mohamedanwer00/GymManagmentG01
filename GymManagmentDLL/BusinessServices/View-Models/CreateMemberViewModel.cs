using GymManagmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.BusinessServices.View_Models
{
    internal class CreateMemberViewModel
    {
        [Required(ErrorMessage ="Name is Required")]
        [StringLength(50,MinimumLength =2,ErrorMessage ="Name Must be between 2 And 50 .")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Email is Required")]

        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        [DataType(DataType.EmailAddress)]//hint for the view
        [StringLength(100,MinimumLength =5, ErrorMessage = "Email must be between 5 And 100.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "Phone is Required")]
        [Phone(ErrorMessage = "Invalid Phone Number")]
        [RegularExpression(@"^(010|011|015|012)\d{8}$")]
        public string Phone { get; set; } = null!;

        [Required(ErrorMessage = "Date Of Barth is Required")]
        [DataType(DataType.Date)]//hint for the view
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public Gender Gender { get; set; }

        //Address
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

        [Required(ErrorMessage = "Health Record is Required")]
        public HealthRecordViewModel HealthRecord { get; set; } = null!;


    }
}
