using Automaton.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Automaton.View
{
    internal class ModValidationViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<Mod> MissingMods { get; set; }

        public string NexusUsername { get; set; }
        public string NexusPassword { get; set; }

        public int CurrentIndex { get; set; } = 1;

        public ModValidationViewModel()
        {

        }
    }
}