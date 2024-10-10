using AFSLib;
using System.Text;
using static GioGioTXT.ggText;
using static PZZ_ARC.PZZArc.PZZ;

namespace GioGioTXT
{
    public partial class Form1 : Form
    {
        PZZ_ARC.Form1 pzz_arc;
        public Form1(PZZ_ARC.Form1 pzz_arc, string text_file, int index)
        {
            InitializeComponent();
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            font_default = new System.Drawing.Font(RichTextInput.Font, FontStyle.Regular);
            font_big = new System.Drawing.Font(font_default.FontFamily, 14, FontStyle.Regular);
            font_underline = new System.Drawing.Font(RichTextInput.Font, FontStyle.Underline);
            this.pzz_arc = pzz_arc;
            OpenFromPZZARC(text_file, index);
        }

        public enum TextType
        {
            Demo2D,
            Demo3D,
            Gameplay
        }

        public Encoding shift_jis = CodePagesEncodingProvider.Instance.GetEncoding(932);

        List<List<Line>> line_group_list = new List<List<Line>>();
        /* nested list for all lines belonging to each "line group"
        each "line group" ends with a line just saying "-1"
        secret factor cutscene files can have multiple "line groups"
        eg; the first cutscene of d030.pzz ends with -1 and then the next cutscene is just written right after 
        so this list has a list of lines for each "line group"
        */

        readonly OpenFileDialog ofd = new OpenFileDialog();
        readonly SaveFileDialog sfd = new SaveFileDialog();
        string input_file = "";
        string output_file = "";
        TextType current_text_type;
        bool update_richtextinput = true; // so it doesnt update before the rows while switching groups 
        bool save_to_pzz = false;
        AFS current_afs = new AFS();

        List<Color> text_palette = new List<Color> {
            Color.Black,    //p0, black
            Color.Gray,     //p1, white
            Color.Crimson,  //p2, dark red ish
            Color.Navy,     //p3, blue
            Color.Orange,   //p4, yellow
            Color.DarkGoldenrod, // \c (cock.pzz) characters
            Color.DarkOliveGreen, // for misc tags (k, ds/de, bs/be)
            Color.Tan // for formatting tags
        }; // using colors clearer for editor use

        System.Drawing.Font font_default;
        System.Drawing.Font font_big; // ds/k tagged text
        System.Drawing.Font font_underline; // bs tagged text

        private void UpdateTitlebar()
        {
            this.Text = "GioGioTXT";
            if (input_file == "") return;
            this.Text += " - [" + input_file + "]";
        }

        public void OpenFromPZZARC(string input_string, int file_index)
        {
            string cleaned_string = input_string.TrimEnd('\0'); // remove trailling 0 bytes

            string[] text_data = cleaned_string.Split(
                new string[] { "\r\n", "\r", "\n" },
                StringSplitOptions.None);


            switch (file_index)
            {
                case 1: // likely a 3D cutscene file
                    current_text_type = TextType.Demo3D;
                    LoadAs3DLine(text_data); 
                    break;
                case 3: // likely a 2D cutscene file
                    current_text_type = TextType.Demo2D;
                    LoadAs2DLine(text_data);
                    break;
                case 2: // likely a stage demo file
                    current_text_type = TextType.Gameplay;
                    LoadAsGameLine(text_data);
                    break;
            }
            if (line_group_list.Count > 0) SetUISaveOptions(true);
        }

        private void StripFileOpen_Click(object sender, EventArgs e)
        {

        }

        private void StripFileOpenAs3D_Click(object sender, EventArgs e)
        {
            ofd.Title = "Select Text Files";
            ofd.Filter = "Text Files|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                line_group_list.Clear();
                input_file = ofd.FileName;
                string input_file_extension = Path.GetExtension(input_file);
                string[] text_file = File.ReadLines(input_file, shift_jis).ToArray();
                current_text_type = TextType.Demo3D;
                LoadAs3DLine(text_file);
                SetUISaveOptionsPZZ(false);
                if (line_group_list.Count > 0) {
                    SetUISaveOptions(true);
                    UpdateTitlebar();
                }
            }
        }

        private void StripFileOpenAs2D_Click(object sender, EventArgs e)
        {
            ofd.Title = "Select Text Files";
            ofd.Filter = "Text Files|*.txt";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                line_group_list.Clear();
                input_file = ofd.FileName;
                string input_file_extension = Path.GetExtension(input_file);
                string[] text_file = File.ReadLines(input_file, shift_jis).ToArray();
                current_text_type = TextType.Demo2D;
                LoadAs2DLine(text_file);
                SetUISaveOptionsPZZ(false);
                if (line_group_list.Count > 0) {
                    SetUISaveOptions(true);
                    UpdateTitlebar();
                }
            }
        }

        private void StripFileSave_Click(object sender, EventArgs e)
        {
            using (var stream = new MemoryStream())
            {
                WriteLinesToMemoryStream(stream, line_group_list, current_text_type, shift_jis);
                pzz_arc.ReplaceBufferAtSelected(stream.ToArray());
            }
        }

        private void StripFileSaveAsPZZ_Click(object sender, EventArgs e)
        {
            if (save_to_pzz != true) return; // shouldn't happen

            sfd.Title = "Save PZZ File";
            sfd.Filter = "PZZ Files|*.pzz";
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                output_file = sfd.FileName;

                if (output_file is null)
                {
                    MessageBox.Show("The ouput path for the file is null.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                byte[] text_bytes = null;
                using (var stream = new MemoryStream())
                {
                    WriteLinesToMemoryStream(stream, line_group_list, current_text_type, shift_jis);
                    text_bytes = stream.ToArray();
                }

                List<PZZFile> pzz_file_list = UnpackFromFile(input_file);
                ReplaceTextOnPZZ(pzz_file_list, text_bytes);

                using (var stream = File.Open(output_file, FileMode.OpenOrCreate))
                {
                    using (var writer = new BinaryWriter(stream))
                    {
                        WriteOutputData(writer, stream, pzz_file_list);
                    }
                }
                input_file = output_file;
                UpdateTitlebar();
            }
        }


        private void StripFileSaveAsTXT_Click(object sender, EventArgs e)
        {
            sfd.Title = "Save Text File";
            sfd.Filter = "Text Files|*.txt";
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
                    WriteLinesToStream(stream, line_group_list, current_text_type, shift_jis);
                }

                input_file = output_file;
                SetUISaveOptionsPZZ(false);
                UpdateTitlebar();
            }
        }

        private void StripFileQuit_Click(object sender, EventArgs e)
        {
            Close();
        }

        void SetUISaveOptions(bool enabled)
        {
            StripFileSave.Enabled = enabled;
            StripFileSaveAs.Enabled = enabled;
            StripFileSaveAsTXT.Enabled = enabled;
        }

        void SetUISaveOptionsPZZ(bool enabled)
        {
            save_to_pzz = enabled;
            StripFileSaveAsPZZ.Enabled = enabled;
        }

        void SetUISaveOptionsAFS()
        {
            save_to_pzz = false;
            StripFileSaveAsPZZ.Enabled = false;
            StripFileSave.Enabled = false;
            StripFileSaveAs.Enabled = true;
            StripFileSaveAsTXT.Enabled = true;
        }

        void ReplaceTextOnPZZ(List<PZZFile> file_list, byte[] new_text)
        {
            int file_count = file_list.Count;
            if (file_count == 3) // likely a 3D cutscene file
            {
                if (file_list[1].type != "TextData") return;
                file_list[1].byte_array = new_text;
            }
            else if (file_count == 4) // likely a 2D cutscene file 
            {
                if (file_list[3].type != "TextData") return;
                file_list[3].byte_array = new_text;
            }
            else if (file_count == 5) // likely a stage demo file
            {
                if (file_list[2].type != "TextData") return;
                file_list[2].byte_array = new_text;
            }
            else
            {
                MessageBox.Show("Couldn't find 3D, 2D or Game type text data on the PZZ it was loaded from."
                    , "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void FindTypeAndLoad(List<PZZFile> file_list)
        {
            
        }

        private void LoadAsGameLine(string[] text_file)
        {
            current_text_type = TextType.Gameplay;


            List<Line> line_list = new List<Line>();

            foreach (var line in text_file)
            {
                AddLineAsGameLine(line_group_list, line_list, line);
            }
            LineGroupAdd.Enabled = true;
            LineSelectionGroup.Enabled = true;
            SetLineGroupUI();
            ShowGameLineProperties();
            LineGroupPicker.SelectedIndex = 0;
        }

        private void LoadAs2DLine(string[] text_file)
        {
            List<Line> line_list = new List<Line>();

            foreach (var line in text_file)
            {
                AddLineAs2DLine(line_group_list, line_list, line);
            }

            line_group_list.Add(new List<Line>(line_list));
            LineGroupAdd.Enabled = false; // 2D cutscenes dont use the group system stage cutscenes do
            LineSelectionGroup.Enabled = true;
            SetLineGroupUI();
            Show2DLineProperties();
            LineGroupPicker.SelectedIndex = 0;
        }

        private void LoadAs3DLine(string[] text_file)
        {
            List<Line> line_list = new List<Line>();

            foreach (var line in text_file)
            {
                AddLineAs3DLine(line_group_list, line_list, line);
            }

            line_group_list.Add(new List<Line>(line_list));
            LineGroupAdd.Enabled = false; // 3D cutscenes dont use the group system stage cutscenes do
            LineSelectionGroup.Enabled = true;
            SetLineGroupUI();
            Show3DLineProperties();
            LineGroupPicker.SelectedIndex = 0;
        }

        private void Show3DLineProperties()
        {
            StartFrameCounter.Visible = true;       // Game / 3D
            EndFrameCounter.Visible = true;         // Game / 3D
            BubbleShapePicker.Visible = true;       // 2D   / 3D
            BubbleTailPicker.Visible = true;        // 2D   / 3D
            BubblePositionPicker.Visible = true;    // 3D
            BubblePosXCounter.Visible = false;      // 2D
            BubblePosYCounter.Visible = false;      // 2D
            Panel2DIndexCounter.Visible = false;    // 2D
            Panel2DBehaviorCounter.Visible = false; // 2D
            CutInPanelPicker.Visible = false;       // Game
            CutInPositionPicker.Visible = false;    // Game

            //labels
            StartFrameLabel.Visible = true;       // Game / 3D
            EndFrameLabel.Visible = true;         // Game / 3D
            BubbleShapeLabel.Visible = true;      // 2D   / 3D
            BubbleTailLabel.Visible = true;       // 2D   / 3D
            BubblePositionLabel.Visible = true;   // 3D
            BubblePosXLabel.Visible = false;      // 2D
            BubblePosYLabel.Visible = false;      // 2D
            Panel2DIndexLabel.Visible = false;    // 2D
            Panel2DBehaviorLabel.Visible = false; // 2D
            CutInPanelLabel.Visible = false;      // Game
            CutInPositionLabel.Visible = false;   // Game
        }

        private void Show2DLineProperties()
        {
            StartFrameCounter.Visible = false;      // Game / 3D
            EndFrameCounter.Visible = false;        // Game / 3D
            BubbleShapePicker.Visible = true;       // 2D   / 3D
            BubbleTailPicker.Visible = true;        // 2D   / 3D
            BubblePositionPicker.Visible = false;   // 3D
            BubblePosXCounter.Visible = true;       // 2D
            BubblePosYCounter.Visible = true;       // 2D
            Panel2DIndexCounter.Visible = true;     // 2D
            Panel2DBehaviorCounter.Visible = true;  // 2D
            CutInPanelPicker.Visible = false;       // Game
            CutInPositionPicker.Visible = false;    // Game

            //labels
            StartFrameLabel.Visible = false;        // Game / 3D
            EndFrameLabel.Visible = false;          // Game / 3D
            BubbleShapeLabel.Visible = true;        // 2D   / 3D
            BubbleTailLabel.Visible = true;         // 2D   / 3D
            BubblePositionLabel.Visible = false;    // 3D
            BubblePosXLabel.Visible = true;         // 2D
            BubblePosYLabel.Visible = true;         // 2D
            Panel2DIndexLabel.Visible = true;       // 2D
            Panel2DBehaviorLabel.Visible = true;    // 2D
            CutInPanelLabel.Visible = false;        // Game
            CutInPositionLabel.Visible = false;     // Game
        }

        private void ShowGameLineProperties()
        {
            StartFrameCounter.Visible = true;       // Game / 3D
            EndFrameCounter.Visible = true;         // Game / 3D
            BubbleShapePicker.Visible = false;      // 2D   / 3D
            BubbleTailPicker.Visible = false;       // 2D   / 3D
            BubblePositionPicker.Visible = false;   // 3D
            BubblePosXCounter.Visible = false;      // 2D
            BubblePosYCounter.Visible = false;      // 2D
            Panel2DIndexCounter.Visible = false;    // 2D
            Panel2DBehaviorCounter.Visible = false; // 2D
            CutInPanelPicker.Visible = true;        // Game
            CutInPositionPicker.Visible = true;     // Game

            //labels
            StartFrameLabel.Visible = true;        // Game / 3D
            EndFrameLabel.Visible = true;          // Game / 3D
            BubbleShapeLabel.Visible = false;      // 2D   / 3D
            BubbleTailLabel.Visible = false;       // 2D   / 3D
            BubblePositionLabel.Visible = false;   // 3D
            BubblePosXLabel.Visible = false;       // 2D
            BubblePosYLabel.Visible = false;       // 2D
            Panel2DIndexLabel.Visible = false;     // 2D
            Panel2DBehaviorLabel.Visible = false;  // 2D
            CutInPanelLabel.Visible = true;        // Game
            CutInPositionLabel.Visible = true;     // Game
        }

        private void ShowEditUI(bool visible)
        {
            PropertiesGroup.Enabled = visible;
            RichTextInput.Enabled = visible;
        }

        private void SetLineGroupUI()
        {
            LineGrid.Rows.Clear();
            RichTextInput.Clear();
            UpdateGroupButtons();
        }

        private void LineGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

            var value_object = LineGrid.Rows[e.RowIndex].Cells[0].Value; // probably jank
            if (value_object == null) return;
            string text = value_object.ToString();
            UpdateLineText(text);
        }

        private void LineGrid_SelectionChanged(object sender, EventArgs e)
        {

            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;

            ShowEditUI(false);
            if (line_group_index < 0 || line_group_index >= line_group_list.Count) return;
            if (line_index < 0 || line_index >= line_group_list[line_group_index].Count) return;

            try
            {

                switch (current_text_type)
                {
                    case TextType.Gameplay:
                        {
                            LinesGame current_line = line_group_list[line_group_index][line_index] as LinesGame;
                            StartFrameCounter.Value = current_line.frame_start;
                            EndFrameCounter.Value = current_line.frame_end;
                            CutInPanelPicker.SelectedIndex = (int)current_line.cutin_panel;
                            CutInPositionPicker.SelectedIndex = (int)current_line.cutin_position;
                        }
                        break;

                    case TextType.Demo2D:
                        {
                            Lines2D current_line = line_group_list[line_group_index][line_index] as Lines2D;
                            Panel2DIndexCounter.Value = current_line.panel_index;
                            BubblePosXCounter.Value = current_line.bubble_x_position;
                            BubblePosYCounter.Value = current_line.bubble_y_position;
                            BubbleShapePicker.SelectedIndex = (int)current_line.bubble_shape;
                            BubbleTailPicker.SelectedIndex = (int)current_line.tail_direction;
                            Panel2DBehaviorCounter.Value = current_line.behavior;
                        }
                        break;

                    case TextType.Demo3D:
                        {
                            Lines3D current_line = line_group_list[line_group_index][line_index] as Lines3D;
                            StartFrameCounter.Value = current_line.frame_start;
                            EndFrameCounter.Value = current_line.frame_end;
                            BubbleShapePicker.SelectedIndex = (int)current_line.bubble_shape;
                            BubbleTailPicker.SelectedIndex = (int)current_line.tail_direction;
                            BubblePositionPicker.SelectedIndex = (int)current_line.bubble_position;
                        }
                        break;
                }

                if (update_richtextinput) UpdateRichTextInput();
                ShowEditUI(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating UI from line list!\n" + ex.Message + "Using default encoding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void LineGrid_UserAddedRow(object sender, DataGridViewRowEventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            List<Line> current_line_list = line_group_list[line_group_index];
            string new_text = "test"; // e.Row.Cells[0].Value.ToString(); // probably jank but whatever
            AddNewLineToList(current_line_list, current_text_type, new_text);
        }

        void UpdateRichTextInput()
        {
            if (LineGrid.CurrentRow == null) return;
            var current_text = LineGrid.CurrentRow.Cells[0].Value; // probably jank but whatever
            RichTextInput.Lines = current_text.ToString().Split("\\n");
        }

        void UpdateLineText(string new_string)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            Line current_line = line_group_list[line_group_index][line_index];
            if (current_line.text != new_string) current_line.text = new_string;
        }

        private void RichTextInput_TextChanged(object sender, EventArgs e)
        {
            SetRichTextFormatting();

            if (LineGrid.CurrentRow == null) return;
            string current_text = System.String.Join("\\n", RichTextInput.Lines);
            UpdateLineText(current_text);
            LineGrid.CurrentRow.SetValues(current_text);
        }

        private void SetRichTextFormatting() // ideally this should be done as fast as possible
        {

            int original_position = RichTextInput.SelectionStart;
            int original_selection = RichTextInput.SelectionLength;

            string RichText = RichTextInput.Text;

            //start by coloring all black, default font
            RichTextInput.Select(0, RichText.Length);
            RichTextInput.SelectionColor = SystemColors.WindowText;
            RichTextInput.SelectionFont = font_default;

            for (int i = 0; i < RichText.Length - 2; i++)
            {
                string format_test = RichText.Substring(i, 2);
                int start_index = i + 3; // start of text within format tag
                switch (format_test)
                {

                    case "\\c": // special characters from cock.pzz
                        {
                            RichTextInput.Select(i, 3);
                            RichTextInput.SelectionColor = text_palette[5];
                        }
                        break;

                    case "\\b": // big text
                        {
                            if (RichText.Substring(i + 2, 1) == "s") // check if big text starts here
                            {
                                RichTextInput.Select(start_index, RichTextInput.Text.Length);
                                RichTextInput.SelectionFont = font_big;
                            }
                            else if (RichText.Substring(i + 2, 1) == "e") // check if big text ends here
                            {
                                RichTextInput.Select(i, RichTextInput.Text.Length);
                                RichTextInput.SelectionFont = font_default;
                            }
                            // color the formatting tag light
                            RichTextInput.Select(i, 3);
                            RichTextInput.SelectionColor = text_palette[7];
                        }
                        break;

                    case "\\d": // dot emphasis
                        {
                            if (RichText.Substring(i + 2, 1) == "s") // check if emphasis text (underline) starts here
                            {
                                RichTextInput.Select(start_index, RichTextInput.Text.Length);
                                RichTextInput.SelectionFont = font_underline;
                            }
                            else if (RichText.Substring(i + 2, 1) == "e") // check if emphasis text (underline) ends here
                            {
                                RichTextInput.Select(i, RichTextInput.Text.Length);
                                RichTextInput.SelectionFont = font_default;
                            }
                            // color the formatting tag light
                            RichTextInput.Select(i, 3);
                            RichTextInput.SelectionColor = text_palette[7];
                        }
                        break;

                    case "\\k": // singular char dot emphasis
                        {
                            RichTextInput.Select(i + 2, 1);
                            RichTextInput.SelectionFont = font_underline;
                            // color the formatting tag light
                            RichTextInput.Select(i, 2);
                            RichTextInput.SelectionColor = text_palette[7];
                        }
                        break;

                    case "\\p": // paletted text
                        {
                            if (int.TryParse(RichText.Substring(i + 2, 1), out _) != true) // check if palette index is a number
                            {
                                if (RichText.Substring(i + 2, 1) == "p") // check if paletted text ends here
                                {
                                    RichTextInput.Select(i, RichTextInput.Text.Length); // color all as black (default)
                                    RichTextInput.SelectionColor = SystemColors.WindowText;
                                }
                                else break; // this is nothing!!!!!!!!!
                            }
                            else // start coloring text from here on out
                            {
                                int palette_index = int.Parse(RichText.Substring(i + 2, 1));
                                if (palette_index <= 4)
                                {
                                    RichTextInput.Select(start_index, RichTextInput.Text.Length);
                                    RichTextInput.SelectionColor = text_palette[palette_index];
                                }
                            }

                            // color the formatting tag light
                            RichTextInput.Select(i, 3);
                            RichTextInput.SelectionColor = text_palette[7];
                        }
                        break;
                }
            }

            RichTextInput.Select(original_position, original_selection); //restore cursor to og writing pos

        }

        private void UpdateGroupButtons()
        {
            LineGroupPicker.Items.Clear();

            for (int i = 0; i < line_group_list.Count; i++)
                LineGroupPicker.Items.Add("Group " + (i + 1).ToString() + " out of " + line_group_list.Count);

            if (line_group_list.Count <= 1) LineGroupRemove.Enabled = false;
            else LineGroupRemove.Enabled = true;
        }

        private void LineGroupAdd_Click(object sender, EventArgs e)
        {
            List<Line> new_line_list = new List<Line>();
            AddNewLineToList(new_line_list, current_text_type, "New Group");
            line_group_list.Add(new_line_list);
            UpdateGroupButtons();
            LineGroupPicker.SelectedIndex = line_group_list.Count - 1;
        }

        private void LineGroupRemove_Click(object sender, EventArgs e)
        {
            int index = LineGroupPicker.SelectedIndex;
            if (line_group_list.Count <= 1 || index < 0) return;
            line_group_list.RemoveAt(index);
            UpdateGroupButtons();

            if (index - 1 < 0) index = 0;
            else index = index - 1;
            LineGroupPicker.SelectedIndex = index;
        }

        private void LineGroupPicker_SelectionChangeCommitted(object sender, EventArgs e)
        {
            if (LineGroupPicker.SelectedIndex < 0 || LineGroupPicker.SelectedItem == null)
            {
                LineGrid.Enabled = false;
                return;
            }
            else LineGrid.Enabled = true;
            OpenLineGroup();
        }

        private void LineGroupPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LineGroupPicker.SelectedIndex < 0 || LineGroupPicker.SelectedItem == null)
            {
                LineGrid.Enabled = false;
                return;
            }
            else LineGrid.Enabled = true;
            OpenLineGroup();
        }

        void OpenLineGroup()
        {
            update_richtextinput = false;
            LineGrid.Rows.Clear();
            int index = LineGroupPicker.SelectedIndex;
            var line_group = line_group_list[index];
            foreach (Line line in line_group)
            {
                LineGrid.Rows.Add(line.text);
            }
            update_richtextinput = true;
            UpdateRichTextInput();
        }

        //whatever i give up now from now on in below
        private void StartFrameCounter_ValueChanged(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            int new_value = (int)StartFrameCounter.Value;
            switch (current_text_type)
            {
                case TextType.Gameplay:
                    {
                        LinesGame current_line = line_group_list[line_group_index][line_index] as LinesGame;
                        if (current_line == null) return;
                        if (current_line.frame_start != new_value)
                        {
                            current_line.frame_start = new_value;
                        }
                    }
                    break;
                case TextType.Demo3D:
                    {
                        Lines3D current_line = line_group_list[line_group_index][line_index] as Lines3D;
                        if (current_line == null) return;
                        if (current_line.frame_start != new_value)
                        {
                            current_line.frame_start = new_value;
                        }
                    }
                    break;
            }
        }

        private void EndFrameCounter_ValueChanged(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            int new_value = (int)EndFrameCounter.Value;
            switch (current_text_type)
            {
                case TextType.Gameplay:
                    {
                        LinesGame current_line = line_group_list[line_group_index][line_index] as LinesGame;
                        if (current_line == null) return;
                        if (current_line.frame_end != new_value)
                        {
                            current_line.frame_end = new_value;
                        }
                    }
                    break;
                case TextType.Demo3D:
                    {
                        Lines3D current_line = line_group_list[line_group_index][line_index] as Lines3D;
                        if (current_line == null) return;
                        if (current_line.frame_end != new_value)
                        {
                            current_line.frame_end = new_value;
                        }
                    }
                    break;
            }
        }

        private void CutInPanelPicker_SelectedValueChanged(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            CutInPanel new_value = (CutInPanel)CutInPanelPicker.SelectedIndex;
            if (current_text_type == TextType.Gameplay)
            {

                LinesGame current_line = line_group_list[line_group_index][line_index] as LinesGame;
                if (current_line == null) return;
                if (current_line.cutin_panel != new_value)
                {
                    current_line.cutin_panel = new_value;
                }
            }
        }

        private void CutInPositionPicker_SelectedValueChanged(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            CutInPosition new_value = (CutInPosition)CutInPositionPicker.SelectedIndex;
            if (current_text_type == TextType.Gameplay)
            {

                LinesGame current_line = line_group_list[line_group_index][line_index] as LinesGame;
                if (current_line == null) return;
                if (current_line.cutin_position != new_value)
                {
                    current_line.cutin_position = new_value;
                }
            }
        }

        private void BubbleShapePicker_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            BubbleShape new_value = (BubbleShape)BubbleShapePicker.SelectedIndex;
            switch (current_text_type)
            {
                case TextType.Demo2D:
                    {
                        Lines2D current_line = line_group_list[line_group_index][line_index] as Lines2D;
                        if (current_line == null) return;
                        if (current_line.bubble_shape != new_value)
                        {
                            current_line.bubble_shape = new_value;
                        }
                    }
                    break;
                case TextType.Demo3D:
                    {
                        Lines3D current_line = line_group_list[line_group_index][line_index] as Lines3D;
                        if (current_line == null) return;
                        if (current_line.bubble_shape != new_value)
                        {
                            current_line.bubble_shape = new_value;
                        }
                    }
                    break;
            }
        }

        private void BubbleTailPicker_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            BubbleTailDirection new_value = (BubbleTailDirection)BubbleTailPicker.SelectedIndex;
            switch (current_text_type)
            {
                case TextType.Demo2D:
                    {
                        Lines2D current_line = line_group_list[line_group_index][line_index] as Lines2D;
                        if (current_line == null) return;
                        if (current_line.tail_direction != new_value)
                        {
                            current_line.tail_direction = new_value;
                        }
                    }
                    break;
                case TextType.Demo3D:
                    {
                        Lines3D current_line = line_group_list[line_group_index][line_index] as Lines3D;
                        if (current_line == null) return;
                        if (current_line.tail_direction != new_value)
                        {
                            current_line.tail_direction = new_value;
                        }
                    }
                    break;
            }
        }

        private void BubblePositionPicker_SelectionChangeCommitted(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            BubblePosition new_value = (BubblePosition)BubblePositionPicker.SelectedIndex;

            if (current_text_type != TextType.Demo3D) return;

            Lines3D current_line = line_group_list[line_group_index][line_index] as Lines3D;
            if (current_line == null) return;
            if (current_line.bubble_position != new_value)
            {
                current_line.bubble_position = new_value;
            }

        }

        private void BubblePosXCounter_ValueChanged(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            int new_value = (int)BubblePosXCounter.Value;
            Lines2D current_line = line_group_list[line_group_index][line_index] as Lines2D;
            if (current_line == null) return;
            if (current_line.bubble_x_position != new_value)
            {
                current_line.bubble_x_position = new_value;
            }
        }

        private void BubblePosYCounter_ValueChanged(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            int new_value = (int)BubblePosYCounter.Value;
            Lines2D current_line = line_group_list[line_group_index][line_index] as Lines2D;
            if (current_line == null) return;
            if (current_line.bubble_y_position != new_value)
            {
                current_line.bubble_y_position = new_value;
            }
        }

        private void Panel2DIndexCounter_ValueChanged(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            int new_value = (int)Panel2DIndexCounter.Value;
            Lines2D current_line = line_group_list[line_group_index][line_index] as Lines2D;
            if (current_line == null) return;
            if (current_line.panel_index != new_value)
            {
                current_line.panel_index = new_value;
            }
        }

        private void Panel2DBehaviorCounter_ValueChanged(object sender, EventArgs e)
        {
            int line_group_index = LineGroupPicker.SelectedIndex;
            int line_index = LineGrid.CurrentRow.Index;
            int new_value = (int)Panel2DBehaviorCounter.Value;
            Lines2D current_line = line_group_list[line_group_index][line_index] as Lines2D;
            if (current_line == null) return;
            if (current_line.behavior != new_value)
            {
                current_line.behavior = new_value;
            }
        }

        private void StripFileFromAFS_Click(object sender, EventArgs e)
        {
            /*ofd.Title = "Select AFS File";
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
            }*/
        }
    }
}
