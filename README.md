#關於MiniTool

因為我很懶，通常拍了很多天照片之後再一次轉檔，然後依照不同拍攝日期，搬到以拍照日期命名的資料夾內。時間一久照片一多，手動選取複製會累死人，網路找過沒類似程式，所以這隻程式就產生了。

舊版讀取Exif是用很笨的方法，讀取速度有點慢。經過好長一段時間研究了Exif的Spec，利用空閒時間寫了個直接處理照片的元件，直接從二進位資料裡抓Exif資訊出來，這樣速度比較快，訊息也比較完整。


主要功能：

- 根據照片拍照日期，產生以日期命名的資料夾，將照片搬移/複製到該資料夾中
- 可自訂資料夾名稱
- 增加顯示相片縮圖功能，會在列表顯示28x28的縮圖，更能方便選擇想要搬移的圖片。
- 增加圖片格式支援，可選擇僅JPG或是所有圖片格式。如果有安裝MS CodecPack的話還可支援相機RAW檔。 **_*(Exif內各廠牌自己的標記僅支援Canon)*_**
- 檢視完整Exif功能，選取圖片按右鍵，功能表內選擇「檢視完整Exif」。
- 顯示GPS地圖功能，若圖片內有GPS座標資訊，可顯示拍攝地點的地圖。
