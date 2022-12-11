using ps_rent_bot.DataBase.Users;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ps_rent_bot.Enums;

namespace ps_rent_bot.DataBase.Accounts
{
    public class Account
    {

        [Required]
        public string Username { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public AccountTypeEnum AccountType { get; set; }
        public bool? IsPsPlus { protected private get; set; }
        public bool IsRented { get; set; }
        public int Id { get; set; }
        public DateTime RentedFor;
        public DateTime DateAdded { get; set; }
        public string Games { get; set; }

        public bool Rent(User user, int months)
        {
            if (this != null && this.IsRented != true)
            {
                try
                {
                    Program.Db.Accounts.Find(this.Id).IsRented = true;
                    Program.Db.Accounts.Find(this.Id).RentedFor = DateTime.Now.AddMonths(months);
                }
                catch (Exception exc)
                {
                    Console.Error.WriteLine(exc.ToString());
                    return false;
                }
                try
                {
                    Orders.Order order = new Orders.Order
                    {
                        Account = Program.Db.Accounts.Find(this.Id),
                        User = Program.Db.Users.Find(user.UserId)
                    };
                    Program.Db.Orders.Add(order);
                    Program.Db.SaveChanges();
                    
                }
                catch (Exception exc)
                {
                    Program.Db.Accounts.Find(this.Id).IsRented = false;
                    Console.Error.WriteLine("Ошибка сохранение заказа. " + this.Id + "Стастус аккаунта откачен" + exc.ToString());
                    Console.WriteLine("Ошибка сохранение заказа. " + this.Id + "Стастус аккаунта откачен");
                    return false;
                }
                return true;
            }
            else return false;

        }
        public void GetInfo()
        {
            string msg = "Имя: " + this.Username + "\nПароль: " + this.Password;
            Console.WriteLine(msg);
        }
        
    }
}
