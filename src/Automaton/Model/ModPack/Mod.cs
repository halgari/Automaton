using Newtonsoft.Json;
using System.Collections.Generic;

namespace Automaton.Model
{
    /// <summary>
    /// The base mod object
    /// </summary>
    internal class Mod
    {
        [JsonProperty("mod_name")]
        public string ModName { get; set; }

        [JsonProperty("mod_archive_name")]
        public string ModArchiveName { get; set; }

        [JsonProperty("mod_archive_size")]
        public string ModArchiveSize { get; set; }

        [JsonIgnore]
        public string ModArchivePath { get; set; }

        [JsonIgnore]
        public string ModInstallParameterPath { get; set; }

        [JsonProperty("archive_md5sum")]
        public string ArchiveMD5Sum { get; set; }

        [JsonProperty("mod_source_url")]
        public string ModSourceURL { get; set; }

        [JsonProperty("archive_id")]
        public ArchiveID ArchiveID { get; set; }

        [JsonProperty("installation_parameters")]
        public List<Installation> InstallationParameters { get; set; }
    }

    /// <summary>
    /// Mod installation parameters
    /// </summary>
    internal class Installation
    {
        [JsonProperty("source_location")]
        public string SourceLocation { get; set; }

        [JsonProperty("target_location")]
        public string TargetLocation { get; set; }

        [JsonProperty("installation_conditions")]
        public List<Conditional> InstallationConditions { get; set; }
    }

    /// <summary>
    /// Flag conditional parameters
    /// </summary>
    internal class Conditional
    {
        [JsonProperty("conditional_flag_name")]
        public string ConditionalFlagName { get; set; }

        [JsonProperty("conditional_flag_value")]
        public string ConditionalFlagValue { get; set; }
    }

    /// <summary>
    /// Allows for fast archive identification data to be stored
    /// </summary>
    internal class ArchiveID
    {
        [JsonProperty("file_markers")]
        public List<FileMarker> FileMarkers { get; set; }

        [JsonProperty("middle_bytes")]
        public List<string> MiddleBytes { get; set; }

        [JsonProperty("archive_identifier")]
        public string ArchiveIdentifier { get; set; }
    }

    internal class FileMarker
    {
        [JsonProperty("byte_index")]
        public string ByteIndex { get; set; }

        [JsonProperty("byte_size")]
        public int ByteSize { get; set; }

        [JsonProperty("byte")]
        public string Byte { get; set; }
    }
}