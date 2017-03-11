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

[assembly: Dependency(typeof(SQLite_Android))]
namespace MyRecordVault.Droid.Services
{
    public class SQLite_Android : ISQLite
    {

        public SQLiteConnection GetConnection()
        {
            const string sqliteFilename = "TodoSQLite.db3";
            var documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var path = Path.Combine(documentsPath, sqliteFilename);
            var plat = new SQLitePlatformAndroid();
            var conn = new SQLiteConnection(plat, path);
            return conn;
        }

    }
}