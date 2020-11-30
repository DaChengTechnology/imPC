namespace ChangLiao.Temple
{
    partial class UnreadTipsItem
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
            this.duiPictureBox1 = new DSkin.DirectUI.DuiPictureBox();
            this.duiLabel1 = new DSkin.DirectUI.DuiLabel();
            this.duiLabel2 = new DSkin.DirectUI.DuiLabel();
            // 
            // duiPictureBox1
            // 
            this.duiPictureBox1.AutoSize = false;
            this.duiPictureBox1.BackgroundRender.BorderWidth = 0;
            this.duiPictureBox1.BackgroundRender.Radius = 39;
            this.duiPictureBox1.BackgroundRender.RenderBorders = true;
            this.duiPictureBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiPictureBox1.Image = global::ChangLiao.Properties.Resources.moren;
            this.duiPictureBox1.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ChangLiao.Properties.Resources.moren))};
            this.duiPictureBox1.Location = new System.Drawing.Point(5, 5);
            this.duiPictureBox1.Name = "duiPictureBox1";
            this.duiPictureBox1.Size = new System.Drawing.Size(40, 40);
            this.duiPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // duiLabel1
            // 
            this.duiLabel1.AutoSize = true;
            this.duiLabel1.DesignModeCanResize = false;
            this.duiLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiLabel1.Location = new System.Drawing.Point(50, 8);
            this.duiLabel1.Name = "duiLabel1";
            this.duiLabel1.Size = new System.Drawing.Size(72, 16);
            this.duiLabel1.Text = "Dui设计模式";
            // 
            // duiLabel2
            // 
            this.duiLabel2.AutoSize = true;
            this.duiLabel2.DesignModeCanResize = false;
            this.duiLabel2.Font = new System.Drawing.Font("宋体", 8F);
            this.duiLabel2.ForeColor = System.Drawing.SystemColors.ControlDark;
            this.duiLabel2.Location = new System.Drawing.Point(50, 28);
            this.duiLabel2.Name = "duiLabel2";
            this.duiLabel2.Size = new System.Drawing.Size(64, 14);
            this.duiLabel2.Text = "Dui设计模式";
            // 
            // UnreadTipsItem
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.duiPictureBox1);
            this.Controls.Add(this.duiLabel1);
            this.Controls.Add(this.duiLabel2);
            this.Size = new System.Drawing.Size(150, 50);
            this.IsSelectedChanged += new System.EventHandler(this.UnreadTipsItem_IsSelectedChanged);
            this.MouseEnter += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.UnreadTipsItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.UnreadTipsItem_MouseLeave);

        }

        #endregion

        private DSkin.DirectUI.DuiPictureBox duiPictureBox1;
        private DSkin.DirectUI.DuiLabel duiLabel1;
        private DSkin.DirectUI.DuiLabel duiLabel2;
    }
}
