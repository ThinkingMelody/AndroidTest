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
    [Activity(Label = "Child1")]
    public class Child1 : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.Login);

            var username = Intent.GetStringExtra("username") ?? "無資料";
            var tvChild1View = FindViewById<TextView>(Resource.Id.textView1);

            tvChild1View.Text = "傳來的資料:" + username;
        }
    }
}