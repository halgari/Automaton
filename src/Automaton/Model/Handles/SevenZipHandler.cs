using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automaton.Model
{
    class SevenZipHandler
    {
        private string ArchivePath;
        private string MetaDirectory;
        private string SevenZipPath;

        public SevenZipHandler(string archivePath)
        {
            GetProcessBinaries();

            MetaDirectory = AppDomain.CurrentDomain.BaseDirectory;
            SevenZipPath = Path.Combine(MetaDirectory, "7z.exe");

            ArchivePath = archivePath;
        }

        public bool Extract(string extractionPath)
        {
            var processInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = SevenZipPath,
                Arguments = ArgumentsBuilder(extractionPath),
                UseShellExecute = false
            };

            var process = new Process
            {
                StartInfo = processInfo
            };

            process.Start();
            process.WaitForExit();

            return true;
        }

        public void Dispose()
        {

        }

        private void GetProcessBinaries()
        {

        }

        private string ArgumentsBuilder(string extractionPath)
        {
            var workingCommand = $"x \"{ArchivePath}\" -o\"{extractionPath}\" -y";

            return workingCommand;
        }
    }
}
