using System.ComponentModel;

namespace Depressurizer.Core.Enums
{
    public enum AutoCatType
    {
        [Description("None")]
        None = 0,

        [Description("AutoCatGenre")]
        Genre = 1,

        [Description("AutoCatFlags")]
        Flags = 2,

        [Description("AutoCatTags")]
        Tags = 3,

        [Description("AutoCatYear")]
        Year = 4,

        [Description("AutoCatUserScore")]
        UserScore = 5,

        [Description("AutoCatHltb")]
        Hltb = 6,

        [Description("AutoCatManual")]
        Manual = 7,

        [Description("AutoCatDevPub")]
        DevPub = 8,

        [Description("AutoCatGroup")]
        Group = 9,

        [Description("AutoCatName")]
        Name = 10,

        [Description("AutoCatVrSupport")]
        VrSupport = 11,

        [Description("AutoCatLanguage")]
        Language = 12,

        [Description("AutoCatCurator")]
        Curator = 13,

        [Description("AutoCatPlatform")]
        Platform = 14
    }
}
