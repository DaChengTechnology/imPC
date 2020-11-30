namespace ChangLiao.windows
{
    partial class MessageTipsForm
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
            this.duiListBox1 = new DSkin.DirectUI.DuiListBox();
            ((System.ComponentModel.ISupportInitialize)(this.duiListBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // duiListBox1
            // 
            this.duiListBox1.AutoSize = false;
            this.duiListBox1.BackColor = System.Drawing.Color.White;
            this.duiListBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiListBox1.ItemSize = new System.Drawing.Size(100, 100);
            this.duiListBox1.Location = new System.Drawing.Point(0, 20);
            this.duiListBox1.Name = "duiListBox1";
            this.duiListBox1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.duiListBox1.RollSize = 20;
            this.duiListBox1.ScrollBarWidth = 12;
            this.duiListBox1.ShowScrollBar = true;
            this.duiListBox1.Size = new System.Drawing.Size(150, 60);
            this.duiListBox1.SmoothScroll = false;
            this.duiListBox1.Ulmul = false;
            this.duiListBox1.ItemClick += new System.EventHandler<DSkin.Controls.ItemClickEventArgs>(this.duiListBox1_ItemClick);
            this.duiListBox1.MouseLeave += new System.EventHandler(this.duiListBox1_MouseLeave);
            // 
            // MessageTipsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.CanResize = false;
            this.ClientSize = new System.Drawing.Size(150, 80);
            this.DoubleClickMaximized = false;
            this.DragChangeBackImage = false;
            this.DrawIcon = false;
            this.DUIControls.Add(this.duiListBox1);
            this.EnableAnimation = false;
            this.Name = "MessageTipsForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.ShowSystemButtons = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "昵称";
            this.TopMost = true;
            this.Deactivate += new System.EventHandler(this.MessageTipsForm_Deactivate);
            this.MouseEnter += new System.EventHandler(this.MessageTipsForm_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.MessageTipsForm_MouseLeave);
            ((System.ComponentModel.ISupportInitialize)(this.duiListBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DSkin.DirectUI.DuiListBox duiListBox1;
    }
}