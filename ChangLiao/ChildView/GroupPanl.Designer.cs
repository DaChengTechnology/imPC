namespace ChangLiao.ChildView
{
    partial class GroupPanl
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
            this.avatarPictureBox = new DSkin.DirectUI.DuiPictureBox();
            this.nameLabel = new DSkin.DirectUI.DuiLabel();
            this.sendMessageButton = new DSkin.DirectUI.DuiButton();
            this.SuspendLayout();
            // 
            // avatarPictureBox
            // 
            this.avatarPictureBox.AutoSize = false;
            this.avatarPictureBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.avatarPictureBox.Image = global::ChangLiao.Properties.Resources.moren;
            this.avatarPictureBox.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ChangLiao.Properties.Resources.moren))};
            this.avatarPictureBox.Location = new System.Drawing.Point(210, 60);
            this.avatarPictureBox.Name = "avatarPictureBox";
            this.avatarPictureBox.Size = new System.Drawing.Size(100, 100);
            this.avatarPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.DesignModeCanResize = false;
            this.nameLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameLabel.Location = new System.Drawing.Point(210, 170);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(99, 24);
            this.nameLabel.Text = "Dui设计模式";
            // 
            // sendMessageButton
            // 
            this.sendMessageButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(26)))));
            this.sendMessageButton.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendMessageButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(72)))), ((int)(((byte)(151)))));
            this.sendMessageButton.HoverColor = System.Drawing.Color.Empty;
            this.sendMessageButton.Location = new System.Drawing.Point(190, 230);
            this.sendMessageButton.Name = "sendMessageButton";
            this.sendMessageButton.PressColor = System.Drawing.Color.Empty;
            this.sendMessageButton.Size = new System.Drawing.Size(140, 50);
            this.sendMessageButton.Text = "发送消息";
            this.sendMessageButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.sendMessageButton.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.sendMessageButton_MouseClick);
            // 
            // GroupPanl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.DUIControls.Add(this.avatarPictureBox);
            this.DUIControls.Add(this.nameLabel);
            this.DUIControls.Add(this.sendMessageButton);
            this.Name = "GroupPanl";
            this.Size = new System.Drawing.Size(520, 351);
            this.Load += new System.EventHandler(this.GroupPanl_Load);
            this.SizeChanged += new System.EventHandler(this.GroupPanl_SizeChanged);
            this.Layout += new System.Windows.Forms.LayoutEventHandler(this.GroupPanl_Layout);
            this.ResumeLayout(false);

        }

        #endregion

        private DSkin.DirectUI.DuiPictureBox avatarPictureBox;
        private DSkin.DirectUI.DuiLabel nameLabel;
        private DSkin.DirectUI.DuiButton sendMessageButton;
    }
}
