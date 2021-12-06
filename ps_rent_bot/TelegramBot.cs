using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace ps_rent_bot
{
    class TelegramBot
    {
        public TelegramBotClient client { get; set; }
        public TelegramBot(string PathToTockenFile, string PathToDescriptionFile, bool log)
        {
            this.LoggingInConsole = log;
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
        private bool WaitingForInput { get; set; }
        public bool LoggingInConsole { get; set; }
        public string BotStartMessage { get; set; } = "Приветственное сообщение";

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
            if (message != null)
            {
                if (LoggingInConsole)
                {
                    Console.WriteLine($"Сообщение: {message.Text} от {message?.From.Username} | {message?.From.FirstName}");
                }
                if (!WaitingForInput)
                {
                    switch (e.Message.Text)
                    {
                        default:
                            try { client.SendTextMessageAsync(message.Chat.Id, BotStartMessage,replyMarkup:GetBaseButtons(message.Chat.Id)); } catch (Exception ex) { Console.WriteLine("Ошибка отправки сообщения |" + ex.Message); }
                            break;
                        case "Генерал":
                            client.SendTextMessageAsync(message.Chat.Id, "Какой?", replyMarkup: GetQuestionButtons(message.Chat.Id));
                            WaitingForInput = true;

                            break;
                        case "О боте":
                            try { client.SendTextMessageAsync(message.Chat.Id, BotDescription, replyMarkup: GetBaseButtons(message.Chat.Id)); } catch (Exception ex) { Console.WriteLine("Ошибка отправки сообщения |" + ex.Message); }
                            break;
                    }
                }
                else
                {
                    if (message.Text == "а) Анал" || message.Text == "Анал" || message.Text == "анал")
                    {
                        client.SendTextMessageAsync(message.Chat.Id, "Правильно", replyMarkup: GetBaseButtons(message.Chat.Id)); ;

                    }
                    else
                    {
                        client.SendTextMessageAsync(message.Chat.Id, "Такого генерала не существует. Правильный ответ: Генерал Анал", replyMarkup: GetBaseButtons(message.Chat.Id));

                    }
                    WaitingForInput = false;
                }
            }
        }

        private void Inizialize()
        {
            int i = 0;

            Console.WriteLine("Инициализация...");
            BotName = "Бот по аренде игр на playstation4 \\ ps5";
            BotUsername = "ps_rent_bot";
            if (i < 1)
            {
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
            }
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
            i++;
        }
        private void ConsoleComands()
        {
            while (true)
            {
                string command = Console.ReadLine();
                switch (command)
                {
                    case "/sendMessage":
                        Console.Write("Введите текстовое сообщение: ");
                        string text = Console.ReadLine();
                        SendMessage(text);
                        break;
                    case "/changeDescriptionFile":
                        Console.Write("Укажите путь к файлу: ");
                        PathToDescriptionFile = Console.ReadLine();
                        Inizialize();
                        break;
                    case "/changeDescription":
                        Console.Write("Введите новое описание: ");
                        BotDescription = Console.ReadLine();
                        break;
                    case "/help":
                        Console.WriteLine("sendMessage - отправить текстовое сообщение всем пользователям");
                        Console.WriteLine("/changeDescriptionFile -  Изменить путь к файлу где находится описание бота");
                        Console.WriteLine("/changeDescription -  изменить описание бота");

                        break;
                    case "exit":
                        return;
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
        private IReplyMarkup GetBaseButtons(long chatId)
        {
            //if (!Admin.Contains(chatId))
            //{
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>()
                    {
                    new List<KeyboardButton>
                    {
                        new KeyboardButton()
                        {
                            Text = "О боте"
                        },
   
                    },
                    new List<KeyboardButton>
                    {
                        new KeyboardButton()
                        {
                            Text = "Генерал"
                        },
                        
                    },
                }
            };

            //}
        }
        private IReplyMarkup GetQuestionButtons(long chatId)
        {
           
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>()
                    {
                    new List<KeyboardButton>
                    {
                        new KeyboardButton()
                        {
                            Text = "а) Анал"
                        },

                    },
                    new List<KeyboardButton>
                    {
                        new KeyboardButton()
                        {
                            Text = "б) Седой Генерал"
                        },

                    },
                     new List<KeyboardButton>
                    {
                        new KeyboardButton()
                        {
                            Text = "в) Кал"
                        },

                    },
                }
            };

            
        }
    }
}
