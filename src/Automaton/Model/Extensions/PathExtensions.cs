﻿using System.IO;

namespace Automaton.Model
{
    internal static class PathExtensions
    {
        public static string StandardizePathSeparators(this string inputString)
        {
            var directorySeparator = Path.DirectorySeparatorChar.ToString();

            if (!string.IsNullOrEmpty(inputString))
            {
                return inputString.Replace("/", directorySeparator);
            }

            return inputString;
        }
    }
}