using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.DirectUI;

namespace ChangLiao.ChildView
{
    class ChatTextBox : DuiTextBox
    {
        /// <summary>
        /// 粘贴图片
        /// </summary>
        public event EventHandler<Image> onPasteImage;
        /// <summary>
        /// 粘贴文字
        /// </summary>
        public event EventHandler<string> onPasteText;
        /// <summary>
        /// 复制消息
        /// </summary>
        public event EventHandler onCopy;
        /// <summary>
        /// 发送消息
        /// </summary>
        public event EventHandler onSend;
        private bool send;

        public override void Copy()
        {
            if (onCopy != null)
            {
                onCopy(this, null);
                return;
            }
            base.Copy();
        }

        public override void Paste()
        {
            if (Clipboard.ContainsImage())
            {
                if (onPasteImage != null)
                {
                    onPasteImage(this, Clipboard.GetImage());
                }
                return;
            }
            if (Clipboard.ContainsText())
            {
                if (onPasteText != null)
                {
                    onPasteText(this, Clipboard.GetText());
                    return;
                }
            }
            base.Paste();
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            send = false;
            if (e.KeyCode == Keys.Enter && !e.Control && !e.Alt)
            {
                send = true;
            }
                base.OnKeyDown(e);
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            if (send)
            {
                if (onSend != null)
                {
                    onSend(this, null);
                }
                e.Handled = true;
            }
            base.OnKeyPress(e);
        }

        protected override void OnKeyUp(KeyEventArgs e)
        {
            base.OnKeyUp(e);
        }
    }
}
