using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using extensions;
using exif;
using Microsoft.WindowsAPICodePack.Shell;

namespace MiniTool
{
    public partial class ViewExif : Form
    {
        exif.exif MyExif = new exif.exif();
        string[] FileNames;
        PictureBox GPSBox = new PictureBox();
        ListViewGroup ExifGroup = new ListViewGroup();
        ListViewGroup MakerGroup = new ListViewGroup();
        ListViewGroup SpecialGroup = new ListViewGroup();

        public ViewExif(string[] FileNames) {
            InitializeComponent();
            this.FileNames = FileNames;

            //產生一個GPS按鈕的PictureBox
            #region 產生一個GPS按鈕的PictureBox
            GPSBox.Anchor = AnchorStyles.Right | AnchorStyles.Bottom;
            GPSBox.BackColor = Color.Transparent;
            GPSBox.Image = Properties.Resources.GPS_1;
            GPSBox.SizeMode = PictureBoxSizeMode.AutoSize;
            GPSBox.Visible = true;
            ThumbBox.Controls.Add(GPSBox);
            GPSBox.Location = new Point(ThumbBox.Width - GPSBox.Width - 5, ThumbBox.Height - GPSBox.Height - 5);
            GPSBox.Cursor = Cursors.Hand;
            toolTip1.SetToolTip(GPSBox, "此照片有GPS內容，點一下顯示地點");
            GPSBox.BringToFront();
            GPSBox.MouseEnter += new EventHandler(GPSBox_MouseEnter);
            GPSBox.MouseLeave += new EventHandler(GPSBox_MouseLeave);
            GPSBox.Click += new EventHandler(GPSBox_Click);
            #endregion

            
        }

        private void ViewExif_Load(object sender, EventArgs e) {
            ExifListView.Columns[0].Width = 135;
            ExifListView.Columns[1].Width = 135;
            ShowBGWorker.RunWorkerAsync();
        }

        #region 顯示圖片列表逼雞窩可區
        private void ShowBGWorker_DoWork(object sender, DoWorkEventArgs e) {
            ShowList((BackgroundWorker)sender, e);

        }

        private void ShowBGWorker_ProgressChanged(object sender, ProgressChangedEventArgs e) {

        }

        private void ShowBGWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e) {
            FileListView.Items[0].Selected = true;
            FileListView.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
        }

        private void ShowList(BackgroundWorker worker, DoWorkEventArgs e) {
            FileImageList.Images.Clear();
            string FolderPath = Config.GetConfig("SourcePath");
            int index = 0;
            foreach (string FileName in FileNames) {
                AddImageList(Path.Combine(FolderPath, FileName));
                AddListItem(FileName, Path.Combine(FolderPath, FileName), index);
                index++;
            }
        }

        private delegate void AddListItemCallBack(string Text, string Path, int ImageIndex);
        private void AddListItem(string Text, string Path, int ImageIndex) {
            if (this.InvokeRequired) {
                AddListItemCallBack c = new AddListItemCallBack(AddListItem);
                this.Invoke(c, Text, Path, ImageIndex);
            }
            else {
                FileListView.Items.Add(new ListViewItem() { Text = Text, Tag = Path, ImageIndex = ImageIndex });
            }

        }

        private delegate void AddImageListCallBack(string Path);
        private void AddImageList(string Path) {
            if (this.InvokeRequired) {
                AddImageListCallBack c = new AddImageListCallBack(AddImageList);
                this.Invoke(c, Path);
            }
            else {
                FileImageList.Images.Add(ImageFunc.GetThumb(ShellFile.FromFilePath(Path).Thumbnail.SmallBitmap, FileImageList.ImageSize.Width, FileImageList.ImageSize.Height));
            }
        }
        #endregion

        /// <summary>
        /// 左邊檔案列表選取檔案有變動的處理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileListView_SelectedIndexChanged(object sender, EventArgs e) {
            ExifListView.Items.Clear();
            bool showMakerNote = false;
            foreach (ListViewItem item in FileListView.SelectedItems) {
                showMakerNote = false;
                MyExif.ProcessExif(item.Tag.ToString(), true);
                ExifGroup.Header = "基本Exif";
                ExifListView.Groups.Add(ExifGroup);
                if (MyExif.ExifList.ContainsKey("Make") && MyExif.ExifList["Make"] != string.Empty) {
                    MakerGroup.Header = MyExif.ExifList["Make"] + "自訂內容";
                    ExifListView.Groups.Add(MakerGroup);
                }
                try {
                    if (MyExif.CameraCustom.Values.Count > 0) {
                        SpecialGroup.Header = MyExif.CameraCustom.DisplayName;
                        ExifListView.Groups.Add(SpecialGroup);
                        showMakerNote = true;
                    }
                }
                catch { }

                if (MyExif.ErrorMsg != string.Empty) { MessageBox.Show(MyExif.ErrorMsg); }
                //MessageBox.Show(MyExif.MakerNoteCount.ToString());
                //MessageBox.Show(MyExif.ErrorMsg);
                //new ShowMakerNote(MyExif.MakerExifList).ShowDialog();
                
                if (MyExif.HadGPS) {
                    GPSBox.Visible = true;
                    SetMapLocation();
                }
                else {
                    GPSBox.Visible = false;
                    MapBox.ImageLocation = string.Empty;
                    MapBox.Visible = false;
                }
                ThumbBox.Image = ImageFunc.GetThumb(ShellFile.FromFilePath(item.Tag.ToString()).Thumbnail.LargeBitmap, ThumbBox.Width, ThumbBox.Height);
                foreach (string tag in MyExif.ExifList.Keys) {
                    ListViewItem lvi = new ListViewItem(tag, ExifGroup);
                    lvi.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = MyExif.ExifList[tag] });
                    ExifListView.Items.Add(lvi);
                }

                foreach (string tag in MyExif.MakerExifList.Keys) {
                    ListViewItem lvi = new ListViewItem(tag.Split(new string[]{":@@:"}, StringSplitOptions.RemoveEmptyEntries)[0], MakerGroup);
                    lvi.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = MyExif.MakerExifList[tag] });
                    ExifListView.Items.Add(lvi);
                }

                if (showMakerNote) {
                    foreach (string tag in MyExif.CameraCustom.Values.Keys) {
                        ListViewItem lvi = new ListViewItem(tag.Split(new string[] { ":@@:" }, StringSplitOptions.RemoveEmptyEntries)[0], SpecialGroup);
                        lvi.SubItems.Add(new ListViewItem.ListViewSubItem() { Text = MyExif.CameraCustom.Values[tag] });
                        ExifListView.Items.Add(lvi);
                    }
                }
            }

        }

        /// <summary>
        /// 滑鼠移到Exif列表上方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ExifListView_MouseEnter(object sender, EventArgs e) {
            this.ExifListView.Focus();
        }

        /// <summary>
        /// 滑鼠移到檔案列表上方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FileListView_MouseEnter(object sender, EventArgs e) {
            this.FileListView.Focus();
        }

        /// <summary>
        /// 縮圖區大小有變動，重新繪製縮圖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ThumbBox_SizeChanged(object sender, EventArgs e) {
            if (FileListView.SelectedItems.Count == 1) {
                ThumbBox.Image = ImageFunc.GetThumb(ShellFile.FromFilePath(FileListView.SelectedItems[0].Tag.ToString()).Thumbnail.LargeBitmap, ThumbBox.Width, ThumbBox.Height);
            }
        }

        /// <summary>
        /// 滑鼠移到GPS按鈕上方
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSBox_MouseEnter(object sender, EventArgs e) {
            GPSBox.Image = Properties.Resources.GPS_2;
        }

        /// <summary>
        /// 滑鼠離開GPS按鈕
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSBox_MouseLeave(object sender, EventArgs e) {
            GPSBox.Image = Properties.Resources.GPS_1;
        }

        /// <summary>
        /// 按下GPS按鈕，要顯示地圖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void GPSBox_Click(object sender, EventArgs e) {
            if (MapBox.Visible == false) {
                MapBox.Visible = true;
                if (MapBox.ImageLocation != string.Empty) { MapBox.LoadAsync(); }
            }
            else { MapBox.Visible = false; }
        }

        /// <summary>
        /// 地圖大小變動，重新取得一次正確大小的地圖
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MapBox_SizeChanged(object sender, EventArgs e) {
            //MapBox.CancelAsync();
            SetMapLocation();
            MapBox.LoadAsync();
        }

        private void SetMapLocation() {
            MapBox.ImageLocation = "http://maps.google.com/maps/api/staticmap?" +
                        "center=" + MyExif.Latitude + "," + MyExif.Longitude +
                        "&markers=color:green%7C" + MyExif.Latitude + "," + MyExif.Longitude +
                        "&size=" + MapBox.Width.ToString() + "x" + MapBox.Height.ToString() + "&sensor=false" +
                        "&zoom=" + Config.GetConfig("Zoom") + "&format=png32";
        }

        private void MapBox_Click(object sender, EventArgs e) {
            MapBox.Visible = false;
        }

        private void MapBox_VisibleChanged(object sender, EventArgs e) {
            if (MapBox.Visible) {
                int x = (this.MainSplitContainer.Width - MapBox.Width) / 2;
                int y = (this.MainSplitContainer.Height - MapBox.Height) / 2;
                MapBox.Location = new Point(x, y);
            }
        }

        private void btnClose_Click(object sender, EventArgs e) {
            this.Close();
        }

    }
}
