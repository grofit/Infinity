namespace Infinity.Tools.Forms
{
    partial class TwoDAViewerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.fileGroup = new System.Windows.Forms.GroupBox();
            this.twoDAGrid = new System.Windows.Forms.DataGridView();
            this.loadFileButton = new System.Windows.Forms.Button();
            this.fileGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.twoDAGrid)).BeginInit();
            this.SuspendLayout();
            // 
            // fileGroup
            // 
            this.fileGroup.Controls.Add(this.loadFileButton);
            this.fileGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.fileGroup.Location = new System.Drawing.Point(0, 0);
            this.fileGroup.Name = "fileGroup";
            this.fileGroup.Size = new System.Drawing.Size(284, 47);
            this.fileGroup.TabIndex = 0;
            this.fileGroup.TabStop = false;
            // 
            // twoDAGrid
            // 
            this.twoDAGrid.AllowUserToAddRows = false;
            this.twoDAGrid.AllowUserToDeleteRows = false;
            this.twoDAGrid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.twoDAGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.twoDAGrid.Location = new System.Drawing.Point(0, 47);
            this.twoDAGrid.Name = "twoDAGrid";
            this.twoDAGrid.ReadOnly = true;
            this.twoDAGrid.Size = new System.Drawing.Size(284, 215);
            this.twoDAGrid.TabIndex = 1;
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(6, 12);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(112, 28);
            this.loadFileButton.TabIndex = 0;
            this.loadFileButton.Text = "Load File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            // 
            // TwoDAViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 262);
            this.Controls.Add(this.twoDAGrid);
            this.Controls.Add(this.fileGroup);
            this.Name = "TwoDAViewerForm";
            this.ShowIcon = false;
            this.Text = "2DA Viewer";
            this.fileGroup.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.twoDAGrid)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox fileGroup;
        private System.Windows.Forms.DataGridView twoDAGrid;
        private System.Windows.Forms.Button loadFileButton;
    }
}