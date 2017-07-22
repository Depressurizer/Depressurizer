/*
    This file is part of Depressurizer.
    Original work Copyright 2011, 2012, 2013 Steve Labbe.
    Modified work Copyright 2017 Martijn Vegter.

    Depressurizer is free software: you can redistribute it and/or modify
    it under the terms of the GNU General Public License as published by
    the Free Software Foundation, either version 3 of the License, or
    (at your option) any later version.

    Depressurizer is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
    GNU General Public License for more details.

    You should have received a copy of the GNU General Public License
    along with Depressurizer.  If not, see <http://www.gnu.org/licenses/>.
*/

using System;
using System.ComponentModel;

namespace Depressurizer
{
    [Flags]
    public enum AppPlatforms
    {
        None = 0,
        Windows = 1,
        Mac = 1 << 1,
        Linux = 1 << 2,
        All = Windows | Mac | Linux
    }

    [Flags]
    public enum AppTypes
    {
        Application = 1,
        Demo = 1 << 1,
        DLC = 1 << 2,
        Game = 1 << 3,
        Media = 1 << 4,
        Tool = 1 << 5,
        Other = 1 << 6,
        Unknown = 1 << 7,
        InclusionNormal = Application | Game,
        InclusionUnknown = InclusionNormal | Unknown,
        InclusionAll = (1 << 8) - 1
    }

    public enum StartupAction
    {
        None,
        Load,
        Create
    }

    public enum GameListSource
    {
        XmlPreferred,
        XmlOnly,
        WebsiteOnly
    }

    public enum UILanguage
    {
        windows,
        en, // English
        es, // Spanish
        ru, // Russian
        uk, // Ukranian
        nl // Dutch
    }

    public enum StoreLanguage
    {
        windows,
        bg, // Bulgarian
        cs, // Czech
        da, // Danish
        nl, // Dutch
        en, // English
        fi, // Finnish
        fr, // French
        de, // German
        el, // Greek
        hu, // Hungarian
        it, // Italian
        ja, // Japanese
        ko, // Korean
        no, // Norwegian
        pl, // Polish
        pt, // Portuguese
        pt_BR, // Portuguese (Brasil)
        ro, // Romanian
        ru, // Russian
        zh_Hans, // Simplified Chinese
        es, // Spanish
        sv, // Swedish
        th, // Thai
        zh_Hant, // Traditional Chinese
        tr, // Turkish
        uk // Ukrainian
    }

    public enum AutoCatType
    {
        [Description("None")] None,
        [Description("AutoCatGenre")] Genre,
        [Description("AutoCatFlags")] Flags,
        [Description("AutoCatTags")] Tags,
        [Description("AutoCatYear")] Year,
        [Description("AutoCatUserScore")] UserScore,
        [Description("AutoCatHltb")] Hltb,
        [Description("AutoCatManual")] Manual,
        [Description("AutoCatDevPub")] DevPub,
        [Description("AutoCatGroup")] Group,
        [Description("AutoCatName")] Name,
        [Description("AutoCatVrSupport")] VrSupport,
        [Description("AutoCatLanguage")] Language
    }

    public enum AutoCatResult
    {
        Success,
        Failure,
        NotInDatabase,
        Filtered
    }
}