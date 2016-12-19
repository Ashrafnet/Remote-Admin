using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using RemoteAdmin.Networking;
using System.IO;
using RemoteAdmin.IconHelper;
using System.Diagnostics;
using System.Collections;

namespace RemoteAdmin
{
    public partial class ctrShares : UserControl
    {
        IconListManager ico;
        public ctrShares()
        {
            InitializeComponent();
            ico= new IconListManager(imgSmallIco, imgBigIco);
            GetShares("LocalHost");
            
        }
        public ctrShares(List<string> Hosts)
        {
            InitializeComponent();
            ico = new IconListManager(imgSmallIco, imgBigIco);

            ListHosts(Hosts);

        }

        public void ListHosts(List<string> Hosts)
        {
            foreach (string host in Hosts)
            {
                GetShares(host);
            }
        }
        void GetShares(string Host)
        {
            TreeNode TNc = new TreeNode(Host );
            ShareCollection shi;
            TNc.Tag = Host ;
            TNc.ImageIndex = 0;
            TNc.SelectedImageIndex = TNc.ImageIndex;
            int CurrIndex = 0;
            if (Host == "LocalHost") 
                 shi = ShareCollection.LocalShares;
            else
                 shi = ShareCollection.GetShares(Host);
            if (shi != null)
            {
                foreach (Share si in shi)
                {
                    TreeNode TN = new TreeNode(si.NetName + "");
                    TN.Tag = si.ToString() + "";
                    if (si.ShareType == ShareType.Special)
                    {
                        TN.ImageIndex = 1;
                        
                        TN.SelectedImageIndex = TN.ImageIndex;
                        TNc.Nodes.Insert(CurrIndex, TN);
                        CurrIndex += 1;
                    }
                    else
                    {
                        TN.ImageIndex = 2;
                        TN.SelectedImageIndex = TN.ImageIndex;
                        TNc.Nodes.Add ( TN);
                    }

                }
                TNc.Expand();
                treeView1.Nodes.Add(TNc);
                treeView1.TreeViewNodeSorter = new NodeSorter();
            }
        }

        void LoadFiles(string DirPath)
        {
            long SizeOfFiles = 0;
            try
            {
                ico.AddFolderIcon(DirPath );
                fileSystemWatcher1.EnableRaisingEvents = false;
                fileSystemWatcher1.Path = DirPath;                
                string[] fPathes = Directory.GetFiles(DirPath);                                
                foreach (string fPath in fPathes)
                {
                    SizeOfFiles +=  AddItemToListView(fPath,true );
                }

            }
            catch
            {
            }
            finally
            {

                toolStripStatusLabel2.Text = String.Format(new FileSizeFormatProvider(), "File size: {0:fs}", SizeOfFiles);
                toolStripStatusLabel1.Text = listView1.Items.Count + " Objects.";

                fileSystemWatcher1.EnableRaisingEvents = true;
            }
        }
        long  AddItemToListView(string fPath,bool IsFile)
        {
            try
            {
                ListViewItem LVI;
                if (IsFile)
                {
                    
                    FileInfo f = new FileInfo(fPath);
                    LVI = new ListViewItem(f.Name);
                    LVI.Tag = fPath;
                    LVI.ImageIndex = ico.AddFileIcon(fPath);                    
                    listView1.Items.Add(LVI);
                    return f.Length;
                }
                else
                {
                    DirectoryInfo f = new DirectoryInfo(fPath);
                    LVI = new ListViewItem(f.Name);
                    LVI.Tag = fPath;
                    LVI.ImageIndex = ico.AddFolderIcon(fPath);
                    listView1.Items.Add(LVI);
                    
                    return 0;
                }
                return 0;
            }
            catch
            {
                return 0;
            }
        }
        void AddComputerNode(string ComputerName)
        {
            try
            {
                TreeNode TNc = new TreeNode(ComputerName);
                TNc.Tag = ComputerName;
                TNc.ImageIndex = 0;
                TNc.SelectedImageIndex = TNc.ImageIndex;
                treeView1.Nodes.Add(TNc);
            }
            catch
            {
            }
        }

        void RemoteShares(string ServerName)
        {
            ShareCollection shi = ShareCollection.GetShares(ServerName);
            if (shi != null)
            {
                foreach (Share si in shi)
                {
                    Console.WriteLine("{0}: {1} [{2}]",
                        si.ShareType, si, si.Path);

                    // If this is a file-system share, try to
                    // list the first five subfolders.
                    // NB: If the share is on a removable device,
                    // you could get "Not ready" or "Access denied"
                    // exceptions.
                    // If you don't have permissions to the share,
                    // you will get security exceptions.
                    if (si.IsFileSystem)
                    {
                        try
                        {
                            System.IO.DirectoryInfo d = si.Root;
                            System.IO.DirectoryInfo[] Flds = d.GetDirectories();
                            for (int i = 0; i < Flds.Length && i < 5; i++)
                                Console.WriteLine("\t{0} - {1}", i, Flds[i].FullName);

                            Console.WriteLine();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("\tError listing {0}:\n\t{1}\n",
                                si.ToString(), ex.Message);
                        }
                    }
                }
            }
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {            
            {
                try
                {
                    TreeNode TN = new TreeNode();
                    if (e.Node.ImageIndex == 0)
                    {
                        return;
                    }
                    e.Node.Nodes.Clear(); listView1.Items.Clear();
                    DirectoryInfo d = new DirectoryInfo(e.Node.Tag +"");
                    DirectoryInfo[] Flds = d.GetDirectories();
                    for (int i = 0; i < Flds.Length ; i++)
                    {
                        TreeNode TNx = new TreeNode();
                        TNx.Text = Flds[i].Name ;
                        TNx.Tag = Flds[i].FullName;
                        TNx.ImageIndex = 3;
                        TNx.SelectedImageIndex  = 4;
                        e.Node.Nodes.Add(TNx);
                        AddItemToListView(Flds[i].FullName, false);
                    }
                    
                    LoadFiles(d.FullName);
                    e.Node.Expand();
                    
                }
                catch (Exception ex)
                {
                }
                

                
            }
        }

        private void listView1_DoubleClick(object sender, EventArgs e)
        {
            Open();
        }
        void Explor()
        {
            try
            {
                string DirPath = listView1.SelectedItems[0].Tag + "";
                DirPath = Path.GetDirectoryName(DirPath);
                Process.Start(DirPath);
            }
            catch
            {
            }
        }
        void Open()
        {
            try
            {
                string DirPath = listView1.SelectedItems[0].Tag + "";
                string DirName = listView1.SelectedItems[0].Text + "";
                if (listView1.SelectedItems.Count < 1) return;

                if (IsDir(DirPath))
                {
                    for (int i = 0; i < treeView1.SelectedNode.Nodes.Count; i++)
                        if (treeView1.SelectedNode.Nodes[i].Text == DirName)
                            treeView1.SelectedNode = treeView1.SelectedNode.Nodes[i];                    
                }
                else
                    Process.Start(DirPath);
            }
            catch(Exception er)
            {
                MessageBox.Show(er.Message);
            }
        }
        private void fileSystemWatcher1_Created(object sender, FileSystemEventArgs e)
        {
            if (!IsDir(e.FullPath))
                AddItemToListView(e.FullPath, true);
            else
            {
                AddItemToListView(e.FullPath, false);
                TreeNode TN = new TreeNode(e.Name);
                TN.ImageIndex = ico.AddFolderIcon(e.FullPath ); ;
                TN.Tag = e.FullPath ;
                treeView1.SelectedNode.Nodes.Add(TN);
            }
        }

        private void fileSystemWatcher1_Deleted(object sender, FileSystemEventArgs e)
        {
            try
            {
                for (int i =0; i  < listView1.Items.Count; i++)
                    if (listView1.Items[i].Text == e.Name )
                        listView1.Items[i].Remove();
            }
            catch
            {
            }
        }
        bool IsDir(string strPath)
        {
            if (!Directory.Exists(strPath))
            {
                DirectoryInfo x = new DirectoryInfo(strPath);
                if (!(x.Extension.Length == 0))
                    return false;
                else
                    return true;
            }
            return true  ;

        }
        private void fileSystemWatcher1_Renamed(object sender, RenamedEventArgs e)
        {
            for (int i = 0; i < listView1.Items.Count; i++)
                if (listView1.Items[i].Text == e.OldName)
                {
                    listView1.Items[i].Text = e.Name;
                    listView1.Items[i].ImageIndex = ico.AddFileIcon(e.FullPath);
                }
            
        }

        private void listView1_Click(object sender, EventArgs e)
        {
              

        }
        private string[] GetSelectedObjects()
        {
            if (listView1.SelectedItems.Count == 0)
                return null;

            string[] files = new string[listView1.SelectedItems.Count];
            int i = 0;
            foreach (ListViewItem item in listView1.SelectedItems)
            {
                files[i++] = item.Tag+"";
            }
            return files;
        }
        /// <summary>
        /// Write files to clipboard (from http://blogs.wdevs.com/idecember/archive/2005/10/27/10979.aspx)
        /// </summary>
        /// <param name="cut">True if cut, false if copy</param>
        void CopyToClipboard(bool cut)
        {
            
            string[] files = GetSelectedObjects();
            if (files != null)
            {
                IDataObject data = new DataObject(DataFormats.FileDrop, files);
                MemoryStream memo = new MemoryStream(4);
                byte[] bytes = new byte[] { (byte)(cut ? 2 : 5), 0, 0, 0 };
                memo.Write(bytes, 0, bytes.Length);
                data.SetData("Preferred DropEffect", memo);
                
                Clipboard.SetDataObject(data);
            }
        }

        void DoPaste()
        {
            ShellLib.ShellFileOperation fo = new ShellLib.ShellFileOperation();

            String[] source = new String[3];
            String[] dest = new String[3];

            source[0] = Environment.SystemDirectory + @"\winmine.exe";
            source[1] = Environment.SystemDirectory + @"\freecell.exe";
            source[2] = Environment.SystemDirectory + @"\mshearts.exe";
            dest[0] = Environment.SystemDirectory.Substring(0, 2) + @"\winmine.exe";
            dest[1] = Environment.SystemDirectory.Substring(0, 2) + @"\freecell.exe";
            dest[2] = Environment.SystemDirectory.Substring(0, 2) + @"\mshearts.exe";

            fo.Operation = ShellLib.ShellFileOperation.FileOperations.FO_COPY;
            fo.OwnerWindow = this.Handle;
            fo.SourceFiles = source;
            fo.DestFiles = dest;

            bool RetVal = fo.DoOperation();
        }
        private void listView1_KeyPress(object sender, KeyPressEventArgs e)
        {
            
        }

        private void listView1_KeyUp(object sender, KeyEventArgs e)
        {
            
        }

        private void listView1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control == true & e.KeyCode == Keys.C)
                CopyToClipboard(false);
            else if (e.Control  == true  & e.KeyCode  == Keys.X)
                CopyToClipboard(true);
        }

        private void contextMenuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Tag+"")
            {
                case "copy":
                    break;
                case "cut":
                    break;
                case "paste":
                    break;
                case "open":
                    Open();
                    break;
                case "explor":
                    Explor();
                    break;
            }
        }
    }
    // Create a node sorter that implements the IComparer interface.
    public class NodeSorter : IComparer
    {
        // Compare the length of the strings, or the strings
        // themselves, if they are the same length.
        public int Compare(object x, object y)
        {
            TreeNode tx = x as TreeNode;
            TreeNode ty = y as TreeNode;

            // Compare the length of the strings, returning the difference.
            if (tx.Text.Length != ty.Text.Length)
                return tx.Text.Length - ty.Text.Length;

            // If they are the same length, call Compare.
            return string.Compare(tx.Text, ty.Text);
        }
    }
}
