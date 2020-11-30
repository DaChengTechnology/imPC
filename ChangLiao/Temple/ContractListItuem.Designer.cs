namespace ChangLiao.Temple
{
    partial class ContractListItuem
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
            this.avatarPictureBox = new DSkin.DirectUI.DuiPictureBox();
            this.nameLabel = new DSkin.DirectUI.DuiLabel();
            this.idLabel = new DSkin.DirectUI.DuiLabel();
            this.line = new DSkin.DirectUI.DuiBaseControl();
            // 
            // avatarPictureBox
            // 
            this.avatarPictureBox.AutoSize = false;
            this.avatarPictureBox.BackgroundImage = global::ChangLiao.Properties.Resources.moren;
            this.avatarPictureBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.avatarPictureBox.BackgroundRender.Radius = 10;
            this.avatarPictureBox.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.avatarPictureBox.Images = new System.Drawing.Image[] {
        null};
            this.avatarPictureBox.Location = new System.Drawing.Point(10, 5);
            this.avatarPictureBox.Name = "avatarPictureBox";
            this.avatarPictureBox.Size = new System.Drawing.Size(40, 40);
            this.avatarPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            // 
            // nameLabel
            // 
            this.nameLabel.AutoSize = true;
            this.nameLabel.DesignModeCanResize = false;
            this.nameLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.nameLabel.Location = new System.Drawing.Point(55, 3);
            this.nameLabel.Name = "nameLabel";
            this.nameLabel.Size = new System.Drawing.Size(50, 24);
            this.nameLabel.Text = "name";
            // 
            // idLabel
            // 
            this.idLabel.AutoSize = true;
            this.idLabel.DesignModeCanResize = false;
            this.idLabel.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.idLabel.Location = new System.Drawing.Point(55, 30);
            this.idLabel.Name = "idLabel";
            this.idLabel.Size = new System.Drawing.Size(72, 16);
            this.idLabel.Text = "Dui设计模式";
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
            // ContractListItuem
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.avatarPictureBox);
            this.Controls.Add(this.nameLabel);
            this.Controls.Add(this.idLabel);
            this.Controls.Add(this.line);
            this.Size = new System.Drawing.Size(180, 50);
            this.IsSelectedChanged += new System.EventHandler(this.ContractListItuem_IsSelectedChanged);
            this.MouseEnter += new System.EventHandler<System.Windows.Forms.MouseEventArgs>(this.ContractListItuem_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.ContractListItuem_MouseLeave);

        }

        #endregion

        private DSkin.DirectUI.DuiPictureBox avatarPictureBox;
        private DSkin.DirectUI.DuiLabel nameLabel;
        private DSkin.DirectUI.DuiLabel idLabel;
        private DSkin.DirectUI.DuiBaseControl line;
    }
}
