using AgroERP.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroERP.Persistence.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): base(options)
        {

        }
        public DbSet<Retailer> Retailers => Set<Retailer>();
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Sale> Sales => Set<Sale>();
        public DbSet<PaymentCollection> PaymentCollections => Set<PaymentCollection>();
        public DbSet<Staff> Staffs => Set<Staff>();
        public DbSet<StaffAttendance> StaffAttendances => Set<StaffAttendance>();
        public DbSet<StaffLeave> StaffLeaves=> Set<StaffLeave>();
        public DbSet<AppUser> AppUsers => Set<AppUser>();
        public DbSet<Role> Roles  => Set<Role>();
        public DbSet<UserRole> UserRoles => Set<UserRole>();
        public DbSet<Permission> Permissions => Set<Permission>();
        public DbSet<Module> Modules => Set<Module>();
        public DbSet<RolePermission> RolePermissions => Set<RolePermission>();
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<UserRole>().HasKey(x =>new{x.UserId,x.RoleId});
            modelBuilder.Entity<RolePermission>().HasKey(x =>new{x.RoleId,x.PermissionId,x.ModuleId});
        }
    }
    
}
