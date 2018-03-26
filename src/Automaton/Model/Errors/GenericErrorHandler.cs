using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Automaton.Model
{
    class GenericErrorHandler
    {
        public static void Throw(GenericErrorType genericErrorType, string message, StackTrace stackTrace)
        {
            MessageBox.Show($"{genericErrorType} error thrown. {Environment.NewLine}{message}", $"Automaton {genericErrorType} error");
        }
    }

    public enum GenericErrorType
    {
        Generic,
        JSONParse,
        ModpackStructure,

    }
}
