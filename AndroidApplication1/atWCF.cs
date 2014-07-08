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
using System.Collections;

namespace AndroidApplication1
{
    [Activity(Label = "atWCF", MainLauncher = true)]
    public class atWCF : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.ltWCF);

            var MyWCF = new CylifeAppReference.AppServices();

            bool blResult, blResultSpecified = false;
            
            MyWCF.Login("B121226579", "2xoiglxA",out blResult, out blResultSpecified);


            var AccountInfo = MyWCF.getAccountInfo("B121226579", "0");


            Toast.MakeText(this, AccountInfo[0].Value.ToString(), ToastLength.Long).Show();

        }
    }
}