namespace PZZ_ARC.PZZArc
{
    partial class AFSFilePicker
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
            TreeAFSFileList = new TreeView();
            SuspendLayout();
            // 
            // TreeAFSFileList
            // 
            TreeAFSFileList.Dock = DockStyle.Fill;
            TreeAFSFileList.Location = new Point(0, 0);
            TreeAFSFileList.Name = "TreeAFSFileList";
            TreeAFSFileList.ShowPlusMinus = false;
            TreeAFSFileList.Size = new Size(304, 341);
            TreeAFSFileList.TabIndex = 0;
            TreeAFSFileList.MouseDoubleClick += TreeAFSFileList_MouseDoubleClick;
            // 
            // AFSFilePicker
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(304, 341);
            Controls.Add(TreeAFSFileList);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new Size(320, 380);
            Name = "AFSFilePicker";
            Text = "Select PZZ file to load...";
            ResumeLayout(false);
        }

        #endregion

        private TreeView TreeAFSFileList;
    }
}