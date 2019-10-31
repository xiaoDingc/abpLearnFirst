using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using JD.CRS.Authorization.Roles;
using JD.CRS.Authorization.Users;
using JD.CRS.Entitys;
using JD.CRS.MultiTenancy;

namespace JD.CRS.EntityFrameworkCore
{
    public class CRSDbContext : AbpZeroDbContext<Tenant, Role, User, CRSDbContext>
    {
        /* Define a DbSet for each entity of the application */

        public CRSDbContext(DbContextOptions<CRSDbContext> options)
            : base(options)
        {
        }
        public DbSet<Course> Courses { get; set; }

        public DbSet<EquipmentType> EquipmentTypes { get; set; }

        /// <summary>
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EquipmentType>().ToTable("EquipmentType")
                .HasAlternateKey(x => x.Code).HasName("UK_EquipmentType_Code");
            base.OnModelCreating(modelBuilder);
        }

    }
}
