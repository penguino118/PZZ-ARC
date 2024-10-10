using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GioGioTXT.ggText;
using static GioGioTXT.Form1;
using System.IO;

namespace GioGioTXT
{
    internal class ggText
    {
        public class Line
        {
            public string text { get; set; } = "";
        }

        public class Lines2D : Line
        {
            public int panel_index { get; set; }
            public int bubble_x_position { get; set; }
            public int bubble_y_position { get; set; }
            public BubbleShape bubble_shape { get; set; }
            public BubbleTailDirection tail_direction { get; set; }
            public int behavior { get; set; }
            
        }

        public class Lines3D : Line
        {
            public int frame_start { get; set; }
            public int frame_end { get; set; }
            public BubbleShape bubble_shape { get; set; }
            public BubbleTailDirection tail_direction { get; set; }
            public BubblePosition bubble_position { get; set; }
        }

        public class LinesGame : Line
        {
            public int frame_start { get; set; }
            public int frame_end { get; set; }
            public CutInPanel cutin_panel { get; set; }
            public CutInPosition cutin_position { get; set; }
        }

        public enum BubbleTailDirection : byte
        {
            UpperLeft,
            UpperRight,
            BottomLeft,
            BottomRight
        }

        public enum BubbleShape : byte
        {
            SpeechTiny,
            SpeechMedium,
            SpeechBig,
            ThoughtTiny,
            ThoughtMedium,
            ThoughtBig,
            ScreamTiny,
            ScreamMedium,
            ScreamBig,
            GhostTiny,
            GhostMedium,
            GhostBig,
            NarrationTiny,
            NarrationMedium,
            NarrationBig
        }

        public enum BubblePosition : byte
        {
            TopLeft,
            TopMiddle,
            TopRight,
            CenterLeft,
            CenterMiddle,
            CenterRight,
            BottomLeft,
            BottomMiddle,
            BottomRight
        }

        public enum CutInPosition : byte
        {
            Left,
            Right,
        }

        public enum CutInPanel : byte
        {
            PlayerNeutral,
            PlayerShock,
            PlayerHurt,
            PlayerAttack,
            EnemyNeutral,
            EnemyShock,
            EnemyHurt,
            EnemyAttack,
            StageA, // sd{xxx}.pzz
            StageB,
            StaceC,
            StageD
        }

        static public void AddLineAsGameLine(List<List<Line>> line_group_list, List<Line> line_list, string line)
        {
            string[] line_args = line.Split(",", 6);

            if (line_args.Length >= 4)
            {
                int frame_start = Int32.Parse(line_args[0]);
                int frame_end = Int32.Parse(line_args[1]);
                byte cutin_panel = Byte.Parse(line_args[2]);
                byte cutin_position = Byte.Parse(line_args[3]);
                string text = line_args[5];

                line_list.Add(new LinesGame()
                {
                    frame_start = frame_start,
                    frame_end = frame_end,
                    cutin_panel = (CutInPanel)cutin_panel,
                    cutin_position = (CutInPosition)cutin_position,
                    text = text
                });
            }
            else
            {
                bool its_a_number = int.TryParse(line_args[0], out _); // some files just have empty lines I guess 
                if (its_a_number)
                {
                    int end_test = Int32.Parse(line_args[0]);

                    if (end_test == -1)
                    {
                        //the dialogue data for this "sequence" is over
                        line_group_list.Add(new List<Line>(line_list));
                        line_list.Clear();
                    }
                }
            }
        }

        static public void AddLineAs2DLine(List<List<Line>> line_group_list, List<Line> line_list, string line)
        {
            string[] line_args = line.Split(",", 7);

            if (line_args.Length >= 6)
            {
                int panel_index = Int32.Parse(line_args[0]);
                int bubble_x_position = Int32.Parse(line_args[1]);
                int bubble_y_position = Int32.Parse(line_args[2]);
                byte bubble_shape = Byte.Parse(line_args[3]);
                byte bubble_tail = Byte.Parse(line_args[4]);
                int panel_behavior = Int32.Parse(line_args[5]);
                string text = line_args[6];

                line_list.Add(new Lines2D()
                {
                    panel_index = panel_index,
                    bubble_x_position = bubble_x_position,
                    bubble_y_position = bubble_y_position,
                    bubble_shape = (BubbleShape)bubble_shape,
                    tail_direction = (BubbleTailDirection)bubble_tail,
                    behavior = panel_behavior,
                    text = text
                });
            }
        }

        static public void AddLineAs3DLine(List<List<Line>> line_group_list, List<Line> line_list, string line)
        {
            string[] line_args = line.Split(",", 6);

            if (line_args.Length >= 5)
            {
                int frame_start = Int32.Parse(line_args[0]);
                int frame_end = Int32.Parse(line_args[1]);
                byte bubble_shape = Byte.Parse(line_args[2]);
                byte bubble_tail = Byte.Parse(line_args[3]);
                int bubble_position = Int32.Parse(line_args[4]);
                string text = line_args[5];

                line_list.Add(new Lines3D()
                {
                    frame_start = frame_start,
                    frame_end = frame_end,
                    bubble_shape = (BubbleShape)bubble_shape,
                    tail_direction = (BubbleTailDirection)bubble_tail,
                    bubble_position = (BubblePosition)bubble_position,
                    text = text
                });
            }
        }

        static public void AddNewLineToList(List<Line> line_list, TextType text_type, string new_text)
        {
            switch (text_type)
            {
                case TextType.Gameplay:
                    line_list.Add(new LinesGame()
                    {
                        frame_start = 0,
                        frame_end = 0,
                        cutin_panel = (CutInPanel)0,
                        cutin_position = (CutInPosition)0,
                        text = new_text
                    });
                    break;
                
                case TextType.Demo2D:
                    line_list.Add(new Lines2D()
                    {
                        panel_index = 0,
                        bubble_x_position = 0,
                        bubble_y_position = 0,
                        bubble_shape = (BubbleShape)0,
                        tail_direction = (BubbleTailDirection)0,
                        behavior = 0,
                        text = new_text
                    });
                    break;
                
                case TextType.Demo3D:
                    line_list.Add(new Lines3D()
                    {
                        frame_start = 0,
                        frame_end = 0,
                        bubble_shape = (BubbleShape)0,
                        tail_direction = (BubbleTailDirection)0,
                        bubble_position = (BubblePosition)0,
                        text = new_text
                    });
                    break;
            }
            
        }
        
        static public void WriteLinesToStream(FileStream stream, List<List<Line>> line_group_list, TextType text_type, Encoding shift_jis)
        {
            using (StreamWriter writer = new StreamWriter(stream, shift_jis)) // SHIFT-JIS
            {
                foreach (var line_group in line_group_list)
                {
                    foreach (var line in line_group)
                    {
                        var strbuilder = new StringBuilder();

                        switch (text_type)
                        {
                            case TextType.Gameplay:
                                {
                                    LinesGame game_line = line as LinesGame;
                                    strbuilder.Append(game_line.frame_start).Append(",");
                                    strbuilder.Append(game_line.frame_end).Append(",");
                                    strbuilder.Append((int)game_line.cutin_panel).Append(",");
                                    strbuilder.Append((int)game_line.cutin_position).Append(",");
                                    strbuilder.Append("0").Append(","); // weird unused value
                                }
                                break;

                            case TextType.Demo2D:
                                {
                                    Lines2D game_line = line as Lines2D;
                                    strbuilder.Append(game_line.panel_index).Append(",");
                                    strbuilder.Append(game_line.bubble_x_position).Append(",");
                                    strbuilder.Append(game_line.bubble_y_position).Append(",");
                                    strbuilder.Append((int)game_line.bubble_shape).Append(",");
                                    strbuilder.Append((int)game_line.tail_direction).Append(",");
                                    strbuilder.Append(game_line.behavior).Append(",");
                                }
                                break;

                            case TextType.Demo3D:
                                {
                                    Lines3D game_line = line as Lines3D;
                                    strbuilder.Append(game_line.frame_start).Append(",");
                                    strbuilder.Append(game_line.frame_end).Append(",");
                                    strbuilder.Append((int)game_line.bubble_shape).Append(",");
                                    strbuilder.Append((int)game_line.tail_direction).Append(",");
                                    strbuilder.Append((int)game_line.bubble_position).Append(",");
                                }
                                break;
                        }
                        strbuilder.Append(line.text).Append("\n");
                        stream.Write(shift_jis.GetBytes(strbuilder.ToString()));
                    }
                    stream.Write(shift_jis.GetBytes("-1\n"));
                }
            }
        }

        // fuck it
        static public void WriteLinesToMemoryStream(MemoryStream stream, List<List<Line>> line_group_list, TextType text_type, Encoding shift_jis)
        {
            using (StreamWriter writer = new StreamWriter(stream, shift_jis)) // SHIFT-JIS
            {
                foreach (var line_group in line_group_list)
                {
                    foreach (var line in line_group)
                    {
                        var strbuilder = new StringBuilder();

                        switch (text_type)
                        {
                            case TextType.Gameplay:
                                {
                                    LinesGame game_line = line as LinesGame;
                                    strbuilder.Append(game_line.frame_start).Append(",");
                                    strbuilder.Append(game_line.frame_end).Append(",");
                                    strbuilder.Append((int)game_line.cutin_panel).Append(",");
                                    strbuilder.Append((int)game_line.cutin_position).Append(",");
                                    strbuilder.Append("0").Append(","); // weird unused value
                                }
                                break;

                            case TextType.Demo2D:
                                {
                                    Lines2D game_line = line as Lines2D;
                                    strbuilder.Append(game_line.panel_index).Append(",");
                                    strbuilder.Append(game_line.bubble_x_position).Append(",");
                                    strbuilder.Append(game_line.bubble_y_position).Append(",");
                                    strbuilder.Append((int)game_line.bubble_shape).Append(",");
                                    strbuilder.Append((int)game_line.tail_direction).Append(",");
                                    strbuilder.Append(game_line.behavior).Append(",");
                                }
                                break;

                            case TextType.Demo3D:
                                {
                                    Lines3D game_line = line as Lines3D;
                                    strbuilder.Append(game_line.frame_start).Append(",");
                                    strbuilder.Append(game_line.frame_end).Append(",");
                                    strbuilder.Append((int)game_line.bubble_shape).Append(",");
                                    strbuilder.Append((int)game_line.tail_direction).Append(",");
                                    strbuilder.Append((int)game_line.bubble_position).Append(",");
                                }
                                break;
                        }
                        strbuilder.Append(line.text).Append("\n");
                        stream.Write(shift_jis.GetBytes(strbuilder.ToString()));
                    }
                    stream.Write(shift_jis.GetBytes("-1\n"));
                }
            }
        }
    }
}
