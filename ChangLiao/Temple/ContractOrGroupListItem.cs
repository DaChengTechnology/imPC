using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.DirectUI;
using DSkin.Controls;

namespace ChangLiao.Temple
{
    public partial class ContractOrGroupListItem : DSkinListItemTemplate
    {
        [DefaultValue(0)]
        private int type;
        public ContractOrGroupListItem()
        {
            InitializeComponent();
        }

        public ContractOrGroupListItem(int t)
        {
            InitializeComponent();
            type = t;
        }

        private void groupBtn_MouseClick(object sender, DuiMouseEventArgs e)
        {
            if (SelectTab != null)
            {
                SelectTab(2);
            }
        }

        private void cantractBtn_MouseClick(object sender, DuiMouseEventArgs e)
        {
            if (SelectTab != null)
            {
                SelectTab(1);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            if (type == 0)
            {
                type = (int)RowData;
            }
            base.OnLoad(e);
            if (type == 1)
            {
                contractLabel.ForeColor = Color.FromArgb(125, 138, 226);
                contractLine.BackColor = Color.FromArgb(125, 138, 226);
                groupLabel.ForeColor = SystemColors.AppWorkspace;
                groupLine.BackColor = SystemColors.AppWorkspace;
            }
            else if (type == 2)
            {
                contractLabel.ForeColor = SystemColors.AppWorkspace;
                contractLine.BackColor = SystemColors.AppWorkspace;
                groupLine.BackColor = Color.FromArgb(125, 138, 226);
                groupLabel.ForeColor = Color.FromArgb(125, 138, 226);
            }
        }
    }
}
