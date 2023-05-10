using System.Collections.Concurrent;
using VoiceTexterBot.Models;

namespace VoiceTexterBot.Services
{
    internal class MemoryStorage: IStorage
    {
        private readonly ConcurrentDictionary<long, Session> _session;

        public MemoryStorage()
        {
            _session= new ConcurrentDictionary<long, Session>();
        }

        public Session GetSession(long chatId)
        {
            if (_session.ContainsKey(chatId))
            {
                return _session[chatId];
            }

            var newSession = new Session() { LanguageCode = "ru" };
            _session.TryAdd(chatId, newSession);
            return newSession;
        }
    }
}
