using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace RemoteAdmin
{
    public partial class frmRemoteShutDown : Form
    {
        public frmRemoteShutDown(List<string> Hosts)
        {
            InitializeComponent();
            for (int i = 0; i < Hosts.Count ; i++)
            {
                ListViewItem LVI =new ListViewItem(Hosts[i]+"");
                LVI.Tag = Hosts[i] + "";
                LVI.ImageIndex = 0;
                listView2.Items.Add(LVI);
            }
        }

        private void frmRemoteShutDown_Load(object sender, EventArgs e)
        {
            comboBox1.SelectedItem = comboBox1.Items[0];
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tabControl1.SelectedIndex = 1;
            progressBar1.Value = 0;
            progressBar1.Maximum = listView2.Items.Count;
            for (int i = 0; i < listView2.Items.Count ; i++)
            {
                if (comboBox1.Text == "Abort")
                    AbortShutdown(listView2.Items[i].Tag + "");
                else
                    ShutRemote(listView2.Items[i].Tag + "");
                progressBar1.Value += 1;
                Application.DoEvents();
            }
            
        }

        void AbortShutdown(string Host)
        {
            ListViewItem LVI = new ListViewItem(Host);
            try
            {                                
                LVI.SubItems.Add(comboBox1.Text);
                
                    
                    //Process[] x= Process.GetProcessesByName("shutx", Host);
                    //if (x.Length > 0)
                    //    x[0].Kill();
                    ProcessStartInfo Pinfo = new ProcessStartInfo();

                    Pinfo.Arguments = "/s " + Host + " /IM shutx.exe";
                    Pinfo.FileName = "TASKKILL";
                    Pinfo.WindowStyle = ProcessWindowStyle.Hidden;
                    Process.Start(Pinfo);
                    LVI.ImageIndex = 1;
                    LVI.SubItems.Add("Shutdown Process Aborted Successfully.");
                

            }
            catch (Exception er)
            {
                LVI.ImageIndex = 2;
                LVI.SubItems.Add(er.Message);
            }
            finally
            {
                LVI.SubItems.Add(DateTime.Now.ToString());                
                listView1.Items.Add(LVI);  
            }
        }
        void ShutRemote(string Host)
        {
            try{
            string dstPath, srcPath;
            srcPath = Application.ExecutablePath;
            FileInfo y = new FileInfo(srcPath);
            srcPath = y.DirectoryName + "\\shutx.exe";
            FileInfo x = new FileInfo(srcPath);
            dstPath = "\\\\" + Host  + "\\admin$\\" + x.Name;
            File.Copy(srcPath, dstPath, true);
            ListViewItem LVI = new ListViewItem(Host);
            LVI.SubItems.Add(comboBox1.Text);                        
            ProcessStartInfo Pinfo = new ProcessStartInfo();
            
            Pinfo.Arguments = "\\\\" + Host + " " + DateTime.Now.AddMinutes(1).ToString("H:mm") + " /interactive "  + dstPath + " " + comboBox1.Text + " " + numericUpDown1.Value ;            
            Pinfo.FileName = "at"; Pinfo.WindowStyle = ProcessWindowStyle.Hidden;
            try
            {
                
                Process.Start(Pinfo);
                LVI.SubItems.Add("Process Started Successfully.");
            }
            catch
            {
                LVI.SubItems.Add("Error while executing a process.");
            }
            
                
            
            LVI.SubItems.Add(DateTime.Now.ToString());
            LVI.ImageIndex = 0;
            listView1.Items.Add(LVI);            
            }
            catch {}
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
        }

        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag+"")
            {
                case "del":
                    for (int i = 0; i < listView2.SelectedItems.Count; i++)
                    {
                        listView2.Items.Remove(listView2.SelectedItems[i]);
                    }
                    
                    break;
                case "":
                    break;

            }
        }
    }
}