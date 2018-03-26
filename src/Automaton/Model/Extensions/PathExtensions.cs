using System.IO;

namespace Automaton.Model
{
    internal static class PathExtensions
    {
        public static string StandardizePathSeparators(this string inputString)
        {
            var directorySeparator = Path.DirectorySeparatorChar.ToString();

            return inputString.Replace("/", directorySeparator);
        }
    }
}