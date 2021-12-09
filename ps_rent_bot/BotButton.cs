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
                            Text = "Арендовать"
                        },
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
        public IReplyMarkup GetQuestionButtons(long chatId)
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

                }) ;
           
        }
    }
}
