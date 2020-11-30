namespace ChangLiao.windows
{
    partial class NewGroupForm
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
        [System.Obsolete]
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NewGroupForm));
            if (string.IsNullOrEmpty(groupID))
            {
                chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser("http://pc.imchangliao.com/pccl/#/index");
            }
            else
            {
                chromiumWebBrowser1 = new CefSharp.WinForms.ChromiumWebBrowser("http://pc.imchangliao.com/pccl/#/index3?gid=" + groupID);
            }
            this.cancelButton = new DSkin.Controls.DSkinButton();
            this.okButton = new DSkin.Controls.DSkinButton();
            this.SuspendLayout();
            // 
            // chromiumWebBrowser1
            // 
            chromiumWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            chromiumWebBrowser1.Location = new System.Drawing.Point(4, 34);
            chromiumWebBrowser1.Name = "chromiumWebBrowser1";
            chromiumWebBrowser1.Size = new System.Drawing.Size(792, 412);
            chromiumWebBrowser1.TabIndex = 0;
            chromiumWebBrowser1.JavascriptObjectRepository.Register("desktopAPP", new ChangLiao.Util.NewGroupJSClass()) ;
            //chromiumWebBrowser1.JavascriptObjectRepository
            
            // 
            // cancelButton
            // 
            this.cancelButton.ButtonBorderWidth = 1;
            this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.cancelButton.HoverColor = System.Drawing.Color.Empty;
            this.cancelButton.HoverImage = null;
            this.cancelButton.Location = new System.Drawing.Point(699, 417);
            this.cancelButton.Name = "cancelButton";
            this.cancelButton.NormalImage = null;
            this.cancelButton.PressColor = System.Drawing.Color.Empty;
            this.cancelButton.PressedImage = null;
            this.cancelButton.Radius = 10;
            this.cancelButton.ShowButtonBorder = true;
            this.cancelButton.Size = new System.Drawing.Size(94, 29);
            this.cancelButton.TabIndex = 1;
            this.cancelButton.Text = "取消";
            this.cancelButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.cancelButton.TextPadding = 0;
            this.cancelButton.Click += new System.EventHandler(this.cancelButton_Click);
            // 
            // okButton
            // 
            this.okButton.ButtonBorderWidth = 1;
            this.okButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.okButton.HoverColor = System.Drawing.Color.Empty;
            this.okButton.HoverImage = null;
            this.okButton.Location = new System.Drawing.Point(599, 417);
            this.okButton.Name = "okButton";
            this.okButton.NormalImage = null;
            this.okButton.PressColor = System.Drawing.Color.Empty;
            this.okButton.PressedImage = null;
            this.okButton.Radius = 10;
            this.okButton.ShowButtonBorder = true;
            this.okButton.Size = new System.Drawing.Size(94, 29);
            this.okButton.TabIndex = 2;
            this.okButton.Text = "确认";
            this.okButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.okButton.TextPadding = 0;
            this.okButton.Click += new System.EventHandler(this.okButton_Click);
            // 
            // NewGroupForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.CaptionOffset = new System.Drawing.Point(10, 3);
            this.ClientSize = new System.Drawing.Size(850, 450);
            this.Controls.Add(this.okButton);
            this.Controls.Add(this.cancelButton);
            this.Controls.Add(chromiumWebBrowser1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsLayeredWindowForm = false;
            this.Name = "NewGroupForm";
            this.Radius = 10;
            this.ShowShadow = true;
            this.CanResize = false;
            this.Text = "新的群组";
            this.Load += new System.EventHandler(this.NewGroupForm_Load);
            this.Resize += new System.EventHandler(this.NewGroupForm_Resize);
            this.ResumeLayout(false);

        }

        #endregion

        private CefSharp.WinForms.ChromiumWebBrowser chromiumWebBrowser1;
        private DSkin.Controls.DSkinButton cancelButton;
        private DSkin.Controls.DSkinButton okButton;
        public event System.Action<string> oked;
        public event System.Action canceled;
    }
}