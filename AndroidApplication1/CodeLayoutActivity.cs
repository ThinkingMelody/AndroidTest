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
    //[Activity(Label = "RotationActivity", MainLauncher = true, Icon = "@drawable/icon")]    
    [Activity(Label = "CodeLayoutActivity", ConfigurationChanges = Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
    public class CodeLayoutActivity : Activity
    {
        TextView _tv;
        RelativeLayout.LayoutParams _layoutParamsPortrait;
        RelativeLayout.LayoutParams _layoutParamsLandscape;

        protected override void OnCreate(Bundle bundle)
        {

            //Create a layout
            var rltMain = new RelativeLayout(this);

            #region "Rotation Sample"

            //base.OnCreate(bundle);

           

            ////set Layout parameter
            //var lpmLayout = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.FillParent);
            //rltMain.LayoutParameters = lpmLayout;

            ////get the initial orientation
            //var otnSurface = WindowManager.DefaultDisplay.Rotation;

            ////create layout based upon orientation
            //RelativeLayout.LayoutParams lpmTv;

            //if (otnSurface == SurfaceOrientation.Rotation0 || otnSurface == SurfaceOrientation.Rotation180)
            //{
            //    lpmTv = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);
            //}
            //else
            //{
            //    lpmTv = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);
            //    lpmTv.LeftMargin = 200;
            //    lpmTv.TopMargin = 200;
            //}

            ////create TextView control
            //var tv = new TextView(this);
            //tv.LayoutParameters = lpmTv;
            //tv.Text = "Programmatic layout";

            ////add Textview to the layout
            //rltMain.AddView(tv);

            ////set the layout as the content view
            //SetContentView(rltMain);

            #endregion

            // create a layout
            // set layout parameters
            // get the initial orientation

            // create portrait and landscape layout for the TextView
            _layoutParamsPortrait = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);

            _layoutParamsLandscape = new RelativeLayout.LayoutParams(ViewGroup.LayoutParams.FillParent, ViewGroup.LayoutParams.WrapContent);
            _layoutParamsLandscape.LeftMargin = 100;
            _layoutParamsLandscape.TopMargin = 100;

            var surfaceOrientation = WindowManager.DefaultDisplay.Rotation;

            _tv = new TextView(this);

            if (surfaceOrientation == SurfaceOrientation.Rotation0 || surfaceOrientation == SurfaceOrientation.Rotation180)
            {
                _tv.LayoutParameters = _layoutParamsPortrait;
            }
            else
            {
                _tv.LayoutParameters = _layoutParamsLandscape;
            }

            _tv.Text = "Programmatic layout";
            rltMain.AddView(_tv);
            SetContentView(rltMain);

        }

        public override void OnConfigurationChanged(Android.Content.Res.Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);

            if (newConfig.Orientation == Android.Content.Res.Orientation.Portrait)
            {
                _tv.LayoutParameters = _layoutParamsPortrait;
                _tv.Text = "Changed to portrait";
            }
            else if (newConfig.Orientation == Android.Content.Res.Orientation.Landscape)
            {
                _tv.LayoutParameters = _layoutParamsLandscape;
                _tv.Text = "Changed to landscape";
            }
        }
    }
}