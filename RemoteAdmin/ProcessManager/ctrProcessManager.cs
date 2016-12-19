using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.Collections;

namespace RemoteAdmin.ProcessManager
{
    public partial class ctrProcessManager : UserControl
    {
        IconHelper.IconListManager Ico;
        string _Machine;
        public ctrProcessManager()
        {
            InitializeComponent();
            
            Ico = new RemoteAdmin.IconHelper.IconListManager(imageList1, IconHelper.IconReader.IconSize.Small);
            
        }

        public void LoadProcess(string Machine,bool LoadJustApps)
        {
            _Machine = Machine;
            
            try
            {
               Process[] PR= Process.GetProcesses(Machine);
               listView1.Items.Clear(); listView2.Items.Clear();
               for (int i = 0; i < PR.Length ; i++)
               {
                   ListViewItem LVI = new ListViewItem();
                   try
                   {
                       if (LoadJustApps)
                       {
                           LVI.Text= (PR[i].MainWindowTitle + "");
                           if(PR[i].Responding )
                                LVI.SubItems.Add("Running.");
                           else
                                LVI.SubItems.Add("Not Responding.");
                            if (LVI.Text.Length > 0)
                                listView2.Items.Add(LVI);
                       }
                       else
                       {
                           LVI.Text =(PR[i].MainModule.ModuleName + "");
                           //PR[i].MainModule.
                           LVI.SubItems.Add(String.Format(new FileSizeFormatProvider(), "{0:fs}", PR[i].PagedMemorySize64));
                           if (LVI.Text.Length > 0)
                               listView1.Items.Add(LVI);
                       }
                       LVI.Tag = PR[i].MainModule.FileName;
                       LVI.ImageIndex = Ico.AddFileIcon(LVI.Tag+"");

                   }
                   catch { }
                   
               }
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
            finally
            {
            }
        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tabControl1.SelectedIndex == 0)
                LoadProcess(_Machine, true);
            else
                LoadProcess(_Machine, false );
        }

        private void listView2_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewSorter Sorter = new ListViewSorter();
            listView2.ListViewItemSorter = Sorter;
            if (!(listView2.ListViewItemSorter is ListViewSorter))
                return;
            Sorter = (ListViewSorter)listView2.ListViewItemSorter;

            if (Sorter.LastSort == e.Column)
            {
                if (listView2.Sorting == SortOrder.Ascending)
                    listView2.Sorting = SortOrder.Descending;
                else
                    listView2.Sorting = SortOrder.Ascending;
            }
            else
            {
                listView2.Sorting = SortOrder.Descending;
            }
            Sorter.ByColumn = e.Column;

            listView2.Sort();
        }

        private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            ListViewSorter Sorter = new ListViewSorter();
            listView1.ListViewItemSorter = Sorter;
            if (!(listView1.ListViewItemSorter is ListViewSorter))
                return;
            Sorter = (ListViewSorter)listView1.ListViewItemSorter;

            if (Sorter.LastSort == e.Column)
            {
                if (listView1.Sorting == SortOrder.Ascending)
                    listView1.Sorting = SortOrder.Descending;
                else
                    listView1.Sorting = SortOrder.Ascending;
            }
            else
            {
                listView1.Sorting = SortOrder.Descending;
            }
            Sorter.ByColumn = e.Column;

            listView1.Sort();

        }
    }
}
