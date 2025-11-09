using GymManagmentDAL.Entities.Enums;

namespace GymManagmentBLL.BusinessServices.View_Models
{
    public class CreateMemberViewModel : BaseMemberViewModel
    {
        [Required(ErrorMessage = "Name is Required")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Name Must be between 2 And 50 .")]
        [RegularExpression(@"^[a-zA-Z\s]+$", ErrorMessage = "Name can only contain letters and spaces.")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "Date Of Barth is Required")]
        [DataType(DataType.Date)]//hint for the view
        public DateOnly DateOfBirth { get; set; }

        [Required(ErrorMessage = "Gender is Required")]
        public Gender Gender { get; set; }

        [Required(ErrorMessage = "Health Record is Required")]
        public HealthRecordViewModel HealthRecordViewModel { get; set; } = null!;
    }
}
