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
using System.IO.IsolatedStorage;
using System.IO;

namespace AndroidApplication1
{
    [Activity(Label = "IsolatedStorage")]
    public class IsolatedStorage : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Isolate);

            Button btnSave = FindViewById<Button>(Resource.Id.btnSave);
            Button btnLoad = FindViewById<Button>(Resource.Id.btnLoad);
            EditText txtMain = FindViewById<EditText>(Resource.Id.txtMain);


            btnSave.Click += (object sender, EventArgs e) =>
                {
                    IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication();

                    if (storeFile.FileExists("UserInfo/data.txt"))
                    {
                        storeFile.DeleteFile("UserInfo/data.txt");
                    }

                    storeFile.CreateDirectory("UserInfo");

                    StreamWriter fwStream = new StreamWriter(new IsolatedStorageFileStream("UserInfo/data.txt", FileMode.OpenOrCreate, storeFile));

                    fwStream.Write(txtMain.Text);
                    fwStream.Close();
                    storeFile.Dispose();
                    Toast.MakeText(this, "Source Save", ToastLength.Short).Show();
                };

            btnLoad.Click += (object sender, EventArgs e) =>
                {
                    IsolatedStorageFile storeFile = IsolatedStorageFile.GetUserStoreForApplication();

                    StreamReader srFileReader = null;

                    if (storeFile.FileExists("UserInfo/data.txt"))
                    {
                        try
                        {
                            srFileReader = new StreamReader(new IsolatedStorageFileStream("UserInfo/data.txt", FileMode.Open, storeFile));
                            var result = srFileReader.ReadToEnd();
                            Toast.MakeText(this, result, ToastLength.Long).Show();
                            srFileReader.Close();
                        }
                        catch (Exception ex)
                        {
                            Toast.MakeText(this, "Error:" + ex.Message, ToastLength.Long).Show();
                        }
                    }
                    else
                    {
                        Toast.MakeText(this, "Error:" + "Sorry！找不到檔案！", ToastLength.Long).Show();
                    }
                };
        }


        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //將Resoirces下的Menu/OptionMenu.xml 載入
            base.OnCreateOptionsMenu(menu);
            MenuInflater.Inflate(Resource.Menu.OptionMenu, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            base.OnOptionsItemSelected(item);
            switch (item.ItemId)
            {
                //如果選取的是 開啟當麻許的超技八
                case Resource.Id.itemMenu1:
                    Toast.MakeText(this, "開啟當麻許的超技八", ToastLength.Short).Show();
                    //開啟一個Inetnt 並且將此呼叫起來
                    StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("http://no2don.blogspot.com")));
                    return true;
                //如果選取的是 開啟Market的愛料理 Download
                case Resource.Id.itemMenu2:
                    Toast.MakeText(this, "開啟Market的愛料理 Download", ToastLength.Short).Show();
                    //開啟一個Inetnt 並且將此呼叫起來
                    StartActivity(new Intent(Intent.ActionView, Android.Net.Uri.Parse("market://details?id=" + "com.polydice.icook")));
                    return true;
                default:
                    return base.OnOptionsItemSelected(item);
            }
        }
    }
}