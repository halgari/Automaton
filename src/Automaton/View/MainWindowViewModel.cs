using Automaton.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Windows;
using MaterialDesignThemes.Wpf;

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

        public string ModpackName { get; set; }

        public MainWindowViewModel()
        {
            OnWindowDragCommand = new RelayCommand<Window>(WindowDrag);
            OnWindowDoubleClickCommand = new RelayCommand<Window>(WindowDoubleClick);
            MinimizeWindowCommand = new RelayCommand<Window>(MinimizeWindow);
            ResizeWindowCommand = new RelayCommand<Window>(WindowDoubleClick);
            CloseWindowCommand = new RelayCommand<Window>(CloseWindow);

            Messenger.Default.Register<ModpackHeader>(this, MessengerTypes.ModpackHeaderUpdate, OnModpackUpdate);
        }

        private void OnModpackUpdate(ModpackHeader modPack)
        {
            if (ModpackName != modPack.ModpackName)
            {
                ModpackName = modPack.ModpackName;
            }
        }

        private void WindowDrag(Window window)
        {
            window.DragMove();

            Test();
        }

        private async void Test()
        {
            await DialogHost.Show(new ModValidationDialog(), "RootDialogHost");
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