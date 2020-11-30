using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ChangLiao.DIYListBox.ScrollBar;
using ChangLiao.DIYListBox.ItemCollection;
using ChangLiao.DIYListBox.ListItem;
using EaseMobLib;

namespace ChangLiao.ChildView
{
    [ToolboxBitmap(typeof(System.Windows.Forms.ListBox))]
    public class ChatHistoryListBox : Control
    {
        /// <summary>
        /// 左右边距
        /// </summary>
        public static int PaddingLorR = 10;
        /// <summary>
        /// 上下边距
        /// </summary>
        public static int PaddingUorD = 5;
        /// <summary>
        /// 气泡和头像的上边距
        /// </summary>
        public static int PaddingBandAUP = 5;
        public static int avatarSize = 35;
        public static int timeRowHeight = 30;
        public Font textMessageFont;
        public Font nameFont;
        public static int BubblePadding = 3;                            
        public List<EMMessage> messages;
        private ChatHistoryListboxItemCollection collection;
        private ChatHistoryListItem mouseItem;
        private IContainer components;
        private int rollSize = 50;
        private ChatListScrollBar scrollBar;
        private List<int> itemsHeight;
        private int totalHeight = 0;
        private bool needShowScroll=false;
        private int m_maxBubbleWith;
        public int maxBubbleWith { get=>m_maxBubbleWith; }
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
        private Timer scrollTimer;

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
        private int scrollSpeed = 0;
        public Font timeAndTipsFont { get; set; }
        internal ChatHistoryListboxItemCollection Collection => collection;
        internal ChatHistoryListItem MouseItem => mouseItem;
        public ChatHistoryListBox()
        {
            InitializeComponent();
            m_maxBubbleWith = Width - (avatarSize + PaddingLorR * 3) * 2;
            itemsHeight = new List<int>();
            totalHeight = 0;
            textMessageFont = new Font(SystemFonts.DefaultFont.Name, 13);
            nameFont = new Font(SystemFonts.DefaultFont.Name, 9);
            
            collection = new ChatHistoryListboxItemCollection(this);
            scrollBar = new ChatListScrollBar(this);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.DoubleBuffer, true); // 双缓冲
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true); // 双缓冲
            this.SetStyle(ControlStyles.ResizeRedraw, true); // 调整大小时重绘
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true); // 禁止擦除背景.
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true); // 开启控件透
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
            this.scrollTimer.Interval = 5;
            this.scrollTimer.Tick += new System.EventHandler(this.scrollTimer_Tick);
            // 
            // ChatHistoryListBox
            // 
            this.SizeChanged += new System.EventHandler(this.ChatHistoryListBox_SizeChanged);
            this.Click += new System.EventHandler(this.ChatHistoryListBox_Click);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChatHistoryListBox_MouseDown);
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
                this.scrollBar.Value += scrollSpeed;
                this.Invalidate();
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            if (totalHeight > Height)
            {
                needShowScroll = true;
            }
            else
            {
                needShowScroll = false;
            }
            base.OnMouseEnter(e);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            needShowScroll = false;
            mouseItem = null;
            base.OnMouseLeave(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
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
                if (e.Delta > 0) scrollBar.Value -= RollSize;
                if (e.Delta < 0) scrollBar.Value += RollSize;
            }
            base.OnMouseWheel(e);
        }

        private void ChatHistoryListBox_Click(object sender, EventArgs e)
        {
            if (sender == this)
            {
                MouseEventArgs me = (MouseEventArgs)e;
                if (needShowScroll && me.X > Width - scrollBar.Bounds.Width)
                {
                    if (scrollBar.Bounds.Contains(me.X, me.Y))
                    {
                        scrollBar.MoveSliderToLocation(me.Y);
                    }
                }
            }
        }

        private void ChatHistoryListBox_SizeChanged(object sender, EventArgs e)
        {
            m_maxBubbleWith = Width - (avatarSize + PaddingLorR * 3) * 2;
            itemsHeight.Clear();
        }

        private void ChatHistoryListBox_MouseDown(object sender, MouseEventArgs e)
        {

        }

        public void AddMessages(List<EMMessage> mMessages,bool isNew)
        {
            
        }
    }
}
