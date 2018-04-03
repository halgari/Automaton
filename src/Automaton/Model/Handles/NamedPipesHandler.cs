﻿using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Messaging;
using NamedPipeWrapper;

namespace Automaton.Model
{
    class NamedPipesHandler
    {
        public static NamedPipeServer<string> PipeServer { get; set; }

        public static StreamReader PipeReader { get; set; }

        public static void InitializeServer()
        {
            PipeServer = new NamedPipeServer<string>("Automaton_PIPE");
            PipeServer.ClientMessage += PipeServer_ClientMessage;

            PipeServer.Start();
        }

        private static void PipeServer_ClientMessage(NamedPipeConnection<string, string> connection, string message)
        {
            RouteMessage(message);
        }

        public static void RouteMessage(string message)
        {
            if (!string.IsNullOrEmpty(message))
            {
                Messenger.Default.Send(message, PipeUpdate.MessageRecievedUpdate);
            }
        }
    }

    enum PipeUpdate
    {
        MessageRecievedUpdate
    }
}
