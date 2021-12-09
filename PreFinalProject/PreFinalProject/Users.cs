using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace PreFinalProject
{
    class Users
    {
        [PrimaryKey]
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
