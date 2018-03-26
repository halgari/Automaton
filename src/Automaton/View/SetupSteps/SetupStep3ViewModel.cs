using Automaton.Model;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton.View.SetupSteps
{
    class SetupStep3ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SetupStep ThisStepType { get => SetupStep.Step3; }

        public OptionalGUI OptionalGUI { get; set; }

        public bool IsEnabled { get; set; } = false;
        public bool IsComplete { get; set; } = false;

        public SetupStep3ViewModel()
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
            // Skip this step if the modpack does not contain an optional GUI
            if (!modpack.ContainsOptionalGUI)
            {
                IsComplete = true;

                return;
            }

            OptionalGUI = modpack.OptionalGUI;
        }
    }
}
