using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using Employees_General_Info.Models;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace Employees_General_Info.Models
{
    public static class SQL
    {        
        public static DataTable Read(string sQuery)
        {
            DataTable dt = new DataTable();
            try
            {                
                using (SqlConnection sqlConn = new SqlConnection(Constants.cn.Replace(@"\\", @"\")))
                {
                    using (SqlCommand cmd = new SqlCommand(sQuery, sqlConn))
                    {
                        cmd.CommandType = CommandType.Text;                        
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return dt;
        }
    }
}
