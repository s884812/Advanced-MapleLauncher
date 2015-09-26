using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Net;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

/*
    Launcher Created By: Torban
    RaGEZONE Profile: https://forum.ragezone.com/members/2000184932.html

    Please do not remove the credits.
*/

namespace Advanced_MapleLauncher
{
    public partial class Update : Form
    {
        public List<string> downloadLinks;
        public string maplePath;
        public Boolean backupWZ;

        public Update()
        {
            InitializeComponent();
        }

        private void Download_Load(object sender, EventArgs e)
        {
            label6.Text = downloadLinks.Count.ToString(); label2.Text = downloadLinks[0].Split('*')[0];

            if (File.Exists(new FileInfo(Path.Combine(maplePath, downloadLinks[0].Split('*')[0])).ToString()))
            {
                if (backupWZ && downloadLinks[0].Split('*')[0].Contains(".wz"))
                {
                    Boolean complete = false;
                    int i = 1;

                    while (!complete)
                    {
                        if (File.Exists(new FileInfo(Path.Combine(maplePath, downloadLinks[0].Split('*')[0].Split('.')[0] + ".BAK" + i.ToString())).ToString()))
                        {
                            i++;
                        }
                        else
                        {
                            complete = true;
                        }
                    }

                    File.Move(new FileInfo(Path.Combine(maplePath, downloadLinks[0].Split('*')[0])).ToString(), new FileInfo(Path.Combine(maplePath, downloadLinks[0].Split('*')[0].Split('.')[0] + ".BAK" + i.ToString())).ToString());
                }
                else
                {
                    File.Delete(new FileInfo(Path.Combine(maplePath, downloadLinks[0].Split('*')[0])).ToString());
                }
            }

            WebClient client = new WebClient();

            client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
            client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

            client.DownloadFileAsync(new Uri(downloadLinks[0].Split('*')[2]), maplePath + downloadLinks[0].Split('*')[0]);
        }

        void client_DownloadProgressChanged(object sender, DownloadProgressChangedEventArgs e)
        {
            double bytesIn = double.Parse(e.BytesReceived.ToString()); 
            double totalBytes = double.Parse(downloadLinks[0].Split('*')[1]);
            double percentage = bytesIn / totalBytes * 100;

            label7.Text = string.Format("{0} MB / {1} MB", (e.BytesReceived / 1024d / 1024d).ToString("0.00"), (Double.Parse(downloadLinks[0].Split('*')[1]) / 1024d / 1024d).ToString("0.00"));

            progressBar1.Value = int.Parse(Math.Truncate(percentage).ToString());
        }


        void client_DownloadFileCompleted(object sender, AsyncCompletedEventArgs e)
        {
            downloadLinks.RemoveAt(0);

            label4.Text = (Convert.ToInt32(label4.Text) + 1).ToString(); 

            if (downloadLinks.Count == 0)
            {
                this.Close();

                MessageBox.Show("Update has been finished!", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                progressBar1.Value = 0; label2.Text = downloadLinks[0].Split('*')[0];

                if (File.Exists(new FileInfo(Path.Combine(maplePath, downloadLinks[0].Split('*')[0])).ToString()))
                {
                    if (backupWZ)
                    {
                        Boolean complete = false;
                        int i = 1;

                        while (!complete)
                        {
                            if (File.Exists(new FileInfo(Path.Combine(maplePath, downloadLinks[0].Split('*')[0].Split('.')[0] + ".BAK" + i.ToString())).ToString()))
                            {
                                i++;
                            }
                            else
                            {
                                complete = true;
                            }
                        }

                        File.Move(new FileInfo(Path.Combine(maplePath, downloadLinks[0].Split('*')[0])).ToString(), new FileInfo(Path.Combine(maplePath, downloadLinks[0].Split('*')[0].Split('.')[0] + ".BAK" + i.ToString())).ToString());
                    }
                    else
                    {
                        File.Delete(new FileInfo(Path.Combine(maplePath, downloadLinks[0].Split('*')[0])).ToString());
                    }
                }

                WebClient client = new WebClient();

                client.DownloadProgressChanged += new DownloadProgressChangedEventHandler(client_DownloadProgressChanged);
                client.DownloadFileCompleted += new AsyncCompletedEventHandler(client_DownloadFileCompleted);

                client.DownloadFileAsync(new Uri(downloadLinks[0].Split('*')[2]), maplePath + downloadLinks[0].Split('*')[0]);
            }
        }
    }
}
