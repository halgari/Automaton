﻿using Automaton.Model;
using System.Windows;

namespace Automaton.View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            if (ProcessFinder.IsProcessAlreadyRunning())
            {
                //if (e.Args.Any() // Check if args contain any data
                //    && e.Args[0].StartsWith("nxm", StringComparison.OrdinalIgnoreCase)) // Check to see if it contains correct data
                //{
                //    NamedPipes.SendMessage(e.Args[0]);
                //}

                //// We only want one instance of Automaton running at one time
                //Environment.Exit(0);
            }
        }
    }
}