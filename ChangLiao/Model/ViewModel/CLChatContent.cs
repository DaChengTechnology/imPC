using DSkin.DirectUI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChangLiao.Model.ViewModel
{
    /// <summary>
    /// 自定义label模型
    /// </summary>
    public partial class CLChatContent
    {
        /// <summary>
        /// 内容数组
        /// </summary>
        private List<ChatContentItem> textarray;
        public List<ChatContentItem> items { get => textarray; }
        public CLChatContent()
        {
            textarray = new List<ChatContentItem>();
        }
        public CLChatContent(string text)
        {
            var t = text;
            textarray = new List<ChatContentItem>();
            MatchCollection matches = Regex.Matches(t, @"\[(.*?)\]");
            if (matches.Count > 0)
            {
                int lastIndex = 0;
                for (int i = 0; i < matches.Count; i++)
                {
                    Match match = matches[i];
                    if (match.Index > lastIndex)
                    {
                        for (int j = lastIndex; j < match.Index; j++)
                        {
                            ChatContentItem cha = new ChatContentItem();
                            cha.type = 1;
                            cha.index = j;
                            cha.ch = t[j];
                            textarray.Add(cha);
                        }
                    }
                    lastIndex += match.Length;
                    EmotionModel emotion = EmotionManager.shard.GetEmotion(match.Value);
                    if (emotion != null)
                    {
                        ChatContentItem cha = new ChatContentItem();
                        cha.type = 2;
                        cha.index = match.Index;
                        cha.emotion = emotion;
                        textarray.Add(cha);
                    }
                    else
                    {
                        for (int j = match.Index; j < match.Length; j++)
                        {
                            ChatContentItem cha = new ChatContentItem();
                            cha.type = 1;
                            cha.index = j;
                            cha.ch = t[j];
                            textarray.Add(cha);
                        }
                    }
                }
                Match a = matches[matches.Count-1];
                if (a.Index + a.Length < t.Length)
                {
                    for (int j = a.Index + a.Length; j < t.Length; j++)
                    {
                        ChatContentItem cha = new ChatContentItem();
                        cha.type = 1;
                        cha.index = j;
                        cha.ch = t[j];
                        textarray.Add(cha);
                    }
                }
            }
            else
            {
                for (int i = 0; i < t.Length; i++)
                {
                    ChatContentItem cha = new ChatContentItem();
                    cha.type = 1;
                    cha.index = i;
                    cha.ch = t[i];
                    textarray.Add(cha);
                }
            }
        }

        public CLChatContent(CLChatContent cLChat)
        {
            textarray = cLChat.items;
        }
        /// <summary>
        /// 插入字符
        /// </summary>
        /// <param name="ch">字符</param>
        /// <param name="index">插入位置</param>
        public void insertChar(char ch, int index)
        {
            if (index >= textarray.Count)
            {
                ChatContentItem cha = new ChatContentItem();
                cha.type = 1;
                cha.ch = ch;
                textarray.Add(cha);
            }
            else
            {
                ChatContentItem cha = new ChatContentItem();
                cha.type = 1;
                cha.ch = ch;
                textarray.Insert(index, cha);
            }
        }
        /// <summary>
        /// 插入表情
        /// </summary>
        /// <param name="emotion">表情</param>
        /// <param name="index">位置</param>
        public void insertEmotion(EmotionModel emotion,int index)
        {
            if (index >= textarray.Count)
            {
                ChatContentItem cha = new ChatContentItem();
                cha.type = 2;
                cha.emotion = emotion;
                textarray.Add(cha);
            }
            else
            {
                if (index > -1)
                {
                    ChatContentItem cha = new ChatContentItem();
                    cha.type = 2;
                    cha.emotion = emotion;
                    textarray.Insert(index, cha);
                }
            }
        }
        /// <summary>
        /// 删除某个位置的内容
        /// </summary>
        /// <param name="index">位置</param>
        public void removeAt(int index)
        {
            if (index >= textarray.Count)
            {
                return;
            }
            if (index < 0)
            {
                return;
            }
            textarray.RemoveAt(index);
        }

        public Size ComputeSize(Graphics graphics, Font font, int maxWidth)
        {
            Size faceSize = /*DuiChar.MeasureChar('啊', font)*/TextRenderer.MeasureText(graphics, "啊",font, new Size(0, 0), TextFormatFlags.NoPadding);
            faceSize.Width = faceSize.Height+2;
            int width = 0;
            Point wp = new Point(0, 0);
            for (int i = 0; i < textarray.Count; i++)
            {
                ChatContentItem emotion = textarray[i];
                if (emotion.type == 2)
                {
                    if (wp.X + faceSize.Width > maxWidth)
                    {
                        if (width < wp.X)
                            width = wp.X;
                        wp.X = 0;
                        wp.Y += faceSize.Height;
                    }
                    else
                    {
                        wp.X += faceSize.Width;
                        if (width < wp.X)
                            width = wp.X;
                    }
                    if (i + 1 == textarray.Count)
                    {
                        if (wp.Y > 0)
                        {
                            wp.X = width;
                        }
                        wp.Y += faceSize.Height;
                    }
                }
                else
                {
                    char ch = emotion.ch;
                    Size size = TextRenderer.MeasureText(graphics, ch.ToString(),font, new Size(0, 0), TextFormatFlags.NoPadding);
                    //size.Width += 1;
                    if (wp.X + size.Width > maxWidth)
                    {
                        if (width < wp.X)
                            width = wp.X;
                        wp.X = 0;
                        wp.Y += size.Height;
                    }else if(ch == '\n')
                    {
                        wp.X = 0;
                        wp.Y += faceSize.Height;
                    }
                    else
                    {
                        wp.X += size.Width;
                        if (width < wp.X)
                            width = wp.X;
                    }
                    if (i + 1 == textarray.Count)
                    {
                        if (wp.Y > 0)
                        {
                            wp.X = width;
                        }
                        else
                        {
                            wp.X += size.Width;
                        }
                        wp.Y += size.Height;
                    }
                }
            }
            return new Size(wp);
        }
        /// <summary>
        /// 获取选择内容
        /// </summary>
        /// <param name="start">开始位置</param>
        /// <param name="lenth">长度</param>
        /// <returns>富文本格式化字符串</returns>
        public string GetSelectionText(int start, int lenth)
        {
            string str = string.Empty;
            for (int i = start; i < start+lenth; i++)
            {
                if (textarray[i].type == 1)
                {
                    str += textarray[i].ch.ToString();
                }
                else
                {
                    str += textarray[i].emotion.id;
                }
            }
            return str;
        }
        /// <summary>
        /// 获取富文本格式化字符串
        /// </summary>
        /// <returns></returns>
        public string GetNormalString()
        {
            string str = string.Empty;
            foreach (var item in textarray)
            {
                if (item.type == 1)
                {
                    str += item.ch.ToString();
                }
                else
                {
                    str += item.emotion.id;
                }
            }
            while (str.Length > 0)
            {
                if (str[0] == '\n' || str[0] == ' ')
                {
                    str = str.Substring(1);
                }
                else
                {
                    break;
                }
            }
            while (str.Length > 0)
            {
                if (str[str.Length - 1] == '\n' || str[str.Length - 1] == ' ')
                {
                    str = str.Substring(0, str.Length - 1);
                }
                else
                {
                    break;
                }
            }
            return str;
        }
    }
}
