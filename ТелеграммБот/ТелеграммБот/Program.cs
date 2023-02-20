using Microsoft.VisualBasic;
using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;
using System.Text.Json.Nodes;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Requests;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;
using static System.Net.Mime.MediaTypeNames;

namespace BotTG
{
    class Program
    {

        //Кнопки
        #region
        private const string Text_1 = "Профиль";
        private const string Text_2 = "Игра";
        private const string Text_3 = "Контакты";
        private const string Text_4 = "Закончить";
        private const string Text_5 = "Камень/Ножницы/Бумага";
        private const string Text_6 = "Назад";

        #endregion

        //Основа всего кода с токеном
        static void Main(string[] args)
        {
            var client = new TelegramBotClient("5886061671:AAHBrLIBbCHZH5AsbzwESgWvGMVNDEkLB4k");
            client.StartReceiving(Update, Error);
            Console.WriteLine("Бот начал свою работу");
            Console.ReadLine();
        }

        //Метод для взаимодействия с ботом
        async static Task Update(ITelegramBotClient botClient, Update update, CancellationToken token)
        {
            //BotButtons создан для исключение ошибки CS0120
            var BotButtons = new Program();
            var message = update.Message;

            if (message.Text.ToLower().Contains("/start"))
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Приветствую дорогой друг, предлогаю тебе выбрать доступную команду из списка.", replyMarkup: BotButtons.GetButtons());
                return;
            }
            switch (message.Text)
            {
                case Text_1:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Никнейм: " + update.Message.Chat.FirstName );
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Ну, а продолжение профиля увидите в следующем месяце.");
                    break;
                case Text_2:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Вы перешли в раздел  с играми, потихоньку их будет становиться больше. Если автор не найдет себе работу, а так пока ждите обнов. :)", replyMarkup: BotButtons.GameButtons());
                    
                    break;
                case Text_3:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Вопросы по сотрудничеству, предлогать по номеру +7991-213-88-79. " + " Если же имеется желание, то донатить можно.");
                    break;
                case Text_4:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "До встречи, но если появиться желание вернуться, то просто напиши \"/start\".");

                    break;
            }
            if (message.Text == Text_5)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "В стадии разработки..");
            }
            if (message.Text == Text_6)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "В стадии разработки....");
            }
        }

        //Метод для работы с ошибками
        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        //Создание кнопок для бота
        private IReplyMarkup GetButtons()
        {

            return new ReplyKeyboardMarkup(new[]
            {
                //первый ряд кнопок
                new[]
                {
                    new KeyboardButton(Text_1),
                    new KeyboardButton(Text_2),
                },
                //второй ряд кнопок
                new[]
                {
                    new KeyboardButton(Text_3),
                    new KeyboardButton(Text_4),
                }
            })
            {
                ResizeKeyboard = true
            };
        }
        private IReplyMarkup GameButtons()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                //первый ряд кнопок
                new[]
                {
                    new KeyboardButton(Text_5),
                    new KeyboardButton(Text_6),
                }
            })
            {
                ResizeKeyboard = true
            };
        }
    }
}