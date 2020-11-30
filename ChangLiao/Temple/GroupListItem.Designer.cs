namespace ChangLiao.Temple
{
    partial class GroupListItem
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
            this.headPictureBox = new DSkin.DirectUI.DuiPictureBox();
            this.nameLabel = new DSkin.DirectUI.DuiLabel();
            this.line = new DSkin.DirectUI.DuiBaseControl();
            // 
            // headPictureBox
            // 
            this.headPictureBox.AutoSize = false;
            this.headPictureBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.headPictureBox.Image = global::ChangLiao.Properties.Resources.moren;
            this.headPictureBox.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ChangLiao.Properties.Resources.moren))};
            this.headPictureBox.Location = new System.Drawing.Point(10, 5);
            this.headPictureBox.Name = "headPictureBox";
            this.headPictureBox.Size = new System.Drawing.Size(40, 40);
            this.headPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.DesignModeCanResize = false;
            this.nameLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameLabel.Location = new System.Drawing.Point(60, 17);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(72, 16);
            this.nameLabel.Text = "Dui设计模式";
            // 
            // line
            // 
            this.line.AutoSize = false;
            this.line.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.line.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.line.Location = new System.Drawing.Point(0, 49);
            this.line.Name = "line";
            this.line.Size = new System.Drawing.Size(180, 1);
            // 
            // GroupListItem
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.headPictureBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.line);
            this.Size = new System.Drawing.Size(180, 50);
            this.IsSelectedChanged += new System.EventHandler(this.GroupListItem_IsSelectedChanged);
            this.MouseEnter += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.GroupListItem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.GroupListItem_MouseLeave);

        }

        #endregion

        private DSkin.DirectUI.DuiPictureBox headPictureBox;
        private DSkin.DirectUI.DuiLabel nameLabel;
        private DSkin.DirectUI.DuiBaseControl line;
    }
}
