using System;
using System.Diagnostics;
using System.Windows;

namespace Automaton.Model
{
    internal class GenericErrorHandler
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