using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using AFSLib;
using PZZ_ARC.PZZArc;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static PZZ_ARC.PZZArc.PZZ;

namespace PZZ_ARC
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

        }

        string input_file = "";
        string output_file = "";
        AFS current_afs = new AFS();
        List<PZZFile> file_list = new List<PZZFile>();
        readonly OpenFileDialog ofd = new OpenFileDialog();
        readonly SaveFileDialog sfd = new SaveFileDialog();
        readonly FolderPicker ffd = new FolderPicker();


        public void ReplaceBufferAtSelected(byte[] new_buffer)
        {
            int index = FileTree.SelectedNode.Index;
            ReplaceFileOnList(file_list, index, new_buffer);
            BuildTree(file_list);
            FileTree.SelectedNode = FileTree.Nodes[0].Nodes[index];
            UpdatePropertyGrid();
        }

        public void LoadFileFromAFS(int file_index)
        {
            try
            {
                StreamEntry afs_file = current_afs.Entries[file_index] as StreamEntry;
                Stream filestream = afs_file.GetStream();
                file_list.Clear();
                UnpackFromStream(filestream, file_list);
                BuildTree(file_list, afs_file.Name);
                StripFileSave.Enabled = false;
                StripFileSaveAs.Enabled = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading from AFS.\n" + ex, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void BuildTree(List<PZZFile> file_list)
        {
            FileTree.Nodes.Clear();
            string filename = Path.GetFileName(input_file);

            // add icons to image list, should move this elsewhere
            ImageList IconList = new ImageList();
            IconList.Images.Add("FileRoot", Properties.Resources.RootICO);
            IconList.Images.Add("ModelData", Properties.Resources.ModelICO);
            IconList.Images.Add("SkeletonData", Properties.Resources.SkeletonICO);
            IconList.Images.Add("TextureData", Properties.Resources.TextureICO);
            IconList.Images.Add("AnimationData", Properties.Resources.AnimationICO);
            IconList.Images.Add("ShadowData", Properties.Resources.ShadowICO);
            IconList.Images.Add("CollisionData", Properties.Resources.CollisionICO);
            IconList.Images.Add("StageCollisionData", Properties.Resources.StageCollisionICO);
            IconList.Images.Add("TextData", Properties.Resources.TextICO);
            IconList.Images.Add("Unknown", Properties.Resources.UnknownICO);
            IconList.Images.Add("UIStub", Properties.Resources.UIStub);
            FileTree.ImageList = IconList;

            // create root, pzz itself
            TreeNode rootNode = new TreeNode();
            rootNode.Text = filename;
            rootNode.ImageIndex = 0;
            FileTree.Nodes.Add(rootNode);

            for (int i = 0; i < file_list.Count; i++)
            {
                var file = file_list[i];
                TreeNode fileNode = new TreeNode();
                fileNode.Text = "File " + i.ToString("D3") + " (." + GetFileExtension(file.type) + ")"; // name

                if (file.byte_array.Count() > 0)
                {
                    fileNode.ImageKey = file.type;
                    fileNode.SelectedImageKey = file.type;
                }
                else // if file is empty
                {
                    fileNode.ImageKey = "UIStub";
                    fileNode.SelectedImageKey = "UIStub";
                    fileNode.ToolTipText = "Empty Data";
                }
                FileTree.Nodes[rootNode.Index].Nodes.Add(fileNode); // add to root
            }
            rootNode.Expand();
        }

        private void BuildTree(List<PZZFile> file_list, string root_name) // for afs load
        {
            FileTree.Nodes.Clear();
            string filename = root_name;

            // add icons to image list, should move this elsewhere
            ImageList IconList = new ImageList();
            IconList.Images.Add("FileRoot", Properties.Resources.RootICO);
            IconList.Images.Add("ModelData", Properties.Resources.ModelICO);
            IconList.Images.Add("SkeletonData", Properties.Resources.SkeletonICO);
            IconList.Images.Add("TextureData", Properties.Resources.TextureICO);
            IconList.Images.Add("AnimationData", Properties.Resources.AnimationICO);
            IconList.Images.Add("ShadowData", Properties.Resources.ShadowICO);
            IconList.Images.Add("CollisionData", Properties.Resources.CollisionICO);
            IconList.Images.Add("StageCollisionData", Properties.Resources.StageCollisionICO);
            IconList.Images.Add("TextData", Properties.Resources.TextICO);
            IconList.Images.Add("Unknown", Properties.Resources.UnknownICO);
            IconList.Images.Add("UIStub", Properties.Resources.UIStub);
            FileTree.ImageList = IconList;

            // create root, pzz itself
            TreeNode rootNode = new TreeNode();
            rootNode.Text = filename;
            rootNode.ImageIndex = 0;
            FileTree.Nodes.Add(rootNode);

            for (int i = 0; i < file_list.Count; i++)
            {
                var file = file_list[i];
                TreeNode fileNode = new TreeNode();
                fileNode.Text = "File " + i.ToString("D3") + " (." + GetFileExtension(file.type) + ")"; // name

                if (file.byte_array.Count() > 0)
                {
                    fileNode.ImageKey = file.type;
                    fileNode.SelectedImageKey = file.type;
                }
                else // if file is empty
                {
                    fileNode.ImageKey = "UIStub";
                    fileNode.SelectedImageKey = "UIStub";
                    fileNode.ToolTipText = "Empty Data";
                }
                FileTree.Nodes[rootNode.Index].Nodes.Add(fileNode); // add to root
            }
            rootNode.Expand();
        }

        private void StripFileOpen_Click(object sender, EventArgs e)
        {
            ofd.Title = "Select PZZ File";
            ofd.Filter = "PZZ Files|*.pzz";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                input_file = ofd.FileName;

                file_list.Clear();
                file_list = UnpackFromFile(input_file);
                BuildTree(file_list);
                StripFileSave.Enabled = true;
                StripFileSaveAs.Enabled = true;
            }

        }
        private void StripFileSave_Click(object sender, EventArgs e)
        {
            if (input_file is null)
            {
                MessageBox.Show("The ouput path for the file is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            using (var stream = File.Open(input_file, FileMode.OpenOrCreate))
            {
                List<byte> output_data = new List<byte>();

                using (var writer = new BinaryWriter(stream))
                {
                    WriteOutputData(writer, stream, file_list);
                }
            }
        }
        private void StripFileSaveAs_Click(object sender, EventArgs e)
        {
            string initial_output_name = input_file;
            sfd.Title = "Save PZZ File";
            sfd.Filter = "PZZ Files|*.pzz";
            sfd.FileName = initial_output_name;
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                output_file = sfd.FileName;

                if (output_file is null)
                {
                    MessageBox.Show("The ouput path for the file is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var stream = File.Open(output_file, FileMode.Create))
                {
                    List<byte> output_data = new List<byte>();

                    using (var writer = new BinaryWriter(stream))
                    {
                        WriteOutputData(writer, stream, file_list);
                    }
                }
            }
        }
        private void UpdatePropertyGrid()
        {
            if (FileTree.SelectedNode != null)
            {
                if (FileTree.SelectedNode.Level == 1)
                {
                    PZZFile current_file = file_list[FileTree.SelectedNode.Index];
                    FileProperties.SelectedObject = current_file;
                }
            }
            else
            {
                FileProperties.SelectedObject = null;
            }
        }
        private void FileTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {


            if (e.Button == MouseButtons.Right) // context menu for right clicking file
            {
                FileTree.SelectedNode = e.Node;
            }

            if (e.Node.Level == 1)
            {
                FileTree.SelectedNode = e.Node;
                e.Node.ContextMenuStrip = ContextPZZInner; // context menu for files inside pzz
                StripEdit.DropDown = ContextPZZInner; // for toolbar edit menu
                SetTXBModifyVisibility(file_list[e.Node.Index].type == "TextureData"); //show txb option if selected file is txb
            }
            else if (e.Node.Level == 0)
            {
                FileTree.SelectedNode = e.Node;
                e.Node.ContextMenuStrip = ContextPZZOuter; // context menu for whole file archive (export all / import all)
                StripEdit.DropDown = ContextPZZOuter; // for toolbar edit menu
            }

            UpdatePropertyGrid();

            if (FileTree.Nodes[0].Nodes.Count < 2) ContextPZZDelete.Enabled = false;
            else ContextPZZDelete.Enabled = true;

            if (FileTree.SelectedNode != null) StripEdit.Enabled = true; // update edit button at the top
            else StripEdit.Enabled = false;

        }

        private void ContextPZZDelete_Click(object sender, EventArgs e)
        {

            int index = FileTree.SelectedNode.Index;

            file_list.Remove(file_list.ElementAt(index));
            BuildTree(file_list);
            if (index > file_list.Count) FileTree.SelectedNode = FileTree.Nodes[0].Nodes[index];
            else if (index == file_list.Count) FileTree.SelectedNode = FileTree.Nodes[0].Nodes[index - 1];
            else if (index < file_list.Count && (index - 1) != -1) FileTree.SelectedNode = FileTree.Nodes[0].Nodes[index - 1];
            else FileTree.SelectedNode = FileTree.Nodes[0].Nodes[0];

        }

        private void ContextPZZDuplicate_Click(object sender, EventArgs e)
        {
            int index = FileTree.SelectedNode.Index;
            file_list.Insert(index, file_list.ElementAt(index));
            BuildTree(file_list);
            FileTree.SelectedNode = FileTree.Nodes[0].Nodes[index];
        }

        private void MoveFileList(int difference)
        {
            if (FileTree.SelectedNode != null && FileTree.Nodes[0].Nodes.Count > 1)
            {
                int new_index = FileTree.SelectedNode.Index + difference;

                if (new_index >= 0 && new_index < FileTree.Nodes[0].Nodes.Count)
                {
                    PZZFile selected = file_list.ElementAt(FileTree.SelectedNode.Index);

                    try
                    {
                        file_list.Remove(selected);
                        file_list.Insert(new_index, selected);
                        BuildTree(file_list);
                        FileTree.SelectedNode = FileTree.Nodes[0].Nodes[new_index];
                    }
                    catch (Exception e)  // try to restore old file position
                    {
                        MessageBox.Show("Error while moving file list!\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        file_list.Insert(FileTree.SelectedNode.Index, selected);
                        BuildTree(file_list);
                        FileTree.SelectedNode = FileTree.Nodes[0].Nodes[00];
                    }
                }
            }
        }

        private void ContextPZZMoveUp_Click(object sender, EventArgs e)
        {
            MoveFileList(-1);
        }

        private void ContextPZZMoveDown_Click(object sender, EventArgs e)
        {
            MoveFileList(1);
        }

        private void SetTXBModifyVisibility(bool value)
        {
            ContextPZZModifySeparator.Visible = value;
            ContextPZZModifyTXB.Visible = value;
        }

        private void ContextPZZExport_Click(object sender, EventArgs e)
        {
            int index = FileTree.SelectedNode.Index;
            PZZFile file = file_list.ElementAt(index);
            string initial_output_name = GetFileName(input_file, index, file.type);

            sfd.Title = "Save " + file.type + " File";
            sfd.Filter = file.type + " Files|*." + GetFileExtension(file.type);
            sfd.FileName = initial_output_name;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string exported_file = sfd.FileName;

                if (exported_file is null)
                {
                    MessageBox.Show("The ouput path for the file is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                File.WriteAllBytes(exported_file, file.byte_array);
            }
        }

        private void ContextPZZImport_Click(object sender, EventArgs e)
        {
            int index = FileTree.SelectedNode.Index;
            PZZFile file = file_list.ElementAt(index);
            string initial_output_name = GetFileName(input_file, index, file.type);
            ofd.Title = "Import File";
            ofd.Filter = file.type + " Files|*." + GetFileExtension(file.type) + "|All files|*.*";
            ofd.FileName = initial_output_name;
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                byte[] imported_file = File.ReadAllBytes(ofd.FileName);
                ReplaceFileOnList(file_list, index, imported_file);
                BuildTree(file_list);
                FileTree.SelectedNode = FileTree.Nodes[0].Nodes[index];
                UpdatePropertyGrid();
            }
        }



        private void ContextPZZExportAll_Click(object sender, EventArgs e)
        {
            ffd.InputPath = input_file;

            if (ffd.ShowDialog(IntPtr.Zero) == true)
            {
                int count = 0;
                int index = 0;
                foreach (PZZFile file in file_list)
                {
                    if (file.byte_array.Length <= 0)
                    {
                        index++;
                        continue;
                    }

                    string output_filename = GetFileName(input_file, index, file.type);
                    string output_path = Path.Join(ffd.ResultPath, output_filename);
                    File.WriteAllBytes(output_path, file.byte_array);
                    count++;
                    index++;
                }
                MessageBox.Show(count + " files were exported successfully.\n" + // this sucks
                    "Please refrain from modifying the filenames if you're gonna use the Import All option later. ",
                    "Export All", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ContextPZZImportAll_Click(object sender, EventArgs e)
        {
            ffd.InputPath = input_file;
            if (ffd.ShowDialog(IntPtr.Zero) == true)
            {
                string input_path = ffd.ResultPath;
                int replaced_count = 0;
                foreach (string file_import in Directory.EnumerateFiles(input_path, "*.*"))
                {
                    for (int i = 0; i < file_list.Count; i++) // lowkey unsafe
                    {
                        PZZFile file = file_list[i];
                        string test_filename = GetFileName(input_file, i, file.type);
                        if (test_filename == Path.GetFileName(file_import))
                        {
                            ReplaceFileOnList(file_list, i, File.ReadAllBytes(file_import));
                            replaced_count++;
                        }
                    }
                }
                MessageBox.Show("Replaced " + replaced_count + " file(s).", "Import All", MessageBoxButtons.OK, MessageBoxIcon.Information);
                BuildTree(file_list);
                FileTree.SelectedNode = FileTree.Nodes[0].Nodes[0];
                UpdatePropertyGrid();
            }
        }

        private void ContextPZZInsertFile_Click(object sender, EventArgs e)
        {

            ofd.Title = "Import File";
            ofd.Filter = "All files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                byte[] imported_file_buffer = File.ReadAllBytes(ofd.FileName);
                string type = GetFileType(imported_file_buffer);
                AddToList(file_list, imported_file_buffer, false, type);
                BuildTree(file_list);
                UpdatePropertyGrid();
            }
        }

        private void ContextPZZStub_Click(object sender, EventArgs e)
        {
            int index = FileTree.SelectedNode.Index;
            PZZFile old_file = file_list.ElementAt(index);
            file_list.Remove(old_file);
            AddToList(file_list, Array.Empty<byte>(), false, "Unknown", index);
            BuildTree(file_list);
            FileTree.SelectedNode = FileTree.Nodes[0].Nodes[index];
            UpdatePropertyGrid();
        }

        private void StripAboutPZZARC_Click(object sender, EventArgs e)
        {
            About about_window = new About();
            about_window.Show();
        }

        private void StripFileExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StripFileFromFolder_Click(object sender, EventArgs e)
        {

            ffd.Title = "Create PZZ From Folder";
            if (ffd.ShowDialog(IntPtr.Zero) == true)
            {
                string input_path = ffd.ResultPath;

                if (Directory.EnumerateFiles(input_path, "*.*").Count() > 255)
                {
                    MessageBox.Show("Cannot fit more than 255 files into a single PZZ archive!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                file_list.Clear();
                file_list = new List<PZZFile>();

                foreach (string file_import in Directory.EnumerateFiles(input_path, "*.*"))
                {
                    byte[] file_buffer = File.ReadAllBytes(file_import);
                    bool is_compressed = false;
                    string type = GetFileType(file_buffer);

                    AddToList(file_list, file_buffer, is_compressed, type);
                }
                BuildTree(file_list);
                FileTree.SelectedNode = FileTree.Nodes[0].Nodes[0];
                UpdatePropertyGrid();
                StripFileSave.Enabled = true;
                StripFileSaveAs.Enabled = true;
            }
        }

        private void ContextPZZModifyTXB_Click(object sender, EventArgs e)
        {
            int index = FileTree.SelectedNode.Index;
            PZZFile txb = file_list[index];
            var txb_form = new TXBeditor.Form1(this);
            txb_form.Show();
            txb_form.OpenFromPZZARC(txb.byte_array);
        }

        private void StripFileFromAFS_Click(object sender, EventArgs e)
        {
            ofd.Title = "Select AFS File";
            ofd.Filter = "AFS Files|*.afs";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string input_afs = ofd.FileName;
                List<string> filelist = new List<string>();
                current_afs = new AFS(input_afs);
                
                foreach (StreamEntry entry in current_afs.Entries)
                {
                    filelist.Add(entry.Name);
                }
                var filepicker_form = new AFSFilePicker(this);
                filepicker_form.Show();
                filepicker_form.BuildTree(Path.GetFileName(input_afs), filelist);
            }
        }
    }
}
