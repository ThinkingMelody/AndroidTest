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
    [Activity(Label = "atFlate")]
    public class atFlate : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.ltFlate);

            var btn1 = FindViewById<Button>(Resource.Id.btn1);

            var custContainer = FindViewById<LinearLayout>(Resource.Id.relativeContainer);

            btn1.Click += (Object sender, EventArgs e) =>
                {
                    LayoutInflater.Inflate(Resource.Layout.CustControlLayout, custContainer);

                    var btnAssignText = FindViewById<Button>(Resource.Id.btnAssignText);

                    btnAssignText.Id = new Random().Next(5000, int.MaxValue);

                    var editTextContent = FindViewById<EditText>(Resource.Id.editTextContent);

                    editTextContent.Id = new Random().Next(5000, int.MaxValue);

                    btnAssignText.Click += delegate
                    {
                        Toast.MakeText(this, editTextContent.Text, ToastLength.Short).Show();
                    };
                };
        }
    }
}