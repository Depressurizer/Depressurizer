using System;
using System.Diagnostics;
using System.Reflection;
using System.Windows.Forms;

namespace Depressurizer.Dialogs
{
	public partial class AboutDialog : Form
	{
		#region Constructors and Destructors

		public AboutDialog()
		{
			InitializeComponent();
		}

		#endregion

		#region Methods

		private void AboutDialog_Load(object sender, EventArgs e)
		{
			Version version = Assembly.GetExecutingAssembly().GetName().Version;

			// ReSharper disable once LocalizableElement
			LabelVersion.Text = $"Depressurizer v{version.Major}.{version.Minor}.{version.Build}";
		}

		private void ButtonClose_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void LinkHomePage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"https://github.com/mvegter/Depressurizer");
		}

		private void LinkLicense_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
		{
			Process.Start(@"https://www.gnu.org/licenses/gpl-3.0.en.html");
		}

		#endregion
	}
}
