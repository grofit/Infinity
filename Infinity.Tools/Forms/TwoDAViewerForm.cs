using System;
using System.Data;
using System.IO;
using System.Windows.Forms;
using Infinity.Lookups;
using Infinity.Plugins;
using Infinity.Plugins.TwoDA;

namespace Infinity.Tools.Forms
{
    public partial class TwoDAViewerForm : Form
    {
        private OpenFileDialog m_FileDialog;
        private TwoDAPlugin m_TwoDAPlugin;
        private DataTable m_DataTable;

        public TwoDAViewerForm(TwoDAPlugin twoDAPlugin)
        {
            InitializeComponent();

            m_FileDialog = new OpenFileDialog();
            m_TwoDAPlugin = twoDAPlugin;

            SetupEvents();
            SetupFileDialogs();
            SetupGrid();
        }

        private void SetupEvents()
        {
            loadFileButton.Click += loadFileButton_Click;
        }

        private void SetupFileDialogs()
        {
            m_FileDialog.Filter = "*.2da | *.2da;";
        }

        private void SetupGrid()
        {
            m_DataTable = new DataTable();
            twoDAGrid.DataSource = m_DataTable;
        }

        private void loadFileButton_Click(object sender, EventArgs e)
        {
            if (m_FileDialog.ShowDialog() == DialogResult.OK)
            {
                using (var fileStream = new FileStream(m_FileDialog.FileName, FileMode.Open))
                {
                    var resource = m_TwoDAPlugin.Import(fileStream);
                    Display2DAResource(resource);
                }
            }
        }

        private void Display2DAResource(TwoDAResource resource)
        {
            m_DataTable.Clear();
            m_DataTable.Columns.Clear();
            m_DataTable.Rows.Clear();

            CreateColumns(resource);
            CreateRows(resource);
        }

        private void CreateColumns(TwoDAResource resource)
        {
            m_DataTable.Columns.Add(" ");

            for(int i=0;i<resource.Columns.Count;i++)
            { m_DataTable.Columns.Add(resource.Columns[i]); }
        }

        private void CreateRows(TwoDAResource resource)
        {
            for(int i=0;i<resource.Rows.Count;i++)
            {
                var dataRow = m_DataTable.NewRow();
                dataRow.SetField(0, resource.Rows[i].RowName);

                for(int j=1;j<resource.Rows[i].RowData.Count;j++)
                { dataRow.SetField(j, resource.Rows[i].RowData[j]); }

                m_DataTable.Rows.Add(dataRow);
            }
        }
    }
}
