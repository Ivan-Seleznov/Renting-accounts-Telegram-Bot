using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using ps_rent_bot.DataBase;
using ps_rent_bot.DataBase.Orders;
using ps_rent_bot.DataBase.Users;
using ps_rent_bot.DataBase.Accounts.Playstation;

namespace ps_rent_bot
{
    class TelegramBot
    {
        public TelegramBotClient client { get; set; }
        public TelegramBot(string PathToTockenFile, string PathToDescriptionFile, string PathToHelloMessage , bool LoggingInConsole = false)
        {
            this.LoggingInConsole = LoggingInConsole;
            this.PathToDescriptionFile = PathToDescriptionFile;
            this.PathToTockenFile = PathToTockenFile;
            this.PathToHelloMessage = PathToHelloMessage;
            StartBot();
        }
        public TelegramBot()
        {
            
        }
        public string BotName { get; set; }
        public string BotUsername { get; set; }
        public string BotDescription { get; set; }
        public string BotTocken { get; private set; }
        public string PathToTockenFile { get; set; }
        public string PathToHelloMessage { get; set; }
        public string PathToDescriptionFile { get; set; }
        private bool WaitingForInput { get; set; }
        public bool LoggingInConsole { get; set; }
        public string HelloMessage { get; set; } = "Приветственное сообщение";
        public bool InitializationException{ get; set; }

        public void StartBot()
        {
            if (Inizialize())
            {
                client = new TelegramBotClient(BotTocken);
                client.OnMessage += OnMessageHandler;
                client.StartReceiving();
                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkMagenta;
                Console.WriteLine($"Бот: {BotName} .\nBot Username: {BotUsername}\n\tзапущен");
                Console.ForegroundColor = ConsoleColor.White;
                ConsoleComands();
                client.StopReceiving();
                Console.WriteLine("Бот остановлен");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Ошибка. Бот остановлен");
                Console.ReadLine();
            }
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
                            try { client.SendTextMessageAsync(message.Chat.Id, HelloMessage,replyMarkup:GetBaseButtons(message.Chat.Id)); } catch (Exception ex) { Console.WriteLine("Ошибка отправки сообщения |" + ex.Message); }
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

        private bool Inizialize()
        {
            Console.ForegroundColor = ConsoleColor.DarkBlue;
            int i = 0;
            Console.WriteLine("Инициализация...");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
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
                catch (Exception ex) 
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Ошибка инициализации токена.\n" + ex.Message);
                    Console.ForegroundColor = ConsoleColor.White;
                    return false;
                    
                }
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
                    if (InitializationException)
                    {
                        throw new Exception(message: "В PathToDescriptionFile null");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine("В PathToDescriptionFile null. Будет задано значение: none");
                        BotDescription = "none";
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                }
            }
            catch (Exception ex) 
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка инициализации описания бота.\n" + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;
                return false;
            }





            try
            {
                if (PathToHelloMessage != null)
                {
                    HelloMessage = File.ReadAllText(PathToHelloMessage);
                    Console.WriteLine("Приветственное сообщение инициализировано");

                }
                else
                {
                    if (InitializationException)
                    {
                        throw new Exception(message: "В PathToHelloMessage null");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;

                        Console.WriteLine("В PathToHelloMessage null. Будет задано значение: hello_none");
                        HelloMessage = "hello_none";
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Ошибка инициализации приветственного сообщения.\n" + ex.Message);
                Console.ForegroundColor = ConsoleColor.White;

                return false;
            }










            Console.ForegroundColor = ConsoleColor.White;
            return true;
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
                        if (!Inizialize())
                        {
                            return;
                        }                        
                        break;
                    case "/changeDescription":
                        Console.Write("Введите новое описание: ");
                        BotDescription = Console.ReadLine();
                        break;
                    case "/Inizialize":
                        Console.WriteLine("");
                        if (!Inizialize())
                        {
                            return;
                        }
                        break;
                    case "/help":
                        Console.WriteLine("sendMessage - отправить текстовое сообщение всем пользователям");
                        Console.WriteLine("/changeDescriptionFile -  Изменить путь к файлу где находится описание бота");
                        Console.WriteLine("/changeDescription -  изменить описание бота");
                        Console.WriteLine("/Inizialize - заново инициализировать значения");

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
