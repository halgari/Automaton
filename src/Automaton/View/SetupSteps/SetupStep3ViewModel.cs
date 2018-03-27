using Automaton.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Automaton.View
{
    internal class SetupStep3ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand IncrementStepCommand { get; set; }

        private SetupStep ThisStepType { get => SetupStep.Step3; }

        public OptionalGUI OptionalGUI { get; set; }

        public bool IsEnabled { get; set; } = false;
        public bool IsComplete { get; set; } = false;

        public SetupStep3ViewModel()
        {
            IncrementStepCommand = new RelayCommand(IncrementStep);

            Messenger.Default.Register<SetupStep>(this, MessengerTypes.SetupStepUpdate, OnRecieveStep);
            Messenger.Default.Register<ModpackHeader>(this, MessengerTypes.ModpackHeaderUpdate, OnRecieveModpack);
        }

        private void OnRecieveStep(SetupStep messengerType)
        {
            IsEnabled = messengerType >= ThisStepType;
        }

        private void OnRecieveModpack(ModpackHeader modpack)
        {
            // Skip this step if the modpack does not contain an optional GUI
            if (!modpack.ContainsOptionalGUI)
            {
                IsComplete = true;

                return;
            }

            OptionalGUI = modpack.OptionalGUI;
        }

        private void IncrementStep()
        {
            SetupController.IncrementStep();

            IsComplete = true;

            // Update the list of mod installation parameters

        }

        #region Event handlers for group controls

        public static void Control_Checked(dynamic sender, RoutedEventArgs e)
        {
            var controlObject = (GroupControl)sender.CommandParameter;
            var matchingControlActions = controlObject.ControlActions
                .Where(x => x.FlagEvent == FlagEventType.Checked);

            if (matchingControlActions.ContainsAny())
            {
                foreach (var action in matchingControlActions)
                {
                    // Add or modify instance in FlagInstance
                    FlagInstance.AddOrModifyFlag(action.FlagName, action.FlagValue, action.FlagAction);
                }
            }
        }

        public static void Control_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        public static void Control_Hover(object sender, RoutedEventArgs e)
        {

        }

        #endregion
    }
}