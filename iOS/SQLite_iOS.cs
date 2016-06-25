using ALFCconnect.iOS;
using ALFCConnect;
using SQLite.Net;
using SQLite.Net.Platform.XamarinIOS;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_iOS))]

namespace ALFCconnect.iOS
{
    public class SQLite_iOS : ISQLite
    {
        public SQLite_iOS()
        {
        }

        #region ISQLite implementation
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "ALFCSQLite.db3";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData); // Documents folder
            string libraryPath = Path.Combine(documentsPath, "..", "Library"); // Library folder
            var path = Path.Combine(libraryPath, sqliteFilename);

            // This is where we copy in the prepopulated database
            Console.WriteLine(path);
            if (!File.Exists(path))
            {
                File.Copy(sqliteFilename, path);
            }

            var conn = new SQLiteConnection( new SQLitePlatformIOS(), path);

            // Return the database connection 
            return conn;
        }
        #endregion
    }
}
