using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace RemoteAdmin
{
    public partial class frmExecuteCommand : Form
    {
        public frmExecuteCommand()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                string FilePath = textBox2.Text ;
                if (checkBox1.Checked)
                {
                    FileInfo x = new FileInfo(textBox2.Text);
                    FilePath = "\\\\" + textBox1.Text + "\\admin$\\" + x.Name;
                    File.Copy(textBox2.Text, FilePath , true);
                }
                Wmi.Process.ProcessRemote pr = new Wmi.Process.ProcessRemote(null, null, null, textBox1.Text);
                MessageBox.Show(pr.CreateProcess(FilePath ));
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
            
            
        }
    }
}