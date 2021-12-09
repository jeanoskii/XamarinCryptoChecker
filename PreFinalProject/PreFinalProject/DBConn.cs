using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Essentials;

namespace PreFinalProject
{
    static class DBConn
    {
        public static SQLiteAsyncConnection db;

        public static async Task Init()
        {
            if (db != null)
                return;
            var connectionString = Path.Combine(FileSystem.AppDataDirectory, "MyDatabase");
            db = new SQLiteAsyncConnection(connectionString);
            await UsersDAO.CreateTable();
        }
    }
}
