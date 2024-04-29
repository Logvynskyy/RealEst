using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealEst.Core.Models;
using RealEst.Core.Models.EnumModels;

namespace RealEst.DataAccess
{
    public class ApplicationContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationContext(DbContextOptions options)
            : base(options)
        {
            
        }

        public DbSet<UnitType> UnitTypes { get; set; } = null!;
        public DbSet<ContactType> ContactTypes { get; set; } = null!;
        public DbSet<DefectType> DefectTypes { get; set; } = null!;
        public DbSet<Organisation> Organisations { get; set; } = null!;
        public DbSet<Tennant> Tennants { get; set; } = null!;
        public DbSet<Defect> Defects { get; set; } = null!;
        public DbSet<Unit> Units { get; set; } = null!;
        public DbSet<Contract> Contracts { get; set; } = null!;
        public DbSet<Contact> Contacts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UnitType>()
                .Property(e => e.Code)
                .HasConversion<string>();

            modelBuilder.Entity<ContactType>()
                .Property(e => e.Code)
                .HasConversion<string>();

            modelBuilder.Entity<DefectType>()
                .Property(e => e.Code)
                .HasConversion<string>();
        }
    }
}
