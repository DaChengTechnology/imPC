namespace ChangLiao.Temple
{
    partial class newFriendListItemTemp
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
            this.duiBaseControl1 = new DSkin.DirectUI.DuiBaseControl();
            // 
            // duiPictureBox1
            // 
            this.duiPictureBox1.AutoSize = false;
            this.duiPictureBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiPictureBox1.Image = global::ChangLiao.Properties.Resources.moren;
            this.duiPictureBox1.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ChangLiao.Properties.Resources.moren))};
            this.duiPictureBox1.Location = new System.Drawing.Point(10, 5);
            this.duiPictureBox1.Name = "duiPictureBox1";
            this.duiPictureBox1.Size = new System.Drawing.Size(60, 60);
            this.duiPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // duiLabel1
            // 
            this.duiLabel1.AutoSize = true;
            this.duiLabel1.DesignModeCanResize = false;
            this.duiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiLabel1.Location = new System.Drawing.Point(70, 23);
            this.duiLabel1.Name = "duiLabel1";
            this.duiLabel1.Size = new System.Drawing.Size(99, 24);
            this.duiLabel1.Text = "Dui设计模式";
            // 
            // duiBaseControl1
            // 
            this.duiBaseControl1.AutoSize = false;
            this.duiBaseControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            this.duiBaseControl1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiBaseControl1.Location = new System.Drawing.Point(0, 69);
            this.duiBaseControl1.Name = "duiBaseControl1";
            this.duiBaseControl1.Size = new System.Drawing.Size(394, 1);
            // 
            // newFriendListItemTemp
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.duiPictureBox1);
            this.Controls.Add(this.duiLabel1);
            this.Controls.Add(this.duiBaseControl1);
            this.Size = new System.Drawing.Size(359, 70);
            this.IsSelectedChanged += new System.EventHandler(this.newFriendListItemTemp_IsSelectedChanged);
            this.MouseEnter += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.newFriendListItemTemp_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.newFriendListItemTemp_MouseLeave);
            this.SizeChanged += new System.EventHandler(this.newFriendListItemTemp_SizeChanged);

        }

        #endregion

        private DSkin.DirectUI.DuiPictureBox duiPictureBox1;
        private DSkin.DirectUI.DuiLabel duiLabel1;
        private DSkin.DirectUI.DuiBaseControl duiBaseControl1;
    }
}
