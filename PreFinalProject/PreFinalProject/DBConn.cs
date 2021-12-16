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
        // Singleton design pattern for the Database connection object
        public static SQLiteAsyncConnection db;

        public static async Task Init()
        {
            if (db != null)
                return; // return the current instance of the database connection
            var connectionString = Path.Combine(FileSystem.AppDataDirectory, "MyDatabase");
            // if no connection is found, create a connection using the path above
            db = new SQLiteAsyncConnection(connectionString);
            await UsersDAO.CreateTable();
        }
    }
}
