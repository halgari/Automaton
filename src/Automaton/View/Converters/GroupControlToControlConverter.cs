using Automaton.Model;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

namespace Automaton.View
{
    class GroupControlToControlConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var groupControls = value as List<GroupControl>;

            foreach (var groupControl in groupControls)
            {
                // Convert control object to a WPF equivalent
                if (groupControl.ControlType == ControlType.CheckBox)
                {
                    var control = new CheckBox()
                    {
                        Content = groupControl.ControlText,
                        IsChecked = groupControl.IsControlChecked,
                        CommandParameter = groupControl
                    };

                    return control;
                }

                else if (groupControl.ControlType == ControlType.RadioButton)
                {
                    var control = new RadioButton()
                    {
                        Content = groupControl.ControlText,
                        IsChecked = groupControl.IsControlChecked,
                        CommandParameter = groupControl
                    };

                    return control;
                }
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
