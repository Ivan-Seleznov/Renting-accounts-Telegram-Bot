using ps_rent_bot.DataBase.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ps_rent_bot.Enums;
using ps_rent_bot;
using Microsoft.EntityFrameworkCore;

namespace ps_rent_bot.DataBase.Users
{
    class User
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long UserId { get; set; }
        public string Name { get; set; }
        public UserStatusEnum Status{ get; set; }
        public DateTime DateAdded { get; set; }
        //и list заказов
        public List<Order> Orders { get; set; } = new List<Order>(); //добавил
        public List<Order> GetOrders()
        {
            
            var orders=Program.Db.Orders.Include(u=> u.User).ToList();
            List<Order> ordersList = new List<Order>();
            foreach (var order in orders)
            {
                if (order.User==this)
                {
                    ordersList.Add(order);
                }
            }
            return ordersList;
        }

    }
}
