using Automaton.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security;
using System.Windows.Controls;
using GalaSoft.MvvmLight.Command;

namespace Automaton.View
{
    internal class ModValidationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Mod> MissingMods { get; set; }

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
            }

            else
            {
                LogInButtonText = "LOG IN";
            }
        }
    }
}