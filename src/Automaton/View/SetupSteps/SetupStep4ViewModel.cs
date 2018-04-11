using System.ComponentModel;
using System.Runtime.CompilerServices;
using Automaton.Annotations;
using Automaton.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using MaterialDesignThemes.Wpf;

namespace Automaton.View
{
    internal class SetupStep4ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand OpenModValidationDialogCommand { get; set; }

        private static SetupStep ThisStepType { get => SetupStep.Step4; }

        public bool IsEnabled { get; set; } = false;

        public SetupStep4ViewModel()
        {
            OpenModValidationDialogCommand = new RelayCommand(OpenModValidationDialog);

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

        private async void OpenModValidationDialog()
        {
            await DialogHost.Show(new ModValidationDialog(), "RootDialogHost");
        }
    }
}