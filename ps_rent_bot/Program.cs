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
               
        static void Main()
        {

            //TelegramBot telegramBot = new TelegramBot(@"BotInfo\tocken.txt", @"BotInfo\description.txt", true);

            //TelegramBot telegramBot = new TelegramBot();
            //telegramBot.PathToTockenFile = @"BotInfo\tocken.txt";
            //telegramBot.PathToDescriptionFile = @"BotInfo\description.txt";
            //telegramBot.PathToHelloMessage = @"BotInfo\hello.txt";
            //telegramBot.LoggingInConsole = true;
            //telegramBot.InitializationException = false;
            //telegramBot.BotName = "Бот по аренде игр на playstation4 \\ ps5";
            //telegramBot.BotUsername = "ps_rent_bot";
            //telegramBot.button = new BotButton();
            //telegramBot.StartBot();

            PsAccount psAccount = new PsAccount { Email="test", Password="1111", Username="testname"};
            using (ApplicationContext db = new ApplicationContext())
            {
               // db.Database.EnsureDeleted();
               // db.Database.EnsureCreated();
                db.Users.Add(new User { Name = "test", ChatId = 0 });
                db.psAccounts.Add(psAccount);
                db.SaveChanges();
                Console.WriteLine("cmplyt adding");
           
            //System.Threading.Thread.Sleep(1000);
           
                psAccount = db.psAccounts.FirstOrDefault();
                psAccount.Rent(db.Users.FirstOrDefault());
                psAccount.Rent(db.Users.FirstOrDefault());
                psAccount.Rent(db.Users.FirstOrDefault());
                psAccount.Rent(db.Users.FirstOrDefault());
                psAccount.Rent(db.Users.FirstOrDefault());
                //db.SaveChanges();
                var orders=db.Orders.Include(u=>u.User).ToList();
                foreach (var item in orders)
                {
                    Console.WriteLine(item.Id);
                }
            }
           
        }

    }
}
