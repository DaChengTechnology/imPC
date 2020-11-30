namespace ChangLiao.ChildView
{
    partial class ConversationPanl
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
            this.searchTextBox = new CCWin.SkinControl.SkinTextBox();
            this.skinButton1 = new CCWin.SkinControl.SkinButton();
            this.conversationListBox1 = new ChangLiao.DIYListBox.ListBox.ConversationListBox();
            this.SuspendLayout();
            // 
            // searchTextBox
            // 
            this.searchTextBox.BackColor = System.Drawing.Color.Transparent;
            this.searchTextBox.DownBack = null;
            this.searchTextBox.Icon = null;
            this.searchTextBox.IconIsButton = false;
            this.searchTextBox.IconMouseState = CCWin.SkinClass.ControlState.Normal;
            this.searchTextBox.IsPasswordChat = '\0';
            this.searchTextBox.IsSystemPasswordChar = false;
            this.searchTextBox.Lines = new string[0];
            this.searchTextBox.Location = new System.Drawing.Point(10, 10);
            this.searchTextBox.Margin = new System.Windows.Forms.Padding(0);
            this.searchTextBox.MaxLength = 32767;
            this.searchTextBox.MinimumSize = new System.Drawing.Size(28, 28);
            this.searchTextBox.MouseBack = null;
            this.searchTextBox.MouseState = CCWin.SkinClass.ControlState.Normal;
            this.searchTextBox.Multiline = false;
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.NormlBack = null;
            this.searchTextBox.Padding = new System.Windows.Forms.Padding(5);
            this.searchTextBox.ReadOnly = false;
            this.searchTextBox.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.searchTextBox.Size = new System.Drawing.Size(121, 28);
            // 
            // 
            // 
            this.searchTextBox.SkinTxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.searchTextBox.SkinTxt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.searchTextBox.SkinTxt.Font = new System.Drawing.Font("微软雅黑", 9.75F);
            this.searchTextBox.SkinTxt.Location = new System.Drawing.Point(5, 5);
            this.searchTextBox.SkinTxt.Name = "BaseText";
            this.searchTextBox.SkinTxt.Size = new System.Drawing.Size(111, 18);
            this.searchTextBox.SkinTxt.TabIndex = 0;
            this.searchTextBox.SkinTxt.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.searchTextBox.SkinTxt.WaterText = "搜索";
            this.searchTextBox.TabIndex = 0;
            this.searchTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.searchTextBox.WaterColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(127)))), ((int)(((byte)(127)))));
            this.searchTextBox.WaterText = "搜索";
            this.searchTextBox.WordWrap = true;
            // 
            // skinButton1
            // 
            this.skinButton1.BackColor = System.Drawing.Color.Transparent;
            this.skinButton1.BackgroundImage = global::ChangLiao.Properties.Resources.addButton;
            this.skinButton1.BaseColor = System.Drawing.Color.Transparent;
            this.skinButton1.BorderColor = System.Drawing.Color.Transparent;
            this.skinButton1.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.skinButton1.DownBack = null;
            this.skinButton1.DownBaseColor = System.Drawing.Color.Transparent;
            this.skinButton1.Location = new System.Drawing.Point(140, 10);
            this.skinButton1.MouseBack = null;
            this.skinButton1.Name = "skinButton1";
            this.skinButton1.NormlBack = null;
            this.skinButton1.Size = new System.Drawing.Size(28, 28);
            this.skinButton1.TabIndex = 2;
            this.skinButton1.UseVisualStyleBackColor = false;
            // 
            // conversationListBox1
            // 
            this.conversationListBox1.ItemHeight = 60;
            this.conversationListBox1.Location = new System.Drawing.Point(0, 48);
            this.conversationListBox1.Margin = new System.Windows.Forms.Padding(0);
            this.conversationListBox1.Name = "conversationListBox1";
            this.conversationListBox1.Size = new System.Drawing.Size(180, 102);
            this.conversationListBox1.TabIndex = 3;
            // 
            // ConversationPanl
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(187)))), ((int)(((byte)(86)))));
            this.Controls.Add(this.conversationListBox1);
            this.Controls.Add(this.skinButton1);
            this.Controls.Add(this.searchTextBox);
            this.DoubleBuffered = true;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "ConversationPanl";
            this.Size = new System.Drawing.Size(180, 150);
            this.Load += new System.EventHandler(this.ConversationPanl_Load);
            this.SizeChanged += new System.EventHandler(this.ConversationPanl_SizeChanged);
            this.ResumeLayout(false);

        }

        #endregion

        private CCWin.SkinControl.SkinTextBox searchTextBox;
        private CCWin.SkinControl.SkinButton skinButton1;
        private DIYListBox.ListBox.ConversationListBox conversationListBox1;
    }
}
