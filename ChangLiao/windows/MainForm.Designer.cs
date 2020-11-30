namespace ChangLiao.windows
{
    partial class MainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.panel1 = new System.Windows.Forms.Panel();
            this.settingTabButton = new CCWin.SkinControl.SkinButton();
            this.contractButton = new CCWin.SkinControl.SkinButton();
            this.conversationTabButton = new CCWin.SkinControl.SkinButton();
            this.HeaderPicture = new CCWin.SkinControl.SkinPictureBox();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.conversationPanl1 = new ChangLiao.ChildView.ConversationPanl();
            this.chatPanl1 = new ChangLiao.ChildView.ChatPanl();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.HeaderPicture)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(72)))), ((int)(((byte)(151)))));
            this.panel1.Controls.Add(this.settingTabButton);
            this.panel1.Controls.Add(this.contractButton);
            this.panel1.Controls.Add(this.conversationTabButton);
            this.panel1.Controls.Add(this.HeaderPicture);
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(75, 450);
            this.panel1.TabIndex = 0;
            // 
            // settingTabButton
            // 
            this.settingTabButton.BackColor = System.Drawing.Color.Transparent;
            this.settingTabButton.BackgroundImage = global::ChangLiao.Properties.Resources.setting_normal;
            this.settingTabButton.BaseColor = System.Drawing.Color.Transparent;
            this.settingTabButton.BorderColor = System.Drawing.Color.Transparent;
            this.settingTabButton.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.settingTabButton.DownBack = global::ChangLiao.Properties.Resources.setting_select;
            this.settingTabButton.DownBaseColor = System.Drawing.Color.Transparent;
            this.settingTabButton.GlowColor = System.Drawing.Color.Transparent;
            this.settingTabButton.InnerBorderColor = System.Drawing.Color.Transparent;
            this.settingTabButton.Location = new System.Drawing.Point(11, 289);
            this.settingTabButton.MouseBack = global::ChangLiao.Properties.Resources.setting_select;
            this.settingTabButton.MouseBaseColor = System.Drawing.Color.Transparent;
            this.settingTabButton.Name = "settingTabButton";
            this.settingTabButton.NormlBack = null;
            this.settingTabButton.Size = new System.Drawing.Size(50, 50);
            this.settingTabButton.TabIndex = 3;
            this.settingTabButton.UseVisualStyleBackColor = false;
            this.settingTabButton.Click += new System.EventHandler(this.settingTabButton_Click);
            // 
            // contractButton
            // 
            this.contractButton.BackColor = System.Drawing.Color.Transparent;
            this.contractButton.BackgroundImage = global::ChangLiao.Properties.Resources.contract_normal;
            this.contractButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.contractButton.BaseColor = System.Drawing.Color.Transparent;
            this.contractButton.BorderColor = System.Drawing.Color.Transparent;
            this.contractButton.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.contractButton.DownBack = null;
            this.contractButton.DownBaseColor = System.Drawing.Color.Transparent;
            this.contractButton.GlowColor = System.Drawing.Color.Transparent;
            this.contractButton.InnerBorderColor = System.Drawing.Color.Transparent;
            this.contractButton.Location = new System.Drawing.Point(11, 194);
            this.contractButton.MouseBack = null;
            this.contractButton.MouseBaseColor = System.Drawing.Color.Transparent;
            this.contractButton.Name = "contractButton";
            this.contractButton.NormlBack = null;
            this.contractButton.Size = new System.Drawing.Size(50, 50);
            this.contractButton.TabIndex = 2;
            this.contractButton.UseVisualStyleBackColor = false;
            this.contractButton.Click += new System.EventHandler(this.contractButton_Click);
            // 
            // conversationTabButton
            // 
            this.conversationTabButton.BackColor = System.Drawing.Color.Transparent;
            this.conversationTabButton.BackgroundImage = global::ChangLiao.Properties.Resources.conversation_normal;
            this.conversationTabButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.conversationTabButton.BaseColor = System.Drawing.Color.Transparent;
            this.conversationTabButton.BorderColor = System.Drawing.Color.Transparent;
            this.conversationTabButton.ControlState = CCWin.SkinClass.ControlState.Normal;
            this.conversationTabButton.DownBack = null;
            this.conversationTabButton.DownBaseColor = System.Drawing.Color.Transparent;
            this.conversationTabButton.GlowColor = System.Drawing.Color.Transparent;
            this.conversationTabButton.InnerBorderColor = System.Drawing.Color.Transparent;
            this.conversationTabButton.Location = new System.Drawing.Point(11, 103);
            this.conversationTabButton.MouseBack = null;
            this.conversationTabButton.MouseBaseColor = System.Drawing.Color.Transparent;
            this.conversationTabButton.Name = "conversationTabButton";
            this.conversationTabButton.NormlBack = null;
            this.conversationTabButton.Size = new System.Drawing.Size(50, 50);
            this.conversationTabButton.TabIndex = 1;
            this.conversationTabButton.UseVisualStyleBackColor = false;
            this.conversationTabButton.Click += new System.EventHandler(this.conversationTabButton_Click);
            // 
            // HeaderPicture
            // 
            this.HeaderPicture.BackColor = System.Drawing.Color.Transparent;
            this.HeaderPicture.Image = global::ChangLiao.Properties.Resources.moren;
            this.HeaderPicture.Location = new System.Drawing.Point(11, 25);
            this.HeaderPicture.Name = "HeaderPicture";
            this.HeaderPicture.Size = new System.Drawing.Size(50, 50);
            this.HeaderPicture.TabIndex = 0;
            this.HeaderPicture.TabStop = false;
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.BalloonTipIcon = System.Windows.Forms.ToolTipIcon.Info;
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // conversationPanl1
            // 
            this.conversationPanl1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.conversationPanl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(187)))), ((int)(((byte)(86)))));
            this.conversationPanl1.Location = new System.Drawing.Point(75, 0);
            this.conversationPanl1.Margin = new System.Windows.Forms.Padding(0);
            this.conversationPanl1.Name = "conversationPanl1";
            this.conversationPanl1.Size = new System.Drawing.Size(180, 461);
            this.conversationPanl1.TabIndex = 1;
            // 
            // chatPanl1
            // 
            this.chatPanl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.chatPanl1.Location = new System.Drawing.Point(258, 31);
            this.chatPanl1.Name = "chatPanl1";
            this.chatPanl1.Size = new System.Drawing.Size(535, 412);
            this.chatPanl1.TabIndex = 2;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.chatPanl1);
            this.Controls.Add(this.conversationPanl1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "";
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.HeaderPicture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private CCWin.SkinControl.SkinPictureBox HeaderPicture;
        private CCWin.SkinControl.SkinButton settingTabButton;
        private CCWin.SkinControl.SkinButton contractButton;
        private CCWin.SkinControl.SkinButton conversationTabButton;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private ChildView.ConversationPanl conversationPanl1;
        private ChildView.ChatPanl chatPanl1;
    }
}