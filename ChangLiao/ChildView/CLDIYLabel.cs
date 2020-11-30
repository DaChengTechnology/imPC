using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.DirectUI;
using ChangLiao.Model.ViewModel;
using System.Drawing;
using System.ComponentModel;

namespace ChangLiao.ChildView
{
    /// <summary>
    /// 自定义展示label
    /// </summary>
    public class CLDIYLabel : DuiLabel
    {
        /// <summary>
        /// 内容
        /// </summary>
        private CLChatContent chatContent;
        public CLChatContent ChatText { get => chatContent; set
            {
                chatContent = value;
                if (Loaded)
                {
                    this.BeginInvoke(() =>
                    {
                        this.Invalidate();
                    });
                }
            } }
        /// <summary>
        /// 选择开始index
        /// </summary>
        [DefaultValue(-1)]
        public int startIndex { get; set; }
        /// <summary>
        /// 选择结束index
        /// </summary>
        [DefaultValue(-1)]
        public int endIndex { get; set; }
        /// <summary>
        /// 鼠标按下状态
        /// </summary>
        [DefaultValue(false)]
        private bool mouseDown;
        public bool isMoseDown { get => mouseDown; }
        public Color selectColor { get; set; }

        public CLDIYLabel():base()
        {
            InitializeComponent();
            endIndex = -1;
            startIndex = -1;
        }

        private void InitializeComponent()
        {
            // 
            // CLDIYLabel
            // 
            this.Size = new System.Drawing.Size(100, 100);
            this.MouseMove += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.CLDIYLabel_MouseMove);
            this.MouseDown += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.CLDIYLabel_MouseDown);
            this.MouseUp += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.CLDIYLabel_MouseUp);
            selectColor = Color.AliceBlue;

        }

        private void CLDIYLabel_MouseDown(object sender, DuiMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (sender == this)
                {
                    mouseDown = true;
                    for (int i = 0; i < Controls.Count; i++)
                    {
                        DuiBaseControl b = Controls[i];
                        if (b.ClientRectangle.Contains(e.Location))
                        {
                            startIndex = i;
                            return;
                        }
                    }
                }
            }
        }

        private void CLDIYLabel_MouseMove(object sender, DuiMouseEventArgs e)
        {
            if (sender == this && mouseDown && e.Button == MouseButtons.Left)
            {
                int currtent = -1;
                for (int i = 0; i < Controls.Count; i++)
                {
                    DuiBaseControl b = Controls[i];
                    if (b.ClientRectangle.Contains(e.Location))
                    {
                        currtent = i;
                        endIndex = currtent;
                        foreach (DuiBaseControl item in Controls)
                        {
                            item.BackColor = Color.Transparent;
                        }
                        if (currtent > startIndex)
                        {
                            for (int j = startIndex; j < currtent + 1; j++)
                            {
                                if (Controls.Count <= j || j < 0) continue;
                                Controls[j].BackColor = selectColor;
                            }
                        }
                        else if (currtent == startIndex)
                        {
                            b.BackColor = selectColor;
                        }
                        else
                        {

                            for (int j = currtent; j < startIndex + 1; j++)
                            {
                                Controls[j].BackColor = selectColor;
                            }
                        }
                        return;
                    }
                }
            }
        }

        private void CLDIYLabel_MouseUp(object sender, DuiMouseEventArgs e)
        {
            if(e.Button== MouseButtons.Left)
            {
                mouseDown = false;
            }
        }

        public void clearSelect()
        {
            endIndex = -1;
            startIndex = -1;
        }

        public void selectAll()
        {
            startIndex = 0;
            endIndex = Controls.Count - 1;
        }

        public string getSelectText()
        {
            if (startIndex > endIndex)
            {
                return chatContent.GetSelectionText(endIndex, startIndex - endIndex + 1);
            }
            if (startIndex == endIndex)
            {
                return chatContent.GetSelectionText(startIndex, 1);
            }
            return chatContent.GetSelectionText(startIndex, endIndex - startIndex + 1);
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            endIndex = -1;
            startIndex = -1;
            Graphics graphics = this.HostControl.CreateGraphics();
            Size faceSize = TextRenderer.MeasureText(graphics,"啊", Font, new Size(0, 0), TextFormatFlags.NoPadding);
            faceSize.Width = faceSize.Height+2;
            Point willpaint = new Point(0, 0);
            this.Controls.Clear();
            if (chatContent == null)
            {
                return;
            }
            foreach (ChatContentItem item in chatContent.items)
            {
                if (item.type == 2)
                {
                    DuiPictureBox face = new DuiPictureBox();
                    face.Location = willpaint;
                    face.Size = faceSize;
                    face.Image = item.emotion.face;
                    face.SizeMode = PictureBoxSizeMode.Zoom;
                    face.BackColor = Color.Transparent;
                    this.Controls.Add(face);
                    if (willpaint.X + faceSize.Width > Width)
                    {
                        willpaint = new Point(0, willpaint.Y + faceSize.Height);
                    }
                    else
                    {
                        willpaint.X += faceSize.Width;
                    }
                }
                else
                {
                    char ch = item.ch;
                    Size charSize = TextRenderer.MeasureText(graphics, ch.ToString(), Font, new Size(0, 0), TextFormatFlags.NoPadding);
                    if (ch != '\n')
                    {
                        DuiLabel label = new DuiLabel();
                        label.ForeColor = ForeColor;
                        label.Font = Font;
                        label.Location = willpaint;
                        label.Size = charSize;
                        label.TextInnerPadding = new Padding(0);
                        label.BackColor = Color.Transparent;
                        label.TextAlign = ContentAlignment.MiddleCenter;
                        label.Margin = new Padding(0, 0, 0, 0);
                        label.Text = ch.ToString();
                        Controls.Add(label);
                    }
                    if (willpaint.X + charSize.Width > Width)
                    {
                        willpaint = new Point(0, willpaint.Y + charSize.Height);
                    }else if(ch == '\n')
                    {
                        willpaint = new Point(0, willpaint.Y + faceSize.Height);
                    }
                    else
                    {
                        willpaint.X += charSize.Width;
                    }
                }
            }
            graphics.Dispose();
        }

        protected override void OnFocusedChanged(EventArgs e)
        {
            base.OnFocusedChanged(e);
            if (!Focused)
            {
                clearSelect();
            }
        }
    }
}
