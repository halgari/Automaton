using System;
using System.Collections.Generic;
using Automaton.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Automaton.Annotations;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Automaton.View
{
    internal class ModValidationViewModel : IProgress<NexusDownloadUpdate>, IProgress<string>, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Mod> MissingMods { get; set; }

        public ObservableCollection<NexusDownloadUpdate> NexusDownloadList { get; set; } = new ObservableCollection<NexusDownloadUpdate>();

        public List<Task> DownloadTaskQueue { get; set; } = new List<Task>();

        public RelayCommand LogInCommand { get; set; }

        public string NexusUsername { get; set; }
        public string NexusPassword { get; set; }

        public string LogInButtonText { get; set; } = "LOG IN";

        public bool IsLoggingIn { get; set; } = false;
        public bool IsLoggedIn { get; set; } = false;

        public int CurrentIndex { get; set; } = 1;
        public int RunningTasks { get; set; }

        public ModValidationViewModel()
        {
            LogInCommand = new RelayCommand(LogIn);
        }

        private async void LogIn()
        {
            LogInButtonText = "LOGGING IN...";
            IsLoggingIn = true;

            // Attempt to log into the nexus servers.
            var loginResult = await Task.Factory.StartNew(() => NexusHandler.AttemptNexusLogIn(NexusUsername, NexusPassword)).Result;
            IsLoggingIn = false;

            if (loginResult)
            {
                IsLoggedIn = true;
                LogInButtonText = "LOGGED IN";

                // Check the registry for a valid NXM handler key. If none exist, create a new one.

                // Initialize the pipe server
                NamedPipesHandler.InitializeServer(new Progress<string>(Report));

                return;
            }

            LogInButtonText = "LOG IN";
        }

        private void StartTask(Action function)
        {
            var task = new Task(function);
            task.Start();

            RunningTasks++;
        }

        // Updates the UI with any nexus download updates
        public void Report(NexusDownloadUpdate value)
        {
            Application.Current.Dispatcher.BeginInvoke(new Action(() =>
            {
                var doesValueExist = NexusDownloadList.Any(x => x.FilePath == value.FilePath);

                if (!doesValueExist)
                {
                    NexusDownloadList.Add(value);
                }

                else
                {
                    var test = NexusDownloadList;

                    NexusDownloadList[NexusDownloadList
                        .IndexOf(NexusDownloadList
                        .First(x => x.FilePath == value.FilePath))] = value;

                    Debug.WriteLine($"Updated: {value.FileName} || {value.DownloadPercentage}");
                }
            }));
        }

        // Updates the viewmodel with nxm parameters
        public void Report(string nxmString)
        {
            // Initialize a new download manager
            StartTask(() => NexusHandler.DownloadNexusModFile(nxmString, new Progress<NexusDownloadUpdate>(Report)));
        }
    }
}