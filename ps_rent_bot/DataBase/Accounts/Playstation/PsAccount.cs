using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_rent_bot.DataBase.Accounts.Playstation
{
    internal class PsAccount : IAccount
    {
        public string Username { get; set ; }
        public string Email { get ; set ; }
        public string Password { get ; set ; }
        public bool IsPsPlus { get; set ; }   
        public bool IsRented { get ; set ; }
        public int Id { get ; set ; }
        public DateTime DateAdded { get ; set ; }
    }
}
