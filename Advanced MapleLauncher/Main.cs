using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Threading;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

/*
    Launcher Created By: Torban
    RaGEZONE Profile: https://forum.ragezone.com/members/2000184932.html

    Please do not remove the credits.
*/

namespace Advanced_MapleLauncher
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private List<string> checkForUpdates()
        {
            string xmlResponse = "";

            try
            {
                xmlResponse = new WebClient().DownloadString(Launcher.Settings.xmlURL);
            }
            catch
            {
                MessageBox.Show("The launcher failed to check updates! The website may be down right now.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return null;
            }

            List<string> downloadLinks = new List<string>();
            string[] wzFiles = { "Base.wz", "Character.wz", "Effect.wz", "Etc.wz", "Item.wz", "List.wz", "Map.wz", "Mob.wz", "Morph.wz", "Npc.wz", "Quest.wz", "Reactor.wz", "Skill.wz", "Sound.wz", "String.wz", "TamingMob.wz", "UI.wz" };

            // Check update for the client

            string clientDownload = getBetween(xmlResponse, "<client_link>", "</client_link>", 0);
            string clientSize = getBetween(xmlResponse, "<client_size>", "</client_size>", 0);
            string clientName = getBetween(xmlResponse, "<client_name>", "</client_name>", 0);

            if (File.Exists(Path.Combine(textBox1.Text, clientName)))
            {
                string clientSizeOnComputer = new FileInfo(Path.Combine(textBox1.Text, clientName)).Length.ToString();

                if (clientSize != clientSizeOnComputer) { downloadLinks.Add(clientName + "*" + clientSize + "*" + clientDownload); }
            }
            else
            {
                downloadLinks.Add(clientName + "*" + clientSize + "*" + clientDownload);
            }

            // Check update for WZ files

            foreach (string wzName in wzFiles)
            {
                string temp = wzName.Split('.')[0].ToLower();

                string wzDownload = getBetween(xmlResponse, "<" + temp + "_link>", "</" + temp + "_link>", 0);
                string wzSize = getBetween(xmlResponse, "<" + temp + "_size>", "</" + temp + "_size>", 0);

                if (File.Exists(Path.Combine(textBox1.Text, wzName)))
                {
                    string wzSizeOnComputer = new FileInfo(Path.Combine(textBox1.Text, wzName)).Length.ToString();

                    if (wzSize != wzSizeOnComputer) { downloadLinks.Add(wzName + "*" + wzSize + "*" + wzDownload); }
                }
                else
                {
                    downloadLinks.Add(wzName + "*" + wzSize + "*" + wzDownload);
                }
            }

            return downloadLinks;
        }

        private void launch()
        {
            List<string> downloadLinks = checkForUpdates();

            if (downloadLinks != null && downloadLinks.Count == 0)
            {
                Process.Start(textBox1.Text + Launcher.Settings.clientName);
            }
            else if(downloadLinks != null)
            {
                if (MessageBox.Show("An update is required! Do you want to update now?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    Update newFrm = new Update();

                    newFrm.maplePath = textBox1.Text + @"\";
                    newFrm.downloadLinks = downloadLinks;
                    newFrm.backupWZ = checkBox1.Checked;
                    newFrm.Show();
                }
            }
        }

        private string getBetween(string input, string str1, string str2, int index)
        {
            return Regex.Split(Regex.Split(input, str1)[index + 1], str2)[0];
        }

        private string[] getBetweenAll(string strSource, string strStart, string strEnd)
        {
            List<string> Matches = new List<string>();

            for (int pos = strSource.IndexOf(strStart, 0),
                end = pos >= 0 ? strSource.IndexOf(strEnd, pos) : -1;
                pos >= 0 && end >= 0;
                pos = strSource.IndexOf(strStart, end),
                end = pos >= 0 ? strSource.IndexOf(strEnd, pos) : -1)
            {
                Matches.Add(strSource.Substring(pos + strStart.Length, end - (pos + strStart.Length)));
            }

            return Matches.ToArray();
        }

        private void checkInfo()
        {
            string xmlResponse = "";

            try
            {
                xmlResponse = new WebClient().DownloadString(Launcher.Settings.xmlURL);
            }
            catch
            {
                MessageBox.Show("An error has occured! The website may be down right now.", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }

            string serverName = getBetween(xmlResponse, "<servername>", "</servername>", 0);

            string expRate = getBetween(xmlResponse, "<exprate>", "</exprate>", 0);
            string mesoRate = getBetween(xmlResponse, "<mesorate>", "</mesorate>", 0);
            string dropRate = getBetween(xmlResponse, "<droprate>", "</droprate>", 0);

            Launcher.Settings.clientName = getBetween(xmlResponse, "<client_name>", "</client_name>", 0);
            Launcher.Settings.websiteURL = getBetween(xmlResponse, "<websiteurl>", "</websiteurl>", 0);
            Launcher.Settings.forumURL = getBetween(xmlResponse, "<forumurl>", "</forumurl>", 0);
            Launcher.Settings.voteURL = getBetween(xmlResponse, "<voteurl>", "</voteurl>", 0);

            Invoke(new Action(() =>
            {
                this.Text = string.Format("{0} Launcher", serverName);

                label2.Text = expRate; label4.Text = mesoRate; label6.Text = dropRate;
            }));

            string[] newsTitle = getBetweenAll(xmlResponse, "<title>", "</title>");
            string[] newsMessage = getBetweenAll(xmlResponse, "<message>", "</message>");

            Invoke(new Action(() => { listView1.Items.Clear(); }));

            for (int i = 0; i < newsTitle.Count(); i++)
            {
                ListViewItem LVI = new ListViewItem(newsTitle[i]);
                LVI.SubItems.Add(newsMessage[i]);

                Invoke(new Action(() => { listView1.Items.Add(LVI); }));
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = Properties.Settings.Default.maplePath;

            new Thread(() => { checkInfo(); }) { IsBackground = true }.Start();
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems.Count == 1)
            {
                News newFrm = new News();
                newFrm.Text = listView1.SelectedItems[0].Text;

                newFrm.textBox1.Text = listView1.SelectedItems[0].Text;
                newFrm.textBox2.Text = listView1.SelectedItems[0].SubItems[1].Text;

                newFrm.Show();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == string.Empty)
            {
                MessageBox.Show("Please set your MapleStory folder path!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                launch();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog FBD = new FolderBrowserDialog();

            if (FBD.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = FBD.SelectedPath;

                Properties.Settings.Default.maplePath = FBD.SelectedPath; Properties.Settings.Default.Save();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if(textBox1.Text == string.Empty)
            {
                MessageBox.Show("Please set your MapleStory folder path!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                List<string> downloadLinks = checkForUpdates();

                if (downloadLinks != null && downloadLinks.Count == 0)
                {
                    MessageBox.Show("No updates found!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else if (downloadLinks != null)
                {
                    if (MessageBox.Show("An update is required! Do you want to update now?", "Info", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                    {
                        Update newFrm = new Update();

                        newFrm.maplePath = textBox1.Text + @"\";
                        newFrm.downloadLinks = downloadLinks;
                        newFrm.backupWZ = checkBox1.Checked;
                        newFrm.Show();
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Process.Start(Launcher.Settings.websiteURL);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start(Launcher.Settings.forumURL);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Process.Start(Launcher.Settings.voteURL);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            new Thread(() => { checkInfo(); }) { IsBackground = true }.Start();
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {
            Process.Start("https://forum.ragezone.com/members/2000184932.html");
        }
    }
}
