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
using Mono.Data.Sqlite;

namespace AndroidApplication1
{
    class CSQLite
    {

        #region "Attributes

        private string _dataBase = @"db/data.db3";
        private bool _isOpen;
        private bool _disposed = false;

        private SqliteConnection _connection;
        private Dictionary<string, string> _parameters;

        #endregion

        #region "Constructor"

        public CSQLite()
        {
            
        }


        public void Dispose()
        {

        }

        #endregion

        ~CSQLite()
        {
            
        }

    }
}