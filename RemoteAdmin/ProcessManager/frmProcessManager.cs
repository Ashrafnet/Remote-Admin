using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteAdmin.ProcessManager
{
    public partial class frmProcessManager : Form
    {
        public frmProcessManager(List<string> Host)
        {
            InitializeComponent();
            this.Text = "Task Manager";
            for (int i = 0; i < Host.Count; i++)
            {
                ctrProcessManager x = new ctrProcessManager();            
                x.LoadProcess(Host[i] , true);
                tabControl1.TabPages.Add(Host[i] + "");
                tabControl1.TabPages[i].Controls.Add(x);
                x.Dock = DockStyle.Fill;
                
            }
            

        }

        private void frmProcessManager_Load(object sender, EventArgs e)
        {
            
        }
    }
}