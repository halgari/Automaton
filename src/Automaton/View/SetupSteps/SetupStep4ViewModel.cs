using Automaton.Model;
using GalaSoft.MvvmLight.Messaging;

namespace Automaton.View.SetupSteps
{
    internal class SetupStep4ViewModel
    {
        private static SetupStep ThisStepType { get => SetupStep.Step4; }

        public bool IsEnabled { get; set; } = false;

        public SetupStep4ViewModel()
        {
            Messenger.Default.Register<SetupStep>(this, MessengerTypes.SetupStepUpdate, OnRecieveStep);
            Messenger.Default.Register<ModpackHeader>(this, MessengerTypes.ModpackHeaderUpdate, OnRecieveModpack);
        }

        private void OnRecieveStep(SetupStep messengerType)
        {
            IsEnabled = messengerType >= ThisStepType;
        }

        private void OnRecieveModpack(ModpackHeader modpack)
        {
        }
    }
}