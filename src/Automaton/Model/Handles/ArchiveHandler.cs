using System;
using System.IO;

namespace Automaton
{
    internal class ArchiveHandler : IDisposable
    {
        private string ArchivePath;
        private string ExtractionPath;
        private string SevenZipPath;

        private readonly string MetaDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public ArchiveHandler(string archivePath, string extractionPath)
        {
            ArchivePath = archivePath;
            ExtractionPath = extractionPath;

            SevenZipPath = Path.Combine(MetaDirectory, "7z.exe");
        }

        public bool Extract()
        {
            return false;
        }

        public ModPack ReadModPack()
        {
            return null;
        }

        public void Dispose()
        {
            Directory.Delete(ExtractionPath, true);
        }
    }
}