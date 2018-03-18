using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton
{
    class PackHandler
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


    }
}
