using Telegram.Bot;

namespace VoiceTexterBot
{
    class Bot
    {
        private ITelegramBotClient _telegramClient;
        public Bot(ITelegramBotClient telegramClient) 
        { _telegramClient = telegramClient;
        
        }
    }
}
