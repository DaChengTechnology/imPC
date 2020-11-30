namespace ChangLiao.Temple
{
    partial class SearchResultListHeaderItem
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
            this.duiLabel1 = new DSkin.DirectUI.DuiLabel();
            // 
            // duiLabel1
            // 
            this.duiLabel1.AutoSize = true;
            this.duiLabel1.DesignModeCanResize = false;
            this.duiLabel1.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiLabel1.Location = new System.Drawing.Point(5, 7);
            this.duiLabel1.Name = "duiLabel1";
            this.duiLabel1.Size = new System.Drawing.Size(72, 16);
            this.duiLabel1.Text = "Dui设计模式";
            // 
            // SearchResultListHeaderItem
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.duiLabel1);
            this.Size = new System.Drawing.Size(180, 30);

        }

        #endregion

        private DSkin.DirectUI.DuiLabel duiLabel1;
    }
}
