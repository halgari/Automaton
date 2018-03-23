using Automaton.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.ComponentModel;
using System.IO;

namespace Automaton.View
{
    /// <summary>
    /// This class contains the logic for location validation
    /// </summary>
    internal class SetupStep1ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand SearchMOExePathCommand { get; set; }
        public RelayCommand SearchSourcePathCommand { get; set; }

        private SetupStep ThisStepType { get => SetupStep.Step1; }

        private string _MOExePath;

        public string MOExePath
        {
            get => _MOExePath;
            set
            {
                if (_MOExePath != value)
                {
                    _MOExePath = value;

                    ValidateTextBoxes();
                }
            }
        }

        private string _SourcePath;

        public string SourcePath
        {
            get => _SourcePath;
            set
            {
                if (_SourcePath != value)
                {
                    _SourcePath = value;

                    ValidateTextBoxes();
                }
            }
        }

        public bool IsEnabled { get; set; } = false;
        public bool IsComplete { get; set; } = false;

        public SetupStep1ViewModel()
        {
            SearchMOExePathCommand = new RelayCommand(SearchMOExePath);
            SearchSourcePathCommand = new RelayCommand(SearchSourcePath);

            Messenger.Default.Register<SetupStep>(this, MessengerTypes.SetupStepUpdate, OnRecieveStep);
        }

        private void IncrementStep()
        {
            SetupController.IncrementStep();
        }

        private void OnRecieveStep(SetupStep messengerType)
        {
            IsEnabled = messengerType >= ThisStepType;
        }

        private void SearchMOExePath()
        {
            var dialog = new OpenFileDialog
            {
                Filter = "Mod Organizer | ModOrganizer.exe",
                Title = "Pick your mod organizer executable location"
            };

            dialog.ShowDialog();

            if (dialog.FileName != string.Empty)
            {
                MOExePath = dialog.FileName;
            }
        }

        private void SearchSourcePath()
        {
            var dialog = new CommonOpenFileDialog
            {
                Title = "Pick your source mod archives location",
                IsFolderPicker = true
            };

            dialog.ShowDialog();

            if (dialog.FileName != string.Empty)
            {
                SourcePath = dialog.FileName;
            }
        }

        private void ValidateTextBoxes()
        {
            if (File.Exists(MOExePath) && Directory.Exists(SourcePath))
            {
                IsComplete = true;
                SetupController.IncrementStep();

                ModpackInstance.MOInstallLocation = Path.Combine(new FileInfo(MOExePath).DirectoryName, "mods");
                ModpackInstance.SourceLocation = SourcePath;
            }
            else
            {
                IsComplete = false;
                SetupController.SetStep(ThisStepType);
            }
        }
    }
}