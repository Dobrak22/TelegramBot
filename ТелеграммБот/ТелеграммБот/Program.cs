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
        private const string Profile = "Профиль";
        private const string Game = "Игра";
        private const string Contact = "Контакты";
        private const string Finish = "Закончить";
        private const string OneGame = "Камень/Ножницы/Бумага";
        private const string Back = "Назад"; 
        private const string Stone = "Камень";
        private const string scissors = "Ножницы";
        private const string paper = "Бумага";
        private const string Cancel = "Отмена";
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
                case Profile:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Никнейм: " + update.Message.Chat.FirstName );
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Ну, а продолжение профиля увидите в следующем месяце.");
                    break;
                case Game:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Вы перешли в раздел  с играми, потихоньку их будет становиться больше. Если автор не найдет себе работу, а так пока ждите обнов. :)", replyMarkup: BotButtons.GameButtons());
                    
                    break;
                case Contact:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "Вопросы по сотрудничеству, предлогать по номеру приложеному ниже. " + " Если же имеется желание, то донатить можно.");
                    await botClient.SendContactAsync(message.Chat.Id, phoneNumber: "+79912138879", firstName: "Ara", lastName: "Company");
                    break;
                case Finish:
                    await botClient.SendTextMessageAsync(message.Chat.Id, "До встречи, но если появиться желание вернуться, то просто напиши \"/start\".");
                    break;
            }
            if (message.Text == OneGame)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Выбирай", replyMarkup: BotButtons.tsuefa());
            }
            if (message.Text == Back)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id,"Переход в основное меню.", replyMarkup: BotButtons.GetButtons());
                
            }
            if (message.Text == Cancel)
            {
                await botClient.SendTextMessageAsync(message.Chat.Id, "Переход в основное меню.", replyMarkup: BotButtons.GetButtons());

            }
        }

        //Метод для работы с ошибками
        private static Task Error(ITelegramBotClient arg1, Exception arg2, CancellationToken arg3)
        {
            throw new NotImplementedException();
        }

        //Основное меню
        private IReplyMarkup GetButtons()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                //первый ряд кнопок
                new[]
                {
                    //Сами кнопки
                    new KeyboardButton(Profile),
                    new KeyboardButton(Game),
                },
                //второй ряд кнопок
                new[]
                {
                    //Сами кнопки
                    new KeyboardButton(Contact),
                    new KeyboardButton(Finish),
                }
            })
            {
                ResizeKeyboard = true
            };
        }

        //Меню с играми
        private IReplyMarkup GameButtons()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                //первый ряд кнопок
                new[]
                {
                    new KeyboardButton(OneGame),
                    new KeyboardButton(Back),
                }
            })
            {
                ResizeKeyboard = true
            };
        }

        //Игра камень/ножницы/бумага
        private IReplyMarkup tsuefa()
        {
            return new ReplyKeyboardMarkup(new[]
            {
                //первый ряд кнопок
                new[]
                {
                    new KeyboardButton(Stone),
                    new KeyboardButton(scissors),
                },
                new[]
                {
                    new KeyboardButton(paper),
                    new KeyboardButton(Cancel),
                }
            })
            {
                ResizeKeyboard = true
            };
        }
    }
}