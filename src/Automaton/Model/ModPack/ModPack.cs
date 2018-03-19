using Newtonsoft.Json;
using System.Collections.Generic;

namespace Automaton.Model
{
    internal class ModPack
    {
        [JsonProperty("automaton_version")]
        public string AutomatonVersion { get; set; }

        [JsonProperty("modpack_name")]
        public string ModPackName { get; set; }

        [JsonProperty("modpack_author")]
        public string ModPackAuthor { get; set; }

        [JsonProperty("modpack_version")]
        public string ModPackVersion { get; set; }

        [JsonProperty("modpack_source_url")]
        public string ModPackSourceURL { get; set; }

        [JsonProperty("contains_optional_gui")]
        public bool ContainsOptionalGUI { get; set; }

        [JsonProperty("optional_gui")]
        public OptionalInstallation OptionalGUI { get; set; }

        [JsonProperty("mod_installations")]
        public List<Mod> Mods { get; set; }
    }

    #region Optional Installation Objects

    internal class OptionalInstallation
    {
        [JsonProperty("default_image")]
        public string DefaultImage { get; set; }

        [JsonProperty("default_description")]
        public string DefaultDescription { get; set; }

        [JsonProperty("control_groups")]
        public List<Group> ControlGroups { get; set; }
    }

    internal class Group
    {
        [JsonProperty("group_header_text")]
        public string GroupHeaderText { get; set; }

        [JsonProperty("group_controls")]
        public List<Control> GroupControls { get; set; }
    }

    internal class Control
    {
        [JsonProperty("control_type")]
        public ControlType ControlType { get; set; }

        [JsonProperty("control_text")]
        public string ControlText { get; set; }

        [JsonProperty("control_checked")]
        public bool? ControlChecked { get; set; }

        [JsonProperty("control_hover_image")]
        public string ControlHoverImage { get; set; }

        [JsonProperty("control_hover_description")]
        public string ControlHoverDescription { get; set; }

        [JsonProperty("control_flags")]
        public List<Flag> ControlFlags { get; set; }
    }

    internal class Flag
    {
        [JsonProperty("flag_name")]
        public string FlagName { get; set; }

        [JsonProperty("flag_value")]
        public string FlagValue { get; set; }

        [JsonProperty("flag_event_type")]
        public FlagEventType FlagEvent { get; set; }

        [JsonProperty("flag_action_type")]
        public FlagActionType FlagAction { get; set; }
    }

    internal enum ControlType
    {
        CheckBox,
        RadioButton
    }

    internal enum FlagEventType
    {
        Checked,
        UnChecked
    }

    internal enum FlagActionType
    {
        Add,
        Subtract,
        Set
    }

    #endregion Optional Installation Objects

    #region Mod Objects

    internal class Mod
    {
        [JsonProperty("mod_name")]
        public string ModName { get; set; }

        [JsonProperty("mod_archive_name")]
        public string ModArchiveName { get; set; }

        [JsonProperty("mod_archive_size")]
        public string ModArchiveSize { get; set; }

        [JsonProperty("archive_md5sum")]
        public string ArchiveMD5Sum { get; set; }

        [JsonIgnore]
        // Contains the exact mod archive path (handled through model)
        public string ModArchivePath { get; set; }

        [JsonProperty("mod_source_url")]
        public string ModSourceURL { get; set; }

        [JsonProperty("installation_parameters")]
        public List<Installation> InstallationParameters { get; set; }
    }

    internal class Installation
    {
        [JsonProperty("source_location")]
        public string SourceLocation { get; set; }

        [JsonProperty("target_location")]
        public string TargetLocation { get; set; }

        [JsonProperty("installation_conditions")]
        public List<Conditional> InstallationConditions { get; set; }
    }

    internal class Conditional
    {
        [JsonProperty("conditional_flag_name")]
        public string ConditionalFlagName { get; set; }

        [JsonProperty("conditional_flag_value")]
        public string ConditionalFlagValue { get; set; }
    }

    #endregion Mod Objects
}