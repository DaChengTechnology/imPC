using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EaseMobLib;
using ChangLiao.DB;
using System.Globalization;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace ChangLiao.Util
{
    class DCUtilTool
    {
        /// <summary>
        /// 获取消息时间字符串
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static string GetMessageTime(EMMessage message)
        {
            if (message == null)
            {
                return string.Empty;
            }
            DateTime now = DateTime.Now;
            DateTime msgTime = DCUtilTool.NewDate(message.timestamp());
            if (msgTime.Year == now.Year && msgTime.Month == now.Month && msgTime.Day == now.Day)
            {
                return msgTime.ToString("HH:mm");
            }
            if (msgTime.Year == now.Year && msgTime.Month == now.Month && msgTime.Day + 1 == now.Day)
            {
                return "昨天 " + msgTime.ToString("HH:mm");
            }
            if (msgTime.Year == now.Year && msgTime.Month == now.Month && msgTime.Day + 2 == now.Day)
            {
                return "前天 " + msgTime.ToString("HH:mm");
            }
            if (msgTime.Year == now.Year)
            {
                return msgTime.ToString("MM-dd HH:mm");
            }
            return msgTime.ToString("yyyy-MM-dd");
        }
        /// <summary>
        /// 获取时间字符串
        /// </summary>
        /// <param name="time"></param>
        /// <returns></returns>
        public static string GetMessageTime(long time)
        {
            DateTime now = DateTime.Now;
            DateTime msgTime = DCUtilTool.NewDate(time);
            if (msgTime.Year == now.Year && msgTime.Month == now.Month && msgTime.Day == now.Day)
            {
                return msgTime.ToString("HH:mm");
            }
            if (msgTime.Year == now.Year && msgTime.Month == now.Month && msgTime.Day + 1 == now.Day)
            {
                return "昨天 " + msgTime.ToString("HH:mm");
            }
            if (msgTime.Year == now.Year && msgTime.Month == now.Month && msgTime.Day + 2 == now.Day)
            {
                return "前天 " + msgTime.ToString("HH:mm");
            }
            if (msgTime.Year == now.Year)
            {
                return msgTime.ToString("MM-dd HH:mm");
            }
            return msgTime.ToString("yyyy-MM-dd");
        }

        /// <summary>
        /// 根据时间戳timestamp（单位毫秒）计算日期
        /// </summary>
        public static DateTime NewDate(long timestamp)
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long tt = dt.Ticks + timestamp * 10000;
            return new DateTime(tt).ToLocalTime();
        }
        /// <summary>
        /// 获取当前时间戳
        /// </summary>
        /// <returns></returns>
        public static long nowTimeSpan()
        {
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long tt = DateTime.UtcNow.Ticks - dt.Ticks;
            tt = tt / 10000;
            return tt;
        }
        /// <summary>
        /// EMChatType转EMConversationType
        /// </summary>
        /// <param name="chatType"></param>
        /// <returns></returns>
        public static EMConversationType GetMConversationType(EMChatType chatType)
        {
            return chatType == EMChatType.GROUP ? EMConversationType.GROUPCHAT : EMConversationType.CHAT;
        }
        /// <summary>
        /// EMConversationType转EMChatType
        /// </summary>
        /// <param name="conversation"></param>
        /// <returns></returns>
        public static EMChatType GetChatType(EMConversation conversation)
        {
            return conversation.conversationType() == EMConversationType.GROUPCHAT ? EMChatType.GROUP : EMChatType.SINGLE;
        }
        /// <summary>
        /// 显示消息预览文字
        /// </summary>
        /// <param name="message">消息</param>
        /// <returns></returns>
        public static string getMessageShowTest(EMMessage message)
        {
            if (message == null)
            {
                return string.Empty;
            }
            var bodys = message.bodies();
            if (bodys.Length < 1)
            {
                return "";
            }
            EMMessageBody body = bodys[0];
            string text = "";
            if (body.type == EMMessageBodyType.TEXT)
            {
                EMTextMessageBody textMessageBody = (EMTextMessageBody)body;
                if (textMessageBody.text().EndsWith("_encode"))
                {
                    var arr = textMessageBody.text().Split('_');
                    text += DCEncrypt.Decrypt(arr[0], DCEncrypt.key);
                }
                else
                {
                    text += textMessageBody.text();
                }
            }
            if(body.type== EMMessageBodyType.VOICE)
            {
                text += "[语音]";
            }
            if(body.type== EMMessageBodyType.VIDEO)
            {
                text += "[视频]";
            }
            if(body.type== EMMessageBodyType.IMAGE)
            {
                text += "[图片]";
            }
            if (body.type == EMMessageBodyType.LOCATION)
            {
                text += "[位置]";
            }
            if(body.type== EMMessageBodyType.FILE)
            {
                text += "[文件]";
            }
            if (message.from() == SettingMenager.shard.userID)
            {
                return text;
            }
            if (message.chatType() == EMChatType.SINGLE)
            {
                var friend = DBHelper.Instance.getFriend(message.from());
                if (friend == null)
                {
                    var stronger = DBHelper.Instance.GetStronger(message.from());
                    if (stronger != null)
                    {
                        text = stronger.nickName + ":" + text;
                    }
                }
                else
                {
                    text = string.IsNullOrEmpty(friend.target_user_nickname) ? friend.friend_self_name : friend.target_user_nickname + ":" + text;
                }
            }
            else
            {
                var groupUser = DBHelper.Instance.GetGroupUser(message.from(), message.conversationId());
                if (groupUser == null)
                {
                    if (DBHelper.Instance.checkFriend(message.from()))
                    {
                        var friend = DBHelper.Instance.getFriend(message.from());
                        if (!string.IsNullOrEmpty(friend.target_user_nickname))
                        {
                            text = friend.target_user_nickname + ":" + text;
                        }
                        else
                        {
                            text = friend.friend_self_name + ":" + text;
                        }

                    }
                    else
                    {
                        var stronger = DBHelper.Instance.GetStronger(message.from());
                        if (stronger != null)
                        {
                            text = stronger.nickName + ":" + text;
                        }
                    }
                }
                else
                {
                    if (DBHelper.Instance.checkFriend(message.from()))
                    {
                        text = groupUser.friend_name + ":" + text;
                    }
                    else if (!string.IsNullOrEmpty(groupUser.group_user_nickname))
                    {
                        text = groupUser.group_user_nickname + ":" + text;
                    }
                    else
                    {
                        text = groupUser.user_name + ":" + text;
                    }
                }
            }
            return text;
        }
        /// <summary>
        /// 转换Image为Icon
        /// </summary>
        /// <param name="image">要转换为图标的Image对象</param>
        /// <param name="nullTonull">当image为null时是否返回null。false则抛空引用异常</param>
        /// <exception cref="ArgumentNullException" />
        public static Icon ConvertToIcon(Image image, bool nullTonull = false)
        {
            if (image == null)
            {
                if (nullTonull) { return null; }
                throw new ArgumentNullException("image");
            }

            using (MemoryStream msImg = new MemoryStream()
                              , msIco = new MemoryStream())
            {
                image.Save(msImg, ImageFormat.Png);

                using (var bin = new BinaryWriter(msIco))
                {
                    //写图标头部
                    bin.Write((short)0);           //0-1保留
                    bin.Write((short)1);           //2-3文件类型。1=图标, 2=光标
                    bin.Write((short)1);           //4-5图像数量（图标可以包含多个图像）

                    bin.Write((byte)image.Width);  //6图标宽度
                    bin.Write((byte)image.Height); //7图标高度
                    bin.Write((byte)0);            //8颜色数（若像素位深>=8，填0。这是显然的，达到8bpp的颜色数最少是256，byte不够表示）
                    bin.Write((byte)0);            //9保留。必须为0
                    bin.Write((short)0);           //10-11调色板
                    bin.Write((short)32);          //12-13位深
                    bin.Write((int)msImg.Length);  //14-17位图数据大小
                    bin.Write(22);                 //18-21位图数据起始字节

                    //写图像数据
                    bin.Write(msImg.ToArray());

                    bin.Flush();
                    bin.Seek(0, SeekOrigin.Begin);
                    return new Icon(msIco);
                }
            }
        }
    }
}
