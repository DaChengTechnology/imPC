namespace ChangLiao.windows
{
    partial class ChangeGroupOwnerForm
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
            this.chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser("file:///"+System.IO.Directory.GetCurrentDirectory()+"/dist/index.html#/index2");
            this.cancenButton = new DSkin.Controls.DSkinButton();
            this.okButton = new DSkin.Controls.DSkinButton();
            this.SuspendLayout();
            // 
            // chromiumWebBrowser1
            // 
            this.chromiumWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.chromiumWebBrowser1.Location = new System.Drawing.Point(4, 34);
            this.chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            this.chromiumWebBrowser1.Size = new System.Drawing.Size(792, 412);
            this.chromiumWebBrowser1.TabIndex = 0;
            // 
            // cancenButton
            // 
            this.cancenButton.ButtonBorderWidth = 1;
            this.cancenButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cancenButton.HoverColor = System.Drawing.Color.Empty;
            this.cancenButton.HoverImage = null;
            this.cancenButton.Location = new System.Drawing.Point(693, 414);
            this.cancenButton.Name = "cancenButton";
            this.cancenButton.NormalImage = null;
            this.cancenButton.PressColor = System.Drawing.Color.Empty;
            this.cancenButton.PressedImage = null;
            this.cancenButton.Radius = 10;
            this.cancenButton.ShowButtonBorder = true;
            this.cancenButton.Size = new System.Drawing.Size(100, 32);
            this.cancenButton.TabIndex = 1;
            this.cancenButton.Text = "取消";
            this.cancenButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cancenButton.TextPadding = 0;
            this.cancenButton.Click += new System.EventHandler(this.cancenButton_Click);
            // 
            // okButton
            // 
            this.okButton.ButtonBorderWidth = 1;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.okButton.HoverColor = System.Drawing.Color.Empty;
            this.okButton.HoverImage = null;
            this.okButton.Location = new System.Drawing.Point(587, 414);
            this.okButton.Name = "okButton";
            this.okButton.NormalImage = null;
            this.okButton.PressColor = System.Drawing.Color.Empty;
            this.okButton.PressedImage = null;
            this.okButton.Radius = 10;
            this.okButton.ShowButtonBorder = true;
            this.okButton.Size = new System.Drawing.Size(100, 32);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "确认";
            this.okButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.okButton.TextPadding = 0;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // ChangeGroupOwnerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancenButton);
            this.Controls.Add(this.chromiumWebBrowser1);
            this.IsLayeredWindowForm = false;
            this.Name = "ChangeGroupOwnerForm";
            this.Text = "ChangeGroupOwnerForm";
            this.Load += new System.EventHandler(this.ChangeGroupOwnerForm_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private DSkin.Controls.DSkinButton cancenButton;
        private DSkin.Controls.DSkinButton okButton;
        public string groupId;
    }
}