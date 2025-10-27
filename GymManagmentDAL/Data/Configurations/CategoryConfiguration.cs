using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymManagmentDAL.Data.Configurations
{
    internal class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(X => X.CategoryName)
                .HasColumnType("varchar(20)");


            builder.HasMany(C => C.Sessions)
                .WithOne(S => S.Category)
                .HasForeignKey(S => S.CategoryId);

        }
    }
}
