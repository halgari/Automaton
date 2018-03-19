using Automaton.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton.View
{
    class SetupStep2ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand LoadModPackCommand { get; set; }

        private SetupStep ThisStepType { get => SetupStep.Step2; }

        public string ModPackName { get; set; }
        public string ModPackAuthor { get; set; }
        public string ModPackCount { get; set; }
        public string ModPackInstallSize { get; set; }
        public string ContainsOptionalGUI { get; set; }

        public bool IsEnabled { get; set; } = false;
        public bool IsComplete { get; set; } = false;
        public bool IsLoading { get; set; } = false;

        public SetupStep2ViewModel()
        {
            LoadModPackCommand = new RelayCommand(LoadModPack);

            Messenger.Default.Register<SetupStep>(this, MessengerTypes.SetupStepUpdate, OnRecieveStep);
            Messenger.Default.Register<ModPack>(this, MessengerTypes.ModPackUpdate, OnRecieveModPack);
        }

        private void OnRecieveStep(SetupStep messengerType)
        {
            IsEnabled = messengerType >= ThisStepType;
        }

        private void IncrementStep()
        {
            SetupController.IncrementStep();
        }

        private void OnRecieveModPack(ModPack modPack)
        {
            ModPackName = modPack.ModPackName;
            ModPackAuthor = modPack.ModPackAuthor;
            ModPackCount = modPack.Mods.Count.ToString();

            var tempSize = 0;

            foreach (var mod in modPack.Mods)
            {
                tempSize += Convert.ToInt32(mod.ModArchiveSize);
            }

            ModPackCount = tempSize.ToString();
            ContainsOptionalGUI = modPack.ContainsOptionalGUI.ToString();

            IsComplete = true;
            IncrementStep();

        }

        private void LoadModPack()
        {
            var dialog = new OpenFileDialog()
            {
                Filter = "ModPack File Types (*.json;*.7z;*.rar;*.zip)|*.json;*.7z;*.rar;*.zip|All files (*.*)|*.*",

            };

            dialog.ShowDialog();

            if (File.Exists(dialog.FileName))
            {
                IsLoading = true;
                ModPackUtilities.LoadModPack(dialog.FileName);

                IsLoading = false;
            }
        }
    }
}
