using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using Automaton.Model;
using System.ComponentModel;

namespace Automaton.View
{
    internal class MainWindowViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand<Window> OnWindowDragCommand { get; set; }
        public RelayCommand<Window> OnWindowDoubleClickCommand { get; set; }
        public RelayCommand<Window> MinimizeWindowCommand { get; set; }
        public RelayCommand<Window> ResizeWindowCommand { get; set; }
        public RelayCommand<Window> CloseWindowCommand { get; set; }

        public string ModPackName { get; set; }

        public MainWindowViewModel()
        {
            OnWindowDragCommand = new RelayCommand<Window>(WindowDrag);
            OnWindowDoubleClickCommand = new RelayCommand<Window>(WindowDoubleClick);
            MinimizeWindowCommand = new RelayCommand<Window>(MinimizeWindow);
            ResizeWindowCommand = new RelayCommand<Window>(WindowDoubleClick);
            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);

            Messenger.Default.Register<ModPack>(this, MessengerTypes.ModPackUpdate, OnModPackUpdate);
        }

        private void OnModPackUpdate(ModPack modPack)
        {
            if (ModPackName != modPack.ModPackName)
            {
                ModPackName = modPack.ModPackName;
            }
        }

        private void WindowDrag(Window window)
        {
            window.DragMove();
        }

        private void WindowDoubleClick(Window window)
        {
            var windowState = window.WindowState;

            window.WindowState = windowState == WindowState.Normal ? WindowState.Maximized : WindowState.Normal;
        }

        private void MinimizeWindow(Window window)
        {
            window.WindowState = WindowState.Minimized;
        }

        private void CloseWindow(Window window)
        {
            window.Close();
        }
    }
}