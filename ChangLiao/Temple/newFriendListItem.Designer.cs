namespace ChangLiao.Temple
{
    partial class newFriendListItem
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
            this.duiButton1 = new DSkin.DirectUI.DuiButton();
            // 
            // duiPictureBox1
            // 
            this.duiPictureBox1.AutoSize = false;
            this.duiPictureBox1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiPictureBox1.Image = global::ChangLiao.Properties.Resources.newFriend;
            this.duiPictureBox1.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ChangLiao.Properties.Resources.newFriend))};
            this.duiPictureBox1.Location = new System.Drawing.Point(10, 5);
            this.duiPictureBox1.Name = "duiPictureBox1";
            this.duiPictureBox1.Size = new System.Drawing.Size(40, 40);
            this.duiPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // duiLabel1
            // 
            this.duiLabel1.AutoSize = true;
            this.duiLabel1.DesignModeCanResize = false;
            this.duiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiLabel1.Location = new System.Drawing.Point(50, 13);
            this.duiLabel1.Name = "duiLabel1";
            this.duiLabel1.Size = new System.Drawing.Size(72, 24);
            this.duiLabel1.Text = "新的朋友";
            // 
            // duiButton1
            // 
            this.duiButton1.BaseColor = System.Drawing.Color.Red;
            this.duiButton1.ButtonBorderWidth = 0;
            this.duiButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiButton1.ForeColor = System.Drawing.Color.White;
            this.duiButton1.HoverColor = System.Drawing.Color.Empty;
            this.duiButton1.Location = new System.Drawing.Point(150, 16);
            this.duiButton1.Name = "duiButton1";
            this.duiButton1.PressColor = System.Drawing.Color.Empty;
            this.duiButton1.Radius = 17;
            this.duiButton1.Size = new System.Drawing.Size(18, 18);
            this.duiButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // newFriendListItem
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.duiPictureBox1);
            this.Controls.Add(this.duiLabel1);
            this.Controls.Add(this.duiButton1);
            this.Size = new System.Drawing.Size(180, 50);
            this.IsSelectedChanged += new System.EventHandler(this.newFriendListItem_IsSelectedChanged);
            this.MouseEnter += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.newFriendListItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.newFriendListItem_MouseLeave);

        }

        #endregion

        private DSkin.DirectUI.DuiPictureBox duiPictureBox1;
        private DSkin.DirectUI.DuiLabel duiLabel1;
        private DSkin.DirectUI.DuiButton duiButton1;
    }
}
