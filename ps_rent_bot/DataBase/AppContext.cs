using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ps_rent_bot.DataBase
{
    class AppContext:DbContext
    {
        public AppContext()
        {
            Database.EnsureCreated();
        }
        protected void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
        {
            contextOptionsBuilder.UseSqlServer("Server=localhost\\SQLExpress;Database=ps_rent_botadb;Trusted_Connection=True;");
        }
    }
}
