using Automaton.Model;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton.View
{
    class SetupStep2ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private SetupStep ThisStepType { get => SetupStep.Step2; }

        public bool IsEnabled { get; set; } = false;
        public bool IsComplete { get; set; } = false;
        public bool IsLoading { get; set; } = false;

        public SetupStep2ViewModel()
        {
            Messenger.Default.Register<SetupStep>(this, MessengerTypes.SetupStepUpdate, OnRecieveStep);
        }

        private void OnRecieveStep(SetupStep messengerType)
        {
            IsEnabled = messengerType >= ThisStepType;
        }
    }
}
