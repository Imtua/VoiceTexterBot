using Telegram.Bot;
using Telegram.Bot.Types;
using VoiceTexterBot.Configuration;
using VoiceTexterBot.Services;

namespace VoiceTexterBot.Controllers
{
    internal class VoiceMessageController
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramClient;
        private readonly IFileHandler _audioFileHandler;

        public VoiceMessageController(AppSettings appSettings,
            ITelegramBotClient telegramBotClient, IFileHandler fileHandler)
        {
            _appSettings = appSettings;
            _telegramClient = telegramBotClient;
            _audioFileHandler = fileHandler;
        }

        public async Task Handle(Message message, CancellationToken ct)
        {
            var fieldId = message.Voice?.FileId;
            if (fieldId == null)
                return;

            await _audioFileHandler.Download(fieldId, ct);
            await _telegramClient.SendTextMessageAsync(message.Chat.Id,
                "Голосовое сообщение загружено", cancellationToken: ct);
        }
    }
}
