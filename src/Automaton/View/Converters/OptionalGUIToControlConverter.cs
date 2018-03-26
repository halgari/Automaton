using Automaton.Model;
using System;
using System.Globalization;
using System.Windows.Data;

namespace Automaton.View
{
    internal class OptionalGUIToControlConverter : IValueConverter
    {
        /// <summary>
        /// Converts a standard OptionalGUI object to a list of control groups. Headers with child input controls (checkbox, radiobutton)
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                var optionalGUI = (OptionalGUI)value;

                return optionalGUI.ControlGroups;
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}