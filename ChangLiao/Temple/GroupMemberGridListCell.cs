using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSkin.Controls;

namespace ChangLiao.Temple
{
    class GroupMemberGridListCell:DSkinGridListCellTemplate
    {
        private DSkin.DirectUI.DuiLabel duiLabel1;

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
            this.duiLabel1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.duiLabel1.Location = new System.Drawing.Point(0, 0);
            this.duiLabel1.Name = "duiLabel1";
            this.duiLabel1.Size = new System.Drawing.Size(99, 24);
            this.duiLabel1.Text = "Dui设计模式";
            this.duiLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // GroupMemberGridListCell
            // 
            this.Controls.Add(this.duiLabel1);
            this.ResumeLayout();

        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            duiLabel1.Text = this.Value as string;
        }

        public GroupMemberGridListCell()
        {
            InitializeComponent();
        }
    }
}
