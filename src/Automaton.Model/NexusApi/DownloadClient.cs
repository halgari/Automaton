﻿using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Automaton.Model.Install;
using Automaton.Model.Install.Intefaces;
using Automaton.Model.NexusApi.Interfaces;
using System;
using System.Linq;
using System.Net;
using Alphaleonis.Win32.Filesystem;
using Autofac;
using System.Threading;
using System.Collections.Generic;
using Automaton.Model.Interfaces;

namespace Automaton.Model.NexusApi
{
    public class DownloadClient : IDownloadClient
    {
        private readonly IInstallBase _installBase;
        private readonly ILogger _logger;

        private int _currentDownloads;
        private readonly int _maxConcurrentDownloads = 1;

        private List<(string, ExtendedMod)> _downloadQueue = new List<(string, ExtendedMod)>();

        public EventHandler<ExtendedMod> DownloadUpdate { get; set; }

        public DownloadClient(IComponentContext components)
        {
            _installBase = components.Resolve<IInstallBase>();
            _logger = components.Resolve<ILogger>();

            Task.Factory.StartNew(QueueController);
        }

        private void QueueController()
        {
            while (true)
            {
                if (_downloadQueue.Any() && _currentDownloads != _maxConcurrentDownloads)
                {
                    var queueObject = _downloadQueue[0];
                    Task.Factory.StartNew(() => DownloadFile(queueObject.Item1, queueObject.Item2));

                    _currentDownloads++;
                    _downloadQueue.RemoveAt(0);
                }

                Thread.Sleep(10);
            }
        }

        public void QueueDownload(string download, ExtendedMod mod)
        {
            if (_downloadQueue.ToList().Any(x => x.Item2.Md5 == mod.Md5))
            {
                return;
            }

            _downloadQueue.Add((download, mod));
        }

        public bool DownloadFile(string downloadUrl, ExtendedMod mod)
        {
            _logger.WriteLine($"Downloading file: {mod.FileName}");

            mod.CurrentDownloadProgress = 0;
            mod.IsIndeterminateProcess = false;
            DownloadUpdate.Invoke(this, mod);

            using (var webClient = new WebClient())
            {
                var downloadPath = Path.Combine(_installBase.DownloadsDirectory, mod.FileName);

                webClient.DownloadProgressChanged += (sender, args) =>
                {
                    if (mod.CurrentDownloadProgress == 100)
                    {
                        _logger.WriteLine("Download complete");

                        return;
                    }

                    mod.CurrentDownloadProgress = args.ProgressPercentage;
                    DownloadUpdate.Invoke(this, mod);
                };

                webClient.DownloadFileCompleted += (sender, args) =>
                {
                    mod.FilePath = downloadPath;
                    _currentDownloads--;
                };

                webClient.DownloadFileAsync(new Uri(downloadUrl), downloadPath);
            }

            return false;
        }
    }
}
