using GalaSoft.MvvmLight.Command;
using System.ComponentModel;

namespace Automaton.View
{
    internal class SetupStep0ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand IncrementStepCommand { get; set; }

        public bool IsComplete { get; set; } = false;

        public SetupStep0ViewModel()
        {
            IncrementStepCommand = new RelayCommand(IncrementStep);
        }

        public void IncrementStep()
        {
            SetupController.IncrementStep();

            IsComplete = true;
        }
    }
}