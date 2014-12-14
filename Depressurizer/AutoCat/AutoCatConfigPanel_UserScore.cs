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
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Rallion;

namespace Depressurizer {
    public partial class AutoCatConfigPanel_UserScore : AutoCatConfigPanel {

        BindingList<UserScore_Rule> ruleList = new BindingList<UserScore_Rule>();

        public AutoCatConfigPanel_UserScore() {
            InitializeComponent();

            ttHelp.Ext_SetToolTip( helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix );

            lstRules.DisplayMember = "Name";
            lstRules.DataSource = ruleList;

            txtRuleName.DataBindings.Add( "Text", ruleList, "Name" );
            numRuleMinScore.DataBindings.Add( "Value", ruleList, "Min" );
            numRuleMaxScore.DataBindings.Add( "Value", ruleList, "Max" );

            UpdateEnabledSettings();
        }

        public override void SaveToAutoCat( AutoCat ac ) {
            AutoCatUserScore acScore = ac as AutoCatUserScore;
            if( ac == null ) return;

            acScore.Prefix = txtPrefix.Name;
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

            txtRuleName.Enabled = numRuleMaxScore.Enabled = numRuleMinScore.Enabled = ruleSelected;
            cmdRuleRemove.Enabled = cmdRuleUp.Enabled = cmdRuleDown.Enabled = ruleSelected;
        }

        private void lstRules_SelectedIndexChanged( object sender, EventArgs e ) {
            if( ruleList.Count > 0 ) {
                ruleList.ResetBindings();
            }
            UpdateEnabledSettings();
        }

        private void cmdRuleAdd_Click( object sender, EventArgs e ) {
            UserScore_Rule newRule = new UserScore_Rule( "New Rule", 0, 100 );
            ruleList.Add( newRule );
        }

        private void cmdRuleRemove_Click( object sender, EventArgs e ) {
            if( lstRules.SelectedIndex >= 0 ) {
                ruleList.RemoveAt( lstRules.SelectedIndex );
            }
        }


    }
}
