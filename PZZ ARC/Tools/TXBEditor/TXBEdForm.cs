using System.Collections;
using System.IO;
using System.Reflection.Metadata;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows.Forms;
using Rainbow.ImgLib;
using Rainbow.ImgLib.Formats;
using Rainbow.ImgLib.Formats.Implementation;
using Rainbow.ImgLib.Formats.Serialization;
using Rainbow.ImgLib.Formats.Serialization.Metadata;
using TXBeditor.TXBEditor;
using static TXBeditor.TXBEditor.TXB;
using static Rainbow.ImgLib.Formats.Implementation.TIM2Texture;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;
using PZZ_ARC;

namespace TXBeditor
{
    public partial class Form1 : Form
    {
        TextureFormatSerializer serializer;
        string input_file = "";
        string output_file = "";
        int texture_count = 0;
        float current_zoom = 1.0f;
        List<TXB.ImageInfo> image_list = new List<TXB.ImageInfo>();

        TIM2TextureSerializer tim2_serializer = new TIM2TextureSerializer();

        readonly OpenFileDialog ofd = new OpenFileDialog();
        readonly SaveFileDialog sfd = new SaveFileDialog();
        readonly FolderPicker ffd = new FolderPicker();
        readonly ColorDialog cld = new ColorDialog();

        bool save_to_pzzarc = false;
        PZZ_ARC.Form1 pzz_arc;

        public Form1(PZZ_ARC.Form1 pzz_arc_form)
        {
            InitializeComponent();
            pzz_arc = pzz_arc_form;
        }

        public void OpenFromPZZARC(byte[] txb_buffer)
        {
            image_list = TXB.LoadFromByteArray(serializer, txb_buffer);
            if (image_list.Count > 0)
            {
                SetPZZArcSave(true);
                SetListFromImageList();
                EnableUIGroupBoxes();
                EnableUISaveOptions();
                ImageListView.Items[0].Selected = true;
                ImageInfo current_image = image_list.ElementAt(ImageListView.SelectedIndices[0]);
                UpdateTIM2PropertyList(tim2_serializer.Open(new MemoryStream(current_image.byte_array)));
                EnableUIImportExport();
            }
        }

        private void SetPZZArcSave(bool val)
        {
            save_to_pzzarc = val;
        }

        public void SetInputPath(string path)
        {
            input_file = path;
        }

        public static float Clamp(float value, float min, float max)
        {
            return Math.Max(min, Math.Min(max, value));
        }

        private void SetListFromImageList()
        {
            ImageListView.Items.Clear();

            int index = 0;

            foreach (var image in image_list)
            {
                string string_index = "Image " + index.ToString();
                string string_load_id = image.load_index.ToString();

                ListViewItem image_item = new ListViewItem(string_index);
                //image_item.Text = string_index;
                image_item.SubItems.Add(string_load_id);

                ImageListView.Items.Add(image_item);
                index++;
            }
        }


        private void OnImageItemSelection(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (ImageListView.SelectedItems.Count <= 0)
            {
                DisableUIImportExport();
            }
            else
            {
                TXB.ImageInfo current_image_info = image_list.ElementAt(e.ItemIndex);
                CurrImgIDField.Value = current_image_info.load_index;
                ImgLib_LoadImage(current_image_info.byte_array);
                EnableUIImportExport();
            }
        }

        private void UpdateTIM2PropertyList(TextureFormat texture)
        {
            if (texture == null)
            {
                MessageBox.Show("Couldn't update TIM2 properties list, texture is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            TIM2Texture? tim2 = texture as TIM2Texture;
            //TIM2DataListView.Items[0].SubItems[1].Text = tim2.Width.ToString(); // Width
            //TIM2DataListView.Items[1].SubItems[1].Text = tim2.Height.ToString(); // Height

            switch (tim2.Alignment) // Byte Alignment
            {
                case TIM2ByteAlignment.Align16bytes:
                    ComboAlignment.SelectedIndex = 0; break;
                case TIM2ByteAlignment.Align128Bytes:
                    ComboAlignment.SelectedIndex = 1; break;
            }

            switch (tim2.Bpp) // Bit Depth
            {
                case 4:
                    ComboBitDepth.SelectedIndex = 0; break;
                case 8:
                    ComboBitDepth.SelectedIndex = 1; break;
                case 16:
                    ComboBitDepth.SelectedIndex = 2; break;
                case 24:
                    ComboBitDepth.SelectedIndex = 3; break;
                case 32:
                    ComboBitDepth.SelectedIndex = 4; break;
            }
        }

        private void ImgLib_LoadImage(byte[] TIM2_buffer)
        {
            //MetadataReader reader;
            MemoryStream TIM2_stream = new MemoryStream(TIM2_buffer);
            serializer = TextureFormatSerializerProvider.FromStream(TIM2_stream);
            TextureFormat texture = serializer.Open(TIM2_stream);
            UpdateTIM2PropertyList(texture);
            TIM2PictureBox.Image = texture.GetImage();
        }

        Image ZoomHandle(Image image, float scale)
        {
            return new Bitmap(image, Convert.ToInt32(image.Width * scale), Convert.ToInt32(image.Height * scale));
        }

        private TextureFormat GetTexture(byte[] TIM2_buffer)
        {
            MemoryStream TIM2_stream = new MemoryStream(TIM2_buffer);
            serializer = TextureFormatSerializerProvider.FromStream(TIM2_stream);
            TextureFormat texture = serializer.Open(TIM2_stream);
            return texture;
        }

        private void ImgLib_LoadImage(byte[] TIM2_buffer, float scale_multiplier)
        {

            TextureFormat texture = GetTexture(TIM2_buffer);
            UpdateTIM2PropertyList(texture);
            Image TIM2_Image = texture.GetImage();

            TIM2_Image = ZoomHandle(TIM2_Image, scale_multiplier);
            TIM2PictureBox.Image = TIM2_Image;
        }

        private void ImgLib_ExportTIM2(byte[] TIM2_buffer, string output_texture)
        {
            TextureFormat texture = serializer.Open(new MemoryStream(TIM2_buffer));
            tim2_serializer.ExportNoMetadata(texture, Path.GetDirectoryName(output_texture), Path.GetFileNameWithoutExtension(output_texture));
        }

        private void ImgLib_ImportTIM2(byte[] TIM2_buffer, string input_png, int image_index)
        {

            MemoryStream tex_stream_temp = new MemoryStream();

            int current_alignment = image_list[image_index].byte_alignment;
            int current_bitdepth = image_list[image_index].bit_depth;

            int height;
            int width;

            using (var img = Image.FromFile(input_png))
            {
                width = img.Width;
                height = img.Height;
            }

            TextureFormat TIM2 = GetTexture(TIM2_buffer);
            TextureFormat texture = tim2_serializer.ImportOver(TIM2, input_png, current_alignment, current_bitdepth, width, height);
            serializer.Save(texture, tex_stream_temp);

            byte[] image_buffer = tex_stream_temp.ToArray();
            image_list[image_index].byte_array = image_buffer;
            tex_stream_temp.Dispose();
        }

        private void UpdateFromTIM2Properties()
        {
            int image_index = ImageListView.SelectedIndices[0];
            MemoryStream tex_stream_temp = new MemoryStream();

            byte[] TIM2_buffer = image_list.ElementAt(ImageListView.SelectedIndices[0]).byte_array;
            List<int> bpp = new List<int>() { 4, 8, 16, 24, 32 };
            int new_bpp = bpp[ComboBitDepth.SelectedIndex];
            TIM2ByteAlignment new_alignment = TIM2ByteAlignment.Align16bytes;
            switch (ComboAlignment.SelectedIndex) // Byte Alignment
            {
                case 0:
                    new_alignment = TIM2ByteAlignment.Align16bytes; break;
                case 1:
                    new_alignment = TIM2ByteAlignment.Align128Bytes; break;
            }

            TextureFormat TIM2 = GetTexture(TIM2_buffer);
            TextureFormat texture = tim2_serializer.Update(TIM2, new_alignment, new_bpp);
            serializer.Save(texture, tex_stream_temp);

            byte[] image_buffer = tex_stream_temp.ToArray();
            image_list[image_index].byte_array = image_buffer;
            tex_stream_temp.Dispose();
        }

        private void OnCurrentImageIDChanged(object sender, EventArgs e)
        {
            if (ImageListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("There's no selected image to change the load ID to.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            image_list.ElementAt(ImageListView.SelectedIndices[0]).load_index = Convert.ToInt32(CurrImgIDField.Value); // List
            ImageListView.SelectedItems[0].SubItems[1].Text = CurrImgIDField.Value.ToString(); // UI
        }

        private void MoveImageList(int difference)
        {
            if (ImageListView.SelectedItems != null && ImageListView.SelectedIndices.Count > 0)
            {
                int new_index = ImageListView.SelectedIndices[0] + difference;

                if (new_index >= 0 && new_index < ImageListView.Items.Count)
                {
                    TXB.ImageInfo selected = image_list.ElementAt(ImageListView.SelectedIndices[0]);

                    image_list.Remove(selected);
                    image_list.Insert(new_index, selected);

                    SetListFromImageList();
                    ImageListView.Items[new_index].Selected = true;
                }

            }
        }

        private void ImageListPushedUp(object sender, EventArgs e)
        {
            MoveImageList(-1);
        }

        private void ImageListPushedDown(object sender, EventArgs e)
        {
            MoveImageList(1);
        }


        private void OnImageListAdd(object sender, EventArgs e)
        {
            ofd.Title = "Select TIM2 File";
            ofd.Filter = ".TM2 Files|*.tm2";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string input_texture = ofd.FileName;
                using (var stream = File.Open(input_texture, FileMode.Open))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        char[] magic_test = reader.ReadChars(4);
                        if (string.Join("", magic_test) != "TIM2")
                        {
                            MessageBox.Show("File is not a valid TIM2 image.\nMagic: " + magic_test, "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                            return;
                        }

                        stream.Position = 5; // get alignment
                        byte alignment = reader.ReadByte();

                        stream.Position = 0;
                        byte[] image_buffer = reader.ReadBytes(Convert.ToInt32(stream.Length));

                        image_list.Add(new ImageInfo()
                        {
                            load_index = 0x0,
                            bit_depth = ImgLib_GetBPP(serializer, image_buffer),
                            byte_alignment = GetAlignment(alignment),
                            byte_array = image_buffer
                        });
                        SetListFromImageList();
                    }
                }
            }
        }

        private void OnImageListRemoved(object sender, EventArgs e)
        {
            if (image_list.Count <= 1)
            {
                MessageBox.Show("The image list cannot be empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                return;
            }

            if (ImageListView.SelectedItems != null && ImageListView.SelectedIndices.Count > 0)
            {
                int index = ImageListView.SelectedIndices[0];
                image_list.Remove(image_list.ElementAt(index));
                SetListFromImageList();

                if (index > image_list.Count) ImageListView.Items[index].Selected = true;
                else if (index == image_list.Count) ImageListView.Items[index - 1].Selected = true;
                else if (index < image_list.Count && (index - 1) != -1) ImageListView.Items[index - 1].Selected = true;
                else ImageListView.Items[0].Selected = true;

                ImageInfo current_image = image_list.ElementAt(ImageListView.SelectedIndices[0]);
                UpdateTIM2PropertyList(tim2_serializer.Open(new MemoryStream(current_image.byte_array)));
            }
        }

        private void OnImageListSave(object sender, EventArgs e)
        {
            if (ImageListView.SelectedItems != null && ImageListView.SelectedIndices.Count > 0)
            {
                byte[] tim2_bytes = image_list.ElementAt(ImageListView.SelectedIndices[0]).byte_array;

                ImageInfo current_image = image_list.ElementAt(ImageListView.SelectedIndices[0]);
                string initial_output_name = Path.GetFileNameWithoutExtension(input_file) + "_image_" + current_image.load_index.ToString();
                sfd.Title = "Save texture to TIM2";
                sfd.Filter = "TIM2 texture|*.tm2";
                sfd.FileName = initial_output_name;

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string output_texture = sfd.FileName;
                    if (output_texture is null)
                    {
                        MessageBox.Show("The output path for the texture is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    File.WriteAllBytes(output_texture, tim2_bytes);
                }
            }
        }

        private void EnableUIGroupBoxes()
        {
            if (GroupBoxImageList.Enabled == false) GroupBoxImageList.Enabled = true;
            if (GroupBoxTXB.Enabled == false) GroupBoxTXB.Enabled = true;
            if (GroupBoxTIM2.Enabled == false) GroupBoxTIM2.Enabled = true;
            //if (TIM2DataListView.Enabled == false) TIM2DataListView.Enabled = true;
            if (GroupBoxView.Enabled == false) GroupBoxView.Enabled = true;
        }

        private void EnableUISaveOptions()
        {
            if (StripFileSave.Enabled == false) StripFileSave.Enabled = true;
            if (StripFileSaveAs.Enabled == false) StripFileSaveAs.Enabled = true;

        }

        private void EnableUIImportExport()
        {
            if (StripEditExport.Enabled == false) StripEditExport.Enabled = true;
            if (StripEditImport.Enabled == false) StripEditImport.Enabled = true;
            if (image_list.Count <= 1)
            {
                StripEditExportAll.Enabled = false;
                StripEditImportAll.Enabled = false;
            }
            else
            {
                if (StripEditExportAll.Enabled == false) StripEditExportAll.Enabled = true;
                if (StripEditImportAll.Enabled == false) StripEditImportAll.Enabled = true;
            }
            if (ComboAlignment.Enabled == false) ComboAlignment.Enabled = true;
            if (ComboBitDepth.Enabled == false) ComboBitDepth.Enabled = true;
        }


        private void DisableUIImportExport()
        {
            if (StripEditExport.Enabled == true) StripEditExport.Enabled = false;
            if (StripEditImport.Enabled == true) StripEditImport.Enabled = false;
            if (image_list.Count > 1)
            {
                StripEditExportAll.Enabled = true;
                StripEditImportAll.Enabled = true;
            }
            else
            {
                if (StripEditExportAll.Enabled == true) StripEditExportAll.Enabled = false;
                if (StripEditImportAll.Enabled == true) StripEditImportAll.Enabled = false;
            }
            if (ComboAlignment.Enabled == true) ComboAlignment.Enabled = false;
            if (ComboBitDepth.Enabled == true) ComboBitDepth.Enabled = false;
        }

        private void StripFileOpen_Click(object sender, EventArgs e)
        {
            ofd.Title = "Select TXB File";
            ofd.Filter = ".TXB Files|*.txb";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                input_file = ofd.FileName;
                image_list = TXB.LoadFromFile(serializer, input_file);
                if (image_list.Count > 0)
                {
                    SetPZZArcSave(false);
                    SetListFromImageList();
                    EnableUIGroupBoxes();
                    EnableUISaveOptions();
                    ImageListView.Items[0].Selected = true;
                    ImageInfo current_image = image_list.ElementAt(ImageListView.SelectedIndices[0]);
                    UpdateTIM2PropertyList(tim2_serializer.Open(new MemoryStream(current_image.byte_array)));
                    EnableUIImportExport();
                }
            }
        }

        public static void UpdatePZZArc(Label lbl)
        {
            lbl.Text = "New label1 text";
        }

        private void StripFileSave_Click(object sender, EventArgs e)
        {

            if (save_to_pzzarc)
            {
                using (var stream = new MemoryStream())
                {
                    using (var writer = new BinaryWriter(stream))
                    {
                        TXB.WriteOutputData(writer, image_list);
                    }
                    pzz_arc.ReplaceBufferAtSelected(stream.ToArray());
                }
            }
            else
            {
                if (input_file is null)
                {
                    MessageBox.Show("The ouput path for the file is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                using (var stream = File.Open(input_file, FileMode.Create))
                {
                    using (var writer = new BinaryWriter(stream))
                    {
                        TXB.WriteOutputData(writer, image_list);
                    }
                }
            }
        }

        private void StripFileSaveAs_Click(object sender, EventArgs e)
        {
            sfd.Title = "Select TXB File";
            sfd.Filter = ".TXB Files|*.txb";
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
                        TXB.WriteOutputData(writer, image_list);
                    }
                }
            }
        }

        private void StripFileQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void StripEditExport_Click(object sender, EventArgs e)
        {
            if (ImageListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("There isn't a selected texture to export.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ImageInfo current_image = image_list.ElementAt(ImageListView.SelectedIndices[0]);
            string initial_output_name = Path.GetFileNameWithoutExtension(input_file) + "_image_" + current_image.load_index.ToString();

            sfd.Title = "Save to PNG";
            sfd.Filter = "PNG Texture|*.png";
            sfd.FileName = initial_output_name;

            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string output_texture = sfd.FileName;

                if (output_texture is null)
                {
                    MessageBox.Show("The output path for the texture is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                ImgLib_ExportTIM2(current_image.byte_array, output_texture);
            }

        }

        private void StripEditImport_Click(object sender, EventArgs e)
        {

            if (ImageListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("There isn't a selected texture to import over.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ofd.Title = "Import from PNG";
            ofd.Filter = "PNG Texture|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string input_png = ofd.FileName;

                if (input_png is null)
                {
                    MessageBox.Show("The input path for the metadata is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                else
                {
                    byte[] current_tim2 = image_list.ElementAt(ImageListView.SelectedIndices[0]).byte_array;
                    ImgLib_ImportTIM2(current_tim2, input_png, ImageListView.SelectedIndices[0]);
                    ImgLib_LoadImage(image_list.ElementAt(ImageListView.SelectedIndices[0]).byte_array);
                }

            }

        }

        private void StripEditExportAll_Click(object sender, EventArgs e)
        {
            if (ffd.ShowDialog(this.Handle) == true)
            {
                string output_path = ffd.ResultPath;
                int count = 0;
                foreach (ImageInfo image in image_list)
                {
                    string output_filename = Path.GetFileNameWithoutExtension(input_file) + "_image_" + image.load_index.ToString() + ".xml";
                    string output_name = Path.Join(output_path, output_filename);
                    ImgLib_ExportTIM2(image.byte_array, output_name);
                    count++;
                }
                MessageBox.Show(count + " textures were exported successfully.\n\"Batch Import\" relies on the program's " +  // This sucks
                    "load IDs, order and the output file names. Please refrain from modifying the filenames or the program's image list " +
                    "until you finished editing the textures and have reimported them.", "Batch Export", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

        }

        private void EditStripImportAll_Click(object sender, EventArgs e) // This sucks
        {
            if (ffd.ShowDialog(this.Handle) == true)
            {
                string input_path = ffd.ResultPath;
                int replaced_count = 0;
                foreach (string file in Directory.EnumerateFiles(input_path, "*.png"))
                {
                    int index = 0;
                    foreach (ImageInfo image in image_list)
                    {
                        string test_filename = Path.GetFileNameWithoutExtension(input_file) + "_image_" + image.load_index.ToString() + "_layer0_0.png";
                        // to do: only layer 0. No textures on giogio/AM use more than one layer so idc for now.
                        if (test_filename == Path.GetFileName(file))
                        {
                            ImageInfo current_image = image_list.ElementAt(index);
                            ImgLib_ImportTIM2(current_image.byte_array, file, index);
                            replaced_count++;
                        }
                        index++;
                    }
                }
                MessageBox.Show("Replaced " + replaced_count + " texture(s).", "Batch Import", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ImgLib_LoadImage(image_list.ElementAt(ImageListView.SelectedIndices[0]).byte_array);

            }

        }

        private void StripHelpImgLib_Click(object sender, EventArgs e)
        {
            ImglibAbout img = new ImglibAbout();
            img.Show();
        }


        private void BGColorButton_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.Button btnSender = (System.Windows.Forms.Button)sender;
            Point ptLowerLeft = new Point(0, btnSender.Height);
            ptLowerLeft = btnSender.PointToScreen(ptLowerLeft);
            StripBGContextMenu.Show(ptLowerLeft);
        }

        private void BGColorStripDefault_Click(object sender, EventArgs e)
        {
            TIM2PictureBox.BackColor = SystemColors.Control;
        }

        private void BGColorStripCustom_Click(object sender, EventArgs e)
        {
            if (cld.ShowDialog() == DialogResult.OK) TIM2PictureBox.BackColor = cld.Color;
        }

        private void ViewZoomAdd_Click(object sender, EventArgs e)
        {
            if (ImageListView.SelectedItems != null && ImageListView.SelectedIndices.Count > 0)
            {
                current_zoom = Clamp(current_zoom + 0.25f, 0.25f, 4.0f);
                ImgLib_LoadImage(image_list.ElementAt(ImageListView.SelectedIndices[0]).byte_array, current_zoom);
            }
        }

        private void ViewZoomSubs_Click(object sender, EventArgs e)
        {
            if (ImageListView.SelectedItems != null && ImageListView.SelectedIndices.Count > 0)
            {
                current_zoom = Clamp(current_zoom - 0.25f, 0.25f, 4.0f);
                ImgLib_LoadImage(image_list.ElementAt(ImageListView.SelectedIndices[0]).byte_array, current_zoom);
            }
        }

        private void ViewZoomReset_Click(object sender, EventArgs e)
        {

            if (ImageListView.SelectedItems != null && ImageListView.SelectedIndices.Count > 0)
            {
                current_zoom = 1.0f;
                ImgLib_LoadImage(image_list.ElementAt(ImageListView.SelectedIndices[0]).byte_array);
            }
        }

        

        private void ComboAlignmentChange(object sender, EventArgs e)
        {
            if (ImageListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("There's no selected image to change the Byte Alignment to.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            ImageInfo current_image = image_list.ElementAt(ImageListView.SelectedIndices[0]);
            current_image.byte_alignment = ComboAlignment.SelectedIndex == 0 ? 16 : 128;
            UpdateFromTIM2Properties();
            ImgLib_LoadImage(image_list.ElementAt(ImageListView.SelectedIndices[0]).byte_array);

        }

        private void ComboBPPChanged(object sender, EventArgs e)
        {
            if (ImageListView.SelectedItems.Count <= 0)
            {
                MessageBox.Show("There's no selected image to change the Bit Depth to.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<int> bpp = new List<int>() { 4, 8, 16, 24, 32 };
            int new_bpp = bpp[ComboBitDepth.SelectedIndex];

            ImageInfo current_image = image_list.ElementAt(ImageListView.SelectedIndices[0]);

            if (new_bpp < current_image.bit_depth)
            {
                DialogResult dialogResult = MessageBox.Show("Lowering Bit Depth will result in quality loss. Proceed?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dialogResult == DialogResult.No)
                {
                    int new_index = bpp.FindIndex(x => (x == current_image.bit_depth));
                    ComboBitDepth.SelectedIndex = new_index;
                    return;
                }
            }
            current_image.bit_depth = new_bpp;
            UpdateFromTIM2Properties();
            ImgLib_LoadImage(image_list.ElementAt(ImageListView.SelectedIndices[0]).byte_array);
        }
    }
}
