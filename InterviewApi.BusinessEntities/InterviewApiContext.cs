using InterviewApi.BusinessEntities.Models.Model;
using InterviewApi.BusinessEntities.Models.ModelConfig;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InterviewApi.BusinessEntities
{

    public class InterviewApiContext : IdentityDbContext<User, Role, string>
    {
        public InterviewApiContext(DbContextOptions<InterviewApiContext> options) : base(options)
        {
            //var kls = new DbContextOptionsBuilder(options).EnableSensitiveDataLogging();
        }

        public virtual DbSet<Bank> Banks { get; set; }
        public virtual DbSet<BankBranch> BankBranches { get; set; }
        public virtual DbSet<UserBranch> UserBranches { get; set; }
        public virtual DbSet<CorporateType> CorporateTypes { get; set; }
        public virtual DbSet<UserCorporateType> UserCorporateTypes { get; set; }
        public virtual DbSet<Institution> Institutions { get; set; }
        public virtual DbSet<UserType> UserTypes { get; set; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.ApplyConfigurationsFromAssembly(typeof(UserMap).Assembly); // Here UseConfiguration is any IEntityTypeConfiguration
        }
    }
}
