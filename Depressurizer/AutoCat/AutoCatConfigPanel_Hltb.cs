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
    public partial class AutoCatConfigPanel_Hltb : AutoCatConfigPanel {

        BindingList<Hltb_Rule> ruleList = new BindingList<Hltb_Rule>();
        BindingSource binding = new BindingSource();

        public AutoCatConfigPanel_Hltb() {
            InitializeComponent();

            //initialize combobox
            this.cmbTimeType.Items.AddRange(new object[] {
            TimeType.Main,
            TimeType.Extras,
            TimeType.Completionist});
            cmbTimeType.SelectedItem = TimeType.Main;

            numRuleMinTime.DecimalPlaces = 1;
            numRuleMaxTime.DecimalPlaces = 1;

            // Set up help tooltips
            ttHelp.Ext_SetToolTip( helpRules, GlobalStrings.AutoCatUserScore_Help_Rules );
            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
            ttHelp.Ext_SetToolTip(helpUnknown, GlobalStrings.AutocatHltb_Help_Unknown);

            // Set up bindings.
            // None of these strings should be localized.
            binding.DataSource = ruleList;

            lstRules.DisplayMember = "Name";
            lstRules.DataSource = binding;

            txtRuleName.DataBindings.Add( "Text", binding, "Name" );
            numRuleMinTime.DataBindings.Add( "Value", binding, "MinHours" );
            numRuleMaxTime.DataBindings.Add( "Value", binding, "MaxHours" );
            cmbTimeType.DataBindings.Add("SelectedItem", binding, "TimeType");

            UpdateEnabledSettings();
        }

        public override void SaveToAutoCat( AutoCat ac ) {
            AutoCatHltb acHltb = ac as AutoCatHltb;
            if( acHltb == null ) return;

            acHltb.Prefix = txtPrefix.Text;
            acHltb.IncludeUnknown = chkIncludeUnknown.Checked;
            acHltb.UnknownText = txtUnknownText.Text;
            acHltb.Rules = new List<Hltb_Rule>( ruleList );
        }

        public override void LoadFromAutoCat( AutoCat ac ) {
            AutoCatHltb acHltb = ac as AutoCatHltb;
            if( acHltb == null ) return;

            txtPrefix.Text = acHltb.Prefix;
            chkIncludeUnknown.Checked = acHltb.IncludeUnknown;
            txtUnknownText.Text = (acHltb.UnknownText == null) ? string.Empty : acHltb.UnknownText;
            acHltb.IncludeUnknown = chkIncludeUnknown.Checked;
            acHltb.UnknownText = txtUnknownText.Text;

            ruleList.Clear();
            foreach( Hltb_Rule rule in acHltb.Rules ) {
                ruleList.Add( new Hltb_Rule( rule ) );
            }
            UpdateEnabledSettings();
        }

        /// <summary>
        /// Updates enabled states of all form elements that depend on the rule selection.
        /// </summary>
        private void UpdateEnabledSettings() {
            bool ruleSelected = ( lstRules.SelectedIndex >= 0 );

            txtRuleName.Enabled =
                numRuleMaxTime.Enabled = numRuleMinTime.Enabled = 
                cmbTimeType.Enabled =
                cmdRuleRemove.Enabled = ruleSelected;
            cmdRuleUp.Enabled = ruleSelected && lstRules.SelectedIndex != 0;
            cmdRuleDown.Enabled = ruleSelected = ruleSelected && lstRules.SelectedIndex != lstRules.Items.Count - 1;
        }

        /// <summary>
        /// Moves the specified rule a certain number of spots up or down in the list. Does nothing if the spot would be off the list.
        /// </summary>
        /// <param name="mainIndex">Index of the rule to move.</param>
        /// <param name="offset">Number of spots to move the rule. Negative moves up, positive moves down.</param>
        /// <param name="selectMoved">If true, select the moved element afterwards</param>
        private void MoveItem( int mainIndex, int offset, bool selectMoved ) {
            int alterIndex = mainIndex + offset;
            if( mainIndex < 0 || mainIndex >= lstRules.Items.Count || alterIndex < 0 || alterIndex >= lstRules.Items.Count ) return;

            Hltb_Rule mainItem = ruleList[mainIndex];
            ruleList[mainIndex] = ruleList[alterIndex];
            ruleList[alterIndex] = mainItem;
            if( selectMoved ) lstRules.SelectedIndex = alterIndex;
        }

        /// <summary>
        /// Adds a new rule to the end of the list and selects it.
        /// </summary>
        private void AddRule() {
            Hltb_Rule newRule = new Hltb_Rule(GlobalStrings.AutoCatUserScore_NewRuleName, 0, 0, (TimeType)cmbTimeType.SelectedItem);
            ruleList.Add( newRule );
            lstRules.SelectedIndex = lstRules.Items.Count - 1;
        }

        /// <summary>
        /// Removes the rule at the given index
        /// </summary>
        /// <param name="index">Index of the rule to remove</param>
        private void RemoveRule( int index ) {
            if( index >= 0 ) {
                ruleList.RemoveAt( index );
            }
        }

        #region Event Handlers
        private void lstRules_SelectedIndexChanged( object sender, EventArgs e ) {
            UpdateEnabledSettings();
        }

        private void cmdRuleAdd_Click( object sender, EventArgs e ) {
            AddRule();
        }

        private void cmdRuleRemove_Click( object sender, EventArgs e ) {
            RemoveRule( lstRules.SelectedIndex );
        }

        private void cmdRuleUp_Click( object sender, EventArgs e ) {
            MoveItem( lstRules.SelectedIndex, -1, true );
        }

        private void cmdRuleDown_Click( object sender, EventArgs e ) {
            MoveItem( lstRules.SelectedIndex, 1, true );
        }
        #endregion

    }
}
