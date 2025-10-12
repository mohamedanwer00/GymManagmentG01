﻿using GymManagmentDAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Data.Configurations
{
    internal class MemberSessionConfiguration : IEntityTypeConfiguration<MemberSession>
    {
        public void Configure(EntityTypeBuilder<MemberSession> builder)
        {
            builder.Property(X=>X.CreatedAt)
                .HasDefaultValueSql("GETDATE()")
                .HasColumnName("BookingDate");

            builder.HasKey(X=> new { X.MemberId, X.SessionId });

            builder.Ignore(X => X.Id);

        }
    }
}
