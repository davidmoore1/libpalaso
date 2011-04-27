﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Palaso.WritingSystems;

namespace Palaso.UI.WindowsForms.WritingSystems.WSIdentifiers
{
	public partial class ScriptRegionVariantView : UserControl, ISelectableIdentifierOptions
	{
		private bool _updatingFromModel;
		private readonly WritingSystemSetupModel _model;

		public ScriptRegionVariantView(WritingSystemSetupModel model)
		{
			_model = model;
			InitializeComponent();
			if (model != null)
			{
				model.SelectionChanged += UpdateDisplayFromModel;
			}
			_scriptCombo.Items.AddRange(StandardTags.ValidIso15924Scripts.ToArray());
			_scriptCombo.DisplayMember = "Label";
			_regionCombo.Items.AddRange(StandardTags.ValidIso3166Regions.ToArray());
			_regionCombo.DisplayMember = "Description";
		}

		private void UpdateDisplayFromModel(object sender, EventArgs e)
		{
			if (_model.CurrentDefinition != null)
			{
				_updatingFromModel = true;
				_regionCombo.SelectedItem = _model.CurrentRegion;
				_variant.Text=_model.CurrentVariant;
				_scriptCombo.SelectedItem = _model.CurrentIso15924Script;
				_updatingFromModel = false;
			}
		}

		private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{

		}

		public string ChoiceName
		{
			get { return "Script/Variant/Region"; }
		}

		private void _variant_TextChanged(object sender, EventArgs e)
		{
			if (_updatingFromModel)
				return;
			_model.CurrentVariant=_variant.Text;
		}

		private void ScriptCombo_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			if (_updatingFromModel)
				return;
			if (_scriptCombo.SelectedItem == null)
			{
				_model.CurrentScriptCode = string.Empty;
			}
			else
			{
				_model.CurrentScriptCode = ((Iso15924Script) _scriptCombo.SelectedItem).Code;
			}
		}

		private void RegionCombo_OnSelectedIndexChanged(object sender, EventArgs e)
		{
			if (_updatingFromModel)
				return;
			if (_regionCombo.SelectedItem == null)
			{
				_model.CurrentRegion = string.Empty;
			}
			else
			{
				_model.CurrentRegion = ((StandardTags.IanaSubtag)_regionCombo.SelectedItem).Subtag;
			}
		}

		#region Implementation of ISelectableIdentifierOptions

		public void Selected()
		{
			UpdateDisplayFromModel(null, null);
		}

		#endregion
	}
}
