using System;
using System.IO;

namespace Automaton.Model
{
    internal class ArchiveHandler : IDisposable
    {
        private SevenZipHandler SevenZipHandler;

        private readonly string MetaDirectory = AppDomain.CurrentDomain.BaseDirectory;

        private bool IsModPack;

        public string ExtractionPath;

        public ArchiveHandler(string archivePath)
        {
            SevenZipHandler = new SevenZipHandler(archivePath);
        }

        public bool ExtractArchive()
        {
            IsModPack = false;
            ExtractionPath = Path.Combine(MetaDirectory, "extract");

            return false;
        }

        public bool ExtractModPack()
        {
            IsModPack = true;
            ExtractionPath = Path.Combine(MetaDirectory, "modpack_temp");

            SevenZipHandler.Extract(ExtractionPath);

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