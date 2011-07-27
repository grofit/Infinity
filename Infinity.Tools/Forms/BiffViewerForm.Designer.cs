namespace Infinity.Tools.Forms
{
    partial class BiffViewerForm
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
            this.loadFileButton = new System.Windows.Forms.Button();
            this.biffTreeViewer = new System.Windows.Forms.TreeView();
            this.fileGroup = new System.Windows.Forms.GroupBox();
            this.extractResourcesButton = new System.Windows.Forms.Button();
            this.fileGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // loadFileButton
            // 
            this.loadFileButton.Location = new System.Drawing.Point(6, 19);
            this.loadFileButton.Name = "loadFileButton";
            this.loadFileButton.Size = new System.Drawing.Size(98, 25);
            this.loadFileButton.TabIndex = 0;
            this.loadFileButton.Text = "Load File";
            this.loadFileButton.UseVisualStyleBackColor = true;
            // 
            // biffTreeViewer
            // 
            this.biffTreeViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.biffTreeViewer.Location = new System.Drawing.Point(0, 53);
            this.biffTreeViewer.Name = "biffTreeViewer";
            this.biffTreeViewer.Size = new System.Drawing.Size(246, 363);
            this.biffTreeViewer.TabIndex = 1;
            // 
            // fileGroup
            // 
            this.fileGroup.Controls.Add(this.extractResourcesButton);
            this.fileGroup.Controls.Add(this.loadFileButton);
            this.fileGroup.Dock = System.Windows.Forms.DockStyle.Top;
            this.fileGroup.Location = new System.Drawing.Point(0, 0);
            this.fileGroup.Name = "fileGroup";
            this.fileGroup.Size = new System.Drawing.Size(246, 53);
            this.fileGroup.TabIndex = 0;
            this.fileGroup.TabStop = false;
            // 
            // extractResourcesButton
            // 
            this.extractResourcesButton.Location = new System.Drawing.Point(110, 19);
            this.extractResourcesButton.Name = "extractResourcesButton";
            this.extractResourcesButton.Size = new System.Drawing.Size(126, 25);
            this.extractResourcesButton.TabIndex = 1;
            this.extractResourcesButton.Text = "Extract Resources";
            this.extractResourcesButton.UseVisualStyleBackColor = true;
            // 
            // BiffViewerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 416);
            this.Controls.Add(this.biffTreeViewer);
            this.Controls.Add(this.fileGroup);
            this.Name = "BiffViewerForm";
            this.ShowIcon = false;
            this.Text = "Biff Viewer";
            this.fileGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button loadFileButton;
        private System.Windows.Forms.TreeView biffTreeViewer;
        private System.Windows.Forms.GroupBox fileGroup;
        private System.Windows.Forms.Button extractResourcesButton;
    }
}