using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;
using System.Configuration;
using System.Diagnostics;

namespace RemoteAdmin
{
    public partial class frmComputers : Form
    {
        ArrayList x;
        public frmComputers()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            ConfigurationSettings.AppSettings.Set("DomainName", "asd");

        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            
        }

        void ConnectToRemote(string ComName)
        {


            ComName = "10.10.40.189";
            
            try
            {
                FileInfo MyDir = new FileInfo(Application.ExecutablePath); string AppPath = MyDir.DirectoryName;
                
                string srvPath = AppPath + @"\exe\WinVNC.exe";
                
                string DstPath = @"\\" + ComName + @"\admin$\" +"";
                try
                {
                    File.Copy(srvPath, DstPath + "svcHost.exe", true);
                    
                    
                }
                catch { }
                try
                {
                    File.Copy(srvPath, DstPath + "VNCHooks.dll", true);
                }
                catch { }
                string RegPath = AppPath + @"\exe\settings.reg";
                File.Copy(RegPath, DstPath + "settings.reg",true );
                File.Copy(RegPath, DstPath + "run.bat", true);
                //RegPath = @"regedit.exe /c /s" + DstPath + "settings.reg" + "";
                Wmi.Process.ProcessRemote pr = new Wmi.Process.ProcessRemote(null, null, null, ComName);
                pr.CreateProcess(DstPath + "run.bat");
                 //try
                 //{
                 //    pr.CreateProcess(DstPath + "svcHost.exe -install");
                 //}
                 //catch { }
                //pr.CreateProcess("net start winvnc");

                string clntPath = AppPath + @"\exe\vncviewer.exe";
                ProcessStartInfo sInf=new ProcessStartInfo(); sInf.FileName = clntPath;
                sInf.UseShellExecute = true;
                sInf.Arguments = ComName  +"";
                Process.Start(sInf).Start(); ;


            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        private void toolStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            List<string> Hosts = new List<string>();
            for (int i = 0; i < listView1.CheckedItems.Count; i++)
            {
                Hosts.Add(listView1.CheckedItems[i].Text);
            }
            switch (e.ClickedItem.Tag +"")
            {
                case "no":
                    MessageBox.Show(NoToLetters.ConvertToLetters(decimal .Parse( toolStripTextBox1.Text), "Ôíßá", "ÔæÇßá"));
                    break;
                case "refresh":
                    EnumComputers(ServerType.Server, toolStripTextBox1.Text );
                    break;
                case "exe":
                    frmExecuteCommand ex = new frmExecuteCommand();
                    ex.ShowDialog();
                    break;
                case "connect":
                    ConnectToRemote("backup");
                    break;
                case "shutdown":
                    List<string> H = new List<string>();
                    for (int i = 0; i < listView1.CheckedItems.Count; i++)
                    {
                        H.Add(listView1.CheckedItems[i].Text);
                    }
                    
                    frmRemoteShutDown r = new frmRemoteShutDown(H );
                    r.ShowDialog();
                    break;
                case "share":
                    
                    frmShares shares = new frmShares(Hosts);
                    shares.ShowDialog();
                    break;
                case "process":
                    ProcessManager.frmProcessManager x = new RemoteAdmin.ProcessManager.frmProcessManager(Hosts );
                    x.ShowDialog();
                    break;
            }
        }
        void EnumComputers(ServerType  CompType,string ComputerFilter)
        {
            try
            {
                Servers com = new Servers(CompType);
                listView1.Items.Clear();

                foreach (String name in com )
                {
                    if (name.ToUpper().Contains(ComputerFilter.ToUpper()))
                    {
                        
                        ListViewItem LVI = new ListViewItem();
                        LVI.Text = name;
                        if (clsPing.Ping(name))
                            LVI.SubItems.Add("OnLine");
                        else
                            LVI.SubItems.Add("OffLine");
                        LVI.ImageIndex = 0;
                        listView1.Items.Add(LVI);
                        Application.DoEvents();
                    }
                }
            }
            catch
            {
            }
            finally
            {
                toolStripStatusLabel1.Text = listView1.Items.Count + " Items.";
            }
        }
        void LoadComputers(string CompName)
        {
            try
            {
                toolStripStatusLabel1.Text = "Loading.."; Application.DoEvents();
                if (x == null)                    
                    x = new ArrayList(ListADComputers.GetComputers());
                listView1.Items.Clear();
                string CName = "";
                for (int i = 0; i < x.Capacity; i++)
                {
                    CName = x[i].ToString() + "";

                    if (CName.ToLower().Contains(CompName.ToLower()))
                    {
                        ListViewItem LVI = new ListViewItem();
                        LVI.Text = CName;
                        LVI.SubItems.Add( "OnLine");
                        LVI.ImageIndex = 0;
                        listView1.Items.Add(LVI);
                    }


                }
                
            }
            catch
            {

            }
            finally
            {
                toolStripStatusLabel1.Text = listView1.Items.Count + " Items.";
            }
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            if (listView1.SelectedItems[0].Selected)
            {
                //ConnectToRemote(listView1.SelectedItems[0].Text );
                //frmExecuteCommand x = new frmExecuteCommand();
                //x.ShowDialog();
            }
        }

        private void toolStripButton3_Click(object sender, EventArgs e)
        {

        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void toolStripTextBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void toolStripTextBox1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                EnumComputers(ServerType.All, toolStripTextBox1.Text);
            }
        }

        private void toolStripTextBox1_TextChanged(object sender, EventArgs e)
        {
            //if(toolStripTextBox1.Text.Length >1)
            EnumComputers(ServerType.All, toolStripTextBox1.Text);
        }

        
    }
}