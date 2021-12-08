using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_rent_bot.DataBase
{
    interface IDb
    {
        public int Id { get; set; }
        public DateTime DateAdded { get; set; }

    }
}
