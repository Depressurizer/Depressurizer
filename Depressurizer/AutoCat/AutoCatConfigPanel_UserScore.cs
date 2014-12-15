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

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace Depressurizer {
    public partial class AutoCatConfigPanel_UserScore : AutoCatConfigPanel {

        BindingList<UserScore_Rule> ruleList = new BindingList<UserScore_Rule>();
        BindingSource binding = new BindingSource();

        public AutoCatConfigPanel_UserScore() {
            InitializeComponent();

            ttHelp.Ext_SetToolTip( helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );

            binding.DataSource = ruleList;

            lstRules.DisplayMember = "Name";
            lstRules.DataSource = binding;

            txtRuleName.DataBindings.Add( "Text", binding, "Name" );
            numRuleMinScore.DataBindings.Add( "Value", binding, "MinScore" );
            numRuleMaxScore.DataBindings.Add( "Value", binding, "MaxScore" );
            numRuleMinReviews.DataBindings.Add( "Value", binding, "MinReviews" );
            numRuleMaxReviews.DataBindings.Add( "Value", binding, "MaxReviews" );

            UpdateEnabledSettings();
        }

        public override void SaveToAutoCat( AutoCat ac ) {
            AutoCatUserScore acScore = ac as AutoCatUserScore;
            if( ac == null ) return;

            acScore.Prefix = txtPrefix.Text;
            acScore.Rules = new List<UserScore_Rule>( ruleList );
        }

        public override void LoadFromAutoCat( AutoCat ac ) {
            AutoCatUserScore acScore = ac as AutoCatUserScore;
            if( ac == null ) return;

            txtPrefix.Text = acScore.Prefix;

            ruleList.Clear();
            foreach( UserScore_Rule rule in acScore.Rules ) {
                ruleList.Add( new UserScore_Rule( rule ) );
            }
            UpdateEnabledSettings();
        }

        private void UpdateEnabledSettings() {
            bool ruleSelected = ( lstRules.SelectedIndex >= 0 );

            txtRuleName.Enabled = 
                numRuleMaxScore.Enabled = numRuleMinScore.Enabled = 
                numRuleMinReviews.Enabled = numRuleMaxReviews.Enabled = 
                cmdRuleRemove.Enabled = ruleSelected;
            cmdRuleUp.Enabled = ruleSelected && lstRules.SelectedIndex != 0;
            cmdRuleDown.Enabled = ruleSelected = ruleSelected && lstRules.SelectedIndex != lstRules.Items.Count - 1;
        }

        private void lstRules_SelectedIndexChanged( object sender, EventArgs e ) {
            UpdateEnabledSettings();
        }

        private void cmdRuleAdd_Click( object sender, EventArgs e ) {
            UserScore_Rule newRule = new UserScore_Rule( "New Rule", 0, 100, 0, 0 );
            ruleList.Add( newRule );
            lstRules.SelectedIndex = lstRules.Items.Count - 1;
        }

        private void cmdRuleRemove_Click( object sender, EventArgs e ) {
            if( lstRules.SelectedIndex >= 0 ) {
                ruleList.RemoveAt( lstRules.SelectedIndex );
            }
        }

        private void MoveItem( int mainIndex, int offset ) {
            int alterIndex = mainIndex + offset;
            if( mainIndex < 0 || mainIndex >= lstRules.Items.Count || alterIndex < 0 || alterIndex >= lstRules.Items.Count ) return;

            UserScore_Rule mainItem = ruleList[mainIndex];
            ruleList[mainIndex] = ruleList[alterIndex];
            ruleList[alterIndex] = mainItem;

            lstRules.SelectedIndex = alterIndex;
        }

        private void cmdRuleUp_Click( object sender, EventArgs e ) {
            MoveItem( lstRules.SelectedIndex, -1 );
        }

        private void cmdRuleDown_Click( object sender, EventArgs e ) {
            MoveItem( lstRules.SelectedIndex, 1 );
        }
    }
}
