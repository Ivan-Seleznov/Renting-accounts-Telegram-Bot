using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ps_rent_bot
{
    class TelegramBot
    {
        public TelegramBotClient client { get; set; }
        public TelegramBot()
        {
           
        }
        public string BotName { get; private set; }
        public string BotUsername { get; private set; }
        public string BotDescription { get; private set; }
        public string BotTocken { get; private set; }
        public string PathToTockenFile { get; set; }
        public string PathToDescriptionFile { get; set; }

        private void StartBot()
        {
            client.StartReceiving();
            Console.WriteLine($"Бот: {BotName} .\nBot Username: {BotUsername}\n\tЗапущен.");
            
            client.StopReceiving();
        }
        private void Inizialize(string PathToTockenFile, string PathToDescriptionFile)
        {
            Console.WriteLine("Инициализация...");
            BotName = "Бот по аренде игр на playstation4 \\ ps5";
            BotUsername = "ps_rent_bot";

            try
            {
                if (PathToTockenFile != null)
                {
                    BotTocken = File.ReadAllText(PathToTockenFile);
                    Console.WriteLine("Токен инициализирован");

                }
                else
                {
                    throw new Exception(message: "В PathToTockenFile null");
                }
            }
            catch (Exception ex) { Console.WriteLine("Ошибка инициализации токена.\n" + ex.Message); }
            try
            {
                if (PathToDescriptionFile != null)
                {
                    BotDescription = File.ReadAllText(PathToDescriptionFile);
                    Console.WriteLine("Описание инициализировано");

                }
                else
                {
                    throw new Exception(message: "В PathToDescriptionFile null");
                }
            }
            catch (Exception ex) { Console.WriteLine("Ошибка инициализации описания бота.\n" + ex.Message); }
        }
        private void ConsoleComands()
        {
            while (true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    default:
                        Console.WriteLine("Введите команду. Для просмотра всего списка комманд используйте /help");
                        break;
                }
            }
        }
    }
}
