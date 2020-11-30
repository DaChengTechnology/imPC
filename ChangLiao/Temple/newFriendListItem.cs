using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Controls;

namespace ChangLiao.Temple
{
    public partial class newFriendListItem : DSkinListItemTemplate
    {
        public newFriendListItem()
        {
            InitializeComponent();
        }
        public newFriendListItem(int count)
        {
            InitializeComponent();
            if (count > 0)
            {
                duiButton1.Visible = true;
                duiButton1.Text = count.ToString();
            }
            else
            {
                duiButton1.Visible = false;
            }
        }

        private void newFriendListItem_MouseLeave(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.White;
        }

        private void newFriendListItem_MouseEnter(object sender, MouseEventArgs e)
        {
            if (IsSelected)
            {
                return;
            }
            BackColor = Color.FromArgb(192, 192, 192);
        }

        private void newFriendListItem_IsSelectedChanged(object sender, EventArgs e)
        {
            if (IsSelected)
            {
                BackColor = Color.FromArgb(204, 204, 204);
            }
            else
            {
                BackColor = Color.White;
            }
        }
    }
}
