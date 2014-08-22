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
using System.Data;

namespace AndroidApplication1
{
    public class CSQLite : IDisposable
    {
        #region "Attributes"

        private string _datasource = "cylife.db3";
        private bool _isOpen;
        private bool _disposed = false;

        private SqliteConnection _connection;
        private Dictionary<string, string> _parameters;

        #endregion


        #region "Constructor"

        public CSQLite()
        {
            init("");
        }

        public CSQLite(string strDatasource)
        {
            init(strDatasource);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected void Dispose(bool blDisposing)
        {
            if (!this._disposed)
            {
                if (blDisposing)
                {
                    this._isOpen = false;
                    this._connection.Dispose();
                }

                this.close();

                _disposed = true;
            }
        }

        ~CSQLite()
        {
            Dispose(false);
        }
        

        #endregion

        #region "Functions"

        private void init(string strDatasource)
        {
            if (strDatasource != "")
                this._datasource = strDatasource;
            this._connection = new SqliteConnection("data source = " + this._datasource);
            this._parameters = new Dictionary<string, string>();
            this._isOpen = false;
        }

        private bool checkDbExist()
        {
            if (System.IO.File.Exists(_datasource))
                return true;
            else
                return false;
        }

        private void open()
        {
            if (!checkDbExist())
                throw new Exception("檔案不存在!!");

            if (!_isOpen)
                _connection.Open();

            this._isOpen = true;
            
        }

        private void close()
        {
            if (_isOpen)
                _connection.Close();

            this._isOpen = false;
        }

        #endregion

        #region "Methods"

        public void AddParameter(string strKey, string strValue)
        {
            _parameters.Add(strKey, strValue);
        }

        public void ExecuteNonQuery(string strSql)
        {
            this.open();

            using (SqliteTransaction stnQuery = _connection.BeginTransaction())
            {
                using (SqliteCommand scdCommand = new SqliteCommand(_connection))
                {
                    scdCommand.CommandText = strSql;

                    foreach (KeyValuePair<string, string> kvpCollection in this._parameters)
                    {
                        scdCommand.Parameters.Add(new SqliteParameter(kvpCollection.Key, kvpCollection.Value));
                    }

                    scdCommand.ExecuteNonQuery();
                }

                stnQuery.Commit();
            }

            this.close();
            this._parameters.Clear();
        }

        public DataTable ExecuteQuery(string strSql)
        {
            DataTable dtResult = new DataTable();

            this.open();

            try
            {
                using (SqliteCommand scdCommand = new SqliteCommand(_connection))
                {
                    SqliteDataAdapter sdpAdapter = new SqliteDataAdapter(scdCommand);

                    scdCommand.CommandText = strSql;

                    foreach (KeyValuePair<string, string> kvpCollection in this._parameters)
                    {
                        scdCommand.Parameters.Add(new SqliteParameter(kvpCollection.Key, kvpCollection.Value));
                    }

                    sdpAdapter.Fill(dtResult);
                }
            }
            catch (SqliteException e)
            {

                throw new Exception(e.Message);
            }
            finally
            {
                this.close();
                this._parameters.Clear();
            }

            return dtResult;
        }

        public DataRow ExecuteRow(string strSql)
        {
            DataRow drResult;

            this.open();

            try
            {
                using (SqliteCommand scdCommand = new SqliteCommand(this._connection))
                {
                    SqliteDataAdapter sdpAdapter = new SqliteDataAdapter(scdCommand);

                    scdCommand.CommandText = strSql;

                    foreach (KeyValuePair<string, string> kvpCollection in this._parameters)
                    {
                        scdCommand.Parameters.Add(new SqliteParameter(kvpCollection.Key, kvpCollection.Value));
                    }

                    DataTable dtResult = new DataTable();

                    sdpAdapter.Fill(dtResult);

                    if (dtResult.Rows.Count == 0)
                        drResult = null;
                    else
                        drResult = dtResult.Rows[0];
                }
            }
            catch (SqliteException e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                this.close();
                this._parameters.Clear();
            }

            return drResult;
        }

        public Object ExecuteScalar(string strSql)
        {
            Object objResult;

            this.open();

            using (SqliteCommand scdCommand = new SqliteCommand(this._connection))
            {
                scdCommand.CommandText = strSql;

                foreach (KeyValuePair<string, string> kvpCollection in this._parameters)
                {
                    scdCommand.Parameters.Add(new SqliteParameter(kvpCollection.Key, kvpCollection.Value));
                }

                objResult = scdCommand.ExecuteScalar();
            }

            this.close();
            this._parameters.Clear();

            return objResult;
        }

        public void ClearParameters()
        {
            this._parameters.Clear();
        }

        #endregion

    }
}