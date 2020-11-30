namespace ChangLiao.windows
{
    partial class PicturePreviewForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.controlHost1 = new DSkin.Controls.ControlHost();
            this.dSkinNewPanel1 = new DSkin.Controls.DSkinNewPanel();
            this.dSkinButton1 = new DSkin.Controls.DSkinButton();
            this.duiBaseControl1 = new DSkin.DirectUI.DuiBaseControl();
            this.vlcControl1 = new Vlc.DotNet.Forms.VlcControl();
            this.dSkinPictureBox1 = new DSkin.Controls.DSkinPictureBox();
            this.leftPictureBox = new DSkin.Controls.DSkinPictureBox();
            this.rightPictureBox2 = new DSkin.Controls.DSkinPictureBox();
            this.controlHost1.SuspendLayout();
            this.dSkinNewPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // controlHost1
            // 
            this.controlHost1.Controls.Add(this.dSkinNewPanel1);
            this.controlHost1.Controls.Add(this.vlcControl1);
            this.controlHost1.Location = new System.Drawing.Point(237, 24);
            this.controlHost1.Name = "controlHost1";
            this.controlHost1.Size = new System.Drawing.Size(384, 419);
            this.controlHost1.TabIndex = 0;
            this.controlHost1.Text = "controlHost1";
            this.controlHost1.TransparencyKey = System.Drawing.Color.Black;
            // 
            // dSkinNewPanel1
            // 
            this.dSkinNewPanel1.BackColor = System.Drawing.Color.Transparent;
            this.dSkinNewPanel1.Controls.Add(this.dSkinButton1);
            this.dSkinNewPanel1.DUIControls.Add(this.duiBaseControl1);
            this.dSkinNewPanel1.Location = new System.Drawing.Point(88, 339);
            this.dSkinNewPanel1.Name = "dSkinNewPanel1";
            this.dSkinNewPanel1.Size = new System.Drawing.Size(200, 50);
            this.dSkinNewPanel1.TabIndex = 1;
            this.dSkinNewPanel1.Text = "dSkinNewPanel1";
            // 
            // dSkinButton1
            // 
            this.dSkinButton1.BaseColor = System.Drawing.Color.Transparent;
            this.dSkinButton1.ButtonBorderWidth = 0;
            this.dSkinButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dSkinButton1.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton1.HoverImage = null;
            this.dSkinButton1.Image = global::ChangLiao.Properties.Resources.play_normal;
            this.dSkinButton1.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton1.ImageSize = new System.Drawing.Size(40, 40);
            this.dSkinButton1.Location = new System.Drawing.Point(75, 3);
            this.dSkinButton1.Name = "dSkinButton1";
            this.dSkinButton1.NormalImage = null;
            this.dSkinButton1.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton1.PressedImage = null;
            this.dSkinButton1.Radius = 10;
            this.dSkinButton1.ShowButtonBorder = true;
            this.dSkinButton1.Size = new System.Drawing.Size(40, 40);
            this.dSkinButton1.TabIndex = 3;
            this.dSkinButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton1.TextPadding = 0;
            this.dSkinButton1.Click += new System.EventHandler(this.dSkinButton1_Click);
            this.dSkinButton1.MouseEnter += new System.EventHandler(this.dSkinButton1_MouseEnter);
            this.dSkinButton1.MouseLeave += new System.EventHandler(this.dSkinButton1_MouseLeave);
            // 
            // duiBaseControl1
            // 
            this.duiBaseControl1.AutoSize = false;
            this.duiBaseControl1.BackColor = System.Drawing.Color.Black;
            this.duiBaseControl1.BackgroundRender.Radius = 49;
            this.duiBaseControl1.BackgroundRender.RenderBorders = true;
            this.duiBaseControl1.DesignModeCanMove = false;
            this.duiBaseControl1.DesignModeCanResize = false;
            this.duiBaseControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duiBaseControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiBaseControl1.Location = new System.Drawing.Point(0, 0);
            this.duiBaseControl1.Name = "duiBaseControl1";
            this.duiBaseControl1.Size = new System.Drawing.Size(200, 50);
            // 
            // vlcControl1
            // 
            this.vlcControl1.BackColor = System.Drawing.Color.Black;
            this.vlcControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.vlcControl1.Location = new System.Drawing.Point(0, 0);
            this.vlcControl1.Name = "vlcControl1";
            this.vlcControl1.Size = new System.Drawing.Size(384, 419);
            this.vlcControl1.Spu = -1;
            this.vlcControl1.TabIndex = 0;
            this.vlcControl1.Text = "vlcControl1";
            this.vlcControl1.VlcLibDirectory = null;
            this.vlcControl1.VlcMediaplayerOptions = null;
            this.vlcControl1.VlcLibDirectoryNeeded += new System.EventHandler<Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs>(this.vlcControl1_VlcLibDirectoryNeeded);
            this.vlcControl1.Paused += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPausedEventArgs>(this.vlcControl1_Paused);
            this.vlcControl1.Playing += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerPlayingEventArgs>(this.vlcControl1_Playing);
            this.vlcControl1.TimeChanged += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerTimeChangedEventArgs>(this.vlcControl1_TimeChanged);
            this.vlcControl1.Stopped += new System.EventHandler<Vlc.DotNet.Core.VlcMediaPlayerStoppedEventArgs>(this.vlcControl1_Stopped);
            // 
            // dSkinPictureBox1
            // 
            this.dSkinPictureBox1.Image = null;
            this.dSkinPictureBox1.Images = null;
            this.dSkinPictureBox1.Location = new System.Drawing.Point(200, 27);
            this.dSkinPictureBox1.Name = "dSkinPictureBox1";
            this.dSkinPictureBox1.Size = new System.Drawing.Size(100, 100);
            this.dSkinPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.dSkinPictureBox1.TabIndex = 1;
            this.dSkinPictureBox1.Text = "dSkinPictureBox1";
            // 
            // leftPictureBox
            // 
            this.leftPictureBox.Image = global::ChangLiao.Properties.Resources.left_normal;
            this.leftPictureBox.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ChangLiao.Properties.Resources.left_normal))};
            this.leftPictureBox.Location = new System.Drawing.Point(0, 0);
            this.leftPictureBox.Name = "leftPictureBox";
            this.leftPictureBox.Size = new System.Drawing.Size(75, 440);
            this.leftPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.leftPictureBox.TabIndex = 2;
            this.leftPictureBox.Text = "dSkinPictureBox2";
            this.leftPictureBox.Click += new System.EventHandler(this.leftPictureBox_Click);
            this.leftPictureBox.MouseEnter += new System.EventHandler(this.leftPictureBox_MouseEnter);
            this.leftPictureBox.MouseLeave += new System.EventHandler(this.leftPictureBox_MouseLeave);
            // 
            // rightPictureBox2
            // 
            this.rightPictureBox2.Image = global::ChangLiao.Properties.Resources.right_normal;
            this.rightPictureBox2.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ChangLiao.Properties.Resources.right_normal))};
            this.rightPictureBox2.Location = new System.Drawing.Point(716, 207);
            this.rightPictureBox2.Name = "rightPictureBox2";
            this.rightPictureBox2.Size = new System.Drawing.Size(75, 100);
            this.rightPictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.rightPictureBox2.TabIndex = 3;
            this.rightPictureBox2.Text = "dSkinPictureBox2";
            this.rightPictureBox2.Click += new System.EventHandler(this.rightPictureBox2_Click);
            this.rightPictureBox2.DragEnter += new System.Windows.Forms.DragEventHandler(this.rightPictureBox2_DragEnter);
            this.rightPictureBox2.DragLeave += new System.EventHandler(this.rightPictureBox2_DragLeave);
            this.rightPictureBox2.MouseEnter += new System.EventHandler(this.rightPictureBox2_MouseEnter);
            this.rightPictureBox2.MouseLeave += new System.EventHandler(this.rightPictureBox2_MouseLeave);
            // 
            // PicturePreviewForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.CloseBox.HoverImage = global::ChangLiao.Properties.Resources.image_close_select;
            this.CloseBox.NormalImage = global::ChangLiao.Properties.Resources.image_close;
            this.CloseBox.PressImage = global::ChangLiao.Properties.Resources.image_close_select;
            this.CloseBox.Size = new System.Drawing.Size(60, 60);
            this.CloseBox.ToolTip = "关闭";
            this.Controls.Add(this.rightPictureBox2);
            this.Controls.Add(this.leftPictureBox);
            this.Controls.Add(this.dSkinPictureBox1);
            this.Controls.Add(this.controlHost1);
            this.DrawIcon = false;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MoveMode = DSkin.Forms.MoveModes.None;
            this.Name = "PicturePreviewForm";
            this.ShowIcon = false;
            this.Text = "";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.controlHost1.ResumeLayout(false);
            this.dSkinNewPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.vlcControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DSkin.Controls.ControlHost controlHost1;
        private Vlc.DotNet.Forms.VlcControl vlcControl1;
        private DSkin.Controls.DSkinPictureBox dSkinPictureBox1;
        private DSkin.Controls.DSkinPictureBox leftPictureBox;
        private DSkin.Controls.DSkinPictureBox rightPictureBox2;
        private DSkin.Controls.DSkinNewPanel dSkinNewPanel1;
        private DSkin.Controls.DSkinButton dSkinButton1;
        private DSkin.DirectUI.DuiBaseControl duiBaseControl1;
    }
}