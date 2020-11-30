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
using ChangLiao.Model.ViewModel;
using ChangLiao.Util;

namespace ChangLiao.ChildView
{
    /// <summary>
    /// 表情面板
    /// </summary>
    public partial class FacePanl : DuiBaseControl
    {
        public delegate void faceClick(EmotionModel model);
        private List<EmotionModel> emotions;
        private int collect = 10;
        private List<DuiButton> btnList;
        private DSkinHatchBrush backBrush;
        public FacePanl()
        {
            InitializeComponent();
            emotions = EmotionManager.shard.allEmotions;
            Width = 25 * collect;
            Height = emotions.Count % collect > 0 ? (emotions.Count / collect + 1) * 25 : emotions.Count / collect * 25;
            backBrush = new DSkinHatchBrush();
            backBrush.BackgroundColor = Color.FromArgb(204, 204, 204);
        }

        private void FacePanl_Load(object sender, EventArgs e)
        {
            btnList = new List<DuiButton>();
            for (int i = 0; i < emotions.Count; i++)
            {
                var btn = new DuiButton();
                btn.Tag = emotions[i];
                btn.Location = new Point(i % collect * 25 + 1, i / collect * 25 + 1);
                btn.Size = new Size(23, 23);
                btn.BackgroundImage = emotions[i].face;
                btn.BackColor = Color.Transparent;
                btn.BackgroundImageLayout = ImageLayout.Zoom;
                btn.BackgroundRender.BorderWidth = 0;
                btn.Image = emotions[i].face;
                btn.ImageSize = btn.Size;
                btn.BitmapCache = true;
                btn.MouseClick += Btn_MouseClick;
                btn.MouseEnter += Btn_MouseEnter;
                btn.MouseLeave += Btn_MouseLeave;
                this.Controls.Add(btn);
                btnList.Add(btn);
            }
        }

        public void setClick(faceClick c)
        {
            click = c;
        }

        private void Btn_MouseLeave(object sender, EventArgs e)
        {
            var btn = sender as DuiButton;
            if (btn != null)
            {
                btn.BackgroundRender.BackgroundBrush = null;
            }
        }

        private void Btn_MouseEnter(object sender, MouseEventArgs e)
        {
            var btn = sender as DuiButton;
            if (btn != null)
            {
                btn.BackgroundRender.BackgroundBrush = backBrush;
            }
        }

        private void Btn_MouseClick(object sender, DuiMouseEventArgs e)
        {
            var btn = sender as DuiButton;
            if (btn != null)
            {
                EmotionModel model = btn.Tag as EmotionModel;
                if (model != null)
                {
                    click(model);
                }
            }
        }
    }
}
