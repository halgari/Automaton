﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hephaestus
{
    public class Log
    {
        private static Stopwatch _time;

        static Log ()
        { 
            _time = new Stopwatch();
            _time.Start();
        }
   
        public static void Info(string fmt, params object[] args)
        {
            lock (_time) {
                var str = String.Format("{0} - ", _time.Elapsed.ToString("c")) +
                          String.Format(fmt, args.Select(arg => arg.ToString()).ToArray()) + "\n";
                File.AppendAllText("Hephaestus.log", str);
                Console.Write(str);
            }
        }

        public static void Warn(string fmt, params object[] args)
        {
            var old = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Info(fmt, args);
            Console.ForegroundColor = old;
        }
    }
}