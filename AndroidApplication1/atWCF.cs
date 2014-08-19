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
using System.Data;
using AndroidApplication1.PersonnelReference;
using Newtonsoft.Json;
using System.Xml;
using System.IO;
using Newtonsoft.Json.Linq;

namespace AndroidApplication1
{
    public class DocumentInfo
    {
        public string ODC_DATE { get; set; }
        public string ODC_NO { get; set; }
        public string ODC_SHORT { get; set; }
        public string ODC_FILE { get; set; }

        public DocumentInfo()
        {
            ODC_DATE = "";
            ODC_NO = "";
            ODC_SHORT = "";
            ODC_FILE = "";
        }
    }

    public class RootLibrary
    {
        // Value name (Library) is Json root "Name".
        public List<DocumentInfo> Library = new List<DocumentInfo>();
    }

    [Activity(Label = "atWCF", MainLauncher = true)]
    public class atWCF : Activity
    {
        protected override void OnCreate(Bundle bundle)
        {
            string strResult;
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.ltWCF);
            
            
            //DataTable dtPerson = new DataTable();

            var MyCylife = new CylifeReference.AppServices();

            //TextView tvAccountInfo = FindViewById<TextView>(Resource.Id.textView2);
            
            //tvAccountInfo.Text = MyCylife.getAccountInfo("B121226579", "0");

            //JsonTextReader reader = new JsonTextReader(new StringReader(tvAccountInfo.Text));
            //while (reader.Read())
            //{
            //    if (reader.Value != null)
            //        Console.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
            //    else
            //        Console.WriteLine("Token: {0}", reader.TokenType);
            //}

            var MyWCF = new PersonnelReference.PersonnelServices();
            //string ServerName = MyWCF.ServerName();
            //strResult = MyWCF.getDineList("B121226579", "10305");
            //if (!String.IsNullOrEmpty(strResult))
            //{
            //    if (strResult != "[]")
            //    {
            //        //DataTable dt = JsonConvert.DeserializeObject<DataTable>(MyWCF.getDineList("B121226579", "10305"));
            //        DataTable dt = JsonConvert.DeserializeObject<DataTable>(strResult);
            //    }
            //}

            //string strSearchDine = MyWCF.SearchDine("B121226579", "10305");
            //DataTable dtSearch = new DataTable("EedList");
            //dtSearch = (DataTable)JsonConvert.DeserializeObject(strSearchDine.Trim(), dtSearch.GetType());

            string OfficialDocuments = MyWCF.SearchOfficialDocuments("B121226579");
            List<JObject> _documents = JsonConvert.DeserializeObject<List<JObject>>(OfficialDocuments);

            var binfo = from b in _documents
                        select b;
            string strInfo = "";
            foreach (var b in binfo)
            {
                strInfo += "ODC_NO:" + b["ODC_NO"];
                strInfo += "ODC_DATE:" + b["ODC_DATE"];
                strInfo += "ODC_SHORT:" + b["ODC_SHORT"];
                strInfo += "ODC_FILE:" + b["ODC_FILE"];
                strInfo += "-------------------------";
            }
            Toast.MakeText(this, strInfo, ToastLength.Long).Show();
            //JObject jObj = JObject.Parse(OfficialDocuments);
            //JToken jtNo = jObj["ODC_NO"];

            //RootLibrary MyDocument = JsonConvert.DeserializeObject<RootLibrary>(OfficialDocuments);
            //DocumentInfo MyDocumnt = JsonConvert.DeserializeObject<DocumentInfo>(OfficialDocuments);
            //Dictionary<string, DocumentInfo> values = JsonConvert.DeserializeObject<Dictionary<string, DocumentInfo>>(OfficialDocuments);
            //foreach (var value in values)
            //{
            //    Toast.MakeText(this, value.Key + value.Value, ToastLength.Long).Show();
            //}
            //JsonSerializer MySerializer = new JsonSerializer();
            //JsonReader Myreader = new JsonReader();
            //DocumentInfo MyDocument = (DocumentInfo)MySerializer.Deserialize(Myreader(OfficialDocuments), typeof(DocumentInfo));

            //foreach (DocumentInfo _document in MyDocument.Library)
            //{
            //    string Result = _document.ODC_NO + "-" + _document.ODC_FILE;
            //}

            DataTable dtOfficial = new DataTable();
            dtOfficial = (DataTable)JsonConvert.DeserializeObject(OfficialDocuments, dtOfficial.GetType());

            //DataTable dtSearch = JsonConvert.DeserializeObject<DataTable>(strSearchDine.Trim());
            //DataTable dtSearch = XmlStringToDataTable(strSearchDine);

            //ArrayOfKeyValueOfstringstringKeyValueOfstringstring[] array =
            //      MyWCF.getDineList("B121226579", "10305").Select(pair =>
            //         new ArrayOfKeyValueOfstringstringKeyValueOfstringstring()
            //         {
            //             Key = pair.Key,
            //             Value = pair.Value
            //         }).ToArray();

            
            //var MyWCF = new CylifeReference.AppServices();
            //CylifeReference.MyDefineType MyDataType = new CylifeReference.MyDefineType();
            //MyDataType = MyWCF.getAccountInfo("B121226579", "0");
            ////var AccountInfo = MyWCF.getAccountInfo("B121226579", "0");
            //strResult = MyDataType.MyHashtable[0].ToString();
            strResult = "Success!!";
            //bool blResult, blResultSpecified = false;
            
            //MyWCF.Login("B121226579", "2dfkielS",out blResult, out blResultSpecified);

            //if (blResult)
            //{
            //    strResult = "success!!";
            //}
            //else
            //{
            //    strResult = "Error Login!!";

            //}
            


            Toast.MakeText(this, strResult, ToastLength.Long).Show();

        }



        //public DataTable XmlStringToDataTable(string Xmlstring)
        //{

        //    //新建XML文件類別
        //    XmlDocument Xmldoc = new XmlDocument();
        //    //從指定的字串載入XML文件
        //    Xmldoc.LoadXml(Xmlstring);
        //    //建立此物件，並輸入透過StringReader讀取Xmldoc中的Xmldoc字串輸出
        //    XmlReader Xmlreader = XmlReader.Create(new System.IO.StringReader(Xmldoc.OuterXml));
        //    //建立DataSet
        //    DataSet ds = new DataSet();
        //    //透過DataSet的ReadXml方法來讀取Xmlreader資料
        //    ds.ReadXml(Xmlreader);
        //    //建立DataTable並將DataSet中的第0個Table資料給DataTable
        //    DataTable dt = ds.Tables[0];
        //    //回傳DataTable
        //    return dt;
        //}
    }

    
}