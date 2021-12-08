using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_rent_bot.DataBase.Accounts.Playstation
{
    
    internal class PsAccount : IAccount
    {
        [Required]
        public string Username { get; set ; }
        [Required]
        public string Email { get ; set ; }
        [Required]
        public string Password { get ; set ; }
        public bool IsPsPlus { get; set ; }   
        public bool IsRented { get ; set ; }
        public int Id { get ; set ; }
        public DateTime DateAdded { get ; set ; }
    }
}
