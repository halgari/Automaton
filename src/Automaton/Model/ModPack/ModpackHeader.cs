using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Automaton.Model
{
    internal class ModpackHeader
    {
        #region Meta Modpack information

        [JsonProperty("automaton_version")]
        public string AutomatonVersion { get; set; }

        [JsonProperty("modpack_name")]
        public string ModpackName { get; set; }

        [JsonProperty("modpack_author")]
        public string ModpackAuthor { get; set; }

        [JsonProperty("modpack_version")]
        public string ModpackVersion { get; set; }

        [JsonProperty("modpack_source_url")]
        public string ModpackSourceURL { get; set; }

        #endregion Meta Modpack information

        // Contain relative sources to mod install folders. Note that lower indexes are installed before higher ones.
        private List<string> _ModInstallFolders;

        [JsonProperty("mod_install_folders")]
        public List<string> ModInstallFolders
        {
            get => _ModInstallFolders;
            set => _ModInstallFolders = value;
        }

        // Value must be set to true for the OptionalGUI to be processed and used.
        [JsonProperty("contains_optional_gui")]
        public bool ContainsOptionalGUI { get; set; }

        [JsonProperty("optional_gui")]
        public OptionalGUI OptionalGUI { get; set; }
    }

    #region Optional Installation Objects

    internal class OptionalGUI
    {
        private string _DefaultImage;

        [JsonProperty("default_image")]
        public string DefaultImage
        {
            get => Path.Combine(ModpackInstance.ModpackExtractionLocation, _DefaultImage.StandardizePathSeparators());
            set => _DefaultImage = value;
        }

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
        public List<GroupControl> GroupControls { get; set; }
    }

    internal class GroupControl
    {
        [JsonProperty("control_type")]
        public ControlType ControlType { get; set; }

        [JsonProperty("control_text")]
        public string ControlText { get; set; }

        [JsonProperty("is_control_checked")]
        public bool? IsControlChecked { get; set; }

        private string _ControlHoverImage;

        [JsonProperty("control_hover_image")]
        public string ControlHoverImage
        {
            get
            {
                if (!string.IsNullOrEmpty(_ControlHoverImage))
                {
                    return Path.Combine(ModpackInstance.ModpackExtractionLocation, _ControlHoverImage.StandardizePathSeparators());
                }

                return _ControlHoverImage;
            }
            set => _ControlHoverImage = value;
        }

        [JsonProperty("control_hover_description")]
        public string ControlHoverDescription { get; set; }

        [JsonProperty("control_actions")]
        public List<Flag> ControlActions { get; set; }
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
        Remove,
        Subtract,
        Set
    }

    #endregion Optional Installation Objects
}