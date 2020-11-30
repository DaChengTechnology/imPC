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
    public partial class SearchResultListHeaderItem : DSkinListItemTemplate
    {
        public SearchResultListHeaderItem()
        {
            InitializeComponent();
        }
        public SearchResultListHeaderItem(string title)
        {
            InitializeComponent();
            duiLabel1.Text = title;
        }
    }
}
