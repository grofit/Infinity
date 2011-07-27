using System;
using System.IO;
using System.Windows.Forms;
using Infinity.Lookups;
using Infinity.Plugins;
using Infinity.Plugins.BIF;
using Infinity.Tools.Helpers;

namespace Infinity.Tools.Forms
{
    public partial class BiffViewerForm : Form
    {
        private OpenFileDialog m_FileDialog;
        private FolderBrowserDialog m_FolderDialog;
        private BIFPlugin m_BifPlugin;

        public BiffViewerForm(BIFPlugin bifPlugin)
        {
            InitializeComponent();

            m_BifPlugin = bifPlugin;
            m_FileDialog = new OpenFileDialog();
            m_FolderDialog = new FolderBrowserDialog();
            
            SetupEvents();
            SetupFileDialogs();
        }

        private void SetupFileDialogs()
        {
            m_FileDialog.Filter = "*.bif, *.biff | *.bif;*.biff";
            m_FolderDialog.Description = "Select folder to extract resources to";
        }

        private void SetupEvents()
        {
            loadFileButton.Click += loadFileButton_Click;
            extractResourcesButton.Click += extractResourcesButton_Click;
        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            if(m_FileDialog.ShowDialog() ==  DialogResult.OK)
            {
                using(var fileStream = new FileStream(m_FileDialog.FileName, FileMode.Open))
                {
                   var resource = m_BifPlugin.Import(fileStream);
                   DisplayBiffResource(resource);
                }
            }
        }

        private void extractResourcesButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(m_FileDialog.FileName))
            { return; }
            
            if(m_FolderDialog.ShowDialog() == DialogResult.OK)
            {
                BifFileHelper.ExtractResources(m_BifPlugin, m_FileDialog.FileName, m_FolderDialog.SelectedPath);
                MessageBox.Show("Extracted resources");
            }
        }

        private void DisplayBiffResource(BIFResource resource)
        {
            biffTreeViewer.Nodes.Clear();

            var fileEntryNode = new TreeNode(string.Format("File Entries [{0}]", resource.FileEntries.Count));
            var tileEntryNode = new TreeNode(string.Format("Tile Entries [{0}]", resource.TilesetEntries.Count));

            for(int i = 0; i < resource.FileEntries.Count; i++)
            { fileEntryNode.Nodes.Add(CreateFileEntryTreeNode(resource.FileEntries[i], i)); }

            for(int i = 0; i < resource.TilesetEntries.Count; i++)
            { tileEntryNode.Nodes.Add(CreateTileEntryTreeNode(resource.TilesetEntries[i], i)); }

            biffTreeViewer.Nodes.Add(fileEntryNode);
            biffTreeViewer.Nodes.Add(tileEntryNode);
        }

        private TreeNode CreateFileEntryTreeNode(BIFFileEntry tileEntry, int entryNumber)
        {
            var entryNode = new TreeNode(string.Format("File Entry Number - {0}", entryNumber));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Locator: {0}", tileEntry.Locator)));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Offset: {0}", tileEntry.Offset)));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Size: {0}", tileEntry.Size)));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Type: {0} ({1})", tileEntry.Type, ResourceTypesHelper.GetResourceStringFromType(tileEntry.Type))));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Unknown: {0}", tileEntry.UnknownData)));

            return entryNode;
        }

        private TreeNode CreateTileEntryTreeNode(BIFTilesetEntry bifTilesetEntry, int entryNumber)
        {
            var entryNode = new TreeNode(string.Format("Tile Entry Number - {0}", entryNumber));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Locator: {0}", bifTilesetEntry.Locator)));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Offset: {0}", bifTilesetEntry.Offset)));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Count: {0}", bifTilesetEntry.Count)));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Size: {0}", bifTilesetEntry.Size)));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Type: {0} ({1})", bifTilesetEntry.Type, ResourceTypesHelper.GetResourceStringFromType(bifTilesetEntry.Type))));
            entryNode.Nodes.Add(new TreeNode(string.Format("Resource Unknown: {0}", bifTilesetEntry.UnknownData)));

            return entryNode;
        }
    }
}
