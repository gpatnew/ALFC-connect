using SQLite.Net;
using Xamarin.Forms;

namespace ALFCconnect.Data
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
