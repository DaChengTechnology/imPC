using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CCWin.SkinControl;
using System.Windows;
using ChangLiao.DIYListBox.ListItem;
using ChangLiao.DIYListBox.ItemCollection;
using System.Windows.Forms;
using System.Drawing;
using ChangLiao.DIYListBox.ScrollBar;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace ChangLiao.DIYListBox.ListBox
{
    [ToolboxBitmap(typeof(System.Windows.Forms.ListBox))]
    class ConversationListBox:System.Windows.Forms.Control
    {
        public ConversationListItem mouseItem;
        public ConversationListItemCollection collection;
        public Font timeFont;
        public SolidBrush timeBrush;
        public Font nameFont;
        public SolidBrush nameBrush;
        public SolidBrush bkPen;
        [DefaultValue(typeof(int),"60")]
        public int ItemHeight;
        private Timer scrollTimer;
        private IContainer components;
        private ChatListScrollBar ChatListScroll;
        private int scrollSpeed=0;
        private SolidBrush selectBrush;
        [DefaultValue(false)]
        private bool needShowScroll;
        public Color selectColor
        {
            get
            {
                return onSelectColor;
            }
            set
            {
                onSelectColor = value;
                if (selectBrush != null)
                {
                    selectBrush.Dispose();
                    selectBrush = null;
                }
                selectBrush = new SolidBrush(onSelectColor);
            }
        }
        [DefaultValue("Color.Blue")]
        private Color onSelectColor;
        int rollSize = 50;
        /// <summary>
        /// 鼠标所在行数
        /// </summary>
        [DefaultValue(-1)]
        public int mouseIndex;
        /// <summary>
        /// 滚轮每格滚动的像素值
        /// </summary>
        [Category("滚动条")]
        [DefaultValue(50)]
        [Description("滚轮每格滚动的像素值")]
        public int RollSize
        {
            get { return rollSize; }
            set { rollSize = value; }
        }

        bool smoothScroll = true;
        /// <summary>
        /// 是否平滑滚动
        /// </summary>
        [Category("滚动条")]
        [DefaultValue(true)]
        [Description("是否平滑滚动")]
        public bool SmoothScroll
        {
            get { return smoothScroll; }
            set
            {
                if (smoothScroll != value)
                {
                    smoothScroll = value;
                    scrollTimer.Enabled = value;
                }
            }
        }

        internal ConversationListItemCollection OldItemSource => collection;
        public ConversationListBox()
        {
            InitializeComponent();
            ChatListScroll = new ChatListScrollBar(this);
            collection = new ConversationListItemCollection(this);
            timeBrush = new SolidBrush(Color.FromArgb(204, 204, 204));
            timeFont = new Font(SystemFonts.DefaultFont.Name, 10);
            nameBrush = new SolidBrush(Color.FromArgb(89,99,168));
            nameFont = new Font(SystemFonts.DefaultFont.Name, 12);
            bkPen = new SolidBrush(this.BackColor);
            onSelectColor = Color.Blue;
            selectBrush = new SolidBrush(onSelectColor);
            mouseIndex = -1;
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
            this.SetStyle(ControlStyles.ResizeRedraw, true); // 调整大小时重绘
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true); // 开启控件透
        }
        public ConversationListItemCollection Items
        {
            get
            {
                return collection;
            }
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            //调用平滑移动
            if (SmoothScroll)
            {
                if (e.Delta > 0) scrollSpeed -= 10;
                if (e.Delta < 0) scrollSpeed += 10;
            }
            else
            {
                //普通移动
                if (e.Delta > 0) ChatListScroll.Value -= RollSize;
                if (e.Delta < 0) ChatListScroll.Value += RollSize;
            }
            base.OnMouseWheel(e);
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.HighQuality; //高质量
            g.PixelOffsetMode = PixelOffsetMode.HighQuality; //高像素偏移质量
            //最高质量绘制文字
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            try
            {
                for (int i = 0; i < collection.Count; i++)
                {
                    Rectangle bounds = this.GetItemRectangle(i);
                    if (ClientRectangle.Contains(bounds))
                    {
                        if (mouseIndex == i)
                        {
                            g.FillRectangle(selectBrush,bounds);
                        }
                        else
                        {
                            g.FillRectangle(bkPen, bounds);
                        }
                        var convversation = (ConversationListItem)Items[i];
                        g.DrawImage(convversation.avatarImage, new Rectangle(new Point(bounds.X + 5, bounds.Y + 5), new Size(50, 50)));
                        Size size = TextRenderer.MeasureText(convversation.time, timeFont);
                        g.DrawString(convversation.time, timeFont, timeBrush, new PointF(bounds.X + bounds.Width - 5 - size.Width, bounds.Y + 10));
                        Size nameSize = TextRenderer.MeasureText(convversation.name, nameFont);
                        g.DrawString(convversation.name, nameFont, nameBrush, new Rectangle(bounds.X + 55, bounds.Y + 8, bounds.Width - 20 - 50 - size.Width, nameSize.Height));
                        g.DrawString(convversation.lastMessageText, timeFont, timeBrush, new Rectangle(bounds.X + 60, bounds.Y + 40, bounds.Width - 50 - 15, 15));
                    }
                }
                if (mouseItem!=null&&needShowScroll)   //是否绘制滚动条
                    ChatListScroll.ReDrawScroll(g);
            }
            finally
            {
                if (!DesignMode)
                {
                    GC.Collect();
                }
            }
        }

        private Rectangle GetItemRectangle(int i)
        {
            return new Rectangle(0, i * ItemHeight - ChatListScroll.Value, Width, ItemHeight);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            needShowScroll = false;
            this.mouseItem = null;
            this.Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            int index = (e.Y + this.ChatListScroll.Value) / ItemHeight;
            if (index < Items.Count)
            {
                mouseItem = Items[index];
                if (needShowScroll)
                {
                    this.Invalidate();
                }
            }
            base.OnMouseMove(e);
        }

        protected override void OnMouseClick(MouseEventArgs e)
        {
            int newMouse = (e.Y + this.ChatListScroll.Value) / ItemHeight;
            if (needShowScroll && e.X > Width - ChatListScroll.Bounds.Width)
            {
                if (ChatListScroll.Bounds.Contains(e.X, e.Y))
                {
                    ChatListScroll.MoveSliderToLocation(e.Y);
                }
            }else if (newMouse != mouseIndex && collection.Count > newMouse)
            {
                mouseIndex = newMouse;
                this.Invalidate();
            }
            base.OnMouseClick(e);
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.scrollTimer = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // scrollTimer
            // 
            this.scrollTimer.Enabled = true;
            this.scrollTimer.Interval = 15;
            this.scrollTimer.Tick += new System.EventHandler(this.scrollTimer_Tick);
            // 
            // ConversationListBox
            // 
            this.BackColor = System.Drawing.Color.White;
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Click += new System.EventHandler(this.ConversationListBox_Click);
            this.MouseEnter += new System.EventHandler(this.ConversationListBox_MouseEnter);
            this.Move += new System.EventHandler(this.ConversationListBox_Move);
            this.ResumeLayout(false);

        }

        private void scrollTimer_Tick(object sender, EventArgs e)
        {
            if (scrollSpeed > RollSize)//限制速度
            {
                scrollSpeed = RollSize;
            }
            if (scrollSpeed < -RollSize)
            {
                scrollSpeed = -RollSize;
            }

            if (scrollSpeed > 1)//平滑速度
            {
                scrollSpeed -= 1;
            }
            else if (scrollSpeed < -1)
            {
                scrollSpeed += 1;
            }
            else
            {
                scrollSpeed = 0;
            }

            if (scrollSpeed != 0)
            {
                this.ChatListScroll.Value += scrollSpeed;
                this.Invalidate();
            }
        }

        private void ConversationListBox_MouseEnter(object sender, EventArgs e)
        {
            if (collection.Count * ItemHeight > Height)
            {
                needShowScroll = true;
            }
            else
            {
                needShowScroll = false;
            }
            
        }

        private void ConversationListBox_Click(object sender, EventArgs e)
        {

        }

        private void ConversationListBox_Move(object sender, EventArgs e)
        {

        }
    }
}
