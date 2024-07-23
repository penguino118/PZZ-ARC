namespace PZZ_ARC
{
    partial class Form1
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            menuStrip1 = new MenuStrip();
            StripFile = new ToolStripMenuItem();
            StripFileOpen = new ToolStripMenuItem();
            StripFileFromFolder = new ToolStripMenuItem();
            toolStripSeparator5 = new ToolStripSeparator();
            StripFileSave = new ToolStripMenuItem();
            StripFileSaveAs = new ToolStripMenuItem();
            toolStripSeparator4 = new ToolStripSeparator();
            StripFileExit = new ToolStripMenuItem();
            StripEdit = new ToolStripMenuItem();
            aboutToolStripMenuItem = new ToolStripMenuItem();
            StripAboutPZZARC = new ToolStripMenuItem();
            FileTree = new TreeView();
            ContextPZZInner = new ContextMenuStrip(components);
            ContextPZZImport = new ToolStripMenuItem();
            ContextPZZExport = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            ContextPZZStub = new ToolStripMenuItem();
            ContextPZZDelete = new ToolStripMenuItem();
            ContextPZZDuplicate = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            ContextPZZMoveUp = new ToolStripMenuItem();
            ContextPZZMoveDown = new ToolStripMenuItem();
            FileProperties = new PropertyGrid();
            splitContainer1 = new SplitContainer();
            ContextPZZOuter = new ContextMenuStrip(components);
            ContextPZZInsertFile = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            ContextPZZExportAll = new ToolStripMenuItem();
            ContextPZZImportAll = new ToolStripMenuItem();
            menuStrip1.SuspendLayout();
            ContextPZZInner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ContextPZZOuter.SuspendLayout();
            SuspendLayout();
            // 
            // menuStrip1
            // 
            menuStrip1.Items.AddRange(new ToolStripItem[] { StripFile, StripEdit, aboutToolStripMenuItem });
            menuStrip1.Location = new Point(0, 0);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Size = new Size(575, 24);
            menuStrip1.TabIndex = 0;
            menuStrip1.Text = "menuStrip1";
            // 
            // StripFile
            // 
            StripFile.DropDownItems.AddRange(new ToolStripItem[] { StripFileOpen, StripFileFromFolder, toolStripSeparator5, StripFileSave, StripFileSaveAs, toolStripSeparator4, StripFileExit });
            StripFile.Image = Properties.Resources.UIStripFile;
            StripFile.Name = "StripFile";
            StripFile.Size = new Size(53, 20);
            StripFile.Text = "File";
            // 
            // StripFileOpen
            // 
            StripFileOpen.Image = Properties.Resources.UIFileOpen;
            StripFileOpen.Name = "StripFileOpen";
            StripFileOpen.Size = new Size(180, 22);
            StripFileOpen.Text = "Open";
            StripFileOpen.Click += StripFileOpen_Click;
            // 
            // StripFileFromFolder
            // 
            StripFileFromFolder.Name = "StripFileFromFolder";
            StripFileFromFolder.Size = new Size(180, 22);
            StripFileFromFolder.Text = "Create From Folder";
            StripFileFromFolder.Click += StripFileFromFolder_Click;
            // 
            // toolStripSeparator5
            // 
            toolStripSeparator5.Name = "toolStripSeparator5";
            toolStripSeparator5.Size = new Size(177, 6);
            // 
            // StripFileSave
            // 
            StripFileSave.Enabled = false;
            StripFileSave.Image = Properties.Resources.UIFileSave;
            StripFileSave.Name = "StripFileSave";
            StripFileSave.Size = new Size(180, 22);
            StripFileSave.Text = "Save";
            StripFileSave.Click += StripFileSave_Click;
            // 
            // StripFileSaveAs
            // 
            StripFileSaveAs.Enabled = false;
            StripFileSaveAs.Name = "StripFileSaveAs";
            StripFileSaveAs.Size = new Size(180, 22);
            StripFileSaveAs.Text = "Save as...";
            StripFileSaveAs.Click += StripFileSaveAs_Click;
            // 
            // toolStripSeparator4
            // 
            toolStripSeparator4.Name = "toolStripSeparator4";
            toolStripSeparator4.Size = new Size(177, 6);
            // 
            // StripFileExit
            // 
            StripFileExit.Image = Properties.Resources.UIExit;
            StripFileExit.Name = "StripFileExit";
            StripFileExit.Size = new Size(180, 22);
            StripFileExit.Text = "Exit";
            StripFileExit.Click += StripFileExit_Click;
            // 
            // StripEdit
            // 
            StripEdit.Enabled = false;
            StripEdit.Image = Properties.Resources.UIStripEdit;
            StripEdit.Name = "StripEdit";
            StripEdit.Size = new Size(55, 20);
            StripEdit.Text = "Edit";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] { StripAboutPZZARC });
            aboutToolStripMenuItem.Image = Properties.Resources.UIAbout;
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new Size(68, 20);
            aboutToolStripMenuItem.Text = "About";
            // 
            // StripAboutPZZARC
            // 
            StripAboutPZZARC.Name = "StripAboutPZZARC";
            StripAboutPZZARC.Size = new Size(123, 22);
            StripAboutPZZARC.Text = "PZZ-ARC";
            StripAboutPZZARC.Click += StripAboutPZZARC_Click;
            // 
            // FileTree
            // 
            FileTree.Dock = DockStyle.Fill;
            FileTree.Location = new Point(0, 0);
            FileTree.Name = "FileTree";
            FileTree.ShowNodeToolTips = true;
            FileTree.Size = new Size(180, 410);
            FileTree.TabIndex = 1;
            FileTree.NodeMouseClick += FileTree_NodeMouseClick;
            // 
            // ContextPZZInner
            // 
            ContextPZZInner.Items.AddRange(new ToolStripItem[] { ContextPZZExport, ContextPZZImport, toolStripSeparator1, ContextPZZStub, ContextPZZDelete, ContextPZZDuplicate, toolStripSeparator2, ContextPZZMoveUp, ContextPZZMoveDown });
            ContextPZZInner.Name = "ContextPZZInner";
            ContextPZZInner.Size = new Size(139, 170);
            // 
            // ContextPZZImport
            // 
            ContextPZZImport.Image = Properties.Resources.UIImport;
            ContextPZZImport.Name = "ContextPZZImport";
            ContextPZZImport.Size = new Size(138, 22);
            ContextPZZImport.Text = "Import";
            ContextPZZImport.Click += ContextPZZImport_Click;
            // 
            // ContextPZZExport
            // 
            ContextPZZExport.Image = Properties.Resources.UIExport;
            ContextPZZExport.Name = "ContextPZZExport";
            ContextPZZExport.Size = new Size(138, 22);
            ContextPZZExport.Text = "Export";
            ContextPZZExport.Click += ContextPZZExport_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(135, 6);
            // 
            // ContextPZZStub
            // 
            ContextPZZStub.Image = Properties.Resources.UIStub;
            ContextPZZStub.Name = "ContextPZZStub";
            ContextPZZStub.Size = new Size(138, 22);
            ContextPZZStub.Text = "Stub";
            ContextPZZStub.ToolTipText = "Keep the file on the filelist, but make the byte array empty";
            ContextPZZStub.Click += ContextPZZStub_Click;
            // 
            // ContextPZZDelete
            // 
            ContextPZZDelete.Image = Properties.Resources.UIDelete;
            ContextPZZDelete.Name = "ContextPZZDelete";
            ContextPZZDelete.Size = new Size(138, 22);
            ContextPZZDelete.Text = "Delete";
            ContextPZZDelete.Click += ContextPZZDelete_Click;
            // 
            // ContextPZZDuplicate
            // 
            ContextPZZDuplicate.Image = Properties.Resources.UIDuplicate;
            ContextPZZDuplicate.Name = "ContextPZZDuplicate";
            ContextPZZDuplicate.Size = new Size(138, 22);
            ContextPZZDuplicate.Text = "Duplicate";
            ContextPZZDuplicate.Click += ContextPZZDuplicate_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(135, 6);
            // 
            // ContextPZZMoveUp
            // 
            ContextPZZMoveUp.Image = Properties.Resources.UIMoveUp;
            ContextPZZMoveUp.Name = "ContextPZZMoveUp";
            ContextPZZMoveUp.Size = new Size(138, 22);
            ContextPZZMoveUp.Text = "Move Up";
            ContextPZZMoveUp.Click += ContextPZZMoveUp_Click;
            // 
            // ContextPZZMoveDown
            // 
            ContextPZZMoveDown.Image = Properties.Resources.UIMoveDown;
            ContextPZZMoveDown.Name = "ContextPZZMoveDown";
            ContextPZZMoveDown.Size = new Size(138, 22);
            ContextPZZMoveDown.Text = "Move Down";
            ContextPZZMoveDown.Click += ContextPZZMoveDown_Click;
            // 
            // FileProperties
            // 
            FileProperties.Dock = DockStyle.Fill;
            FileProperties.Location = new Point(0, 0);
            FileProperties.Name = "FileProperties";
            FileProperties.PropertySort = PropertySort.Categorized;
            FileProperties.Size = new Size(391, 410);
            FileProperties.TabIndex = 2;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 24);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(FileTree);
            splitContainer1.Panel1MinSize = 180;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(FileProperties);
            splitContainer1.Size = new Size(575, 410);
            splitContainer1.SplitterDistance = 180;
            splitContainer1.TabIndex = 3;
            // 
            // ContextPZZOuter
            // 
            ContextPZZOuter.Items.AddRange(new ToolStripItem[] { ContextPZZInsertFile, toolStripSeparator3, ContextPZZExportAll, ContextPZZImportAll });
            ContextPZZOuter.Name = "ContextPZZOuter";
            ContextPZZOuter.Size = new Size(128, 76);
            // 
            // ContextPZZInsertFile
            // 
            ContextPZZInsertFile.Image = Properties.Resources.UIInsert;
            ContextPZZInsertFile.Name = "ContextPZZInsertFile";
            ContextPZZInsertFile.Size = new Size(127, 22);
            ContextPZZInsertFile.Text = "Insert File";
            ContextPZZInsertFile.Click += ContextPZZInsertFile_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(124, 6);
            // 
            // ContextPZZExportAll
            // 
            ContextPZZExportAll.Image = Properties.Resources.UIExportAll;
            ContextPZZExportAll.Name = "ContextPZZExportAll";
            ContextPZZExportAll.Size = new Size(127, 22);
            ContextPZZExportAll.Text = "Export All";
            ContextPZZExportAll.Click += ContextPZZExportAll_Click;
            // 
            // ContextPZZImportAll
            // 
            ContextPZZImportAll.Image = Properties.Resources.UIImportAll;
            ContextPZZImportAll.Name = "ContextPZZImportAll";
            ContextPZZImportAll.Size = new Size(127, 22);
            ContextPZZImportAll.Text = "Import All";
            ContextPZZImportAll.Click += ContextPZZImportAll_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(575, 434);
            Controls.Add(splitContainer1);
            Controls.Add(menuStrip1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MainMenuStrip = menuStrip1;
            Name = "Form1";
            Text = "PZZ-ARC";
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            ContextPZZInner.ResumeLayout(false);
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ContextPZZOuter.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private MenuStrip menuStrip1;
        private ToolStripMenuItem StripFile;
        private ToolStripMenuItem StripFileOpen;
        private TreeView FileTree;
        private ToolStripMenuItem StripFileSave;
        private ToolStripMenuItem StripFileSaveAs;
        private ContextMenuStrip ContextPZZInner;
        private ToolStripMenuItem ContextPZZExport;
        private ToolStripMenuItem ContextPZZImport;
        private ToolStripMenuItem ContextPZZDuplicate;
        private ToolStripMenuItem ContextPZZDelete;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem ContextPZZMoveUp;
        private ToolStripMenuItem ContextPZZMoveDown;
        private PropertyGrid FileProperties;
        private SplitContainer splitContainer1;
        private ContextMenuStrip ContextPZZOuter;
        private ToolStripMenuItem ContextPZZExportAll;
        private ToolStripMenuItem ContextPZZImportAll;
        private ToolStripMenuItem ContextPZZInsertFile;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem StripEdit;
        private ToolStripSeparator toolStripSeparator4;
        private ToolStripMenuItem StripFileExit;
        private ToolStripMenuItem ContextPZZStub;
        private ToolStripMenuItem aboutToolStripMenuItem;
        private ToolStripMenuItem StripAboutPZZARC;
        private ToolStripMenuItem StripFileFromFolder;
        private ToolStripSeparator toolStripSeparator5;
    }
}