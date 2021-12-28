using Microsoft.EntityFrameworkCore;
using ps_rent_bot.DataBase;
using ps_rent_bot.DataBase.Accounts.Playstation;
using ps_rent_bot.DataBase.Orders;
using ps_rent_bot.DataBase.Users;
using Telegram.Bot;
namespace ps_rent_bot
{
    class Program
    {
        public static ApplicationContext Db { get; set; }
        static void Main()
        {

            //TelegramBot telegramBot = new TelegramBot(@"BotInfo\tocken.txt", @"BotInfo\description.txt", true);
            //using (Db = new ApplicationContext())
            //{


            //    TelegramBot telegramBot = new TelegramBot();
            //    telegramBot.PathToTockenFile = @"BotInfo\tocken.txt";
            //    telegramBot.PathToDescriptionFile = @"BotInfo\description.txt";
            //    telegramBot.PathToHelloMessage = @"BotInfo\hello.txt";
            //    telegramBot.LoggingInConsole = true;
            //    telegramBot.InitializationException = false;
            //    telegramBot.BotName = "Бот по аренде игр на playstation4 \\ ps5";
            //    telegramBot.BotUsername = "ps_rent_bot";
            //    telegramBot.button = new BotButton();
            //    telegramBot.StartBot();
            //}

            PsAccount psAccount = new PsAccount { Email = "test", Password = "1111", Username = "testname" };
            using (Db = new ApplicationContext())
            {
                Db.Database.EnsureDeleted();
                Db.Database.EnsureCreated();
                Db.Users.Add(new User { Name = "test", UserId = 1 });
                Db.Users.Add(new User { Name = "test2", UserId = 2 });
                Db.psAccounts.Add(psAccount);
                Db.SaveChanges();
                Console.WriteLine("cmplyt adding");

                //System.Threading.Thread.Sleep(1000);

                psAccount = Db.psAccounts.Find(1);
                psAccount.Rent(Db.Users.FirstOrDefault());
                psAccount.Rent(Db.Users.FirstOrDefault());
                psAccount.Rent(Db.Users.FirstOrDefault());
                psAccount.Rent(Db.Users.Find((long)2));
                psAccount.Rent(Db.Users.FirstOrDefault());
                Db.SaveChanges();
                var orders = Db.Orders.Include(u => u.User).ToList();
                foreach (var item in orders)
                {
                    Console.WriteLine(item.Id+""+item.User.Name);
                }
                Console.WriteLine("//////////////////////////////////////////////////////////////");
                orders = Db.Users.FirstOrDefault().GetOrders();
                foreach (var item in orders)
                {
                    Console.WriteLine(item.Id + "" + item.User.Name);
                }
            }
            try
            {
                throw new Exception();
            }
            catch (Exception exc)
            {
                Console.Error.WriteLine(exc.ToString());
            }
           

        }

    }
}
