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
                // �]�wTitle
                alertDialog1.SetTitle("ĵ�i����TITLE");
                // ����
                alertDialog1.SetMessage("Hello , �ڬO����");
                alertDialog1.SetIcon(Resource.Drawable.Icon);
                //�Ĥ@�����s
                alertDialog1.SetButton("OK", (sender, args) => Toast.MakeText(this, "OK�Q���U�F", ToastLength.Long).Show());
                //�ĤG�����s
                alertDialog1.SetButton2("����", (sender, args) => Toast.MakeText(this, "�����Q���U�F", ToastLength.Short).Show());
                alertDialog1.Show();

            };
        }

        /// <summary>
        /// �P�_�O�_�i���`�s�_�۾�
        /// </summary>
        /// <returns></returns>
        private bool IsThereAnAppToTakePictures()
        {
            var intent = new Intent(MediaStore.ActionImageCapture);
            IList<ResolveInfo> availableActivities = PackageManager.QueryIntentActivities(intent, PackageInfoFlags.MatchDefaultOnly);
            return availableActivities != null && availableActivities.Count > 0;
        }

        /// <summary>
        /// �b�~���x�s�˸m���إ߰_�ɮק��åB�ϥη�@�Ȧs
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
        /// �I����ӫ��s��Ұʩ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="eventArgs"></param>
        private void TakeAPicture(object sender, EventArgs eventArgs)
        {
            //�ϥ�intent �s�_��Ӱʧ@
            var intent = new Intent(MediaStore.ActionImageCapture);

            //�^�s���ɦW
            _file = new Java.IO.File(_dir, String.Format("myPhoto_{0}.jpg", Guid.NewGuid()));

            intent.PutExtra(MediaStore.ExtraOutput, Android.Net.Uri.FromFile(_file));
            //���ݵ��G���I�sActivity 
            //�i�H�Ѧ� http://no2don.blogspot.com/2013/07/xamarin-startactivityforresult.html
            StartActivityForResult(intent, 0);
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);

            // �����i�H�b�Ϥ��w���Q�ϥ� 
            // �o�@�q���g���|�v�T�\��u�O�b�Ϥ��w���A�ä��|��ܦ��i�Ӥ�
            var mediaScanIntent = new Intent(Intent.ActionMediaScannerScanFile);
            var contentUri = Android.Net.Uri.FromFile(_file);
            mediaScanIntent.SetData(contentUri);
            SendBroadcast(mediaScanIntent);
            //

            // �N����ܦbImageView�W��
            // �]���קK��Ӥ��Ӥj�ɭPapp crash 
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