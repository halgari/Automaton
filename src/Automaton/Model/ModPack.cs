using System.Collections.Generic;

namespace Automaton
{
    internal class ModPack
    {
        public string AutomatonVersion { get; set; }
        public string ModPackName { get; set; }
        public string ModPackAuthor { get; set; }
        public string ModPackVersion { get; set; }
        public string ModPackSourceURL { get; set; }

        public OptionalInstallation OptionalInstallation { get; set; }

        public List<Mod> Mods { get; set; }
    }

    #region Optional Installation Objects

    internal class OptionalInstallation
    {
        public string DefaultImage { get; set; }
        public string DefaultDescription { get; set; }
        public List<Group> Groups { get; set; }
    }

    internal class Group
    {
        public string HeaderText { get; set; }
        public List<Element> SubElements { get; set; }
    }

    internal class Element
    {
        public ElementType ElementType { get; set; }
        public string ElementText { get; set; }
        public bool? IsChecked { get; set; }
        public string HoverImage { get; set; }
        public string HoverDescription { get; set; }
        public List<Flag> Flags { get; set; }
    }

    internal class Flag
    {
        public string FlagName { get; set; }
        public string FlagValue { get; set; }
        public string FlagEvent { get; set; }
        public string FlagAction { get; set; }
    }

    internal enum ElementType
    {
        CheckBox,
        RadioButton
    }

    #endregion Optional Installation Objects

    #region Mod Objects

    internal class Mod
    {
        public string ModName { get; set; }
        public string ModArchiveName { get; set; }
        public string ModArchiveSize { get; set; }
        public string ModSourceURL { get; set; }
        public string MD5Sum { get; set; }
    }

    internal class Installation
    {
        public string SourceLocation { get; set; }
        public string TargetLocation { get; set; }
    }

    internal class Conditional
    {
        public string FlagName { get; set; }
        public string FlagValue { get; set; }
    }

    #endregion Mod Objects
}