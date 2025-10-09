using GymManagmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    internal class Trainer: GymUser
    {
        //HireDate==CeartedAt
        public Specialties Specialties { get; set; }

    }
}
