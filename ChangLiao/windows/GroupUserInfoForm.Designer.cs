namespace ChangLiao.windows
{
    partial class GroupUserInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GroupUserInfoForm));
            this.duiPictureBox1 = new DSkin.DirectUI.DuiPictureBox();
            this.nameLabel1 = new DSkin.DirectUI.DuiLabel();
            this.groupNameLabel = new DSkin.DirectUI.DuiLabel();
            this.comeFromLabel = new DSkin.DirectUI.DuiLabel();
            this.comevarLabel1 = new DSkin.DirectUI.DuiLabel();
            this.sendMessaageButton = new DSkin.DirectUI.DuiButton();
            this.addFriendButton = new DSkin.DirectUI.DuiButton();
            this.SuspendLayout();
            // 
            // duiPictureBox1
            // 
            this.duiPictureBox1.AutoSize = false;
            this.duiPictureBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiPictureBox1.Image = global::ChangLiao.Properties.Resources.moren;
            this.duiPictureBox1.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ChangLiao.Properties.Resources.moren))};
            this.duiPictureBox1.Location = new System.Drawing.Point(243, 40);
            this.duiPictureBox1.Name = "duiPictureBox1";
            this.duiPictureBox1.Size = new System.Drawing.Size(100, 100);
            this.duiPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // nameLabel1
            // 
            this.nameLabel1.AutoSize = true;
            this.nameLabel1.DesignModeCanResize = false;
            this.nameLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameLabel1.Location = new System.Drawing.Point(240, 150);
            this.nameLabel1.Name = "nameLabel1";
            this.nameLabel1.Size = new System.Drawing.Size(72, 16);
            this.nameLabel1.Text = "Dui设计模式";
            // 
            // groupNameLabel
            // 
            this.groupNameLabel.AutoSize = true;
            this.groupNameLabel.DesignModeCanResize = false;
            this.groupNameLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupNameLabel.Location = new System.Drawing.Point(241, 170);
            this.groupNameLabel.Name = "groupNameLabel";
            this.groupNameLabel.Size = new System.Drawing.Size(72, 16);
            this.groupNameLabel.Text = "Dui设计模式";
            // 
            // comeFromLabel
            // 
            this.comeFromLabel.AutoSize = true;
            this.comeFromLabel.DesignModeCanResize = false;
            this.comeFromLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comeFromLabel.Location = new System.Drawing.Point(241, 190);
            this.comeFromLabel.Name = "comeFromLabel";
            this.comeFromLabel.Size = new System.Drawing.Size(72, 16);
            this.comeFromLabel.Text = "Dui设计模式";
            // 
            // comevarLabel1
            // 
            this.comevarLabel1.AutoSize = true;
            this.comevarLabel1.DesignModeCanResize = false;
            this.comevarLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.comevarLabel1.Location = new System.Drawing.Point(241, 210);
            this.comevarLabel1.Name = "comevarLabel1";
            this.comevarLabel1.Size = new System.Drawing.Size(72, 16);
            this.comevarLabel1.Text = "Dui设计模式";
            // 
            // sendMessaageButton
            // 
            this.sendMessaageButton.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(214)))), ((int)(((byte)(26)))));
            this.sendMessaageButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sendMessaageButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(72)))), ((int)(((byte)(151)))));
            this.sendMessaageButton.HoverColor = System.Drawing.Color.Empty;
            this.sendMessaageButton.Location = new System.Drawing.Point(200, 230);
            this.sendMessaageButton.Name = "sendMessaageButton";
            this.sendMessaageButton.PressColor = System.Drawing.Color.Empty;
            this.sendMessaageButton.Size = new System.Drawing.Size(150, 50);
            this.sendMessaageButton.Text = "发送消息";
            this.sendMessaageButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.sendMessaageButton.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.sendMessaageButton_MouseClick);
            // 
            // addFriendButton
            // 
            this.addFriendButton.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.addFriendButton.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(72)))), ((int)(((byte)(151)))));
            this.addFriendButton.HoverColor = System.Drawing.Color.Empty;
            this.addFriendButton.Location = new System.Drawing.Point(200, 300);
            this.addFriendButton.Name = "addFriendButton";
            this.addFriendButton.PressColor = System.Drawing.Color.Empty;
            this.addFriendButton.Size = new System.Drawing.Size(150, 50);
            this.addFriendButton.Text = "加为好友";
            this.addFriendButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.addFriendButton.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.addFriendButton_MouseClick);
            // 
            // GroupUserInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.CanResize = false;
            this.CaptionOffset = new System.Drawing.Point(3, 2);
            this.ClientSize = new System.Drawing.Size(587, 450);
            this.DUIControls.Add(this.duiPictureBox1);
            this.DUIControls.Add(this.nameLabel1);
            this.DUIControls.Add(this.groupNameLabel);
            this.DUIControls.Add(this.comeFromLabel);
            this.DUIControls.Add(this.comevarLabel1);
            this.DUIControls.Add(this.sendMessaageButton);
            this.DUIControls.Add(this.addFriendButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GroupUserInfoForm";
            this.Text = "群成员信息";
            this.Load += new System.EventHandler(this.GroupUserInfoForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private DSkin.DirectUI.DuiPictureBox duiPictureBox1;
        private DSkin.DirectUI.DuiLabel nameLabel1;
        private DSkin.DirectUI.DuiLabel groupNameLabel;
        private DSkin.DirectUI.DuiLabel comeFromLabel;
        private DSkin.DirectUI.DuiLabel comevarLabel1;
        private DSkin.DirectUI.DuiButton sendMessaageButton;
        private DSkin.DirectUI.DuiButton addFriendButton;
    }
}