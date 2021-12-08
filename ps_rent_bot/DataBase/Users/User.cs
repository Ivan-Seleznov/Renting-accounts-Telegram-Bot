using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_rent_bot.DataBase.Users
{
    class User: IDb
    {
        public enum userStatus
        {
            None = 0,
            Premium_Standart
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public long ChatId { get; set; }
        public userStatus Status{ get; set; }
        public DateTime DateAdded { get; set; }
        //и list заказов
    }
}
