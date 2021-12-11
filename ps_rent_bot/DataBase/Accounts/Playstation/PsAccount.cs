using ps_rent_bot.DataBase.Users;
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

        public void Rent(User user)
        {
            
            using (ApplicationContext db=new ApplicationContext())
            {
                db.psAccounts.Find(this.Id).IsRented = true;
                    db.SaveChanges();
                Orders.Order order = new Orders.Order { 
                    Account =db.psAccounts.Find(this.Id),
                    User =db.Users.Find(user.Id) };
                
                db.Orders.Add(order);
                db.SaveChanges();
                //user.Orders.Add(db.Orders.Find(order.Id));
                //db.SaveChanges();

            }
            GiveAccountInformation();
        }
        public void GiveAccountInformation()
        {
            string msg = this.Username +"\n"+ this.Password;
            Console.WriteLine(msg);
        }

    }
}
