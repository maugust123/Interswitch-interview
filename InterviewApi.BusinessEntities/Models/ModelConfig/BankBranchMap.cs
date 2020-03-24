using System;
using System.Collections.Generic;
using System.Text;
using InterviewApi.BusinessEntities.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewApi.BusinessEntities.Models.ModelConfig
{

    public class BankBranchMap : IEntityTypeConfiguration<BankBranch>
    {
        public void Configure(EntityTypeBuilder<BankBranch> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(m => m.BankId).IsRequired();
            builder.HasQueryFilter(p => p.IsActive);
            builder.HasOne(p => p.Bank).WithMany(p => p.BankBranches).HasForeignKey(f => f.BankId).IsRequired().OnDelete(DeleteBehavior.Cascade);


        }
    }

    public class BankMap : IEntityTypeConfiguration<Bank>
    {
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.Property(m => m.Code).IsRequired().HasMaxLength(6);
            builder.HasIndex(p => new { p.Name, p.Code }).IsUnique();
            builder.HasQueryFilter(p => p.IsActive);
        }
    }

    public class UserBranchMap : IEntityTypeConfiguration<UserBranch>
    {
        public void Configure(EntityTypeBuilder<UserBranch> builder)
        {
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(m => m.BankBranchId).IsRequired();
            builder.HasQueryFilter(p => p.IsActive);

            builder.HasOne(p => p.User).WithMany().HasForeignKey(x => x.UserId).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(p => p.BankBranch).WithMany().HasForeignKey(x => x.BankBranchId).IsRequired().OnDelete(DeleteBehavior.Restrict);
        }
    }
}
