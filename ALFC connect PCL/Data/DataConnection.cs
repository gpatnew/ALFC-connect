using ALFCConnect;
using SQLite.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ALFCConnect.Data
{
    public class DataConnection
    {

        public SQLiteConnection DataBase { get; private set; }
        private volatile static DataConnection DBConnection;
        private static object lockingObject = new object();

        public static DataConnection Instance()
        {
           
            if(DBConnection == null)
            {
                lock(lockingObject)
                {
                    DBConnection = new DataConnection();
                }
            }

            return DBConnection;
        }
        private DataConnection()
        {
           
                DataBase = DependencyService.Get<ISQLite>().GetConnection();
        }
    }
}
