using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ps_rent_bot
{
    class TelegramBot
    {
        public TelegramBotClient client { get; set; }
        public TelegramBot(string PathToTockenFile, string PathToDescriptionFile)
        {
            this.PathToDescriptionFile = PathToDescriptionFile;
            this.PathToTockenFile = PathToTockenFile;
            StartBot();
        }
        public string BotName { get; private set; }
        public string BotUsername { get; private set; }
        public string BotDescription { get; private set; }
        public string BotTocken { get; private set; }
        public string PathToTockenFile { get; set; }
        public string PathToDescriptionFile { get; set; }

        private void StartBot()
        {
            Inizialize();
            client = new TelegramBotClient(BotTocken);
            client.OnMessage += OnMessageHandler;
            client.StartReceiving();
            Console.WriteLine($"Бот: {BotName} .\nBot Username: {BotUsername}\n\tЗапущен.");
            ConsoleComands();
            client.StopReceiving();
            Console.WriteLine("Бот остановлен");
            Console.ReadLine();
        }

        private void OnMessageHandler(object? sender, MessageEventArgs e)
        {
            var message = e.Message;
            if (e.Message.Text == "/start")
            {
                try { client.SendTextMessageAsync(message.Chat.Id, BotDescription); } catch(Exception ex) { Console.WriteLine("Ошибка отправки сообщения |" + ex.Message); }
            }
        }

        private void Inizialize()
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
                    case "sendMessage":
                        Console.Write("Введите текстовое сообщение:");
                        string text = Console.ReadLine();
                        SendMessage(text);
                            break;
                    case "/help":
                            Console.WriteLine("sendMessage - отправить текстовое сообщение всем пользователям");
                        break;
                    default:
                        Console.WriteLine("Введите команду. Для просмотра всего списка комманд используйте /help");
                        break;
                }
            }
        }
        public void SendMessage(string message)
        {

        }
    }
}
