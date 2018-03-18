using System.Collections.Generic;

namespace Automaton.Model
{
    internal class ModPackHandler
    {
        /// <summary>
        /// Returns a list of type <see cref="Mod"/> which contains mods that were not able to be found in the standard source directory
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <returns></returns>
        public static List<Mod> GetModsWithMissingArchives(string sourceDirectory)
        {
            return null;
        }

        /// <summary>
        /// Returns true/false on whether paths outlined by <see cref="Mod.ModArchivePath"/> exist
        /// Checks filesize and MD5Sum (on edge cases) to confirm that the archive is correct
        /// </summary>
        /// <param name="sourceDirectory"></param>
        /// <returns></returns>
        public static bool ValidModArchiveLocations(string sourceDirectory)
        {
            return false;
        }

        /// <summary>
        /// Determines is parameterized mod archive path exists and contains correct data
        /// </summary>
        /// <param name="mod"></param>
        /// <returns></returns>
        public static bool DoesModArchivePathExist(Mod mod)
        {
            return false;
        }

        /// <summary>
        /// Builds initial modpack mod source archive paths <see cref="Mod.ModArchivePath"/>
        /// </summary>
        /// <returns></returns>
        public static void BuildModArchivePaths()
        {
        }

        /// <summary>
        /// Patches updated source path into modpack at specified <see cref="Mod"/> value with path
        /// </summary>
        /// <param name="mod"></param>
        /// <param name="path"></param>
        /// <returns></returns>
        public static void PatchModArchivePath(Mod mod, string path)
        {
        }
    }
}