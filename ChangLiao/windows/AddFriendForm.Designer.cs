namespace ChangLiao.windows
{
    partial class AddFriendForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFriendForm));
            this.dSkinTextBox1 = new DSkin.Controls.DSkinTextBox();
            this.dSkinButton1 = new DSkin.Controls.DSkinButton();
            this.SuspendLayout();
            // 
            // dSkinTextBox1
            // 
            this.dSkinTextBox1.BitmapCache = false;
            this.dSkinTextBox1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinTextBox1.Location = new System.Drawing.Point(7, 37);
            this.dSkinTextBox1.Name = "dSkinTextBox1";
            this.dSkinTextBox1.Size = new System.Drawing.Size(234, 29);
            this.dSkinTextBox1.TabIndex = 0;
            this.dSkinTextBox1.TransparencyKey = System.Drawing.Color.Empty;
            this.dSkinTextBox1.WaterFont = new System.Drawing.Font("微软雅黑", 10F);
            this.dSkinTextBox1.WaterText = "搜索好友的畅聊号或者手机号";
            this.dSkinTextBox1.WaterTextOffset = new System.Drawing.Point(3, 2);
            this.dSkinTextBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.dSkinTextBox1_KeyDown);
            // 
            // dSkinButton1
            // 
            this.dSkinButton1.ButtonBorderWidth = 1;
            this.dSkinButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dSkinButton1.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton1.HoverImage = null;
            this.dSkinButton1.Location = new System.Drawing.Point(70, 82);
            this.dSkinButton1.Name = "dSkinButton1";
            this.dSkinButton1.NormalImage = null;
            this.dSkinButton1.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton1.PressedImage = null;
            this.dSkinButton1.Radius = 10;
            this.dSkinButton1.ShowButtonBorder = true;
            this.dSkinButton1.Size = new System.Drawing.Size(100, 27);
            this.dSkinButton1.TabIndex = 1;
            this.dSkinButton1.Text = "搜索";
            this.dSkinButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton1.TextPadding = 0;
            this.dSkinButton1.Click += new System.EventHandler(this.dSkinButton1_Click);
            // 
            // AddFriendForm
            // 
            this.AcceptButton = this.dSkinButton1;
            this.AllowDrop = false;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.CanResize = false;
            this.CaptionOffset = new System.Drawing.Point(2, 2);
            this.ClientSize = new System.Drawing.Size(248, 116);
            this.Controls.Add(this.dSkinButton1);
            this.Controls.Add(this.dSkinTextBox1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AddFriendForm";
            this.Radius = 10;
            this.ShowIcon = false;
            this.ShowShadow = true;
            this.Text = "添加好友";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DSkin.Controls.DSkinTextBox dSkinTextBox1;
        private DSkin.Controls.DSkinButton dSkinButton1;
    }
}