using Auth.DAO.Model;
using Auth.Models;
using Microsoft.EntityFrameworkCore;
namespace Auth.DAO
{
    public class ContextAuth : DbContext
    { 
        public DbSet<User> Users { get; set; }
        public DbSet<UserClients> UserClients { get; set; }
        public DbSet<UserClientsEmployee> UserClientsEmployee { get; set; }
        public DbSet<Admin> Admin{ get; set; }

        public ContextAuth()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=(localdb)\\mssqllocaldb;Database=Auth;Trusted_Connection=True;");
            }
        }
    }
}
