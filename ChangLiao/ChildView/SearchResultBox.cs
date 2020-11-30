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
using ChangLiao.Model.ViewModel;
using ChangLiao.windows;
using ChangLiao.Temple;

namespace ChangLiao.ChildView
{
    /// <summary>
    /// 搜索结果面板
    /// </summary>
    public partial class SearchResultBox : DSkinUserControl
    {
        private SearchViewModel data;
        private int index;
        public SearchResultBox()
        {
            InitializeComponent();
        }
        
        public void refreshData(SearchViewModel model)
        {
            data = model;
            if (data != null)
            {
                int h = 0;
                if (data.friends.Count > 0)
                {
                    h += data.friends.Count * 40 + 30;
                }
                if (data.groups.Count > 0)
                {
                    h += data.groups.Count * 40 + 30;
                }
                if (data.friends.Count == 0 && data.groups.Count == 0)
                {
                    h = 100;
                }
                MainFrm frm = Application.OpenForms["MainFrm"] as MainFrm;
                if (frm != null)
                {
                    if (frm.Height - Location.Y < h)
                    {
                        h = frm.Height - Location.Y;
                    }
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        duiListBox1.Items.Clear();
                        this.Size = new Size(180, h);
                        if (data.friends.Count > 0)
                        {
                            duiListBox1.Items.Add(new SearchResultListHeaderItem("好友"));
                            foreach(var f in data.friends)
                            {
                                duiListBox1.Items.Add(new SearchResultListItem(f));
                            }
                        }
                        if (data.groups.Count > 0)
                        {
                            duiListBox1.Items.Add(new SearchResultListHeaderItem("群组"));
                            foreach (var f in data.groups)
                            {
                                duiListBox1.Items.Add(new SearchResultListItem(f));
                            }
                        }
                        if(data.friends.Count==0 && data.groups.Count == 0)
                        {
                            duiLabel1.Visible = true;
                        }
                        else
                        {
                            SearchResultListItem i = duiListBox1.Items[1] as SearchResultListItem;
                            i.IsSelected = true;
                            index = 1;
                            duiLabel1.Visible = false;
                        }
                        this.BringToFront();
                        this.Invalidate();
                    }));
                }
            }
        }

        public void down()
        {
            if (index + 1 < duiListBox1.Items.Count)
            {
                SearchResultListItem i = duiListBox1.Items[index+1] as SearchResultListItem;

                if (i == null)
                {
                    if (index + 2 < duiListBox1.Items.Count)
                    {
                        i = duiListBox1.Items[index + 2] as SearchResultListItem;
                        SearchResultListItem ii = duiListBox1.Items[index] as SearchResultListItem;
                        if (i != null)
                        {
                            this.BeginInvoke(new EventHandler((s, e) =>
                            {
                                ii.IsSelected = false;
                                i.IsSelected = true;
                                duiListBox1.Invalidate();
                            }));
                            index += 2;
                        }
                    }
                }
                else
                {
                    SearchResultListItem ii = duiListBox1.Items[index] as SearchResultListItem;
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        ii.IsSelected = false;
                        i.IsSelected = true;
                        duiListBox1.Invalidate();
                    }));
                    index += 1;
                }
            }
        }

        public void up()
        {
            if (index - 1 > 0)
            {
                SearchResultListItem i = duiListBox1.Items[index - 1] as SearchResultListItem;
                if (i == null)
                {
                    if (index - 2 > 0)
                    {
                        i = duiListBox1.Items[index - 2] as SearchResultListItem;
                        if (i != null)
                        {
                            SearchResultListItem ii = duiListBox1.Items[index] as SearchResultListItem;
                            this.BeginInvoke(new EventHandler((s, e) =>
                            {
                                ii.IsSelected = false;
                                i.IsSelected = true;
                                duiListBox1.Invalidate();
                            }));
                            index -= 2;
                        }
                    }
                }
                else
                {
                    SearchResultListItem ii = duiListBox1.Items[index] as SearchResultListItem;
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        ii.IsSelected = false;
                        i.IsSelected = true;
                        duiListBox1.Invalidate();
                    }));
                    index -= 1;
                }
            }
        }
        /// <summary>
        /// 进入聊天
        /// </summary>
        public void EnterChat()
        {
            if (index >= duiListBox1.Items.Count)
            {
                return;
            }
            SearchResultListItem i = duiListBox1.Items[index] as SearchResultListItem;
            if (i != null)
            {
                i.enterChat();
            }
        }

        private void SearchResultBox_SizeChanged(object sender, EventArgs e)
        {
            duiListBox1.Size = Size;
            duiLabel1.Size = Size;
        }
    }
}
