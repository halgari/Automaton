using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

namespace Automaton.View
{
    public static class PasswordHelper
    {
        public static readonly DependencyProperty PasswordProperty = DependencyProperty.RegisterAttached("Password", typeof(string), typeof(PasswordHelper),
             new FrameworkPropertyMetadata(string.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnPasswordPropertyChanged, (o, value) => value, true, UpdateSourceTrigger.PropertyChanged));

        public static string GetPassword(DependencyObject dp)
        {
            return (string)dp.GetValue(PasswordProperty);
        }
        public static void SetPassword(DependencyObject dp, string value)
        {
            dp.SetValue(PasswordProperty, value);
        }

        private static void OnPasswordPropertyChanged(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                passwordBox.PasswordChanged -= PasswordChanged;

                var password = (string)e.NewValue;
                passwordBox.Password = password;

                var caretPos = string.IsNullOrEmpty(password) ? 0 : password.Length;
                passwordBox.SetSelectionOrCaretPosition(caretPos);

                passwordBox.PasswordChanged += PasswordChanged;
            }
        }

        private static MethodInfo _select;
        private static void SetSelectionOrCaretPosition(this PasswordBox passwordBox, int start, int length = 0)
        {
            if (_select == null)
            {
                _select = passwordBox.GetType().GetMethod("Select", BindingFlags.Instance | BindingFlags.NonPublic);
            }
            _select.Invoke(passwordBox, new object[] { start, length });
            passwordBox.Focus();
        }

        private static void PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (sender is PasswordBox passwordBox)
            {
                SetPassword(passwordBox, passwordBox.Password);
            }
        }
    }
}
