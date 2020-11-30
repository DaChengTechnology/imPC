namespace ChangLiao.windows
{
    partial class MainFrm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainFrm));
            this.dSkinPen1 = new DSkin.Controls.DSkinPen(this.components);
            this.userInfoPanl = new DSkin.DirectUI.DuiBaseControl();
            this.userHeadBtn = new DSkin.DirectUI.DuiButton();
            this.dSkinTabBar1 = new DSkin.Controls.DSkinTabBar();
            this.conversationTabItem = new DSkin.Controls.DSkinTabItem();
            this.conversationTablePage = new DSkin.Controls.DSkinTabPage();
            this.conversationPanel = new DSkin.Controls.DSkinNewPanel();
            this.conversationListBox = new DSkin.Controls.DSkinListBox();
            this.defualticon = new DSkin.Controls.DSkinPictureBox();
            this.contractTabItem = new DSkin.Controls.DSkinTabItem();
            this.contractTabPage = new DSkin.Controls.DSkinTabPage();
            this.contractOrGroupListBox = new DSkin.Controls.DSkinListBox();
            this.settingTabItem = new DSkin.Controls.DSkinTabItem();
            this.settingTabPage = new DSkin.Controls.DSkinTabPage();
            this.versionLabel = new DSkin.Controls.DSkinLabel();
            this.dSkinButton3 = new DSkin.Controls.DSkinButton();
            this.dSkinLabel2 = new DSkin.Controls.DSkinLabel();
            this.dSkinLabel1 = new DSkin.Controls.DSkinLabel();
            this.dSkinPictureBox1 = new DSkin.Controls.DSkinPictureBox();
            this.mainTabControl = new DSkin.Controls.DSkinTabControl();
            this.searchPanel = new DSkin.Controls.DSkinNewPanel();
            this.addButton = new DSkin.Controls.DSkinButton();
            this.searchTextBox = new DSkin.Controls.DSkinTextBox();
            this.titleLabel = new DSkin.Controls.DSkinLabel();
            this.CLnotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notificatiionContextMenuStrip = new DSkin.Controls.DSkinContextMenuStrip();
            this.退出ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.friendContextMenuStrip = new DSkin.Controls.DSkinContextMenuStrip();
            this.发送消息ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.设置备注ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.删除好友ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupContextMenuStrip = new DSkin.Controls.DSkinContextMenuStrip();
            this.发送消息ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.群详情ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.新的群组ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personalConversationMenuStrip1 = new DSkin.Controls.DSkinContextMenuStrip();
            this.删除会话ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.groupConversaationMenuStrip = new DSkin.Controls.DSkinContextMenuStrip();
            this.删除会话ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.群详情ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.新的群组ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.dSkinButton1 = new DSkin.Controls.DSkinButton();
            this.dSkinButton2 = new DSkin.Controls.DSkinButton();
            this.clearGroupContextMenuStrip1 = new DSkin.Controls.DSkinContextMenuStrip();
            this.新的群组ToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.conversationTablePage.SuspendLayout();
            this.conversationPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.conversationListBox)).BeginInit();
            this.contractTabPage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.contractOrGroupListBox)).BeginInit();
            this.settingTabPage.SuspendLayout();
            this.mainTabControl.SuspendLayout();
            this.searchPanel.SuspendLayout();
            this.notificatiionContextMenuStrip.SuspendLayout();
            this.friendContextMenuStrip.SuspendLayout();
            this.groupContextMenuStrip.SuspendLayout();
            this.personalConversationMenuStrip1.SuspendLayout();
            this.groupConversaationMenuStrip.SuspendLayout();
            this.clearGroupContextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dSkinPen1
            // 
            this.dSkinPen1.CompoundArray = new float[0];
            this.dSkinPen1.DashPattern = new float[0];
            this.dSkinPen1.EndCap = System.Drawing.Drawing2D.LineCap.Flat;
            // 
            // userInfoPanl
            // 
            this.userInfoPanl.AutoSize = false;
            this.userInfoPanl.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(61)))), ((int)(((byte)(72)))), ((int)(((byte)(151)))));
            this.userInfoPanl.Controls.Add(this.userHeadBtn);
            this.userInfoPanl.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userInfoPanl.Location = new System.Drawing.Point(0, 0);
            this.userInfoPanl.Name = "userInfoPanl";
            this.userInfoPanl.Size = new System.Drawing.Size(70, 450);
            // 
            // userHeadBtn
            // 
            this.userHeadBtn.BackgroundImage = global::ChangLiao.Properties.Resources.moren;
            this.userHeadBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.userHeadBtn.BackgroundRender.BorderColor = System.Drawing.Color.Transparent;
            this.userHeadBtn.BackgroundRender.Radius = 10;
            this.userHeadBtn.BackgroundRender.RenderBorders = true;
            this.userHeadBtn.BaseColor = System.Drawing.Color.Transparent;
            this.userHeadBtn.ButtonBorderColor = System.Drawing.Color.Transparent;
            this.userHeadBtn.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.userHeadBtn.HoverColor = System.Drawing.Color.Empty;
            this.userHeadBtn.Location = new System.Drawing.Point(5, 30);
            this.userHeadBtn.Name = "userHeadBtn";
            this.userHeadBtn.PressColor = System.Drawing.Color.Empty;
            this.userHeadBtn.Size = new System.Drawing.Size(60, 60);
            this.userHeadBtn.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.userHeadBtn.MouseClick += new System.EventHandler<DSkin.DirectUI.DuiMouseEventArgs>(this.userHeadBtn_MouseClick);
            // 
            // dSkinTabBar1
            // 
            this.dSkinTabBar1.DMargin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.dSkinTabBar1.EnabledLayoutContent = true;
            this.dSkinTabBar1.Items.AddRange(new DSkin.DirectUI.DuiBaseControl[] {
            this.conversationTabItem,
            this.contractTabItem,
            this.settingTabItem});
            this.dSkinTabBar1.Location = new System.Drawing.Point(7, 132);
            this.dSkinTabBar1.Name = "dSkinTabBar1";
            this.dSkinTabBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.dSkinTabBar1.Size = new System.Drawing.Size(50, 209);
            this.dSkinTabBar1.TabControl = this.mainTabControl;
            this.dSkinTabBar1.TabIndex = 0;
            this.dSkinTabBar1.TabControlSelectedIndexChanged += new System.EventHandler<DSkin.Controls.DSkinTabBarEventArgs>(this.dSkinTabBar1_TabControlSelectedIndexChanged);
            this.dSkinTabBar1.TabIndexChanged += new System.EventHandler(this.dSkinTabBar1_TabIndexChanged);
            // 
            // conversationTabItem
            // 
            this.conversationTabItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.conversationTabItem.ButtonBorderColor = System.Drawing.Color.Transparent;
            this.conversationTabItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.conversationTabItem.HoverColor = System.Drawing.Color.Empty;
            this.conversationTabItem.HoverImage = ((System.Drawing.Image)(resources.GetObject("conversationTabItem.HoverImage")));
            this.conversationTabItem.ImageAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.conversationTabItem.Location = new System.Drawing.Point(0, 10);
            this.conversationTabItem.Margin = new System.Windows.Forms.Padding(0, 10, 0, 5);
            this.conversationTabItem.Name = "conversationTabItem";
            this.conversationTabItem.NormalImage = global::ChangLiao.Properties.Resources.conversation_normal;
            this.conversationTabItem.PressColor = System.Drawing.Color.Empty;
            this.conversationTabItem.PressedImage = global::ChangLiao.Properties.Resources.converstation_select;
            this.conversationTabItem.Size = new System.Drawing.Size(50, 50);
            this.conversationTabItem.TabPage = this.conversationTablePage;
            this.conversationTabItem.Tag = "4";
            this.conversationTabItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // conversationTablePage
            // 
            this.conversationTablePage.BackColor = System.Drawing.Color.Transparent;
            this.conversationTablePage.Controls.Add(this.conversationPanel);
            this.conversationTablePage.Controls.Add(this.defualticon);
            this.conversationTablePage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.conversationTablePage.Location = new System.Drawing.Point(0, 1);
            this.conversationTablePage.Margin = new System.Windows.Forms.Padding(0);
            this.conversationTablePage.Name = "conversationTablePage";
            this.conversationTablePage.Size = new System.Drawing.Size(729, 395);
            this.conversationTablePage.TabIndex = 0;
            this.conversationTablePage.TabItemImage = null;
            this.conversationTablePage.Text = "dSkinTabPage1";
            this.conversationTablePage.SizeChanged += new System.EventHandler(this.conversationTablePage_SizeChanged);
            // 
            // conversationPanel
            // 
            this.conversationPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.conversationPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(187)))), ((int)(((byte)(86)))));
            this.conversationPanel.Controls.Add(this.conversationListBox);
            this.conversationPanel.Location = new System.Drawing.Point(0, 0);
            this.conversationPanel.Name = "conversationPanel";
            this.conversationPanel.Size = new System.Drawing.Size(180, 397);
            this.conversationPanel.TabIndex = 0;
            this.conversationPanel.Text = "dSkinNewPanel1";
            // 
            // conversationListBox
            // 
            this.conversationListBox.BackColor = System.Drawing.Color.White;
            this.conversationListBox.Location = new System.Drawing.Point(0, 0);
            this.conversationListBox.Name = "conversationListBox";
            this.conversationListBox.ScrollBarWidth = 12;
            this.conversationListBox.SelectionMode = DSkin.Controls.SelectionModes.Radio;
            this.conversationListBox.Size = new System.Drawing.Size(180, 395);
            this.conversationListBox.SmoothScroll = true;
            this.conversationListBox.TabIndex = 4;
            this.conversationListBox.Value = 0D;
            this.conversationListBox.ItemClick += new System.EventHandler<DSkin.Controls.ItemClickEventArgs>(this.conversationListBox_ItemClick);
            // 
            // defualticon
            // 
            this.defualticon.Image = global::ChangLiao.Properties.Resources.defualtIclon;
            this.defualticon.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ChangLiao.Properties.Resources.defualtIclon))};
            this.defualticon.Location = new System.Drawing.Point(395, 130);
            this.defualticon.Name = "defualticon";
            this.defualticon.Size = new System.Drawing.Size(100, 100);
            this.defualticon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.defualticon.TabIndex = 4;
            this.defualticon.Text = "dSkinPictureBox1";
            // 
            // contractTabItem
            // 
            this.contractTabItem.ButtonBorderColor = System.Drawing.Color.Transparent;
            this.contractTabItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.contractTabItem.HoverColor = System.Drawing.Color.Empty;
            this.contractTabItem.HoverImage = ((System.Drawing.Image)(resources.GetObject("contractTabItem.HoverImage")));
            this.contractTabItem.IsDrawText = false;
            this.contractTabItem.Location = new System.Drawing.Point(0, 70);
            this.contractTabItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.contractTabItem.Name = "contractTabItem";
            this.contractTabItem.NormalImage = global::ChangLiao.Properties.Resources.contract_normal;
            this.contractTabItem.PressColor = System.Drawing.Color.Empty;
            this.contractTabItem.PressedImage = global::ChangLiao.Properties.Resources.contract_select;
            this.contractTabItem.Size = new System.Drawing.Size(50, 50);
            this.contractTabItem.TabIndex = 1;
            this.contractTabItem.TabPage = this.contractTabPage;
            this.contractTabItem.Tag = "5";
            this.contractTabItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // contractTabPage
            // 
            this.contractTabPage.BackColor = System.Drawing.Color.Transparent;
            this.contractTabPage.Controls.Add(this.contractOrGroupListBox);
            this.contractTabPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.contractTabPage.Location = new System.Drawing.Point(0, 1);
            this.contractTabPage.Margin = new System.Windows.Forms.Padding(0);
            this.contractTabPage.Name = "contractTabPage";
            this.contractTabPage.Size = new System.Drawing.Size(729, 395);
            this.contractTabPage.TabIndex = 1;
            this.contractTabPage.TabItemImage = null;
            this.contractTabPage.Text = "dSkinTabPage2";
            this.contractTabPage.SizeChanged += new System.EventHandler(this.contractTabPage_SizeChanged);
            // 
            // contractOrGroupListBox
            // 
            this.contractOrGroupListBox.BackColor = System.Drawing.Color.White;
            this.contractOrGroupListBox.Location = new System.Drawing.Point(0, 0);
            this.contractOrGroupListBox.Name = "contractOrGroupListBox";
            this.contractOrGroupListBox.ScrollBarWidth = 12;
            this.contractOrGroupListBox.SelectionMode = DSkin.Controls.SelectionModes.Radio;
            this.contractOrGroupListBox.Size = new System.Drawing.Size(180, 395);
            this.contractOrGroupListBox.TabIndex = 0;
            this.contractOrGroupListBox.Text = "dSkinListBox1";
            this.contractOrGroupListBox.Value = 0D;
            this.contractOrGroupListBox.ItemClick += new System.EventHandler<DSkin.Controls.ItemClickEventArgs>(this.contractOrGroupListBox_ItemClick);
            this.contractOrGroupListBox.MouseDown += new System.Windows.Forms.MouseEventHandler(this.contractOrGroupListBox_MouseDown);
            this.contractOrGroupListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.contractOrGroupListBox_MouseUp);
            // 
            // settingTabItem
            // 
            this.settingTabItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.settingTabItem.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.settingTabItem.HoverColor = System.Drawing.Color.Empty;
            this.settingTabItem.HoverImage = ((System.Drawing.Image)(resources.GetObject("settingTabItem.HoverImage")));
            this.settingTabItem.Location = new System.Drawing.Point(0, 130);
            this.settingTabItem.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.settingTabItem.Name = "settingTabItem";
            this.settingTabItem.NormalImage = global::ChangLiao.Properties.Resources.setting_normal;
            this.settingTabItem.PressColor = System.Drawing.Color.Empty;
            this.settingTabItem.PressedImage = global::ChangLiao.Properties.Resources.setting_select;
            this.settingTabItem.Size = new System.Drawing.Size(50, 50);
            this.settingTabItem.TabIndex = 2;
            this.settingTabItem.TabPage = this.settingTabPage;
            this.settingTabItem.Tag = "6";
            this.settingTabItem.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // settingTabPage
            // 
            this.settingTabPage.BackColor = System.Drawing.Color.Transparent;
            this.settingTabPage.Controls.Add(this.versionLabel);
            this.settingTabPage.Controls.Add(this.dSkinButton3);
            this.settingTabPage.Controls.Add(this.dSkinLabel2);
            this.settingTabPage.Controls.Add(this.dSkinLabel1);
            this.settingTabPage.Controls.Add(this.dSkinPictureBox1);
            this.settingTabPage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.settingTabPage.Location = new System.Drawing.Point(0, 1);
            this.settingTabPage.Name = "settingTabPage";
            this.settingTabPage.Size = new System.Drawing.Size(729, 395);
            this.settingTabPage.TabIndex = 2;
            this.settingTabPage.TabItemImage = null;
            this.settingTabPage.Text = "dSkinTabPage3";
            this.settingTabPage.SizeChanged += new System.EventHandler(this.settingTabPage_SizeChanged);
            this.settingTabPage.Layout += new System.Windows.Forms.LayoutEventHandler(this.settingTabPage_Layout);
            // 
            // versionLabel
            // 
            this.versionLabel.ForeColor = System.Drawing.Color.White;
            this.versionLabel.Location = new System.Drawing.Point(306, 340);
            this.versionLabel.Name = "versionLabel";
            this.versionLabel.Size = new System.Drawing.Size(73, 14);
            this.versionLabel.TabIndex = 4;
            this.versionLabel.Text = "dSkinLabel3";
            // 
            // dSkinButton3
            // 
            this.dSkinButton3.BaseColor = System.Drawing.Color.Red;
            this.dSkinButton3.ButtonBorderWidth = 0;
            this.dSkinButton3.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dSkinButton3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinButton3.ForeColor = System.Drawing.Color.White;
            this.dSkinButton3.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton3.HoverImage = null;
            this.dSkinButton3.Location = new System.Drawing.Point(279, 207);
            this.dSkinButton3.Name = "dSkinButton3";
            this.dSkinButton3.NormalImage = null;
            this.dSkinButton3.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton3.PressedImage = null;
            this.dSkinButton3.Radius = 10;
            this.dSkinButton3.ShowButtonBorder = true;
            this.dSkinButton3.Size = new System.Drawing.Size(100, 40);
            this.dSkinButton3.TabIndex = 3;
            this.dSkinButton3.Text = "退出登录";
            this.dSkinButton3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton3.TextPadding = 0;
            this.dSkinButton3.MouseClick += new System.Windows.Forms.MouseEventHandler(this.dSkinButton3_MouseClick);
            // 
            // dSkinLabel2
            // 
            this.dSkinLabel2.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinLabel2.ForeColor = System.Drawing.Color.White;
            this.dSkinLabel2.Location = new System.Drawing.Point(296, 152);
            this.dSkinLabel2.Name = "dSkinLabel2";
            this.dSkinLabel2.Size = new System.Drawing.Size(128, 18);
            this.dSkinLabel2.TabIndex = 2;
            this.dSkinLabel2.Text = "**********************";
            // 
            // dSkinLabel1
            // 
            this.dSkinLabel1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinLabel1.ForeColor = System.Drawing.Color.White;
            this.dSkinLabel1.Location = new System.Drawing.Point(296, 128);
            this.dSkinLabel1.Name = "dSkinLabel1";
            this.dSkinLabel1.Size = new System.Drawing.Size(76, 18);
            this.dSkinLabel1.TabIndex = 1;
            this.dSkinLabel1.Text = "dSkinLabel1";
            // 
            // dSkinPictureBox1
            // 
            this.dSkinPictureBox1.DuiBackgroundRender.BorderColor = System.Drawing.Color.Transparent;
            this.dSkinPictureBox1.DuiBackgroundRender.Radius = 10;
            this.dSkinPictureBox1.DuiBackgroundRender.RenderBorders = true;
            this.dSkinPictureBox1.Image = global::ChangLiao.Properties.Resources.moren;
            this.dSkinPictureBox1.Images = new System.Drawing.Image[] {
        ((System.Drawing.Image)(global::ChangLiao.Properties.Resources.moren))};
            this.dSkinPictureBox1.Location = new System.Drawing.Point(279, 0);
            this.dSkinPictureBox1.Name = "dSkinPictureBox1";
            this.dSkinPictureBox1.Size = new System.Drawing.Size(100, 100);
            this.dSkinPictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.dSkinPictureBox1.TabIndex = 0;
            this.dSkinPictureBox1.Text = "dSkinPictureBox1";
            // 
            // mainTabControl
            // 
            this.mainTabControl.BitmapCache = false;
            this.mainTabControl.Borders.AllWidth = 0;
            this.mainTabControl.Borders.BottomWidth = 0;
            this.mainTabControl.Borders.LeftWidth = 0;
            this.mainTabControl.Borders.RightWidth = 0;
            this.mainTabControl.Borders.TopWidth = 0;
            this.mainTabControl.Controls.Add(this.conversationTablePage);
            this.mainTabControl.Controls.Add(this.contractTabPage);
            this.mainTabControl.Controls.Add(this.settingTabPage);
            this.mainTabControl.DividingLineWidth = 0;
            this.mainTabControl.HoverBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))))};
            this.mainTabControl.ItemBackgroundImage = null;
            this.mainTabControl.ItemBackgroundImageHover = null;
            this.mainTabControl.ItemBackgroundImageSelected = null;
            this.mainTabControl.ItemSize = new System.Drawing.Size(0, 1);
            this.mainTabControl.Location = new System.Drawing.Point(70, 53);
            this.mainTabControl.Margin = new System.Windows.Forms.Padding(0);
            this.mainTabControl.Name = "mainTabControl";
            this.mainTabControl.NormalBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))))};
            this.mainTabControl.Padding = new System.Drawing.Point(0, 0);
            this.mainTabControl.PageImagePosition = DSkin.Controls.ePageImagePosition.Left;
            this.mainTabControl.SelectedBackColors = new System.Drawing.Color[] {
        System.Drawing.Color.White};
            this.mainTabControl.Size = new System.Drawing.Size(729, 396);
            this.mainTabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.mainTabControl.TabIndex = 1;
            this.mainTabControl.UpdownBtnArrowNormalColor = System.Drawing.Color.Black;
            this.mainTabControl.UpdownBtnArrowPressColor = System.Drawing.Color.Gray;
            this.mainTabControl.UpdownBtnBackColor = System.Drawing.Color.White;
            this.mainTabControl.UpdownBtnBorderColor = System.Drawing.Color.Black;
            this.mainTabControl.SizeChanged += new System.EventHandler(this.dSkinTabControl1_SelectedIndexChanged);
            // 
            // searchPanel
            // 
            this.searchPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(187)))), ((int)(((byte)(86)))));
            this.searchPanel.Controls.Add(this.addButton);
            this.searchPanel.Controls.Add(this.searchTextBox);
            this.searchPanel.Location = new System.Drawing.Point(70, 0);
            this.searchPanel.Name = "searchPanel";
            this.searchPanel.Size = new System.Drawing.Size(180, 53);
            this.searchPanel.TabIndex = 2;
            this.searchPanel.Text = "dSkinNewPanel1";
            // 
            // addButton
            // 
            this.addButton.BackgroundImage = global::ChangLiao.Properties.Resources.addButton;
            this.addButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.addButton.BaseColor = System.Drawing.Color.Transparent;
            this.addButton.ButtonBorderColor = System.Drawing.Color.Transparent;
            this.addButton.ButtonBorderWidth = 1;
            this.addButton.DialogResult = System.Windows.Forms.DialogResult.None;
            this.addButton.HoverColor = System.Drawing.Color.Empty;
            this.addButton.HoverImage = null;
            this.addButton.Location = new System.Drawing.Point(140, 10);
            this.addButton.Name = "addButton";
            this.addButton.NormalImage = null;
            this.addButton.PressColor = System.Drawing.Color.Empty;
            this.addButton.PressedImage = null;
            this.addButton.Radius = 10;
            this.addButton.ShowButtonBorder = true;
            this.addButton.Size = new System.Drawing.Size(32, 32);
            this.addButton.TabIndex = 4;
            this.addButton.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.addButton.TextPadding = 0;
            this.addButton.Click += new System.EventHandler(this.addButton_Click);
            // 
            // searchTextBox
            // 
            this.searchTextBox.BitmapCache = false;
            this.searchTextBox.Font = new System.Drawing.Font("微软雅黑", 14F);
            this.searchTextBox.Location = new System.Drawing.Point(5, 10);
            this.searchTextBox.Name = "searchTextBox";
            this.searchTextBox.Size = new System.Drawing.Size(130, 32);
            this.searchTextBox.TabIndex = 3;
            this.searchTextBox.TransparencyKey = System.Drawing.Color.Empty;
            this.searchTextBox.WaterFont = new System.Drawing.Font("微软雅黑", 13F);
            this.searchTextBox.WaterText = "搜索";
            this.searchTextBox.WaterTextOffset = new System.Drawing.Point(0, 2);
            this.searchTextBox.TextChanged += new System.EventHandler(this.searchTextBox_TextChanged);
            this.searchTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.searchTextBox_KeyDown);
            this.searchTextBox.Leave += new System.EventHandler(this.searchTextBox_Leave);
            // 
            // titleLabel
            // 
            this.titleLabel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.titleLabel.Location = new System.Drawing.Point(256, 14);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(126, 28);
            this.titleLabel.TabIndex = 3;
            this.titleLabel.Text = "dSkinLabel1";
            // 
            // CLnotifyIcon
            // 
            this.CLnotifyIcon.ContextMenuStrip = this.notificatiionContextMenuStrip;
            this.CLnotifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("CLnotifyIcon.Icon")));
            this.CLnotifyIcon.Text = "notifyIcon1";
            this.CLnotifyIcon.Visible = true;
            this.CLnotifyIcon.BalloonTipShown += new System.EventHandler(this.CLnotifyIcon_BalloonTipShown);
            this.CLnotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this.CLnotifyIcon_MouseClick);
            this.CLnotifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.CLnotifyIcon_MouseDoubleClick);
            this.CLnotifyIcon.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CLnotifyIcon_MouseMove);
            // 
            // notificatiionContextMenuStrip
            // 
            this.notificatiionContextMenuStrip.Arrow = System.Drawing.Color.Black;
            this.notificatiionContextMenuStrip.Back = System.Drawing.Color.White;
            this.notificatiionContextMenuStrip.BackRadius = 4;
            this.notificatiionContextMenuStrip.Base = System.Drawing.Color.White;
            this.notificatiionContextMenuStrip.CheckedImage = null;
            this.notificatiionContextMenuStrip.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.notificatiionContextMenuStrip.Fore = System.Drawing.Color.Black;
            this.notificatiionContextMenuStrip.HoverFore = System.Drawing.Color.White;
            this.notificatiionContextMenuStrip.ItemAnamorphosis = true;
            this.notificatiionContextMenuStrip.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.notificatiionContextMenuStrip.ItemBorderShow = false;
            this.notificatiionContextMenuStrip.ItemHover = System.Drawing.Color.Gold;
            this.notificatiionContextMenuStrip.ItemPressed = System.Drawing.Color.Gold;
            this.notificatiionContextMenuStrip.ItemRadius = 4;
            this.notificatiionContextMenuStrip.ItemRadiusStyle = DSkin.Common.RoundStyle.All;
            this.notificatiionContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.退出ToolStripMenuItem});
            this.notificatiionContextMenuStrip.ItemSplitter = System.Drawing.Color.WhiteSmoke;
            this.notificatiionContextMenuStrip.Name = "notificatiionContextMenuStrip";
            this.notificatiionContextMenuStrip.RadiusStyle = DSkin.Common.RoundStyle.All;
            this.notificatiionContextMenuStrip.Size = new System.Drawing.Size(101, 26);
            this.notificatiionContextMenuStrip.SkinAllColor = true;
            this.notificatiionContextMenuStrip.TitleAnamorphosis = true;
            this.notificatiionContextMenuStrip.TitleColor = System.Drawing.Color.White;
            this.notificatiionContextMenuStrip.TitleRadius = 4;
            this.notificatiionContextMenuStrip.TitleRadiusStyle = DSkin.Common.RoundStyle.All;
            // 
            // 退出ToolStripMenuItem
            // 
            this.退出ToolStripMenuItem.Name = "退出ToolStripMenuItem";
            this.退出ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.退出ToolStripMenuItem.Text = "退出";
            this.退出ToolStripMenuItem.Click += new System.EventHandler(this.退出ToolStripMenuItem_Click);
            // 
            // friendContextMenuStrip
            // 
            this.friendContextMenuStrip.Arrow = System.Drawing.Color.Black;
            this.friendContextMenuStrip.Back = System.Drawing.Color.White;
            this.friendContextMenuStrip.BackRadius = 4;
            this.friendContextMenuStrip.Base = System.Drawing.Color.White;
            this.friendContextMenuStrip.CheckedImage = null;
            this.friendContextMenuStrip.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.friendContextMenuStrip.Fore = System.Drawing.Color.Black;
            this.friendContextMenuStrip.HoverFore = System.Drawing.Color.Black;
            this.friendContextMenuStrip.ItemAnamorphosis = true;
            this.friendContextMenuStrip.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.friendContextMenuStrip.ItemBorderShow = false;
            this.friendContextMenuStrip.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.friendContextMenuStrip.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.friendContextMenuStrip.ItemRadius = 1;
            this.friendContextMenuStrip.ItemRadiusStyle = DSkin.Common.RoundStyle.None;
            this.friendContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.发送消息ToolStripMenuItem,
            this.设置备注ToolStripMenuItem,
            this.删除好友ToolStripMenuItem});
            this.friendContextMenuStrip.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.friendContextMenuStrip.Name = "friendContextMenuStrip";
            this.friendContextMenuStrip.RadiusStyle = DSkin.Common.RoundStyle.All;
            this.friendContextMenuStrip.Size = new System.Drawing.Size(125, 70);
            this.friendContextMenuStrip.SkinAllColor = true;
            this.friendContextMenuStrip.TitleAnamorphosis = true;
            this.friendContextMenuStrip.TitleColor = System.Drawing.Color.Transparent;
            this.friendContextMenuStrip.TitleRadius = 4;
            this.friendContextMenuStrip.TitleRadiusStyle = DSkin.Common.RoundStyle.All;
            this.friendContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.friendContextMenuStrip_ItemClicked);
            // 
            // 发送消息ToolStripMenuItem
            // 
            this.发送消息ToolStripMenuItem.Name = "发送消息ToolStripMenuItem";
            this.发送消息ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.发送消息ToolStripMenuItem.Text = "发送消息";
            // 
            // 设置备注ToolStripMenuItem
            // 
            this.设置备注ToolStripMenuItem.Name = "设置备注ToolStripMenuItem";
            this.设置备注ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.设置备注ToolStripMenuItem.Text = "设置备注";
            // 
            // 删除好友ToolStripMenuItem
            // 
            this.删除好友ToolStripMenuItem.Name = "删除好友ToolStripMenuItem";
            this.删除好友ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除好友ToolStripMenuItem.Text = "删除好友";
            // 
            // groupContextMenuStrip
            // 
            this.groupContextMenuStrip.Arrow = System.Drawing.Color.Black;
            this.groupContextMenuStrip.Back = System.Drawing.Color.White;
            this.groupContextMenuStrip.BackRadius = 4;
            this.groupContextMenuStrip.Base = System.Drawing.Color.White;
            this.groupContextMenuStrip.CheckedImage = null;
            this.groupContextMenuStrip.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.groupContextMenuStrip.Fore = System.Drawing.Color.Black;
            this.groupContextMenuStrip.HoverFore = System.Drawing.Color.Black;
            this.groupContextMenuStrip.ItemAnamorphosis = true;
            this.groupContextMenuStrip.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.groupContextMenuStrip.ItemBorderShow = false;
            this.groupContextMenuStrip.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.groupContextMenuStrip.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.groupContextMenuStrip.ItemRadius = 4;
            this.groupContextMenuStrip.ItemRadiusStyle = DSkin.Common.RoundStyle.None;
            this.groupContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.发送消息ToolStripMenuItem1,
            this.群详情ToolStripMenuItem,
            this.新的群组ToolStripMenuItem});
            this.groupContextMenuStrip.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.groupContextMenuStrip.Name = "groupContextMenuStrip";
            this.groupContextMenuStrip.RadiusStyle = DSkin.Common.RoundStyle.All;
            this.groupContextMenuStrip.Size = new System.Drawing.Size(125, 70);
            this.groupContextMenuStrip.SkinAllColor = true;
            this.groupContextMenuStrip.TitleAnamorphosis = true;
            this.groupContextMenuStrip.TitleColor = System.Drawing.Color.Transparent;
            this.groupContextMenuStrip.TitleRadius = 4;
            this.groupContextMenuStrip.TitleRadiusStyle = DSkin.Common.RoundStyle.All;
            this.groupContextMenuStrip.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.groupContextMenuStrip_ItemClicked);
            // 
            // 发送消息ToolStripMenuItem1
            // 
            this.发送消息ToolStripMenuItem1.Name = "发送消息ToolStripMenuItem1";
            this.发送消息ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.发送消息ToolStripMenuItem1.Text = "发送消息";
            // 
            // 群详情ToolStripMenuItem
            // 
            this.群详情ToolStripMenuItem.Name = "群详情ToolStripMenuItem";
            this.群详情ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.群详情ToolStripMenuItem.Text = "群详情";
            // 
            // 新的群组ToolStripMenuItem
            // 
            this.新的群组ToolStripMenuItem.Name = "新的群组ToolStripMenuItem";
            this.新的群组ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.新的群组ToolStripMenuItem.Text = "新的群组";
            // 
            // personalConversationMenuStrip1
            // 
            this.personalConversationMenuStrip1.Arrow = System.Drawing.Color.Black;
            this.personalConversationMenuStrip1.Back = System.Drawing.Color.White;
            this.personalConversationMenuStrip1.BackRadius = 4;
            this.personalConversationMenuStrip1.Base = System.Drawing.Color.White;
            this.personalConversationMenuStrip1.CheckedImage = null;
            this.personalConversationMenuStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.personalConversationMenuStrip1.Fore = System.Drawing.Color.Black;
            this.personalConversationMenuStrip1.HoverFore = System.Drawing.Color.White;
            this.personalConversationMenuStrip1.ItemAnamorphosis = true;
            this.personalConversationMenuStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.personalConversationMenuStrip1.ItemBorderShow = false;
            this.personalConversationMenuStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.personalConversationMenuStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.personalConversationMenuStrip1.ItemRadius = 4;
            this.personalConversationMenuStrip1.ItemRadiusStyle = DSkin.Common.RoundStyle.All;
            this.personalConversationMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除会话ToolStripMenuItem});
            this.personalConversationMenuStrip1.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.personalConversationMenuStrip1.Name = "personalConversationMenuStrip1";
            this.personalConversationMenuStrip1.RadiusStyle = DSkin.Common.RoundStyle.All;
            this.personalConversationMenuStrip1.Size = new System.Drawing.Size(125, 26);
            this.personalConversationMenuStrip1.SkinAllColor = true;
            this.personalConversationMenuStrip1.TitleAnamorphosis = true;
            this.personalConversationMenuStrip1.TitleColor = System.Drawing.Color.Transparent;
            this.personalConversationMenuStrip1.TitleRadius = 4;
            this.personalConversationMenuStrip1.TitleRadiusStyle = DSkin.Common.RoundStyle.All;
            // 
            // 删除会话ToolStripMenuItem
            // 
            this.删除会话ToolStripMenuItem.Name = "删除会话ToolStripMenuItem";
            this.删除会话ToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
            this.删除会话ToolStripMenuItem.Text = "删除会话";
            // 
            // groupConversaationMenuStrip
            // 
            this.groupConversaationMenuStrip.Arrow = System.Drawing.Color.Black;
            this.groupConversaationMenuStrip.Back = System.Drawing.Color.White;
            this.groupConversaationMenuStrip.BackRadius = 4;
            this.groupConversaationMenuStrip.Base = System.Drawing.Color.White;
            this.groupConversaationMenuStrip.CheckedImage = null;
            this.groupConversaationMenuStrip.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.groupConversaationMenuStrip.Fore = System.Drawing.Color.Black;
            this.groupConversaationMenuStrip.HoverFore = System.Drawing.Color.White;
            this.groupConversaationMenuStrip.ItemAnamorphosis = true;
            this.groupConversaationMenuStrip.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.groupConversaationMenuStrip.ItemBorderShow = false;
            this.groupConversaationMenuStrip.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.groupConversaationMenuStrip.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.groupConversaationMenuStrip.ItemRadius = 4;
            this.groupConversaationMenuStrip.ItemRadiusStyle = DSkin.Common.RoundStyle.All;
            this.groupConversaationMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.删除会话ToolStripMenuItem1,
            this.群详情ToolStripMenuItem1,
            this.新的群组ToolStripMenuItem1});
            this.groupConversaationMenuStrip.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.groupConversaationMenuStrip.Name = "groupConversaationMenuStrip";
            this.groupConversaationMenuStrip.RadiusStyle = DSkin.Common.RoundStyle.All;
            this.groupConversaationMenuStrip.Size = new System.Drawing.Size(125, 70);
            this.groupConversaationMenuStrip.SkinAllColor = true;
            this.groupConversaationMenuStrip.TitleAnamorphosis = true;
            this.groupConversaationMenuStrip.TitleColor = System.Drawing.Color.Transparent;
            this.groupConversaationMenuStrip.TitleRadius = 4;
            this.groupConversaationMenuStrip.TitleRadiusStyle = DSkin.Common.RoundStyle.All;
            this.groupConversaationMenuStrip.Opening += new System.ComponentModel.CancelEventHandler(this.dSkinContextMenuStrip1_Opening);
            // 
            // 删除会话ToolStripMenuItem1
            // 
            this.删除会话ToolStripMenuItem1.Name = "删除会话ToolStripMenuItem1";
            this.删除会话ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.删除会话ToolStripMenuItem1.Text = "删除会话";
            // 
            // 群详情ToolStripMenuItem1
            // 
            this.群详情ToolStripMenuItem1.Name = "群详情ToolStripMenuItem1";
            this.群详情ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.群详情ToolStripMenuItem1.Text = "群详情";
            // 
            // 新的群组ToolStripMenuItem1
            // 
            this.新的群组ToolStripMenuItem1.Name = "新的群组ToolStripMenuItem1";
            this.新的群组ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.新的群组ToolStripMenuItem1.Text = "新的群组";
            // 
            // dSkinButton1
            // 
            this.dSkinButton1.BaseColor = System.Drawing.Color.Red;
            this.dSkinButton1.ButtonBorderWidth = 0;
            this.dSkinButton1.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dSkinButton1.DuiBackgroundRender.BorderWidth = 0;
            this.dSkinButton1.DuiBackgroundRender.Radius = 23;
            this.dSkinButton1.DuiBackgroundRender.RenderBorders = true;
            this.dSkinButton1.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.dSkinButton1.ForeColor = System.Drawing.Color.White;
            this.dSkinButton1.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton1.HoverImage = null;
            this.dSkinButton1.Location = new System.Drawing.Point(40, 132);
            this.dSkinButton1.Name = "dSkinButton1";
            this.dSkinButton1.NormalImage = null;
            this.dSkinButton1.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton1.PressedImage = null;
            this.dSkinButton1.Radius = 23;
            this.dSkinButton1.ShowButtonBorder = true;
            this.dSkinButton1.Size = new System.Drawing.Size(24, 24);
            this.dSkinButton1.TabIndex = 4;
            this.dSkinButton1.Text = "1";
            this.dSkinButton1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton1.TextPadding = 0;
            // 
            // dSkinButton2
            // 
            this.dSkinButton2.BaseColor = System.Drawing.Color.Red;
            this.dSkinButton2.ButtonBorderWidth = 0;
            this.dSkinButton2.DialogResult = System.Windows.Forms.DialogResult.None;
            this.dSkinButton2.ForeColor = System.Drawing.Color.White;
            this.dSkinButton2.HoverColor = System.Drawing.Color.Empty;
            this.dSkinButton2.HoverImage = null;
            this.dSkinButton2.Location = new System.Drawing.Point(40, 196);
            this.dSkinButton2.Name = "dSkinButton2";
            this.dSkinButton2.NormalImage = null;
            this.dSkinButton2.PressColor = System.Drawing.Color.Empty;
            this.dSkinButton2.PressedImage = null;
            this.dSkinButton2.Radius = 23;
            this.dSkinButton2.ShowButtonBorder = true;
            this.dSkinButton2.Size = new System.Drawing.Size(24, 24);
            this.dSkinButton2.TabIndex = 5;
            this.dSkinButton2.Text = "1";
            this.dSkinButton2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.dSkinButton2.TextPadding = 0;
            // 
            // clearGroupContextMenuStrip1
            // 
            this.clearGroupContextMenuStrip1.Arrow = System.Drawing.Color.Black;
            this.clearGroupContextMenuStrip1.Back = System.Drawing.Color.White;
            this.clearGroupContextMenuStrip1.BackRadius = 4;
            this.clearGroupContextMenuStrip1.Base = System.Drawing.Color.White;
            this.clearGroupContextMenuStrip1.CheckedImage = null;
            this.clearGroupContextMenuStrip1.DropDownImageSeparator = System.Drawing.Color.FromArgb(((int)(((byte)(197)))), ((int)(((byte)(197)))), ((int)(((byte)(197)))));
            this.clearGroupContextMenuStrip1.Fore = System.Drawing.Color.Black;
            this.clearGroupContextMenuStrip1.HoverFore = System.Drawing.Color.White;
            this.clearGroupContextMenuStrip1.ItemAnamorphosis = true;
            this.clearGroupContextMenuStrip1.ItemBorder = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(148)))), ((int)(((byte)(212)))));
            this.clearGroupContextMenuStrip1.ItemBorderShow = false;
            this.clearGroupContextMenuStrip1.ItemHover = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.clearGroupContextMenuStrip1.ItemPressed = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.clearGroupContextMenuStrip1.ItemRadius = 4;
            this.clearGroupContextMenuStrip1.ItemRadiusStyle = DSkin.Common.RoundStyle.All;
            this.clearGroupContextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.新的群组ToolStripMenuItem2});
            this.clearGroupContextMenuStrip1.ItemSplitter = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(234)))), ((int)(((byte)(234)))));
            this.clearGroupContextMenuStrip1.Name = "clearGroupContextMenuStrip1";
            this.clearGroupContextMenuStrip1.RadiusStyle = DSkin.Common.RoundStyle.All;
            this.clearGroupContextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            this.clearGroupContextMenuStrip1.SkinAllColor = true;
            this.clearGroupContextMenuStrip1.TitleAnamorphosis = true;
            this.clearGroupContextMenuStrip1.TitleColor = System.Drawing.Color.White;
            this.clearGroupContextMenuStrip1.TitleRadius = 4;
            this.clearGroupContextMenuStrip1.TitleRadiusStyle = DSkin.Common.RoundStyle.All;
            // 
            // 新的群组ToolStripMenuItem2
            // 
            this.新的群组ToolStripMenuItem2.Name = "新的群组ToolStripMenuItem2";
            this.新的群组ToolStripMenuItem2.Size = new System.Drawing.Size(124, 22);
            this.新的群组ToolStripMenuItem2.Text = "新的群组";
            this.新的群组ToolStripMenuItem2.Click += new System.EventHandler(this.新的群组ToolStripMenuItem2_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // MainFrm
            // 
            this.AnimationType = DSkin.Forms.AnimationTypes.GradualCurtainEffect;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(138)))), ((int)(((byte)(226)))));
            this.BorderColor = System.Drawing.Color.Black;
            this.BorderWidth = 0;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.dSkinButton2);
            this.Controls.Add(this.dSkinButton1);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.searchPanel);
            this.Controls.Add(this.mainTabControl);
            this.Controls.Add(this.dSkinTabBar1);
            this.DragChangeBackImage = false;
            this.DrawIcon = false;
            this.DUIControls.Add(this.userInfoPanl);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimumSize = new System.Drawing.Size(600, 350);
            this.Name = "MainFrm";
            this.Padding = new System.Windows.Forms.Padding(0);
            this.Radius = 20;
            this.ShadowColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ShowIcon = false;
            this.ShowShadow = true;
            this.Text = "";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainFrm_FormClosing);
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainFrm_FormClosed);
            this.Load += new System.EventHandler(this.MainFrm_Load);
            this.SizeChanged += new System.EventHandler(this.MainFrm_SizeChanged);
            this.conversationTablePage.ResumeLayout(false);
            this.conversationPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.conversationListBox)).EndInit();
            this.contractTabPage.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.contractOrGroupListBox)).EndInit();
            this.settingTabPage.ResumeLayout(false);
            this.settingTabPage.PerformLayout();
            this.mainTabControl.ResumeLayout(false);
            this.searchPanel.ResumeLayout(false);
            this.searchPanel.PerformLayout();
            this.notificatiionContextMenuStrip.ResumeLayout(false);
            this.friendContextMenuStrip.ResumeLayout(false);
            this.groupContextMenuStrip.ResumeLayout(false);
            this.personalConversationMenuStrip1.ResumeLayout(false);
            this.groupConversaationMenuStrip.ResumeLayout(false);
            this.clearGroupContextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DSkin.Controls.DSkinPen dSkinPen1;
        private DSkin.DirectUI.DuiBaseControl userInfoPanl;
        private DSkin.DirectUI.DuiButton userHeadBtn;
        private DSkin.Controls.DSkinTabBar dSkinTabBar1;
        private DSkin.Controls.DSkinTabItem conversationTabItem;
        private DSkin.Controls.DSkinTabItem contractTabItem;
        private DSkin.Controls.DSkinTabItem settingTabItem;
        private DSkin.Controls.DSkinTabPage conversationTablePage;
        private DSkin.Controls.DSkinTabPage contractTabPage;
        private DSkin.Controls.DSkinTabPage settingTabPage;
        private DSkin.Controls.DSkinTabControl mainTabControl;
        private DSkin.Controls.DSkinNewPanel conversationPanel;
        private DSkin.Controls.DSkinListBox conversationListBox;
        private DSkin.Controls.DSkinPictureBox defualticon;
        private DSkin.Controls.DSkinNewPanel searchPanel;
        private DSkin.Controls.DSkinButton addButton;
        private DSkin.Controls.DSkinTextBox searchTextBox;
        private DSkin.Controls.DSkinLabel titleLabel;
        private System.Windows.Forms.NotifyIcon CLnotifyIcon;
        private DSkin.Controls.DSkinListBox contractOrGroupListBox;
        private DSkin.Controls.DSkinContextMenuStrip friendContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 发送消息ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 设置备注ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 删除好友ToolStripMenuItem;
        private DSkin.Controls.DSkinContextMenuStrip groupContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 发送消息ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 群详情ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 新的群组ToolStripMenuItem;
        private DSkin.Controls.DSkinContextMenuStrip personalConversationMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 删除会话ToolStripMenuItem;
        private DSkin.Controls.DSkinContextMenuStrip groupConversaationMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 删除会话ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 新的群组ToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem 群详情ToolStripMenuItem1;
        private DSkin.Controls.DSkinButton dSkinButton1;
        private DSkin.Controls.DSkinButton dSkinButton2;
        private DSkin.Controls.DSkinPictureBox dSkinPictureBox1;
        private DSkin.Controls.DSkinLabel dSkinLabel2;
        private DSkin.Controls.DSkinLabel dSkinLabel1;
        private DSkin.Controls.DSkinButton dSkinButton3;
        private DSkin.Controls.DSkinContextMenuStrip notificatiionContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem 退出ToolStripMenuItem;
        private DSkin.Controls.DSkinContextMenuStrip clearGroupContextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 新的群组ToolStripMenuItem2;
        private System.Windows.Forms.Timer timer1;
        private DSkin.Controls.DSkinLabel versionLabel;
    }
}