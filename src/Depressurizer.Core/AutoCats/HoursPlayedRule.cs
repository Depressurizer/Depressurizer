﻿using System.Xml.Serialization;

namespace Depressurizer.Core.AutoCats
{
    public sealed class HoursPlayedRule
    {
        #region Constructors and Destructors

        public HoursPlayedRule(string name, float minHours, float maxHours)
        {
            Name = name;
            MinHours = minHours;
            MaxHours = maxHours;
        }

        public HoursPlayedRule(HoursPlayedRule other)
        {
            Name = other.Name;
            MinHours = other.MinHours;
            MaxHours = other.MaxHours;
        }

        /// <summary>
        ///     Parameter-less constructor for XmlSerializer.
        /// </summary>
        private HoursPlayedRule() { }

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

        [XmlElement("Text")]
        public string Name { get; set; }

        #endregion
    }
}
