namespace ChangLiao.ChildView
{
    partial class NewFriendPanl
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
            this.duiListBox1 = new DSkin.DirectUI.DuiListBox();
            ((System.ComponentModel.ISupportInitialize)(this.duiListBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // duiListBox1
            // 
            this.duiListBox1.AutoSize = false;
            this.duiListBox1.DesignModeCanMove = false;
            this.duiListBox1.DesignModeCanResize = false;
            this.duiListBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duiListBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiListBox1.ItemSize = new System.Drawing.Size(100, 100);
            this.duiListBox1.Location = new System.Drawing.Point(0, 0);
            this.duiListBox1.Name = "duiListBox1";
            this.duiListBox1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.duiListBox1.RollSize = 20;
            this.duiListBox1.ScrollBarWidth = 12;
            this.duiListBox1.ShowScrollBar = true;
            this.duiListBox1.Size = new System.Drawing.Size(376, 150);
            this.duiListBox1.SmoothScroll = false;
            this.duiListBox1.Ulmul = false;
            this.duiListBox1.ItemClick += new System.EventHandler<DSkin.Controls.ItemClickEventArgs>(this.duiListBox1_ItemClick);
            this.duiListBox1.ItemAdded += new System.EventHandler<DSkin.DirectUI.DuiControlEventArgs>(this.duiListBox1_ItemAdded);
            // 
            // NewFriendPanl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.DUIControls.Add(this.duiListBox1);
            this.Name = "NewFriendPanl";
            this.Size = new System.Drawing.Size(376, 150);
            ((System.ComponentModel.ISupportInitialize)(this.duiListBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DSkin.DirectUI.DuiListBox duiListBox1;
    }
}
