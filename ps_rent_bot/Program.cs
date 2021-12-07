using Telegram.Bot;
namespace ps_rent_bot
{
    class Program
    {
        //Бот по аренде игр на playstation4 \ ps5
        //ps_rent_bot
        //Tocken: 5057979799:AAFXniv23yLZq-orze4eyQqCidhC9I9TtHY        
        static void Main()
        {
            //TelegramBot telegramBot = new TelegramBot(@"BotInfo\tocken.txt", @"BotInfo\description.txt", true);
            
            TelegramBot telegramBot = new TelegramBot();
            telegramBot.PathToTockenFile = @"BotInfo\tocken.txt";
            telegramBot.PathToDescriptionFile = @"BotInfo\description.txt";
            telegramBot.PathToHelloMessage = @"BotInfo\hello.txt";
            telegramBot.LoggingInConsole = true;
            telegramBot.InitializationException = false;         
            telegramBot.BotName = "Бот по аренде игр на playstation4 \\ ps5";
            telegramBot.BotUsername = "ps_rent_bot";
            telegramBot.StartBot();
        }

    }
}
