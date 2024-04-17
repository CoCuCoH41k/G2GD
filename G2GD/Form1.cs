using G2GD.Properties;
using geometrize_to_gd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Settings = geometrize_to_gd.Settings;

namespace G2GD
{
    public partial class App : Form
    {
        private Thread _thread = null;

        public App local_app = null;

        public Data current_file_data;

        public bool file_opened = false;
        public string file_path = String.Empty;

        public Shape bg_object;

        public int images_count = 0;
        public int images_done = 0;

        public List<string> program_results = new List<string>();


        public App(bool init = true)
        {
            Form app = ActiveForm;;
            if (init) InitializeComponent();
        }

        public App set_local_app()
        {
            local_app = this;
            return this;
        }

        private void set_enabled_settings(bool enabled)
        {
            runButton.Enabled = enabled;
            height.Enabled = enabled;
            width.Enabled = enabled;
            inputSize.Enabled = enabled;
            borderSizeInput.Enabled = enabled;
            maxObjectsInput.Enabled = enabled;
            editorLayerOffsetInput.Enabled = enabled;

            if (enabled)
            {
                Data data = JsonSerializer.Deserialize<Data>(File.ReadAllText(file_path));
                bg_object = data.shapes[0];

                set_settings();
            }
        }

        public void set_pb_images(int images_count, int images_done = 0)
        {
            RunOnUiThread(() =>
            {
                pbImages.Value = (int)Math.Round(((decimal)images_done / (decimal)images_count) * 100);
                imagesState.Text = $"Images done: {images_done}/{images_count}";
            });
        }

        public void set_pb_current(int objects_count, int objects_done = 0)
        {
            RunOnUiThread(() =>
            {
                if (objects_count == 0 && objects_done == 0)
                {
                    pbCurrentImage.Value = 0;
                    currentImageState.Text = "Current image: 0/0";
                    return;
                }

                pbCurrentImage.Value = (int)Math.Round(((decimal)objects_done / (decimal)objects_count) * 100);
                currentImageState.Text = $"Current image: {objects_done}/{objects_count}";
            });
        }

        private void set_settings()
        {
            decimal size;

            if (width.Checked) { size = bg_object.data[2] / 30; } else { size = bg_object.data[3] / 30; }

            inputSize.Text = $"{size}";
        }

        private Settings get_raw_settings()
        {
            Settings settings = new Settings();

            settings.selected_axis = width.Checked ? Axis.X : Axis.Y;
            settings.size = Decimal.Parse(inputSize.Text);
            settings.edge_weight = Decimal.Parse(borderSizeInput.Text) * 30;
            settings.max_objects = int.Parse(maxObjectsInput.Text);
            settings.editor_layer_offset = Math.Abs(int.Parse(editorLayerOffsetInput.Text));

            return settings;
        }

        private void RunOnUiThread(Action action)
        {
            if (InvokeRequired)
            {
                Invoke(action);
            }
            else
            {
                action();
            }
        }

        public bool local_start_point(string path)
        {
            if (_thread == null)
            {

                
                set_pb_images(1);

                _thread = new Thread(() => prepare_raw(path));
                _thread.Name = "image_prepare_thread";
                _thread.IsBackground = true;
                _thread.Start();

                return true;
            }
            else
            {
                Console.WriteLine("Idk dfkb;kdfbjdfkgdgpk");

                return false;
            }
        }

        private void prepare_raw(string path)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.GetCultureInfo("en-US");

            Settings settings = get_raw_settings();
            settings.path = path;

            

            string output = new Image_prepare(settings).prepare(local_app);

            // Call saveFileDialog for output
            RunOnUiThread(() =>
            {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Filter = "SPWN files (*.spwn) | *.spwn";
                dialog.DefaultExt = "spwn";
                dialog.FileName = Path.GetFileNameWithoutExtension(path);

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    Stream file = dialog.OpenFile();
                    StreamWriter writer = new StreamWriter(file);

                    writer.Write(output);

                    writer.Close();
                    file.Close();
                }

                if (_thread != null) {
                    _thread.Abort();
                    _thread = null;
                }
            });
            

            
        }

        private void drop_open(object sender, DragEventArgs e)
        {
            openFileButton.Text = "Nom!";

            string[] paths = (string[]) e.Data.GetData(DataFormats.FileDrop);
            file_path = paths[0];

            file_opened = File.Exists(file_path);

            set_enabled_settings(file_opened);

            openFileButton.Text = "Drop/Open file";
        }
        
        private void drop_enter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                openFileButton.Text = "DROP!";
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void drop_leave(object sender, EventArgs e)
        {
            openFileButton.Text = "Drop/Open file";
        }

        private void click_open(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            dialog.Filter = "Any json (*.json) | *.json";
            dialog.Multiselect = false;

            openFileButton.Text = "Opening file";
            
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                file_path = dialog.FileNames[0];
                file_opened = File.Exists(file_path);

                set_enabled_settings(file_opened);
            }

            openFileButton.Text = "Drop/Open file";
        }

        private void size_axis_change(object sender, EventArgs e)
        {
            decimal size;

            if (width.Checked) { size = bg_object.data[2] / 30; } else { size = bg_object.data[3] / 30; }

            inputSize.Text = $"{size}";
        }

        private void click_run(object sender, EventArgs e)
        {
            if (!file_opened) return;

            local_start_point(file_path);
        }

        private void open_donate(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://www.donationalerts.com/r/cocucka_xd");
        }
    }
}
