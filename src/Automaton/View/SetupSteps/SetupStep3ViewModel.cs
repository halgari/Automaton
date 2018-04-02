using Automaton.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using System.Windows;

namespace Automaton.View
{
    internal class SetupStep3ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public RelayCommand IncrementStepCommand { get; set; }

        private static SetupStep ThisStepType { get => SetupStep.Step3; }

        public OptionalGUI OptionalGUI { get; set; }

        public string StepDescriptionText { get; set; }
        public string ImagePath { get; set; }
        public string DescriptionText { get; set; }

        public string NotificationText { get; set; } =
            "Hmm, I haven't found anything interesting yet. Make sure you've loaded a modpack first!";

        public bool IsEnabled { get; set; } = false;
        public bool IsNotificationEnabled { get; set; } = true;
        public bool IsComplete { get; set; } = false;

        public SetupStep3ViewModel()
        {
            IncrementStepCommand = new RelayCommand(IncrementStep);

            Messenger.Default.Register<SetupStep>(this, MessengerTypes.SetupStepUpdate, OnRecieveStep);
            Messenger.Default.Register<ModpackHeader>(this, MessengerTypes.ModpackHeaderUpdate, OnRecieveModpack);

            // I'm an idiot and made everything static. This will work for now.
            Messenger.Default.Register<string>(this, "HoverImage", x => ImagePath = x);
            Messenger.Default.Register<string>(this, "HoverDescription", x => DescriptionText = x);
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
                NotificationText = "No optional installation configuration was found. You can skip this step!";

                IsComplete = true;

                return;
            }

            IsNotificationEnabled = false;
            IsComplete = false;
            IsEnabled = true;

            StepDescriptionText =
                "An optional installer configuration was found. Please select from the options below to modify your modpack installation.";

            OptionalGUI = modpack.OptionalGUI;
            ImagePath = modpack.OptionalGUI.DefaultImage;
            DescriptionText = modpack.OptionalGUI.DefaultDescription;
        }

        private void IncrementStep()
        {
            SetupController.IncrementStep();

            NotificationText = "I've saved all of your modifications, lets continue on to the next step!";

            // We have to unload some strings for the card to resize correctly
            ImagePath = "";
            DescriptionText = "";

            // May throw null binding error
            OptionalGUI = null;

            IsNotificationEnabled = true;
            IsComplete = true;
        }

        /// <summary>
        /// Routes off a specified command and flag event to the model
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="flagEventType"></param>
        private static void RouteControlActionEvent(dynamic sender, FlagEventType flagEventType)
        {
            var controlObject = (GroupControl)sender.CommandParameter;
            var matchingControlActions = controlObject.ControlActions
                .Where(x => x.FlagEvent == flagEventType);

            if (matchingControlActions.ContainsAny())
            {
                foreach (var action in matchingControlActions)
                {
                    FlagInstance.AddOrModifyFlag(action.FlagName, action.FlagValue, action.FlagAction);
                }
            }
        }

        #region Event handlers for group controls

        public static void Control_Checked(dynamic sender, RoutedEventArgs e)
        {
            RouteControlActionEvent(sender, FlagEventType.Checked);
        }

        public static void Control_Unchecked(dynamic sender, RoutedEventArgs e)
        {
            RouteControlActionEvent(sender, FlagEventType.UnChecked);
        }

        public static void Control_Hover(dynamic sender, RoutedEventArgs e)
        {
            var controlObject = (GroupControl)sender.CommandParameter;

            // Terrible code
            if (!string.IsNullOrEmpty(controlObject.ControlHoverImage))
            {
                Messenger.Default.Send(controlObject.ControlHoverImage, "HoverImage");
            }

            if (!string.IsNullOrEmpty(controlObject.ControlHoverImage))
            {
                Messenger.Default.Send(controlObject.ControlHoverDescription, "HoverDescription");
            }
        }

        #endregion Event handlers for group controls
    }
}