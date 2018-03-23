using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton.Model
{
    class ModPackUtilities
    {
        public static void LoadModPack(string modPackPath)
        {
            var modPack = LoadArchivedModPack(modPackPath);

            ModPackInstance.ModPack = modPack;
        }

        private static ModPack LoadArchivedModPack(string modPackPath)
        {


            return null;
        }
    }
}
