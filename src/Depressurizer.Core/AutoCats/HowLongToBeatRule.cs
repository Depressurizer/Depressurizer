using Depressurizer.Core.Enums;

namespace Depressurizer.Core.AutoCats
{
    public sealed class HowLongToBeatRule : AutoCatRule
    {
        #region Constructors and Destructors

        public HowLongToBeatRule(string name, float minHours, float maxHours, TimeType timeType)
        {
            Name = name;
            MinHours = minHours;
            MaxHours = maxHours;
            TimeType = timeType;
        }

        public HowLongToBeatRule(HowLongToBeatRule other)
        {
            Name = other.Name;
            MinHours = other.MinHours;
            MaxHours = other.MaxHours;
            TimeType = other.TimeType;
        }

        /// <summary>
        ///     Parameter-less constructor for XmlSerializer.
        /// </summary>
        private HowLongToBeatRule() { }

        #endregion

        #region Public Properties

        /// <summary>
        ///     Maximum hours played, upper limit of the rule.
        /// </summary>
        public float MaxHours { get; set; }

        /// <summary>
        ///     Minimum hours played, lower limit of the rule.
        /// </summary>
        public float MinHours { get; set; }

        public TimeType TimeType { get; set; }

        #endregion
    }
}
