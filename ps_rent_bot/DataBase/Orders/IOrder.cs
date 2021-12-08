using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_rent_bot.DataBase.Orders
{
    interface IOrder:IDb
    {
        public long Number { get; set; }
        public ps_rent_bot.DataBase.Users.User User { get; set; }

    }
}
