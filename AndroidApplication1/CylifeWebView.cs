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
using Android.Webkit;

namespace AndroidApplication1
{
    [Activity(Label = "CylifeWebView")]
    public class CylifeWebView : Activity
    {
        WebView wvCylife;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here

            SetContentView(Resource.Layout.WebView);

            wvCylife = FindViewById<WebView>(Resource.Id.wvCylife);

            wvCylife.Settings.JavaScriptEnabled = true;

            wvCylife.LoadUrl("http://www.cylife.com.tw");

            wvCylife.SetWebViewClient(new WebViewClient());
        }

        private class CustWebViewClient : WebViewClient
        {
            public override bool ShouldOverrideUrlLoading(WebView view, string url)
            {
                view.LoadUrl(url);
                return true;
            }
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (keyCode == Keycode.Back && wvCylife.CanGoBack())
            {
                wvCylife.GoBack();
                return true;
            }
            return base.OnKeyDown(keyCode, e);
        }
    }

}