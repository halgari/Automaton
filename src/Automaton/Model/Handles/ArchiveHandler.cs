using System;
using System.IO;

namespace Automaton.Model
{
    internal class ArchiveHandler : IDisposable
    {
        public string ArchivePath;
        public string ExtractionPath;

        private SevenZipHandler SevenZipHandler;

        private readonly string MetaDirectory = AppDomain.CurrentDomain.BaseDirectory;

        public ArchiveHandler(string archivePath)
        {
            SevenZipHandler = new SevenZipHandler(archivePath);
        }

        public bool ExtractArchive(string extractionPath)
        {
            return false;
        }

        public bool ExtractModPack()
        {
            ExtractionPath = Path.Combine(MetaDirectory, new FileInfo(ArchivePath).Name);

            return false;
        }

        public void Dispose()
        {
            if (Directory.Exists(ExtractionPath))
            {
                Directory.Delete(ExtractionPath, true);
            }
        }
    }
}