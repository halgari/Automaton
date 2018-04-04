using System.Collections.Generic;
using Automaton.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Microsoft.WindowsAPICodePack.Shell.PropertySystem;

namespace Automaton.View
{
    internal class ModValidationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Mod> MissingMods { get; set; }
        public ObservableCollection<NexusDownload> CurrentNexusDownloads { get; set; }

        private static List<Task> TaskList { get; set; } = new List<Task>();

        public RelayCommand LogInCommand { get; set; }

        public string NexusUsername { get; set; }
        public string NexusPassword { get; set; }

        public string LogInButtonText { get; set; } = "LOG IN";

        public bool IsLoggingIn { get; set; } = false;
        public bool IsLoggedIn { get; set; } = false;

        public int CurrentIndex { get; set; } = 1;

        public ModValidationViewModel()
        {
            LogInCommand = new RelayCommand(LogIn);

            Messenger.Default.Register<string>(this, PipeUpdate.MessageRecievedUpdate, OnRecieveServerMessage);
            Messenger.Default.Register<NexusDownload>(this, NexusDownloadUpdate.Update, OnRecieveDownloadUpdate);
        }

        private async void LogIn()
        {
            LogInButtonText = "LOGGING IN...";

            IsLoggingIn = true;

            // Attempt to log into the nexus servers.
            var loginResult = await NexusHandler.AttemptNexusLogIn(NexusUsername, NexusPassword);

            IsLoggingIn = false;

            if (loginResult)
            {
                IsLoggedIn = true;
                LogInButtonText = "LOGGED IN";

                // Check the registry for a valid NXM handler key. If none exist, create a new one.

                // Initialize the pipe server
                NamedPipesHandler.InitializeServer();
            }

            else
            {
                LogInButtonText = "LOG IN";
            }
        }

        private void OnRecieveServerMessage(string nxmString)
        {
            // Create new task and initialize download
            var cancellationTokenSouce = new CancellationTokenSource();
            var cancellationToken = cancellationTokenSouce.Token;
            var task = Task.Factory.StartNew(() => DownloadTask(nxmString, cancellationTokenSouce), cancellationToken);


            TaskList.Add(task);
        }

        private void OnRecieveDownloadUpdate(NexusDownload nexusDownload)
        {
            if (CurrentNexusDownloads.Contains(nexusDownload))
            {
                nexusDownload.CancellationTokenSource.Cancel();
            }


        }

        private void DownloadTask(string nxmString, CancellationTokenSource cancellationToken)
        {
            var downloadLink = NexusHandler.AttemptFindDownloadPath(nxmString).Result;
            var downloadResult = NexusHandler.StartFileDownload(downloadLink, cancellationToken).Result;
        }
    }
}