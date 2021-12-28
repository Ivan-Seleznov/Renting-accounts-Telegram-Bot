using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_rent_bot.DataBase.Accounts
{
    interface IAccount: IDb
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsRented { get; set; }

        public bool Rent(Users.User user);
        public void GiveAccountInformation();

    }
}
