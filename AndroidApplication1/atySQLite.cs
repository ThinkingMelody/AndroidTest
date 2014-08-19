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
using System.Data;
using Mono.Data.Sqlite;

namespace AndroidApplication1
{
    [Activity(Label = "atySQLite")]
    public class atySQLite : Activity
    {
        string connectionString;
        SqliteConnection conn;
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            // Create your application here
            SetContentView(Resource.Layout.ltSQLite);

            var sqlLiteFilePath = GetFileStreamPath("") + "/db_user.db";

            var btnCreate = FindViewById<Button>(Resource.Id.btnCreateDb);
            var btnTable = FindViewById<Button>(Resource.Id.btnCreateTable);
            var btnInsert = FindViewById<Button>(Resource.Id.btnInsertData);
            var btnSearch = FindViewById<Button>(Resource.Id.btnSearchData);

            //connectionString = String.Format("Data Source ={0};Version=3;", sqlLiteFilePath);
            //conn = new SqliteConnection(connectionString);

            btnCreate.Click += (object sender, EventArgs e) =>
                {
                    SqliteConnection.CreateFile(sqlLiteFilePath);
                };

            btnTable.Click += delegate
                {
                    try
                    {
                        if (!System.IO.File.Exists(sqlLiteFilePath))
                        {
                            var connectionString = String.Format("Data Source ={0};Version=3;", sqlLiteFilePath);
                            var conn = new SqliteConnection(connectionString);

                            var cmd =
                                new SqliteCommand(
                                    "CREATE TABLE Users (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name ntext, Addr ntext)", conn)
                                {
                                    CommandType = CommandType.Text
                                };

                            conn.Open();
                            cmd.ExecuteNonQuery();
                            conn.Close();
                            Toast.MakeText(this, "Table �إߧ���", ToastLength.Short).Show();
                        }
                    }
                    catch (Exception ex)
                    {
                        Toast.MakeText(this, "�إߥ���:" + ex.Message, ToastLength.Short).Show();
                    }
                };

            btnInsert.Click += delegate
            {

                try
                {
                    var connectionString = String.Format("Data Source={0};Version=3;", sqlLiteFilePath);
                    var conn = new SqliteConnection(connectionString);
                    
                    conn.Open();
                    var cmd = new SqliteCommand(conn) {
                        CommandText = "INSERT INTO Users (\"Name\", \"Addr\") VALUES(\"�©���\", \"�x�W�x��\");",
                        CommandType = CommandType.Text
                    };
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    Toast.MakeText(this, "��Ƽg�J����", ToastLength.Short).Show();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "��Ƽg�J���ѡG" + ex.Message, ToastLength.Short).Show();
                }

            };

            btnSearch.Click += delegate
            {
                try
                {
                    //���M��LinearLayout�����Ҧ���� �A�A���s�פJ
                    var dataContainer = FindViewById<LinearLayout>(Resource.Id.lltSearch);
                    dataContainer.RemoveAllViews();

                    var connectionString = String.Format("Data Source={0};Version=3;", sqlLiteFilePath);
                    var conn = new SqliteConnection(connectionString);
                    conn.Open();
                    var cmd = new SqliteCommand(conn)
                    {
                        CommandText = "SELECT * FROM Users",
                        CommandType = CommandType.Text
                    };
                    var dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        var txtView = new TextView(this);
                        txtView.Text = dr["ID"] + "," + dr["Name"] + "," + dr["Addr"];
                        dataContainer.AddView(txtView);
                    }
                    conn.Close();
                }
                catch (Exception ex)
                {
                    Toast.MakeText(this, "��Ƭd�ߥ���:" + ex.Message, ToastLength.Short).Show();
                }
            };
        }
    }
}