using Microsoft.EntityFrameworkCore;
using ps_rent_bot.DataBase;
using ps_rent_bot.DataBase.Accounts;
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
            //another example of use class TelegramBot
            //TelegramBot telegramBot = new TelegramBot(@"BotInfo\tocken.txt", @"BotInfo\description.txt", true);
            using (Db = new ApplicationContext())
            {

                Account account = new Account { AccountType = Enums.AccountTypeEnum.PS, Email = "SaS@gmail.huy", DateAdded = DateTime.Now, Games = "Suspendit2d", Username = "baza", IsRented = false, Password = "213112dqd", IsPsPlus = false };
                Db.Accounts.Add(account);
                Db.SaveChanges();

                TelegramBot telegramBot = new TelegramBot();
                telegramBot.PathToTockenFile = @"BotInfo\tocken.txt";
                telegramBot.PathToDescriptionFile = @"BotInfo\description.txt";
                telegramBot.PathToHelloMessage = @"BotInfo\hello.txt";
                telegramBot.LoggingInConsole = true;
                telegramBot.InitializationException = false;
                telegramBot.BotName = "Бот по аренде игр на playstation4 \\ ps5";
                telegramBot.BotUsername = "ps_rent_bot";
                telegramBot.button = new BotButton();
                telegramBot.StartBot();
            }        
        }

    }
}
