using Microsoft.EntityFrameworkCore;
using System;
using ManagementSystem.Models;
using ManagementSystem.Models.ViewModels;

namespace ManagementSystem.Data
{
    public class ManagementSystemDbContext : DbContext
    {
        public ManagementSystemDbContext(DbContextOptions<ManagementSystemDbContext> options) : base(options) { }
       
        public DbSet<UserEntity> User { get; set; }
        public DbSet<EmployeeEntity> Employee { get; set; }
        public DbSet<UnityEntity> Unity { get; set; }
        public DbSet<ManagementSystem.Models.ViewModels.EmployeeViewModel> EmployeeViewModel { get; set; } = default!;
    }
}
