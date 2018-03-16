using Alphaleonis.Win32.Filesystem;
using System.Collections.Generic;
using System.Linq;

namespace Automaton.Model
{
    class PackHandlerHelper
    {
        /// <summary>
        /// Gets a list of source file infos from the mod source location.
        /// </summary>
        /// <param name="mods">List of mods</param>
        /// <param name="sourceLocation">Mod source location</param>
        /// <returns></returns>
        public static List<FileInfo> GetSourceFiles(List<Mod> mods, string sourceLocation)
        {
            var sourceFiles = Directory.GetFiles(sourceLocation);
            var sourceFileInfos = sourceFiles.Select(x => new FileInfo(x));
            var matchingSourceFiles = new List<FileInfo>();

            foreach (var mod in mods)
            {
                var matchingFiles = sourceFileInfos.Where(x => x.Length.ToString() == mod.FileSize);

                if (matchingFiles.Count() == 1)
                {
                    matchingSourceFiles.Add(matchingFiles.First());
                }

                // When there are more than one matches found in matchingFiles.
                else if (matchingFiles.Count() > 1)
                {
                    matchingFiles = sourceFileInfos.Where(x => mod.FileName == x.Name);

                    if (matchingFiles.Count() >= 1)
                    {
                        matchingSourceFiles.Add(matchingFiles.First());
                    }

                    else if (matchingFiles.Count() == 0)
                    {
                        throw new System.Exception($"{mod.ModName}'s location was not determined. Make sure that the filename matches {mod.FileName}, and the filesize matches {mod.FileSize}.");
                    }
                }

                // Should only happen on edge cases.
                else if (matchingFiles.Count() == 0)
                {
                    throw new System.Exception($"The location of \"{mod.ModName}\" was not able to be determined. Make sure that the filename matches \"{mod.FileName}\", and the filesize matches \"{mod.FileSize}\".");
                }
            }

            return matchingSourceFiles;
        }

        /// <summary>
        /// Checks the FlagHandler for any matching conditional values.
        /// </summary>
        /// <param name="conditional"></param>
        /// <returns></returns>
        public static bool ShouldRemoveInstallation(Conditional conditional)
        {
            var matchingValues = FlagHandler.FlagList.Where(x => x.FlagName == conditional.Name
                && x.FlagValue == conditional.Value);

            // There is a value in matchingValues = return false;
            // No value = return true;
            return !(matchingValues.Count() > 0);
        }

        /// <summary>
        /// Detects if the ModPack contains a non-null Optionals object with values.
        /// </summary>
        /// <param name="modPack"></param>
        /// <returns></returns>
        public static bool DoOptionalsExist(ModPack modPack)
        {
            // Note to self: Clean this up in the future.

            var modPackOptionals = modPack.OptionalInstallation;

            if (modPackOptionals == null || modPackOptionals.Title == null || modPackOptionals.Groups == null)
            {
                return false;
            }

            if (modPackOptionals.Groups.Count == 0)
            {
                return false;
            }

            return true;
        }
    }
}
