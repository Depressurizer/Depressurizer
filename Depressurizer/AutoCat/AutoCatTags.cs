/*
This file is part of Depressurizer.
Copyright 2011, 2012, 2013 Steve Labbe.

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
using Rallion;
using System;
using System.Collections.Generic;
using System.Xml;

namespace Depressurizer {

public class AutoCatTags : AutoCat {

        public override AutoCatType AutoCatType {
            get { return AutoCatType.Tags; }
        }

        public string Prefix { get; set; }
        public int MaxTags { get; set; }
        public HashSet<string> IncludedTags { get; set; }

        public bool ListOwnedOnly { get; set; }
        public float ListWeightFactor { get; set; }
        public int ListMinScore { get; set; }
        public int ListTagsPerGame { get; set; }
        public bool ListScoreSort { get; set; }
        public bool ListExcludeGenres { get; set; }

        public const string TypeIdString = "AutoCatTags";
        private const string XmlName_Name = "Name",
            XmlName_Prefix = "Prefix",
            XmlName_TagList = "Tags",
            XmlName_Tag = "Tag",
            XmlName_MaxTags = "MaxTags",
            XmlName_ListOwnedOnly = "List_OwnedOnly",
            XmlName_ListWeightFactor = "List_WeightedScore",
            XmlName_ListMinScore = "List_MinScore",
            XmlName_ListTagsPerGame = "List_TagsPerGame",
            XmlName_ListExcludeGenres = "List_ExcludeGenres",
            XmlName_ListScoreSort = "List_ScoreSort";

        public AutoCatTags( string name, string prefix = "",
            HashSet<string> tags = null, int maxTags = 0,
            bool listOwnedOnly = true, float listWeightFactor = 1, int listMinScore = 0, int listTagsPerGame = 0, bool listScoreSort = true, bool listExcludeGenres = true )
            : base( name ) {
            this.Prefix = prefix;

            if( tags == null ) IncludedTags = new HashSet<string>();
            else IncludedTags = tags;

            this.MaxTags = maxTags;
            this.ListOwnedOnly = listOwnedOnly;
            this.ListWeightFactor = listWeightFactor;
            this.ListMinScore = listMinScore;
            this.ListTagsPerGame = listTagsPerGame;
            this.ListScoreSort = listScoreSort;
            this.ListExcludeGenres = listExcludeGenres;
        }

        protected AutoCatTags( AutoCatTags other )
            : base( other ) {
            this.Prefix = other.Prefix;
            this.IncludedTags = new HashSet<string>( other.IncludedTags );
            this.MaxTags = other.MaxTags;

            this.ListOwnedOnly = other.ListOwnedOnly;
            this.ListWeightFactor = other.ListWeightFactor;
            this.ListMinScore = other.ListMinScore;
            this.ListTagsPerGame = other.ListTagsPerGame;
            this.ListScoreSort = other.ListScoreSort;
            this.ListExcludeGenres = other.ListExcludeGenres;
        }

        public override AutoCat Clone() {
            return new AutoCatTags( this );
        }

        public override AutoCatResult CategorizeGame( GameInfo game ) {
            if( games == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_GamelistNull );
                throw new ApplicationException( GlobalStrings.AutoCatGenre_Exception_NoGameList );
            }
            if( db == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_DBNull );
                throw new ApplicationException( GlobalStrings.AutoCatGenre_Exception_NoGameDB );
            }
            if( game == null ) {
                Program.Logger.Write( LoggerLevel.Error, GlobalStrings.Log_AutoCat_GameNull );
                return AutoCatResult.Failure;
            }

            if( !db.Contains( game.Id ) || db.Games[game.Id].LastStoreScrape == 0 ) return AutoCatResult.NotInDatabase;

            List<string> gameTags = db.GetTagList( game.Id );

            if( gameTags != null ) {
                int added = 0;
                for( int index = 0; index < gameTags.Count && ( MaxTags == 0 || added < MaxTags ); index++ ) {
                    if( IncludedTags.Contains( gameTags[index] ) ) {
                        game.AddCategory( games.GetCategory( GetProcessedString( gameTags[index] ) ) );
                        added++;
                    }
                }
            }

            return AutoCatResult.Success;
        }

        public string GetProcessedString( string s ) {
            if( string.IsNullOrEmpty( Prefix ) ) {
                return s;
            } else {
                return Prefix + s;
            }
        }

        public override void WriteToXml( XmlWriter writer ) {
            writer.WriteStartElement( TypeIdString );

            writer.WriteElementString( XmlName_Name, Name );
            if( !string.IsNullOrEmpty( Prefix ) ) writer.WriteElementString( XmlName_Prefix, Prefix );
            writer.WriteElementString( XmlName_MaxTags, MaxTags.ToString() );

            if( IncludedTags != null && IncludedTags.Count > 0 ) {
                writer.WriteStartElement( XmlName_TagList );
                foreach( string s in IncludedTags ) {
                    writer.WriteElementString( XmlName_Tag, s );
                }
                writer.WriteEndElement();
            }

            writer.WriteElementString( XmlName_ListOwnedOnly, ListOwnedOnly.ToString() );
            writer.WriteElementString( XmlName_ListWeightFactor, ListWeightFactor.ToString() );
            writer.WriteElementString( XmlName_ListMinScore, ListMinScore.ToString() );
            writer.WriteElementString( XmlName_ListTagsPerGame, ListTagsPerGame.ToString() );
            writer.WriteElementString( XmlName_ListScoreSort, ListScoreSort.ToString() );
            writer.WriteElementString( XmlName_ListExcludeGenres, ListExcludeGenres.ToString() );

            writer.WriteEndElement();
        }

        public static AutoCatTags LoadFromXmlElement( XmlElement xElement ) {
            string name = XmlUtil.GetStringFromNode( xElement[XmlName_Name], TypeIdString );

            AutoCatTags result = new AutoCatTags( name );

            string prefix;
            if( XmlUtil.TryGetStringFromNode( xElement[XmlName_Prefix], out prefix ) ) result.Prefix = prefix;

            int maxTags;
            if( XmlUtil.TryGetIntFromNode( xElement[XmlName_MaxTags], out maxTags ) ) result.MaxTags = maxTags;

            bool listOwnedOnly;
            if( XmlUtil.TryGetBoolFromNode( xElement[XmlName_ListOwnedOnly], out listOwnedOnly ) ) result.ListOwnedOnly = listOwnedOnly;

            float listWeightFactor;
            if( XmlUtil.TryGetFloatFromNode( xElement[XmlName_ListWeightFactor], out listWeightFactor ) ) result.ListWeightFactor = listWeightFactor;

            int listMinScore;
            if( XmlUtil.TryGetIntFromNode( xElement[XmlName_ListMinScore], out listMinScore ) ) result.ListMinScore = listMinScore;

            int listTagsPerGame;
            if( XmlUtil.TryGetIntFromNode( xElement[XmlName_ListTagsPerGame], out listTagsPerGame ) ) result.ListTagsPerGame = listTagsPerGame;

            bool listScoreSort;
            if( XmlUtil.TryGetBoolFromNode( xElement[XmlName_ListScoreSort], out listScoreSort ) ) result.ListScoreSort = listScoreSort;

            bool listExcludeGenres;
            if( XmlUtil.TryGetBoolFromNode( xElement[XmlName_ListExcludeGenres], out listExcludeGenres ) ) result.ListExcludeGenres = listExcludeGenres;

            List<string> tagList = XmlUtil.GetStringsFromNodeList( xElement.SelectNodes( XmlName_TagList + "/" + XmlName_Tag ) );
            result.IncludedTags = ( tagList == null ) ? new HashSet<string>() : new HashSet<string>( tagList );

            return result;
        }
    }

}