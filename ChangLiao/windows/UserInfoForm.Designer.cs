namespace ChangLiao.windows
{
    partial class UserInfoForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UserInfoForm));
            this.personalPanl1 = new ChangLiao.ChildView.PersonalPanl();
            this.SuspendLayout();
            // 
            // personalPanl1
            // 
            this.personalPanl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.personalPanl1.BitmapCache = false;
            this.personalPanl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.personalPanl1.Location = new System.Drawing.Point(4, 34);
            this.personalPanl1.model = null;
            this.personalPanl1.Name = "personalPanl1";
            this.personalPanl1.RightBottom = ((System.Drawing.Image)(resources.GetObject("personalPanl1.RightBottom")));
            this.personalPanl1.Size = new System.Drawing.Size(503, 412);
            this.personalPanl1.TabIndex = 0;
            // 
            // UserInfoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.CaptionOffset = new System.Drawing.Point(3, 2);
            this.ClientSize = new System.Drawing.Size(511, 450);
            this.Controls.Add(this.personalPanl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UserInfoForm";
            this.Radius = 10;
            this.ShowShadow = true;
            this.Text = "个人信息";
            this.ResumeLayout(false);

        }

        #endregion

        private ChildView.PersonalPanl personalPanl1;
    }
}