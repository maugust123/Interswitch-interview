using InterviewApi.BusinessEntities.Models.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InterviewApi.BusinessEntities.Models.ModelConfig
{
    public class CorporateTypeMap : IEntityTypeConfiguration<CorporateType>
    {
        public void Configure(EntityTypeBuilder<CorporateType> builder)
        {
            builder.Property(p => p.Name).IsRequired();
            builder.HasQueryFilter(p => p.IsActive);
        }
    }

    public class UserCorporateTypeMap : IEntityTypeConfiguration<UserCorporateType>
    {
        public void Configure(EntityTypeBuilder<UserCorporateType> builder)
        {
            builder.Property(p => p.UserId).IsRequired();
            builder.Property(p => p.CorporateTypeId).IsRequired();
            builder.HasQueryFilter(p => p.IsActive);

            builder.HasOne(p => p.User).WithMany().HasForeignKey(f => f.UserId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(p => p.CorporateType)
                .WithMany(p => p.UserCorporateTypes)
                .HasForeignKey(f => f.CorporateTypeId).IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
