using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton.Model
{
    static class PathExtensions
    {
        public static string StandardizePathSeparators(this string inputString)
        {
            var directorySeparator = Path.DirectorySeparatorChar.ToString();

            return inputString.Replace("/", directorySeparator);
        }
    }
}
