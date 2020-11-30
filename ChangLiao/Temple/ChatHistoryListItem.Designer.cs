namespace ChangLiao.Temple
{
    partial class ChatHistoryListItem
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.headBtn = new DSkin.DirectUI.DuiButton();
            this.timePanl = new DSkin.DirectUI.DuiBaseControl();
            this.timeLabel = new DSkin.DirectUI.DuiLabel();
            this.nameLabel = new DSkin.DirectUI.DuiLabel();
            this.BubbleControl = new DSkin.DirectUI.DuiBaseControl();
            this.filePanl = new DSkin.DirectUI.DuiBaseControl();
            this.textLabel = new DSkin.DirectUI.DuiLabel();
            this.durentLabel = new DSkin.DirectUI.DuiLabel();
            this.imagePictureBox = new DSkin.DirectUI.DuiPictureBox();
            this.voicePictureBox = new DSkin.DirectUI.DuiPictureBox();
            this.fileLine = new DSkin.DirectUI.DuiBaseControl();
            this.idCardLabel = new DSkin.DirectUI.DuiLabel();
            this.textMessageLabel = new ChangLiao.ChildView.CLDIYLabel();
            this.loaddingPicture = new DSkin.DirectUI.DuiPictureBox();
            this.uploadPrograss = new ChangLiao.ChildView.GDProgressBar();
            // 
            // headBtn
            // 
            this.headBtn.BackgroundImage = global::ChangLiao.Properties.Resources.moren;
            this.headBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.headBtn.BackgroundRender.Radius = 10;
            this.headBtn.BaseColor = System.Drawing.Color.Transparent;
            this.headBtn.ButtonBorderWidth = 0;
            this.headBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.headBtn.HoverColor = System.Drawing.Color.Empty;
            this.headBtn.ImageSize = new System.Drawing.Size(40, 40);
            this.headBtn.Location = new System.Drawing.Point(0, 0);
            this.headBtn.Name = "headBtn";
            this.headBtn.PressColor = System.Drawing.Color.Empty;
            this.headBtn.Size = new System.Drawing.Size(40, 40);
            this.headBtn.SudokuDrawBackImage = true;
            this.headBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.headBtn.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.headBtn_MouseClick);
            // 
            // timePanl
            // 
            this.timePanl.AutoSize = false;
            this.timePanl.Controls.Add(this.timeLabel);
            this.timePanl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timePanl.Location = new System.Drawing.Point(0, 0);
            this.timePanl.Name = "timePanl";
            this.timePanl.Size = new System.Drawing.Size(100, 20);
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.DesignModeCanResize = false;
            this.timeLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timeLabel.Location = new System.Drawing.Point(0, 0);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(72, 16);
            this.timeLabel.Text = "Dui设计模式";
            this.timeLabel.TextRenderMode = System.Drawing.Text.TextRenderingHint.SingleBitPerPixelGridFit;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.DesignModeCanResize = false;
            this.nameLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameLabel.Location = new System.Drawing.Point(0, 0);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(72, 16);
            this.nameLabel.Text = "Dui设计模式";
            // 
            // BubbleControl
            // 
            this.BubbleControl.AutoSize = false;
            this.BubbleControl.BackgroundRender.BorderWidth = 0;
            this.BubbleControl.BackgroundRender.RenderBorders = true;
            this.BubbleControl.Controls.Add(this.filePanl);
            this.BubbleControl.Controls.Add(this.textLabel);
            this.BubbleControl.Controls.Add(this.durentLabel);
            this.BubbleControl.Controls.Add(this.imagePictureBox);
            this.BubbleControl.Controls.Add(this.voicePictureBox);
            this.BubbleControl.Controls.Add(this.fileLine);
            this.BubbleControl.Controls.Add(this.idCardLabel);
            this.BubbleControl.Controls.Add(this.textMessageLabel);
            this.BubbleControl.Controls.Add(this.uploadPrograss);
            this.BubbleControl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.BubbleControl.Location = new System.Drawing.Point(0, 0);
            this.BubbleControl.Name = "BubbleControl";
            this.BubbleControl.Size = new System.Drawing.Size(100, 100);
            this.BubbleControl.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.BubbleControl_MouseClick);
            // 
            // filePanl
            // 
            this.filePanl.AutoSize = false;
            this.filePanl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.filePanl.Location = new System.Drawing.Point(0, 0);
            this.filePanl.Name = "filePanl";
            this.filePanl.Size = new System.Drawing.Size(100, 100);
            // 
            // textLabel
            // 
            this.textLabel.AutoSize = true;
            this.textLabel.DesignModeCanResize = false;
            this.textLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textLabel.Location = new System.Drawing.Point(0, 0);
            this.textLabel.Name = "textLabel";
            this.textLabel.Size = new System.Drawing.Size(74, 18);
            this.textLabel.Text = "Dui设计模式";
            // 
            // durentLabel
            // 
            this.durentLabel.AutoSize = true;
            this.durentLabel.DesignModeCanResize = false;
            this.durentLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.durentLabel.Location = new System.Drawing.Point(0, 0);
            this.durentLabel.Name = "durentLabel";
            this.durentLabel.Size = new System.Drawing.Size(72, 16);
            this.durentLabel.Text = "Dui设计模式";
            // 
            // imagePictureBox
            // 
            this.imagePictureBox.AutoSize = false;
            this.imagePictureBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.imagePictureBox.Images = null;
            this.imagePictureBox.Location = new System.Drawing.Point(0, 0);
            this.imagePictureBox.Name = "imagePictureBox";
            this.imagePictureBox.Size = new System.Drawing.Size(100, 100);
            this.imagePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // voicePictureBox
            // 
            this.voicePictureBox.AutoSize = false;
            this.voicePictureBox.BackgroundRender.BorderWidth = 0;
            this.voicePictureBox.BackgroundRender.RenderBorders = true;
            this.voicePictureBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.voicePictureBox.Images = null;
            this.voicePictureBox.Location = new System.Drawing.Point(0, 0);
            this.voicePictureBox.Name = "voicePictureBox";
            this.voicePictureBox.Size = new System.Drawing.Size(100, 100);
            this.voicePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // fileLine
            // 
            this.fileLine.AutoSize = false;
            this.fileLine.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fileLine.Location = new System.Drawing.Point(0, 0);
            this.fileLine.Name = "fileLine";
            this.fileLine.Size = new System.Drawing.Size(100, 100);
            // 
            // idCardLabel
            // 
            this.idCardLabel.AutoSize = true;
            this.idCardLabel.DesignModeCanResize = false;
            this.idCardLabel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.idCardLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(62)))), ((int)(((byte)(74)))), ((int)(((byte)(146)))));
            this.idCardLabel.Location = new System.Drawing.Point(0, 0);
            this.idCardLabel.Name = "idCardLabel";
            this.idCardLabel.Size = new System.Drawing.Size(117, 28);
            this.idCardLabel.Text = "Dui设计模式";
            // 
            // textMessageLabel
            // 
            this.textMessageLabel.AutoSize = true;
            this.textMessageLabel.ChatText = null;
            this.textMessageLabel.DesignModeCanResize = false;
            this.textMessageLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textMessageLabel.Location = new System.Drawing.Point(0, 0);
            this.textMessageLabel.Name = "textMessageLabel";
            this.textMessageLabel.selectColor = System.Drawing.Color.AliceBlue;
            this.textMessageLabel.Size = new System.Drawing.Size(72, 16);
            // 
            // loaddingPicture
            // 
            this.loaddingPicture.AutoSize = false;
            this.loaddingPicture.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.loaddingPicture.Images = null;
            this.loaddingPicture.Location = new System.Drawing.Point(0, 0);
            this.loaddingPicture.Name = "loaddingPicture";
            this.loaddingPicture.Size = new System.Drawing.Size(100, 100);
            this.loaddingPicture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Normal;
            // 
            // uploadPrograss
            // 
            this.uploadPrograss.AutoSize = false;
            this.uploadPrograss.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.uploadPrograss.Location = new System.Drawing.Point(0, 0);
            this.uploadPrograss.Name = "uploadPrograss";
            this.uploadPrograss.Size = new System.Drawing.Size(100, 100);
            // 
            // ChatHistoryListItem
            // 
            this.BackgroundRender.Radius = 10;
            this.Controls.Add(this.headBtn);
            this.Controls.Add(this.timePanl);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.BubbleControl);
            this.Controls.Add(this.loaddingPicture);
            this.Size = new System.Drawing.Size(430, 15);
            this.SizeChanged += new System.EventHandler(this.ChatHistoryListItem_SizeChanged);
            this.ParentChanged += new System.EventHandler(this.ChatHistoryListItem_ParentChanged);

        }

        #endregion

        private DSkin.DirectUI.DuiButton headBtn;
        private DSkin.DirectUI.DuiBaseControl timePanl;
        private DSkin.DirectUI.DuiLabel timeLabel;
        private DSkin.DirectUI.DuiLabel nameLabel;
        private DSkin.DirectUI.DuiBaseControl BubbleControl;
        private DSkin.DirectUI.DuiLabel textLabel;
        private DSkin.DirectUI.DuiLabel durentLabel;
        private DSkin.DirectUI.DuiPictureBox imagePictureBox;
        private DSkin.DirectUI.DuiPictureBox voicePictureBox;
        private DSkin.DirectUI.DuiBaseControl filePanl;
        private DSkin.DirectUI.DuiBaseControl fileLine;
        private DSkin.DirectUI.DuiLabel idCardLabel;
        private ChangLiao.ChildView.CLDIYLabel textMessageLabel;
        private ChangLiao.ChildView.GDProgressBar uploadPrograss;
        private DSkin.DirectUI.DuiPictureBox loaddingPicture;

        public event System.EventHandler messageClick;
        public event System.EventHandler headClick;
        public event System.EventHandler messageRightClick;
        public event System.EventHandler headRightClick;
    }
}
