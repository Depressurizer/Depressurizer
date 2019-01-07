using System.ComponentModel;

namespace Depressurizer.Core.Enums
{
    public enum CuratorRecommendation
    {
        [Description("Error")]
        Error = 0,

        [Description("Recommended")]
        Recommended = 1,

        [Description("Not Recommended")]
        NotRecommended = 2,

        [Description("Informational")]
        Informational = 3
    }
}
