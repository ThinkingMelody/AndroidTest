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
using System.IO;

namespace AndroidApplication1
{
    [Activity(Label = "atyInternalStorage")]
    public class atyInternalStorage : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ltInternalStorage);
            // Create your application here

            var btnWriteInternal = FindViewById<Button>(Resource.Id.btnWriteInternal);
            var btnReadInternal = FindViewById<Button>(Resource.Id.btnReadInternal);
            var btnWriteFolder = FindViewById<Button>(Resource.Id.btnWriteFolder);
            var btnReadFolder = FindViewById<Button>(Resource.Id.btnReadFolder);

            btnWriteInternal.Click += (object sender, EventArgs e) =>
                {
                    WriteAllText("Note.txt", "´ú¸Õ¼g¤JInternalStorage!!" + DateTime.Now.ToString());
                };

            btnReadInternal.Click += delegate
            {
                try
                {
                    var message = ReadAllText("Note.txt");
                    Toast.MakeText(this, message, ToastLength.Short).Show();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, ex.Message, ToastLength.Short).Show();
                }
            };

        }

        public void WriteAllText(string filename, string content)
        {
            if (filename[0] != '/')
            {
                filename = '/' + filename;
            }

            if (filename.Contains("/"))
            {
                Directory.CreateDirectory(GetFileStreamPath("") + filename.Substring(0, filename.LastIndexOf('/')));
            }

            System.IO.File.WriteAllText(GetFileStreamPath("") + filename, content);
        }

        public string ReadAllText(string filename)
        {
            try
            {
                if (filename[0] != '/')
                {
                    filename = '/' + filename;
                }

                return System.IO.File.ReadAllText(GetFileStreamPath("/") + filename);
            }
            catch
            {
                throw;
            }
        }
    }
}