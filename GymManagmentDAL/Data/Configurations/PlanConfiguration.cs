using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class PlanConfiguration : IEntityTypeConfiguration<PlanReposatory>
    {
        public void Configure(EntityTypeBuilder<PlanReposatory> builder)
        {
            builder.Property(p => p.Name)
                .HasColumnType("nvarchar(50)");
            builder.Property(p => p.Description)
                .HasColumnType("nvarchar(200)");
            builder.Property(p => p.Price)
                .HasPrecision(10, 2);
            builder.ToTable(Tb=> Tb.HasCheckConstraint("DurationDaysConstraint",
                "DurationDays between 1 and 365 "));
        }
    }
}
