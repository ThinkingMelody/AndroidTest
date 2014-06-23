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
    [Activity(Label = "ActivityNotiCall")]
    public class ActivityNotiCall : Activity
    {
        private DateTime dtNow;
        const int DATE_DIALOG_ID = 0;
        TextView dateDisplay;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.NotificationCall);

            var textView1 = FindViewById<TextView>(Resource.Id.textView1);

            //Toast.MakeText(this, "Toast Test!!", ToastLength.Short).Show();

            //textView1.Text = "doma";
            //textView1.Text = this.Intent.GetStringExtra("user");
            //textView1.Text = "can't use GetStringExtra"

            var txtDate = FindViewById<TextView>(Resource.Id.textView2);
            var btnDate = FindViewById<Button>(Resource.Id.button6);
            var dpDate = FindViewById<DatePicker>(Resource.Id.datePicker1);
            dateDisplay = FindViewById<TextView>(Resource.Id.textView3);
            var pickDate = FindViewById<Button>(Resource.Id.button7);

            btnDate.Click += (object sender, EventArgs e) =>
            {
                txtDate.Text = dpDate.Year + "-" + (dpDate.Month + 1) + "-" + dpDate.DayOfMonth;
            };

            pickDate.Click += delegate
            {
                ShowDialog(DATE_DIALOG_ID);
            };

            dtNow = DateTime.Today;


        }

        private void UpdateDispaly()
        {
            dateDisplay.Text = dtNow.ToString("d");
        }

        void OnDateSet(object sender, DatePickerDialog.DateSetEventArgs e)
        {
            this.dtNow = e.Date;
            UpdateDispaly();
        }

        protected override Dialog OnCreateDialog(int id)
        {
            switch(id)
            {
                case DATE_DIALOG_ID:
                    return new DatePickerDialog(this, OnDateSet, dtNow.Year, dtNow.Month -1, dtNow.Day);
            }
            return null;
        }
    }
}