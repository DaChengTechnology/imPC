namespace ChangLiao.windows
{
    partial class SetBacupNameForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetBacupNameForm));
            this.dSkinTextBox1 = new DSkin.Controls.DSkinTextBox();
            this.dSkinButton1 = new DSkin.Controls.DSkinButton();
            this.dSkinButton2 = new DSkin.Controls.DSkinButton();
            this.SuspendLayout();
            // 
            // dSkinTextBox1
            // 
            this.dSkinTextBox1.BitmapCache = false;
            this.dSkinTextBox1.Font = new System.Drawing.Font("宋体", 12F);
            this.dSkinTextBox1.Location = new System.Drawing.Point(26, 37);
            this.dSkinTextBox1.Name = "dSkinTextBox1";
            this.dSkinTextBox1.Size = new System.Drawing.Size(255, 26);
            this.dSkinTextBox1.TabIndex = 0;
            this.dSkinTextBox1.TransparencyKey = System.Drawing.Color.Empty;
            this.dSkinTextBox1.WaterFont = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinTextBox1.WaterText = "";
            this.dSkinTextBox1.WaterTextOffset = new System.Drawing.Point(0, 0);
            // 
            // dSkinButton1
            // 
            this.dSkinButton1.ButtonBorderWidth = 1;
            this.dSkinButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dSkinButton1.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton1.HoverImage = null;
            this.dSkinButton1.Location = new System.Drawing.Point(26, 69);
            this.dSkinButton1.Name = "dSkinButton1";
            this.dSkinButton1.NormalImage = null;
            this.dSkinButton1.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton1.PressedImage = null;
            this.dSkinButton1.Radius = 10;
            this.dSkinButton1.ShowButtonBorder = true;
            this.dSkinButton1.Size = new System.Drawing.Size(100, 31);
            this.dSkinButton1.TabIndex = 1;
            this.dSkinButton1.Text = "确定";
            this.dSkinButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton1.TextPadding = 0;
            this.dSkinButton1.Click += new System.EventHandler(this.dSkinButton1_Click);
            // 
            // dSkinButton2
            // 
            this.dSkinButton2.ButtonBorderWidth = 1;
            this.dSkinButton2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.dSkinButton2.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton2.HoverImage = null;
            this.dSkinButton2.Location = new System.Drawing.Point(181, 69);
            this.dSkinButton2.Name = "dSkinButton2";
            this.dSkinButton2.NormalImage = null;
            this.dSkinButton2.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton2.PressedImage = null;
            this.dSkinButton2.Radius = 10;
            this.dSkinButton2.ShowButtonBorder = true;
            this.dSkinButton2.Size = new System.Drawing.Size(100, 31);
            this.dSkinButton2.TabIndex = 2;
            this.dSkinButton2.Text = "取消";
            this.dSkinButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton2.TextPadding = 0;
            this.dSkinButton2.Click += new System.EventHandler(this.dSkinButton2_Click);
            // 
            // SetBacupNameForm
            // 
            this.AcceptButton = this.dSkinButton1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.CancelButton = this.dSkinButton2;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(318, 107);
            this.Controls.Add(this.dSkinButton2);
            this.Controls.Add(this.dSkinButton1);
            this.Controls.Add(this.dSkinTextBox1);
            this.DoubleClickMaximized = false;
            this.DragChangeBackImage = false;
            this.DuiBackgroundRender.Radius = 10;
            this.DuiBackgroundRender.RenderBorders = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SetBacupNameForm";
            this.ShowInTaskbar = false;
            this.Text = "设置备注";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DSkin.Controls.DSkinTextBox dSkinTextBox1;
        private DSkin.Controls.DSkinButton dSkinButton1;
        private DSkin.Controls.DSkinButton dSkinButton2;
        public string userID;
        public delegate void onOK(string id, string name);
        public onOK ChangeBackupName;
    }
}