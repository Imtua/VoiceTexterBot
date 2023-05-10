
using VoiceTexterBot.Models;

namespace VoiceTexterBot.Services
{
    internal interface IStorage
    {
        Session GetSession(long chatId);
    }
}
