﻿using GymManagmentDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    internal abstract class GymUser: BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public Gender Gender { get; set; }

        public Adress Address { get; set; }

    }

    [Owned]
    class Adress 
    { 
        public int BuildingNumber { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
    }
}
