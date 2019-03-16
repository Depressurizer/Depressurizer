﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;
using Depressurizer.Core.AutoCats;
using Depressurizer.Core.Enums;

namespace Depressurizer.AutoCats
{
    public partial class AutoCatConfigPanel_Hltb : AutoCatConfigPanel
    {
        #region Fields

        private readonly BindingSource binding = new BindingSource();

        private readonly BindingList<HowLongToBeatRule> ruleList = new BindingList<HowLongToBeatRule>();

        #endregion

        #region Constructors and Destructors

        public AutoCatConfigPanel_Hltb()
        {
            InitializeComponent();

            //initialize combobox
            cmbTimeType.Items.AddRange(new object[]
            {
                TimeType.Main,
                TimeType.Extras,
                TimeType.Completionist
            });
            cmbTimeType.SelectedItem = TimeType.Main;

            numRuleMinTime.DecimalPlaces = 1;
            numRuleMaxTime.DecimalPlaces = 1;

            // Set up help tooltips
            ttHelp.Ext_SetToolTip(helpRules, GlobalStrings.AutoCatUserScore_Help_Rules);
            ttHelp.Ext_SetToolTip(helpPrefix, GlobalStrings.DlgAutoCat_Help_Prefix);
            ttHelp.Ext_SetToolTip(helpUnknown, GlobalStrings.AutocatHltb_Help_Unknown);

            // Set up bindings.
            // None of these strings should be localized.
            binding.DataSource = ruleList;

            lstRules.DisplayMember = "Name";
            lstRules.DataSource = binding;

            txtRuleName.DataBindings.Add("Text", binding, "Name");
            numRuleMinTime.DataBindings.Add("Value", binding, "MinHours");
            numRuleMaxTime.DataBindings.Add("Value", binding, "MaxHours");
            cmbTimeType.DataBindings.Add("SelectedItem", binding, "TimeType");

            UpdateEnabledSettings();
        }

        #endregion

        #region Public Methods and Operators

        public override void LoadFromAutoCat(AutoCat autoCat)
        {
            AutoCatHltb acHltb = autoCat as AutoCatHltb;
            if (acHltb == null)
            {
                return;
            }

            txtPrefix.Text = acHltb.Prefix;
            chkIncludeUnknown.Checked = acHltb.IncludeUnknown;
            txtUnknownText.Text = acHltb.UnknownText == null ? string.Empty : acHltb.UnknownText;
            acHltb.IncludeUnknown = chkIncludeUnknown.Checked;
            acHltb.UnknownText = txtUnknownText.Text;

            ruleList.Clear();
            foreach (HowLongToBeatRule rule in acHltb.Rules)
            {
                ruleList.Add(new HowLongToBeatRule(rule));
            }

            UpdateEnabledSettings();
        }

        public override void SaveToAutoCat(AutoCat autoCat)
        {
            AutoCatHltb acHltb = autoCat as AutoCatHltb;
            if (acHltb == null)
            {
                return;
            }

            acHltb.Prefix = txtPrefix.Text;
            acHltb.IncludeUnknown = chkIncludeUnknown.Checked;
            acHltb.UnknownText = txtUnknownText.Text;
            acHltb.Rules = new List<HowLongToBeatRule>(ruleList);
        }

        #endregion

        #region Methods

        /// <summary>
        ///     Adds a new rule to the end of the list and selects it.
        /// </summary>
        private void AddRule()
        {
            HowLongToBeatRule newRule = new HowLongToBeatRule(GlobalStrings.AutoCatUserScore_NewRuleName, 0, 0, (TimeType) cmbTimeType.SelectedItem);
            ruleList.Add(newRule);
            lstRules.SelectedIndex = lstRules.Items.Count - 1;
        }

        private void cmdRuleAdd_Click(object sender, EventArgs e)
        {
            AddRule();
        }

        private void cmdRuleDown_Click(object sender, EventArgs e)
        {
            MoveItem(lstRules.SelectedIndex, 1, true);
        }

        private void cmdRuleRemove_Click(object sender, EventArgs e)
        {
            RemoveRule(lstRules.SelectedIndex);
        }

        private void cmdRuleUp_Click(object sender, EventArgs e)
        {
            MoveItem(lstRules.SelectedIndex, -1, true);
        }

        private void lstRules_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdateEnabledSettings();
        }

        /// <summary>
        ///     Moves the specified rule a certain number of spots up or down in the list. Does nothing if the spot would be off
        ///     the list.
        /// </summary>
        /// <param name="mainIndex">Index of the rule to move.</param>
        /// <param name="offset">Number of spots to move the rule. Negative moves up, positive moves down.</param>
        /// <param name="selectMoved">If true, select the moved element afterwards</param>
        private void MoveItem(int mainIndex, int offset, bool selectMoved)
        {
            int alterIndex = mainIndex + offset;
            if (mainIndex < 0 || mainIndex >= lstRules.Items.Count || alterIndex < 0 || alterIndex >= lstRules.Items.Count)
            {
                return;
            }

            HowLongToBeatRule mainItem = ruleList[mainIndex];
            ruleList[mainIndex] = ruleList[alterIndex];
            ruleList[alterIndex] = mainItem;
            if (selectMoved)
            {
                lstRules.SelectedIndex = alterIndex;
            }
        }

        /// <summary>
        ///     Removes the rule at the given index
        /// </summary>
        /// <param name="index">Index of the rule to remove</param>
        private void RemoveRule(int index)
        {
            if (index >= 0)
            {
                ruleList.RemoveAt(index);
            }
        }

        /// <summary>
        ///     Updates enabled states of all form elements that depend on the rule selection.
        /// </summary>
        private void UpdateEnabledSettings()
        {
            bool ruleSelected = lstRules.SelectedIndex >= 0;

            txtRuleName.Enabled = numRuleMaxTime.Enabled = numRuleMinTime.Enabled = cmbTimeType.Enabled = cmdRuleRemove.Enabled = ruleSelected;
            cmdRuleUp.Enabled = ruleSelected && lstRules.SelectedIndex != 0;
            cmdRuleDown.Enabled = ruleSelected && lstRules.SelectedIndex != lstRules.Items.Count - 1;
        }

        #endregion
    }
}
