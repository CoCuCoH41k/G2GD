using G2GD;
using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading;

namespace geometrize_to_gd
{
    public class Image_prepare
    {
        public Settings settings;

        public Image_prepare(Settings settings) => this.settings = settings;

        public string prepare(App local_app)
        {
             
            
            string path = settings.path;
            decimal size = settings.size;
            Axis size_axis = settings.selected_axis;
            string json = File.ReadAllText(path);

            decimal scale;

            Data objects = JsonSerializer.Deserialize<Data>(json);

            int all_objs = objects.shapes.Count > settings.max_objects ? settings.max_objects + 4 : objects.shapes.Count + 4;
            int done_objs = 0;

            if (objects.shapes.Count == 0)
            {
                throw new Exception("Invalid JSON shapes list.");
            }

            local_app.set_pb_current(all_objs, done_objs);

            Shape shape = objects.shapes[0];

            decimal original_size_x = shape.data[2] / 30;
            decimal original_size_y = shape.data[3] / 30;

            if (size_axis == Axis.X)
            {
                scale = size != 0 ? size / original_size_x : 1;
            }
            else
            {
                scale = size != 0 ? size / original_size_y : 1;
            }

            decimal xMax, yMax;
            xMax = shape.data[2] * scale; yMax = shape.data[3] * scale;

            Obj[] list = objects.convert(scale, xMax, yMax, settings.max_objects);
            string to_spwn = "extract obj_props\r\nextract {\r\n    SCALE_X: @object_key::{id: 128, pattern: @number, name: \"SCALE_X\"},\r\n    SCALE_Y: @object_key::{id: 129, pattern: @number, name: \"SCALE_Y\"},\r\n    HSV_1_ENABLED: @object_key::{id: 41, pattern: @bool, name: \"HSV_1_ENABLED\"},\r\n    HSV_2_ENABLED: @object_key::{id: 42, pattern: @bool, name: \"HSV_2_ENABLED\"},\r\n    HSV_1_DATA: @object_key::{id: 43, pattern: @string, name: \"HSV_1_DATA\"},\r\n    HSV_2_DATA: @object_key::{id: 44, pattern: @string, name: \"HSV_2_DATA\"}\r\n}\n";

            decimal offset = Convert.ToInt32(Math.Round(settings.edge_weight));

            
            list[list.Length - 4] = new Obj(1, new decimal[] { -offset, -offset, xMax + offset, 0 }, new int[] { 0, 0, 0, 255 }, 0.1, 998);
            list[list.Length - 3] = new Obj(1, new decimal[] { -offset, yMax, xMax + offset, yMax + offset }, new int[] { 0, 0, 0, 255 }, 0.1, 998);
            list[list.Length - 2] = new Obj(1, new decimal[] { -offset, 0, 0, yMax }, new int[] { 0, 0, 0, 255 }, 0.1, 998);
            list[list.Length - 1] = new Obj(1, new decimal[] { xMax, 0, xMax + offset, yMax }, new int[] { 0, 0, 0, 255 }, 0.1, 998);

            local_app.set_pb_current(all_objs, done_objs);

            Console.WriteLine("Doing stuff...");
            Obj bg = list[1];
            xMax = bg.x; yMax = bg.y;

            for (int index = 0; index < list.Length; index++)
            {
                int z_order = -list.Length + index;

                list[index].x -= (xMax) + 30;
                list[index].y -= (yMax) + 30;

                list[index].scale[0] = list[index].scale[0] == 0 ? (decimal) 0.001 : list[index].scale[0];
                list[index].scale[1] = list[index].scale[1] == 0 ? (decimal) 0.001 : list[index].scale[1];

                to_spwn += "$.add(obj {OBJ_ID: " + $"{list[index].id}, X: {list[index].x + offset}, Y: {list[index].y + offset}, SCALE_X: {list[index].scale[0]}, SCALE_Y: {list[index].scale[1]}, ROTATION: {list[index].rotation}, HSV_1_DATA: \"{list[index].hsv.hue}a{list[index].hsv.saturation}a{list[index].hsv.value}a1a1\", HSV_2_DATA: \"{list[index].hsv.hue}a{list[index].hsv.saturation}a{list[index].hsv.value}a1a1\", HSV_1_ENABLED: true, HSV_2_ENABLED: true, EDITOR_LAYER_1: {list[index].editor_level + settings.editor_layer_offset}, COLOR: {list[index].color_channel}c, COLOR_2: {list[index].color_channel}c, Z_LAYER: 1, Z_ORDER: {z_order}" + "})" + $" // Score is {list[index].score}\n";

                done_objs++;

                //Console.WriteLine($" How's going: {(decimal) done_objs/ (decimal) all_objs}");

                local_app.set_pb_current(all_objs, done_objs);
            }

            Console.WriteLine("Returning data...");

            local_app.set_pb_images(1, 1);

            

            return to_spwn;
        }
    }
}
