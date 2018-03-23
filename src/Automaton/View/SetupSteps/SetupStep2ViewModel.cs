using Automaton.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System.ComponentModel;
using System.IO;

namespace Automaton.View
{
    internal class SetupStep2ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand LoadModpackCommand { get; set; }

        private SetupStep ThisStepType { get => SetupStep.Step2; }

        public string ModPackName { get; set; }
        public string ModPackAuthor { get; set; }

        public bool IsEnabled { get; set; } = false;
        public bool IsComplete { get; set; } = false;
        public bool IsLoading { get; set; } = false;

        public SetupStep2ViewModel()
        {
            LoadModpackCommand = new RelayCommand(LoadModpack);

            Messenger.Default.Register<SetupStep>(this, MessengerTypes.SetupStepUpdate, OnRecieveStep);
            Messenger.Default.Register<ModpackHeader>(this, MessengerTypes.ModpackHeaderUpdate, OnRecieveModpack);
        }

        private void OnRecieveStep(SetupStep messengerType)
        {
            IsEnabled = messengerType >= ThisStepType;
        }

        private void IncrementStep()
        {
            SetupController.IncrementStep();
        }

        private void OnRecieveModpack(ModpackHeader modPack)
        {
            ModPackName = modPack.ModpackName;
            ModPackAuthor = modPack.ModpackAuthor;

            IsComplete = true;
            IncrementStep();
        }

        private void LoadModpack()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "ModPack File Types (.7z, .rar, .zip)|*.7z;*.rar;*.zip|All files (*.*)|*.*",
            };

            dialog.ShowDialog();

            if (File.Exists(dialog.FileName))
            {
                IsLoading = true;
                ModpackUtilities.LoadModPack(dialog.FileName);

                IsLoading = false;
            }
        }
    }
}