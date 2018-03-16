﻿using Alphaleonis.Win32.Filesystem;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows;
using static Automaton.Handles.LoadingDialogHandle;

namespace Automaton.Model
{
    class PackHandler
    {
        public static ModPack ModPack { get; set; }

        public static string ModPackLocation { get; set; }
        public static string SourceLocation { get; set; }
        public static string InstallationLocation { get; set; }

        /// <summary>
        /// Initializes the PackHandler class with required source and installation locations.
        /// </summary>
        /// <param name="sourceLocation">The mod source location.</param>
        /// <param name="installationLocation">The mod pack installation location.</param>
        public static void Initialize(string sourceLocation, string installationLocation)
        {
            SourceLocation = sourceLocation;
            InstallationLocation = installationLocation;
        }

        /// <summary>
        /// Reads the targeted file for valid JSON and converts it to a ModPack object. This is saved within the PackHandler
        /// </summary>
        public static ModPack ReadPack(string modPackLocation)
        {
            ModPackLocation = modPackLocation;

            if (!File.Exists(ModPackLocation))
            {
                throw new Exception($"Modpack location not found: {ModPackLocation}");
            }

            if (Path.GetExtension(modPackLocation) == ".json")
            {
                var modPackContents = File.ReadAllText(modPackLocation);

                try
                {
                    ModPack = JsonConvert.DeserializeObject<ModPack>(modPackContents);

                    Messenger.Default.Send(ModPack, MessengerToken.ModPack);

                    return ModPack;
                }

                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "PARSE ERROR", MessageBoxButton.OK, MessageBoxImage.Error);

                    return null;
                }
            }

            // Unzip the file into the temporary directory
            using (var sevenZipHandler = new SevenZipHandler())
            {
                var tempDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "modpack_temp");
                var extractedModPackName = Path.GetFileNameWithoutExtension(ModPackLocation);
                var extractedModPackPath = Path.Combine(tempDirectory, extractedModPackName);

                sevenZipHandler.ExtractArchive(modPackLocation, tempDirectory);

                var packFileLocation = Path.Combine(extractedModPackPath, "modpack.json");

                if (!File.Exists(packFileLocation))
                {
                    return null;
                }

                var modPackContents = File.ReadAllText(packFileLocation);

                try
                {
                    ModPack = JsonConvert.DeserializeObject<ModPack>(modPackContents);
                    ModPack.Mods = ModPack.Mods.OrderBy(x => x.LoadOrder).ToList();

                    Messenger.Default.Send(ModPack, MessengerToken.ModPack);

                    return ModPack;
                }

                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
        }

        /// <summary>
        /// Filters the ModPack with optional installer parameters.
        /// </summary>
        /// <returns></returns>
        public static ModPack FilterModPack()
        {
            var workingModPack = ModPack;
            var mods = workingModPack.Mods;
            var installationsToRemove = new List<Installation>();

            var modInstallations = mods.SelectMany(x => x.Installations)
                .Where(x => x.Conditionals != null);

            var placeholderInstallations = modInstallations;

            foreach (var installation in placeholderInstallations)
            {
                var installationConditionals = installation.Conditionals;

                foreach (var conditional in installationConditionals)
                {
                    if (PackHandlerHelper.ShouldRemoveInstallation(conditional))
                    {
                        installationsToRemove.Add(installation);
                    }
                }
            }

            // Filter out all of the installations that need to be removed
            foreach (var installation in installationsToRemove.Distinct())
            {
                workingModPack.Mods.ForEach(x => x.Installations.Remove(installation));
            }

            // Filter out all mods with no installations
            workingModPack.Mods = workingModPack.Mods.Where(x => x.Installations.Count > 0).ToList();

            ModPack = workingModPack;

            Messenger.Default.Send(workingModPack, MessengerToken.ModPack);

            return workingModPack;
        }

        /// <summary>
        /// Validate the source mod location for all required mod files, as generated by the optionals list.
        /// </summary>
        public static List<Mod> ValidateSourceLocation()
        {
            var files = Directory.GetFiles(SourceLocation);
            var fileSizes = files.Select(x => new FileInfo(x).Length);
            var modPack = ModPack;
            var missingMods = new List<Mod>();

            foreach (var mod in modPack.Mods)
            {
                // Gets files from sourceLocation which match the size of the modPack mod
                var filteredFileSizes = fileSizes.Where(x => x.ToString() == mod.FileSize);

                if (filteredFileSizes.Count() == 0)
                {
                    missingMods.Add(mod);
                }
            }

            Messenger.Default.Send(missingMods, MessengerToken.MissingMods);

            return missingMods;
        }

        /// <summary>
        /// Installs the mod pack into the installation location.
        /// </summary>
        public static void InstallModPack()
        {
            var mods = ModPack.Mods;
            var sourceFiles = PackHandlerHelper.GetSourceFiles(mods, SourceLocation);

            // No mods were found in the source -- should not occur with proper mod validation.
            if (sourceFiles.Count() == 0)
            {
                return;
            }

            var totalTime = Stopwatch.StartNew();

            // Initialize the loading dialog.
            OpenDialog("Installing Modpack", "This may take a while...");

            using (var sevenZipExtractor = new SevenZipHandler())
            {
                foreach (var mod in mods)
                {
                    var stopwatch = Stopwatch.StartNew();
                    var workingModFile = sourceFiles.Where(x => x.Length.ToString() == mod.FileSize || x.Name == mod.FileName).First();
                    var installations = mod.Installations;

                    UpdateDebugText($"({mods.FindIndex(x => x == mod) + 1}/{mods.Count}) - {mod.ModName}");
                    UpdateDebugText($"Extracting: {mod.FileName}");

                    sevenZipExtractor.ExtractArchive(workingModFile.FullName);

                    UpdateDebugText($"Extracted successfully");

                    foreach (var installation in installations)
                    {
                        UpdateDebugText($"Copying: \"{installation.Source}\" → \"{installation.Target}\"");

                        sevenZipExtractor.Copy(mod, installation, installation.Source, installation.Target);
                    }

                    UpdateDebugText("Deleting extracted files...");
                    sevenZipExtractor.DeleteExtractedFiles(workingModFile.FullName);

                    stopwatch.Stop();

                    UpdateDebugText($"Completed in {stopwatch.Elapsed} seconds");
                    UpdateDebugText("####################");

                }
            }

            totalTime.Stop();
            UpdateDialog("Installation Complete", "Automaton can now be closed. Enjoy your modded experience!", $"OPERATION COMPLETED IN {totalTime.Elapsed}");
            LoadingComplete();
        }

        /// <summary>
        /// A threaded variation of InstallModPack.
        /// </summary>
        public static void ThreadedInstallModPack()
        {
            var thread = new Thread(new ThreadStart(InstallModPack))
            {
                IsBackground = true
            };

            thread.Start();

        }
    }
}
