
namespace VoiceTexterBot.Services
{
    internal interface IFileHandler
    {
        Task Download(string fieldId, CancellationToken ct);
        string Process(string param);
    }
}
