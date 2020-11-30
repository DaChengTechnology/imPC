namespace ChangLiao.Temple
{
    partial class ContractOrGroupListItem
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
            this.cantractBtn = new DSkin.DirectUI.DuiBaseControl();
            this.groupBtn = new DSkin.DirectUI.DuiBaseControl();
            this.contractLabel = new DSkin.DirectUI.DuiLabel();
            this.contractLine = new DSkin.DirectUI.DuiBaseControl();
            this.groupLabel = new DSkin.DirectUI.DuiLabel();
            this.groupLine = new DSkin.DirectUI.DuiBaseControl();
            // 
            // cantractBtn
            // 
            this.cantractBtn.AutoSize = false;
            this.cantractBtn.BackColor = System.Drawing.Color.White;
            this.cantractBtn.Controls.Add(this.contractLabel);
            this.cantractBtn.Controls.Add(this.contractLine);
            this.cantractBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.cantractBtn.Location = new System.Drawing.Point(0, 0);
            this.cantractBtn.MouseEventBubble = false;
            this.cantractBtn.Name = "cantractBtn";
            this.cantractBtn.Size = new System.Drawing.Size(90, 50);
            this.cantractBtn.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.cantractBtn_MouseClick);
            // 
            // groupBtn
            // 
            this.groupBtn.AutoSize = false;
            this.groupBtn.BackColor = System.Drawing.Color.White;
            this.groupBtn.Controls.Add(this.groupLabel);
            this.groupBtn.Controls.Add(this.groupLine);
            this.groupBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupBtn.Location = new System.Drawing.Point(90, 0);
            this.groupBtn.Name = "groupBtn";
            this.groupBtn.Size = new System.Drawing.Size(90, 50);
            this.groupBtn.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.groupBtn_MouseClick);
            // 
            // contractLabel
            // 
            this.contractLabel.AutoEllipsis = true;
            this.contractLabel.AutoSize = true;
            this.contractLabel.DesignModeCanResize = false;
            this.contractLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.contractLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.contractLabel.Location = new System.Drawing.Point(22, 13);
            this.contractLabel.Name = "contractLabel";
            this.contractLabel.Size = new System.Drawing.Size(55, 24);
            this.contractLabel.Text = "联系人";
            this.contractLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contractLine
            // 
            this.contractLine.AutoSize = false;
            this.contractLine.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.contractLine.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.contractLine.Location = new System.Drawing.Point(10, 40);
            this.contractLine.Name = "contractLine";
            this.contractLine.Size = new System.Drawing.Size(70, 1);
            // 
            // groupLabel
            // 
            this.groupLabel.AutoSize = true;
            this.groupLabel.DesignModeCanResize = false;
            this.groupLabel.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupLabel.ForeColor = System.Drawing.SystemColors.AppWorkspace;
            this.groupLabel.Location = new System.Drawing.Point(25, 13);
            this.groupLabel.Name = "groupLabel";
            this.groupLabel.Size = new System.Drawing.Size(39, 24);
            this.groupLabel.Text = "群组";
            // 
            // groupLine
            // 
            this.groupLine.AutoSize = false;
            this.groupLine.BackColor = System.Drawing.SystemColors.AppWorkspace;
            this.groupLine.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.groupLine.Location = new System.Drawing.Point(10, 40);
            this.groupLine.Name = "groupLine";
            this.groupLine.Size = new System.Drawing.Size(70, 1);
            // 
            // ContractOrGroupListItem
            // 
            this.Controls.Add(this.cantractBtn);
            this.Controls.Add(this.groupBtn);
            this.Size = new System.Drawing.Size(180, 50);

        }

        #endregion

        private DSkin.DirectUI.DuiBaseControl cantractBtn;
        private DSkin.DirectUI.DuiBaseControl groupBtn;
        private DSkin.DirectUI.DuiLabel contractLabel;
        private DSkin.DirectUI.DuiBaseControl contractLine;
        private DSkin.DirectUI.DuiLabel groupLabel;
        private DSkin.DirectUI.DuiBaseControl groupLine;
        public delegate void BtnClick(int index);
        public BtnClick SelectTab;
    }
}
