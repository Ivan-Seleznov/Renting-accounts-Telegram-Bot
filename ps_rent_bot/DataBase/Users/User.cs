using ps_rent_bot.DataBase.Orders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_rent_bot.DataBase.Users
{
    class User
    {
        public enum userStatus
        {
            None = 0,
            Premium_Standart
        }

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public long ChatId { get; set; }
        public string Name { get; set; }

        public userStatus Status{ get; set; }
        public DateTime DateAdded { get; set; }
        //и list заказов
        public List<Order> Orders { get; set; } = new List<Order>(); //добавил
       
    }
}
