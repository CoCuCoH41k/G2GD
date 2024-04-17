using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;

namespace geometrize_to_gd
{
    public class Data
    {
        public List<Shape> shapes { get; set; }

        public Obj[] convert(decimal scale, decimal xMax, decimal yMax, int max_objects)
        {
            max_objects = shapes.Count > max_objects ? max_objects : shapes.Count;

            Obj[] list = new Obj[max_objects + 5]; // 0 index for solid black BG, size - 1..3 for borders. in all 5 empty Objs

            Console.WriteLine("Creating Obj[] array...");
            for (int index = 0; index < max_objects; index++)
            {
                Shape shape = shapes[index];
                decimal[] data = new decimal[shape.data.Count];
                for (int index_data = 0; index_data < shape.data.Count; index_data++)
                {
                    data[index_data] = shape.data[index_data];
                }
                list[index + 1] = new Obj(shape.type, data, shape.color.ToArray<int>(), shape.score);
            }

            Console.WriteLine("Applying scale...");
            for (int index = 0; index < list.Length; index++)
            {
                Obj cur_obj = list[index];
                if (cur_obj == null) continue;

                cur_obj.scale[0] *= scale;
                cur_obj.scale[1] *= scale;
                cur_obj.x *= scale;
                cur_obj.y = (decimal)Math.Abs((float)(cur_obj.y * scale - yMax));

                list[index] = cur_obj;
            }
            return list;
        }
    }

    public class Shape
    {
        public int type { get; set; }
        public List<int> data { get; set; }
        public List<int> color { get; set; }
        public double score { get; set; }
    }

    public class Obj
    {
        public int id { get; set; }
        public decimal x { get; set; }
        public decimal y { get; set; }
        public decimal[] scale { get; set; } = new decimal[2] { 1, 1 };
        public decimal rotation { get; set; }
        public double score { get; set; }
        public int color_channel { get; set; }
        public int editor_level { get; set; } = 0;
        public HSV hsv { get; set; }

        public Obj(int type, decimal[] data, int[] color, double score, int color_channel = 999)
        {
            int[] allowed_ID = { 1, 2, 8, 16, 32 };

            if (!allowed_ID.Contains(type)) throw new InvalidDataException("Unsupported obj ID.");

            hsv = new HSV(Color.FromArgb(color[0], color[1], color[2]));

            this.color_channel = color_channel;

            decimal xMin = (decimal)data[0];
            decimal yMin = (decimal)data[1];

            if (type == 32) // Scaled Circle
            {
                editor_level = 2;
                id = 3621;

                x = xMin;
                y = yMin;

                scale[0] = scale[1] = data[2] / (decimal)15;

                rotation = 0;

                //average_size = (scale[0] * 15) * (scale[1] * 15);

                return;
            }

            decimal xMax = (decimal)data[2];
            decimal yMax = (decimal)data[3];

            this.score = score;

            if (type == 1) // Cube
            {
                editor_level = 1;
                id = 955;

                x = (xMin + xMax) / (decimal)2;
                y = (yMin + yMax) / (decimal)2;

                scale[0] = (xMax - xMin) / (decimal)30;
                scale[1] = (yMax - yMin) / (decimal)30;

                rotation = 0;

                //average_size = (scale[0] * 30) * (scale[1] * 30);

                return;
            }

            if (type == 2) // Rotated n Scaled Cube
            {
                editor_level = 3;
                id = 955;


                x = (xMin + xMax) / (decimal)2;
                y = (yMin + yMax) / (decimal)2;

                scale[0] = (xMax - xMin) / (decimal)30;
                scale[1] = (yMax - yMin) / (decimal)30;

                if (scale[0] < 0 || scale[1] < 0) { Console.WriteLine($"xScale: {scale[0]} | yScale: {scale[1]}"); }

                rotation = (decimal)data[4]; // rotation_offset - 

                //average_size = (scale[0] * 30) * (scale[1] * 30);

                return;
            }

            if (type == 8) // Ellipse
            {
                editor_level = 4;
                id = 3621;

                x = xMin;
                y = yMin;

                xMax = data[0] + data[2];
                yMax = data[1] + data[3];

                scale[0] = (xMax - xMin) / 15;
                scale[1] = (yMax - yMin) / 15;

                rotation = 0;

                //average_size = (scale[0] * 30) * (scale[1] * 30);

                return;
            }

            if (type == 16) // Rotated Ellipse
            {
                editor_level = 5;
                id = 3621;

                x = xMin;
                y = yMin;

                xMax = data[0] + data[2];
                yMax = data[1] + data[3];

                scale[0] = (xMax - xMin) / 15;
                scale[1] = (yMax - yMin) / 15;

                rotation = data[4]; // rotation_offset - 

                //average_size = (scale[0] * 15) * (scale[1] * 15);

                return;
            }

            return;

        }
    }

    public class HSV
    {
        public float hue { get; set; }
        public float saturation { get; set; }
        public float value { get; set; }

        public float[] rgbToHSV(Color color)
        {
            float[] output = new float[3];

            float hue, saturation, value;

            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1f - (1f * min / max);
            value = max / 255f;

            output[0] = hue;
            output[1] = saturation;
            output[2] = value;

            return output;
        }

        public void ColorToHSV(Color color, out double hue, out double saturation, out double value)
        {
            int max = Math.Max(color.R, Math.Max(color.G, color.B));
            int min = Math.Min(color.R, Math.Min(color.G, color.B));

            hue = color.GetHue();
            saturation = (max == 0) ? 0 : 1d - (1d * min / max);
            value = max / 255d;
        }

        public HSV rgb_to_hsv(int r, int g, int b)
        {
            float local_hue, local_saturation, local_value;
            float[] colors = { r / 255f, g / 255f, b / 255f };
            float min = colors.Min();
            float max = colors.Max();
            float range = max - min;
            local_value = range;


            if (min == max) return new HSV(0f, 0f, local_value);

            local_saturation = range / max;

            float rc = (max - colors[0]) / range;
            float gc = (max - colors[1]) / range;
            float bc = (max - colors[2]) / range;

            if (colors[0] == max)
            {
                local_hue = bc - gc;
            }
            else if (colors[1] == max)
            {
                local_hue = 2f + rc - bc;
            }
            else
            {
                local_hue = 4f + gc - rc;
            }
            local_hue = (local_hue / 6f) % 1f;

            return new HSV(local_hue * 360f, local_saturation, local_value);
        }

        public HSV(float h, float s, float v)
        {
            hue = h;
            saturation = s;
            value = v;
        }

        public HSV(int r, int g, int b)
        {
            HSV hsv = rgb_to_hsv(r, g, b);
            hue = hsv.hue; saturation = hsv.saturation; value = hsv.value;
        }

        public HSV(Color color)
        {
            double hue, saturation, value;
            ColorToHSV(color, out hue, out saturation, out value);
            this.hue = (float)hue; this.saturation = (float)saturation; this.value = (float)value;

            if (hue == 0 && saturation == 1 && value >= 0.23) this.hue = 1;


            /*
             * float[] hsv = rgbToHSV(color);
            hue = hsv[0]; saturation = hsv[1]; value = hsv[2];
            */



            /*
            HSV hsv = color_to_hsv(color);
            hue = hsv.hue; saturation = hsv.saturation; value = hsv.value;
            */
        }
    }

    public class Settings
    {
        public string path = null;
        public decimal edge_weight = 30;
        public Axis selected_axis = Axis.Y;
        public decimal size = 0;
        public double scale = 1;
        public int max_objects = 16_000;
        public int editor_layer_offset = 0;

        public Settings() { }

        public Settings (string path, decimal edge_weight, Axis selected_axis, decimal size, int max_objects, int editor_layer_offset)
        {
            this.path = path;
            this.edge_weight = edge_weight;
            this.selected_axis = selected_axis;
            this.size = size;
            this.max_objects = max_objects;
            this.editor_layer_offset = editor_layer_offset;
        }
    }

    public enum Axis
    {
        X, Y
    }
}
