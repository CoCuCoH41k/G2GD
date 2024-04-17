namespace G2GD
{
    partial class App
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            this.openFileButton = new System.Windows.Forms.Button();
            this.width = new System.Windows.Forms.RadioButton();
            this.height = new System.Windows.Forms.RadioButton();
            this.inputSize = new System.Windows.Forms.TextBox();
            this.pbImages = new System.Windows.Forms.ProgressBar();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.runButton = new System.Windows.Forms.Button();
            this.imagesState = new System.Windows.Forms.Label();
            this.currentImageState = new System.Windows.Forms.Label();
            this.pbCurrentImage = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.borderSizeInput = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.maxObjectsInput = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.editorLayerOffsetInput = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // openFileButton
            // 
            this.openFileButton.AllowDrop = true;
            this.openFileButton.Location = new System.Drawing.Point(12, 12);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(128, 48);
            this.openFileButton.TabIndex = 0;
            this.openFileButton.TabStop = false;
            this.openFileButton.Text = "Drop/Open file";
            this.openFileButton.UseVisualStyleBackColor = true;
            this.openFileButton.Click += new System.EventHandler(this.click_open);
            this.openFileButton.DragDrop += new System.Windows.Forms.DragEventHandler(this.drop_open);
            this.openFileButton.DragEnter += new System.Windows.Forms.DragEventHandler(this.drop_enter);
            this.openFileButton.DragLeave += new System.EventHandler(this.drop_leave);
            // 
            // width
            // 
            this.width.AutoSize = true;
            this.width.Checked = true;
            this.width.Enabled = false;
            this.width.Location = new System.Drawing.Point(12, 66);
            this.width.Name = "width";
            this.width.Size = new System.Drawing.Size(62, 20);
            this.width.TabIndex = 1;
            this.width.TabStop = true;
            this.width.Text = "Width";
            this.width.UseVisualStyleBackColor = true;
            this.width.CheckedChanged += new System.EventHandler(this.size_axis_change);
            // 
            // height
            // 
            this.height.AutoSize = true;
            this.height.Enabled = false;
            this.height.Location = new System.Drawing.Point(12, 92);
            this.height.Name = "height";
            this.height.Size = new System.Drawing.Size(67, 20);
            this.height.TabIndex = 2;
            this.height.Text = "Height";
            this.height.UseVisualStyleBackColor = true;
            // 
            // inputSize
            // 
            this.inputSize.Enabled = false;
            this.inputSize.Location = new System.Drawing.Point(12, 118);
            this.inputSize.MaxLength = 6;
            this.inputSize.Name = "inputSize";
            this.inputSize.Size = new System.Drawing.Size(42, 22);
            this.inputSize.TabIndex = 3;
            this.inputSize.TabStop = false;
            this.inputSize.Text = "1";
            this.inputSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pbImages
            // 
            this.pbImages.Location = new System.Drawing.Point(146, 12);
            this.pbImages.Name = "pbImages";
            this.pbImages.Size = new System.Drawing.Size(336, 32);
            this.pbImages.TabIndex = 4;
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(418, 268);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(64, 16);
            this.linkLabel1.TabIndex = 5;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Donate :3";
            this.linkLabel1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.open_donate);
            // 
            // runButton
            // 
            this.runButton.Enabled = false;
            this.runButton.Location = new System.Drawing.Point(12, 230);
            this.runButton.Name = "runButton";
            this.runButton.Size = new System.Drawing.Size(128, 48);
            this.runButton.TabIndex = 6;
            this.runButton.TabStop = false;
            this.runButton.Text = "Run";
            this.runButton.UseVisualStyleBackColor = true;
            this.runButton.Click += new System.EventHandler(this.click_run);
            // 
            // imagesState
            // 
            this.imagesState.AutoSize = true;
            this.imagesState.BackColor = System.Drawing.SystemColors.Control;
            this.imagesState.Location = new System.Drawing.Point(146, 47);
            this.imagesState.Name = "imagesState";
            this.imagesState.Size = new System.Drawing.Size(158, 16);
            this.imagesState.TabIndex = 7;
            this.imagesState.Text = "Images done: DONE/ALL";
            // 
            // currentImageState
            // 
            this.currentImageState.AutoSize = true;
            this.currentImageState.BackColor = System.Drawing.SystemColors.Control;
            this.currentImageState.Location = new System.Drawing.Point(146, 115);
            this.currentImageState.Name = "currentImageState";
            this.currentImageState.Size = new System.Drawing.Size(195, 16);
            this.currentImageState.TabIndex = 8;
            this.currentImageState.Text = "Current image: DONE_OBJ/ALL";
            // 
            // pbCurrentImage
            // 
            this.pbCurrentImage.Location = new System.Drawing.Point(146, 80);
            this.pbCurrentImage.Name = "pbCurrentImage";
            this.pbCurrentImage.Size = new System.Drawing.Size(336, 32);
            this.pbCurrentImage.TabIndex = 9;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(60, 121);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(33, 16);
            this.label1.TabIndex = 10;
            this.label1.Text = "Size";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(75, 16);
            this.label2.TabIndex = 12;
            this.label2.Text = "Border size";
            // 
            // borderSizeInput
            // 
            this.borderSizeInput.Enabled = false;
            this.borderSizeInput.Location = new System.Drawing.Point(12, 146);
            this.borderSizeInput.MaxLength = 6;
            this.borderSizeInput.Name = "borderSizeInput";
            this.borderSizeInput.Size = new System.Drawing.Size(42, 22);
            this.borderSizeInput.TabIndex = 11;
            this.borderSizeInput.TabStop = false;
            this.borderSizeInput.Text = "1";
            this.borderSizeInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(60, 177);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(79, 16);
            this.label3.TabIndex = 14;
            this.label3.Text = "Max objects";
            // 
            // maxObjectsInput
            // 
            this.maxObjectsInput.Enabled = false;
            this.maxObjectsInput.Location = new System.Drawing.Point(12, 174);
            this.maxObjectsInput.MaxLength = 6;
            this.maxObjectsInput.Name = "maxObjectsInput";
            this.maxObjectsInput.Size = new System.Drawing.Size(42, 22);
            this.maxObjectsInput.TabIndex = 13;
            this.maxObjectsInput.TabStop = false;
            this.maxObjectsInput.Text = "16000";
            this.maxObjectsInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(60, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(58, 16);
            this.label4.TabIndex = 16;
            this.label4.Text = "EL offset";
            // 
            // editorLayerOffsetInput
            // 
            this.editorLayerOffsetInput.Enabled = false;
            this.editorLayerOffsetInput.Location = new System.Drawing.Point(12, 202);
            this.editorLayerOffsetInput.MaxLength = 6;
            this.editorLayerOffsetInput.Name = "editorLayerOffsetInput";
            this.editorLayerOffsetInput.Size = new System.Drawing.Size(42, 22);
            this.editorLayerOffsetInput.TabIndex = 15;
            this.editorLayerOffsetInput.TabStop = false;
            this.editorLayerOffsetInput.Text = "0";
            this.editorLayerOffsetInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // App
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(494, 293);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.editorLayerOffsetInput);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.maxObjectsInput);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.borderSizeInput);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.currentImageState);
            this.Controls.Add(this.pbCurrentImage);
            this.Controls.Add(this.imagesState);
            this.Controls.Add(this.runButton);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.pbImages);
            this.Controls.Add(this.inputSize);
            this.Controls.Add(this.height);
            this.Controls.Add(this.width);
            this.Controls.Add(this.openFileButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "App";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "G2GD (none ver)";
            this.TransparencyKey = System.Drawing.Color.Fuchsia;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.RadioButton width;
        private System.Windows.Forms.RadioButton height;
        private System.Windows.Forms.TextBox inputSize;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Button runButton;
        private System.Windows.Forms.Label imagesState;
        private System.Windows.Forms.Label currentImageState;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox borderSizeInput;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox maxObjectsInput;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox editorLayerOffsetInput;
        public System.Windows.Forms.ProgressBar pbImages;
        public System.Windows.Forms.ProgressBar pbCurrentImage;
    }
}

