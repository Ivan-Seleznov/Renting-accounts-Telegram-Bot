using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot.Types.ReplyMarkups;

namespace ps_rent_bot
{
    class BotButton
    {

        public IReplyMarkup GetBaseButtons(long chatId)
        {
            return new ReplyKeyboardMarkup
            {
                Keyboard = new List<List<KeyboardButton>>()
                    {
                    new List<KeyboardButton>
                    {
                        new KeyboardButton()
                        {
                            Text = "Мои заказы"
                        },
                    },
                    new List<KeyboardButton>
                    {
                        new KeyboardButton()
                        {
                            Text = "О боте"
                        },
                    },
                }
            };
        }
        
        public InlineKeyboardMarkup GetCallBackRentButtons(int accountId)
        {
            return new InlineKeyboardMarkup(inlineKeyboard: new List<List<InlineKeyboardButton>>()
            {
                new List<InlineKeyboardButton>
                {
                    new InlineKeyboardButton()
                    {
                        Text = "7 дней",
                        CallbackData = "7days" + accountId.ToString()
                    },
                    new InlineKeyboardButton()
                    {
                        Text = "14 дней",
                        CallbackData = "14days" + accountId.ToString()

                    },
                    new InlineKeyboardButton()
                    {
                        Text = "21 день",
                        CallbackData = "21days" + accountId.ToString()
                    }

                },
                new List<InlineKeyboardButton>
                {
                    new InlineKeyboardButton()
                    {
                        Text = "30 дней",
                        CallbackData = "30days" + accountId.ToString()
                    },
                    new InlineKeyboardButton()
                    {
                        Text = "60 дней",
                        CallbackData = "60days" + accountId.ToString()

                    },
                },

            });

        }
        public InlineKeyboardMarkup GetCallBackButtons()
        {
            //var InlineKeyboard = new InlineKeyboardMarkup(new[]
            //{
            //    new InlineKeyboardButton
            //    {
            //        Text = "Чат",
            //        CallbackData = "бАн"

            //    },
            //}) ;
            //return InlineKeyboard;

            return new InlineKeyboardMarkup(inlineKeyboard: new List<List<InlineKeyboardButton>>()
                    {
                    new List<InlineKeyboardButton>
                    {
                        new InlineKeyboardButton()
                        {
                            Text = "Чат",
                            CallbackData = "chat"
                        },
                        new InlineKeyboardButton()
                        {
                            Text = "Отзывы",
                            CallbackData = "reviews"

                        },

                    },
                    new List<InlineKeyboardButton>
                    {
                        new InlineKeyboardButton()
                        {
                            Text = "PS 5 бот",
                            Url = "https://t.me/ps5ukrbot",
                            CallbackData = "ps5bot"
                        },

                    },

                });

        }
    }
}

