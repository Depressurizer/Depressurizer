using System.ComponentModel;

namespace Depressurizer.Core.Enums
{
    public enum AutoCatType
    {
        [Description("None")]
        None,

        [Description("AutoCatGenre")]
        Genre,

        [Description("AutoCatFlags")]
        Flags,

        [Description("AutoCatTags")]
        Tags,

        [Description("AutoCatYear")]
        Year,

        [Description("AutoCatUserScore")]
        UserScore,

        [Description("AutoCatHltb")]
        Hltb,

        [Description("AutoCatManual")]
        Manual,

        [Description("AutoCatDevPub")]
        DevPub,

        [Description("AutoCatGroup")]
        Group,

        [Description("AutoCatName")]
        Name,

        [Description("AutoCatVrSupport")]
        VrSupport,

        [Description("AutoCatLanguage")]
        Language,

        [Description("AutoCatCurator")]
        Curator,

        [Description("AutoCatPlatform")]
        Platform
    }
}
