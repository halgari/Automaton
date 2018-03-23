using System;
using System.Diagnostics;
using System.IO;

namespace Automaton.Model
{
    internal class ArchiveHandler : IDisposable
    {
        private readonly string MetaDirectory = AppDomain.CurrentDomain.BaseDirectory;

        private string SevenZipPath;

        public string ExtractionPath;
        public string ModpackExtractionPath;
        public string ArchivePath;

        public ArchiveHandler(string archivePath)
        {
            SevenZipPath = Path.Combine(MetaDirectory, "7z.exe");
            ExtractionPath = Path.Combine(MetaDirectory, "extract");
            ModpackExtractionPath = Path.Combine(MetaDirectory, "modpack_temp");
            ArchivePath = archivePath;
        }

        public bool ExtractArchive()
        {
            return false;
        }

        public bool ExtractModpack()
        {
            return Extract(ModpackExtractionPath);
        }

        public void Dispose()
        {
            if (Directory.Exists(ExtractionPath))
            {
                Directory.Delete(ExtractionPath, true);
            }
        }

        private bool Extract(string extractionPath)
        {
            var processInfo = new ProcessStartInfo()
            {
                WindowStyle = ProcessWindowStyle.Hidden,
                FileName = SevenZipPath,
                Arguments = $"x \"{ArchivePath}\" -o\"{extractionPath}\" -y",
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
    }
}