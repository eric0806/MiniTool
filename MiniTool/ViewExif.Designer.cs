namespace MiniTool
{
    partial class ViewExif
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ViewExif));
            this.MainSplitContainer = new System.Windows.Forms.SplitContainer();
            this.FileListView = new extensions.NativeListView();
            this.columnHeader1 = ((extensions.ColHeader)(new extensions.ColHeader()));
            this.FileImageList = new System.Windows.Forms.ImageList(this.components);
            this.ExifSplitContainer = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.ThumbBox = new System.Windows.Forms.PictureBox();
            this.ExifListView = new extensions.NativeListView();
            this.HeaderExifTag = ((extensions.ColHeader)(new extensions.ColHeader()));
            this.HeaderExifValue = ((extensions.ColHeader)(new extensions.ColHeader()));
            this.ShowBGWorker = new System.ComponentModel.BackgroundWorker();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnClose = new System.Windows.Forms.Button();
            this.MapBox = new System.Windows.Forms.PictureBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).BeginInit();
            this.MainSplitContainer.Panel1.SuspendLayout();
            this.MainSplitContainer.Panel2.SuspendLayout();
            this.MainSplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ExifSplitContainer)).BeginInit();
            this.ExifSplitContainer.Panel1.SuspendLayout();
            this.ExifSplitContainer.Panel2.SuspendLayout();
            this.ExifSplitContainer.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ThumbBox)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.MapBox)).BeginInit();
            this.SuspendLayout();
            // 
            // MainSplitContainer
            // 
            this.MainSplitContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MainSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.MainSplitContainer.Name = "MainSplitContainer";
            // 
            // MainSplitContainer.Panel1
            // 
            this.MainSplitContainer.Panel1.Controls.Add(this.FileListView);
            // 
            // MainSplitContainer.Panel2
            // 
            this.MainSplitContainer.Panel2.Controls.Add(this.ExifSplitContainer);
            this.MainSplitContainer.Size = new System.Drawing.Size(511, 579);
            this.MainSplitContainer.SplitterDistance = 199;
            this.MainSplitContainer.TabIndex = 0;
            // 
            // FileListView
            // 
            this.FileListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.FileListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FileListView.DoubleClickActivation = false;
            this.FileListView.HideSelection = false;
            this.FileListView.Location = new System.Drawing.Point(0, 0);
            this.FileListView.Name = "FileListView";
            this.FileListView.Size = new System.Drawing.Size(199, 579);
            this.FileListView.SmallImageList = this.FileImageList;
            this.FileListView.TabIndex = 0;
            this.FileListView.UseCompatibleStateImageBehavior = false;
            this.FileListView.View = System.Windows.Forms.View.Details;
            this.FileListView.SelectedIndexChanged += new System.EventHandler(this.FileListView_SelectedIndexChanged);
            this.FileListView.MouseEnter += new System.EventHandler(this.FileListView_MouseEnter);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "檔名";
            this.columnHeader1.Width = 168;
            // 
            // FileImageList
            // 
            this.FileImageList.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.FileImageList.ImageSize = new System.Drawing.Size(28, 28);
            this.FileImageList.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // ExifSplitContainer
            // 
            this.ExifSplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExifSplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.ExifSplitContainer.Location = new System.Drawing.Point(0, 0);
            this.ExifSplitContainer.Name = "ExifSplitContainer";
            this.ExifSplitContainer.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // ExifSplitContainer.Panel1
            // 
            this.ExifSplitContainer.Panel1.Controls.Add(this.groupBox1);
            // 
            // ExifSplitContainer.Panel2
            // 
            this.ExifSplitContainer.Panel2.Controls.Add(this.ExifListView);
            this.ExifSplitContainer.Size = new System.Drawing.Size(308, 579);
            this.ExifSplitContainer.SplitterDistance = 231;
            this.ExifSplitContainer.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.ThumbBox);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(308, 231);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "縮圖";
            // 
            // ThumbBox
            // 
            this.ThumbBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ThumbBox.Location = new System.Drawing.Point(3, 18);
            this.ThumbBox.Name = "ThumbBox";
            this.ThumbBox.Size = new System.Drawing.Size(302, 210);
            this.ThumbBox.TabIndex = 0;
            this.ThumbBox.TabStop = false;
            this.ThumbBox.SizeChanged += new System.EventHandler(this.ThumbBox_SizeChanged);
            // 
            // ExifListView
            // 
            this.ExifListView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.HeaderExifTag,
            this.HeaderExifValue});
            this.ExifListView.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ExifListView.DoubleClickActivation = false;
            this.ExifListView.FullRowSelect = true;
            this.ExifListView.GridLines = true;
            this.ExifListView.HideSelection = false;
            this.ExifListView.Location = new System.Drawing.Point(0, 0);
            this.ExifListView.Name = "ExifListView";
            this.ExifListView.ShowItemToolTips = true;
            this.ExifListView.Size = new System.Drawing.Size(308, 344);
            this.ExifListView.TabIndex = 0;
            this.ExifListView.UseCompatibleStateImageBehavior = false;
            this.ExifListView.View = System.Windows.Forms.View.Details;
            this.ExifListView.MouseEnter += new System.EventHandler(this.ExifListView_MouseEnter);
            // 
            // HeaderExifTag
            // 
            this.HeaderExifTag.Text = "標籤";
            this.HeaderExifTag.Width = 119;
            // 
            // HeaderExifValue
            // 
            this.HeaderExifValue.Text = "值";
            this.HeaderExifValue.Width = 135;
            // 
            // ShowBGWorker
            // 
            this.ShowBGWorker.WorkerReportsProgress = true;
            this.ShowBGWorker.WorkerSupportsCancellation = true;
            this.ShowBGWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.ShowBGWorker_DoWork);
            this.ShowBGWorker.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.ShowBGWorker_ProgressChanged);
            this.ShowBGWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.ShowBGWorker_RunWorkerCompleted);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnClose);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 585);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(511, 29);
            this.panel1.TabIndex = 1;
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(424, 3);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "關閉";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // MapBox
            // 
            this.MapBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.MapBox.BackColor = System.Drawing.SystemColors.Control;
            this.MapBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MapBox.ErrorImage = ((System.Drawing.Image)(resources.GetObject("MapBox.ErrorImage")));
            this.MapBox.InitialImage = global::MiniTool.Properties.Resources.loading16;
            this.MapBox.Location = new System.Drawing.Point(103, 132);
            this.MapBox.Name = "MapBox";
            this.MapBox.Size = new System.Drawing.Size(300, 300);
            this.MapBox.TabIndex = 2;
            this.MapBox.TabStop = false;
            this.MapBox.Visible = false;
            this.MapBox.SizeChanged += new System.EventHandler(this.MapBox_SizeChanged);
            this.MapBox.VisibleChanged += new System.EventHandler(this.MapBox_VisibleChanged);
            this.MapBox.Click += new System.EventHandler(this.MapBox_Click);
            // 
            // toolTip1
            // 
            this.toolTip1.AutoPopDelay = 5000;
            this.toolTip1.InitialDelay = 100;
            this.toolTip1.ReshowDelay = 100;
            // 
            // ViewExif
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.Disable;
            this.ClientSize = new System.Drawing.Size(511, 614);
            this.Controls.Add(this.MapBox);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.MainSplitContainer);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "ViewExif";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "檢視Exif";
            this.Load += new System.EventHandler(this.ViewExif_Load);
            this.MainSplitContainer.Panel1.ResumeLayout(false);
            this.MainSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MainSplitContainer)).EndInit();
            this.MainSplitContainer.ResumeLayout(false);
            this.ExifSplitContainer.Panel1.ResumeLayout(false);
            this.ExifSplitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ExifSplitContainer)).EndInit();
            this.ExifSplitContainer.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ThumbBox)).EndInit();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.MapBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer MainSplitContainer;
        private System.Windows.Forms.SplitContainer ExifSplitContainer;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.PictureBox ThumbBox;
        private extensions.NativeListView ExifListView;
        private extensions.ColHeader HeaderExifTag;
        private extensions.ColHeader HeaderExifValue;
        private System.Windows.Forms.ImageList FileImageList;
        private System.ComponentModel.BackgroundWorker ShowBGWorker;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnClose;
        private extensions.NativeListView FileListView;
        private extensions.ColHeader columnHeader1;
        private System.Windows.Forms.PictureBox MapBox;
        private System.Windows.Forms.ToolTip toolTip1;
    }
}