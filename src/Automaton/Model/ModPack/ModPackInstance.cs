using GalaSoft.MvvmLight.Messaging;

namespace Automaton.Model
{
    internal class ModPackInstance
    {
        /// <summary>
        /// Global modpack object
        /// </summary>
        private static ModPack _ModPack = new ModPack();

        public static ModPack ModPack
        {
            get => _ModPack;

            set
            {
                if (_ModPack != value)
                {
                    _ModPack = value;

                    // Fires an event off via the MessengerService which updates subscribed variables
                    Messenger.Default.Send(ModPack, MessengerTypes.ModPackUpdate);
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
    }
}