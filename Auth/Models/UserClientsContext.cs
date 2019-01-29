using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Models
{
    public class UserClientsContext : DbContext
    {
        public DbSet<UserClients> userClients { get; set; }

        public UserClientsContext(DbContextOptions<UserClientsContext> options) :
            base(options)
        {
            Database.EnsureCreated();
        }
    }
}
