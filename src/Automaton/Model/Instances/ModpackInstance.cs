using GalaSoft.MvvmLight.Messaging;
using System.Collections.Generic;

namespace Automaton.Model
{
    internal class ModpackInstance
    {
        /// <summary>
        /// Global modpack object
        /// </summary>
        private static ModpackHeader _ModpackHeader;

        public static ModpackHeader ModpackHeader
        {
            get => _ModpackHeader;

            set
            {
                if (_ModpackHeader != value)
                {
                    _ModpackHeader = value;

                    // Fires an event off via the MessengerService which updates subscribed variables
                    Messenger.Default.Send(_ModpackHeader, MessengerTypes.ModpackHeaderUpdate);
                }
            }
        }

        private static List<Mod> _ModpackMods;

        public static List<Mod> ModpackMods
        {
            get => _ModpackMods;
            set
            {
                if (_ModpackMods != value)
                {
                    _ModpackMods = value;
                }
            }
        }

        private static string _MOInstallLocation = "";

        public static string MOInstallLocation
        {
            get => _MOInstallLocation;
            set
            {
                if (_MOInstallLocation != value)
                {
                    _MOInstallLocation = value;
                }
            }
        }

        private static string _SourceLocation = "";

        public static string SourceLocation
        {
            get => _SourceLocation;
            set
            {
                if (_SourceLocation != value)
                {
                    _SourceLocation = value;
                }
            }
        }

        private static string _ModpackExtractionLocation = "";

        public static string ModpackExtractionLocation
        {
            get => _ModpackExtractionLocation;
            set
            {
                if (_ModpackExtractionLocation != value)
                {
                    _ModpackExtractionLocation = value;
                }
            }
        }

        #region Modification Methods

        /// <summary>
        /// Adds a new ModInstallFolder to the instance
        /// </summary>
        /// <param name="path"></param>
        public static void AddModInstallFolder(string path)
        {
            var tempModpackHeader = ModpackInstance.ModpackHeader;
            tempModpackHeader.ModInstallFolders.Add(path);

            ModpackInstance.ModpackHeader = tempModpackHeader;
        }

        /// <summary>
        /// Removes a ModInstallFolder from the instance
        /// </summary>
        /// <param name="path"></param>
        public static void RemoveModInstallFolder(string path)
        {
            var tempModpackHeader = ModpackInstance.ModpackHeader;
            tempModpackHeader.ModInstallFolders.RemoveAll(x => x == path);

            ModpackInstance.ModpackHeader = tempModpackHeader;
        }

        #endregion Modification Methods
    }
}