using Automaton.Model;
using GalaSoft.MvvmLight.Messaging;

namespace Automaton.View
{
    internal class SetupController
    {
        private static SetupStep _CurrentStep { get; set; }

        public static SetupStep CurrentStep
        {
            get => _CurrentStep;
            set
            {
                if (_CurrentStep != value)
                {
                    _CurrentStep = value;

                    Messenger.Default.Send(_CurrentStep, MessengerTypes.SetupStepUpdate);
                }
            }
        }

        public static void IncrementStep()
        {
            CurrentStep++;
        }

        public static void SetStep(SetupStep setupStep)
        {
            CurrentStep = setupStep;
        }
    }

    internal enum SetupStep
    {
        Step0,
        Step1,
        Step2,
        Step3,
        Step4,
        Step5
    }
}