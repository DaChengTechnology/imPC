namespace ChangLiao.ChildView
{
    partial class ChatPanl
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
            this.components = new System.ComponentModel.Container();
            this.chathistoryListBox = new DSkin.DirectUI.DuiListBox();
            this.chatToolbar = new DSkin.DirectUI.DuiBaseControl();
            this.faceButton = new DSkin.DirectUI.DuiButton();
            this.fileButton = new DSkin.DirectUI.DuiButton();
            this.sendButton = new DSkin.DirectUI.DuiButton();
            this.messageQueueTimer = new System.Windows.Forms.Timer(this.components);
            this.chatContextMenuStrip = new DSkin.Controls.DSkinContextMenuStrip();
            this.删除消息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.headContextMenuStrip = new DSkin.Controls.DSkinContextMenuStrip();
            this.禁言ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.踢人ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.解除禁言ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.复制ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.chatTextBox = new ChangLiao.ChildView.ChatTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.chathistoryListBox)).BeginInit();
            this.chatContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // chathistoryListBox
            // 
            this.chathistoryListBox.AutoSize = false;
            this.chathistoryListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.chathistoryListBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chathistoryListBox.ItemSize = new System.Drawing.Size(100, 100);
            this.chathistoryListBox.Location = new System.Drawing.Point(0, 0);
            this.chathistoryListBox.Name = "chathistoryListBox";
            this.chathistoryListBox.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.chathistoryListBox.RollSize = 20;
            this.chathistoryListBox.ScrollBarWidth = 12;
            this.chathistoryListBox.ShowScrollBar = true;
            this.chathistoryListBox.Size = new System.Drawing.Size(446, 179);
            this.chathistoryListBox.SmoothScroll = false;
            this.chathistoryListBox.Ulmul = false;
            this.chathistoryListBox.ValueChanged += new System.EventHandler(this.chathistoryListBox_ValueChanged);
            this.chathistoryListBox.LayoutContented += new System.EventHandler(this.chathistoryListBox_LayoutContented_1);
            this.chathistoryListBox.ItemAdded += new System.EventHandler<DSkin.DirectUI.DuiControlEventArgs>(this.chathistoryListBox_ItemAdded);
            this.chathistoryListBox.ItemRemoved += new System.EventHandler<DSkin.DirectUI.DuiControlEventArgs>(this.chathistoryListBox_ItemRemoved);
            this.chathistoryListBox.MouseEnter += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.chathistoryListBox_MouseEnter);
            this.chathistoryListBox.MouseLeave += new System.EventHandler(this.chathistoryListBox_MouseLeave);
            this.chathistoryListBox.SizeChanged += new System.EventHandler(this.chathistoryListBox_SizeChanged);
            this.chathistoryListBox.MouseWheel += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.chathistoryListBox_MouseWheel);
            // 
            // chatToolbar
            // 
            this.chatToolbar.AutoSize = false;
            this.chatToolbar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.chatToolbar.Controls.Add(this.faceButton);
            this.chatToolbar.Controls.Add(this.fileButton);
            this.chatToolbar.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chatToolbar.Location = new System.Drawing.Point(0, 179);
            this.chatToolbar.Name = "chatToolbar";
            this.chatToolbar.Size = new System.Drawing.Size(446, 25);
            // 
            // faceButton
            // 
            this.faceButton.BackgroundImage = global::ChangLiao.Properties.Resources.face_normal;
            this.faceButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.faceButton.BaseColor = System.Drawing.Color.Transparent;
            this.faceButton.ButtonBorderWidth = 0;
            this.faceButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.faceButton.HoverColor = System.Drawing.Color.Empty;
            this.faceButton.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.faceButton.ImageSize = new System.Drawing.Size(23, 23);
            this.faceButton.Location = new System.Drawing.Point(1, 1);
            this.faceButton.Name = "faceButton";
            this.faceButton.PressColor = System.Drawing.Color.Empty;
            this.faceButton.Size = new System.Drawing.Size(23, 23);
            this.faceButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.faceButton.MouseEnter += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.faceButton_MouseEnter);
            this.faceButton.MouseLeave += new System.EventHandler(this.faceButton_MouseLeave);
            this.faceButton.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.faceButton_MouseClick);
            // 
            // fileButton
            // 
            this.fileButton.BackgroundImage = global::ChangLiao.Properties.Resources.file_normal;
            this.fileButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.fileButton.BaseColor = System.Drawing.Color.Transparent;
            this.fileButton.ButtonBorderColor = System.Drawing.Color.Transparent;
            this.fileButton.ButtonBorderWidth = 0;
            this.fileButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fileButton.HoverColor = System.Drawing.Color.Empty;
            this.fileButton.Location = new System.Drawing.Point(26, 1);
            this.fileButton.Name = "fileButton";
            this.fileButton.PressColor = System.Drawing.Color.Empty;
            this.fileButton.Size = new System.Drawing.Size(23, 23);
            this.fileButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.fileButton.MouseEnter += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.fileButton_MouseEnter);
            this.fileButton.MouseLeave += new System.EventHandler(this.fileButton_MouseLeave);
            this.fileButton.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.fileButton_MouseClick);
            // 
            // sendButton
            // 
            this.sendButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendButton.HoverColor = System.Drawing.Color.Empty;
            this.sendButton.Location = new System.Drawing.Point(346, 274);
            this.sendButton.Name = "sendButton";
            this.sendButton.PressColor = System.Drawing.Color.Empty;
            this.sendButton.Size = new System.Drawing.Size(100, 30);
            this.sendButton.Text = "发送 (S)";
            this.sendButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.sendButton.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.sendButton_MouseClick);
            // 
            // messageQueueTimer
            // 
            this.messageQueueTimer.Enabled = true;
            this.messageQueueTimer.Interval = 1;
            this.messageQueueTimer.Tick += new System.EventHandler(this.messageQueueTimer_Tick);
            // 
            // chatContextMenuStrip
            // 
            this.chatContextMenuStrip.Arrow = System.Drawing.Color.Black;
            this.chatContextMenuStrip.Back = System.Drawing.Color.White;
            this.chatContextMenuStrip.BackRadius = 4;
            this.chatContextMenuStrip.Base = System.Drawing.Color.Transparent;
            this.chatContextMenuStrip.CheckedImage = null;
            this.chatContextMenuStrip.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.chatContextMenuStrip.Fore = System.Drawing.Color.Black;
            this.chatContextMenuStrip.HoverFore = System.Drawing.Color.White;
            this.chatContextMenuStrip.ItemAnamorphosis = true;
            this.chatContextMenuStrip.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.chatContextMenuStrip.ItemBorderShow = false;
            this.chatContextMenuStrip.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.chatContextMenuStrip.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.chatContextMenuStrip.ItemRadius = 4;
            this.chatContextMenuStrip.ItemRadiusStyle = DSkin.Common.RoundStyle.All;
            this.chatContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除消息ToolStripMenuItem});
            this.chatContextMenuStrip.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.chatContextMenuStrip.Name = "chatContextMenuStrip";
            this.chatContextMenuStrip.RadiusStyle = DSkin.Common.RoundStyle.All;
            this.chatContextMenuStrip.Size = new System.Drawing.Size(125, 26);
            this.chatContextMenuStrip.SkinAllColor = true;
            this.chatContextMenuStrip.TitleAnamorphosis = true;
            this.chatContextMenuStrip.TitleColor = System.Drawing.Color.Transparent;
            this.chatContextMenuStrip.TitleRadius = 4;
            this.chatContextMenuStrip.TitleRadiusStyle = DSkin.Common.RoundStyle.All;
            this.chatContextMenuStrip.Closing += new System.Windows.Forms.ToolStripDropDownClosingEventHandler(this.chatContextMenuStrip_Closing);
            // 
            // 删除消息ToolStripMenuItem
            // 
            this.删除消息ToolStripMenuItem.Name = "删除消息ToolStripMenuItem";
            this.删除消息ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除消息ToolStripMenuItem.Text = "删除消息";
            this.删除消息ToolStripMenuItem.Click += new System.EventHandler(this.删除消息ToolStripMenuItem_Click);
            // 
            // headContextMenuStrip
            // 
            this.headContextMenuStrip.Arrow = System.Drawing.Color.Black;
            this.headContextMenuStrip.Back = System.Drawing.Color.White;
            this.headContextMenuStrip.BackRadius = 4;
            this.headContextMenuStrip.Base = System.Drawing.Color.FromArgb(((int)(((byte)(105)))), ((int)(((byte)(200)))), ((int)(((byte)(254)))));
            this.headContextMenuStrip.CheckedImage = null;
            this.headContextMenuStrip.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.headContextMenuStrip.Fore = System.Drawing.Color.Black;
            this.headContextMenuStrip.HoverFore = System.Drawing.Color.White;
            this.headContextMenuStrip.ItemAnamorphosis = true;
            this.headContextMenuStrip.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.headContextMenuStrip.ItemBorderShow = false;
            this.headContextMenuStrip.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.headContextMenuStrip.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.headContextMenuStrip.ItemRadius = 4;
            this.headContextMenuStrip.ItemRadiusStyle = DSkin.Common.RoundStyle.All;
            this.headContextMenuStrip.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.headContextMenuStrip.Name = "headContextMenuStrip";
            this.headContextMenuStrip.RadiusStyle = DSkin.Common.RoundStyle.All;
            this.headContextMenuStrip.Size = new System.Drawing.Size(61, 4);
            this.headContextMenuStrip.SkinAllColor = true;
            this.headContextMenuStrip.TitleAnamorphosis = true;
            this.headContextMenuStrip.TitleColor = System.Drawing.Color.White;
            this.headContextMenuStrip.TitleRadius = 4;
            this.headContextMenuStrip.TitleRadiusStyle = DSkin.Common.RoundStyle.All;
            // 
            // 禁言ToolStripMenuItem
            // 
            this.禁言ToolStripMenuItem.Name = "禁言ToolStripMenuItem";
            this.禁言ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.禁言ToolStripMenuItem.Text = "禁言";
            this.禁言ToolStripMenuItem.Click += new System.EventHandler(this.禁言ToolStripMenuItem_Click);
            // 
            // 踢人ToolStripMenuItem
            // 
            this.踢人ToolStripMenuItem.Name = "踢人ToolStripMenuItem";
            this.踢人ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.踢人ToolStripMenuItem.Text = "踢人";
            this.踢人ToolStripMenuItem.Click += new System.EventHandler(this.踢人ToolStripMenuItem_Click);
            // 
            // 解除禁言ToolStripMenuItem
            // 
            this.解除禁言ToolStripMenuItem.Name = "解除禁言ToolStripMenuItem";
            this.解除禁言ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.解除禁言ToolStripMenuItem.Text = "解除禁言";
            this.解除禁言ToolStripMenuItem.Click += new System.EventHandler(this.解除禁言ToolStripMenuItem_Click);
            // 
            // 复制ToolStripMenuItem
            // 
            this.复制ToolStripMenuItem.Name = "复制ToolStripMenuItem";
            this.复制ToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.复制ToolStripMenuItem.Text = "复制";
            this.复制ToolStripMenuItem.Click += new System.EventHandler(this.复制ToolStripMenuItem_Click);
            // 
            // chatTextBox
            // 
            this.chatTextBox.AutoSize = false;
            this.chatTextBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.chatTextBox.Controls.Add(this.chatTextBox.InnerScrollBar);
            this.chatTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.chatTextBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.chatTextBox.Location = new System.Drawing.Point(0, 204);
            this.chatTextBox.Margin = new System.Windows.Forms.Padding(3);
            this.chatTextBox.Multiline = true;
            this.chatTextBox.Name = "chatTextBox";
            this.chatTextBox.Size = new System.Drawing.Size(446, 70);
            this.chatTextBox.onPasteImage += new System.EventHandler<System.Drawing.Image>(this.chatTextBox_onPasteImage);
            this.chatTextBox.onPasteText += new System.EventHandler<string>(this.chatTextBox_onPasteText);
            this.chatTextBox.onCopy += new System.EventHandler(this.chatTextBox_onCopy);
            this.chatTextBox.onSend += new System.EventHandler(this.chatTextBox_onSend);
            this.chatTextBox.TextChanged += new System.EventHandler(this.chatTextBox_TextChanged);
            this.chatTextBox.ControlRemoved += new System.EventHandler<DSkin.DirectUI.DuiControlEventArgs>(this.chatTextBox_ControlRemoved);
            this.chatTextBox.KeyDown += new System.EventHandler<System.Windows.Forms.KeyEventArgs>(this.chatTextBox_KeyDown);
            this.chatTextBox.KeyUp += new System.EventHandler<System.Windows.Forms.KeyEventArgs>(this.chatTextBox_KeyUp);
            // 
            // ChatPanl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.DUIControls.Add(this.chathistoryListBox);
            this.DUIControls.Add(this.chatToolbar);
            this.DUIControls.Add(this.chatTextBox);
            this.DUIControls.Add(this.sendButton);
            this.Name = "ChatPanl";
            this.Size = new System.Drawing.Size(446, 304);
            this.Load += new System.EventHandler(this.ChatPanl_Load);
            this.SizeChanged += new System.EventHandler(this.ChatPanl_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.ChatPanl_VisibleChanged);
            this.ParentChanged += new System.EventHandler(this.ChatPanl_ParentChanged);
            ((System.ComponentModel.ISupportInitialize)(this.chathistoryListBox)).EndInit();
            this.chatContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DSkin.DirectUI.DuiListBox chathistoryListBox;
        private DSkin.DirectUI.DuiBaseControl chatToolbar;
        private ChatTextBox chatTextBox;
        private DSkin.DirectUI.DuiButton sendButton;
        private DSkin.DirectUI.DuiButton faceButton;
        private DSkin.DirectUI.DuiButton fileButton;
        private System.Windows.Forms.Timer messageQueueTimer;
        private DSkin.Controls.DSkinContextMenuStrip chatContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 删除消息ToolStripMenuItem;
        public bool topLeave;
        private DSkin.Controls.DSkinContextMenuStrip headContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 禁言ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 踢人ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 解除禁言ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 复制ToolStripMenuItem;
        public Model.ReciveModel.FriendListData friend;
        public DB.GroupTable group;
    }
}
