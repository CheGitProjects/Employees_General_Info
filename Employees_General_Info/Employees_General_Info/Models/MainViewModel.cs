using Employees_General_Info.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Employees_General_Info.Models
{
    public class MainViewModel
    {
        public frmLogin login { get; set; }
        public frmMain main { get; set; }
        public frmShow show { get; set; }

        private static MainViewModel instance;
        public MainViewModel()
        {
            instance = this;
        }

        public static MainViewModel GetInstance()
        {
            if (instance == null)
            {
                return new MainViewModel();
            }
            return instance;
        }
    }
}
