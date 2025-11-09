

namespace GymManagmentBLL.BusinessServices.View_Models
{
    public class MemberToUpdateViewModel : BaseMemberViewModel
    {
        public string Name { get; set; } = null!;
        public string? Photo { get; set; }
    }
}
