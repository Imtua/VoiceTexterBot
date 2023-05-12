
namespace VoiceTexterBot.Extensions
{
    internal class DirectoryExtension
    {
        public static string GetSolutionRoot()
        {
            var dir = Path.GetDirectoryName(Directory.GetCurrentDirectory());
            var fullName = Directory.GetParent(dir).FullName;
            var projectRoot = fullName.Substring(0, fullName.Length - 4);
            return Directory.GetParent(projectRoot)?.FullName;
        }
    }
}
