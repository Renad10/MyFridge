using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.WebRequestMethods;

namespace MyFridge
{
    public class Constants
    {
        // api
        public const string UPC_LOOKUP = "https://api.upcitemdb.com/prod/trial/lookup?upc={0}";
        // database
        private const string DBFileName = "MyFridge.db3";
        public const SQLiteOpenFlags flags =
            SQLiteOpenFlags.ReadWrite |
            SQLiteOpenFlags.Create |
            SQLiteOpenFlags.SharedCache;

        public static string DatabasePath
        {
            get
            {
                return Path.Combine(FileSystem.AppDataDirectory, DBFileName);
            }
        }
    }
}
