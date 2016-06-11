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
using SQLite.Net;
using System.IO;
using Xamarin.Forms;
using SQLite.Net.Interop;
using SQLite.Net.Platform.XamarinAndroid;
using ALFCConnect.Droid;

[assembly: Dependency (typeof (SQLite_Android))]
namespace ALFCConnect.Droid
{
    public class SQLite_Android : ISQLite
    {

        public SQLite_Android()
        {
        }
        public SQLiteConnection GetConnection()
        {

            var sqliteFilename = "AlfcDbSQLite.db3";
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);

            // This is where we copy in the prepopulated database
            //Console.WriteLine(path);
            if (!File.Exists(path))
            {
                //var s = new FileStream( //Forms.Context.Resources.OpenRawResource();  // RESOURCE NAME ###

                // create a write stream
                FileStream writeStream = new FileStream(path, FileMode.CreateNew, FileAccess.Write);
                // write to the stream
                //ReadWriteStream(s, writeStream);
            }

             
            var conn = new SQLiteConnection(new SQLitePlatformAndroid(), path);

            // Return the database connection 
            return conn;
        }

        /// <summary>
		/// helper method to get the database out of /raw/ and into the user filesystem
		/// </summary>
		void ReadWriteStream(Stream readStream, Stream writeStream)
        {
            int Length = 256;
            Byte[] buffer = new Byte[Length];
            int bytesRead = readStream.Read(buffer, 0, Length);
            // write the required bytes
            while (bytesRead > 0)
            {
                writeStream.Write(buffer, 0, bytesRead);
                bytesRead = readStream.Read(buffer, 0, Length);
            }
            readStream.Close();
            writeStream.Close();
        }
    }
}