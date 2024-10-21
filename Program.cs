using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace VoiceTexterBot
{
    // Создаем основной класс Bot
    
    class Bot
    {
         // Создаем конструктор объекта ITelegramBotClient, через который будет общение с API
        
        private ITelegramBotClient _telegramClient;
        public Bot(ITelegramBotClient telegramClient)
        {
            // Добавим метод HandleUpdateAsync (обработчик событий)

            _telegramClient = telegramClient;
            async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
            {
                //  Обрабатываем нажатия на кнопки  из Telegram Bot API: https://core.telegram.org/bots/api#callbackquery
                if (update.Type == UpdateType.CallbackQuery)
                {
                    await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы нажали кнопку", cancellationToken: cancellationToken);
                    return;
                }

                // Обрабатываем входящие сообщения из Telegram Bot API: https://core.telegram.org/bots/api#message
                if (update.Type == UpdateType.Message)
                {
                    await _telegramClient.SendTextMessageAsync(update.Message.Chat.Id, "Вы отправили сообщение", cancellationToken: cancellationToken);
                    return;
                }
            }
        }
        // Обработчик ошибок
        Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
        {
            // Задаем сообщение об ошибке в зависимости от того, какая именно ошибка произошла
            var errorMessage = exception switch
            {
                ApiRequestException apiRequestException
                    => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
                _ => exception.ToString()
            };

            // Выводим в консоль информацию об ошибке
            Console.WriteLine(errorMessage);

            // Задержка перед повторным подключением
            Console.WriteLine("Ожидаем 10 секунд перед повторным подключением.");
            Thread.Sleep(10000);

            return Task.CompletedTask;
        }
    }
}
