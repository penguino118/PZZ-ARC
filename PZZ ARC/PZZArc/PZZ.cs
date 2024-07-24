using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Xml.Linq;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.ComponentModel.DataAnnotations;

namespace PZZ_ARC.PZZArc
{
    internal class PZZ
    {
        public List<String> file_types = new List<string> { 
            "ModelData", "SkeletonData", "TextureData", "AnimationData", "ShadowData",
            "CollisionData", "StageCollisionData", "TextData", "Unknown" };        
        
        public class PZZFile
        {
            [Browsable(false)]
            public required bool compressed { get; set; }
            
            [Browsable(false)]
            public required byte[] byte_array { get; set; }

            [Browsable(false)]
            public required string type { get; set; }


            // PROPERTY GRID DISPLAY below

            [Category("PZZ Entry")]
            [DisplayName("Detected Data Type")]
            [Description("The type of data which the file contains")]
            public string display_type
            {
                get { return this.type; }
            }

            [Category("PZZ Entry")]
            [DisplayName("Compressed")]
            [Description("Compress the file data before saving")]
            public bool display_compressed
            {
                get { return this.compressed; }
                set { this.compressed = value; }
            }
        }
        
        class AMOFile : PZZFile
        {
            int mesh_count
            {
                get
                {
                    using (var stream = new MemoryStream(this.byte_array))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            stream.Position = 0x14;
                            int skip = reader.ReadInt32();
                            stream.Position += skip - 0x8; // should land on 0x2 sector count
                            return reader.ReadInt32();
                        }
                    }
                }
                set { }
            }

            int material_count
            {
                get
                {
                    using (var stream = new MemoryStream(this.byte_array))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            stream.Position = 0x14;
                            int skip = reader.ReadInt32();
                            stream.Position += skip - 0x4;
                            skip = reader.ReadInt32();
                            stream.Position += skip - 0x8; // should land on 0x9 sector count
                            return reader.ReadInt32();
                        }
                    }
                }
                set { }
            }

            int texture_count
            {
                get
                {
                    using (var stream = new MemoryStream(this.byte_array))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            stream.Position = 0x14;
                            int skip = reader.ReadInt32();
                            stream.Position += skip - 0x4;
                            skip = reader.ReadInt32();
                            stream.Position += skip - 0x4;
                            skip = reader.ReadInt32();
                            stream.Position += skip - 0x8; // should land on 0xA sector count
                            return reader.ReadInt32();
                        }
                    }
                }
                set { }
            }
            // PROPERTY GRID DISPLAY below
            [Category("Artistoon Model")]
            [DisplayName("Mesh Count")]
            [Description("Amount of meshes contained in the model")]
            public int display_mesh_count
            {
                get { return this.mesh_count; }
            }

            [Category("Artistoon Model")]
            [DisplayName("Material Count")]
            [Description("Amount of materials used by the model")]
            public int display_material_count
            {
                get { return this.material_count; }
            }

            [Category("Artistoon Model")]
            [DisplayName("Texture Count")]
            [Description("Amount of textures listed by the model")]
            public int display_texture_count
            {
                get { return this.texture_count; }
            }
        }

        class TXBFile : PZZFile
        {
            int texture_count 
            {
                get
                {
                    using (var stream = new MemoryStream(this.byte_array))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            return reader.ReadInt32();
                        }
                    }
                }
                set { }
            }
            
            // PROPERTY GRID DISPLAY below

            [Category("Texture Batch")]
            [DisplayName("Texture Count")]
            [Description("How many textures are contained inside this texture batch")]
            public int display_texture_count
            {
                get { return this.texture_count; }
            }
        }

        class TextFile : PZZFile
        {
            public Encoding shift_jis = CodePagesEncodingProvider.Instance.GetEncoding(932);
            string text
            {

                get
                {
                    using (var stream = new MemoryStream(this.byte_array))
                    {
                        try // ugh
                        {
                            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                            using (StreamReader reader = new StreamReader(stream, shift_jis)) // SHIFT-JIS
                            {
                                return reader.ReadToEnd();
                            }
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error while getting encoded text data!\n" + e.Message + "Using default encoding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return System.Text.Encoding.Default.GetString(this.byte_array);
                        }
                        
                    }
            }
                set {
                    using (var stream = new MemoryStream())
                    {
                        try // uuuuugh
                        {
                            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                            using (StreamWriter writer = new StreamWriter(stream, shift_jis)) // SHIFT-JIS
                            {
                                writer.Write(value);
                            }
                            this.byte_array = stream.ToArray(); // sure?
                        }
                        catch (Exception e)
                        {
                            MessageBox.Show("Error while writing encoded text data!\n" + e.Message + "Using default encoding.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.byte_array = Encoding.Default.GetBytes(value);
                        }
                        
                    }
                }
            }

            // PROPERTY GRID DISPLAY below
            [Category("Text Data")]
            [DisplayName("Raw Text")]
            [Description("All written text inside the file")]
            [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
            public string display_text
            {
                get { return this.text; }
                set { this.text = value; }
            }

        }

        class HITSFile : PZZFile // Stage Collision
        {

            int[] grid_size
            {
                get
                {
                    using (var stream = new MemoryStream(this.byte_array))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            stream.Position += 0x8;
                            int grid_size_x = reader.ReadInt32();
                            int grid_size_y = reader.ReadInt32();
                            return [grid_size_x, grid_size_y];
                        }
                    }
                }
            }

            int[] grid_count
            {
                get
                {
                    using (var stream = new MemoryStream(this.byte_array))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            stream.Position += 0x10;
                            int grid_count_x = reader.ReadInt32();
                            int grid_count_y = reader.ReadInt32();
                            return [grid_count_x, grid_count_y];
                        }
                    }
                }
            }

            float[] grid_offset
            {
                get
                {
                    using (var stream = new MemoryStream(this.byte_array))
                    {
                        using (var reader = new BinaryReader(stream))
                        {
                            stream.Position += 0x18;
                            float grid_offset_x = reader.ReadSingle();
                            float grid_offset_y = reader.ReadSingle();
                            return [grid_offset_x, grid_offset_y];
                        }
                    }
                }
            }
            // PROPERTY GRID DISPLAY below

            [Category("Stage Collison")]
            [DisplayName("Grid Size")]
            [Description("Size of the collision grid, in the XZ axis")]
            public int[] display_grid_size
            {
                get { return this.grid_size; }
            }

            [Category("Stage Collison")]
            [DisplayName("Grid Cell Count")]
            [Description("Numbers of cell across the grid, in the XZ axis")]
            public int[] display_grid_count
            {
                get { return this.grid_count; }
            }

            [Category("Stage Collison")]
            [DisplayName("Grid Offset")]
            [Description("Position offset of the grid, in the XZ axis")]
            [DisplayFormat]
            public float[] display_grid_offset
            {
                get { return this.grid_offset; }
            }
        }

        static public List<PZZFile> UnpackFromFile(string file_path)
        {
            List<PZZFile> file_list = new List<PZZFile>();
            
            using (var stream = File.Open(file_path, FileMode.Open))
            {
                UnpackFromStream(stream, file_list);
            }
            return file_list;
        }

        static public void UnpackFromStream(Stream stream, List<PZZFile> file_list)
        {
            long file_reader_position = 0x800;
            using (var reader = new BinaryReader(stream))
            {
                int file_count = reader.ReadInt32();

                for (int i = 0; i < file_count; i++)
                {
                    short sector_count = reader.ReadInt16();
                    bool is_compressed = reader.ReadInt16() == -32768;

                    long original_stream_position = stream.Position;
                    stream.Position = file_reader_position; // get da files
                    byte[] file_buffer = reader.ReadBytes(sector_count * 0x800); // get bytes
                    if (is_compressed) file_buffer = GetDecompressed(file_buffer); // decompress if compressed
                    file_reader_position = stream.Position;
                    stream.Position = original_stream_position;

                    string type = "Unknown";
                    if (file_buffer.Length > 4) type = GetFileType(file_buffer);


                    AddToList(file_list, file_buffer, is_compressed, type);

                }
            }
        }

        static public void WriteOutputData(BinaryWriter writer, FileStream stream, List<PZZFile> file_list)
        {
            if (file_list.Count > 255)
            {
                MessageBox.Show("Cannot fit more than 255 files into a single PZZ archive!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            writer.Write(file_list.Count);
            List<byte> output_file_data = new List<byte>();
            foreach (var file in file_list)
            {
                short sector_size = 0;
                short compressed_flag = 0;
                byte[] file_bytes = file.byte_array;

                if (file.compressed)
                {
                    compressed_flag = -32768; // 0x8000
                    file_bytes = GetCompressed(file_bytes); // compress bytes if necessary
                }

                file_bytes = PadToSectorSize(file_bytes, out sector_size); //pad ending to fit to sector sizes

                writer.Write(sector_size);
                writer.Write(compressed_flag);

                output_file_data.AddRange(file_bytes);
            }

            int pad_size = (int)(0x800 - stream.Position) / 4; //padding until file data start at 0x800
            for (int i = 0; i < pad_size; i++) writer.Write(0x0);
            writer.Write(output_file_data.ToArray()); // add file data

        }

        static public void ReplaceFileOnList(List<PZZFile> file_list, int index, byte[] new_file_buffer)
        {
            PZZFile replaced_file = file_list[index];
            bool is_compressed = replaced_file.compressed;
            string new_type = GetFileType(new_file_buffer);

            file_list.RemoveAt(index);
            AddToList(file_list, new_file_buffer, is_compressed, new_type, index);
        }

        static public void AddToList(List<PZZFile> file_list, byte[] file_buffer, bool is_compressed, string type)
        {
            switch (type)
            {
                case "TextureData": file_list.Add(new TXBFile() { compressed = is_compressed, byte_array = file_buffer, type = type, }); break;
                case "ModelData": file_list.Add(new AMOFile() { compressed = is_compressed, byte_array = file_buffer, type = type, }); break;
                case "TextData": file_list.Add(new TextFile() { compressed = is_compressed, byte_array = file_buffer, type = type, }); break;
                case "StageCollisionData": file_list.Add(new HITSFile() { compressed = is_compressed, byte_array = file_buffer, type = type, }); break;
                default: file_list.Add(new PZZFile() { compressed = is_compressed, byte_array = file_buffer, type = type, }); break;
            }
        }

        static public void AddToList(List<PZZFile> file_list, byte[] file_buffer, bool is_compressed, string type, int index)
        {
            switch (type)
            {
                case "TextureData": file_list.Insert(index, new TXBFile() { compressed = is_compressed, byte_array = file_buffer, type = type, }); break;
                case "ModelData": file_list.Insert(index, new AMOFile() { compressed = is_compressed, byte_array = file_buffer, type = type, }); break;
                case "TextData": file_list.Insert(index, new TextFile() { compressed = is_compressed, byte_array = file_buffer, type = type, }); break;
                case "StageCollisionData": file_list.Insert(index, new HITSFile() { compressed = is_compressed, byte_array = file_buffer, type = type, }); break;
                default: file_list.Insert(index, new PZZFile() { compressed = is_compressed, byte_array = file_buffer, type = type, }); break;
            }
        }

        private static byte[] PadToSectorSize(byte[] infile, out short sector_size)
        {

            if (infile.Length <= 0)
            {
                sector_size = 0;
                return infile;  // wont pad nothing to something
            }

            sector_size = 1;

            for (short i = 0; (i * 0x800) < infile.Length; i++) //calculate filesize sectors
            {
                sector_size = i;
            }

            sector_size++;

            List<byte> output_bytes = new List<byte>(); //pad bytes until sector size is met
            output_bytes.AddRange(infile);

            int padding_size = ((sector_size * 0x800) - infile.Length);

            for (int i = 0; i < padding_size; i++)
            {
                output_bytes.Add(0x0);
            }

            return output_bytes.ToArray();
        }

        public static string GetFileType(byte[] infile)
        {
            try
            {
                using (var stream = new MemoryStream(infile))
                {
                    using (var reader = new BinaryReader(stream))
                    {
                        uint test_int1 = reader.ReadUInt32();
                        uint test_int2 = reader.ReadUInt32();
                        stream.Position = 0x0;
                        string test_read = reader.ReadString();

                        if (test_int1 == 1 & (test_int2 == 4 || test_int2 == 3)) return "ModelData";
                        if (test_int1 == 0xC0000000) return "SkeletonData";
                        if (test_int2 == 0 & test_int1 > 0)
                        {
                            stream.Position = 0xC;
                            uint test_offset = reader.ReadUInt32(); // get first texture offset
                            stream.Position = test_offset;
                            uint test_tim2 = reader.ReadUInt32(); // should be "TIM2"
                            if (test_tim2 == 0x324D4954) return "TextureData";
                        }
                        if (test_int2 == 0x40) return "AnimationData"; // todo: flawed?
                        if (test_int2 == 0x50000) return "ShadowData";
                        if (test_int1 == 0x20) return "CollisionData";
                        if (test_int1 == 0x53544948) return "StageCollisionData";
                        if (test_read.Contains(",") || test_read.Contains("-1")) return "TextData"; // todo: sorta flawed
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while detecting file type!\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return "Unknown";
        }

        public static string GetFileExtension(string filetype)
        {
            switch (filetype)
            {
                case "ModelData": return "amo"; 
                case "SkeletonData": return "ahi";
                case "TextureData": return "txb";
                case "AnimationData": return "aan";
                case "ShadowData": return "sdt";
                case "CollisionData": return "hit";
                case "StageCollisionData": return "hits";
                case "TextData": return "txt";
                default: return "bin";
            }
        }

        public static string GetFileName(string filename, int index, string type) // for files inside the pzz
        {
            return Path.GetFileNameWithoutExtension(filename) + "_" + index.ToString("D3") + "." + GetFileExtension(type);
        }

        private static byte[] GetDecompressed(byte[] infile)
        {
            //---------------------------------------------------------------------//
            //originally written in C by infval
            //https://github.com/infval/pzzcompressor_jojo/blob/master/pzzcomp_jojo.c
            //---------------------------------------------------------------------//
            List<byte> output_bytes = new List<byte> { };
            
            int size_b = infile.Length / 2 * 2;
            int cb = 0;  // Control bytes
            int cb_bit = -1;
            int count;
            int offset;
            int dpos = 0;
            int spos = 0;
            try
            {
                while (spos < size_b)
                {
                    if (cb_bit < 0)
                    {
                        cb = infile[spos++] << 0;
                        cb |= infile[spos++] << 8;
                        cb_bit = 15;
                    }

                    int compress_flag = cb & (1 << cb_bit);
                    cb_bit--;

                    if (compress_flag != 0)
                    {
                        count = infile[spos++] << 0;
                        count |= infile[spos++] << 8;
                        offset = (count & 0x7FF) * 2;

                        if (offset == 0) break; // End of the compressed data

                        count >>= 11;
                        if (count == 0)
                        {
                            count = infile[spos++] << 0;
                            count |= infile[spos++] << 8;
                        }
                        count *= 2;
                        for (int j = 0; j < count; j++)
                        {
                            output_bytes.Insert(dpos, output_bytes[dpos - offset]);
                            dpos++;
                        }
                    }
                    else
                    {
                        output_bytes.Insert(dpos++, infile[spos++]);
                        output_bytes.Insert(dpos++, infile[spos++]);
                    }
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while decompressing!\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return output_bytes.ToArray();
        }

        private static byte[] GetCompressed(byte[] infile)
        {
            //---------------------------------------------------------------------//
            //originally written in C by infval
            //https://github.com/infval/pzzcompressor_jojo/blob/master/pzzcomp_jojo.c
            //---------------------------------------------------------------------//
            int max_compressed_size = (2 + 2)             // Last 0x0000
                                    + infile.Length       //
                                    + infile.Length / 16; // 0x0000 for every 32 bytes

            List<byte> output_bytes = Enumerable.Repeat((byte)(0x0), max_compressed_size).ToList(); //new List<byte> { };

            int size_b = infile.Length / 2 * 2;
            int cb = 0;  // Control bytes
            int cb_bit = 15;
            int cb_pos = 0;

            int dpos = 0;
            int spos = 0;

            //output_bytes.Insert(dpos++, 0x0);
            //output_bytes.Insert(dpos++, 0x0);
            output_bytes[dpos++] = 0x0;
            output_bytes[dpos++] = 0x0;
            try
            {
                while (spos < size_b)
                {
                    int offset = 0;
                    int length = 0;

                    for (int i = (spos >= 0x7FF * 2 ? spos - 0x7FF * 2 : 0); i < spos; i += 2)
                    {
                        if (infile[i] == infile[spos] && infile[i + 1] == infile[spos + 1])
                        {
                            int cur_len = 0;
                            do
                            {
                                cur_len += 2;
                            } while ((cur_len < 0xFFFF * 2)
                                && (spos + cur_len < size_b)
                                && infile[i + cur_len] == infile[spos + cur_len]
                                && infile[i + 1 + cur_len] == infile[spos + 1 + cur_len]);

                            if (cur_len > length)
                            {
                                offset = spos - i;
                                length = cur_len;
                                if (length >= 0xFFFF * 2)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    int compress_flag = 0;
                    if (length >= 4)
                    {
                        compress_flag = 1;
                        offset /= 2;
                        length /= 2;
                        int c = offset;
                        if (length <= 0x1F)
                        {
                            c |= length << 11;
                            output_bytes[dpos++] = (byte)(c & 0xFF);
                            output_bytes[dpos++] = (byte)(c >> 8);
                        }
                        else
                        {
                            output_bytes[dpos++] = (byte)(c & 0xFF);
                            output_bytes[dpos++] = (byte)(c >> 8);
                            output_bytes[dpos++] = (byte)(length & 0xFF);
                            output_bytes[dpos++] = (byte)(length >> 8);
                        }
                        spos += length * 2;
                    }
                    else
                    {
                        output_bytes[dpos++] = infile[spos++];
                        output_bytes[dpos++] = infile[spos++];
                    }

                    cb |= compress_flag << cb_bit;
                    cb_bit--;

                    if (cb_bit < 0)
                    {
                        output_bytes[cb_pos + 0] = (byte)(cb & 0xFF);
                        output_bytes[cb_pos + 1] = (byte)(cb >> 8);
                        cb = 0x0000;
                        cb_bit = 15;
                        cb_pos = dpos;
                        output_bytes[dpos++] = 0x00;
                        output_bytes[dpos++] = 0x00;
                    }
                }
                cb |= 1 << cb_bit;
                output_bytes[cb_pos + 0] = (byte)(cb & 0xFF);
                output_bytes[cb_pos + 1] = (byte)(cb >> 8);
                output_bytes[dpos++] = 0x00;
                output_bytes[dpos++] = 0x00;
            }
            catch (Exception e)
            {
                MessageBox.Show("Error while compressing!\n" + e.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            output_bytes = output_bytes.Slice(0, dpos);
            return output_bytes.ToArray();
        }
    }
}
