using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.Model;
using Auth.Infrastructure.Configuration;

namespace Auth.Infrastructure.Data
{
    public class WebAppContext : DbContext
    {
        public WebAppContext(DbContextOptions<WebAppContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new EndPointConfiguration());
            modelBuilder.ApplyConfiguration(new RoleEndPointConfiguration());
            modelBuilder.ApplyConfiguration(new ApplicationConfiguration());
            modelBuilder.ApplyConfiguration(new TokenConfiguration());
           
        }
        public DbSet<User> User { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Application> Application { get; set; }
        public DbSet<EndPoint> EndPoint { get; set; }
        public DbSet<Token> Token { get; set; }
        public DbSet<RoleEndPoint> RoleEndPoints { get; set; }

    }
}
