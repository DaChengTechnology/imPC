namespace ChangLiao.ChildView
{
    partial class SearchResultBox
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
            this.duiLabel1 = new DSkin.DirectUI.DuiLabel();
            ((System.ComponentModel.ISupportInitialize)(this.duiListBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // duiListBox1
            // 
            this.duiListBox1.AutoSize = false;
            this.duiListBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiListBox1.ItemSize = new System.Drawing.Size(100, 100);
            this.duiListBox1.Location = new System.Drawing.Point(0, 0);
            this.duiListBox1.Name = "duiListBox1";
            this.duiListBox1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.duiListBox1.RollSize = 20;
            this.duiListBox1.ScrollBarWidth = 12;
            this.duiListBox1.SelectionMode = DSkin.Controls.SelectionModes.Radio;
            this.duiListBox1.ShowScrollBar = true;
            this.duiListBox1.Size = new System.Drawing.Size(180, 100);
            this.duiListBox1.SmoothScroll = false;
            this.duiListBox1.Ulmul = false;
            // 
            // duiLabel1
            // 
            this.duiLabel1.DesignModeCanMove = false;
            this.duiLabel1.DesignModeCanResize = false;
            this.duiLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duiLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiLabel1.Location = new System.Drawing.Point(0, 0);
            this.duiLabel1.Name = "duiLabel1";
            this.duiLabel1.Size = new System.Drawing.Size(180, 100);
            this.duiLabel1.Text = "没有数据";
            this.duiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // SearchResultBox
            // 
            this.BackColor = System.Drawing.Color.White;
            this.DuiBackgroundRender.BorderWidth = 0;
            this.DuiBackgroundRender.Radius = 10;
            this.DuiBackgroundRender.RenderBorders = true;
            this.DUIControls.Add(this.duiListBox1);
            this.DUIControls.Add(this.duiLabel1);
            this.Name = "SearchResultBox";
            this.Size = new System.Drawing.Size(180, 100);
            this.SizeChanged += new System.EventHandler(this.SearchResultBox_SizeChanged);
            ((System.ComponentModel.ISupportInitialize)(this.duiListBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DSkin.DirectUI.DuiListBox duiListBox1;
        private DSkin.DirectUI.DuiLabel duiLabel1;
    }
}
