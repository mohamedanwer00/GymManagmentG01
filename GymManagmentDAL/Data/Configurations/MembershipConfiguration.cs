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
    internal class MembershipConfiguration : IEntityTypeConfiguration<Membership>
    {
        public void Configure(EntityTypeBuilder<Membership> builder)
        {
            builder.Property(X=>X.CreatedAt)
                .HasColumnName("StartDate")
                .HasDefaultValueSql("GETDATE()");
            builder.HasKey(X => new { X.MemberId, X.PlanId });

            builder.Ignore(X => X.Status);
            builder.Ignore(X => X.Id);
        }
    }
}
