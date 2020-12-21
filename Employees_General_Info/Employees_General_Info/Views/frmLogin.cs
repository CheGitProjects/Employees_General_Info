using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using Employees_General_Info.Models;


namespace Employees_General_Info.Views
{
    public partial class frmLogin : DevExpress.XtraEditors.XtraForm
    {
        public Users user;
        public static string sVersion;

        public frmLogin()
        {
            InitializeComponent();
            btnLogin.Click += new EventHandler(Login);
            txtUser.KeyDown += new KeyEventHandler(LoginEnter);
            txtPass.KeyDown += new KeyEventHandler(LoginEnter);
            sVersion = CurrentPublishedVersion();
        }

        private string CurrentPublishedVersion()
        {
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed)
            {
                return " V" + System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion;
            }
            else
            {
                return "";
            }
        }

        private void Login(object sender, EventArgs e)
        {
            Login();
        }

        private void LoginEnter(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!string.IsNullOrEmpty(txtUser.Text) && !string.IsNullOrEmpty(txtPass.Text))
                {
                    Login();
                }
            }
        }

        private void Login()
        {
            if (string.IsNullOrEmpty(txtUser.Text) || string.IsNullOrEmpty(txtPass.Text))
            {
                XtraMessageBox.Show("Please type your user name and password then click \"Login\"", "Login Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                DataTable dt = new DataTable();
                string sUser = txtUser.Text.Trim();
                string sPass = txtPass.Text.Trim();

                using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                {
                    string sCommand = "SELECT us.ID_User AS 'ID', em.Name + ' ' + em.P_LastName + ' ' + em.M_LastName AS 'EMPLOYEE', us.Username AS 'USERNAME', " +
                                        "us.Password AS 'PASSWORD', us.FirstTime AS 'FIRST TIME', us.Status AS 'STATUS', rl.Role AS 'ROLE' " +
                                        "FROM Users us " +
                                        "LEFT JOIN Employees em ON em.ID_Employee = us.ID_Employee " +
                                        "LEFT JOIN Employees_General_Info.dbo.Roles rl ON rl.ID_Role = us.ID_Right " +
                                        "WHERE Username = @sID_User";
                    using (SqlCommand cmd = new SqlCommand(sCommand, sqlConn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.Add("@sID_User", SqlDbType.NVarChar).Value = sUser;
                        try
                        {
                            sqlConn.Open();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                da.Fill(dt);
                            }
                        }
                        catch { }
                    }
                }
                if (dt.Rows.Count > 0)
                {
                    user = new Users()
                    {
                        ID_User = dt.Rows[0]["ID"].ToString(),
                        Employee = dt.Rows[0]["EMPLOYEE"].ToString(),
                        Username = dt.Rows[0]["USERNAME"].ToString(),
                        Password = dt.Rows[0]["PASSWORD"].ToString(),
                        FirstTime = Convert.ToBoolean(dt.Rows[0]["FIRST TIME"]),
                        Status = Convert.ToBoolean(dt.Rows[0]["STATUS"]),
                        Role = dt.Rows[0]["ROLE"].ToString()
                    };                    

                    if (Models.SecurePassword.Verify(sPass, user.Password))
                    {
                        string sCommand = "SELECT rl.Role AS 'ROLE', rl.FullControl AS 'FULLCONTROL', rl.[Read] AS 'READ', rl.Write AS 'WRITE' " +
                                            "FROM Users eu " +
                                            "LEFT JOIN Employees_General_Info.dbo.Roles rl ON rl.ID_Role = eu.ID_Right " +
                                            $"WHERE eu.ID_Employee = '{user.ID_User}'";
                        DataTable dtUsers = SQL.Read(sCommand);
                        if (dtUsers.Rows.Count > 0)
                        {
                            user.Role = dtUsers.Rows[0]["ROLE"].ToString();
                            user.FullControl = Convert.ToBoolean(dtUsers.Rows[0]["FULLCONTROL"]);
                            user.Read = Convert.ToBoolean(dtUsers.Rows[0]["READ"]);
                            user.Write = Convert.ToBoolean(dtUsers.Rows[0]["WRITE"]);
                            MainViewModel.GetInstance().login = this;
                            Hide();
                            MainViewModel.GetInstance().main = new frmMain();
                            MainViewModel.GetInstance().main.Show();
                        }
                        else
                        {
                            XtraMessageBox.Show("User not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                    else
                    {
                        XtraMessageBox.Show("The password is incorrect, please try again.", "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        txtPass.Focus();
                        txtPass.SelectAll();
                    }                    
                }
                else
                {
                    XtraMessageBox.Show("The user is incorrect, please try again.", "Login error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
            Text = $"Login {sVersion}";
            txtUser.Focus();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}