using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Provider;
using Android.Content.PM;
using Android.Graphics;

namespace AndroidApplication1
{
    [Activity(Label = "atyCamera")]
    public class atyCamera : Activity
    {
        Java.IO.File _file;
        Java.IO.File _dir;
        ImageView _imageView;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ltCamera);

            if (IsThereAnAppToTakePictures())
            {
                CreateDirectoryForPictures();

                Button button = FindViewById<Button>(Resource.Id.btnCamera);
                _imageView = FindViewById<ImageView>(Resource.Id.imageView1);

                button.Click += TakeAPicture;
            }
            // Create your application here

            var btn1 = FindViewById<Button>(Resource.Id.btnAlert);

            btn1.Click += delegate
            {
                var alertDialog1 = new AlertDialog.Builder(this).Create();
                // 設定Title
                alertDialog1.SetTitle("警告視窗TITLE");
                // 內文
                alertDialog1.SetMessage("Hello , 我是內文");
                alertDialog1.SetIcon(Resource.Drawable.Icon);
                //第一顆按鈕
                alertDialog1.SetButton("OK", (sender, args) => Toast.MakeText(this, "OK被按下了", ToastLength.Long).Show());
                //第二顆按鈕
                alertDialog1.SetButton2("取消", (sender, args) => Toast.MakeText(this, "取消被按下了", ToastLength.Short).Show());
                alertDialog1.Show();

            };
        }

        /// <summary>
        /// 判斷是否可正常叫起相機
        /// </summary>
        /// <returns></returns>
        private bool IsThereAnAppToTakePictures()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        /// <summary>
        /// 在外部儲存裝置中建立起檔案夾並且使用當作暫存
        /// </summary>
        private void CreateDirectoryForPictures()
        {
            _dir = new Java.IO.File(Android.OS.Environment.GetExternalStoragePublicDirectory(Android.OS.Environment.DirectoryPictures), "CameraAppDemo");
            if (!_dir.Exists())
            {
                _dir.Mkdirs();
            }
        }

        /// <summary>
        /// 點擊拍照按鈕後啟動拍照
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            //使用intent 叫起拍照動作
            var intent = new Intent(MediaStore.ActionImageCapture);

            //回存的檔名
            _file = new Java.IO.File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
            //等待結果的呼叫Activity 
            //可以參考 http://no2don.blogspot.com/2013/07/xamarin-startactivityforresult.html
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // 讓此可以在圖片庫中被使用 
            // 這一段不寫不會影響功能只是在圖片庫中，並不會顯示此張照片
            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(_file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
            //

            // 將其顯示在ImageView上面
            // 因為避免拍照片太大導致app crash 
            int height = _imageView.Height;
            int width = Resources.DisplayMetrics.WidthPixels;
            //using (Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height))
            using (Bitmap bitmap = _file.Path.LoadAndResizeBitmap(width, height))
            {
                _imageView.SetImageBitmap(bitmap);
            }
        }
    }
}