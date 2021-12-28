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
        public bool IsPsPlus {get; set ; }   
        public bool IsRented { get ; set ; }
        public int Id { get ; set ; }
        public DateTime DateAdded { get ; set ; }
        

        public bool Rent(User user)
        {
            if (this!=null)
            {
                try
                {
                    Program.Db.psAccounts.Find(this.Id).IsRented = true;
                    Program.Db.SaveChanges();
                }
                catch (Exception exc)
                {
                    Console.Error.WriteLine(exc.ToString());
                    return false;
                }
                try
                {
                Orders.Order order = new Orders.Order { 
                    Account = Program.Db.psAccounts.Find(this.Id),
                    User = Program.Db.Users.Find(user.UserId) 
                };
                Program.Db.Orders.Add(order);
                Program.Db.SaveChanges(); 
                }
                catch (Exception exc)
                {
                    Program.Db.psAccounts.Find(this.Id).IsRented = false;
                    Console.Error.WriteLine("Ошибка сохранение заказа. " + this.Id + "Стастус аккаунта откачен"+exc.ToString());
                    Console.WriteLine("Ошибка сохранение заказа. " +this.Id+ "Стастус аккаунта откачен");
                    return false;
                }
                return true; 
                
                

            }
            else return false;
                
        }
        public void GiveAccountInformation()
        {
            string msg ="Имя: "+ this.Username +"\nПароль: "+ this.Password;
            Console.WriteLine(msg);
        }

    }
}
