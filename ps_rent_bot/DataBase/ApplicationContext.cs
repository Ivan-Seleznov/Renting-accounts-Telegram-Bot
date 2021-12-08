using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ps_rent_bot.DataBase
{
    class ApplicationContext:DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }
        protected void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
        {
            contextOptionsBuilder.UseSqlServer("Server=localhost\\SQLExpress;Database=ps_rent_botadb;Trusted_Connection=True;");
        }
        public DbSet<Accounts.Playstation.PsAccount> psAccounts { get; set; }
        public DbSet<Users.User> Users { get; set; }
        public DbSet<Orders.Order> Orders { get; set; }

    }
}
