using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.BusinessServices.View_Models
{
    internal class MemberViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public string? photo { get; set; }
        public string? Photo { get; internal set; }
        public string Email { get; set; } = null!;
        public string Gender { get; set; } = null!;

    }
}
