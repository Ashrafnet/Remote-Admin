using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace RemoteAdmin
{
    public partial class frmShares : Form
    {
        public frmShares()
        {
            InitializeComponent();
        }
        public frmShares(List<string> Hosts)
        {
            InitializeComponent();
            ctrShares1.ListHosts(Hosts);
            //webBrowser1.Url = new Uri("\\\\ashrafk\\c$");
        }

        private void frmShares_Load(object sender, EventArgs e)
        {

        }

    }
}