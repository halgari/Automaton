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
    }
}