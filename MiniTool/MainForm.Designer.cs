namespace MiniTool
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.MainToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.btnRefresh = new System.Windows.Forms.Button();
            this.btnSelectAll = new System.Windows.Forms.Button();
            this.btnOpenTarget = new System.Windows.Forms.Button();
            this.btnOpenSource = new System.Windows.Forms.Button();
            this.btnStart = new System.Windows.Forms.Button();
            this.chkSyncName = new System.Windows.Forms.CheckBox();
            this.MoveImageList = new System.Windows.Forms.ImageList(this.components);
            this.MoveListMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuInvertSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuViewExif = new System.Windows.Forms.ToolStripMenuItem();
            this.SourceBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.TargetBrowser = new System.Windows.Forms.FolderBrowserDialog();
            this.StartupBGWorker = new System.ComponentModel.BackgroundWorker();
            this.MoveBGWorker = new System.ComponentModel.BackgroundWorker();
            this.LoadImageBGWorker = new System.ComponentModel.BackgroundWorker();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.MenuFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpenSource = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuOpenTarget = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuRun = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuTool = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuMoveType = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuIsCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuIsMove = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuYearType = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuYear3 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuYear4 = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuIsSync = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.StatusBar = new System.Windows.Forms.ToolStripProgressBar();
            this.StatusText = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.TopSplitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtTargetPath = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.radioCopy = new System.Windows.Forms.RadioButton();
            this.radioMove = new System.Windows.Forms.RadioButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.txtSourcePath = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.radioYear4 = new System.Windows.Forms.RadioButton();
            this.radioYear3 = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.FileListView = new extensions.NativeListView();
            this.cboxFolderPrefix = new System.Windows.Forms.ComboBox();
            this.MoveListMenu.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).BeginInit();
            this.TopSplitContainer.Panel1.SuspendLayout();
            this.TopSplitContainer.Panel2.SuspendLayout();
            this.TopSplitContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainToolTip
            // 
            this.MainToolTip.AutoPopDelay = 5000;
            this.MainToolTip.InitialDelay = 100;
            this.MainToolTip.ReshowDelay = 100;
            // 
            // btnRefresh
            // 
            this.btnRefresh.AutoSize = true;
            this.btnRefresh.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnRefresh.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRefresh.Image = global::MiniTool.Properties.Resources.refresh;
            this.btnRefresh.Location = new System.Drawing.Point(9, 97);
            this.btnRefresh.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(24, 24);
            this.btnRefresh.TabIndex = 4;
            this.MainToolTip.SetToolTip(this.btnRefresh, "重新整理列表");
            this.btnRefresh.UseVisualStyleBackColor = true;
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // btnSelectAll
            // 
            this.btnSelectAll.AutoSize = true;
            this.btnSelectAll.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnSelectAll.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSelectAll.Image = global::MiniTool.Properties.Resources.check;
            this.btnSelectAll.Location = new System.Drawing.Point(50, 97);
            this.btnSelectAll.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnSelectAll.Name = "btnSelectAll";
            this.btnSelectAll.Size = new System.Drawing.Size(24, 24);
            this.btnSelectAll.TabIndex = 5;
            this.MainToolTip.SetToolTip(this.btnSelectAll, "選擇全部影像");
            this.btnSelectAll.UseVisualStyleBackColor = true;
            this.btnSelectAll.Click += new System.EventHandler(this.MenuSelectAll_Click);
            // 
            // btnOpenTarget
            // 
            this.btnOpenTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenTarget.AutoSize = true;
            this.btnOpenTarget.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenTarget.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenTarget.Location = new System.Drawing.Point(580, 79);
            this.btnOpenTarget.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenTarget.Name = "btnOpenTarget";
            this.btnOpenTarget.Size = new System.Drawing.Size(28, 27);
            this.btnOpenTarget.TabIndex = 9;
            this.btnOpenTarget.Text = "...";
            this.MainToolTip.SetToolTip(this.btnOpenTarget, "選擇目的資料夾");
            this.btnOpenTarget.UseVisualStyleBackColor = true;
            this.btnOpenTarget.Click += new System.EventHandler(this.OpenTargetPathClick);
            // 
            // btnOpenSource
            // 
            this.btnOpenSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnOpenSource.AutoSize = true;
            this.btnOpenSource.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnOpenSource.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOpenSource.Location = new System.Drawing.Point(580, 16);
            this.btnOpenSource.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOpenSource.Name = "btnOpenSource";
            this.btnOpenSource.Size = new System.Drawing.Size(28, 27);
            this.btnOpenSource.TabIndex = 2;
            this.btnOpenSource.Text = "...";
            this.MainToolTip.SetToolTip(this.btnOpenSource, "選擇來源資料夾");
            this.btnOpenSource.UseVisualStyleBackColor = true;
            this.btnOpenSource.Click += new System.EventHandler(this.OpenSourcePathClick);
            // 
            // btnStart
            // 
            this.btnStart.AutoSize = true;
            this.btnStart.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.btnStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStart.Image = global::MiniTool.Properties.Resources.start;
            this.btnStart.Location = new System.Drawing.Point(185, 26);
            this.btnStart.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(83, 83);
            this.btnStart.TabIndex = 6;
            this.MainToolTip.SetToolTip(this.btnStart, "開始處理");
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.StartClick);
            // 
            // chkSyncName
            // 
            this.chkSyncName.AutoSize = true;
            this.chkSyncName.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.chkSyncName.Location = new System.Drawing.Point(9, 68);
            this.chkSyncName.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.chkSyncName.Name = "chkSyncName";
            this.chkSyncName.Size = new System.Drawing.Size(119, 19);
            this.chkSyncName.TabIndex = 3;
            this.chkSyncName.Text = "自訂名稱同步變動";
            this.MainToolTip.SetToolTip(this.chkSyncName, "此項若勾選，在列表中「自訂資料夾名稱」若\r\n有變動，會同步更新該項底下所有的名稱。\r\n\r\n若不勾選，則只會更新底下沒有自訂名稱的項\r\n目。");
            this.chkSyncName.UseVisualStyleBackColor = true;
            this.chkSyncName.CheckedChanged += new System.EventHandler(this.chkSyncName_CheckedChanged);
            // 
            // MoveImageList
            // 
            this.MoveImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.MoveImageList.ImageSize = new System.Drawing.Size(28, 28);
            this.MoveImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // MoveListMenu
            // 
            this.MoveListMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSelectAll,
            this.MenuInvertSelect,
            this.toolStripSeparator1,
            this.MenuViewExif});
            this.MoveListMenu.Name = "MoveListMenu";
            this.MoveListMenu.Size = new System.Drawing.Size(185, 76);
            this.MoveListMenu.Opening += new System.ComponentModel.CancelEventHandler(this.MoveListMenu_Opening);
            // 
            // MenuSelectAll
            // 
            this.MenuSelectAll.Name = "MenuSelectAll";
            this.MenuSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.MenuSelectAll.Size = new System.Drawing.Size(184, 22);
            this.MenuSelectAll.Text = "全選";
            this.MenuSelectAll.Click += new System.EventHandler(this.MenuSelectAll_Click);
            // 
            // MenuInvertSelect
            // 
            this.MenuInvertSelect.Name = "MenuInvertSelect";
            this.MenuInvertSelect.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.I)));
            this.MenuInvertSelect.Size = new System.Drawing.Size(184, 22);
            this.MenuInvertSelect.Text = "反向選擇";
            this.MenuInvertSelect.Click += new System.EventHandler(this.MenuInvertSelect_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(181, 6);
            // 
            // MenuViewExif
            // 
            this.MenuViewExif.Name = "MenuViewExif";
            this.MenuViewExif.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.E)));
            this.MenuViewExif.Size = new System.Drawing.Size(184, 22);
            this.MenuViewExif.Text = "檢視完整Exif";
            this.MenuViewExif.Click += new System.EventHandler(this.MenuViewExif_Click);
            // 
            // StartupBGWorker
            // 
            this.StartupBGWorker.WorkerReportsProgress = true;
            this.StartupBGWorker.WorkerSupportsCancellation = true;
            this.StartupBGWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.StartupBGWorker_DoWork);
            this.StartupBGWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.StartupBGWorker_ProgressChanged);
            this.StartupBGWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.StartupBGWorker_RunWorkerCompleted);
            // 
            // MoveBGWorker
            // 
            this.MoveBGWorker.WorkerReportsProgress = true;
            this.MoveBGWorker.WorkerSupportsCancellation = true;
            this.MoveBGWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.MoveBGWorker_DoWork);
            this.MoveBGWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.MoveBGWorker_ProgressChanged);
            this.MoveBGWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.MoveBGWorker_RunWorkerCompleted);
            // 
            // LoadImageBGWorker
            // 
            this.LoadImageBGWorker.WorkerReportsProgress = true;
            this.LoadImageBGWorker.WorkerSupportsCancellation = true;
            this.LoadImageBGWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.LoadImageBGWorker_DoWork);
            this.LoadImageBGWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.LoadImageBGWorker_ProgressChanged);
            this.LoadImageBGWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.LoadImageBGWorker_RunWorkerCompleted);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFile,
            this.MenuTool,
            this.MenuAbout});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(7, 3, 0, 3);
            this.MainMenu.Size = new System.Drawing.Size(915, 25);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
            // 
            // MenuFile
            // 
            this.MenuFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuOpenSource,
            this.MenuOpenTarget,
            this.MenuRun,
            this.toolStripSeparator4,
            this.MenuRefresh,
            this.toolStripSeparator2,
            this.MenuEnd});
            this.MenuFile.Name = "MenuFile";
            this.MenuFile.Size = new System.Drawing.Size(57, 19);
            this.MenuFile.Text = "檔案(&F)";
            // 
            // MenuOpenSource
            // 
            this.MenuOpenSource.Name = "MenuOpenSource";
            this.MenuOpenSource.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuOpenSource.Size = new System.Drawing.Size(191, 22);
            this.MenuOpenSource.Text = "選擇來源目錄";
            this.MenuOpenSource.Click += new System.EventHandler(this.OpenSourcePathClick);
            // 
            // MenuOpenTarget
            // 
            this.MenuOpenTarget.Name = "MenuOpenTarget";
            this.MenuOpenTarget.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.T)));
            this.MenuOpenTarget.Size = new System.Drawing.Size(191, 22);
            this.MenuOpenTarget.Text = "選擇目的目錄";
            this.MenuOpenTarget.Click += new System.EventHandler(this.OpenTargetPathClick);
            // 
            // MenuRun
            // 
            this.MenuRun.Name = "MenuRun";
            this.MenuRun.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.R)));
            this.MenuRun.Size = new System.Drawing.Size(191, 22);
            this.MenuRun.Text = "開始執行";
            this.MenuRun.Click += new System.EventHandler(this.StartClick);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(188, 6);
            // 
            // MenuRefresh
            // 
            this.MenuRefresh.Name = "MenuRefresh";
            this.MenuRefresh.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.MenuRefresh.Size = new System.Drawing.Size(191, 22);
            this.MenuRefresh.Text = "重新整理";
            this.MenuRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(188, 6);
            // 
            // MenuEnd
            // 
            this.MenuEnd.Name = "MenuEnd";
            this.MenuEnd.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.MenuEnd.Size = new System.Drawing.Size(191, 22);
            this.MenuEnd.Text = "結束";
            this.MenuEnd.Click += new System.EventHandler(this.MenuEnd_Click);
            // 
            // MenuTool
            // 
            this.MenuTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuFunction,
            this.toolStripSeparator3,
            this.MenuSettings});
            this.MenuTool.Name = "MenuTool";
            this.MenuTool.Size = new System.Drawing.Size(58, 19);
            this.MenuTool.Text = "工具(&T)";
            // 
            // MenuFunction
            // 
            this.MenuFunction.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuMoveType,
            this.MenuYearType,
            this.MenuIsSync});
            this.MenuFunction.Name = "MenuFunction";
            this.MenuFunction.Size = new System.Drawing.Size(125, 22);
            this.MenuFunction.Text = "快速功能";
            // 
            // MenuMoveType
            // 
            this.MenuMoveType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuIsCopy,
            this.MenuIsMove});
            this.MenuMoveType.Name = "MenuMoveType";
            this.MenuMoveType.Size = new System.Drawing.Size(160, 22);
            this.MenuMoveType.Text = "移動方式";
            // 
            // MenuIsCopy
            // 
            this.MenuIsCopy.CheckOnClick = true;
            this.MenuIsCopy.Name = "MenuIsCopy";
            this.MenuIsCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.C)));
            this.MenuIsCopy.Size = new System.Drawing.Size(141, 22);
            this.MenuIsCopy.Text = "複製";
            this.MenuIsCopy.Click += new System.EventHandler(this.CopyTypeClick);
            // 
            // MenuIsMove
            // 
            this.MenuIsMove.CheckOnClick = true;
            this.MenuIsMove.Name = "MenuIsMove";
            this.MenuIsMove.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.M)));
            this.MenuIsMove.Size = new System.Drawing.Size(141, 22);
            this.MenuIsMove.Text = "移動";
            this.MenuIsMove.Click += new System.EventHandler(this.MoveTypeClick);
            // 
            // MenuYearType
            // 
            this.MenuYearType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuYear3,
            this.MenuYear4});
            this.MenuYearType.Name = "MenuYearType";
            this.MenuYearType.Size = new System.Drawing.Size(160, 22);
            this.MenuYearType.Text = "年份表示";
            // 
            // MenuYear3
            // 
            this.MenuYear3.CheckOnClick = true;
            this.MenuYear3.Name = "MenuYear3";
            this.MenuYear3.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D3)));
            this.MenuYear3.Size = new System.Drawing.Size(148, 22);
            this.MenuYear3.Text = "民國年";
            this.MenuYear3.Click += new System.EventHandler(this.Year3Click);
            // 
            // MenuYear4
            // 
            this.MenuYear4.CheckOnClick = true;
            this.MenuYear4.Name = "MenuYear4";
            this.MenuYear4.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.D4)));
            this.MenuYear4.Size = new System.Drawing.Size(148, 22);
            this.MenuYear4.Text = "西元年";
            this.MenuYear4.Click += new System.EventHandler(this.Year4Click);
            // 
            // MenuIsSync
            // 
            this.MenuIsSync.CheckOnClick = true;
            this.MenuIsSync.Name = "MenuIsSync";
            this.MenuIsSync.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.S)));
            this.MenuIsSync.Size = new System.Drawing.Size(160, 22);
            this.MenuIsSync.Text = "同步變動";
            this.MenuIsSync.ToolTipText = "同步變動資料夾自訂名稱";
            this.MenuIsSync.Click += new System.EventHandler(this.MenuIsSync_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(122, 6);
            // 
            // MenuSettings
            // 
            this.MenuSettings.Name = "MenuSettings";
            this.MenuSettings.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.MenuSettings.Size = new System.Drawing.Size(125, 22);
            this.MenuSettings.Text = "設定";
            this.MenuSettings.Click += new System.EventHandler(this.MenuSettings_Click);
            // 
            // MenuAbout
            // 
            this.MenuAbout.Name = "MenuAbout";
            this.MenuAbout.Size = new System.Drawing.Size(59, 19);
            this.MenuAbout.Text = "關於(&A)";
            this.MenuAbout.Click += new System.EventHandler(this.MenuAbout_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.StatusBar,
            this.StatusText});
            this.statusStrip1.Location = new System.Drawing.Point(0, 676);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.statusStrip1.Size = new System.Drawing.Size(915, 26);
            this.statusStrip1.TabIndex = 2;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // StatusBar
            // 
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(117, 20);
            // 
            // StatusText
            // 
            this.StatusText.Name = "StatusText";
            this.StatusText.Size = new System.Drawing.Size(0, 21);
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 25);
            this.MainSplitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MainSplitContainer.Name = "MainSplitContainer";
            this.MainSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.TopSplitContainer);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.FileListView);
            this.MainSplitContainer.Size = new System.Drawing.Size(915, 651);
            this.MainSplitContainer.SplitterDistance = 123;
            this.MainSplitContainer.SplitterWidth = 5;
            this.MainSplitContainer.TabIndex = 3;
            // 
            // TopSplitContainer
            // 
            this.TopSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TopSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.TopSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.TopSplitContainer.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.TopSplitContainer.Name = "TopSplitContainer";
            // 
            // TopSplitContainer.Panel1
            // 
            this.TopSplitContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // TopSplitContainer.Panel2
            // 
            this.TopSplitContainer.Panel2.Controls.Add(this.groupBox2);
            this.TopSplitContainer.Size = new System.Drawing.Size(915, 123);
            this.TopSplitContainer.SplitterDistance = 615;
            this.TopSplitContainer.SplitterWidth = 5;
            this.TopSplitContainer.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnOpenTarget);
            this.groupBox1.Controls.Add(this.txtTargetPath);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.pictureBox2);
            this.groupBox1.Controls.Add(this.radioCopy);
            this.groupBox1.Controls.Add(this.radioMove);
            this.groupBox1.Controls.Add(this.pictureBox1);
            this.groupBox1.Controls.Add(this.btnOpenSource);
            this.groupBox1.Controls.Add(this.txtSourcePath);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox1.Size = new System.Drawing.Size(615, 123);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "資料夾設定";
            // 
            // txtTargetPath
            // 
            this.txtTargetPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTargetPath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTargetPath.Location = new System.Drawing.Point(111, 82);
            this.txtTargetPath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTargetPath.Name = "txtTargetPath";
            this.txtTargetPath.ReadOnly = true;
            this.txtTargetPath.Size = new System.Drawing.Size(459, 23);
            this.txtTargetPath.TabIndex = 8;
            this.txtTargetPath.TextChanged += new System.EventHandler(this.txtTargetPath_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(14, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "目的資料夾：";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::MiniTool.Properties.Resources.down;
            this.pictureBox2.Location = new System.Drawing.Point(288, 53);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(16, 16);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox2.TabIndex = 6;
            this.pictureBox2.TabStop = false;
            // 
            // radioCopy
            // 
            this.radioCopy.AutoSize = true;
            this.radioCopy.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioCopy.Location = new System.Drawing.Point(136, 53);
            this.radioCopy.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioCopy.Name = "radioCopy";
            this.radioCopy.Size = new System.Drawing.Size(48, 19);
            this.radioCopy.TabIndex = 5;
            this.radioCopy.TabStop = true;
            this.radioCopy.Text = "複製";
            this.radioCopy.UseVisualStyleBackColor = true;
            this.radioCopy.CheckedChanged += new System.EventHandler(this.radioCopy_CheckedChanged);
            this.radioCopy.Click += new System.EventHandler(this.CopyTypeClick);
            // 
            // radioMove
            // 
            this.radioMove.AutoSize = true;
            this.radioMove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioMove.Location = new System.Drawing.Point(226, 53);
            this.radioMove.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioMove.Name = "radioMove";
            this.radioMove.Size = new System.Drawing.Size(48, 19);
            this.radioMove.TabIndex = 4;
            this.radioMove.TabStop = true;
            this.radioMove.Text = "移動";
            this.radioMove.UseVisualStyleBackColor = true;
            this.radioMove.CheckedChanged += new System.EventHandler(this.radioMove_CheckedChanged);
            this.radioMove.Click += new System.EventHandler(this.MoveTypeClick);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::MiniTool.Properties.Resources.down;
            this.pictureBox1.Location = new System.Drawing.Point(111, 53);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(16, 16);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 3;
            this.pictureBox1.TabStop = false;
            // 
            // txtSourcePath
            // 
            this.txtSourcePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtSourcePath.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtSourcePath.Location = new System.Drawing.Point(111, 19);
            this.txtSourcePath.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSourcePath.Name = "txtSourcePath";
            this.txtSourcePath.ReadOnly = true;
            this.txtSourcePath.Size = new System.Drawing.Size(459, 23);
            this.txtSourcePath.TabIndex = 1;
            this.txtSourcePath.TextChanged += new System.EventHandler(this.txtSourcePath_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(14, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(79, 15);
            this.label1.TabIndex = 0;
            this.label1.Text = "來源資料夾：";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Controls.Add(this.btnSelectAll);
            this.groupBox2.Controls.Add(this.btnRefresh);
            this.groupBox2.Controls.Add(this.chkSyncName);
            this.groupBox2.Controls.Add(this.radioYear4);
            this.groupBox2.Controls.Add(this.radioYear3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Padding = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.groupBox2.Size = new System.Drawing.Size(295, 123);
            this.groupBox2.TabIndex = 0;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "其他設定";
            // 
            // radioYear4
            // 
            this.radioYear4.AutoSize = true;
            this.radioYear4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioYear4.Location = new System.Drawing.Point(85, 41);
            this.radioYear4.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioYear4.Name = "radioYear4";
            this.radioYear4.Size = new System.Drawing.Size(60, 19);
            this.radioYear4.TabIndex = 2;
            this.radioYear4.TabStop = true;
            this.radioYear4.Text = "西元年";
            this.radioYear4.UseVisualStyleBackColor = true;
            this.radioYear4.CheckedChanged += new System.EventHandler(this.radioYear4_CheckedChanged);
            this.radioYear4.Click += new System.EventHandler(this.Year4Click);
            // 
            // radioYear3
            // 
            this.radioYear3.AutoSize = true;
            this.radioYear3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.radioYear3.Location = new System.Drawing.Point(9, 41);
            this.radioYear3.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.radioYear3.Name = "radioYear3";
            this.radioYear3.Size = new System.Drawing.Size(60, 19);
            this.radioYear3.TabIndex = 1;
            this.radioYear3.TabStop = true;
            this.radioYear3.Text = "民國年";
            this.radioYear3.UseVisualStyleBackColor = true;
            this.radioYear3.CheckedChanged += new System.EventHandler(this.radioYear3_CheckedChanged);
            this.radioYear3.Click += new System.EventHandler(this.Year3Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(7, 22);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(127, 15);
            this.label3.TabIndex = 0;
            this.label3.Text = "目的資料夾年份格式：";
            // 
            // FileListView
            // 
            this.FileListView.ContextMenuStrip = this.MoveListMenu;
            this.FileListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileListView.DoubleClickActivation = true;
            this.FileListView.FullRowSelect = true;
            this.FileListView.HideSelection = false;
            this.FileListView.LabelWrap = false;
            this.FileListView.Location = new System.Drawing.Point(0, 0);
            this.FileListView.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.FileListView.Name = "FileListView";
            this.FileListView.ShowItemToolTips = true;
            this.FileListView.Size = new System.Drawing.Size(915, 523);
            this.FileListView.SmallImageList = this.MoveImageList;
            this.FileListView.TabIndex = 0;
            this.FileListView.UseCompatibleStateImageBehavior = false;
            this.FileListView.View = System.Windows.Forms.View.Details;
            this.FileListView.MouseEnter += new System.EventHandler(this.FileListView_MouseEnter);
            // 
            // cboxFolderPrefix
            // 
            this.cboxFolderPrefix.FormattingEnabled = true;
            this.cboxFolderPrefix.Location = new System.Drawing.Point(528, 676);
            this.cboxFolderPrefix.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.cboxFolderPrefix.Name = "cboxFolderPrefix";
            this.cboxFolderPrefix.Size = new System.Drawing.Size(140, 23);
            this.cboxFolderPrefix.TabIndex = 4;
            this.cboxFolderPrefix.Visible = false;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(915, 702);
            this.Controls.Add(this.cboxFolderPrefix);
            this.Controls.Add(this.MainSplitContainer);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainMenu);
            this.Font = new System.Drawing.Font("Microsoft JhengHei UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(931, 740);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "迷你照片小工具";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.MoveListMenu.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.TopSplitContainer.Panel1.ResumeLayout(false);
            this.TopSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TopSplitContainer)).EndInit();
            this.TopSplitContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolTip MainToolTip;
        private System.Windows.Forms.ImageList MoveImageList;
        private System.Windows.Forms.ContextMenuStrip MoveListMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuSelectAll;
        private System.Windows.Forms.ToolStripMenuItem MenuInvertSelect;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuViewExif;
        private System.Windows.Forms.FolderBrowserDialog SourceBrowser;
        private System.Windows.Forms.FolderBrowserDialog TargetBrowser;
        private System.ComponentModel.BackgroundWorker StartupBGWorker;
        private System.ComponentModel.BackgroundWorker MoveBGWorker;
        private System.ComponentModel.BackgroundWorker LoadImageBGWorker;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuFile;
        private System.Windows.Forms.ToolStripMenuItem MenuOpenSource;
        private System.Windows.Forms.ToolStripMenuItem MenuOpenTarget;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuEnd;
        private System.Windows.Forms.ToolStripMenuItem MenuTool;
        private System.Windows.Forms.ToolStripMenuItem MenuSettings;
        private System.Windows.Forms.ToolStripMenuItem MenuAbout;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar StatusBar;
        private System.Windows.Forms.ToolStripStatusLabel StatusText;
        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.SplitContainer TopSplitContainer;
        private System.Windows.Forms.ToolStripMenuItem MenuFunction;
        private System.Windows.Forms.ToolStripMenuItem MenuMoveType;
        private System.Windows.Forms.ToolStripMenuItem MenuIsMove;
        private System.Windows.Forms.ToolStripMenuItem MenuIsCopy;
        private System.Windows.Forms.ToolStripMenuItem MenuYearType;
        private System.Windows.Forms.ToolStripMenuItem MenuYear3;
        private System.Windows.Forms.ToolStripMenuItem MenuYear4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MenuIsSync;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private extensions.NativeListView FileListView;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtSourcePath;
        private System.Windows.Forms.Button btnOpenSource;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.RadioButton radioMove;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.RadioButton radioCopy;
        private System.Windows.Forms.TextBox txtTargetPath;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnOpenTarget;
        private System.Windows.Forms.RadioButton radioYear3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.RadioButton radioYear4;
        private System.Windows.Forms.CheckBox chkSyncName;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.Button btnSelectAll;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.ComboBox cboxFolderPrefix;
        private System.Windows.Forms.ToolStripMenuItem MenuRun;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem MenuRefresh;
    }
}