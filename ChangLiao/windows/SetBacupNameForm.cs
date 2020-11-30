using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Forms;

namespace ChangLiao.windows
{
    public partial class SetBacupNameForm : DSkinForm
    {
        public SetBacupNameForm()
        {
            InitializeComponent();
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dSkinTextBox1.Text))
            {
                MessageBox.Show("备注不能为空");
                return;
            }
            if(dSkinTextBox1.Text.Length > 18)
            {
                MessageBox.Show("备注不能超过18个字符");
                return;
            }
            if (ChangeBackupName != null)
            {
                ChangeBackupName(userID, dSkinTextBox1.Text);
            }
            this.Close();
        }

        private void dSkinButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
