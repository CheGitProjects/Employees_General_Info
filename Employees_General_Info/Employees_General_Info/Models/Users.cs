using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Employees_General_Info.Models
{
    public class Users
    {
        string id_User;
        string employee;
        string username;
        string password;
        bool firstTime;
        bool status;
        string right;

        public string ID_User
        {
            get { return id_User; }
            set { id_User = value; }
        }

        public string Employee
        {
            get { return employee; }
            set { employee = value; }
        }

        public string Username
        {
            get { return username; }
            set { username = value; }
        }

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        public bool FirstTime
        {
            get { return firstTime; }
            set { firstTime = value; }
        }

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }

        public string Right
        {
            get { return right; }
            set { right = value; }
        }
    }
}
