using ps_rent_bot.DataBase.Accounts;
using ps_rent_bot.DataBase.Accounts.Playstation;
using ps_rent_bot.DataBase.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_rent_bot.DataBase.Orders
{
    class Order : IOrder
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public User User { get; set; }
        public int UserId { get; set; }
        public DateTime DateAdded { get ; set ; }
    }
}
