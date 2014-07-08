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
using System.Net;
using System.Json;

namespace AndroidApplication1
{
    [Activity(Label = "NonConfigInstanceActivity")]
    public class NonConfigInstanceActivity : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.LayoutAskQuestion);

            var btnBlackWidow = FindViewById<Button>(Resource.Id.btnBlackWidow);
            btnBlackWidow.Click += delegate
            {
                //�}�Ҥ@�ӷs��intent
                var intent = new Intent(this, typeof(MainActivity));
                //��J�@��key ��hero �Ȭ� �¹��
                intent.PutExtra("hero", "�¹��");
                //���A�]��OK
                SetResult(Result.Ok, intent);
                //�I�s��N����������
                Finish();
            };

            var btnIronMan = FindViewById<Button>(Resource.Id.btnIronMan);
            btnIronMan.Click += delegate(object sender, EventArgs e)
            {
                var intent = new Intent(this, typeof(MainActivity));
                intent.PutExtra("hero", "���K�H");
                SetResult(Result.Ok, intent);
                Finish();
            };
        }

        //public void SearchTwitter(string text)
        //{
        //    //string searchUrl = String.Format("http://search.twitter.com/search.json?" + "q={0}&rpp=10&include_entities=false&" + "result_type=mixed", text);

        //    string searchUrl = @"http://ajax.googleapis.com/ajax/services/search/web?v=1.0&q=Dog&callback=myFunc";

        //    var httpReq = (HttpWebRequest)HttpWebRequest.Create(new Uri(searchUrl));

        //    IWebProxy myProxy = new WebProxy("proxy.cylife.com.tw", 8080);
        //    NetworkCredential myNetworkCredential = new NetworkCredential("thinker", "2xoiglxA", "CYLIFE");
        //    httpReq.Proxy = myProxy;
        //    httpReq.Proxy.Credentials = myNetworkCredential;
                                   
        //    httpReq.BeginGetResponse(new AsyncCallback(ResponseCallback), httpReq);
        //}

        //void ResponseCallback(IAsyncResult ar)
        //{
        //    var httpReq = (HttpWebRequest)ar.AsyncState;

        //    using (var httpRes = (HttpWebResponse)httpReq.EndGetResponse(ar))
        //    {
        //        ParseResults(httpRes);
        //    }
        //}

        //void ParseResults(HttpWebResponse httpRes)
        //{
        //    var s = httpRes.GetResponseStream();
        //    var j = (JsonObject)JsonObject.Load(s);

        //    var results = (from result in (JsonArray)j["results"] let jResult = result as JsonObject select jResult["text"].ToString()).ToArray();

        //    RunOnUiThread(() =>
        //    {
        //        PopulateTweetList(results);
        //    });
        //}

        //void PopulateTweetList(string[] results)
        //{
        //    //ListAdapter = new ArrayAdapter<string>(this, Resource.Layout.ItemView, results);
        //    ListAdapter = new ArrayAdapter<string>(this, Android.Resource.Layout.SimpleListItem1, results);
        //}
    }
}