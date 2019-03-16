﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using Depressurizer.Core.Enums;
using Depressurizer.Core.Helpers;
using Depressurizer.Core.Models;

namespace Depressurizer.AutoCats
{
    public class AutoCatVrSupport : AutoCat
    {
        #region Constants

        // Serialization constants
        public const string TypeIdString = "AutoCatVrSupport";

        private const string XmlNameFilter = "Filter";

        private const string XmlNameFlag = "Flag";

        private const string XmlNameHeadsetsList = "Headsets";

        private const string XmlNameInputList = "Input";

        private const string XmlNameName = "Name";

        private const string XmlNamePlayAreaList = "PlayArea";

        private const string XmlNamePrefix = "Prefix";

        #endregion

        #region Fields

        private VRSupport _includedVrSupportFlags;

        #endregion

        #region Constructors and Destructors

        public AutoCatVrSupport(string name, string filter = null, string prefix = null, List<string> headsets = null, List<string> input = null, List<string> playArea = null, bool selected = false) : base(name)
        {
            Filter = filter;
            Prefix = prefix;

            IncludedVRSupportFlags.Headsets = headsets ?? new List<string>();
            IncludedVRSupportFlags.Input = input ?? new List<string>();
            IncludedVRSupportFlags.PlayArea = playArea ?? new List<string>();
            Selected = selected;
        }

        protected AutoCatVrSupport(AutoCatVrSupport other) : base(other)
        {
            Filter = other.Filter;
            Prefix = other.Prefix;
            IncludedVRSupportFlags = other.IncludedVRSupportFlags;
            Selected = other.Selected;
        }

        //XmlSerializer requires a parameterless constructor
        private AutoCatVrSupport() { }

        #endregion

        #region Public Properties

        public override AutoCatType AutoCatType => AutoCatType.VrSupport;

        public VRSupport IncludedVRSupportFlags
        {
            get => _includedVrSupportFlags ?? (_includedVrSupportFlags = new VRSupport());
            set => _includedVrSupportFlags = value;
        }

        // AutoCat configuration
        public string Prefix { get; set; }

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public static AutoCatVrSupport LoadFromXmlElement(XmlElement xElement)
        {
            string name = XmlUtil.GetStringFromNode(xElement[XmlNameName], TypeIdString);
            string filter = XmlUtil.GetStringFromNode(xElement[XmlNameFilter], null);
            string prefix = XmlUtil.GetStringFromNode(xElement[XmlNamePrefix], null);
            List<string> headsetsList = new List<string>();
            List<string> inputList = new List<string>();
            List<string> playAreaList = new List<string>();

            XmlElement headset = xElement[XmlNameHeadsetsList];
            XmlElement input = xElement[XmlNameInputList];
            XmlElement playArea = xElement[XmlNamePlayAreaList];

            XmlNodeList headsetElements = headset?.SelectNodes(XmlNameFlag);
            if (headsetElements != null)
            {
                for (int i = 0; i < headsetElements.Count; i++)
                {
                    XmlNode n = headsetElements[i];
                    if (XmlUtil.TryGetStringFromNode(n, out string flag))
                    {
                        headsetsList.Add(flag);
                    }
                }
            }

            XmlNodeList inputElements = input?.SelectNodes(XmlNameFlag);
            if (inputElements != null)
            {
                for (int i = 0; i < inputElements.Count; i++)
                {
                    XmlNode n = inputElements[i];
                    if (XmlUtil.TryGetStringFromNode(n, out string flag))
                    {
                        inputList.Add(flag);
                    }
                }
            }

            XmlNodeList playAreaElements = playArea?.SelectNodes(XmlNameFlag);
            if (playAreaElements != null)
            {
                for (int i = 0; i < playAreaElements.Count; i++)
                {
                    XmlNode n = playAreaElements[i];
                    if (XmlUtil.TryGetStringFromNode(n, out string flag))
                    {
                        playAreaList.Add(flag);
                    }
                }
            }

            return new AutoCatVrSupport(name, filter, prefix, headsetsList, inputList, playAreaList);
        }

        public override AutoCatResult CategorizeGame(GameInfo game, Filter filter)
        {
            if (games == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GamelistNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameList);
            }

            if (db == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_DBNull);
                throw new ApplicationException(GlobalStrings.AutoCatGenre_Exception_NoGameDB);
            }

            if (game == null)
            {
                Logger.Error(GlobalStrings.Log_AutoCat_GameNull);
                return AutoCatResult.Failure;
            }

            if (!db.Contains(game.Id, out DatabaseEntry entry) || entry.LastStoreScrape == 0)
            {
                return AutoCatResult.NotInDatabase;
            }

            if (!game.IncludeGame(filter))
            {
                return AutoCatResult.Filtered;
            }

            VRSupport vrSupport = entry.VRSupport;

            vrSupport.Headsets = vrSupport.Headsets ?? new List<string>();
            vrSupport.Input = vrSupport.Input ?? new List<string>();
            vrSupport.PlayArea = vrSupport.PlayArea ?? new List<string>();

            IEnumerable<string> headsets = vrSupport.Headsets.Intersect(IncludedVRSupportFlags.Headsets);
            IEnumerable<string> input = vrSupport.Input.Intersect(IncludedVRSupportFlags.Input);
            IEnumerable<string> playArea = vrSupport.PlayArea.Intersect(IncludedVRSupportFlags.PlayArea);

            foreach (string catString in headsets)
            {
                Category c = games.GetCategory(GetProcessedString(catString));
                game.AddCategory(c);
            }

            foreach (string catString in input)
            {
                Category c = games.GetCategory(GetProcessedString(catString));
                game.AddCategory(c);
            }

            foreach (string catString in playArea)
            {
                Category c = games.GetCategory(GetProcessedString(catString));
                game.AddCategory(c);
            }

            return AutoCatResult.Success;
        }

        public override AutoCat Clone()
        {
            return new AutoCatVrSupport(this);
        }

        public override void WriteToXml(XmlWriter writer)
        {
            writer.WriteStartElement(TypeIdString);

            writer.WriteElementString(XmlNameName, Name);
            if (Filter != null)
            {
                writer.WriteElementString(XmlNameFilter, Filter);
            }

            if (Prefix != null)
            {
                writer.WriteElementString(XmlNamePrefix, Prefix);
            }

            writer.WriteStartElement(XmlNameHeadsetsList);

            foreach (string s in IncludedVRSupportFlags.Headsets)
            {
                writer.WriteElementString(XmlNameFlag, s);
            }

            writer.WriteEndElement(); // VR Headsets list

            writer.WriteStartElement(XmlNameInputList);

            foreach (string s in IncludedVRSupportFlags.Input)
            {
                writer.WriteElementString(XmlNameFlag, s);
            }

            writer.WriteEndElement(); // VR Input list

            writer.WriteStartElement(XmlNamePlayAreaList);

            foreach (string s in IncludedVRSupportFlags.PlayArea)
            {
                writer.WriteElementString(XmlNameFlag, s);
            }

            writer.WriteEndElement(); // VR Play Area list
            writer.WriteEndElement(); // type ID string
        }

        #endregion

        #region Methods

        private string GetProcessedString(string baseString)
        {
            if (string.IsNullOrEmpty(Prefix))
            {
                return baseString;
            }

            return Prefix + baseString;
        }

        #endregion
    }
}
