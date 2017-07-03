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

namespace Depressurizer.Model
{
    public class UserScoreRule
    {
        public string Name { get; set; }
        public int MinScore { get; set; }
        public int MaxScore { get; set; }
        public int MinReviews { get; set; }
        public int MaxReviews { get; set; }

        public UserScoreRule(string name, int minScore, int maxScore, int minReviews, int maxReviews)
        {
            Name = name;
            MinScore = minScore;
            MaxScore = maxScore;
            MinReviews = minReviews;
            MaxReviews = maxReviews;
        }

        public UserScoreRule(UserScoreRule other)
        {
            Name = other.Name;
            MinScore = other.MinScore;
            MaxScore = other.MaxScore;
            MinReviews = other.MinReviews;
            MaxReviews = other.MaxReviews;
        }
    }
}