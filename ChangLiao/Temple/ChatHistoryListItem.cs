using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using ChangLiao.Ease;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSkin.Controls;
using EaseMobLib;
using ChangLiao.Util;
using ChangLiao.DB;
using System.Windows.Forms;
using ChangLiao.ChildView;
using ChangLiao.Model.ViewModel;
using DSkin.DirectUI;
using System.IO;
using System.Threading;

namespace ChangLiao.Temple
{
    public partial class ChatHistoryListItem : DSkinListItemTemplate
    {
        public MessageModel model;
        private ChatPanl panl;
        public int bubbleX { get => this.BubbleControl.LocationToScreen.X; }
        public int bubbleY { get => this.BubbleControl.LocationToScreen.Y; }
        public int headX { get => this.headBtn.LocationToScreen.X; }
        public int headY { get => this.headBtn.LocationToScreen.Y; }
        public bool noSupport { get; set; }
        public CLDIYLabel textMSGLable { get => textMessageLabel; }
        public ChatHistoryListItem()
        {
            InitializeComponent();
        }
        public ChatHistoryListItem(MessageModel message, ChatPanl p)
        {
            model = message;
            panl = p;
            InitializeComponent();
            if (model.isTime)
            {
                setupTimeOrTips();
                loadTimeOrTips();
            }
            else if (model.isshowTest)
            {
                setupTimeOrTips();
                loadTimeOrTips();
            }
            else
            {
                if (model.bodyType == EMMessageBodyType.TEXT)
                {
                    if (model.isGifFace)
                    {
                        setupGifFace();
                        loadGifFace();
                    }
                    else if (model.isIDCard)
                    {
                        setupIDCard();
                        //loadIDCard();
                        loadNosupport();
                    }
                    else
                    {
                        setupText();
                        loadText();
                    }
                }
                else if (model.bodyType == EMMessageBodyType.IMAGE)
                {
                    setupImage();
                    loadImaage();
                }
                else if (model.bodyType == EMMessageBodyType.VOICE)
                {
                    setupVoice();
                    loadVoice();
                }
                else if (model.bodyType == EMMessageBodyType.VIDEO)
                {
                    setupVideo();
                    loadVideo();
                }
                else if (model.bodyType == EMMessageBodyType.LOCATION)
                {
                    setupLocation();
                    //loadLocation();
                    loadNosupport();
                }
                else
                {
                    setupFile();
                    loadFile();
                }
            }
        }

        private void setupTimeOrTips()
        {
            timePanl.Visible = true;
            timeLabel.Text = model.text;
            timeLabel.Font = panl.timeFont;
            headBtn.Visible = false;
            nameLabel.Visible = false;
            BubbleControl.Visible = false;
            loaddingPicture.Visible = false;
            if (Height != ChatPanl.timeRowHeight)
            {
                Height = ChatPanl.timeRowHeight;
            }
        }

        private void loadTimeOrTips()
        {
            if (panl != null)
            {
                Width = panl.Width;
                Graphics graphics = panl.CreateGraphics();
                timePanl.BackColor = Color.FromArgb(230, 230, 230);
                Size size = graphics.MeasureString(model.text, panl.timeFont).ToSize();
                timePanl.Location = new Point((Width - 6 - size.Width) / 2, 3);
                timePanl.Size = new Size(size.Width + 6, size.Height + 6);
                timeLabel.Location = new Point(3, 3);
                timeLabel.Size = size;
                this.Invalidate();
            }
        }

        private void setupGifFace()
        {
            if (!string.IsNullOrEmpty(model.gitFaceURL))
            {
                if (model.image != null)
                {
                    imagePictureBox.Image = model.image;
                }
                else
                {
                    DCWebImageMaanager.shard.downloadImageAsync(model.gitFaceURL, (imagePath, b) =>
                    {
                        model.image = imagePath;
                        imagePictureBox.BeginInvoke(() =>
                        {
                            imagePictureBox.Image = model.image;
                        });
                    });
                }
            }
            if (model.message.status() == EMMessageStatus.DELIVERING || model.message.status() == EMMessageStatus.FAIL)
            {
                loaddingPicture.Visible = true;
            }
            else
            {
                loaddingPicture.Visible = false;
            }
            uploadPrograss.Visible = false;
            timePanl.Visible = false;
            headBtn.Visible = true;
            textLabel.Visible = false;
            durentLabel.Visible = false;
            voicePictureBox.Visible = false;
            filePanl.Visible = false;
            fileLine.Visible = false;
            BubbleControl.Visible = true;
            idCardLabel.Visible = false;
            textMessageLabel.Visible = false;
            imagePictureBox.MouseDown += ImagePictureBox_MouseDown;
            BubbleControl.BackColor = Color.Transparent;
            if (model.isSender)
            {
                nameLabel.Visible = false;
            }
            else
            {
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Visible = true;
                    nameLabel.Text = model.nickName;
                }
                else
                {
                    nameLabel.Visible = false;
                }
            }
        }

        private void loadGifFace()
        {
            if (Parent != null && panl != null)
            {
                Width = Parent.InnerDuiControl.Width;
                int h = 100;
                var w = 100;
                if (model.faceH > 0 && model.faceW > 0)
                {
                    try
                    {
                        w = Convert.ToInt32((double)model.faceW / (double)model.faceH * 100);
                    }
                    catch { }
                }
                else
                {
                    if (model.imageSize.Width > 0 && model.imageSize.Height > 0)
                    {
                        w = Convert.ToInt32((double)model.imageSize.Width / (double)model.imageSize.Height * 100);
                    }
                    else if (model.image != null)
                    {
                        w = Convert.ToInt32((double)model.image.Width / (double)model.image.Height * 100);
                    }
                }
                if (model.isSender)
                {
                    headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                    headBtn.Location = new Point(Width - ChatPanl.PaddingLorR - ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                    if (w + 10 > panl.maxBubbleWith)
                    {
                        try
                        {
                            h = Convert.ToInt32((double)model.faceH / (double)model.faceW * ((double)panl.maxBubbleWith - 10));
                        }
                        catch { }
                        BubbleControl.Location = new Point(Width - 10 - ChatPanl.PaddingLorR - ChatPanl.avatarSize - (panl.maxBubbleWith - 10), ChatPanl.PaddingUorD);
                        BubbleControl.Size = new Size(panl.maxBubbleWith, h + 10);
                        imagePictureBox.Location = new Point(5, 5);
                        imagePictureBox.Size = new Size((panl.maxBubbleWith - 10), h);
                    }
                    else
                    {
                        BubbleControl.Location = new Point(Width - 10 - ChatPanl.PaddingLorR - ChatPanl.avatarSize - w, ChatPanl.PaddingUorD);
                        BubbleControl.Size = new Size(w + 10, 110);
                        imagePictureBox.Location = new Point(5, 5);
                        imagePictureBox.Size = new Size(w, 100);
                    }
                    h = h + 10 + ChatPanl.PaddingUorD * 2;
                    if (h != Height)
                    {
                        Height = h;
                    }
                    loaddingPicture.Size = new Size(20, 20);
                    loaddingPicture.Location = new Point(Width - 10 - ChatPanl.PaddingLorR - ChatPanl.avatarSize - BubbleControl.Width - 20, (h - 20) / 2);
                }
                else
                {
                    headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                    headBtn.Location = new Point(ChatPanl.PaddingLorR, ChatPanl.PaddingUorD);
                    if (w + 10 > panl.maxBubbleWith)
                    {
                        try { h = Convert.ToInt32((double)model.faceH / (double)model.faceW * ((double)panl.maxBubbleWith - 10)); } catch { }
                        if (model.chatType == EMChatType.GROUP)
                        {
                            nameLabel.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, 0);
                            BubbleControl.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, ChatPanl.PaddingUorD * 2);
                        }
                        else
                        {
                            BubbleControl.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                        }
                        BubbleControl.Size = new Size(panl.maxBubbleWith, h + 10);
                        imagePictureBox.Location = new Point(5, 5);
                        imagePictureBox.Size = new Size((panl.maxBubbleWith - 10), h);
                    }
                    else
                    {
                        if (model.chatType == EMChatType.GROUP)
                        {
                            nameLabel.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, 0);
                            BubbleControl.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, ChatPanl.PaddingUorD * 2);
                        }
                        else
                        {
                            BubbleControl.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                        }
                        BubbleControl.Size = new Size(w + 10, 110);
                        imagePictureBox.Location = new Point(5, 5);
                        imagePictureBox.Size = new Size(w, 100);
                    }
                    h = h + 10 + ChatPanl.PaddingUorD * 2;
                    if (h != Height)
                    {
                        Height = h;
                    }
                    loaddingPicture.Size = new Size(20, 20);
                    loaddingPicture.Location = new Point(10 + ChatPanl.PaddingLorR + ChatPanl.avatarSize + BubbleControl.Width + 20, (h - 20) / 2);
                }
            }
        }

        private void setupIDCard()
        {
            if (model.message.status() == EMMessageStatus.DELIVERING || model.message.status() == EMMessageStatus.FAIL)
            {
                loaddingPicture.Visible = true;
            }
            else
            {
                loaddingPicture.Visible = false;
            }
            uploadPrograss.Visible = false;
            if (DBHelper.Instance.checkFriend(model.IDCardID))
            {
                model.isFriend = true;
                var f = DBHelper.Instance.getFriend(model.IDCardID);
                model.IDCardAvatar = f.portrait;
                model.IDCardNum = f.id_card;
                model.IDCardName = f.friend_self_name;
            }
            else
            {
                var s = DBHelper.Instance.GetStronger(model.IDCardID);
                model.isFriend = false;
                if (s != null)
                {
                    model.IDCardAvatar = s.avatar;
                    model.IDCardName = s.nickName;
                    model.IDCardNum = s.idCard;
                }
                else
                {
                    UserInfoMenager.shard.GetUser(model.IDCardID, (data) =>
                    {
                        model.IDCardName = data.user_name;
                        model.IDCardAvatar = data.portrait;
                        model.IDCardNum = data.id_card;
                    });
                    string nam;
                    model.message.getAttribute("username", out nam);
                    model.IDCardName = nam;
                    model.message.getAttribute("userhead", out nam);
                    model.IDCardAvatar = nam;
                    model.message.getAttribute("usernum", out nam);
                    model.IDCardNum = nam;
                }
            }
            timePanl.Visible = false;
            headBtn.Visible = true;
            textLabel.Visible = true;
            durentLabel.Visible = true;
            voicePictureBox.Visible = false;
            imagePictureBox.Visible = true;
            filePanl.Visible = true;
            fileLine.Visible = true;
            idCardLabel.Visible = true;
            BubbleControl.Visible = true;
            textMessageLabel.Visible = false;
            textLabel.Font = new Font("微软雅黑", 13);
            durentLabel.Font = new Font("微软雅黑", 11);
            textLabel.Text = model.IDCardName;
            durentLabel.Text = String.Format("畅聊号:%s", model.IDCardNum);
            idCardLabel.Text = "个人名片";
            if (model.isSender)
            {
                nameLabel.Visible = false;
            }
            else
            {
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Visible = true;
                    nameLabel.Text = model.nickName;
                }
                else
                {
                    nameLabel.Visible = false;
                }
            }
            DCWebImageMaanager.shard.downloadImageAsync(model.avatarURL, (imagePath, b) =>
            {
                model.avatarImg = imagePath;
                if (model.avatarImg != null)
                {
                    if (headBtn.Loaded)
                    {
                        headBtn.BeginInvoke(() =>
                        {
                            headBtn.BackgroundImage = model.avatarImg;
                        });
                    }
                }
            });
            DCWebImageMaanager.shard.downloadImageAsync(model.IDCardAvatar, (imagePath, b) =>
            {
                model.image = imagePath;
                if (model.image != null)
                {
                    if (imagePictureBox.Loaded)
                    {
                        imagePictureBox.BeginInvoke(() =>
                        {
                            imagePictureBox.Image = model.image;
                            imagePictureBox.Invalidate();
                        });
                    }
                }
            });
        }

        private void loadIDCard()
        {
            BubbleControl.BackColor = Color.FromArgb(204, 204, 204);
            filePanl.BackColor = Color.White;
            fileLine.BackColor = Color.FromArgb(204, 204, 204);
            BubbleControl.BackgroundRender.Radius = 10;
            filePanl.BackgroundRender.Radius = 10;
            if (Parent != null && panl != null)
            {
                if (model.isSender)
                {
                    headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                    headBtn.Location = new Point(Width - ChatPanl.PaddingLorR - ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                    BubbleControl.Size = new Size(260, 100);
                    BubbleControl.Location = new Point(Width - 260 - ChatPanl.PaddingLorR * 2 - ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                    filePanl.Location = new Point(5, 5);
                    filePanl.Size = new Size(250, 90);
                    imagePictureBox.Location = new Point(5 + ChatPanl.PaddingLorR, 5 + ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                    imagePictureBox.Size = new Size(40, 40);
                    Size size = TextRenderer.MeasureText(textLabel.Text, textLabel.Font);
                    textLabel.Location = new Point((BubbleControl.Width - size.Width) / 2 + 5, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP + 10);
                    textLabel.Size = size;
                    size = TextRenderer.MeasureText(durentLabel.Text, durentLabel.Font);
                    durentLabel.Location = new Point((BubbleControl.Width - size.Width) / 2 + 5, textLabel.Location.Y + textLabel.Height + 5);
                    durentLabel.Size = size;
                    size = TextRenderer.MeasureText(idCardLabel.Text, idCardLabel.Font);
                    fileLine.Location = new Point(5, durentLabel.Location.Y + durentLabel.Height + 5);
                    fileLine.Size = new Size(BubbleControl.Size.Width - 10, 1);
                    idCardLabel.Location = new Point((BubbleControl.Width - size.Width) / 2 + 5, fileLine.Location.Y + fileLine.Height + 5);
                    idCardLabel.Size = size;
                    var h = 100 + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP;
                    if (h != Height)
                    {
                        Height = h;
                    }
                    loaddingPicture.Size = new Size(20, 20);
                    loaddingPicture.Location = new Point(Width - 10 - ChatPanl.PaddingLorR - ChatPanl.avatarSize - BubbleControl.Width - 20, (h - 20) / 2);
                }
                else
                {
                    headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                    headBtn.Location = new Point(ChatPanl.PaddingLorR, ChatPanl.PaddingUorD);
                    BubbleControl.Size = new Size(260, 100);
                    if (model.chatType == EMChatType.GROUP)
                    {
                        nameLabel.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingUorD, 0);
                        BubbleControl.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingUorD, ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP);
                    }
                    else
                    {
                        BubbleControl.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingUorD, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                    }
                    filePanl.Location = new Point(5, 5);
                    filePanl.Size = new Size(250, 90);
                    imagePictureBox.Location = new Point(5 + ChatPanl.PaddingLorR, 5 + ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                    imagePictureBox.Size = new Size(40, 40);
                    Size size = TextRenderer.MeasureText(textLabel.Text, textLabel.Font);
                    textLabel.Location = new Point((BubbleControl.Width - size.Width) / 2 + 5, ChatPanl.PaddingUorD + 10 + ChatPanl.PaddingBandAUP);
                    textLabel.Size = size;
                    size = TextRenderer.MeasureText(durentLabel.Text, durentLabel.Font);
                    durentLabel.Location = new Point((BubbleControl.Width - size.Width) / 2 + 5, textLabel.Location.Y + textLabel.Height + 5);
                    durentLabel.Size = size;
                    size = TextRenderer.MeasureText(idCardLabel.Text, idCardLabel.Font);
                    fileLine.Location = new Point(5, durentLabel.Location.Y + durentLabel.Height + 5);
                    fileLine.Size = new Size(BubbleControl.Size.Width - 10, 1);
                    idCardLabel.Location = new Point((BubbleControl.Width - size.Width) / 2 + 5, fileLine.Location.Y + fileLine.Height + 5);
                    idCardLabel.Size = size;
                    var h = 100 + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP;
                    if (h != Height)
                    {
                        Height = h;
                    }
                    loaddingPicture.Size = new Size(20, 20);
                    loaddingPicture.Location = new Point(10 + ChatPanl.PaddingLorR + ChatPanl.avatarSize + BubbleControl.Width + 20, (h - 20) / 2);
                }
            }
        }

        private void setupImage()
        {
            timePanl.Visible = false;
            headBtn.Visible = true;
            textLabel.Visible = false;
            durentLabel.Visible = false;
            voicePictureBox.Visible = false;
            imagePictureBox.Visible = true;
            filePanl.Visible = false;
            fileLine.Visible = false;
            idCardLabel.Visible = false;
            textMessageLabel.Visible = false;
            BubbleControl.Visible = true;
            if (model.message.status() == EMMessageStatus.DELIVERING || model.message.status() == EMMessageStatus.FAIL)
            {
                loaddingPicture.Visible = true;
            }
            else
            {
                loaddingPicture.Visible = false;
            }
            uploadPrograss.Visible = false;
            imagePictureBox.MouseDown += ImagePictureBox_MouseDown;
            if (model.isSender)
            {
                nameLabel.Visible = false;
            }
            else
            {
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Visible = true;
                    nameLabel.Text = model.nickName;
                }
                else
                {
                    nameLabel.Visible = false;
                }
            }
            DCWebImageMaanager.shard.downloadImageAsync(model.avatarURL, (imagePath, b) =>
            {
                model.avatarImg = imagePath;
                if (model.avatarImg != null)
                {
                    headBtn.BeginInvoke(() =>
                    {
                        headBtn.BackgroundImage = model.avatarImg;
                    });
                }
            });
            if (!string.IsNullOrEmpty(model.thumbnailFileLocalPath))
            {
                if (File.Exists(model.thumbnailFileLocalPath))
                {
                    model.thumbnailImage = Image.FromFile(model.thumbnailFileLocalPath);
                }
                if (model.thumbnailImage == null)
                {
                    DownLoadChatFileManager.shard.downloadThumbnailFileAttchment(model.message, (aMessage) =>
                    {
                        model.message = aMessage;
                        model.firstbody = aMessage.bodies()[0];
                        var body = model.message.bodies()[0] as EMImageMessageBody;
                        if (body.thumbnailDownloadStatus() == EMDownloadStatus.SUCCESSED)
                        {
                            model.thumbnailFileLocalPath = body.thumbnailLocalPath();
                            model.thumbnailImage = Image.FromFile(model.thumbnailFileLocalPath);
                            model.thumbnailImageSize = model.thumbnailImage.Size;
                            body.setThumbnailSize(new EMImageMessageBody.Size((double)model.thumbnailImageSize.Width, (double)model.thumbnailImageSize.Height));
                            EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(model.message.conversationId(), DCUtilTool.GetMConversationType(model.message.chatType()), true);
                            conversation.updateMessage(model.message);
                            if (model.thumbnailImage != null)
                            {
                                if (panl.IsHandleCreated)
                                {
                                    panl.BeginInvoke(new EventHandler((s, e) =>
                                    {
                                        imagePictureBox.Image = model.thumbnailImage;
                                        loadImaage();
                                        this.Invalidate();
                                    }));
                                }
                            }
                        }
                    });
                }
                else
                {
                    imagePictureBox.Image = model.thumbnailImage;
                    loadImaage();
                }
            }
            else
            {
                DownLoadChatFileManager.shard.downloadThumbnailFileAttchment(model.message, (aMessage) =>
                {
                    model.message = aMessage;
                    model.firstbody = aMessage.bodies()[0];
                    var body = model.message.bodies()[0] as EMImageMessageBody;
                    if (body.thumbnailDownloadStatus() == EMDownloadStatus.SUCCESSED)
                    {
                        model.thumbnailFileLocalPath = body.thumbnailLocalPath();
                        model.thumbnailImage = Image.FromFile(model.thumbnailFileLocalPath);
                        model.thumbnailImageSize = model.thumbnailImage.Size;
                        body.setThumbnailSize(new EMImageMessageBody.Size((double)model.thumbnailImageSize.Width, (double)model.thumbnailImageSize.Height));
                        EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(model.message.conversationId(), DCUtilTool.GetMConversationType(model.message.chatType()), true);
                        conversation.updateMessage(model.message);
                        if (model.thumbnailImage != null)
                        {
                            if (panl.IsHandleCreated)
                            {
                                panl.BeginInvoke(new EventHandler((s, e) =>
                                {
                                    imagePictureBox.Image = model.thumbnailImage;
                                    loadImaage();
                                    this.Invalidate();
                                }));
                            }
                        }
                    }
                });
            }
            if (string.IsNullOrEmpty(model.fileLocalPath))
            {
                DownLoadChatFileManager.shard.downloadFileAttchment(model.message, (p) => {
                    model.message.setStatus(EMMessageStatus.DELIVERING);
                    updateProgress(Convert.ToInt32(p));
                }, (aMessage) =>
                {
                    model.message = aMessage;
                    model.firstbody = aMessage.bodies()[0];
                    var body = model.message.bodies()[0] as EMImageMessageBody;
                    if (body.downloadStatus() == EMDownloadStatus.SUCCESSED)
                    {
                        model.fileLocalPath = body.localPath();
                    }
                });
            }
            else if (!File.Exists(model.fileLocalPath))
            {
                DownLoadChatFileManager.shard.downloadFileAttchment(model.message, (p) => {
                    model.message.setStatus(EMMessageStatus.DELIVERING);
                    updateProgress(Convert.ToInt32(p));
                }, (aMessage) =>
                {
                    model.message = aMessage;
                    model.firstbody = aMessage.bodies()[0];
                    var body = model.message.bodies()[0] as EMImageMessageBody;
                    if (body.downloadStatus() == EMDownloadStatus.SUCCESSED)
                    {
                        model.fileLocalPath = body.localPath();
                    }
                });
            }
        }

        private void ImagePictureBox_MouseDown(object sender, DuiMouseEventArgs e)
        {

            if (e.Button == MouseButtons.Left)
            {
                EventHandler h = messageClick;
                if (h != null)
                {
                    h(this, e);
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                EventHandler h = messageRightClick;
                if (h != null)
                {
                    h(this, e);
                }
            }
        }

        private void loadImaage()
        {
            BubbleControl.BackgroundRender.Radius = 10;
            imagePictureBox.BackgroundRender.Radius = 10;
            if (model.isSender)
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(Width - ChatPanl.PaddingLorR - ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                int h = 100;
                int w = 100;
                if (model.thumbnailImage != null)
                {
                    model.thumbnailImageSize = model.thumbnailImage.Size;
                    if (model.thumbnailImageSize.Width > 0 && model.thumbnailImageSize.Height > 0)
                    {
                        w = (int)((double)model.thumbnailImageSize.Width / (double)model.thumbnailImageSize.Height * (double)h);
                        if (w + 10 > panl.maxBubbleWith)
                        {
                            w = panl.maxBubbleWith - 10;
                            h = (int)((double)model.thumbnailImageSize.Height / (double)model.thumbnailImageSize.Width * (double)w);
                        }
                    }
                    var body = model.firstbody as EMImageMessageBody;
                    body.setThumbnailSize(new EMImageMessageBody.Size((double)model.thumbnailImageSize.Width, (double)model.thumbnailImageSize.Height));
                    EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(model.message.conversationId(), DCUtilTool.GetMConversationType(model.message.chatType()), true);
                    conversation.updateMessage(model.message);
                }
                else if (model.thumbnailImageSize.Width > 0 && model.thumbnailImageSize.Height > 0)
                {
                    w = (int)((double)model.thumbnailImageSize.Width / (double)model.thumbnailImageSize.Height * (double)h);
                    if (w + 10 > panl.maxBubbleWith)
                    {
                        w = panl.maxBubbleWith - 10;
                        h = (int)((double)model.thumbnailImageSize.Height / (double)model.thumbnailImageSize.Width * (double)w);
                    }
                }
                BubbleControl.Size = new Size(w + 10, h + 10);
                BubbleControl.Location = new Point(Width - w - 10 - ChatPanl.PaddingLorR * 2 - ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                BubbleControl.BackColor = Color.FromArgb(255, 239, 209);
                imagePictureBox.Location = new Point(5, 5);
                imagePictureBox.Size = new Size(w, h);
                uploadPrograss.Location = new Point(5, 5);
                uploadPrograss.Size = new Size(w, h);
                h = h + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP;
                if (h != Height)
                {
                    Height = h;
                }
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(Width - 10 - ChatPanl.PaddingLorR - ChatPanl.avatarSize - BubbleControl.Width - 20, (h - 20) / 2);
            }
            else
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(ChatPanl.PaddingLorR, ChatPanl.PaddingUorD);
                int h = 100;
                int w = 100;
                if (model.thumbnailImage != null)
                {
                    model.thumbnailImageSize = model.thumbnailImage.Size;
                    if (model.thumbnailImageSize.Width > 0 && model.thumbnailImageSize.Height > 0)
                    {
                        w = (int)((double)model.thumbnailImageSize.Width / (double)model.thumbnailImageSize.Height * (double)h);
                        if (w + 10 > panl.maxBubbleWith)
                        {
                            w = panl.maxBubbleWith - 10;
                            h = (int)((double)model.thumbnailImageSize.Height / (double)model.thumbnailImageSize.Width * (double)w);
                        }
                    }
                    var body = model.firstbody as EMImageMessageBody;
                    body.setThumbnailSize(new EMImageMessageBody.Size((double)model.thumbnailImageSize.Width, (double)model.thumbnailImageSize.Height));
                    EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(model.message.conversationId(), DCUtilTool.GetMConversationType(model.message.chatType()), true);
                    conversation.updateMessage(model.message);
                }
                else if (model.thumbnailImageSize.Width > 0 && model.thumbnailImageSize.Height > 0)
                {
                    w = (int)((double)model.thumbnailImageSize.Width / (double)model.thumbnailImageSize.Height * (double)h);
                    if (w + 10 > panl.maxBubbleWith)
                    {
                        w = panl.maxBubbleWith - 10;
                        h = (int)((double)model.thumbnailImageSize.Height / (double)model.thumbnailImageSize.Width * (double)w);
                    }
                }
                BubbleControl.Size = new Size(w + 10, h + 10);
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, 0);
                    BubbleControl.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP);
                }
                else
                {
                    BubbleControl.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                }
                BubbleControl.BackColor = Color.White;
                imagePictureBox.Location = new Point(5, 5);
                imagePictureBox.Size = new Size(w, h);
                uploadPrograss.Location = new Point(5, 5);
                uploadPrograss.Size = new Size(w, h);
                h = h + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP;
                if (h != Height)
                {
                    Height = h;
                }
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(10 + ChatPanl.PaddingLorR + ChatPanl.avatarSize + BubbleControl.Width + 20, (h - 20) / 2);
            }
            if (model.thumbnailImage != null)
            {
                imagePictureBox.Image = model.thumbnailImage;
            }
        }

        private void setupVoice()
        {
            if (model.message.status() == EMMessageStatus.DELIVERING || model.message.status() == EMMessageStatus.FAIL)
            {
                loaddingPicture.Visible = true;
            }
            else
            {
                loaddingPicture.Visible = false;
            }
            uploadPrograss.Visible = false;
            timePanl.Visible = false;
            headBtn.Visible = true;
            textLabel.Visible = false;
            durentLabel.Visible = true;
            imagePictureBox.Visible = true;
            filePanl.Visible = false;
            fileLine.Visible = false;
            idCardLabel.Visible = false;
            textMessageLabel.Visible = false;
            BubbleControl.Visible = true;
            durentLabel.Font = new Font("微软雅黑", 8);
            durentLabel.Text = string.Format("{0:0.0,.}″", model.mediaDuration);
            if (model.isSender)
            {
                nameLabel.Visible = false;
                BubbleControl.BackColor = Color.FromArgb(255, 239, 209);
            }
            else
            {
                BubbleControl.BackColor = Color.White;
                if (model.isRead)
                {
                    voicePictureBox.Visible = false;
                }
                else
                {
                    voicePictureBox.Visible = true;
                }
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Visible = true;
                    nameLabel.Text = model.nickName;
                }
                else
                {
                    nameLabel.Visible = false;
                }
            }
        }

        private void loadVoice()
        {
            BubbleControl.BackgroundRender.Radius = 10;
            voicePictureBox.BackgroundImage = null;
            voicePictureBox.Image = Properties.Resources.dian;
            if (model.isSender)
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(Width - ChatPanl.PaddingLorR - ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                BubbleControl.Location = new Point(Width - 100 - 10 - ChatPanl.PaddingLorR * 2 - ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                BubbleControl.Size = new Size(100, 35);
                imagePictureBox.Location = new Point(100 - 5 - 35, 5);
                imagePictureBox.Size = new Size(35, 25);
                imagePictureBox.Image = Properties.Resources.voice_send_3;
                Size size = TextRenderer.MeasureText(durentLabel.Text, durentLabel.Font);
                durentLabel.Location = new Point(100 - imagePictureBox.Location.X - 5 - size.Width, (35 - size.Height) / 2);
                durentLabel.Size = size;
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(Width - 10 - ChatPanl.PaddingLorR - ChatPanl.avatarSize - BubbleControl.Width - 20, (ChatPanl.PaddingUorD * 4 + ChatPanl.avatarSize - 20) / 2);
            }
            else
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(ChatPanl.PaddingLorR, ChatPanl.PaddingUorD);
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, 0);
                    BubbleControl.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingLorR, ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP);
                }
                else
                {
                    BubbleControl.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingLorR, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                }
                BubbleControl.Size = new Size(100, 35);
                imagePictureBox.Location = new Point(5, 5);
                imagePictureBox.Size = new Size(35, 25);
                imagePictureBox.Image = Properties.Resources.voice_recive_3;
                Size size = TextRenderer.MeasureText(durentLabel.Text, durentLabel.Font);
                durentLabel.Location = new Point(45, (35 - size.Height) / 2);
                durentLabel.Size = size;
                voicePictureBox.Location = new Point(87, 3);
                voicePictureBox.Size = new Size(10, 10);
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(10 + ChatPanl.PaddingLorR + ChatPanl.avatarSize + BubbleControl.Width + 20, (ChatPanl.PaddingUorD * 4 + ChatPanl.avatarSize - 20) / 2);
            }
            if (Height != ChatPanl.PaddingUorD * 4 + ChatPanl.avatarSize)
            {
                Height = ChatPanl.PaddingUorD * 4 + ChatPanl.avatarSize;
            }
        }

        private void setupVideo()
        {
            if (model.message.status() == EMMessageStatus.DELIVERING || model.message.status() == EMMessageStatus.FAIL)
            {
                loaddingPicture.Visible = true;
            }
            else
            {
                loaddingPicture.Visible = false;
            }
            uploadPrograss.Visible = false;
            timePanl.Visible = false;
            headBtn.Visible = true;
            textLabel.Visible = false;
            durentLabel.Visible = false;
            voicePictureBox.Visible = true;
            imagePictureBox.Visible = true;
            filePanl.Visible = false;
            fileLine.Visible = false;
            idCardLabel.Visible = false;
            textMessageLabel.Visible = false;
            BubbleControl.Visible = true;
            imagePictureBox.MouseDown += ImagePictureBox_MouseDown;
            voicePictureBox.MouseDown += VoicePictureBox_MouseDown;
            if (model.isSender)
            {
                nameLabel.Visible = false;
            }
            else
            {
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Visible = true;
                    nameLabel.Text = model.nickName;
                }
                else
                {
                    nameLabel.Visible = false;
                }
            }
            DCWebImageMaanager.shard.downloadImageAsync(model.avatarURL, (imagePath, b) =>
            {
                model.avatarImg = imagePath;
                if (model.avatarImg != null)
                {
                    headBtn.BackgroundImage = model.avatarImg;
                }
            });
            if (!string.IsNullOrEmpty(model.thumbnailFileLocalPath))
            {
                if (File.Exists(model.thumbnailFileLocalPath))
                {
                    model.thumbnailImage = Image.FromFile(model.thumbnailFileLocalPath);
                    if (model.thumbnailImage == null)
                    {
                        DownLoadChatFileManager.shard.downloadThumbnailFileAttchment(model.message, (aMessage) =>
                        {
                            model.message = aMessage;
                            model.firstbody = aMessage.bodies()[0];
                            var body = model.message.bodies()[0] as EMVideoMessageBody;
                            model.thumbnailFileLocalPath = body.thumbnailLocalPath();
                            model.thumbnailImage = Image.FromFile(model.thumbnailFileLocalPath);
                            model.thumbnailImageSize = model.thumbnailImage.Size;
                            body.setSize(new EMVideoMessageBody.Size((double)model.thumbnailImageSize.Width, (double)model.thumbnailImageSize.Height));
                            EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(model.message.conversationId(), DCUtilTool.GetMConversationType(model.message.chatType()), true);
                            conversation.updateMessage(model.message);
                            if (model.thumbnailImage != null)
                            {
                                panl.BeginInvoke(new EventHandler((s, e) =>
                                {
                                    imagePictureBox.Image = model.thumbnailImage;
                                    loadVideo();
                                    this.Invalidate();
                                }));
                            }
                        });
                    }
                    else
                    {
                        imagePictureBox.Image = model.thumbnailImage;
                    }
                }
                else
                {
                    DownLoadChatFileManager.shard.downloadThumbnailFileAttchment(model.message, (aMessage) =>
                    {
                        model.message = aMessage;
                        model.firstbody = aMessage.bodies()[0];
                        var body = model.message.bodies()[0] as EMVideoMessageBody;
                        model.thumbnailFileLocalPath = body.thumbnailLocalPath();
                        model.thumbnailImage = Image.FromFile(model.thumbnailFileLocalPath);
                        model.thumbnailImageSize = model.thumbnailImage.Size;
                        body.setSize(new EMVideoMessageBody.Size((double)model.thumbnailImageSize.Width, (double)model.thumbnailImageSize.Height));
                        EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(model.message.conversationId(), DCUtilTool.GetMConversationType(model.message.chatType()), true);
                        conversation.updateMessage(model.message);
                        if (model.thumbnailImage != null)
                        {
                            panl.BeginInvoke(new EventHandler((s, e) =>
                            {
                                imagePictureBox.Image = model.thumbnailImage;
                                loadVideo();
                                this.Invalidate();
                            }));
                        }
                    });
                }

            }
            else
            {
                DownLoadChatFileManager.shard.downloadThumbnailFileAttchment(model.message, (aMessage) =>
                {
                    model.message = aMessage;
                    model.firstbody = aMessage.bodies()[0];
                    var body = model.message.bodies()[0] as EMVideoMessageBody;
                    model.thumbnailFileLocalPath = body.thumbnailLocalPath();
                    model.thumbnailImage = Image.FromFile(model.thumbnailFileLocalPath);
                    model.thumbnailImageSize = model.thumbnailImage.Size;
                    body.setSize(new EMVideoMessageBody.Size((double)model.thumbnailImageSize.Width, (double)model.thumbnailImageSize.Height));
                    EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(model.message.conversationId(), DCUtilTool.GetMConversationType(model.message.chatType()), true);
                    conversation.updateMessage(model.message);
                    if (model.thumbnailImage != null)
                    {
                        panl.BeginInvoke(new EventHandler((s, e) =>
                        {
                            imagePictureBox.Image = model.thumbnailImage;
                            loadVideo();
                            this.Invalidate();
                        }));
                    }
                });
            }
            if (string.IsNullOrEmpty(model.fileLocalPath))
            {
                DownLoadChatFileManager.shard.downloadFileAttchment(model.message, (p) => {
                    model.message.setStatus(EMMessageStatus.DELIVERING);
                    updateProgress(Convert.ToInt32(p));
                }, (aMessage) =>
                {
                    model.message = aMessage;
                    model.firstbody = aMessage.bodies()[0];
                    var body = model.message.bodies()[0] as EMVideoMessageBody;
                    if (body.downloadStatus() == EMDownloadStatus.SUCCESSED)
                    {
                        model.fileLocalPath = body.localPath();
                    }
                });
            }
            else if (!File.Exists(model.fileLocalPath))
            {
                DownLoadChatFileManager.shard.downloadFileAttchment(model.message, (p) => {
                    model.message.setStatus(EMMessageStatus.DELIVERING);
                    updateProgress(Convert.ToInt32(p));
                }, (aMessage) =>
                {
                    model.message = aMessage;
                    model.firstbody = aMessage.bodies()[0];
                    var body = model.message.bodies()[0] as EMVideoMessageBody;
                    if (body.downloadStatus() == EMDownloadStatus.SUCCESSED)
                    {
                        model.fileLocalPath = body.localPath();
                    }
                });
            }
        }

        private void VoicePictureBox_MouseDown(object sender, DuiMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                EventHandler h = messageClick;
                if (h != null)
                {
                    h(this, e);
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                EventHandler h = messageRightClick;
                if (h != null)
                {
                    h(this, e);
                }
            }
        }

        private void loadVideo()
        {
            BubbleControl.BackgroundRender.Radius = 10;
            imagePictureBox.BackgroundRender.Radius = 10;
            if (!File.Exists(model.fileLocalPath))
            {
                new Thread(new ThreadStart(() =>
                {
                    DownLoadChatFileManager.shard.downloadFileAttchment(model.message, (msg) =>
                    {
                        model.message = msg;
                        EMVideoMessageBody b = msg.bodies()[0] as EMVideoMessageBody;
                        if (b != null)
                        {
                            if (b.downloadStatus() == EMDownloadStatus.SUCCESSED)
                            {
                                model.fileLocalPath = b.localPath();
                            }
                        }
                    });
                })).Start();
            }
            if (model.isSender)
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(Width - ChatPanl.PaddingLorR - ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                int h = 100;
                int w = 100;
                if (model.thumbnailImage != null)
                {
                    model.thumbnailImageSize = model.thumbnailImage.Size;
                    if (model.thumbnailImageSize.Width > 0 && model.thumbnailImageSize.Height > 0)
                    {
                        w = (int)((double)model.thumbnailImageSize.Width / (double)model.thumbnailImageSize.Height * (double)h);
                        if (w + 10 > panl.maxBubbleWith)
                        {
                            w = panl.maxBubbleWith - 10;
                            h = (int)((double)model.thumbnailImageSize.Height / (double)model.thumbnailImageSize.Width * (double)w);
                        }
                    }
                    var body = model.firstbody as EMVideoMessageBody;
                    body.setSize(new EMVideoMessageBody.Size((double)model.thumbnailImageSize.Width, (double)model.thumbnailImageSize.Height));
                    EMConversation conversation = EaseHelper.shard.client.getChatManager().conversationWithType(model.message.conversationId(), DCUtilTool.GetMConversationType(model.message.chatType()), true);
                    conversation.updateMessage(model.message);
                }
                else if (model.thumbnailImageSize.Width > 0 && model.thumbnailImageSize.Height > 0)
                {
                    w = (int)((double)model.thumbnailImageSize.Width / (double)model.thumbnailImageSize.Height * (double)h);
                    if (w + 10 > panl.maxBubbleWith)
                    {
                        w = panl.maxBubbleWith - 10;
                        h = (int)((double)model.thumbnailImageSize.Height / (double)model.thumbnailImageSize.Width * (double)w);
                    }
                }
                BubbleControl.Size = new Size(w + 10, h + 10);
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, 0);
                    BubbleControl.Location = new Point(Width - w - 10 - ChatPanl.PaddingLorR * 2 - ChatPanl.avatarSize, ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP);
                }
                else
                {
                    BubbleControl.Location = new Point(Width - w - 10 - ChatPanl.PaddingLorR * 2 - ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                }
                BubbleControl.BackColor = Color.FromArgb(255, 239, 209);
                imagePictureBox.Location = new Point(5, 5);
                imagePictureBox.Size = new Size(w, h);
                uploadPrograss.Location = new Point(5, 5);
                uploadPrograss.Size = new Size(w, h);
                voicePictureBox.Size = new Size(30, 30);
                voicePictureBox.Location = new Point(5 + w / 2 - 15, 5 + h / 2 - 15);
                voicePictureBox.Image = Properties.Resources.play_normal;
                h = h + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP;
                if (h != Height)
                {
                    Height = h;
                }
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(Width - 10 - ChatPanl.PaddingLorR - ChatPanl.avatarSize - BubbleControl.Width - 20, (h - 20) / 2);
            }
            else
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(ChatPanl.PaddingLorR, ChatPanl.PaddingUorD);
                int h = 100;
                int w = 100;
                if (model.thumbnailImageSize.Width > 0 && model.thumbnailImageSize.Height > 0)
                {
                    w = (int)((double)model.thumbnailImageSize.Width / (double)model.thumbnailImageSize.Height * (double)h);
                    if (w + 10 > panl.maxBubbleWith)
                    {
                        w = panl.maxBubbleWith - 10;
                        h = (int)((double)model.thumbnailImageSize.Height / (double)model.thumbnailImageSize.Width * (double)w);
                    }
                }
                BubbleControl.Size = new Size(w + 10, h + 10);
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, 0);
                    BubbleControl.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP);
                }
                else
                {
                    BubbleControl.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                }
                BubbleControl.BackColor = Color.White;
                imagePictureBox.Location = new Point(5, 5);
                imagePictureBox.Size = new Size(w, h);
                uploadPrograss.Location = new Point(5, 5);
                uploadPrograss.Size = new Size(w, h);
                voicePictureBox.Size = new Size(30, 30);
                voicePictureBox.Location = new Point(5 + w / 2 - 15, 5 + h / 2 - 15);
                voicePictureBox.Image = Properties.Resources.play_normal;
                h = h + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP;
                if (h != Height)
                {
                    Height = h;
                }
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(10 + ChatPanl.PaddingLorR + ChatPanl.avatarSize + BubbleControl.Width + 20, (h - 20) / 2);
            }
            if (model.thumbnailImage != null)
            {
                imagePictureBox.Image = model.thumbnailImage;
            }
        }

        private void setupFile()
        {
            if (model.message.status() == EMMessageStatus.DELIVERING || model.message.status() == EMMessageStatus.FAIL)
            {
                loaddingPicture.Visible = true;
            }
            else
            {
                loaddingPicture.Visible = false;
            }
            uploadPrograss.Visible = false;
            timePanl.Visible = false;
            headBtn.Visible = true;
            textLabel.Visible = true;
            durentLabel.Visible = true;
            voicePictureBox.Visible = false;
            imagePictureBox.Visible = true;
            filePanl.Visible = false;
            fileLine.Visible = false;
            idCardLabel.Visible = false;
            textMessageLabel.Visible = false;
            BubbleControl.Visible = true;
            if (model.isSender)
            {
                nameLabel.Visible = false;
            }
            else
            {
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Visible = true;
                    nameLabel.Text = model.nickName;
                }
                else
                {
                    nameLabel.Visible = false;
                }
            }
            model.image = Properties.Resources.file;
            if (string.IsNullOrEmpty(model.fileLocalPath))
            {
                DownLoadChatFileManager.shard.downloadFileAttchment(model.message, (p) => {
                    model.message.setStatus(EMMessageStatus.DELIVERING);
                    updateProgress(Convert.ToInt32(p));
                }, (aMessage) =>
                {
                    model.message = aMessage;
                    model.firstbody = aMessage.bodies()[0];
                    var body = model.message.bodies()[0] as EMFileMessageBody;
                    if (body.downloadStatus() == EMDownloadStatus.SUCCESSED)
                    {
                        model.fileLocalPath = body.localPath();
                    }
                });
            }
            else if (!File.Exists(model.fileLocalPath))
            {
                DownLoadChatFileManager.shard.downloadFileAttchment(model.message, (p) => {
                    model.message.setStatus(EMMessageStatus.DELIVERING);
                    updateProgress(Convert.ToInt32(p));
                }, (aMessage) =>
                {
                    model.message = aMessage;
                    model.firstbody = aMessage.bodies()[0];
                    var body = model.message.bodies()[0] as EMFileMessageBody;
                    if (body.downloadStatus() == EMDownloadStatus.SUCCESSED)
                    {
                        model.fileLocalPath = body.localPath();
                    }
                });
            }
        }

        private void loadFile()
        {
            BubbleControl.BackgroundRender.Radius = 10;
            if (model.isSender)
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(Width - ChatPanl.PaddingLorR - ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                BubbleControl.BackColor = Color.FromArgb(255, 239, 209);
                BubbleControl.Size = new Size(140, 50);
                BubbleControl.Location = new Point(Width - 140 - ChatPanl.PaddingLorR * 2 - ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                imagePictureBox.Location = new Point(10, 10);
                imagePictureBox.Size = new Size(30, 30);
                imagePictureBox.Image = Properties.Resources.file;
                uploadPrograss.Location = new Point(5, 5);
                uploadPrograss.Size = new Size(40, 40);
                textLabel.Text = model.displayName;
                textLabel.Location = new Point(50, 10);
                var textSize = TextRenderer.MeasureText(model.displayName, panl.textMessageFont);
                if (textSize.Width <= 80)
                {
                    textLabel.Size = textSize;
                }
                else
                {
                    textLabel.Size = new Size(80, textSize.Height);
                }
                durentLabel.Text = model.fileSizeDes;
                textSize = TextRenderer.MeasureText(model.fileSizeDes, panl.textMessageFont);
                durentLabel.Size = textSize;
                durentLabel.Location = new Point(50, 10 + textLabel.Height + 5);
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(Width - 10 - ChatPanl.PaddingLorR - ChatPanl.avatarSize - BubbleControl.Width - 20, (50 + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP - 20) / 2);
            }
            else
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(ChatPanl.PaddingLorR, ChatPanl.PaddingUorD);
                BubbleControl.BackColor = Color.White;
                BubbleControl.Size = new Size(140, 50);
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, 0);
                    BubbleControl.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP);
                }
                else
                {
                    BubbleControl.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                }
                imagePictureBox.Location = new Point(10, 10);
                imagePictureBox.Size = new Size(30, 30);
                imagePictureBox.Image = Properties.Resources.file;
                uploadPrograss.Location = new Point(5, 5);
                uploadPrograss.Size = new Size(40, 40);
                textLabel.Text = model.displayName;
                textLabel.Location = new Point(50, 10);
                var textSize = TextRenderer.MeasureText(model.displayName, panl.textMessageFont);
                if (textSize.Width <= 80)
                {
                    textLabel.Size = textSize;
                }
                else
                {
                    textLabel.Size = new Size(80, textSize.Height);
                }
                durentLabel.Text = model.fileSizeDes;
                textSize = TextRenderer.MeasureText(model.fileSizeDes, panl.textMessageFont);
                durentLabel.Size = textSize;
                durentLabel.Location = new Point(50, 10 + textLabel.Height + 5);
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(10 + ChatPanl.PaddingLorR + ChatPanl.avatarSize + BubbleControl.Width + 20, (50 + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP - 20) / 2);
            }
            var h = 50 + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP;
            if (h != Height)
            {
                Height = h;
            }
        }

        private void setupText()
        {
            if (model.message.status() == EMMessageStatus.DELIVERING || model.message.status() == EMMessageStatus.FAIL)
            {
                loaddingPicture.Visible = true;
            }
            else
            {
                loaddingPicture.Visible = false;
            }
            uploadPrograss.Visible = false;
            timePanl.Visible = false;
            headBtn.Visible = true;
            textLabel.Visible = false;
            durentLabel.Visible = false;
            voicePictureBox.Visible = false;
            imagePictureBox.Visible = false;
            filePanl.Visible = false;
            fileLine.Visible = false;
            idCardLabel.Visible = false;
            textMessageLabel.Visible = true;
            BubbleControl.Visible = true;
            if (model.isSender)
            {
                nameLabel.Visible = false;
            }
            else
            {
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Visible = true;
                    nameLabel.Text = model.nickName;
                }
                else
                {
                    nameLabel.Visible = false;
                }
            }
        }

        private void loadText()
        {
            BubbleControl.BackgroundRender.Radius = 10;
            textMessageLabel.ChatText = new CLChatContent(model.text);
            textMessageLabel.Font = panl.textMessageFont;
            Graphics graphics = panl.CreateGraphics();
            if (model.isSender)
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(Width - ChatPanl.PaddingLorR - ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                BubbleControl.BackColor = Color.FromArgb(255, 239, 209);
                Size ts = textMessageLabel.ChatText.ComputeSize(graphics, textMessageLabel.Font, panl.maxBubbleWith - 10);
                BubbleControl.Location = new Point(Width - ts.Width - 10 - ChatPanl.PaddingLorR * 2 - ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                BubbleControl.Size = new Size(ts.Width + 10, ts.Height + 10);
                textMessageLabel.Location = new Point(5, 5);
                textMessageLabel.Size = ts;
            }
            else
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(ChatPanl.PaddingLorR, ChatPanl.PaddingUorD);
                BubbleControl.BackColor = Color.White;
                Size ts = textMessageLabel.ChatText.ComputeSize(graphics, textMessageLabel.Font, panl.maxBubbleWith - 10);
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, 0);
                    BubbleControl.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingLorR, ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP);
                }
                else
                {
                    BubbleControl.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingLorR, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                }
                BubbleControl.Size = new Size(ts.Width + 10, ts.Height + 10);
                textMessageLabel.Location = new Point(5, 5);
                textMessageLabel.Size = ts;
            }
            BubbleControl.MouseClick -= BubbleControl_MouseClick;
            textMessageLabel.MouseDown += TextMessageLabel_MouseDown;
            graphics.Dispose();
            var h = BubbleControl.Height + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP;
            if (h != Height)
            {
                Height = h;
            }
            if (model.isSender)
            {
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(Width - 10 - ChatPanl.PaddingLorR - ChatPanl.avatarSize - BubbleControl.Width - 20, (h - 20) / 2);
            }
            else
            {
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(10 + ChatPanl.PaddingLorR + ChatPanl.avatarSize + BubbleControl.Width + 20, (h - 20) / 2);
            }
        }

        private void setupNoSupport()
        {
            noSupport = true;
            timePanl.Visible = false;
            headBtn.Visible = true;
            textLabel.Visible = true;
            durentLabel.Visible = false;
            voicePictureBox.Visible = false;
            imagePictureBox.Visible = false;
            filePanl.Visible = false;
            fileLine.Visible = false;
            idCardLabel.Visible = false;
            textMessageLabel.Visible = false;
            BubbleControl.Visible = true;
            uploadPrograss.Visible = false;
            loaddingPicture.Visible = false;
            if (model.isSender)
            {
                nameLabel.Visible = false;
            }
            else
            {
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Visible = true;
                    nameLabel.Text = model.nickName;
                }
                else
                {
                    nameLabel.Visible = false;
                }
            }
            model.text = "[不支持该消息类型]";
        }

        private void loadNosupport()
        {
            BubbleControl.BackgroundRender.Radius = 10;
            textLabel.Text = model.text;
            textLabel.Font = panl.textMessageFont;
            Graphics graphics = this.HostControl.CreateGraphics();
            if (model.isSender)
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(Width - ChatPanl.PaddingLorR - ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                BubbleControl.BackColor = Color.FromArgb(255, 239, 209);
                Size ts = graphics.MeasureString(textLabel.Text, textLabel.Font).ToSize();
                BubbleControl.Location = new Point(Width - ts.Width - 10 - ChatPanl.PaddingLorR * 2 - ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                BubbleControl.Size = new Size(ts.Width + 10, ts.Height + 10);
                textLabel.Location = new Point(5, 5);
                textLabel.Size = ts;
            }
            else
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(ChatPanl.PaddingLorR, ChatPanl.PaddingUorD);
                BubbleControl.BackColor = Color.White;
                Size ts = graphics.MeasureString(textLabel.Text, textLabel.Font).ToSize();
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, 0);
                    BubbleControl.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingLorR, ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP);
                }
                else
                {
                    BubbleControl.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingLorR, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                }
                BubbleControl.Size = new Size(ts.Width + 10, ts.Height + 10);
                textLabel.Location = new Point(5, 5);
                textLabel.Size = ts;
            }
            graphics.Dispose();
            var h = BubbleControl.Height + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP;
            if (h != Height)
            {
                Height = h;
            }
        }

        private void TextMessageLabel_MouseDown(object sender, DuiMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                EventHandler h = messageRightClick;
                if (h != null)
                {
                    h(this, e);
                }
            }
        }

        private void setupLocation()
        {
            timePanl.Visible = false;
            headBtn.Visible = true;
            textLabel.Visible = true;
            durentLabel.Visible = false;
            voicePictureBox.Visible = false;
            imagePictureBox.Visible = true;
            filePanl.Visible = false;
            fileLine.Visible = false;
            idCardLabel.Visible = false;
            textMessageLabel.Visible = false;
            BubbleControl.Visible = true;
            uploadPrograss.Visible = false;
            loaddingPicture.Visible = false;
            imagePictureBox.Image = Properties.Resources.location;
            if (model.isSender)
            {
                nameLabel.Visible = false;
            }
            else
            {
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Visible = true;
                    nameLabel.Text = model.nickName;
                }
                else
                {
                    nameLabel.Visible = false;
                }
            }
        }

        private void loadLocation()
        {
            BubbleControl.BackgroundRender.Radius = 10;
            if (model.isSender)
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(Width - ChatPanl.PaddingLorR - ChatPanl.avatarSize, ChatPanl.PaddingUorD);
                BubbleControl.BackColor = Color.FromArgb(255, 239, 209);
                BubbleControl.Size = new Size(140, 50);
                BubbleControl.Location = new Point(Width - 140 - ChatPanl.PaddingLorR * 2 - ChatPanl.avatarSize, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                imagePictureBox.Location = new Point(10, 10);
                imagePictureBox.Size = new Size(30, 30);
                textLabel.Text = model.address;
                textLabel.Location = new Point(50, 20);
            }
            else
            {
                headBtn.Size = new Size(ChatPanl.avatarSize, ChatPanl.avatarSize);
                headBtn.Location = new Point(ChatPanl.PaddingLorR, ChatPanl.PaddingUorD);
                BubbleControl.BackColor = Color.White;
                BubbleControl.Size = new Size(140, 50);
                if (model.chatType == EMChatType.GROUP)
                {
                    nameLabel.Location = new Point(ChatPanl.PaddingLorR * 2 + ChatPanl.avatarSize, 0);
                    BubbleControl.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingLorR, ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP);
                }
                else
                {
                    BubbleControl.Location = new Point(headBtn.Location.X + headBtn.Width + ChatPanl.PaddingLorR, ChatPanl.PaddingUorD + ChatPanl.PaddingBandAUP);
                }
                imagePictureBox.Location = new Point(10, 10);
                imagePictureBox.Size = new Size(30, 30);
                textLabel.Text = model.address;
                textLabel.Location = new Point(50, 20);
            }
            var h = 50 + 10 + ChatPanl.PaddingUorD * 2 + ChatPanl.PaddingBandAUP;
            if (h != Height)
            {
                Height = h;
            }
            if (model.isSender)
            {
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(Width - 10 - ChatPanl.PaddingLorR - ChatPanl.avatarSize - BubbleControl.Width - 20, (h - 20) / 2);
            }
            else
            {
                loaddingPicture.Size = new Size(20, 20);
                loaddingPicture.Location = new Point(10 + ChatPanl.PaddingLorR + ChatPanl.avatarSize + BubbleControl.Width + 20, (h - 20) / 2);
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        private void ChatHistoryListItem_ParentChanged(object sender, EventArgs e)
        {
            if (Parent is ChatPanl)
            {
                Width = Parent.InnerDuiControl.Width;
            }
            if (model.isTime)
            {
                loadTimeOrTips();
            }
            else if (model.isshowTest)
            {
                loadTimeOrTips();
            }
            else
            {
                if (model.bodyType == EMMessageBodyType.TEXT)
                {
                    if (model.isGifFace)
                    {
                        loadGifFace();
                    }
                    else if (model.isIDCard)
                    {
                        //loadIDCard();
                        loadNosupport();
                    }
                    else
                    {
                        loadText();
                    }
                }
                else if (model.bodyType == EMMessageBodyType.IMAGE)
                {
                    loadImaage();
                }
                else if (model.bodyType == EMMessageBodyType.VOICE)
                {
                    loadVoice();
                }
                else if (model.bodyType == EMMessageBodyType.VIDEO)
                {
                    loadVideo();
                }
                else if (model.bodyType == EMMessageBodyType.LOCATION)
                {
                    //loadLocation();
                    loadNosupport();
                }
                else
                {
                    loadFile();
                }
            }
        }

        private void ChatHistoryListItem_SizeChanged(object sender, EventArgs e)
        {
            if (model.isTime)
            {
                loadTimeOrTips();
            }
            else if (model.isshowTest)
            {
                loadTimeOrTips();
            }
            else
            {
                if (model.bodyType == EMMessageBodyType.TEXT)
                {
                    if (model.isGifFace)
                    {
                        loadGifFace();
                    }
                    else if (model.isIDCard)
                    {
                        //loadIDCard();
                        loadNosupport();
                    }
                    else
                    {
                        loadText();
                    }
                }
                else if (model.bodyType == EMMessageBodyType.IMAGE)
                {
                    loadImaage();
                }
                else if (model.bodyType == EMMessageBodyType.VOICE)
                {
                    loadVoice();
                }
                else if (model.bodyType == EMMessageBodyType.VIDEO)
                {
                    loadVideo();
                }
                else if (model.bodyType == EMMessageBodyType.LOCATION)
                {
                    //loadLocation();
                    loadNosupport();
                }
                else
                {
                    loadFile();
                }
            }
        }

        private void BubbleControl_MouseClick(object sender, DSkin.DirectUI.DuiMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                EventHandler h = messageClick;
                if (h != null)
                {
                    h(this, e);
                }
            }
            if (e.Button == MouseButtons.Right)
            {
                EventHandler h = messageRightClick;
                if (h != null)
                {
                    h(this, e);
                }
            }
        }

        private void BubbleControl_MouseLeave(object sender, System.EventArgs e)
        {
            if (!model.isTime && !model.isshowTest && !model.isGifFace && !model.isIDCard)
            {
                if (model.isSender)
                {
                    BubbleControl.BackColor = Color.FromArgb(255, 239, 209);
                }
                else
                {
                    BubbleControl.BackColor = Color.White;
                }
            }
        }

        private void BubbleControl_MouseEnter(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            if (!model.isTime && !model.isshowTest && !model.isGifFace && !model.isIDCard)
            {
                BubbleControl.BackColor = Color.FromArgb(204, 204, 204);
            }
        }

        private void headBtn_MouseClick(object sender, DSkin.DirectUI.DuiMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                EventHandler h = headClick;
                if (h != null)
                {
                    h(this, e);
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                EventHandler h = headRightClick;
                if (h != null)
                {
                    h(this, e);
                }
            }
        }

        protected override void OnPrePaint(PaintEventArgs e)
        {

            base.OnPrePaint(e);
        }

        protected override void OnLayout(EventArgs e)
        {
            if (!model.isSender && model.chatType == EMChatType.GROUP)
            {
                nameLabel.Text = model.nickName;
            }
            base.OnLayout(e);
        }

        public void updateMessgeState(EMMessage message)
        {
            model.message = message;
            model.isRead = message.isRead();
            if (model.bodyType == EMMessageBodyType.VOICE)
            {
                panl.BeginInvoke(new EventHandler((s, e) =>
                {
                    if (model.isRead)
                    {
                        voicePictureBox.Visible = false;
                    }
                    else
                    {
                        voicePictureBox.Visible = true;
                    }
                }));
            }
            if (model.message.status() == EMMessageStatus.DELIVERING)
            {
                if (model.bodyType == EMMessageBodyType.FILE || model.bodyType == EMMessageBodyType.IMAGE || model.bodyType == EMMessageBodyType.VIDEO)
                {
                    panl.BeginInvoke(new EventHandler((s, e) =>
                    {
                        loaddingPicture.Visible = false;
                        uploadPrograss.Visible = true;
                    }));
                }
                else
                {
                    panl.BeginInvoke(new EventHandler((s, e) =>
                    {
                        loaddingPicture.Image = Properties.Resources.loading;
                        loaddingPicture.Visible = true;
                        uploadPrograss.Visible = false;
                    }));
                }
            }
            else if (model.message.status() == EMMessageStatus.FAIL)
            {
                panl.BeginInvoke(new EventHandler((s, e) =>
                {
                    loaddingPicture.Image = Properties.Resources.error;
                    loaddingPicture.Visible = true;
                    uploadPrograss.Visible = false;
                }));
            }
            else
            {
                panl.BeginInvoke(new EventHandler((s, e) =>
                {
                    loaddingPicture.Visible = false;
                    uploadPrograss.Visible = false;
                }));
            }
        }

        public void updateProgress(int value)
        {
            if (model.bodyType == EMMessageBodyType.FILE || model.bodyType == EMMessageBodyType.IMAGE || model.bodyType == EMMessageBodyType.VIDEO)
            {
                panl.BeginInvoke(new EventHandler((s, e) =>
                {
                    uploadPrograss.Visible = true;
                    uploadPrograss.Progress = value;
                }));
            }
        }
    }
}
