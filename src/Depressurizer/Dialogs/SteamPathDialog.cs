using System;
using System.IO;
using System.Windows.Forms;
using Depressurizer.Properties;
using Microsoft.Win32;

namespace Depressurizer.Dialogs
{
	public partial class SteamPathDialog : Form
	{
		#region Constructors and Destructors

		public SteamPathDialog()
		{
			InitializeComponent();
		}

		#endregion

		#region Public Properties

		public string Path => LabelPath.Text;

		#endregion

		#region Methods

		private static DialogResult ShowWarning(string text)
		{
			return MessageBox.Show(text, Resources.Warning, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
		}

		private void ButtonBrowse_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				DialogResult result = dialog.ShowDialog();
				if (result == DialogResult.OK)
				{
					LabelPath.Text = dialog.SelectedPath;
				}
			}
		}

		private void ButtonOk_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(Path))
			{
				MessageBox.Show(Resources.SteamPathDialog_PathEmpty, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (!Directory.Exists(Path))
			{
				DialogResult result = ShowWarning(Resources.SteamPathDialog_PathDoesntExist);
				if (result == DialogResult.No)
				{
					return;
				}
			}

			string path = System.IO.Path.Combine(Path, "appcache", "appinfo.vdf");
			if (!File.Exists(path))
			{
				DialogResult result = ShowWarning(Resources.SteamPathDialog_PathDoesntContain);
				if (result == DialogResult.No)
				{
					return;
				}
			}

			Close();
		}

		private void SteamPathDialog_Load(object sender, EventArgs e)
		{
			string regPath = Environment.Is64BitOperatingSystem ? @"SOFTWARE\Wow6432Node\Valve\Steam" : @"SOFTWARE\Valve\Steam";

			try
			{
				using (RegistryKey key = Registry.LocalMachine.OpenSubKey(regPath))
				{
					if (key == null)
					{
						return;
					}

					object value = key.GetValue("InstallPath");
					if (value != null)
					{
						LabelPath.Text = (string) value;
					}
				}
			}
			catch
			{
				// ignored
			}
		}

		#endregion
	}
}
