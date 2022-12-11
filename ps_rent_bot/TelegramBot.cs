using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;
using ps_rent_bot.DataBase.Users;

namespace ps_rent_bot
{
    class TelegramBot
    {

        enum SendMessageResult
        {
            Exxeption = 0,
            Seccesfull = 1,
            DeletedUser = 2
        }
        public BotButton button { get; set; }
        private TelegramBotClient client { get; set; }
        public TelegramBot(string PathToTockenFile, string PathToDescriptionFile, string PathToHelloMessage, BotButton botButton, bool LoggingInConsole = false)
        {
            button = botButton;
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
        public bool InitializationException { get; set; }

        public void StartBot()
        {
            if (Initialize())
            {

                client = new TelegramBotClient(BotTocken);
                client.OnMessage += OnMessageHandler;
                client.OnCallbackQuery += Client_OnCallbackQuery;
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

        private async void Client_OnCallbackQuery(object? sender, CallbackQueryEventArgs e)
        {
            var callback = e.CallbackQuery;
            if (callback != null)
            {
                if (callback.Data != null)
                {
                    switch (callback.Data)
                    {
                        case "ps5bot":
                            break;
                        case "chat":
                            client.AnswerCallbackQueryAsync(callback.Id, "Недоступно");
                            break;
                        case "reviews":
                            client.AnswerCallbackQueryAsync(callback.Id, "Недоступно");
                            break;
                        case "7days":
                            await client.SendTextMessageAsync(callback.From.Id, "Тест");
                            client.EditMessageTextAsync(callback.From.Id, callback.Message.MessageId, "12");

                            break;
                        default:
                            break;
                    }
                    try
                    {
                        if (callback.Data.StartsWith("7days"))
                        {
                            CheckOut(7, "none", callback);

                        }
                        if (callback.Data.StartsWith("14days"))
                        {
                            CheckOut(14, "none", callback);
                        }
                        if (callback.Data.StartsWith("21days"))
                        {
                            CheckOut(21, "none", callback);
                        }
                        if (callback.Data.StartsWith("30days"))
                        {
                            CheckOut(30, "none", callback);

                        }
                        if (callback.Data.StartsWith("60days"))
                        {
                            CheckOut(60, "none", callback);

                        }
                    }
                    catch (Exception ex)
                    {
                        client.EditMessageTextAsync(callback.From.Id, callback.Message.MessageId, "Ошибка аренды аккаунта.");
                    }
                }


            }
        }
        public void CheckOut(int days, string Paylink,Telegram.Bot.Types.CallbackQuery callback)
        {
            try
            {
                string paylink = "none";
                try { client.EditMessageTextAsync(callback.From.Id, callback.Message.MessageId, callback.Message.Text + $"\nКоличество дней: {days}\nСсылка на оплату: {paylink}").Wait(); } catch { Console.WriteLine("Ошибка отправки сообщения"); return; }
                ps_rent_bot.DataBase.Accounts.Account account = Program.Db.Accounts.Find(Convert.ToInt32(callback.Data.Replace(days + "days", "")));
                if (!account.IsRented)
                {
                    account.Rent(Program.Db.Users.Find(callback.From.Id), days);
                    SendMessageToId($"Аккаунт #{account.Id} арендован. Данные об аккаунте\nЛогин: {account.Email}\nПароль: {account.Password}\nПриятной игры В СУСПЕНДИТ", callback.From.Id, "https://imgur.com/a/bEvwkOy");
                }
                else
                {
                    SendMessageToId("Данный аккаунт уже арендован.", callback.From.Id);
                }
            }
            catch (Exception ex) { LogInConsole.PrintException($"Ошибка оформления заказа. {ex.Message}"); }

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
                if (message.Text != null)
                {

                    if (!WaitingForInput)
                    {
                        switch (e.Message.Text)
                        {
                            default:
                                ps_rent_bot.DataBase.Accounts.Account account = ps_rent_bot.DataBase.AccountManager.FindAccount(message.Text);
                                if (account == null)
                                {
                                    SendMessageToId("К сожалению у нас нет аккаунта с такой игрой в данный момент ((. В будующем это поправим, НО У ГНАС ЕСТЬ САСПЕНДИТ. КУПИТЕ ЕГО!", message.Chat.Id, replyMarkup: button.GetBaseButtons(message.Chat.Id));
                                }
                                else
                                {
                                    SendMessageToId($"Аккаунт с {message.Text} найден. Также на нём присутствуют: {account.Games}", message.Chat.Id, replyMarkup: button.GetCallBackRentButtons(account.Id));
                                }
                                break;
                            case "Генерал":
                                SendMessageToId("Какой", message.Chat.Id);
                                WaitingForInput = true;

                                break;
                            case "О боте":
                                SendMessageToId(BotDescription, message.Chat.Id, replyMarkup: button.GetCallBackButtons());
                                break;
                            case "Мои заказы":
                                SendMessageToId(DataBase.Orders.OrderManager.GetAllOrdersInMessage(message.Chat.Id), message.Chat.Id, replyMarkup: button.GetBaseButtons(message.Chat.Id), AddToDb: true, UserName: message.From.FirstName ?? message.From.Username ?? "Аноним");
                                break;
                            case "/start":
                                SendMessageToId(HelloMessage, message.Chat.Id, replyMarkup: button.GetBaseButtons(message.Chat.Id), AddToDb: true, UserName: message.From.FirstName ?? message.From.Username ?? "Аноним", photoUrl: "https://media.discordapp.net/attachments/566347121364828160/1032748768254251088/unknown.png");
                                break;
                            case "ЧТОБЫ СДЕСЬ КОММУНИСТОУ НЕ БЫЛО БЛЯТЬ":
                                SendMessageToId("",message.Chat.Id, "https://media.discordapp.net/attachments/566347121364828160/1032749017572048998/unknown.png");
                                break;

                        }
                    }
                    else
                    {
    
                    }
                }
                else
                {
                    SendMessageToId("Введите название игры.", message.Chat.Id);
                }









            }
    }

       
        private bool Initialize()
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
                   
                    LogInConsole.PrintException("Ошибка инициализации токена.\n" + ex.Message);
                    
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

                        LogInConsole.PrintException("В PathToHelloMessage null. Будет задано значение: hello_none");
                        HelloMessage = "hello_none";
                        Console.ForegroundColor = ConsoleColor.White;

                    }
                }
            }
            catch (Exception ex)
            {
                LogInConsole.PrintException("Ошибка инициализации приветственного сообщения.\n");
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
                    case "/sendMessageAll":
                        Console.Write("Введите текстовое сообщение: ");
                        string text = Console.ReadLine();
                        SendMessageToAll(text);
                        break;
                    case "/sendMessageTo":
                        bool result = false;
                        Console.Write("Укажите chatId пользователя либо его Firstname либо Username: ");
                        string userIdent = Console.ReadLine();
                        long chatid = 0;
                        if (long.TryParse(userIdent,out chatid))
                        {
                            var user = Program.Db.Users.Find(chatid);
                            if (user == null)
                            {
                                Console.WriteLine("Пользователь не найден");
                                break;
                            }
                            else
                            {
                                Console.WriteLine("Пользователь " + user.UserId + " найден. Отправка сообщения");
                                if (user.Name != null)
                                {
                                    Console.WriteLine("Имя пользователя: " + user.Name);
                                }
                                Console.WriteLine("Введите сообщение: ");
                                string message = Console.ReadLine();
                                SendMessageToId(message,user.UserId);
                                break;
                            }
                        }
                        else
                        {
                            
                            foreach (var u in Program.Db.Users)
                            {
                                if (u.Name == userIdent)
                                {
                                    Console.WriteLine("Пользователь " + u.UserId + " найден. Отправка сообщения");
                                    if (u.Name != null)
                                    {
                                        Console.WriteLine("Имя пользователя: " + u.Name);
                                    }
                                    Console.WriteLine("Введите сообщение: ");
                                    string message = Console.ReadLine();
                                    SendMessageToId(message, u.UserId);
                                    result = true;
                                    break;
                                }
                            }
                        }
                        if (!result)
                        {
                            Console.WriteLine("Пользователь не найден. Сообщения не будут отправлены");

                        }
                        break;
                    case "/changeDescriptionFile":
                        Console.Write("Укажите путь к файлу: ");
                        PathToDescriptionFile = Console.ReadLine();
                        if (!Initialize())
                        {
                            return;
                        }
                        break;
                    case "/changeDescription":
                        Console.Write("Введите новое описание: ");
                        BotDescription = Console.ReadLine();
                        break;
                    case "/init":
                        Console.WriteLine("");
                        if (!Initialize())
                        {
                            return;
                        }
                        break;
                    case "/chAc":                                           
                        try {
                            Console.WriteLine("Введите id аккаунта");
                            int acId = Convert.ToInt32(Console.ReadLine());
                            Console.WriteLine("Поиск аккаунта в бд");
                            ps_rent_bot.DataBase.Accounts.Account account = Program.Db.Accounts.Find(acId);
                            if (account != null)
                            {
                                Console.WriteLine("Аккаунт найден.");
                                account.IsRented = false;
                                Program.Db.Accounts.Update(account);
                                Console.WriteLine("Снят с аренды");
                            }
                            else
                            {
                                Console.WriteLine("Аккаунт не найден");
                            }
                        }
                        catch (Exception exc4) { Console.WriteLine("Ошибка: " + exc4.Message); }                       
                        break;
                    case "/help":
                        Console.WriteLine("sendMessage - отправить текстовое сообщение всем пользователям");
                        Console.WriteLine("/changeDescriptionFile -  Изменить путь к файлу где находится описание бота");
                        Console.WriteLine("/changeDescription -  изменить описание бота");
                        Console.WriteLine("/init - заново инициализировать значения");
                        Console.WriteLine("/chAc - сменить статус аккаунта на IsRented = False");
                       

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
        public async Task<bool> SendMessageToId(string message,long chatId,string photoUrl = null,bool AddToDb = false, string UserName = null, IReplyMarkup replyMarkup = null)
        {
            try
            {
                if (photoUrl == null)
                {
                    await client.SendTextMessageAsync(chatId, message,replyMarkup: replyMarkup);
                }
                else
                {
                    await client.SendPhotoAsync(photo: photoUrl, chatId: chatId, caption: message, replyMarkup: replyMarkup);
                }
                if (AddToDb && UserName != null)
                {
                    if (Program.Db.Users.Find(chatId) == null)
                    {
                        Program.Db.Users.Add(new User { Name = UserName, UserId = chatId });
                        Console.WriteLine("Добавлен новый пользователь");
                        Program.Db.SaveChanges();
                    }
                    else { Console.WriteLine("Пользователь существует"); }
                }
                return true;
            }
            catch (Telegram.Bot.Exceptions.ApiRequestException) 
            {
               
                RemoveUser(chatId);
                return false;
            }
            catch (Exception ex) 
            {
                LogInConsole.PrintException("Ошибка отправки сообщения: " + ex.Message);
                return false;
            }
        }
        void SendMessageToAll(string message,string photoUrl = null)
        {
            try
            {
                foreach (var user in Program.Db.Users)
                {
                    SendMessageToId(message, user.UserId, photoUrl);
                }
            }
            catch(Exception ex)
            {
                LogInConsole.PrintException("Ошибка получения данных из бд для отправки сообщений: " + ex.Message);
            }
        }
        void RemoveUser(long chatId)
        {
            try
            {
                Program.Db.Remove( Program.Db.Users.Find(chatId));
                Program.Db.SaveChanges();
                Console.WriteLine("Пользователь удалён " + chatId);
            }
            catch (Exception ex)
            {
                LogInConsole.PrintException($"Ошибка удаления пользователя {chatId}. {ex.Message}" );

            }
        }



    }
}


