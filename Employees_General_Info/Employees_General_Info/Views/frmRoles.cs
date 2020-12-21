using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Employees_General_Info.Models;
using DevExpress.XtraGrid.Views.Grid;

namespace Employees_General_Info.Views
{
    public partial class frmRoles : DevExpress.XtraEditors.XtraForm
    {
        private string sID;
        public frmRoles()
        {
            InitializeComponent();
            btnSave.Click += new EventHandler(Save);
            btnDelete.Click += new EventHandler(DeleteRole);
            btnClear.Click += new EventHandler(Clear);
            gvRoles.DoubleClick += new EventHandler(GV_DoubleClick);
        }

        private void Clear()
        {
            sID = null;
            txtRole.EditValue = null;
            rbFullControl.Checked = false;            
            rbRead.Checked = false;
            rbWrite.Checked = false;
        }

        private void Clear(object sender, EventArgs e)
        {
            Clear();
        }

        private void LoadRoles()
        {
            string sCommand = "SELECT ID_Role AS 'ID', Role AS 'ROLE', FullControl AS 'FULLCONTROL', [Read] AS 'READ', Write AS 'WRITE' " +
                                "FROM Employees_General_Info.dbo.Roles ORDER BY Role";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                gcRoles.DataSource = dt;
                gvRoles.Columns["ID"].Visible = false;
                gvRoles.BestFitColumns();
            }
        }

        private void GV_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            try
            {
                sID = view.GetRowCellValue(view.FocusedRowHandle, "ID").ToString();
                txtRole.Text = view.GetRowCellValue(view.FocusedRowHandle, "ROLE").ToString();
                rbFullControl.Checked = Convert.ToBoolean(view.GetRowCellValue(view.FocusedRowHandle, "FULLCONTROL"));                
                rbRead.Checked = Convert.ToBoolean(view.GetRowCellValue(view.FocusedRowHandle, "READ"));
                rbWrite.Checked = Convert.ToBoolean(view.GetRowCellValue(view.FocusedRowHandle, "WRITE"));
            }
            catch { }

        }

        private void Save(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRole.Text))
            {
                using (SqlConnection sqlConn = new SqlConnection(Constants.cn.Replace(@"\\", @"\")))
                {
                    using (SqlCommand cmd = new SqlCommand("Employees_General_Info.dbo.SP_UpdateRoles", sqlConn))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@sID_Role", SqlDbType.NVarChar).Value = !string.IsNullOrEmpty(sID) ? sID : (object)DBNull.Value;
                        cmd.Parameters.Add("@sRole", SqlDbType.NVarChar).Value = txtRole.Text.Trim();
                        cmd.Parameters.Add("@bFullControl", SqlDbType.Bit).Value = rbFullControl.Checked;                        
                        cmd.Parameters.Add("@bRead", SqlDbType.Bit).Value = rbRead.Checked;
                        cmd.Parameters.Add("@bWrite", SqlDbType.Bit).Value = rbWrite.Checked;
                        cmd.Parameters.Add("@bDelete", SqlDbType.Bit).Value = false;

                        try
                        {
                            sqlConn.Open();
                            cmd.ExecuteNonQuery();
                            XtraMessageBox.Show($"Role \"{txtRole.Text}\" has been saved", "Save", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            Clear();
                            LoadRoles();
                        }
                        catch (Exception ex)
                        {
                            XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            else
            {
                XtraMessageBox.Show("The Role field is missing, please verify.", "Data missing", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void DeleteRole(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(sID))
            {
                if (XtraMessageBox.Show($"You are about to delete {txtRole.Text} role.\nDo you want to continue?", "Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn.Replace(@"\\", @"\")))
                    {
                        using (SqlCommand cmd = new SqlCommand("Employees_General_Info.dbo.SP_UpdateRoles", sqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sID_Role", SqlDbType.NVarChar).Value = sID;
                            cmd.Parameters.Add("@bDelete", SqlDbType.Bit).Value = true;
                            try
                            {
                                sqlConn.Open();
                                cmd.ExecuteNonQuery();
                                XtraMessageBox.Show($"The {txtRole.Text} role has been successfully deleted.", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Clear();
                                LoadRoles();                                
                            }
                            catch(Exception ex)
                            {
                                XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void frmRoles_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                MainViewModel.GetInstance().main.FillRolesCombo();
            }
            catch { }
        }
                               
        private void frmRoles_Load(object sender, EventArgs e)
        {
            LoadRoles();
        }
    }
}