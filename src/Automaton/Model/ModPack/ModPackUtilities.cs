using System.Collections.Generic;
using System.IO;

namespace Automaton.Model
{
    internal class ModpackUtilities
    {
        /// <summary>
        /// Extracts and loads an archived modpack into the model
        /// </summary>
        /// <param name="modpackPath"></param>
        public static void LoadModPack(string modpackPath)
        {
            // Extract the modpack archive out to a temp folder
            using (var extractionHandler = new ArchiveHandler(modpackPath))
            {
                extractionHandler.ExtractModpack();
                ModpackInstance.ModpackExtractionLocation = extractionHandler.ModpackExtractionPath;
            }

            // Load the modpack header
            var modpackHeaderPath = Path.Combine(ModpackInstance.ModpackExtractionLocation, "modpack.auto");

            // TODO: Requires exception handling
            if (!File.Exists(modpackHeaderPath))
            {
                return;
            }

            ModpackInstance.ModpackHeader = JSONHandler.DeserializeJson<ModpackHeader>(File.ReadAllText(modpackHeaderPath));

            return;
        }

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