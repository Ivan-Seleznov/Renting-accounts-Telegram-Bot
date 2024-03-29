﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ps_rent_bot.Enums;
using static ps_rent_bot.DataBase.Users.User;

namespace ps_rent_bot.DataBase
{
    class ApplicationContext:DbContext
    {   //public DbSet<Accounts.Playstation.PsAccount> psAccounts { get; set; }
        public DbSet<Accounts.Account> Accounts { get; set; }
        public DbSet<Users.User> Users { get; set; }
        public DbSet<Orders.Order> Orders { get; set; }
        public ApplicationContext()
        { 
            Database.EnsureCreated();
            
        }
        protected override void OnConfiguring(DbContextOptionsBuilder contextOptionsBuilder)
        {
            contextOptionsBuilder.UseSqlServer("Server=localhost\\SQLExpress;Database=ps_rent_botadb;Trusted_Connection=True;");
        }
       

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Users.User>().Property(u => u.Status).HasDefaultValue(UserStatusEnum.None);
            modelBuilder.Entity<Orders.Order>().Property(u => u.DateAdded).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Users.User>().Property(u => u.DateAdded).HasDefaultValueSql("GETDATE()");
            //modelBuilder.Entity<Accounts.Playstation.PsAccount>().Property(u => u.DateAdded).HasDefaultValueSql("GETDATE()");
            modelBuilder.Entity<Accounts.Account>().Property(u => u.DateAdded).HasDefaultValueSql("GETDATE()");
        }
    }
}
