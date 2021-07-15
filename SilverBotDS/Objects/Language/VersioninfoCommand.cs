﻿namespace SilverBotDS.Objects
{
    public partial class Language
    {
        public class VersioninfoCommand
        {
            public string OS { get; set; } = "Operating system:";
            public string DsharpplusVersion { get; set; } = "Dsharpplus version:";
            public string VersionNumber { get; set; } = "Version number";
            public string GitRepo { get; set; } = "Git repo";
            public string GitCommitHash { get; set; } = "Git Commit hash";
            public string GitBranch { get; set; } = "Git Branch";
            public string IsDirty { get; set; } = "Is dirty";
            public string CLR { get; set; } = "CLR";
        }
    }
}