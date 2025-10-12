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
    internal class GymUserConfigurations<T> : IEntityTypeConfiguration<T> where T : GymUser
    {
        public void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property(X => X.Name)
                .HasColumnType("nvarchar(50)");
            builder.Property(X => X.Email)
                .HasColumnType("nvarchar(100)");
            builder.ToTable(Tb => Tb.HasCheckConstraint("EmailValidFormatConstraint",
                "Email LIKE '%_@__%.__%'"));
            builder.HasIndex(X => X.Email).IsUnique();

            builder.Property(X => X.Phone)
                .HasColumnType("nvarchar(11)");
            builder.ToTable(Tb => Tb.HasCheckConstraint("PhoneValidEgpConstraint",
                "Phone LIKE '01[0125]________' AND LEN(Phone) = 11"));

            builder.HasIndex(X => X.Phone).IsUnique();

            builder.OwnsOne(X => X.Address, AddressBuilder =>
            {
                AddressBuilder.Property(A => A.Street)
                .HasColumnType("nvarchar(30)");
                AddressBuilder.Property(A => A.City)
                .HasColumnType("nvarchar(30)");

            });





        }
    }
}
