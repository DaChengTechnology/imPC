using ChangLiao.DB;
using ChangLiao.Util;
using EaseMobLib;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChangLiao.Model.ViewModel
{
    public class MessageModel
    {
        /// <summary>
        /// 是否是时间cell
        /// </summary>
        [DefaultValue(false)]
        public bool isTime { get; set; }
        public long timespan { get; set; }
        /// <summary>
        /// 消息体类型
        /// </summary>
        public EMMessageBodyType bodyType;
        /// <summary>
        /// 消息类型
        /// </summary>
        public EMChatType chatType;
        /// <summary>
        /// 消息ID
        /// </summary>
        public string messageId;
        /// <summary>
        /// 消息
        /// </summary>
        public EMMessage message;
        /// <summary>
        /// 第一个消息体
        /// </summary>
        public EMMessageBody firstbody;
        /// <summary>
        /// 是否是发送方
        /// </summary>
        public bool isSender;
        /// <summary>
        /// 发送方昵称
        /// </summary>
        public string nickName;
        /// <summary>
        /// 发送方头像地址
        /// </summary>
        public string avatarURL;
        public Image avatarImg;
        /// <summary>
        /// 发送方头像
        /// </summary>
        public Image avatarImage { get; set; }
        /// <summary>
        /// 文本消息
        /// </summary>
        public string text;
        /// <summary>
        /// 地址
        /// </summary>
        public string address;
        /// <summary>
        /// 精度
        /// </summary>
        public double latitude;
        /// <summary>
        /// 维度
        /// </summary>
        public double longitude;
        /// <summary>
        /// 失败图像
        /// </summary>
        public Image failImage;
        /// <summary>
        /// 图片大小
        /// </summary>
        public Size imageSize;
        /// <summary>
        /// 缩略图大小
        /// </summary>
        public Size thumbnailImageSize;
        /// <summary>
        /// 图片
        /// </summary>
        public Image image { get; set; }
        /// <summary>
        /// 缩略图
        /// </summary>
        public Image thumbnailImage { get; set; }
        /// <summary>
        /// 语音是否在播放
        /// </summary>
        public bool isMediaPlaying { get; set; }
        /// <summary>
        /// 语音是否已经播放过
        /// </summary>
        public bool isMediaPlayed;
        /// <summary>
        /// 语音时长
        /// </summary>
        public float mediaDuration;
        /// <summary>
        /// 文件图标
        /// </summary>
        public Image fileIcon { get; set; }
        /// <summary>
        /// 文件名
        /// </summary>
        public string filename;
        /// <summary>
        /// 文件大小描述
        /// </summary>
        public string fileSizeDes;
        /// <summary>
        /// 文件大小
        /// </summary>
        public long fileSize;
        /// <summary>
        /// 文件下载进度
        /// </summary>
        public float progress { get; set; }
        /// <summary>
        /// 文件本地路径
        /// </summary>
        public string fileLocalPath { get; set; }
        /// <summary>
        /// 缩略图本地路径
        /// </summary>
        public string thumbnailFileLocalPath { get; set; }
        /// <summary>
        /// 文件远程路径
        /// </summary>
        public string fileURLPath;
        /// <summary>
        /// 缩略图远程路径
        /// </summary>
        public string thumbnailFileURLPath;
        /// <summary>
        /// 是否是名片
        /// </summary>
        public bool isIDCard;
        /// <summary>
        /// 名片要显示的昵称
        /// </summary>
        public string IDCardName { get; set; }
        /// <summary>
        /// 名片的userID
        /// </summary>
        public string IDCardID;
        /// <summary>
        /// 名片的TM号
        /// </summary>
        public string IDCardNum { get; set; }
        /// <summary>
        /// 名片的头像地址
        /// </summary>
        public string IDCardAvatar { get; set; }
        /// <summary>
        /// 是否是表情
        /// </summary>
        public bool isGifFace;
        /// <summary>
        /// 表情URL
        /// </summary>
        public string gitFaceURL;
        /// <summary>
        /// 表情的高度
        /// </summary>
        public int faceH;
        /// <summary>
        /// 表情的宽度
        /// </summary>
        public int faceW;
        /// <summary>
        /// 文件显示名称
        /// </summary>
        public string displayName;
        /// <summary>
        /// 显示提示
        /// </summary>
        [DefaultValue(false)]
        public bool isshowTest { get; set; }
        [DefaultValue(false)]
        public bool isNotLoadData { get; set; }
        [DefaultValue(false)]
        public bool isFriend { get; set; }
        public bool isRead { get; set; }

        public MessageModel(long time)
        {
            isTime = true;
            text = DCUtilTool.GetMessageTime(time);
        }

        public MessageModel(string text)
        {
            isshowTest = true;
            this.text = text;
        }

        public MessageModel(EMMessage mMessage)
        {
            this.message = mMessage;
            if (message.bodies().Length < 1)
            {
                return;
            }
            bodyType = message.bodies()[0].type;
            chatType = message.chatType();
            messageId = message.msgId();
            firstbody = message.bodies()[0];
            isRead = message.isRead();
            if (message.msgDirection() == EMMessageDirection.SEND)
            {
                isSender = true;
            }
            else
            {
                isSender = false;
            }
            nickName = message.from();
            if (bodyType == EMMessageBodyType.TEXT)
            {
                if (message.getAttribute("jpzim_is_big_expression", out isGifFace))
                {
                    if (isGifFace)
                    {
                        message.getAttribute("jpzim_big_expression_path", out gitFaceURL);
                        message.getAttribute("faceH", out faceH);
                        message.getAttribute("faceW", out faceW);
                        if (!string.IsNullOrEmpty(gitFaceURL))
                        {
                            DCWebImageMaanager.shard.downloadImageAsync(gitFaceURL, (image, b) =>
                            {
                                this.image = image;
                                if (faceH < 1 || faceW < 1)
                                {
                                    faceW = image.Width;
                                    faceH = image.Height;
                                }
                            });
                        }
                        text = "[动画表情]";
                        isRead = message.isRead();
                        setupUserInfo();
                        return;
                    }
                }
                string typ;
                if (message.getAttribute("type", out typ))
                {
                    if (typ.Equals("person"))
                    {
                        isIDCard = true;
                        isRead = message.isRead();
                        message.getAttribute("id", out IDCardID);
                        return;
                    }
                }
                EMTextMessageBody body = (EMTextMessageBody)message.bodies()[0];
                text = body.text();
                isRead = message.isRead();
                if (text.EndsWith("_encode"))
                {
                    var arr = text.Split('_');
                    text = DCEncrypt.Decrypt(arr[0], DCEncrypt.key);
                    if (string.IsNullOrEmpty(text))
                    {
                        text = body.text();
                    }
                }
            }
            else if (bodyType == EMMessageBodyType.IMAGE)
            {
                EMImageMessageBody body = (EMImageMessageBody)message.bodies()[0];
                var file = body.localPath();
                if (!string.IsNullOrEmpty(body.localPath()) && File.Exists(body.localPath()))
                {
                    image = new Bitmap(body.localPath());
                }
                fileLocalPath = body.localPath();
                thumbnailFileLocalPath = body.thumbnailLocalPath();
                fileURLPath = body.remotePath();
                thumbnailFileURLPath = body.thumbnailRemotePath();
                fileSize = body.fileLength();
                imageSize = new Size(Convert.ToInt32(body.size().mWidth), Convert.ToInt32(body.size().mHeight));
                thumbnailImageSize = new Size(Convert.ToInt32(body.thumbnailSize().mWidth), Convert.ToInt32(body.thumbnailSize().mWidth));
            }
            else if (bodyType == EMMessageBodyType.VOICE)
            {
                EMVoiceMessageBody body = (EMVoiceMessageBody)message.bodies()[0];
                fileLocalPath = body.localPath();
                fileURLPath = body.remotePath();
                fileSize = body.fileLength();
                mediaDuration = body.duration();
            }
            else if (bodyType == EMMessageBodyType.VIDEO)
            {
                EMVideoMessageBody body = (EMVideoMessageBody)message.bodies()[0];
                fileLocalPath = body.localPath();
                thumbnailFileLocalPath = body.thumbnailLocalPath();
                fileURLPath = body.remotePath();
                thumbnailFileURLPath = body.thumbnailRemotePath();
                fileSize = body.fileLength();
                if (string.IsNullOrEmpty(thumbnailFileLocalPath) && File.Exists(body.thumbnailLocalPath()))
                {
                    thumbnailImage = Image.FromFile(thumbnailFileLocalPath);
                }
                thumbnailImageSize = new Size(Convert.ToInt32(body.size().mWidth), Convert.ToInt32(body.size().mHeight));
            }
            else
            {
                if (bodyType == EMMessageBodyType.COMMAND)
                {
                    return;
                }
                if (bodyType == EMMessageBodyType.LOCATION)
                {
                    EMLocationMessageBody body1 = firstbody as EMLocationMessageBody;
                    latitude = body1.latitude();
                    longitude = body1.longitude();
                    address = body1.address();
                    return;
                }
                EMFileMessageBody body = (EMFileMessageBody)message.bodies()[0];
                fileLocalPath = body.localPath();
                fileURLPath = body.remotePath();
                fileSize = body.fileLength();
                displayName = body.displayName();
                fileSizeDes = getFileSize();
            }
            isRead = message.isRead();
            setupUserInfo();
        }

        private string getFileSize()
        {
            if (fileSize < 1024)
            {
                return string.Format("{0:0.,}B", fileSize);
            }
            else if (fileSize < 1024)
            {
                return string.Format("{0:0.00}KB", (float)fileSize / 1024);
            }
            return string.Format("{0:0.00}MB", (float)fileSize / 1024 / 1024);
        }

        private void setupUserInfo()
        {
            if (isSender)
            {
                avatarURL = SettingMenager.shard.avatar;
                DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                {
                    avatarImage = image;
                    if (avatarImage == null)
                    {
                        avatarImage = Properties.Resources.moren;
                    }
                });
                return;
            }
            if (message.chatType() == EMChatType.SINGLE)
            {
                using (var friend = DBHelper.Instance.getFriend(message.conversationId()))
                {
                    if (friend != null)
                    {
                        nickName = string.IsNullOrEmpty(friend.target_user_nickname) ? friend.friend_self_name : friend.target_user_nickname;
                        avatarURL = friend.portrait;
                        DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                        {
                            avatarImage = image;
                        });
                    }
                    else
                    {
                        using (var stonger = DBHelper.Instance.GetStronger(message.conversationId()))
                        {
                            if (stonger != null)
                            {
                                nickName = stonger.nickName;
                                avatarURL = stonger.avatar;
                                DCWebImageMaanager.shard.downloadImageAsync(stonger.avatar, (image, b) =>
                                {
                                    avatarImage = image;
                                });
                            }
                            else
                            {
                                isNotLoadData = true;
                                avatarImage = Properties.Resources.moren;
                                UserInfoMenager.shard.GetUser(message.from(), (data) =>
                                {
                                    avatarURL = data.portrait;
                                    DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                                    {
                                        avatarImage = image;
                                    });
                                });
                            }
                        }
                    }
                }
            }
            else
            {
                using (var group = DBHelper.Instance.GetGroupUser(message.from(), message.conversationId()))
                {
                    if (group != null)
                    {
                        if (DBHelper.Instance.checkFriend(message.from()))
                        {
                            nickName = string.IsNullOrEmpty(group.friend_name) ? (string.IsNullOrEmpty(group.group_user_nickname) ? group.user_name : group.group_user_nickname) : group.friend_name;
                        }
                        else
                        {
                            nickName = string.IsNullOrEmpty(group.group_user_nickname) ? group.user_name : group.group_user_nickname;
                        }
                        avatarURL = group.avatar;
                        DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                        {
                            avatarImage = image;
                            if (avatarImage == null)
                            {
                                avatarImage = Properties.Resources.moren;
                            }
                        });
                    }
                    else
                    {
                        using (var gt = DBHelper.Instance.GetStronger(message.from(), message.conversationId()))
                        {
                            if (gt != null)
                            {
                                nickName = gt.nickName;
                                avatarURL = gt.avatar;
                                DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                                {
                                    avatarImage = image;
                                    if (avatarImage == null)
                                    {
                                        avatarImage = Properties.Resources.moren;
                                    }
                                });
                            }
                            else
                            {
                                isNotLoadData = true;
                                avatarImage = Properties.Resources.moren;
                                UserInfoMenager.shard.getGroupUser(message.conversationId(), message.from(), (data) =>
                                {
                                    nickName = string.IsNullOrEmpty(data.group_user_nickname) ? data.user_name : data.group_user_nickname;
                                    avatarURL = data.portrait;
                                    DCWebImageMaanager.shard.downloadImageAsync(avatarURL, (image, b) =>
                                    {
                                        avatarImage = image;
                                        if (avatarImage == null)
                                        {
                                            avatarImage = Properties.Resources.moren;
                                        }
                                    });
                                });
                            }
                        }
                    }
                }
            }
        }
    }
}
