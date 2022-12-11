using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ps_rent_bot.DataBase.Orders
{
    public class OrderManager
    {
        public static List<Order> GetOrders(long userId, bool IsRent)
        {
            List<Order> orders = new List<Order>();
            int i = 0;
            foreach (var item in Program.Db.Orders)
            {
                if (item.User.UserId == userId && item.Account.IsRented == IsRent)
                {
                    orders.Add(item);
                    Console.WriteLine(item.Account.Games);
                }
            }
            Program.Db.SaveChanges();
            return orders;
        }
        public static string GetAllOrdersInMessage(long userId)
        {
           
            string message = "Список ваших заказов:\n";
            List<Order> activeOrders = GetOrders(userId, true);
            List<Order> noactiveOrders = GetOrders(userId, false);
            
            if (activeOrders.Count > 0)
            {
                message += "Активные:\n";
                foreach (var item in activeOrders)
                {
                    message += $"Арендован аккаунт с {item.Account.Games} | Логин: {item.Account.Email} | Дата аренды аккаунта: {item.DateAdded}\n";
                }
            }
            else
            {
                message += "Активный заказов у вас нету\n";
            }
            if (noactiveOrders.Count > 0)
            {
                message += "Не активные:\n";
                foreach (var item in noactiveOrders)
                {
                    message += $"Не активный аккаунт: {item.Account.Games} | Логин: {item.Account.Email} | Дата оформления заказа: {item.DateAdded}\n";

                }
            }
            else
            {
                message += "Вы не сделали не одного заказа\n";
            }
            return message;
        }
    }
}
