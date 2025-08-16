using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using System.Xml;
using Depressurizer.AutoCats;
using Depressurizer.Core;
using Depressurizer.Core.Helpers;
using Depressurizer.Properties;

namespace Depressurizer
{
    public partial class DlgProfile : Form
    {
        #region Fields

        public Profile Profile;

        private readonly bool editMode;

        private int currentThreadCount;

        private ThreadLocker currentThreadLock = new ThreadLocker();

        #endregion

        #region Constructors and Destructors

        public DlgProfile()
        {
            InitializeComponent();
        }

        public DlgProfile(Profile profile) : this()
        {
            Profile = profile;
            editMode = true;
        }

        #endregion

        #region Delegates

        private delegate void SimpleDelegate();

        private delegate void UpdateDelegate(int i, string s);

        #endregion

        #region Public Properties

        public bool DownloadNow => chkActUpdate.Checked;

        public bool ImportNow => chkActImport.Checked;

        public bool SetStartup => chkSetStartup.Checked;

        #endregion

        #region Properties

        private static Logger Logger => Logger.Instance;

        #endregion

        #region Public Methods and Operators

        public string GetDisplayName(long accountId)
        {
            try
            {
                XmlDocument doc = new XmlDocument();
                HttpWebRequest req = (HttpWebRequest) WebRequest.Create(string.Format(CultureInfo.InvariantCulture, Constants.SteamProfile, accountId));
                using (WebResponse resp = req.GetResponse())
                {
                    doc.Load(resp.GetResponseStream());
                }

                XmlNode nameNode = doc.SelectSingleNode("profile/steamID");
                if (nameNode != null)
                {
                    return nameNode.InnerText;
                }
            }
            catch (Exception e)
            {
                Logger.Warn(GlobalStrings.DlgProfile_ExceptionRaisedWhenTryingScrapeProfileName, accountId);
                Logger.Warn(e.Message);
            }

            return null;
        }

        #endregion

        #region Methods

        private bool Apply()
        {
            if (radSelUserByURL.Checked)
            {
                using (CDlgGetSteamID dialog = new CDlgGetSteamID(txtUserUrl.Text))
                {
                    DialogResult result = dialog.ShowDialog();

                    if (result == DialogResult.Cancel)
                    {
                        return false;
                    }

                    if (dialog.Success == false || dialog.SteamID == 0)
                    {
                        MessageBox.Show(this, GlobalStrings.DlgProfile_CouldNotFindSteamProfile, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }

                    txtUserID.Text = dialog.SteamID.ToString();
                }
            }

            if (!editMode)
            {
                return CreateProfile();
            }

            if (!ValidateEntries())
            {
                return false;
            }

            SaveModifiables(Profile);

            return true;
        }

        private void cmdBrowse_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                try
                {
                    FileInfo f = new FileInfo(txtFilePath.Text);
                    dialog.InitialDirectory = f.DirectoryName;
                    dialog.FileName = f.Name;
                }
                catch (ArgumentException) { }

                dialog.DefaultExt = "profile";
                dialog.AddExtension = true;
                dialog.Filter = GlobalStrings.DlgProfile_Filter;
                DialogResult result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    txtFilePath.Text = dialog.FileName;
                }
            }
        }

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cmdIgnore_Click(object sender, EventArgs e)
        {
            if (int.TryParse(txtIgnore.Text, out int id))
            {
                lstIgnored.Items.Add(id.ToString());
                txtIgnore.ResetText();
                lstIgnored.Sort();
            }
            else
            {
                MessageBox.Show(GlobalStrings.DlgGameDBEntry_IDMustBeInteger, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void cmdOk_Click(object sender, EventArgs e)
        {
            if (Apply())
            {
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void cmdUnignore_Click(object sender, EventArgs e)
        {
            while (lstIgnored.SelectedIndices.Count > 0)
            {
                lstIgnored.Items.RemoveAt(lstIgnored.SelectedIndices[0]);
            }
        }

        private void cmdUserUpdate_Click(object sender, EventArgs e)
        {
            StartThreadedNameUpdate();
        }

        private void cmdUserUpdateCancel_Click(object sender, EventArgs e)
        {
            if (currentThreadCount > 0)
            {
                lock (currentThreadLock)
                {
                    currentThreadLock.Aborted = true;
                }

                SetUpdateInterfaceStopping();
            }
        }

        private bool CreateProfile()
        {
            if (!ValidateEntries())
            {
                return false;
            }

            FileInfo file;
            try
            {
                file = new FileInfo(txtFilePath.Text);
            }
            catch
            {
                MessageBox.Show(GlobalStrings.DlgProfile_YouMustEnterValidProfilePath, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!file.Directory.Exists)
            {
                try
                {
                    file.Directory.Create();
                }
                catch
                {
                    MessageBox.Show(GlobalStrings.DlgProfile_FailedToCreateParentDirectory, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            Profile profile = new Profile();

            SaveModifiables(profile);
            AutoCat.GenerateDefaultAutoCatSet(profile.AutoCats);

            try
            {
                profile.Save(file.FullName);
            }
            catch (ApplicationException e)
            {
                MessageBox.Show(e.Message, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Profile = profile;
            return true;
        }

        /// <summary>
        ///     Gets a list of located account ids. Uses settings for the steam path.
        /// </summary>
        /// <returns>An array of located IDs</returns>
        private string[] GetSteamIds()
        {
            try
            {
                DirectoryInfo dir = new DirectoryInfo(Settings.Instance.SteamPath + "\\userdata");
                if (dir.Exists)
                {
                    DirectoryInfo[] userDirs = dir.GetDirectories();
                    string[] result = new string[userDirs.Length];
                    for (int i = 0; i < userDirs.Length; i++)
                    {
                        result[i] = userDirs[i].Name;
                    }

                    return result;
                }

                return new string[0];
            }
            catch
            {
                return new string[0];
            }
        }

        private void InitializeEditMode()
        {
            txtFilePath.Text = Profile.FilePath;
            grpProfInfo.Enabled = false;

            textBoxSteamApi.Text = Profile.SteamWebApiKey;
            chkActUpdate.Checked = false;
            chkActImport.Checked = false;
            chkSetStartup.Checked = false;

            chkAutoUpdate.Checked = Profile.AutoUpdate;
            chkAutoImport.Checked = Profile.AutoImport;
            chkLocalUpdate.Checked = Profile.LocalUpdate;
            chkWebUpdate.Checked = Profile.WebUpdate;
            chkExportDiscard.Checked = Profile.ExportDiscard;
            chkIncludeShortcuts.Checked = Profile.IncludeShortcuts;
            chkOverwriteNames.Checked = Profile.OverwriteOnDownload;

            Text = GlobalStrings.DlgProfile_EditProfile;

            chkAutoIgnore.Checked = Profile.AutoIgnore;
            chkIncludeUnknown.Checked = Profile.IncludeUnknown;
            chkBypassIgnoreOnImport.Checked = Profile.BypassIgnoreOnImport;

            foreach (int i in Profile.IgnoreList)
            {
                lstIgnored.Items.Add(i.ToString());
            }

            lstIgnored.Sort();

            bool found = SelectUserInList(Profile.SteamID64);
            if (found)
            {
                radSelUserFromList.Checked = true;
            }
            else
            {
                radSelUserByID.Checked = true;
                txtUserID.Text = Profile.SteamID64.ToString();
            }
        }

        /// <summary>
        ///     Populates the combo box with all located account IDs
        /// </summary>
        private void LoadShortIds()
        {
            lstUsers.BeginUpdate();

            lstUsers.Items.Clear();

            string[] ids = GetSteamIds();

            foreach (string id in ids)
            {
                lstUsers.Items.Add(new UserRecord(id));
            }

            lstUsers.EndUpdate();
        }

        private void lstUsers_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstUsers.SelectedItem is UserRecord userRecord)
            {
                txtUserID.Text = Steam.ToSteamId64(userRecord.DirName).ToString();
            }
        }

        private void NameUpdateThread(object d)
        {
            UpdateData data = (UpdateData) d;
            bool abort = false;
            do
            {
                UpdateJob job = null;
                lock (data.jobs)
                {
                    if (data.jobs.Count > 0)
                    {
                        job = data.jobs.Dequeue();
                    }
                    else
                    {
                        abort = true;
                    }
                }

                if (job != null)
                {
                    string name = GetDisplayName(Steam.ToSteamId64(job.dir));

                    lock (data.tLock)
                    {
                        if (data.tLock.Aborted)
                        {
                            abort = true;
                        }
                        else
                        {
                            UpdateDisplayNameInList(job.index, name);
                        }
                    }
                }
            } while (!abort);

            OnNameUpdateThreadTerminate();
        }

        private void OnNameUpdateThreadTerminate()
        {
            if (InvokeRequired)
            {
                Invoke(new SimpleDelegate(OnNameUpdateThreadTerminate));
            }
            else
            {
                currentThreadCount--;
                if (currentThreadCount == 0)
                {
                    SetUpdateInterfaceNormal();
                }
            }
        }

        private void ProfileDlg_FormClosing(object sender, FormClosingEventArgs e)
        {
            lock (currentThreadLock)
            {
                currentThreadLock.Aborted = true;
            }
        }

        private void ProfileDlg_Load(object sender, EventArgs e)
        {
            ttHelp.Ext_SetToolTip(lblHelp_ExportDiscard, GlobalStrings.DlgProfile_Help_ExportDiscard);
            ttHelp.Ext_SetToolTip(lblHelp_LocalUpdate, GlobalStrings.DlgProfile_Help_LocalUpdate);
            ttHelp.Ext_SetToolTip(lblHelp_WebUpdate, GlobalStrings.DlgProfile_Help_WebUpdate);
            ttHelp.Ext_SetToolTip(lblHelp_IncludeUnknown, GlobalStrings.DlgProfile_Help_IncludeUnknown);
            ttHelp.Ext_SetToolTip(lblHelp_BypassIgnoreOnImport, GlobalStrings.DlgProfile_Help_BypassIgnoreOnImport);

            LoadShortIds();
            if (editMode)
            {
                InitializeEditMode();
            }
            else
            {
                txtFilePath.Text = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Depressurizer\Default.profile";

                if (lstUsers.Items.Count == 0)
                {
                    MessageBox.Show(GlobalStrings.DlgProfile_NoAccountConfiguration, Resources.Warning, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    radSelUserByURL.Checked = true;
                }
                else
                {
                    radSelUserFromList.Checked = true;
                }

                StartThreadedNameUpdate();
            }

            lstIgnored.ListViewItemSorter = new IgnoreListViewItemComparer();
        }

        private void radSelUser_CheckedChanged(object sender, EventArgs e)
        {
            lstUsers.Enabled = radSelUserFromList.Checked;
            lstUsers.SelectedItem = null;
            txtUserID.Enabled = radSelUserByID.Checked;
            txtUserUrl.Enabled = radSelUserByURL.Checked;

            if (radSelUserFromList.Checked)
            {
                SelectUserInList(txtUserID.Text);
            }
        }

        private void SaveModifiables(Profile p)
        {
            p.SteamID64 = long.Parse(txtUserID.Text);

            p.SteamWebApiKey = textBoxSteamApi.Text;
            p.AutoUpdate = chkAutoUpdate.Checked;
            p.AutoImport = chkAutoImport.Checked;
            p.LocalUpdate = chkLocalUpdate.Checked;
            p.WebUpdate = chkWebUpdate.Checked;
            p.ExportDiscard = chkExportDiscard.Checked;
            p.IncludeShortcuts = chkIncludeShortcuts.Checked;
            p.OverwriteOnDownload = chkOverwriteNames.Checked;

            p.AutoIgnore = chkAutoIgnore.Checked;
            p.IncludeUnknown = chkIncludeUnknown.Checked;
            p.BypassIgnoreOnImport = chkBypassIgnoreOnImport.Checked;

            SortedSet<long> ignoreSet = new SortedSet<long>();
            foreach (ListViewItem item in lstIgnored.Items)
            {
                if (long.TryParse(item.Text, out long id))
                {
                    ignoreSet.Add(id);
                }
            }

            p.IgnoreList = ignoreSet;
        }

        private bool SelectUserInList(long accountId)
        {
            string profDirName = Steam.ToSteam3Id(accountId);

            for (int i = 0; i < lstUsers.Items.Count; i++)
            {
                UserRecord r = lstUsers.Items[i] as UserRecord;
                if (r != null && r.DirName == profDirName)
                {
                    lstUsers.SelectedIndex = i;
                    return true;
                }
            }

            return false;
        }

        private void SelectUserInList(string accountId)
        {
            if (!long.TryParse(accountId, out long val))
            {
                return;
            }

            SelectUserInList(val);
        }

        private void SetUpdateInterfaceNormal()
        {
            cmdUserUpdate.Enabled = true;
            cmdUserUpdateCancel.Enabled = false;
            lblUserStatus.Text = GlobalStrings.DlgProfile_ClickUpdateToDisplayNames;
        }

        private void SetUpdateInterfaceRunning()
        {
            cmdUserUpdate.Enabled = false;
            cmdUserUpdateCancel.Enabled = true;
            lblUserStatus.Text = GlobalStrings.DlgProfile_UpdatingNames;
        }

        private void SetUpdateInterfaceStopping()
        {
            cmdUserUpdate.Enabled = false;
            cmdUserUpdateCancel.Enabled = false;
            lblUserStatus.Text = GlobalStrings.DlgProfile_Cancelling;
        }

        private void StartThreadedNameUpdate()
        {
            if (currentThreadCount > 0)
            {
                return;
            }

            int maxThreads = 1;

            Queue<UpdateJob> q = new Queue<UpdateJob>();
            for (int i = 0; i < lstUsers.Items.Count; i++)
            {
                UserRecord r = lstUsers.Items[i] as UserRecord;
                if (r != null)
                {
                    q.Enqueue(new UpdateJob(i, r.DirName));
                }
            }

            int threads = maxThreads > q.Count ? maxThreads : q.Count;

            if (threads > 0)
            {
                currentThreadLock = new ThreadLocker();
                SetUpdateInterfaceRunning();
                for (int i = 0; i < threads; i++)
                {
                    Thread t = new Thread(NameUpdateThread);
                    currentThreadCount++;
                    t.Start(new UpdateData(q, currentThreadLock));
                }
            }
        }

        private void txtUserID_TextChanged(object sender, EventArgs e)
        {
            //    if( !skipUserClear ) {
            //        lstUsers.ClearSelected();
            //    }
        }

        private void UpdateDisplayNameInList(int index, string name)
        {
            if (InvokeRequired)
            {
                Invoke(new UpdateDelegate(UpdateDisplayNameInList), index, name);
            }
            else
            {
                UserRecord u = lstUsers.Items[index] as UserRecord;
                if (u != null)
                {
                    bool selected = lstUsers.SelectedIndex == index;
                    if (name == null)
                    {
                        name = "?";
                    }

                    u.DisplayName = name;

                    lstUsers.Items.RemoveAt(index);
                    lstUsers.Items.Insert(index, u);
                    lstUsers.SelectedIndex = selected ? index : 0;
                }
            }
        }

        private bool ValidateEntries()
        {
            if (long.TryParse(txtUserID.Text, out long _))
            {
                return true;
            }

            MessageBox.Show(GlobalStrings.DlgProfile_AccountIDMustBeNumber, GlobalStrings.DBEditDlg_Error, MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        #endregion

        public class ThreadLocker
        {
            #region Public Properties

            public bool Aborted { get; set; }

            #endregion
        }

        public class UpdateData
        {
            #region Fields

            public Queue<UpdateJob> jobs;

            public ThreadLocker tLock;

            #endregion

            #region Constructors and Destructors

            public UpdateData(Queue<UpdateJob> q, ThreadLocker l)
            {
                jobs = q;
                tLock = l;
            }

            #endregion
        }

        public class UpdateJob
        {
            #region Fields

            public string dir;

            public int index;

            #endregion

            #region Constructors and Destructors

            public UpdateJob(int i, string d)
            {
                index = i;
                dir = d;
            }

            #endregion
        }

        public class UserRecord
        {
            #region Fields

            public string DirName;

            public string DisplayName;

            #endregion

            #region Constructors and Destructors

            public UserRecord(string dir)
            {
                DirName = dir;
            }

            #endregion

            #region Public Methods and Operators

            public override string ToString()
            {
                if (DisplayName == null)
                {
                    return DirName;
                }

                return $"{DirName} - {DisplayName}";
            }

            #endregion
        }
    }
}
