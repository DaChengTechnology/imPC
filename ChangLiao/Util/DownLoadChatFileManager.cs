using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EaseMobLib;
using System.IO;
using ChangLiao.Ease;
using System.Drawing;

namespace ChangLiao.Util
{
    class DownLoadChatFileManager
    {
        public delegate void DownloadMessageAttchmentComplite(EMMessage mMessage);
        public delegate void DownloadMessageAttchmentProgress(float progress);
        private static readonly object padlock = new object();
        private static DownLoadChatFileManager manager;
        public static DownLoadChatFileManager shard
        {
            get
            {
                lock (padlock)
                {
                    if (manager == null)
                    {
                        manager = new DownLoadChatFileManager();
                    }
                    return manager;
                }
            }
        }
        /// <summary>
        /// 下载消息附件
        /// </summary>
        /// <param name="mMessage">消息</param>
        /// <param name="complite">下载完成回调</param>
        public void downloadThumbnailFileAttchment(EMMessage mMessage, DownloadMessageAttchmentComplite complite)
        {
            downloadThumbnailFileAttchment(mMessage, null, complite);
        }
        /// <summary>
        /// 下载消息附件
        /// </summary>
        /// <param name="mMessage">消息</param>
        /// <param name="progress">进度回调</param>
        /// <param name="complite">完成回调</param>
        public void downloadThumbnailFileAttchment(EMMessage mMessage, DownloadMessageAttchmentProgress progress, DownloadMessageAttchmentComplite complite)
        {
            new Thread(new ThreadStart(() =>
            {
                if (mMessage.bodies().Length < 1)
                {
                    return;
                }
                if (mMessage.bodies()[0].type == EMMessageBodyType.VOICE)
                {
                    downloadVoiceThumbnailFileAttchment(mMessage, progress, complite);
                }
                else if (mMessage.bodies()[0].type == EMMessageBodyType.IMAGE)
                {
                    downloadImageThumbnailFileAttchment(mMessage, progress, complite);
                }
                else if (mMessage.bodies()[0].type == EMMessageBodyType.VIDEO)
                {
                    downloadVideoThumbnailFileAttchment(mMessage, progress, complite);
                }
            })).Start();
        }
        /// <summary>
        /// 下载消息文件
        /// </summary>
        /// <param name="mMessage">消息</param>
        /// <param name="complite">完成回调</param>
        public void downloadFileAttchment(EMMessage mMessage,DownloadMessageAttchmentComplite complite)
        {
            downloadFileAttchment(mMessage, null, complite);
        }
        /// <summary>
        /// 下载消息文件
        /// </summary>
        /// <param name="mMessage">消息</param>
        /// <param name="progress">进度回调</param>
        /// <param name="complite">完成回调</param>
        public void downloadFileAttchment(EMMessage mMessage,DownloadMessageAttchmentProgress progress,DownloadMessageAttchmentComplite complite)
        {
            new Thread(new ThreadStart(() =>
            {
                if (mMessage.bodies().Length < 1)
                {
                    return;
                }
                if (mMessage.bodies()[0].type == EMMessageBodyType.IMAGE)
                {
                    downloadImageMainAttchment(mMessage, progress, complite);
                }
                else if (mMessage.bodies()[0].type == EMMessageBodyType.VIDEO)
                {
                    downloadVideoMainAttchment(mMessage, progress, complite);
                }
                else if (mMessage.bodies()[0].type == EMMessageBodyType.FILE)
                {
                    downloadFileMainAttchment(mMessage, progress, complite);
                }
            })).Start();
        }

        private void downloadVoiceThumbnailFileAttchment(EMMessage mMessage, DownloadMessageAttchmentProgress progress, DownloadMessageAttchmentComplite complite)
        {
            EMVoiceMessageBody body = mMessage.bodies()[0] as EMVoiceMessageBody;
            body.setDownloadStatus(EMDownloadStatus.DOWNLOADING);
            mMessage.clearBodies();
            mMessage.addBody(body);
            var conversation = EaseHelper.shard.client.getChatManager().conversationWithType(mMessage.conversationId(), DCUtilTool.GetMConversationType(mMessage.chatType()), true);
            conversation.updateMessage(mMessage);
            string dir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao" + "\\ChangLiao\\" + SettingMenager.shard.idCard;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = dir + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".amr";
            HttpUitls.Instance.DownloadFile(body.remotePath(), path, (p) =>
            {
                if (progress != null)
                {
                    progress(p);
                }
            }, (b) =>
            {
                if (b)
                {
                    var fi = new FileInfo(path);
                    body.setFileLength(fi.Length);
                    body.setLocalPath(path);
                    body.setDownloadStatus(EMDownloadStatus.SUCCESSED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
                else
                {
                    body.setDownloadStatus(EMDownloadStatus.FAILED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
            });
        }

        private void downloadImageThumbnailFileAttchment(EMMessage mMessage, DownloadMessageAttchmentProgress progress, DownloadMessageAttchmentComplite complite)
        {
            EMImageMessageBody body = mMessage.bodies()[0] as EMImageMessageBody;
            body.setThumbnailDownloadStatus(EMDownloadStatus.DOWNLOADING);
            mMessage.clearBodies();
            mMessage.addBody(body);
            var conversation = EaseHelper.shard.client.getChatManager().conversationWithType(mMessage.conversationId(), DCUtilTool.GetMConversationType(mMessage.chatType()), true);
            conversation.updateMessage(mMessage);
            string dir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao" + "\\ChangLiao\\" + SettingMenager.shard.idCard;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = dir + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
            HttpUitls.Instance.DownloadFile(body.thumbnailRemotePath(), path, (p) =>
            {
                if (progress != null)
                {
                    progress(p);
                }
            }, (b) =>
            {
                if (b)
                {
                    var fi = new FileInfo(path);
                    body.setThumbnailFileLength(fi.Length);
                    body.setThumbnailLocalPath(path);
                    Image image = Image.FromFile(path);
                    body.setThumbnailSize(new EMImageMessageBody.Size((double)image.Width, (double)image.Height));
                    body.setThumbnailDownloadStatus(EMDownloadStatus.SUCCESSED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
                else
                {
                    body.setThumbnailDownloadStatus(EMDownloadStatus.FAILED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
            });
        }

        private void downloadVideoThumbnailFileAttchment(EMMessage mMessage, DownloadMessageAttchmentProgress progress, DownloadMessageAttchmentComplite complite)
        {
            EMVideoMessageBody body = mMessage.bodies()[0] as EMVideoMessageBody;
            body.setDownloadStatus(EMDownloadStatus.DOWNLOADING);
            mMessage.clearBodies();
            mMessage.addBody(body);
            var conversation = EaseHelper.shard.client.getChatManager().conversationWithType(mMessage.conversationId(), DCUtilTool.GetMConversationType(mMessage.chatType()), true);
            conversation.updateMessage(mMessage);
            string dir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao" + "\\ChangLiao\\" + SettingMenager.shard.idCard;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = dir + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
            HttpUitls.Instance.DownloadFile(body.thumbnailRemotePath(), path, (p) =>
            {
                if (progress != null)
                {
                    progress(p);
                }
            }, (b) =>
            {
                if (b)
                {
                    body.setThumbnailLocalPath(path);
                    body.setThumbnailDownloadStatus(EMDownloadStatus.SUCCESSED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
                else
                {
                    body.setThumbnailDownloadStatus(EMDownloadStatus.FAILED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
            });
        }

        private void downloadFileMainAttchment(EMMessage mMessage, DownloadMessageAttchmentProgress progress, DownloadMessageAttchmentComplite complite)
        {
            EMFileMessageBody body = mMessage.bodies()[0] as EMFileMessageBody;
            body.setDownloadStatus(EMDownloadStatus.DOWNLOADING);
            mMessage.clearBodies();
            mMessage.addBody(body);
            var conversation = EaseHelper.shard.client.getChatManager().conversationWithType(mMessage.conversationId(), DCUtilTool.GetMConversationType(mMessage.chatType()), true);
            conversation.updateMessage(mMessage);
            string dir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao" + "\\ChangLiao\\" + SettingMenager.shard.idCard;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = dir + "\\" + body.displayName();
            var p = path;
            int i = 0;
            while (File.Exists(p))
            {
                i += 1;
                var arr = path.Split('.');
                if (arr.Length > 2)
                {
                    p = "";
                    for (int j = 0; j < arr.Length - 1; j++)
                    {
                        p += arr[j];
                    }
                    p = p + "(" + i + ")." + arr[arr.Length - 1];
                }
                else if (arr.Length == 2)
                {
                    p = arr[0] + "(" + i + ")." + arr[1];
                }
                else
                {
                    p = path + "(" + i + ")";
                }
            }
            HttpUitls.Instance.DownloadFile(body.remotePath(), p, (p1) =>
            {
                if (progress != null)
                {
                    progress(p1);
                }
            }, (b) =>
            {
                if (b)
                {
                    var fi = new FileInfo(path);
                    body.setFileLength(fi.Length);
                    body.setLocalPath(path);
                    body.setDownloadStatus(EMDownloadStatus.SUCCESSED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
                else
                {
                    body.setDownloadStatus(EMDownloadStatus.FAILED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
            });
        }

        private void downloadImageMainAttchment(EMMessage mMessage, DownloadMessageAttchmentProgress progress, DownloadMessageAttchmentComplite complite)
        {
            EMImageMessageBody body = mMessage.bodies()[0] as EMImageMessageBody;
            body.setDownloadStatus(EMDownloadStatus.DOWNLOADING);
            mMessage.clearBodies();
            mMessage.addBody(body);
            var conversation = EaseHelper.shard.client.getChatManager().conversationWithType(mMessage.conversationId(), DCUtilTool.GetMConversationType(mMessage.chatType()), true);
            conversation.updateMessage(mMessage);
            string dir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao" + "\\ChangLiao\\" + SettingMenager.shard.idCard;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = dir + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".jpg";
            HttpUitls.Instance.DownloadFile(body.remotePath(), path, (p) =>
            {
                if (progress != null)
                {
                    progress(p);
                }
            }, (b) =>
            {
                if (b)
                {
                    var fi = new FileInfo(path);
                    body.setFileLength(fi.Length);
                    body.setLocalPath(path);
                    Image image = Image.FromFile(path);
                    body.setSize(new EMImageMessageBody.Size((double)image.Width, (double)image.Height));
                    body.setDownloadStatus(EMDownloadStatus.SUCCESSED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
                else
                {
                    body.setDownloadStatus(EMDownloadStatus.FAILED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
            });
        }

        private void downloadVideoMainAttchment(EMMessage mMessage, DownloadMessageAttchmentProgress progress, DownloadMessageAttchmentComplite complite)
        {
            EMVideoMessageBody body = mMessage.bodies()[0] as EMVideoMessageBody;
            body.setDownloadStatus(EMDownloadStatus.DOWNLOADING);
            mMessage.clearBodies();
            mMessage.addBody(body);
            var conversation = EaseHelper.shard.client.getChatManager().conversationWithType(mMessage.conversationId(), DCUtilTool.GetMConversationType(mMessage.chatType()), true);
            conversation.updateMessage(mMessage);
            string dir = System.Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\Changliao" + "\\ChangLiao\\" + SettingMenager.shard.idCard;
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            string path = dir + "\\" + DateTime.Now.ToString("yyyyMMddHHmmssffff") + ".mp4";
            HttpUitls.Instance.DownloadFile(body.remotePath(), path, (p) =>
            {
                Console.WriteLine(p);
                if (progress != null)
                {
                    progress(p);
                }
            }, (b) =>
            {
                if (b)
                {
                    var fi = new FileInfo(path);
                    body.setFileLength(fi.Length);
                    body.setLocalPath(path);
                    body.setDownloadStatus(EMDownloadStatus.SUCCESSED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
                else
                {
                    body.setDownloadStatus(EMDownloadStatus.FAILED);
                    conversation.updateMessage(mMessage);
                    complite(mMessage);
                }
            });
        }
    }
}
