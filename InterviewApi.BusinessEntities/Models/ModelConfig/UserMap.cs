using System;
using System.Collections.Generic;
using System.Text;
using InterviewApi.BusinessEntities.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewApi.BusinessEntities.Models.ModelConfig
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.UserTypeId).IsRequired();
            builder.Property(m => m.InstitutionId).IsRequired();
            builder.HasOne(p => p.UserType)
                .WithMany(p => p.Users)
                .HasForeignKey(f => f.UserTypeId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
