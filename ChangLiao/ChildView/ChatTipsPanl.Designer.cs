namespace ChangLiao.ChildView
{
    partial class ChatTipsPanl
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
            this.SuspendLayout();
            // 
            // duiLabel1
            // 
            this.duiLabel1.AutoSize = true;
            this.duiLabel1.DesignModeCanMove = false;
            this.duiLabel1.DesignModeCanResize = false;
            this.duiLabel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.duiLabel1.EffectColor = System.Drawing.Color.White;
            this.duiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiLabel1.ForeColor = System.Drawing.Color.White;
            this.duiLabel1.Location = new System.Drawing.Point(10, 8);
            this.duiLabel1.Name = "duiLabel1";
            this.duiLabel1.Size = new System.Drawing.Size(200, 24);
            this.duiLabel1.Text = "1分钟之内只能发10条消息";
            this.duiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ChatTipsPanl
            // 
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(72)))), ((int)(((byte)(151)))));
            this.BackgroundRender.BorderColor = System.Drawing.Color.Transparent;
            this.BackgroundRender.Radius = 15;
            this.BackgroundRender.RenderBorders = true;
            this.Controls.Add(this.duiLabel1);
            this.Size = new System.Drawing.Size(326, 40);
            this.SizeChanged += new System.EventHandler(this.ChatTipsPanl_SizeChanged);
            this.VisibleChanged += new System.EventHandler(this.ChatTipsPanl_VisibleChanged);
            this.ResumeLayout();

        }

        #endregion

        private DSkin.DirectUI.DuiLabel duiLabel1;
    }
}
