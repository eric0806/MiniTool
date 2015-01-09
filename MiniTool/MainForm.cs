using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using extensions;
using System.Diagnostics;
using System.IO;
using extensions.Functions;
using System.Reflection;
using Microsoft.WindowsAPICodePack.Shell;
using System.Threading;
using SimpleExif;

namespace MiniTool
{
    public partial class MainForm : Form
    {
        //private string DefaultFileExt = ".jpg";
        //private string ExtensionFileExt = ".jpg,.cr2,.tif,.tiff,.gif";
        private Control[] Editors;
        private Stopwatch s = new Stopwatch();
        //sf.Properties.System.ItemTypeText.Value
        public MainForm() {
            InitializeComponent();
            FileListView.Columns.Add(new ColHeader("檔案名稱", 150, HorizontalAlignment.Left, true));
            FileListView.Columns.Add(new ColHeader("檔案大小", 60, HorizontalAlignment.Right, true));
            FileListView.Columns.Add(new ColHeader("類型", 70, HorizontalAlignment.Left, true));
            FileListView.Columns.Add(new ColHeader("拍攝時間/產生時間", 130, HorizontalAlignment.Left, true));
            FileListView.Columns.Add(new ColHeader("相機廠牌", 80, HorizontalAlignment.Left, true));
            FileListView.Columns.Add(new ColHeader("相機型號", 130, HorizontalAlignment.Left, true));
            FileListView.Columns.Add(new ColHeader("自訂資料夾名稱", 150, HorizontalAlignment.Left, true));

            FileListView.SubItemClicked += new SubItemEventHandler(FileListView_SubItemClicked);
            FileListView.SubItemEndEditing += new SubItemEndEditingEventHandler(FileListView_SubItemEndEditing);

            this.Text += " " + Assembly.GetExecutingAssembly().GetName().Version.ToString();
        }

        private void MainForm_Load(object sender, EventArgs e) {
            StartupBGWorker.RunWorkerAsync();
        }

        /// <summary>
        /// 表單關閉動作，要儲存視窗設定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (ThisIsMax()) {
                Config.SetConfig("IsMax", "1");
            }
            else {
                Config.SetConfig("IsMax", "0");
                Config.SetConfig("WindowWidth", this.Width.ToString());
                Config.SetConfig("WindowHeight", this.Height.ToString());
            }
        }

        #region 各元件事件區
        /// <summary>
        /// 關於按鈕按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuAbout_Click(object sender, EventArgs e) {
            new AboutMe().ShowDialog();
        }

        /// <summary>
        /// 結束按鈕按下
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuEnd_Click(object sender, EventArgs e) {
            this.Close();
        }

        /// <summary>
        /// 處理開啟來源資料夾的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenSourcePathClick(object sender, EventArgs e) {
            if (txtSourcePath.Text != string.Empty) {
                if (Directory.Exists(txtSourcePath.Text)) {
                    SourceBrowser.SelectedPath = txtSourcePath.Text;
                }
            }
            if (SourceBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                txtSourcePath.Text = SourceBrowser.SelectedPath;
            }
        }

        /// <summary>
        /// 處理開啟目的資料夾的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenTargetPathClick(object sender, EventArgs e) {
            if (txtTargetPath.Text != string.Empty) {
                if (Directory.Exists(txtTargetPath.Text)) {
                    TargetBrowser.SelectedPath = txtTargetPath.Text;
                }
            }
            if (TargetBrowser.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                txtTargetPath.Text = TargetBrowser.SelectedPath;
            }
        }

        /// <summary>
        /// 來源資料夾路徑有更改動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSourcePath_TextChanged(object sender, EventArgs e) {
            Config.SetConfig("SourcePath", txtSourcePath.Text);
            //載入列表
            StartLoadImage();
        }

        /// <summary>
        /// 目的資料夾路徑有更改的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTargetPath_TextChanged(object sender, EventArgs e) {
            Config.SetConfig("TargetPath", txtTargetPath.Text);
        }

        /// <summary>
        /// 處理複製方式的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CopyTypeClick(object sender, EventArgs e) {
            MenuIsCopy.Checked = true;
            radioCopy.Checked = true;
            MenuIsMove.Checked = false;
            radioMove.Checked = false;
        }
        private void radioCopy_CheckedChanged(object sender, EventArgs e) {
            if (radioCopy.Checked) { CopyTypeClick(sender, e); }
            Config.SetConfig("MoveMethod", "C");
        }

        /// <summary>
        /// 處理搬移方式的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveTypeClick(object sender, EventArgs e) {
            MenuIsCopy.Checked = false;
            radioCopy.Checked = false;
            MenuIsMove.Checked = true;
            radioMove.Checked = true;
        }
        private void radioMove_CheckedChanged(object sender, EventArgs e) {
            if (radioMove.Checked) { MoveTypeClick(sender, e); }
            Config.SetConfig("MoveMethod", "M");
        }

        /// <summary>
        /// 處理民國年的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Year3Click(object sender, EventArgs e) {
            MenuYear3.Checked = true;
            radioYear3.Checked = true;
            MenuYear4.Checked = false;
            radioYear4.Checked = false;
        }
        private void radioYear3_CheckedChanged(object sender, EventArgs e) {
            if (radioYear3.Checked) { Year3Click(sender, e); }
            Config.SetConfig("YearMode", "3");
        }

        /// <summary>
        /// 處理西元年的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Year4Click(object sender, EventArgs e) {
            MenuYear3.Checked = false;
            radioYear3.Checked = false;
            MenuYear4.Checked = true;
            radioYear4.Checked = true;
        }
        private void radioYear4_CheckedChanged(object sender, EventArgs e) {
            if (radioYear4.Checked) { Year4Click(sender, e); }
            Config.SetConfig("YearMode", "4");
        }

        /// <summary>
        /// 同步自訂資料夾名稱按鈕或功能表按下動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuIsSync_Click(object sender, EventArgs e) {
            chkSyncName.Checked = MenuIsSync.Checked;
        }
        private void chkSyncName_CheckedChanged(object sender, EventArgs e) {
            MenuIsSync.Checked = chkSyncName.Checked;
            if (chkSyncName.Checked) { Config.SetConfig("SyncName", "1"); }
            else { Config.SetConfig("SyncName", "0"); }
        }

        /// <summary>
        /// 重新整理按鈕按下的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e) {
            StartLoadImage();
        }

        /// <summary>
        /// 開始處理按鈕或功能表按下的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartClick(object sender, EventArgs e) {
            if (txtSourcePath.Text == string.Empty) { StatusText.Text = "沒有選擇來源資料夾!"; return; }
            if (txtTargetPath.Text == string.Empty) { StatusText.Text = "沒有選擇目的資料夾!"; return; }
            if (!Directory.Exists(txtTargetPath.Text)) { StatusText.Text = "目的資料夾不存在!"; return; }
            if (!radioCopy.Checked && !radioMove.Checked) { radioMove.Checked = true; }
            if (!radioYear3.Checked && !radioYear4.Checked) { radioYear4.Checked = true; }
            if (FileListView.SelectedItems.Count > 0) { StartMove(); }
        }

        /// <summary>
        /// 設定功能表按下的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuSettings_Click(object sender, EventArgs e) {
            SettingForm stForm = new SettingForm();
            string TempReadRAW = Config.GetConfig("ReadRAW");
            if (stForm.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
                if (Config.GetConfig("ReadRAW") != TempReadRAW) {
                    StartLoadImage();
                }
            }
        }

        /// <summary>
        /// 全選按鈕按下的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuSelectAll_Click(object sender, EventArgs e) {
            if (FileListView.Items.Count > 0) {
                foreach (ListViewItem item in FileListView.Items) {
                    item.Selected = true;
                }
            }
        }

        /// <summary>
        /// 反選按鈕按下的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuInvertSelect_Click(object sender, EventArgs e) {
            for (int i = 0; i < FileListView.Items.Count; i++) {
                FileListView.Items[i].Selected = !FileListView.Items[i].Selected;
            }
        }

        /// <summary>
        /// 檢視完整Exif按下的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuViewExif_Click(object sender, EventArgs e) {
            string[] FNames = new string[FileListView.SelectedItems.Count];
            for (int i = 0; i < FNames.Length; i++) {
                FNames[i] = FileListView.SelectedItems[i].Text;
            }
            ViewExif vf = new ViewExif(FNames);
            vf.ShowDialog();
        }

        /// <summary>
        /// ListViewItem被滑鼠點兩下之後的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileListView_SubItemClicked(object sender, extensions.SubItemEventArgs e) {
            if (e.SubItem == 6) { //如果是自訂名稱
                FileListView.StartEditing(Editors[e.SubItem], e.Item, e.SubItem);
            }
            else { //其他
                Process.Start(Path.Combine(txtSourcePath.Text, e.Item.Text));
            }
        }

        /// <summary>
        /// ListViewItem結束編輯後的動作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileListView_SubItemEndEditing(object sender, extensions.SubItemEndEditingEventArgs e) {
            if (e.SubItem == 6) {
                //MessageBox.Show(e.Item.SubItems[e.SubItem].Text);
                if (!cboxFolderPrefix.Items.Contains(e.DisplayText)) {
                    cboxFolderPrefix.Items.Add(e.DisplayText);
                }
                //將底下的Item預設成該名稱
                for (int i = e.Item.Index; i < FileListView.Items.Count; i++) {
                    if (Config.GetConfig("SyncName") == "1") {
                        FileListView.Items[i].SubItems[6].Text = e.DisplayText;
                    }
                    else {
                        if (FileListView.Items[i].SubItems[6].Text == string.Empty) {
                            FileListView.Items[i].SubItems[6].Text = e.DisplayText;
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 滑鼠進入列表自動取得焦點，這樣就可用滾輪上下翻，不須再點一下導致原本選取範圍消失
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileListView_MouseEnter(object sender, EventArgs e) {
            FileListView.Focus();
        }

        /// <summary>
        /// 快捷功能表出現時的判斷
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MoveListMenu_Opening(object sender, CancelEventArgs e) {
            if (FileListView.SelectedItems.Count < 1) {
                MenuViewExif.Enabled = false;
            }
            else {
                MenuViewExif.Enabled = true;
            }
        }
        #endregion

        #region 初始化逼雞窩可區
        private void StartupBGWorker_DoWork(object sender, DoWorkEventArgs e) {
            RunStartUp((BackgroundWorker)sender, e);
        }

        private void StartupBGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            StatusBar.Value = e.ProgressPercentage;
            StatusText.Text = Config.StatusBarText;
        }

        private void StartupBGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            StatusBar.Value = StatusBar.Minimum;
            if (e.Cancelled) { StatusText.Text = Config.StatusBarText; }
            else {
                StatusText.Text = string.Empty;
                InitControl();
            }
        }

        /// <summary>
        /// 啟動時讀取設定檔
        /// </summary>
        /// <param name="worker"></param>
        /// <param name="e"></param>
        private void RunStartUp(BackgroundWorker worker, DoWorkEventArgs e) {
            Config.StatusBarText = string.Empty;
            worker.ReportProgress(50);
            Config.StatusBarText = "讀取設定...";
            Config.LoadConfig();
            if (Config.StatusBarText != string.Empty) {
                //StatusText.Text = Config.StatusBarText;
                worker.CancelAsync();
            }
            Thread.Sleep(50);
            worker.ReportProgress(100);
        }

        /// <summary>
        /// 動態初始化設定元件
        /// </summary>
        private void InitControl() {
            try {
                txtSourcePath.Text = Config.GetConfig("SourcePath");
                txtTargetPath.Text = Config.GetConfig("TargetPath");
                switch (Config.GetConfig("MoveMethod")) {
                    case "M":
                        radioMove.Checked = true;
                        break;
                    case "C":
                        radioCopy.Checked = true;
                        break;
                    default:
                        goto case "M";
                }
                switch (Config.GetConfig("YearMode")) {
                    case "3":
                        radioYear3.Checked = true;
                        break;
                    case "4":
                        radioYear4.Checked = true;
                        break;
                    default:
                        goto case "3";
                }
                switch (Config.GetConfig("SyncName")) {
                    case "0":
                        chkSyncName.Checked = false;
                        break;
                    case "1":
                        chkSyncName.Checked = true;
                        break;
                    default:
                        goto case "1";
                }
                if (Config.GetConfig("IsMax") == "1") {
                    this.WindowState = FormWindowState.Maximized;
                }
                else {
                    if (Config.GetConfig("WindowWidth") != string.Empty && Config.GetConfig("WindowHeight") != string.Empty) {
                        this.Width = int.Parse(Config.GetConfig("WindowWidth"));
                        this.Height = int.Parse(Config.GetConfig("WindowHeight"));
                        this.CenterToScreen();
                    }
                }
                //throw new Exception("dd");
            }
            catch {
                StatusText.Text = "無法初始化控制項"; // +ex.Message;
            }
        }
        #endregion

        #region 讀取列表逼雞窩可區
        private void LoadImageBGWorker_DoWork(object sender, DoWorkEventArgs e) {
            s.Reset();
            s.Start();
            RunLoadImage(e.Argument.ToString(), (BackgroundWorker)sender, e);
        }

        private void LoadImageBGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            StatusBar.Value = e.ProgressPercentage;
            StatusText.Text = Config.StatusBarText;
        }

        private void LoadImageBGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Cancelled) { StatusText.Text = "取消載入"; return; }
            //else { StatusText.Text = "載入圖片完成"; }
            ShowImageList();
            s.Stop();
            StatusBar.Value = StatusBar.Minimum;
            StatusText.Text = "載入圖片完成，花" + string.Format("{0:0.00}", (float)((float)s.ElapsedMilliseconds / (float)1000)) + "秒";

        }

        private void RunLoadImage(string SourcePath, BackgroundWorker worker, DoWorkEventArgs e) {
            Config.StatusBarText = string.Empty;
            exif exif = new exif();
            worker.ReportProgress(0);
            if (SourcePath != string.Empty) {
                if (Directory.Exists(SourcePath)) {
                    Config.StatusBarText = "開始載入圖片...";
                    DirectoryInfo dir = new DirectoryInfo(SourcePath);
                    int FileCount, NowCount;
                    FileCount = dir.GetFiles().Length;
                    NowCount = 0;

                    foreach (FileInfo file in dir.GetFiles()) {
                        ShellFile sf = ShellFile.FromFilePath(file.FullName);
                        if (Config.GetConfig("ReadRAW") == "1") {
                            try {
                                if (sf.Properties.System.Kind.Value == null) { continue; }
                                if (sf.Properties.System.Kind.Value[0] != "picture") { continue; }
                            }
                            catch { continue; }
                        }
                        else {
                            if (file.Extension.ToLower() != ".jpg") { continue; }
                        }
                        //if ((Config.GetConfig("ReadRAW") == "1" ? ExtensionFileExt : DefaultFileExt).IndexOf(file.Extension.ToLower()) >= 0) {
                        NowCount += 1;
                        Config.StatusBarText = "載入" + file.Name;
                        worker.ReportProgress((int)((float)NowCount / (float)FileCount * 99));
                        exif.ProcessExif(file.FullName, false);
                        
                        ImageFunc.ImageList.Add(new List<string>());
                        ImageFunc.ImageList[ImageFunc.ImageList.Count - 1].Add(file.Name);
                        ImageFunc.ImageList[ImageFunc.ImageList.Count - 1].Add(Func.GetFileSize(file.Length));
                        ImageFunc.ImageList[ImageFunc.ImageList.Count - 1].Add(sf.Properties.System.ItemTypeText.Value);
                        ImageFunc.ImageList[ImageFunc.ImageList.Count - 1].Add(exif.ExifList.ContainsKey("DateTimeOriginal") ? exif.ExifList["DateTimeOriginal"] : Func.MakeExifDateFormat(file.CreationTime));
                        ImageFunc.ImageList[ImageFunc.ImageList.Count - 1].Add(exif.ExifList.ContainsKey("Make") ? exif.ExifList["Make"] : string.Empty);
                        ImageFunc.ImageList[ImageFunc.ImageList.Count - 1].Add(exif.ExifList.ContainsKey("Model") ? exif.ExifList["Model"] : string.Empty);

                        MoveImageList.Images.Add(ImageFunc.GetThumb(sf.Thumbnail.SmallBitmap, MoveImageList.ImageSize.Width, MoveImageList.ImageSize.Height));
                        //}
                    }
                    Config.StatusBarText = "顯示列表...";
                    worker.ReportProgress(100);
                }
                else {
                    MessageBox.Show("來源資料夾不存在", "目錄錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            exif = null;
        }

        private void StartLoadImage() {
            //如果有其他正在載入的工作，就取消他
            if (LoadImageBGWorker.IsBusy) { LoadImageBGWorker.CancelAsync(); }
            ImageFunc.ImageList.Clear();
            FileListView.Items.Clear();
            MoveImageList.Images.Clear();
            LoadImageBGWorker.RunWorkerAsync(txtSourcePath.Text);
        }

        private void ShowImageList() {
            FileListView.Items.Clear();
            ListViewItem lvi;
            int index = 0;
            foreach (List<string> sitem in ImageFunc.ImageList) {
                lvi = new ListViewItem(sitem[0], 0);
                for (int i = 1; i < sitem.Count; i++) {
                    lvi.SubItems.Add(sitem[i]);
                }
                lvi.SubItems.Add("");
                lvi.ImageIndex = index;
                index++;
                FileListView.Items.Add(lvi);
                Editors = new Control[] { null, null, null, null, null, null, cboxFolderPrefix };
                sitem.Clear();
            }
            //FileListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            //FileListView.Columns[5].Width = 180;
            lvi = null;
            ImageFunc.ImageList.Clear();

        }

        #endregion

        #region 搬移逼雞窩可區
        private void MoveBGWorker_DoWork(object sender, DoWorkEventArgs e) {
            ProcessMove((BackgroundWorker)sender, e);
            //ProcessMove(FileListView.SelectedItems, (BackgroundWorker)sender, e);
        }

        private void MoveBGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {
            StatusBar.Value = e.ProgressPercentage;
            StatusText.Text = Config.StatusBarText;
        }

        private void MoveBGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            if (e.Error != null) { MessageBox.Show(e.Error.Message, "發生錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error); }
            else if (e.Cancelled) { MessageBox.Show("使用者取消"); }
            else { MessageBox.Show("完成"); }
            StatusBar.Value = StatusBar.Minimum;
            StatusText.Text = string.Empty;
            StartLoadImage();
        }

        private void ProcessMove(BackgroundWorker worker, DoWorkEventArgs e) {
            Config.StatusBarText = "";
            worker.ReportProgress(0);
            int TotalCount = ImageFunc.SelectedImageList.Count;
            int NowCount = 0;
            string FolderPrefix = string.Empty, FolderBack = string.Empty;
            string Folder = string.Empty;
            string Photo = string.Empty;
            FileInfo SFile, TFile;
            foreach (List<string> SItem in ImageFunc.SelectedImageList) {
                NowCount += 1;
                if (Config.GetConfig("MoveMethod") == "M") { Config.StatusBarText = "搬移" + SItem[0] + "中..."; }
                else { Config.StatusBarText = "複製" + SItem[0] + "中..."; }

                worker.ReportProgress((int)((float)NowCount / (float)TotalCount * 100));

                if (Config.GetConfig("YearMode") == "3") { FolderPrefix = Func.ChineseDateDash(DateTime.Parse(SItem[1].Split(new char[1] { ' ' })[0].Replace(":", "-"))); }
                else { FolderPrefix = Func.DateSlashToDash(DateTime.Parse(SItem[1].Split(new char[1] { ' ' })[0].Replace(":", "-"))); }
                FolderBack = (SItem[2] == string.Empty ? string.Empty : " " + SItem[2]);

                //判斷資料夾是否存在
                Folder = Path.Combine(Config.GetConfig("TargetPath"), FolderPrefix + FolderBack);
                if (!Directory.Exists(Folder)) {
                    try { Directory.CreateDirectory(Folder); }
                    catch (Exception ex) {
                        throw new Exception("無法產生 " + Folder + "資料夾:" +
                            Environment.NewLine + ex.Message);
                    }
                }

                //檢查檔案是否存在
                Photo = Path.Combine(Folder, SItem[0]);
                //MessageBox.Show(Photo);
                if (File.Exists(Photo)) { //檔案存在，跳出對話框詢問是否覆蓋
                    SFile = new FileInfo(Path.Combine(Config.GetConfig("SourcePath"), SItem[0]));
                    TFile = new FileInfo(Photo);
                    switch (MessageBox.Show("檔案已存在!是否覆蓋?" + Environment.NewLine + Environment.NewLine +
                        "來源檔案:" + SFile.FullName + Environment.NewLine +
                        "大小:" + Func.GetFileSize(SFile.Length) + Environment.NewLine +
                        "最後修改日期:" + SFile.LastWriteTime + Environment.NewLine + Environment.NewLine +
                        "目的檔案:" + TFile.FullName + Environment.NewLine +
                        "大小:" + Func.GetFileSize(TFile.Length) + Environment.NewLine +
                        "最後修改日期:" + TFile.LastWriteTime, "檔案重複", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question)) {
                        case DialogResult.Yes: {
                                try {
                                    File.Delete(Photo);
                                    MoveCopyPhoto(Config.GetConfig("MoveMethod"), Path.Combine(Config.GetConfig("SourcePath"), SItem[0]), Photo);
                                }
                                catch (Exception ex) {
                                    MessageBox.Show("覆蓋" + TFile.FullName + "錯誤:" + Environment.NewLine + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                }
                                break;
                            }
                        case DialogResult.No:
                            continue;
                        case DialogResult.Cancel:
                            worker.CancelAsync();
                            break;
                    }
                }
                else {
                    MoveCopyPhoto(Config.GetConfig("MoveMethod"), Path.Combine(Config.GetConfig("SourcePath"), SItem[0]), Photo);
                }
            }
            SFile = null;
            TFile = null;
            ImageFunc.SelectedImageList.Clear();
        }

        private void MoveCopyPhoto(string Type, string SourcePhoto, string TargetPhoto) {
            try {
                if (Type == "M") { File.Move(SourcePhoto, TargetPhoto); }
                else { File.Copy(SourcePhoto, TargetPhoto); }
            }
            catch (Exception ex) {
                MessageBox.Show("移動檔案錯誤:" + Environment.NewLine + ex.Message, "錯誤", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void StartMove() {
            ImageFunc.SelectedImageList.Clear();
            foreach (ListViewItem Item in FileListView.SelectedItems) {
                ImageFunc.SelectedImageList.Add(new List<string>());
                ImageFunc.SelectedImageList[ImageFunc.SelectedImageList.Count - 1].Add(Item.Text);
                ImageFunc.SelectedImageList[ImageFunc.SelectedImageList.Count - 1].Add(Item.SubItems[3].Text);
                ImageFunc.SelectedImageList[ImageFunc.SelectedImageList.Count - 1].Add(Item.SubItems[6].Text);
            }
            MoveBGWorker.RunWorkerAsync();
        }
        #endregion


        /// <summary>
        /// 判斷現在視窗是否為最大化
        /// </summary>
        /// <returns></returns>
        private bool ThisIsMax() {
            if (this.WindowState == FormWindowState.Maximized) { return true; }
            else { return false; }
        }

        private void testToolStripMenuItem_Click(object sender, EventArgs e) {
            new Make().ShowDialog();
        }

    }
}
