using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DSkin.Forms;
using ChangLiao.Temple;
using EaseMobLib;
using ChangLiao.Util;
using Vlc.DotNet.Core;
using Vlc.DotNet.Core.Interops.Signatures;
using Vlc.DotNet.Core.Interops;

namespace ChangLiao.windows
{
    public partial class PicturePreviewForm : DSkinForm
    {
        private List<ChatHistoryListItem> imageList;
        private int ShowIndex;
        private bool isPaused;
        public PicturePreviewForm()
        {
            InitializeComponent();
            BackColor = Color.FromArgb(230, 0, 0, 0);
        }

        public PicturePreviewForm(List<ChatHistoryListItem> messageList,int index)
        {
            imageList = messageList;
            ShowIndex = index;
            InitializeComponent();
            BackColor = Color.FromArgb(230, 0, 0, 0);
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            leftPictureBox.Location = new Point(0, 60);
            leftPictureBox.Size = new Size(75, Height - 60);
            rightPictureBox2.Location = new Point(Width - 75, 60);
            rightPictureBox2.Size = new Size(75, Height - 60);
            dSkinButton1.Visible = false;
            if (ShowIndex == 0)
            {
                leftPictureBox.Visible = false;
            }
            else
            {
                leftPictureBox.Visible = true;
            }
            if (ShowIndex == imageList.Count - 1)
            {
                rightPictureBox2.Visible = false;
            }
            else
            {
                rightPictureBox2.Visible = true;
            }
            showImage(ShowIndex);
        }

        private void showImage(int index)
        {
            if (index >= imageList.Count)
            {
                return;
            }
            ShowIndex = index;
            this.BeginInvoke(new EventHandler((s, e) =>
            {
                if (ShowIndex == 0)
                {
                    leftPictureBox.Visible = false;
                }
                else
                {
                    leftPictureBox.Visible = true;
                }
                if (ShowIndex == imageList.Count - 1)
                {
                    rightPictureBox2.Visible = false;
                }
                else
                {
                    rightPictureBox2.Visible = true;
                }
                this.Invalidate();
            }));
            ChatHistoryListItem item = imageList[ShowIndex];
            if (item.model.isGifFace)
            {
                if (item.model.faceH < Height - 60 && item.model.faceW < Width - 150)
                {
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        if (vlcControl1.IsPlaying)
                        {
                            vlcControl1.Stop();
                        }
                        vlcControl1.Visible = false;
                        dSkinPictureBox1.Visible = true;
                        dSkinNewPanel1.Visible = false;
                        dSkinButton1.Visible = false;
                        dSkinPictureBox1.Image = item.model.image == null ? item.model.thumbnailImage : item.model.image;
                        dSkinPictureBox1.Location = new Point((Width - item.model.faceW - 150) / 2, (Height - item.model.faceH) / 2 + 60);
                        dSkinPictureBox1.Size = new Size(item.model.faceW, item.model.faceH);
                        DCWebImageMaanager.shard.downloadImageAsync(item.model.gitFaceURL, (image, b) =>
                        {
                            if (image != null)
                            {
                                if (dSkinPictureBox1.IsHandleCreated)
                                {
                                    dSkinPictureBox1.BeginInvoke(new EventHandler((ss, ee) =>
                                    {
                                        dSkinPictureBox1.Image = image;
                                        dSkinPictureBox1.Invalidate();
                                    }));
                                }
                            }
                        });
                        this.Invalidate();
                    }));
                }
                else if (item.model.faceH > Height - 60)
                {
                    int w = Convert.ToInt32((double)item.model.faceW / (double)item.model.faceH * (Height - 60));
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        if (vlcControl1.IsPlaying)
                        {
                            vlcControl1.Stop();
                        }
                        vlcControl1.Visible = false;
                        dSkinPictureBox1.Visible = true;
                        dSkinNewPanel1.Visible = false;
                        dSkinButton1.Visible = false;
                        dSkinPictureBox1.Image = item.model.image == null ? item.model.thumbnailImage : item.model.image;
                        dSkinPictureBox1.Location = new Point((Width - 150 - w) / 2, 60);
                        dSkinPictureBox1.Size = new Size(w, Height - 60);
                        DCWebImageMaanager.shard.downloadImageAsync(item.model.gitFaceURL, (image, b) =>
                        {
                            if (image != null)
                            {
                                if (dSkinPictureBox1.IsHandleCreated)
                                {
                                    dSkinPictureBox1.BeginInvoke(new EventHandler((ss, ee) =>
                                    {
                                        dSkinPictureBox1.Image = image;
                                        dSkinPictureBox1.Invalidate();
                                    }));
                                }
                            }
                        });
                        this.Invalidate();
                    }));
                }
                else
                {
                    int h = Convert.ToInt32((double)item.model.faceH / (double)item.model.faceW * ((double)Width - 150));
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        if (vlcControl1.IsPlaying)
                        {
                            vlcControl1.Stop();
                        }
                        vlcControl1.Visible = false;
                        dSkinPictureBox1.Visible = true;
                        dSkinNewPanel1.Visible = false;
                        dSkinButton1.Visible = false;
                        dSkinPictureBox1.Image = item.model.image == null ? item.model.thumbnailImage : item.model.image;
                        dSkinPictureBox1.Location = new Point((Width - 150 - item.model.faceW) / 2, (Height - h) / 2 + 60);
                        dSkinPictureBox1.Size = new Size(item.model.faceW, h);
                        DCWebImageMaanager.shard.downloadImageAsync(item.model.gitFaceURL, (image, b) =>
                        {
                            if (image != null)
                            {
                                if (dSkinPictureBox1.IsHandleCreated)
                                {
                                    dSkinPictureBox1.BeginInvoke(new EventHandler((ss, ee) =>
                                    {
                                        dSkinPictureBox1.Image = image;
                                        dSkinPictureBox1.Invalidate();
                                    }));
                                }
                            }
                        });
                        this.Invalidate();
                    }));
                }
            }
            else if (item.model.bodyType == EMMessageBodyType.IMAGE)
            {
                if (File.Exists(item.model.fileLocalPath))
                {
                    try
                    {
                        Image image = Image.FromFile(item.model.fileLocalPath);
                        Size s = image.Size;
                        if (s.Height > s.Width)
                        {
                            int w = Convert.ToInt32((double)s.Width / (double)s.Height * (this.Height - 60));
                            int h = Convert.ToInt32((double)s.Height / (double)s.Width * w);
                            s = new Size(w, h);
                        }
                        else
                        {
                            int h = Convert.ToInt32((double)s.Height / (double)s.Width * (this.Width - 150));
                            int w = Convert.ToInt32((double)s.Width / (double)s.Height * h);
                            s = new Size(w, h);
                        }
                        this.BeginInvoke(new EventHandler((ss, ee) =>
                        {
                            if (vlcControl1.IsPlaying)
                            {
                                vlcControl1.Stop();
                            }
                            vlcControl1.Visible = false;
                            dSkinPictureBox1.Visible = true;
                            dSkinNewPanel1.Visible = false;
                            dSkinPictureBox1.Image = image;
                            dSkinButton1.Visible = false;
                            dSkinPictureBox1.Location = new Point((this.Width - 150 - s.Width) / 2 + 75, (this.Height - s.Height) / 2 + 60);
                            dSkinPictureBox1.Size = s;
                            this.Invalidate();
                        }));
                    }
                    catch (Exception e)
                    {
                        DownLoadChatFileManager.shard.downloadFileAttchment(item.model.message, (msg) =>
                        {
                            EMImageMessageBody mImageMessageBody = msg.bodies()[0] as EMImageMessageBody;
                            if (File.Exists(mImageMessageBody.localPath()))
                            {
                                Image image = Image.FromFile(mImageMessageBody.localPath());
                                if (image != null)
                                {
                                    Size s = image.Size;
                                    if (s.Height > s.Width)
                                    {
                                        int w = Convert.ToInt32((double)s.Width / (double)s.Height * (this.Height - 60));
                                        int h = Convert.ToInt32((double)s.Height / (double)s.Width * w);
                                        s = new Size(w, h);
                                    }
                                    else
                                    {
                                        int h = Convert.ToInt32((double)s.Height / (double)s.Width * (this.Width - 150));
                                        int w = Convert.ToInt32((double)s.Width / (double)s.Height * h);
                                        s = new Size(w, h);
                                    }
                                    this.BeginInvoke(new EventHandler((ss, ee) =>
                                    {
                                        if (vlcControl1.IsPlaying)
                                        {
                                            vlcControl1.Stop();
                                        }
                                        vlcControl1.Visible = false;
                                        dSkinPictureBox1.Visible = true;
                                        dSkinNewPanel1.Visible = false;
                                        dSkinButton1.Visible = false;
                                        dSkinPictureBox1.Image = image;
                                        dSkinPictureBox1.Location = new Point((this.Width - 150 - s.Width) / 2 + 75, (this.Height - s.Height) / 2 + 60);
                                        dSkinPictureBox1.Size = s;
                                        this.Invalidate();
                                    }));
                                }
                                else
                                {
                                    MessageBox.Show("加载图片失败");
                                }
                            }
                        });
                    }
                }
                else
                {
                    DownLoadChatFileManager.shard.downloadFileAttchment(item.model.message, (msg) =>
                    {
                        EMImageMessageBody mImageMessageBody = msg.bodies()[0] as EMImageMessageBody;
                        if (File.Exists(mImageMessageBody.localPath()))
                        {

                            Image image = Image.FromFile(mImageMessageBody.localPath());
                            if (image != null)
                            {
                                Size s = image.Size;
                                if (s.Height > s.Width)
                                {
                                    int w = Convert.ToInt32((double)s.Width / (double)s.Height * (this.Height - 60));
                                    int h = Convert.ToInt32((double)s.Height / (double)s.Width * w);
                                    s = new Size(w, h);
                                }
                                else
                                {
                                    int h = Convert.ToInt32((double)s.Height / (double)s.Width * (this.Width - 150));
                                    int w = Convert.ToInt32((double)s.Width / (double)s.Height * h);
                                    s = new Size(w, h);
                                }
                                this.BeginInvoke(new EventHandler((ss, ee) =>
                                {
                                    if (vlcControl1.IsPlaying)
                                    {
                                        vlcControl1.Stop();
                                    }
                                    vlcControl1.Visible = false;
                                    dSkinPictureBox1.Visible = true;
                                    dSkinNewPanel1.Visible = false;
                                    dSkinButton1.Visible = false;
                                    dSkinPictureBox1.Image = image;
                                    dSkinPictureBox1.Location = new Point((this.Width - 150 - s.Width) / 2, (this.Height - s.Height) / 2 + 60);
                                    dSkinPictureBox1.Size = s;
                                    this.Invalidate();
                                }));
                            }
                            else
                            {
                                MessageBox.Show("加载图片失败");
                            }
                        }
                    });
                }
            }
            else if (item.model.bodyType == EMMessageBodyType.VIDEO)
            {
                this.BeginInvoke(new EventHandler((s, e) =>
                {
                    controlHost1.Location = new Point(75, 60);
                    controlHost1.Size = new Size(Width - 150, Height - 60);
                    dSkinNewPanel1.Visible = true;
                    dSkinPictureBox1.Visible = false;
                    dSkinButton1.Visible = true;
                    dSkinNewPanel1.Location = new Point((controlHost1.Width - 200) / 2, controlHost1.Height - 100);
                    this.Invalidate();
                }));
                if (File.Exists(item.model.fileLocalPath))
                {
                    this.BeginInvoke(new EventHandler((s, e) =>
                    {
                        vlcControl1.Play(new FileStream(item.model.fileLocalPath, FileMode.Open));
                    }));
                }
                else
                {
                    DownLoadChatFileManager.shard.downloadFileAttchment(item.model.message, (msg) =>
                    {
                        EMVideoMessageBody body = msg.bodies()[0] as EMVideoMessageBody;
                        if (body != null)
                        {
                            item.model.fileLocalPath = body.localPath();
                            if (File.Exists(item.model.fileLocalPath))
                            {
                                this.BeginInvoke(new EventHandler((s, e) =>
                                {
                                    vlcControl1.Play(new FileStream(item.model.fileLocalPath, FileMode.Open));
                                }));
                            }
                            else
                            {
                                MessageBox.Show("下载失败");
                            }
                        }
                    });
                }
            }
        }

        private void leftPictureBox_Click(object sender, EventArgs e)
        {
            if (ShowIndex == 0)
            {
                return;
            }
            showImage(ShowIndex - 1);
        }

        private void rightPictureBox2_Click(object sender, EventArgs e)
        {
            if (ShowIndex == imageList.Count - 1)
            {
                return;
            }
            showImage(ShowIndex + 1);
        }

        private void vlcControl1_Playing(object sender, VlcMediaPlayerPlayingEventArgs e)
        {
            Console.WriteLine("--------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------播放");
            dSkinButton1.BeginInvoke(new EventHandler((ss, ee) =>
            {
                dSkinButton1.Image = Properties.Resources.pause_normal;
                dSkinButton1.Invalidate();
            }));
        }

        private void vlcControl1_VlcLibDirectoryNeeded(object sender, Vlc.DotNet.Forms.VlcLibDirectoryNeededEventArgs e)
        {
            e.VlcLibDirectory = new DirectoryInfo(Path.Combine(Directory.GetCurrentDirectory(), "libvlc", "win-x86"));
        }

        private void leftPictureBox_MouseEnter(object sender, EventArgs e)
        {
            leftPictureBox.BackColor = Color.DarkGray;
        }

        private void vlcControl1_Paused(object sender, VlcMediaPlayerPausedEventArgs e)
        {
            isPaused = true;
            dSkinButton1.BeginInvoke(new EventHandler((ss, ee) =>
            {
                dSkinButton1.Image = Properties.Resources.play_normal;
                dSkinButton1.Invalidate();
            }));
        }

        private void vlcControl1_TimeChanged(object sender, VlcMediaPlayerTimeChangedEventArgs e)
        {

        }

        private void vlcControl1_Stopped(object sender, VlcMediaPlayerStoppedEventArgs e)
        {
            vlcControl1.GetCurrentMedia().Dispose();
            dSkinButton1.BeginInvoke(new EventHandler((ss, ee) =>
            {
                dSkinButton1.Image = Properties.Resources.play_normal;
                dSkinButton1.Invalidate();
            }));
        }

        private void leftPictureBox_MouseLeave(object sender, EventArgs e)
        {
            leftPictureBox.BackColor = Color.Transparent;
        }

        private void rightPictureBox2_DragEnter(object sender, DragEventArgs e)
        {

        }

        private void rightPictureBox2_DragLeave(object sender, EventArgs e)
        {

        }

        private void rightPictureBox2_MouseEnter(object sender, EventArgs e)
        {
            rightPictureBox2.BackColor = Color.DarkGray;
        }

        private void rightPictureBox2_MouseLeave(object sender, EventArgs e)
        {
            rightPictureBox2.BackColor = Color.Transparent;
        }

        private void dSkinButton1_Click(object sender, EventArgs e)
        {
            if (vlcControl1.GetCurrentMedia() == null)
            {
                ChatHistoryListItem item = imageList[ShowIndex];
                if (!File.Exists(item.model.fileLocalPath))
                {
                    MessageBox.Show("视频下载中");
                    return;
                }
                else
                {
                    this.BeginInvoke(new EventHandler((s, ee) =>
                    {
                        vlcControl1.Play(new FileStream(item.model.fileLocalPath, FileMode.Open));
                    }));
                }
            }
            else
            {
                if (vlcControl1.IsPlaying)
                {
                    vlcControl1.BeginInvoke(new EventHandler((ss, ee) =>
                    {
                        vlcControl1.Pause();
                    }));
                }
                else
                {
                    if (isPaused)
                    {
                        isPaused = false;
                        vlcControl1.BeginInvoke(new EventHandler((ss, ee) =>
                        {
                            vlcControl1.Play();
                        }));
                    }
                    else
                    {
                        ChatHistoryListItem item = imageList[ShowIndex];
                        if (File.Exists(item.model.fileLocalPath))
                        {
                            this.BeginInvoke(new EventHandler((s, eee) =>
                            {
                                vlcControl1.Play(new FileStream(item.model.fileLocalPath, FileMode.Open));
                            }));
                        }
                        else
                        {
                            DownLoadChatFileManager.shard.downloadFileAttchment(item.model.message, (msg) =>
                            {
                                EMVideoMessageBody body = msg.bodies()[0] as EMVideoMessageBody;
                                if (body != null)
                                {
                                    item.model.fileLocalPath = body.localPath();
                                    if (File.Exists(item.model.fileLocalPath))
                                    {
                                        this.BeginInvoke(new EventHandler((s, eee) =>
                                        {
                                            vlcControl1.Play(new FileStream(item.model.fileLocalPath, FileMode.Open));
                                        }));
                                    }
                                    else
                                    {
                                        MessageBox.Show("下载失败");
                                    }
                                }
                            });
                        }
                    }
                }
            }
        }

        private void dSkinButton1_MouseEnter(object sender, EventArgs e)
        {
            if (vlcControl1.IsPlaying)
            {
                dSkinButton1.Image = Properties.Resources.pause_select;
            }
            else
            {
                dSkinButton1.Image = Properties.Resources.play_select;
            }
        }

        private void dSkinButton1_MouseLeave(object sender, EventArgs e)
        {
            if (vlcControl1.IsPlaying)
            {
                dSkinButton1.Image = Properties.Resources.pause_normal;
            }
            else
            {
                dSkinButton1.Image = Properties.Resources.play_normal;
            }
        }
    }
}
