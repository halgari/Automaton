using Automaton.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;

namespace Automaton.View
{
    internal class GroupControlToControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var groupControls = value as List<GroupControl>;
            var controlList = new List<object>();
            var stackPanel = new StackPanel();

            foreach (var groupControl in groupControls)
            {
                // Convert control object to a WPF equivalent
                if (groupControl.ControlType == ControlType.CheckBox)
                {
                    var control = new CheckBox()
                    {
                        Content = groupControl.ControlText,
                        IsChecked = groupControl.IsControlChecked ?? false,
                        CommandParameter = groupControl
                    };

                    control.Checked += SetupStep3ViewModel.Control_Checked;
                    control.Unchecked += SetupStep3ViewModel.Control_Unchecked;
                    control.MouseEnter += SetupStep3ViewModel.Control_Hover;

                    stackPanel.Children.Add(control);
                }

                else if (groupControl.ControlType == ControlType.RadioButton)
                {
                    var control = new RadioButton()
                    {
                        Content = groupControl.ControlText,
                        IsChecked = groupControl.IsControlChecked ?? false,
                        CommandParameter = groupControl
                    };

                    control.Checked += SetupStep3ViewModel.Control_Checked;
                    control.Unchecked += SetupStep3ViewModel.Control_Unchecked;
                    control.MouseEnter += SetupStep3ViewModel.Control_Hover;

                    stackPanel.Children.Add(control);
                }
            }

            controlList.Add(stackPanel);

            return controlList;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}