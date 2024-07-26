namespace TXBeditor
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            toolStrip1 = new ToolStrip();
            StripFile = new ToolStripDropDownButton();
            StripFileOpen = new ToolStripMenuItem();
            toolStripSeparator2 = new ToolStripSeparator();
            StripFileSave = new ToolStripMenuItem();
            StripFileSaveAs = new ToolStripMenuItem();
            toolStripSeparator1 = new ToolStripSeparator();
            StripFileQuit = new ToolStripMenuItem();
            StripEdit = new ToolStripDropDownButton();
            StripEditExport = new ToolStripMenuItem();
            StripEditImport = new ToolStripMenuItem();
            toolStripSeparator3 = new ToolStripSeparator();
            StripEditExportAll = new ToolStripMenuItem();
            StripEditImportAll = new ToolStripMenuItem();
            StripHelp = new ToolStripDropDownButton();
            StripHelpImgLib = new ToolStripMenuItem();
            toolTip1 = new ToolTip(components);
            ImageListPushUp = new Button();
            ImageListRemove = new Button();
            ImageListPushDown = new Button();
            ImageListAdd = new Button();
            ImageListSave = new Button();
            StripBGContextMenu = new ContextMenuStrip(components);
            BGColorStripDefault = new ToolStripMenuItem();
            BGColorStripCustom = new ToolStripMenuItem();
            GroupBoxImageList = new GroupBox();
            ImageListView = new ListView();
            image_index = new ColumnHeader();
            image_id = new ColumnHeader();
            GroupBoxTXB = new GroupBox();
            CurrImgIDField = new NumericUpDown();
            label1 = new Label();
            GroupBoxTIM2 = new GroupBox();
            BPPLabel = new Label();
            AlignmentLabel = new Label();
            ComboAlignment = new ComboBox();
            ComboBitDepth = new ComboBox();
            GroupBoxView = new GroupBox();
            ViewZoomReset = new Button();
            ViewZoomSubs = new Button();
            ViewZoomAdd = new Button();
            BGColorButton = new Button();
            TIM2PictureBox = new PictureBox();
            splitContainer1 = new SplitContainer();
            splitContainer2 = new SplitContainer();
            toolStrip1.SuspendLayout();
            StripBGContextMenu.SuspendLayout();
            GroupBoxImageList.SuspendLayout();
            GroupBoxTXB.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)CurrImgIDField).BeginInit();
            GroupBoxTIM2.SuspendLayout();
            GroupBoxView.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)TIM2PictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer2).BeginInit();
            splitContainer2.Panel1.SuspendLayout();
            splitContainer2.Panel2.SuspendLayout();
            splitContainer2.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.Items.AddRange(new ToolStripItem[] { StripFile, StripEdit, StripHelp });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(752, 25);
            toolStrip1.TabIndex = 17;
            toolStrip1.Text = "toolStrip1";
            // 
            // StripFile
            // 
            StripFile.AutoToolTip = false;
            StripFile.DropDownItems.AddRange(new ToolStripItem[] { StripFileOpen, toolStripSeparator2, StripFileSave, StripFileSaveAs, toolStripSeparator1, StripFileQuit });
            StripFile.Image = PZZ_ARC.Properties.Resources.UIStripFile;
            StripFile.ImageTransparentColor = Color.Magenta;
            StripFile.Name = "StripFile";
            StripFile.Size = new Size(54, 22);
            StripFile.Text = "File";
            // 
            // StripFileOpen
            // 
            StripFileOpen.Image = PZZ_ARC.Properties.Resources.UIFileOpen;
            StripFileOpen.Name = "StripFileOpen";
            StripFileOpen.Size = new Size(180, 22);
            StripFileOpen.Text = "Open...";
            StripFileOpen.Click += StripFileOpen_Click;
            // 
            // toolStripSeparator2
            // 
            toolStripSeparator2.Name = "toolStripSeparator2";
            toolStripSeparator2.Size = new Size(177, 6);
            // 
            // StripFileSave
            // 
            StripFileSave.Enabled = false;
            StripFileSave.Image = PZZ_ARC.Properties.Resources.UIFileSave;
            StripFileSave.Name = "StripFileSave";
            StripFileSave.Size = new Size(180, 22);
            StripFileSave.Text = "Save to PZZ";
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
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(177, 6);
            // 
            // StripFileQuit
            // 
            StripFileQuit.Image = PZZ_ARC.Properties.Resources.UIExit;
            StripFileQuit.Name = "StripFileQuit";
            StripFileQuit.Size = new Size(180, 22);
            StripFileQuit.Text = "Quit";
            StripFileQuit.Click += StripFileQuit_Click;
            // 
            // StripEdit
            // 
            StripEdit.AutoToolTip = false;
            StripEdit.DropDownItems.AddRange(new ToolStripItem[] { StripEditExport, StripEditImport, toolStripSeparator3, StripEditExportAll, StripEditImportAll });
            StripEdit.Image = PZZ_ARC.Properties.Resources.TXBEd_picture_edit;
            StripEdit.ImageTransparentColor = Color.Magenta;
            StripEdit.Name = "StripEdit";
            StripEdit.Size = new Size(56, 22);
            StripEdit.Text = "Edit";
            // 
            // StripEditExport
            // 
            StripEditExport.Enabled = false;
            StripEditExport.Image = (Image)resources.GetObject("StripEditExport.Image");
            StripEditExport.Name = "StripEditExport";
            StripEditExport.Size = new Size(166, 22);
            StripEditExport.Text = "Export to PNG";
            StripEditExport.Click += StripEditExport_Click;
            // 
            // StripEditImport
            // 
            StripEditImport.Enabled = false;
            StripEditImport.Image = (Image)resources.GetObject("StripEditImport.Image");
            StripEditImport.Name = "StripEditImport";
            StripEditImport.Size = new Size(166, 22);
            StripEditImport.Text = "Import from PNG";
            StripEditImport.Click += StripEditImport_Click;
            // 
            // toolStripSeparator3
            // 
            toolStripSeparator3.Name = "toolStripSeparator3";
            toolStripSeparator3.Size = new Size(163, 6);
            // 
            // StripEditExportAll
            // 
            StripEditExportAll.Enabled = false;
            StripEditExportAll.Image = PZZ_ARC.Properties.Resources.TXBEd_folder_picture;
            StripEditExportAll.Name = "StripEditExportAll";
            StripEditExportAll.Size = new Size(166, 22);
            StripEditExportAll.Text = "Batch Export";
            StripEditExportAll.Click += StripEditExportAll_Click;
            // 
            // StripEditImportAll
            // 
            StripEditImportAll.Enabled = false;
            StripEditImportAll.Image = PZZ_ARC.Properties.Resources.TXBEd_folder_magnify;
            StripEditImportAll.Name = "StripEditImportAll";
            StripEditImportAll.Size = new Size(166, 22);
            StripEditImportAll.Text = "Batch Import";
            StripEditImportAll.Click += EditStripImportAll_Click;
            // 
            // StripHelp
            // 
            StripHelp.AutoToolTip = false;
            StripHelp.DropDownItems.AddRange(new ToolStripItem[] { StripHelpImgLib });
            StripHelp.Image = PZZ_ARC.Properties.Resources.UIAbout;
            StripHelp.ImageTransparentColor = Color.Magenta;
            StripHelp.Name = "StripHelp";
            StripHelp.Size = new Size(61, 22);
            StripHelp.Text = "Help";
            // 
            // StripHelpImgLib
            // 
            StripHelpImgLib.Name = "StripHelpImgLib";
            StripHelpImgLib.Size = new Size(147, 22);
            StripHelpImgLib.Text = "About ImgLib";
            StripHelpImgLib.Click += StripHelpImgLib_Click;
            // 
            // ImageListPushUp
            // 
            ImageListPushUp.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ImageListPushUp.Image = PZZ_ARC.Properties.Resources.UIMoveUp;
            ImageListPushUp.Location = new Point(203, 184);
            ImageListPushUp.Name = "ImageListPushUp";
            ImageListPushUp.Size = new Size(26, 50);
            ImageListPushUp.TabIndex = 9;
            toolTip1.SetToolTip(ImageListPushUp, "Move the currently selected texture up");
            ImageListPushUp.UseVisualStyleBackColor = true;
            ImageListPushUp.Click += ImageListPushedUp;
            // 
            // ImageListRemove
            // 
            ImageListRemove.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ImageListRemove.Image = PZZ_ARC.Properties.Resources.TXBEd_image_delete;
            ImageListRemove.Location = new Point(203, 70);
            ImageListRemove.Name = "ImageListRemove";
            ImageListRemove.Size = new Size(26, 50);
            ImageListRemove.TabIndex = 14;
            ImageListRemove.Text = "-";
            toolTip1.SetToolTip(ImageListRemove, "Remove the currently selected texture from the image list");
            ImageListRemove.UseVisualStyleBackColor = true;
            ImageListRemove.Click += OnImageListRemoved;
            // 
            // ImageListPushDown
            // 
            ImageListPushDown.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            ImageListPushDown.Image = PZZ_ARC.Properties.Resources.UIMoveDown;
            ImageListPushDown.Location = new Point(203, 240);
            ImageListPushDown.Name = "ImageListPushDown";
            ImageListPushDown.Size = new Size(26, 50);
            ImageListPushDown.TabIndex = 10;
            toolTip1.SetToolTip(ImageListPushDown, "Move the currently selected texture down");
            ImageListPushDown.UseVisualStyleBackColor = true;
            ImageListPushDown.Click += ImageListPushedDown;
            // 
            // ImageListAdd
            // 
            ImageListAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ImageListAdd.Image = PZZ_ARC.Properties.Resources.TXBEd_image_add;
            ImageListAdd.Location = new Point(203, 14);
            ImageListAdd.Name = "ImageListAdd";
            ImageListAdd.Size = new Size(26, 50);
            ImageListAdd.TabIndex = 13;
            toolTip1.SetToolTip(ImageListAdd, "Add a new TIM2 (.tm2) texture to the TXB image list");
            ImageListAdd.UseVisualStyleBackColor = true;
            ImageListAdd.Click += OnImageListAdd;
            // 
            // ImageListSave
            // 
            ImageListSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ImageListSave.Image = (Image)resources.GetObject("ImageListSave.Image");
            ImageListSave.Location = new Point(203, 126);
            ImageListSave.Name = "ImageListSave";
            ImageListSave.Size = new Size(26, 50);
            ImageListSave.TabIndex = 15;
            ImageListSave.Text = "-";
            toolTip1.SetToolTip(ImageListSave, "Save the currently selected image as a TIM2 (.tm2) file");
            ImageListSave.UseVisualStyleBackColor = true;
            ImageListSave.Click += OnImageListSave;
            // 
            // StripBGContextMenu
            // 
            StripBGContextMenu.Items.AddRange(new ToolStripItem[] { BGColorStripDefault, BGColorStripCustom });
            StripBGContextMenu.Name = "StripBGContextMenu";
            StripBGContextMenu.Size = new Size(145, 48);
            // 
            // BGColorStripDefault
            // 
            BGColorStripDefault.Image = PZZ_ARC.Properties.Resources.TXBEd_arrow_undo;
            BGColorStripDefault.Name = "BGColorStripDefault";
            BGColorStripDefault.Size = new Size(144, 22);
            BGColorStripDefault.Text = "Default Color";
            BGColorStripDefault.Click += BGColorStripDefault_Click;
            // 
            // BGColorStripCustom
            // 
            BGColorStripCustom.Image = PZZ_ARC.Properties.Resources.TXBEd_color_swatch;
            BGColorStripCustom.Name = "BGColorStripCustom";
            BGColorStripCustom.Size = new Size(144, 22);
            BGColorStripCustom.Text = "Custom...";
            BGColorStripCustom.Click += BGColorStripCustom_Click;
            // 
            // GroupBoxImageList
            // 
            GroupBoxImageList.AutoSize = true;
            GroupBoxImageList.Controls.Add(ImageListSave);
            GroupBoxImageList.Controls.Add(ImageListView);
            GroupBoxImageList.Controls.Add(ImageListAdd);
            GroupBoxImageList.Controls.Add(ImageListPushDown);
            GroupBoxImageList.Controls.Add(ImageListRemove);
            GroupBoxImageList.Controls.Add(ImageListPushUp);
            GroupBoxImageList.Dock = DockStyle.Fill;
            GroupBoxImageList.Enabled = false;
            GroupBoxImageList.Location = new Point(3, 0);
            GroupBoxImageList.Name = "GroupBoxImageList";
            GroupBoxImageList.Size = new Size(233, 302);
            GroupBoxImageList.TabIndex = 18;
            GroupBoxImageList.TabStop = false;
            // 
            // ImageListView
            // 
            ImageListView.AllowColumnReorder = true;
            ImageListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            ImageListView.Columns.AddRange(new ColumnHeader[] { image_index, image_id });
            ImageListView.FullRowSelect = true;
            ImageListView.GridLines = true;
            ImageListView.Location = new Point(6, 14);
            ImageListView.MultiSelect = false;
            ImageListView.Name = "ImageListView";
            ImageListView.Size = new Size(191, 276);
            ImageListView.TabIndex = 1;
            ImageListView.UseCompatibleStateImageBehavior = false;
            ImageListView.View = View.Details;
            ImageListView.ItemSelectionChanged += OnImageItemSelection;
            // 
            // image_index
            // 
            image_index.Text = "Index";
            image_index.Width = 99;
            // 
            // image_id
            // 
            image_id.Text = "Load ID";
            image_id.Width = 88;
            // 
            // GroupBoxTXB
            // 
            GroupBoxTXB.Controls.Add(CurrImgIDField);
            GroupBoxTXB.Controls.Add(label1);
            GroupBoxTXB.Enabled = false;
            GroupBoxTXB.Location = new Point(3, 7);
            GroupBoxTXB.Name = "GroupBoxTXB";
            GroupBoxTXB.Size = new Size(230, 51);
            GroupBoxTXB.TabIndex = 15;
            GroupBoxTXB.TabStop = false;
            GroupBoxTXB.Text = "TXB Specific";
            // 
            // CurrImgIDField
            // 
            CurrImgIDField.Location = new Point(77, 22);
            CurrImgIDField.Maximum = new decimal(new int[] { int.MaxValue, 0, 0, 0 });
            CurrImgIDField.Minimum = new decimal(new int[] { int.MinValue, 0, 0, int.MinValue });
            CurrImgIDField.Name = "CurrImgIDField";
            CurrImgIDField.Size = new Size(147, 23);
            CurrImgIDField.TabIndex = 8;
            CurrImgIDField.ValueChanged += OnCurrentImageIDChanged;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(8, 24);
            label1.Name = "label1";
            label1.Size = new Size(47, 15);
            label1.TabIndex = 6;
            label1.Text = "Load ID";
            // 
            // GroupBoxTIM2
            // 
            GroupBoxTIM2.Controls.Add(BPPLabel);
            GroupBoxTIM2.Controls.Add(AlignmentLabel);
            GroupBoxTIM2.Controls.Add(ComboAlignment);
            GroupBoxTIM2.Controls.Add(ComboBitDepth);
            GroupBoxTIM2.Enabled = false;
            GroupBoxTIM2.Location = new Point(3, 62);
            GroupBoxTIM2.Name = "GroupBoxTIM2";
            GroupBoxTIM2.Size = new Size(230, 80);
            GroupBoxTIM2.TabIndex = 16;
            GroupBoxTIM2.TabStop = false;
            GroupBoxTIM2.Text = "TIM2 Specific";
            // 
            // BPPLabel
            // 
            BPPLabel.AutoSize = true;
            BPPLabel.Location = new Point(8, 53);
            BPPLabel.Name = "BPPLabel";
            BPPLabel.Size = new Size(56, 15);
            BPPLabel.TabIndex = 3;
            BPPLabel.Text = "Bit Depth";
            // 
            // AlignmentLabel
            // 
            AlignmentLabel.AutoSize = true;
            AlignmentLabel.Location = new Point(8, 24);
            AlignmentLabel.Name = "AlignmentLabel";
            AlignmentLabel.Size = new Size(89, 15);
            AlignmentLabel.TabIndex = 2;
            AlignmentLabel.Text = "Byte Alignment";
            // 
            // ComboAlignment
            // 
            ComboAlignment.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboAlignment.Enabled = false;
            ComboAlignment.FormattingEnabled = true;
            ComboAlignment.Items.AddRange(new object[] { "16 Bytes", "128 Bytes" });
            ComboAlignment.Location = new Point(121, 21);
            ComboAlignment.Name = "ComboAlignment";
            ComboAlignment.Size = new Size(103, 23);
            ComboAlignment.TabIndex = 1;
            ComboAlignment.SelectionChangeCommitted += ComboAlignmentChange;
            // 
            // ComboBitDepth
            // 
            ComboBitDepth.DropDownStyle = ComboBoxStyle.DropDownList;
            ComboBitDepth.Enabled = false;
            ComboBitDepth.FormattingEnabled = true;
            ComboBitDepth.Items.AddRange(new object[] { "4 BPP", "8 BPP", "16 BPP", "24 BPP", "32 BPP" });
            ComboBitDepth.Location = new Point(121, 50);
            ComboBitDepth.Name = "ComboBitDepth";
            ComboBitDepth.Size = new Size(103, 23);
            ComboBitDepth.TabIndex = 0;
            ComboBitDepth.SelectedIndexChanged += ComboBPPChanged;
            // 
            // GroupBoxView
            // 
            GroupBoxView.Controls.Add(ViewZoomReset);
            GroupBoxView.Controls.Add(ViewZoomSubs);
            GroupBoxView.Controls.Add(ViewZoomAdd);
            GroupBoxView.Controls.Add(BGColorButton);
            GroupBoxView.Enabled = false;
            GroupBoxView.Location = new Point(3, 145);
            GroupBoxView.Name = "GroupBoxView";
            GroupBoxView.Size = new Size(230, 57);
            GroupBoxView.TabIndex = 19;
            GroupBoxView.TabStop = false;
            GroupBoxView.Text = "View";
            // 
            // ViewZoomReset
            // 
            ViewZoomReset.Image = PZZ_ARC.Properties.Resources.TXBEd_zoom;
            ViewZoomReset.Location = new Point(169, 22);
            ViewZoomReset.Name = "ViewZoomReset";
            ViewZoomReset.Size = new Size(26, 26);
            ViewZoomReset.TabIndex = 3;
            ViewZoomReset.UseVisualStyleBackColor = true;
            ViewZoomReset.Click += ViewZoomReset_Click;
            // 
            // ViewZoomSubs
            // 
            ViewZoomSubs.Image = PZZ_ARC.Properties.Resources.TXBEd_zoom_out;
            ViewZoomSubs.Location = new Point(139, 22);
            ViewZoomSubs.Name = "ViewZoomSubs";
            ViewZoomSubs.Size = new Size(26, 26);
            ViewZoomSubs.TabIndex = 2;
            ViewZoomSubs.UseVisualStyleBackColor = true;
            ViewZoomSubs.Click += ViewZoomSubs_Click;
            // 
            // ViewZoomAdd
            // 
            ViewZoomAdd.Image = PZZ_ARC.Properties.Resources.TXBEd_zoom_in;
            ViewZoomAdd.Location = new Point(198, 22);
            ViewZoomAdd.Name = "ViewZoomAdd";
            ViewZoomAdd.Size = new Size(26, 26);
            ViewZoomAdd.TabIndex = 1;
            ViewZoomAdd.UseVisualStyleBackColor = true;
            ViewZoomAdd.Click += ViewZoomAdd_Click;
            // 
            // BGColorButton
            // 
            BGColorButton.Location = new Point(6, 22);
            BGColorButton.Name = "BGColorButton";
            BGColorButton.Size = new Size(117, 26);
            BGColorButton.TabIndex = 0;
            BGColorButton.Text = "Background Color";
            BGColorButton.UseVisualStyleBackColor = true;
            BGColorButton.Click += BGColorButton_Click;
            // 
            // TIM2PictureBox
            // 
            TIM2PictureBox.Dock = DockStyle.Fill;
            TIM2PictureBox.Location = new Point(0, 0);
            TIM2PictureBox.Margin = new Padding(5);
            TIM2PictureBox.Name = "TIM2PictureBox";
            TIM2PictureBox.Padding = new Padding(0, 0, 5, 5);
            TIM2PictureBox.Size = new Size(512, 511);
            TIM2PictureBox.TabIndex = 3;
            TIM2PictureBox.TabStop = false;
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.FixedPanel = FixedPanel.Panel1;
            splitContainer1.IsSplitterFixed = true;
            splitContainer1.Location = new Point(0, 25);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(splitContainer2);
            splitContainer1.Panel1MinSize = 236;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(TIM2PictureBox);
            splitContainer1.Panel2.Margin = new Padding(0, 0, 5, 5);
            splitContainer1.Size = new Size(752, 511);
            splitContainer1.SplitterDistance = 236;
            splitContainer1.TabIndex = 20;
            // 
            // splitContainer2
            // 
            splitContainer2.Dock = DockStyle.Fill;
            splitContainer2.FixedPanel = FixedPanel.Panel2;
            splitContainer2.IsSplitterFixed = true;
            splitContainer2.Location = new Point(0, 0);
            splitContainer2.Name = "splitContainer2";
            splitContainer2.Orientation = Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            splitContainer2.Panel1.Controls.Add(GroupBoxImageList);
            splitContainer2.Panel1.Padding = new Padding(3, 0, 0, 0);
            splitContainer2.Panel1MinSize = 30;
            // 
            // splitContainer2.Panel2
            // 
            splitContainer2.Panel2.Controls.Add(GroupBoxTXB);
            splitContainer2.Panel2.Controls.Add(GroupBoxTIM2);
            splitContainer2.Panel2.Controls.Add(GroupBoxView);
            splitContainer2.Panel2MinSize = 205;
            splitContainer2.Size = new Size(236, 511);
            splitContainer2.SplitterDistance = 302;
            splitContainer2.TabIndex = 0;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            ClientSize = new Size(752, 536);
            Controls.Add(splitContainer1);
            Controls.Add(toolStrip1);
            MinimumSize = new Size(768, 573);
            Name = "Form1";
            Text = " TXB Editor";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            StripBGContextMenu.ResumeLayout(false);
            GroupBoxImageList.ResumeLayout(false);
            GroupBoxTXB.ResumeLayout(false);
            GroupBoxTXB.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)CurrImgIDField).EndInit();
            GroupBoxTIM2.ResumeLayout(false);
            GroupBoxTIM2.PerformLayout();
            GroupBoxView.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)TIM2PictureBox).EndInit();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            splitContainer2.Panel1.ResumeLayout(false);
            splitContainer2.Panel1.PerformLayout();
            splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer2).EndInit();
            splitContainer2.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private ToolStrip toolStrip1;
        private ToolStripDropDownButton StripFile;
        private ToolStripMenuItem StripFileOpen;
        private ToolStripDropDownButton StripEdit;
        private ToolStripDropDownButton StripHelp;
        private ToolStripSeparator toolStripSeparator2;
        private ToolStripMenuItem StripFileSave;
        private ToolStripMenuItem StripFileSaveAs;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripMenuItem StripFileQuit;
        private ToolStripMenuItem StripEditExport;
        private ToolStripMenuItem StripEditImport;
        private ToolStripMenuItem StripHelpImgLib;
        private ToolStripSeparator toolStripSeparator3;
        private ToolStripMenuItem StripEditExportAll;
        private ToolStripMenuItem StripEditImportAll;
        private ToolTip toolTip1;
        private ContextMenuStrip StripBGContextMenu;
        private ToolStripMenuItem BGColorStripDefault;
        private ToolStripMenuItem BGColorStripCustom;
        private GroupBox GroupBoxImageList;
        private Button ImageListSave;
        private ListView ImageListView;
        private ColumnHeader image_index;
        private ColumnHeader image_id;
        private Button ImageListAdd;
        private Button ImageListPushDown;
        private Button ImageListRemove;
        private Button ImageListPushUp;
        private GroupBox GroupBoxTXB;
        private NumericUpDown CurrImgIDField;
        private Label label1;
        private GroupBox GroupBoxTIM2;
        private Label BPPLabel;
        private Label AlignmentLabel;
        private ComboBox ComboAlignment;
        private ComboBox ComboBitDepth;
        private GroupBox GroupBoxView;
        private Button ViewZoomReset;
        private Button ViewZoomSubs;
        private Button ViewZoomAdd;
        private Button BGColorButton;
        private PictureBox TIM2PictureBox;
        private SplitContainer splitContainer1;
        private SplitContainer splitContainer2;
    }
}
