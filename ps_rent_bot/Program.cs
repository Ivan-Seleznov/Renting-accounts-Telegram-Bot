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
            TelegramBot telegramBot = new TelegramBot(@"BotInfo\tocken.txt", @"BotInfo\description.txt", true);
            

        }
        
    }
}
