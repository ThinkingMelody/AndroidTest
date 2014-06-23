using System;

using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using System.Collections.Generic;
using Android.Util;
using System.IO;
using System.Text;
using Android.Support.V4.App;
using Android.Text;

namespace AndroidApplication1
{
    //[Activity(Label = "Phoneword", MainLauncher = true, Icon = "@drawable/icon")]
    [Activity(Label = "Phoneword")]
    public class MainActivity : Activity
    {
        //static readonly List<string> phoneNumbers = new List<string>();
        int _counter = 0;

        private static string[] strTaiwan = { "Keelung", "Taipei", "Taoyuan", "Tainam", "Taichung", "Yilan", "Kaohsiung" };
        
        protected override void OnCreate(Bundle bundle)
        {

            //StartService(new Intent(this, typeof(DemoService)));

            //Log.Debug(GetType().FullName, "Activity A - OnCreate");

            base.OnCreate(bundle);
            
            #region "Demo1-3"

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.LayoutNotification);

            TextView tv1 = FindViewById<TextView>(Resource.Id.textView1);
            AutoCompleteTextView atv = FindViewById<AutoCompleteTextView>(Resource.Id.tvCity);
            CheckBox chk1 = FindViewById<CheckBox>(Resource.Id.checkBox1);
            
            ArrayAdapter autoCompleteAdapter = new ArrayAdapter(this, Android.Resource.Layout.SimpleDropDownItem1Line, strTaiwan);

            atv.Adapter = autoCompleteAdapter;


            ToggleButton tbtnTest = FindViewById<ToggleButton>(Resource.Id.toggleButton1);

            tbtnTest.Click += (object sender, EventArgs e) =>
                {
                    if (tbtnTest.Checked)
                    {
                        tv1.Text = "Now is On";
                    }
                    else
                    {
                        tv1.Text = "Now is Off";
                    }
                };

            chk1.Click += (object sender, EventArgs e) =>
                {
                    if (chk1.Checked)
                    {
                        tv1.Text = "CheckBox1 is V";
                    }
                    else
                    {
                        tv1.Text = "CheckBox1 is null";
                    }
                };
            
            EditText etxtPhoneNumber = FindViewById<EditText>(Resource.Id.etxtPhoneNumberText);
            //Button btnTranslate = FindViewById<Button>(Resource.Id.btnTranslate);
            //Button btnCall = FindViewById<Button>(Resource.Id.btnCall);
            //Button btnCallHistory = FindViewById<Button>(Resource.Id.btnCallHistory);
            //Button btnConfigInstance = FindViewById<Button>(Resource.Id.btnConfigInstance);
            //Button btnMyButton = FindViewById<Button>(Resource.Id.btnMyButton);
            //Button btnClickButton = FindViewById<Button>(Resource.Id.btnClickButton);
            //Button btnToastImage = FindViewById<Button>(Resource.Id.btnToastImage);
            //Button btnChild = FindViewById<Button>(Resource.Id.btnChild);

            Button btnBaseNotification = FindViewById<Button>(Resource.Id.btnBaseNotification);
            Button btnIDNotification = FindViewById<Button>(Resource.Id.btnIDNotification);
            Button btnStartActivity = FindViewById<Button>(Resource.Id.btnStartActivity);


            btnBaseNotification.Click += (Object sender, EventArgs e) =>
                {
                    NotificationCompat.Builder brBase = new NotificationCompat.Builder(this)
                        .SetContentTitle("測試同ID的通知!!")
                        .SetSmallIcon(Resource.Drawable.ic_go)
                        .SetContentText("你點擊了" + _counter + "次");

                    var NotificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                    NotificationManager.Notify(0, brBase.Build());
                    _counter++;
                };

            btnIDNotification.Click += (Object sender, EventArgs e) =>
                {
                    NotificationCompat.Builder brBase = new NotificationCompat.Builder(this)
                        .SetContentTitle("測試不同ID的通知!!")
                        .SetSmallIcon(Resource.Drawable.ic_go)
                        .SetContentText("你點擊了" + _counter + "次");

                    var NotificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                    NotificationManager.Notify(_counter, brBase.Build());
                    _counter++;
                };

            btnStartActivity.Click += delegate
                {
                    //成立一個新Intent
                    //並在bundle中帶資料
                    var resultIntent = new Intent(this, typeof(ActivityNotiCall));
                    var BundleData = new Bundle();

                    BundleData.PutString("user", "doma");
                    resultIntent.PutExtras(BundleData);

                    TaskStackBuilder stackBuilder = TaskStackBuilder.Create(this);
                    stackBuilder.AddNextIntent(resultIntent);

                    //建立一個PendingIntent 使用者點擊後透過TaskStackBuilder 送至新的Activity
                    PendingIntent resultPendingIntent = stackBuilder.GetPendingIntent(0, (int)PendingIntentFlags.UpdateCurrent);

                    NotificationCompat.Builder builder = new NotificationCompat.Builder(this)
                    .SetAutoCancel(true)
                    .SetContentTitle("測試叫起Activity")
                    .SetSmallIcon(Resource.Drawable.ic_go)
                    .SetNumber(_counter)
                    .SetContentText("點我啟動")
                        //帶入 PendingIntent
                    .SetContentIntent(resultPendingIntent);


                    var notificationManager = (NotificationManager)GetSystemService(Context.NotificationService);
                    notificationManager.Notify(_counter, builder.Build());
                    _counter++;
                };
            //btnClickButton.Click += delegate
            //    {
            //        Toast.MakeText(this, "測試訊息Toast吳許!!", ToastLength.Short).Show();
            //    };

            //btnToastImage.Click += (object sender, EventArgs e) =>
            //    {
            //        var view = LayoutInflater.Inflate(Resource.Layout.Login, null);

            //        var imgMain = view.FindViewById<ImageView>(Resource.Id.imageView1);
            //        var txt = view.FindViewById<TextView>(Resource.Id.textView1);

            //        //載入Drawable中的png檔
            //        imgMain.SetImageResource(Resource.Drawable.ic_go);
            //        txt.Text = "圖檔加文字Toast!!";
            //        var toast = new Toast(this);
            //        toast.View = view;
            //        toast.Show();
            //    };

            //btnChild.Click += (object sender, EventArgs e) =>
            //    {
            //        var intentAct2 = new Intent(this, typeof(Child1));

            //        intentAct2.PutExtra("username", "thinker");

            //        StartActivity(intentAct2);
            //    };

            //btnConfigInstance.Click += (object sender, EventArgs e) =>
            //    {
            //        //requestcode設定讓OnactivityResult，可以判斷當出發出的動機
            //        StartActivityForResult(typeof(NonConfigInstanceActivity), 1);

            //    };

            //btnCall.Enabled = false;

            //string strTranslatedNumber = string.Empty;

            //btnTranslate.Click += (object sender, EventArgs e) =>
            //{
            //    strTranslatedNumber = CPhoneTranslator.ToNumber(etxtPhoneNumber.Text);

            //    if (string.IsNullOrWhiteSpace(strTranslatedNumber))
            //    {
            //        btnCall.Text = "Call";
            //        btnCall.Enabled = false;
            //    }
            //    else
            //    {
            //        btnCall.Text = "Call" + strTranslatedNumber;
            //        btnCall.Enabled = true;
            //    }
            //};

            //btnCall.Click += (object sender, EventArgs e) =>
            //{
            //    var dlgCall = new AlertDialog.Builder(this);

            //    dlgCall.SetMessage("Call" + strTranslatedNumber + "?");
            //    dlgCall.SetNeutralButton("Call", delegate
            //    {

            //        phoneNumbers.Add(strTranslatedNumber);

            //        btnCallHistory.Enabled = true;

            //        //Create intent to dial phone
            //        var ittCall = new Intent(Intent.ActionCall);
            //        ittCall.SetData(Android.Net.Uri.Parse("tel:" + strTranslatedNumber));
            //        StartActivity(ittCall);
            //    });
            //    dlgCall.SetNegativeButton("Cancel", delegate { });

            //    dlgCall.Show();
            //};

            //btnCallHistory.Click += (object sender, EventArgs e) =>
            //{
            //    Intent ittHistory = new Intent(this, typeof(CallHistoryActivity));

            //    ittHistory.PutStringArrayListExtra("phone_numbers", phoneNumbers);

            //    StartActivity(ittHistory);
            //};

            //btnConfigInstance.Click += (object sender, EventArgs e) =>
            //{
            //    Intent ittConfigInstance = new Intent(this, typeof(NonConfigInstanceActivity));

            //    StartActivity(ittConfigInstance);
            //};


            //FindViewById<Button>(Resource.Id.btnMyButton).Click += (sender, args) =>
            //{
            //    var intent = new Intent(this, typeof(NonConfigInstanceActivity));
            //    StartActivity(intent);
            //};
            //if (bundle != null)
            //{
            //    _counter = bundle.GetInt("click_count", 0);
            //    Log.Debug(GetType().FullName, "Recovered instance state");
            //}

            //var clickbutton = FindViewById<Button>(Resource.Id.btnClickButton);
            //clickbutton.Text = Resources.GetString(Resource.String.counterbutton_text, _counter);
            //clickbutton.Click += (object sender, System.EventArgs e) =>
            //{
            //    _counter++;
            //    clickbutton.Text = Resources.GetString(Resource.String.counterbutton_text, _counter);
            //};

            #endregion

            //var tv = new TextView(this);

            //string content;
            //using (StreamReader sr = new StreamReader(Assets.Open("asset.txt"), Encoding.GetEncoding(950)))
            //{
            //    content = sr.ReadToEnd();
            //}

            //tv.Text = content;

            //SetContentView(tv);
        }

        //protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        //{
        //    base.OnActivityResult(requestCode, resultCode, data);

        //    if (requestCode == 1 && resultCode == Result.Ok)
        //    {
        //        Toast.MakeText(this, "選取的結果：" + data.GetStringExtra("hero"), ToastLength.Short).Show();
        //    }
        //}

        //protected override void OnSaveInstanceState(Bundle outState)
        //{
        //    outState.PutInt("click_count", _counter);
        //    Log.Debug(GetType().FullName, "Saving instance state");

        //    base.OnSaveInstanceState(outState);
        //}

        //c = bundle.GetInt ("counter", -1);

        //protected override void OnRestoreInstanceState(Bundle saveState)
        //{
        //    base.OnRestoreInstanceState(saveState);
        //    var myString = saveState.GetString("myString");
        //    var myBool = GetBoolean("myBool");
        //}

    }
}

