namespace ChangLiao.Temple
{
    partial class ConversationItem
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
            this.avatarImageBtn = new DSkin.DirectUI.DuiButton();
            this.nameLabel = new DSkin.DirectUI.DuiLabel();
            this.timeLabel = new DSkin.DirectUI.DuiLabel();
            this.showMessageLabel = new DSkin.DirectUI.DuiLabel();
            this.duiButton1 = new DSkin.DirectUI.DuiButton();
            // 
            // avatarImageBtn
            // 
            this.avatarImageBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.avatarImageBtn.BaseColor = System.Drawing.Color.Transparent;
            this.avatarImageBtn.BitmapCache = true;
            this.avatarImageBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.avatarImageBtn.HoverColor = System.Drawing.Color.Empty;
            this.avatarImageBtn.Image = global::ChangLiao.Properties.Resources.moren;
            this.avatarImageBtn.ImageSize = new System.Drawing.Size(50, 50);
            this.avatarImageBtn.IsDrawText = false;
            this.avatarImageBtn.Location = new System.Drawing.Point(5, 10);
            this.avatarImageBtn.Name = "avatarImageBtn";
            this.avatarImageBtn.PressColor = System.Drawing.Color.Empty;
            this.avatarImageBtn.Size = new System.Drawing.Size(40, 40);
            this.avatarImageBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // nameLabel
            // 
            this.nameLabel.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameLabel.Location = new System.Drawing.Point(50, 12);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(29, 18);
            this.nameLabel.Text = "名称";
            // 
            // timeLabel
            // 
            this.timeLabel.AutoSize = true;
            this.timeLabel.DesignModeCanResize = false;
            this.timeLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.timeLabel.Location = new System.Drawing.Point(146, 12);
            this.timeLabel.Name = "timeLabel";
            this.timeLabel.Size = new System.Drawing.Size(29, 16);
            this.timeLabel.Text = "昨天";
            // 
            // showMessageLabel
            // 
            this.showMessageLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.showMessageLabel.ForeColor = System.Drawing.Color.DimGray;
            this.showMessageLabel.Location = new System.Drawing.Point(49, 37);
            this.showMessageLabel.Name = "showMessageLabel";
            this.showMessageLabel.Size = new System.Drawing.Size(120, 16);
            this.showMessageLabel.Text = "消息";
            // 
            // duiButton1
            // 
            this.duiButton1.BaseColor = System.Drawing.Color.Red;
            this.duiButton1.ButtonBorderWidth = 0;
            this.duiButton1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiButton1.ForeColor = System.Drawing.Color.White;
            this.duiButton1.HoverColor = System.Drawing.Color.Empty;
            this.duiButton1.Location = new System.Drawing.Point(30, 5);
            this.duiButton1.Name = "duiButton1";
            this.duiButton1.PressColor = System.Drawing.Color.Empty;
            this.duiButton1.Radius = 19;
            this.duiButton1.Size = new System.Drawing.Size(20, 20);
            this.duiButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ConversationItem
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.avatarImageBtn);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.timeLabel);
            this.Controls.Add(this.showMessageLabel);
            this.Controls.Add(this.duiButton1);
            this.Size = new System.Drawing.Size(180, 60);
            this.IsSelectedChanged += new System.EventHandler(this.ConversationItem_IsSelectedChanged);
            this.MouseEnter += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.ConversationItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ConversationItem_MouseLeave);
            this.SizeChanged += new System.EventHandler(this.ConversationItem_SizeChanged);

        }

        #endregion

        private DSkin.DirectUI.DuiButton avatarImageBtn;
        private DSkin.DirectUI.DuiLabel nameLabel;
        private DSkin.DirectUI.DuiLabel timeLabel;
        private DSkin.DirectUI.DuiLabel showMessageLabel;
        private DSkin.DirectUI.DuiButton duiButton1;
    }
}
