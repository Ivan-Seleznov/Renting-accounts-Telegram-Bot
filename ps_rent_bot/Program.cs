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
            //telegramBot.StartBot();
            using (ApplicationContext db = new ApplicationContext())
            {
                db.Add<PsAccount>(new PsAccount { Email="1", Password="11", Username="111"});
                db.Add<User>(new User { Name = "11", ChatId = 1 });
                db.SaveChanges();
                ////for (int i = 0; i < 1000; i++)
                ////{
                ////   db.Add<Order>(new Order { Account = db.psAccounts.Find(1), User = db.Users.Find(1) });
                ////    db.SaveChanges();

                ////}
                db.Add<Order>(new Order { Account = db.psAccounts.Find(1), User = db.Users.Find(1) });
                db.SaveChanges();
            }
        }

    }
}
