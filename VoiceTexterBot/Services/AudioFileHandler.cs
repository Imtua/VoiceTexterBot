
using Telegram.Bot;
using VoiceTexterBot.Configuration;

namespace VoiceTexterBot.Services
{
    internal class AudioFileHandler : IFileHandler
    {
        private readonly AppSettings _appSettings;
        private readonly ITelegramBotClient _telegramBotClient;
        public AudioFileHandler(ITelegramBotClient botClient, AppSettings appSettings)
        {
            _telegramBotClient = botClient;
            _appSettings = appSettings;
        }

        public async Task Download(string fieldId, CancellationToken ct)
        {
            string inputAudioFilePath = Path.Combine(_appSettings.DownloadsFolder,
                $"{_appSettings.AudioFileName}.{_appSettings.InputAudioFormat}");
            using (FileStream destinationStream = File.Create(inputAudioFilePath))
            {
                var file = await _telegramBotClient.GetFileAsync(fieldId, ct);
                if (file.FilePath == null)
                    return;

                await _telegramBotClient.DownloadFileAsync(file.FilePath, destinationStream, ct);
            }

        }

        public string Process(string param)
        {
            throw new NotImplementedException();
        }
    }
}
