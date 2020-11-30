namespace ChangLiao.ChildView
{
    partial class FacePanl
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        public faceClick click;

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
            // 
            // FacePanl
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Borders.AllColor = System.Drawing.Color.DimGray;
            this.Borders.BottomColor = System.Drawing.Color.DimGray;
            this.Borders.LeftColor = System.Drawing.Color.DimGray;
            this.Borders.RightColor = System.Drawing.Color.DimGray;
            this.Borders.TopColor = System.Drawing.Color.DimGray;
            this.Load += new System.EventHandler(this.FacePanl_Load);

        }

        #endregion
    }
}
