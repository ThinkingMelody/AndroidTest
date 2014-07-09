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

namespace AndroidApplication1
{
    [Activity(Label = "atyOptionMenu", MainLauncher = true)]
    public class atyOptionMenu : Activity
    {
        public const int aboutBtnID = Menu.First;
        public const int exitBtnID = Menu.First + 1;
        public const int newBtnID = Menu.First + 2;
        public const int delBtnID = Menu.First + 3;
        public const int modBtnID = Menu.First + 4;
        public const int playBtnID = Menu.First + 5;
        public const int addBtnID = Menu.First + 6;
        public const int searchBtnID = Menu.First + 7;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            SetContentView(Resource.Layout.ltOptionMenu);
            // Create your application here


        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            //MenuInflater.Inflate(Resource.Menu.mymenu, menu);
            //return base.OnCreateOptionsMenu(menu);
            
            menu.Add(0, aboutBtnID, 0, "確認").SetIcon(Resource.Drawable.Icon);
            menu.Add(0, exitBtnID, 0, "取消");
            menu.Add(0, newBtnID, 0, "新的");
            menu.Add(0, delBtnID, 0, "刪除");
            menu.Add(0, modBtnID, 0, "修改");
            menu.Add(0, playBtnID, 0, "開始");
            menu.Add(0, addBtnID, 0, "加上");
            menu.Add(0, searchBtnID, 0, "查詢");
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            switch (item.ItemId)
            {
                //case Resource.Id.new_game:
                case aboutBtnID:
                    openDialog("按下了OK確認按鈕!!");
                    //Toast.MakeText(this, "OK!", ToastLength.Short).Show();
                    return true;
                //case Resource.Id.help:
                case exitBtnID:
                    openDialog("按下Cancel取消按鈕!!");
                    //Toast.MakeText(this, "Cancel!", ToastLength.Short).Show();
                    //do something
                    return true;
            }
            //return base.OnOptionsItemSelected(item);
            return true;
        }

        public void openDialog(string strMessage)
        {
            var alertDialog = new AlertDialog.Builder(this).Create();

            alertDialog.SetTitle("new_game警告視窗!!");
            alertDialog.SetMessage("new_game文字內容!!");
            alertDialog.SetButton("確認", (sender, args) => Toast.MakeText(this, strMessage, ToastLength.Short).Show());
            alertDialog.SetButton2("取消", (sender, args) => Toast.MakeText(this, strMessage, ToastLength.Short).Show());
            alertDialog.Show();
        }
    }
}