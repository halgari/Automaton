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
            var modPack = new ModPack();


            if (new FileInfo(modPackPath).Extension == ".json")
            {
                modPack = JSONHandler.DeserializeJson<ModPack>(File.ReadAllText(modPackPath));
            }

            else
            {
                modPack = LoadArchivedModPack(modPackPath);
            }

            ModPackInstance.ModPack = modPack;
        }

        private static ModPack LoadArchivedModPack(string modPackPath)
        {


            return null;
        }
    }
}
