namespace ChangLiao.windows
{
    partial class UpdateForm
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
            DSkin.Controls.BlendColor blendColor1 = new DSkin.Controls.BlendColor();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(UpdateForm));
            this.dSkinProgressBar1 = new DSkin.Controls.DSkinProgressBar();
            this.dSkinLinearGradientBrush1 = new DSkin.Controls.DSkinLinearGradientBrush();
            this.dSkinLabel1 = new DSkin.Controls.DSkinLabel();
            this.SuspendLayout();
            // 
            // dSkinProgressBar1
            // 
            this.dSkinProgressBar1.AutoSize = false;
            this.dSkinProgressBar1.BackColor = System.Drawing.Color.Silver;
            this.dSkinProgressBar1.DuiBackgroundRender.BorderColor = System.Drawing.Color.DimGray;
            this.dSkinProgressBar1.DuiBackgroundRender.Radius = 13;
            this.dSkinProgressBar1.DuiBackgroundRender.RenderBorders = true;
            this.dSkinProgressBar1.ForeBrush = this.dSkinLinearGradientBrush1;
            this.dSkinProgressBar1.ForeColors = new System.Drawing.Color[] {
        System.Drawing.Color.Lime};
            this.dSkinProgressBar1.ForeColorsAngle = 90F;
            this.dSkinProgressBar1.Format = "50\'%\'";
            this.dSkinProgressBar1.Location = new System.Drawing.Point(30, 73);
            this.dSkinProgressBar1.Name = "dSkinProgressBar1";
            this.dSkinProgressBar1.Size = new System.Drawing.Size(358, 14);
            this.dSkinProgressBar1.TabIndex = 0;
            this.dSkinProgressBar1.Text = "50%";
            // 
            // dSkinLinearGradientBrush1
            // 
            blendColor1.Color = System.Drawing.Color.Lime;
            blendColor1.Position = 1F;
            this.dSkinLinearGradientBrush1.Colors.AddRange(new DSkin.Controls.BlendColor[] {
            blendColor1});
            this.dSkinLinearGradientBrush1.GammaCorrection = true;
            this.dSkinLinearGradientBrush1.WrapMode = System.Drawing.Drawing2D.WrapMode.Tile;
            // 
            // dSkinLabel1
            // 
            this.dSkinLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.dSkinLabel1.Location = new System.Drawing.Point(315, 53);
            this.dSkinLabel1.Name = "dSkinLabel1";
            this.dSkinLabel1.Size = new System.Drawing.Size(73, 14);
            this.dSkinLabel1.TabIndex = 1;
            this.dSkinLabel1.Text = "dSkinLabel1";
            // 
            // UpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.ClientSize = new System.Drawing.Size(418, 135);
            this.Controls.Add(this.dSkinLabel1);
            this.Controls.Add(this.dSkinProgressBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "UpdateForm";
            this.ShowSystemButtons = false;
            this.Text = "自动更新";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DSkin.Controls.DSkinProgressBar dSkinProgressBar1;
        private DSkin.Controls.DSkinLinearGradientBrush dSkinLinearGradientBrush1;
        private DSkin.Controls.DSkinLabel dSkinLabel1;
    }
}