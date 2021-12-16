using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PreFinalProject
{
    static class UsersDAO
    {
        // all tasks in manipulating the Users database
        // will be in this Data Access Object (DAO) class
        // this includes adding, querying, editing, and deleting users
        public static async Task CreateTable()
        {
            await DBConn.db.CreateTableAsync<Users>();
        }
        public static async Task AddUser(string username, string password)
        {
            await DBConn.Init();
            var user = new Users
            {
                Username = username,
                Password = password
            };
            await DBConn.db.InsertAsync(user);
        }
        public static async Task<IEnumerable<Users>> GetUsers()
        {
            await DBConn.Init();
            var users = await DBConn.db.Table<Users>().ToListAsync();
            return users;
        }

        public static async Task<Users> Login(string username, string password)
        {
            await DBConn.Init();
            Users returnedUser;
            try
            {
                returnedUser = await DBConn.db.GetAsync<Users>(username);
                if (password != returnedUser.Password)
                {
                    returnedUser = null;
                }
            } catch (Exception)
            {
                returnedUser = null;
            }
            return returnedUser;
        }
    }
}
