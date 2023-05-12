using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using VoiceTexterBot.Services;

namespace VoiceTexterBot.Controllers
{
    internal class InlineMessageController
    {
        private readonly IStorage _memoryStorage;
        private readonly ITelegramBotClient _telegramClient;

        public InlineMessageController(ITelegramBotClient telegramBotClient, IStorage memoryStorage)
        {
            _telegramClient = telegramBotClient;
            _memoryStorage = memoryStorage;
        }

        public async Task Handle(CallbackQuery? callbackQuery, CancellationToken ct)
        {
            if (callbackQuery == null)
                return;

            _memoryStorage.GetSession(callbackQuery.From.Id).LanguageCode = callbackQuery.Data;

            string languageText = callbackQuery.Data switch
            {
                ":ru:" => "Русский",
                ":en:" => "Английский",
                _ => String.Empty
            };

            await _telegramClient.SendTextMessageAsync(callbackQuery.From.Id,
                $"<b>Язык аудио - {languageText}.{Environment.NewLine}</b>" +
                $"{Environment.NewLine}Можно поменять в главном меню.",
                cancellationToken: ct, parseMode: ParseMode.Html);

        }
    }
}
