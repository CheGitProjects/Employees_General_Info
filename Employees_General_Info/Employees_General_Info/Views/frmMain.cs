using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Employees_General_Info.Models;
using Employees_General_Info.Views;
using System.Security.Policy;
using System.IO;
using DevExpress.Utils.Extensions;
using Employees_General_Info.Properties;

namespace Employees_General_Info
{
    public partial class frmMain : DevExpress.XtraEditors.XtraForm
    {
        private string sFileName;
        private Image img;        

        public string sEmployeeID;
        public string sPayroll;
        public string sNo_Emp;
        public string sName;
        public string sP_LastName;
        public string sM_LastName;
        public string sEmail;
        public string sEmail2;
        public string sEmail3;
        public string sPhone;
        public string sCellPhone;
        public string sAD;
        public string sPic;
        public decimal nPayroll_Rate;
        public bool bStatus;

        public string sPosID;
        public string sPosition;

        public string sDepID;
        public string sDepartment;

        public string sBusID;
        public string sBusiness;
        public string sDescription;

        public string sLocID;
        public string sCountryID;
        public string sStateID;
        public string sLocation;
        public string sCountry;
        public string sState;

        public string sPayrollID;        
        public string sPayExchange;

        public string sUserID;
        public string sUser;
        public string sPassword;
        public bool bFirstTime;
        public bool bUserStatus;

        public string sRightID;
        public string sRight;
        public bool bEditing;
        public bool bModify;
        public bool bReadOnly;

        //public static frmMain Main;

        public frmMain()
        {
            InitializeComponent();
            //Main = this;
            picEmployee.AllowDrop = true;
            picEmployee.DragEnter += new DragEventHandler(EnterImage);
            picEmployee.DragDrop += new DragEventHandler(DropImage);

            //  Employee Controls
            btnEmpSave.Click += new EventHandler(SaveEmployee);
            btnEmpClear.Click += new EventHandler(ClearEmployees);
            btnEmpShow.Click += new EventHandler(ShowEmployees);
            

            //  Position Controls
            btnPosSave.Click += new EventHandler(SavePosition);
            btnPosClear.Click += new EventHandler(ClearPosition);
            btnPosShow.Click += new EventHandler(ShowPositions);

            //  Department Controls
            btnDepSave.Click += new EventHandler(SaveDepartments);
            btnDepClear.Click += new EventHandler(ClearDepartment);
            btnDepShow.Click += new EventHandler(ShowDepartments);

            //  Business Controls
            btnBusSave.Click += new EventHandler(SaveBusiness);
            btnBusClear.Click += new EventHandler(ClearBusiness);
            btnBusShow.Click += new EventHandler(ShowBusiness);

            //  Location Controls
            btnLocSave.Click += new EventHandler(SaveLocation);
            btnLocClear.Click += new EventHandler(ClearLocations);
            btnLocShow.Click += new EventHandler(ShowLocations);

            //  Payroll Controls
            btnPaySave.Click += new EventHandler(SavePayroll);
            btnPayClear.Click += new EventHandler(ClearPayroll);
            btnPayShow.Click += new EventHandler(ShowPayroll);

            //  User Controls
            btnUserSave.Click += new EventHandler(SaveUser);
            btnUserClear.Click += new EventHandler(ClearUser);
            btnUserShow.Click += new EventHandler(ShowUser);

            //  Rights Controls
            btnRightsSave.Click += new EventHandler(SaveRights);
            btnRightsClear.Click += new EventHandler(ClearRights);
            btnRightsShow.Click += new EventHandler(ShowRights);
        }

        #region Employee
        private void EnterImage(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void DropImage(object sender, DragEventArgs e)
        {
            PictureBox pic = (PictureBox)sender;
            try
            {
                pic.SizeMode = PictureBoxSizeMode.Zoom;
                var data = e.Data.GetData(DataFormats.FileDrop);
                if (data != null)
                {
                    string[] fileNames = data as string[];
                    if (fileNames.Length > 0)
                    {
                        sFileName = fileNames[0];                        
                        img = Image.FromFile(fileNames[0]);                        
                        pic.Image.Dispose();
                        pic.Image = img;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveEmployee(object sender, EventArgs e)
        {
            if (NoEmptyValidation())
            {
                bool Exists = false;
                if (string.IsNullOrEmpty(sEmployeeID))
                {
                    string sCommand = "SELECT No_EMP, Name, P_LastName, M_LastName FROM Employees WHERE No_EMP = @sNoEmp AND P_LastName = @sPat AND M_LastName = @sMat";
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand(sCommand, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@sNoEmp", SqlDbType.NVarChar).Value = teEmpNumber.Text.Trim();
                            cmd.Parameters.Add("@sName", SqlDbType.NVarChar).Value = teName.Text.Trim();
                            cmd.Parameters.Add("@sPat", SqlDbType.NVarChar).Value = tePatSurname.Text.Trim();
                            cmd.Parameters.Add("@sMat", SqlDbType.NVarChar).Value = teMatSurname.Text.Trim();

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    Exists = true;
                                }
                            }
                        }
                    }
                }

                if (Exists)
                {
                    XtraMessageBox.Show("This registry already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    decimal nPayroll = 0;
                    if (!string.IsNullOrEmpty(tePayroll_Rate.Text))
                    {
                        nPayroll = Convert.ToDecimal(tePayroll_Rate.Text);
                    }
                    sNo_Emp = teEmpNumber.Text.Trim();
                    sName = teName.Text.Trim();
                    sP_LastName = tePatSurname.Text.Trim();
                    sM_LastName = teMatSurname.Text.Trim();
                    SavePic();
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateEmployees", sqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sID_Employee", SqlDbType.NVarChar).Value = sEmployeeID;
                            cmd.Parameters.Add("@sID_Position", SqlDbType.NVarChar).Value = cmbPosition.SelectedValue;
                            cmd.Parameters.Add("@sID_Payroll", SqlDbType.NVarChar).Value = cmbPayroll.SelectedValue;
                            cmd.Parameters.Add("@sNo_Emp", SqlDbType.NVarChar).Value = sNo_Emp;
                            cmd.Parameters.Add("@sName", SqlDbType.NVarChar).Value = sName;
                            cmd.Parameters.Add("@sP_LastName", SqlDbType.NVarChar).Value = sP_LastName;
                            cmd.Parameters.Add("@sM_LastName", SqlDbType.NVarChar).Value = sM_LastName;
                            cmd.Parameters.Add("@sEmail", SqlDbType.NVarChar).Value = teEmail.Text.Trim();
                            cmd.Parameters.Add("@sEmail2", SqlDbType.NVarChar).Value = teEmail2.Text.Trim();
                            cmd.Parameters.Add("@sEmail3", SqlDbType.NVarChar).Value = teEmail3.Text.Trim();
                            cmd.Parameters.Add("@sPhone", SqlDbType.NVarChar).Value = tePhone.Text.Trim();
                            cmd.Parameters.Add("@sCellphone", SqlDbType.NVarChar).Value = teCellPhone.Text.Trim();
                            cmd.Parameters.Add("@sAD", SqlDbType.NVarChar).Value = teAD.Text.Trim(); ;
                            cmd.Parameters.Add("@sPic", SqlDbType.NVarChar).Value = sPic;
                            cmd.Parameters.Add("@nPayroll", SqlDbType.Decimal).Value = nPayroll;
                            cmd.Parameters.Add("@bStatus", SqlDbType.Bit).Value = chkStatus.Checked;

                            try
                            {
                                sqlConn.Open();
                                int nSuccess = cmd.ExecuteNonQuery();
                                if (nSuccess != 0)
                                {
                                    XtraMessageBox.Show("Data has been successfully registered!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearEmployees();
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void SavePic()
        {
            if (!string.IsNullOrEmpty(sFileName))
            {
                if (!Directory.Exists(Constants.sPath))
                {
                    Directory.CreateDirectory(Constants.sPath);
                }            
                string sExtension = Path.GetExtension(sFileName);
                string sFile = $"{sNo_Emp}_{sName}_{sP_LastName}_{sM_LastName}{sExtension}";
                string sTargetFile = string.Concat(Constants.sPath, sFile);
                picEmployee.Image.Dispose();
                picEmployee.Image = null;
                try
                {
                    File.Copy(sFileName, sTargetFile, true);
                    sPic = sFile;
                }
                catch { }
            }
            else
            {
                sPic = "";
            }
        }

        private void ShowEmployees(object sender, EventArgs e)
        {
            string sCommand = "SELECT em.ID_Employee AS 'ID', em.No_EMP AS 'EMPLOYEE NUMBER', em.Name AS 'NAME', em.P_LastName AS 'PATERNAL SURNAME', " +
                                "em.M_LastName AS 'MATERNAL SURNAME', jp.Position + ' - ' + dp.Department AS 'POSITION', em.Email AS 'EMAIL 1', em.Email2 AS 'EMAIL 2', em.Email3 AS 'EMAIL 3', em.Phone AS 'PHONE', " +
                                "em.Cellphone AS 'CELL PHONE', em.AD AS 'ACTIVE DIRECTORY', em.Pic AS 'PIC', em.PayRoll_Rate AS 'PAYROLL RATE', em.Status AS 'ACTIVE' " +
                                "FROM Employees em " +
                                "LEFT JOIN Job_Positions jp ON jp.ID_Position = em.ID_Position " +
                                "LEFT JOIN Department dp ON dp.ID_Department = jp.ID_Department";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                MainViewModel.GetInstance().show = new frmShow("Employee", dt);
                MainViewModel.GetInstance().show.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("No records found.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ReturnEmployeeValues()
        {
            teEmpNumber.Text = sNo_Emp;
            teName.Text = sName;
            tePatSurname.Text = sP_LastName;
            teMatSurname.Text = sM_LastName;
            teEmail.Text = sEmail;
            teEmail2.Text = sEmail2;
            teEmail3.Text = sEmail3;
            tePhone.Text = sPhone;
            teCellPhone.Text = sCellPhone;
            teAD.Text = sAD;
            tePayroll_Rate.Text = nPayroll_Rate.ToString();
            cmbPayroll.Text = sPayroll;
            cmbPosition.Text = sPosition;
            chkStatus.Checked = bStatus;
            if (!string.IsNullOrEmpty(Path.GetExtension(sPic)))
            {
                picEmployee.Image = Image.FromFile(sPic);
            }
        }

        private bool NoEmptyValidation()
        {
            bool bReturn = false;
            if (!string.IsNullOrEmpty(teName.Text) && !string.IsNullOrEmpty(tePatSurname.Text))
            {
                bReturn = true;
            }
            return bReturn;
        }

        private void ClearEmployees(object sender, EventArgs e)
        {
            ClearEmployees();
        }

        private void ClearEmployees()
        {
            sEmployeeID = null;
            teEmpNumber.Text = string.Empty;
            teName.Text = string.Empty;
            tePatSurname.Text = string.Empty;
            teMatSurname.Text = string.Empty;
            teEmail.Text = string.Empty;
            teEmail2.Text = string.Empty;
            teEmail3.Text = string.Empty;
            tePhone.Text = string.Empty;
            teCellPhone.Text = string.Empty;
            teAD.Text = string.Empty;
            tePayroll_Rate.Text = string.Empty;
            cmbPayroll.SelectedIndex = -1;
            cmbPosition.SelectedIndex = -1;
            try
            {
                picEmployee.Image.Dispose();
            }
            catch { }
            picEmployee.Image = Resources.User;
            
        }

        private void FillEmpPositions()
        {
            string sCommand = "SELECT jp.ID_Position AS 'ID', jp.Position + ' - ' +  dp.Department AS 'POSITION' " +
                                "FROM Job_Positions jp " +
                                "LEFT JOIN Department dp ON dp.ID_Department = jp.ID_Department " +
                                "ORDER BY dp.Department, jp.Position";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                cmbPosition.ValueMember = "ID";
                cmbPosition.DisplayMember = "POSITION";
                cmbPosition.DataSource = dt;
                cmbPosition.SelectedIndex = -1;
            }
        }        

        private void FillEmpPayroll()
        {
            string sCommand = "SELECT ID_PayRoll AS 'ID', PayRoll AS 'PAYROLL' FROM PayRoll";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                cmbPayroll.ValueMember = "ID";
                cmbPayroll.DisplayMember = "PAYROLL";
                cmbPayroll.DataSource = dt;
                cmbPayroll.SelectedIndex = -1;
            }
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            FillEmpPositions();
            FillEmpPayroll();
        }
        #endregion

        #region Position
        private void SavePosition(object sender, EventArgs e)
        {
            bool Exists = false;
            if (!string.IsNullOrEmpty(tePosition.Text))
            {
                if (string.IsNullOrEmpty(sPosID))
                {
                    string sCommand = "SELECT ID_Department AS 'DEPARTMENT', Position AS 'POSITION' FROM Job_Positions WHERE ID_Department = @sDep AND Position = @sPos";
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand(sCommand, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@sDep", SqlDbType.NVarChar).Value = cmbPosDepartment.SelectedValue;
                            cmd.Parameters.Add("@sPos", SqlDbType.NVarChar).Value = tePosition.Text;
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    Exists = true;
                                }
                            }
                        }
                    }
                }

                if (Exists)
                {
                    XtraMessageBox.Show("This registry already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else 
                { 
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateJobPositions", sqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sID_Position", SqlDbType.NVarChar).Value = sPosID;
                            cmd.Parameters.Add("@sPosition", SqlDbType.NVarChar).Value = tePosition.Text;
                            cmd.Parameters.Add("@sID_Department", SqlDbType.NVarChar).Value = cmbPosDepartment.SelectedValue;
                            cmd.Parameters.Add("@sDescription", SqlDbType.NVarChar).Value = meDescription.Text.Trim();

                            try
                            {
                                sqlConn.Open();
                                int nSuccess = cmd.ExecuteNonQuery();
                                if (nSuccess != 0)
                                {
                                    XtraMessageBox.Show("Data has been successfully registered!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearPosition();
                                    FillEmpPositions();
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
                
            }
        }

        private void ShowPositions(object sender, EventArgs e)
        {
            string sCommand = "SELECT ps.ID_Position AS 'ID', ps.Position AS 'POSITION', dp.Department AS 'DEPARTMENT', ps.Description AS 'DESCRIPTION' " +
                                "FROM Job_Positions ps " +
                                "LEFT JOIN Department dp ON dp.ID_Department = ps.ID_Department " +
                                "ORDER BY dp.Department, ps.Position";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                MainViewModel.GetInstance().show = new frmShow("Position", dt);
                MainViewModel.GetInstance().show.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("No records found.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ReturnPositionValues()
        {
            tePosition.Text = sPosition;
            cmbPosDepartment.Text = sDepartment;
            meDescription.Text = sDescription;
        }

        private void ClearPosition(object sender, EventArgs e)
        {
            ClearPosition();
        }

        private void ClearPosition()
        {
            sPosID = null;
            tePosition.Text = string.Empty;
            cmbPosDepartment.SelectedIndex = -1;
            meDescription.Text = string.Empty;
        }

        private void FillPosDepartment()
        {
            string sCommand = "SELECT ID_Department AS 'ID', Department AS 'DEPARTMENT' FROM Department ORDER BY Department";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                cmbPosDepartment.ValueMember = "ID";
                cmbPosDepartment.DisplayMember = "DEPARTMENT";
                cmbPosDepartment.DataSource = dt;
                cmbPosDepartment.SelectedIndex = -1;
            }
        }        
        #endregion

        #region Departments
        private void SaveDepartments(object sender, EventArgs e)
        {
            bool Exists = false;
            if (!string.IsNullOrEmpty(teDepartment.Text))
            {
                if (string.IsNullOrEmpty(sDepID))
                {
                    string sCommand = "SELECT ID_Department FROM Department WHERE ID_Business = @sIDBusiness AND Department = @sDepartment";
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand(sCommand, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@sIDBusiness", SqlDbType.NVarChar).Value = cmbDepBusiness.SelectedValue;
                            cmd.Parameters.Add("@sDepartment", SqlDbType.NVarChar).Value = teDepartment.Text.Trim();

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    Exists = true;
                                }
                            }
                        }    
                    }
                }

                if (Exists)
                {
                    XtraMessageBox.Show("This registry already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateDepartments", sqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sID_Department", SqlDbType.NVarChar).Value = sDepID;
                            cmd.Parameters.Add("@sDepartment", SqlDbType.NVarChar).Value = teDepartment.Text.Trim();
                            cmd.Parameters.Add("@sID_Business", SqlDbType.NVarChar).Value = cmbDepBusiness.SelectedValue;
                            cmd.Parameters.Add("@sDescription", SqlDbType.NVarChar).Value = meDepDescription.Text.Trim();

                            try
                            {
                                sqlConn.Open();
                                int nSuccess = cmd.ExecuteNonQuery();
                                if (nSuccess != 0)
                                {
                                    if (nSuccess != 0)
                                    {
                                        XtraMessageBox.Show("Data has been successfully registered!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        ClearDepartment();
                                        FillPosDepartment();
                                    }
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void ShowDepartments(object sender, EventArgs e)
        {
            string sCommand = "SELECT dp.ID_Department AS 'ID', dp.Department AS 'DEPARTMENT', bs.Business AS 'BUSINESS', dp.Description AS 'DESCRIPTION' " +
                                "FROM Department dp " +
                                "LEFT JOIN Business bs ON bs.ID_Business = dp.ID_Business " +
                                "ORDER BY bs.Business, dp.Department";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                MainViewModel.GetInstance().show = new frmShow("Department", dt);
                MainViewModel.GetInstance().show.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("No records found.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ReturnDepartmentValues()
        {
            teDepartment.Text = sDepartment;
            cmbDepBusiness.Text = sBusiness;
            meDepDescription.Text = sDescription;
        }

        private void ClearDepartment(object sender, EventArgs e)
        {
            ClearDepartment();
        }

        private void ClearDepartment()
        {
            sDepID = null;
            teDepartment.Text = string.Empty;
            cmbDepBusiness.SelectedIndex = -1;
            meDepDescription.Text = string.Empty;
        }

        private void FillDepBusiness()
        {
            string sCommand = "SELECT ID_Business AS 'ID', Business AS 'BUSINESS' FROM Business ORDER BY Business";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                cmbDepBusiness.ValueMember = "ID";
                cmbDepBusiness.DisplayMember = "BUSINESS";
                cmbDepBusiness.DataSource = dt;
                cmbDepBusiness.SelectedIndex = -1;
            }
        }                        
        #endregion

        #region Business
        private void SaveBusiness(object sender, EventArgs e)
        {
            bool Exists = false;
            if (!string.IsNullOrEmpty(teBusiness.Text))
            {
                if (string.IsNullOrEmpty(sBusID))
                {
                    string sCommand = "SELECT ID_Business FROM Business WHERE ID_Location = @sLocID AND Business = @sBus";
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand(sCommand, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@sLocID", SqlDbType.NVarChar).Value = cmbBusLocation.SelectedValue;
                            cmd.Parameters.Add("@sBus", SqlDbType.NVarChar).Value = teBusiness.Text.Trim();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    Exists = true;
                                }
                            }
                        }
                    }
                }

                if (Exists)
                {
                    XtraMessageBox.Show("This registry already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn.Replace(@"\\", @"\")))
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateBusiness", sqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sID_Business", SqlDbType.NVarChar).Value = sBusID;
                            cmd.Parameters.Add("@sBusiness", SqlDbType.NVarChar).Value = teBusiness.Text;
                            cmd.Parameters.Add("@sID_Location", SqlDbType.NVarChar).Value = cmbBusLocation.SelectedValue;
                            cmd.Parameters.Add("@sDescription", SqlDbType.NVarChar).Value = meBusDescription.Text;

                            try
                            {
                                sqlConn.Open();
                                int nSuccess = cmd.ExecuteNonQuery();
                                if (nSuccess != 0)
                                {
                                    XtraMessageBox.Show("Data has been successfully registered!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearBusiness();
                                    FillDepBusiness();
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void ShowBusiness(object sender, EventArgs e)
        {
            string sCommand = "SELECT bs.ID_Business AS 'ID', bs.Business AS 'BUSINESS', lc.Location AS 'LOCATION', bs.Description AS 'DESCRIPTION' " +
                                "FROM Business bs " +
                                "LEFT JOIN Locations lc ON lc.ID_Location = bs.ID_Location " +
                                "ORDER BY lc.Location, bs.Business";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                MainViewModel.GetInstance().show = new frmShow("Business", dt);
                MainViewModel.GetInstance().show.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("No records found.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ReturnBusValues()
        {
            teBusiness.Text = sBusiness;
            cmbBusLocation.Text = sLocation;
            meBusDescription.Text = sDescription;
        }

        private void ClearBusiness(object sender, EventArgs e)
        {
            ClearBusiness();
        }

        private void ClearBusiness()
        {
            sBusID = null;
            teBusiness.Text = string.Empty;
            cmbBusLocation.SelectedIndex = -1;
            meBusDescription.Text = string.Empty;
        }

        private void FillBusLocation()
        {
            string sCommand = "SELECT ID_Location AS 'ID', Location AS 'LOCATION' FROM Locations ORDER BY Location";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                cmbBusLocation.ValueMember = "ID";
                cmbBusLocation.DisplayMember = "LOCATION";
                cmbBusLocation.DataSource = dt;
                cmbBusLocation.SelectedIndex = -1;
            }
        }                      
        #endregion

        #region Locations
        private void SaveLocation(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(teLocation.Text))
            {
                bool Exists = false;
                if (string.IsNullOrEmpty(sLocID))
                {
                    string sCommand = "SELECT ID_Location FROM Locations WHERE Country = @sCountry AND State = @sState AND Location = @sLocation";
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand(sCommand, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@sCountry", SqlDbType.NVarChar).Value = cmbLocCountry.SelectedValue;
                            cmd.Parameters.Add("@sState", SqlDbType.NVarChar).Value = cmbLocState.SelectedValue;
                            cmd.Parameters.Add("@sLocation", SqlDbType.NVarChar).Value = teLocation.Text.Trim();

                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    Exists = true;
                                }
                            }
                        }
                    }
                }

                if (Exists)
                {
                    XtraMessageBox.Show("This registry already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn.Replace(@"\\", @"\")))
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateLocations", sqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sID_Location", SqlDbType.NVarChar).Value = sLocID;
                            cmd.Parameters.Add("@sLocation", SqlDbType.NVarChar).Value = teLocation.Text;
                            cmd.Parameters.Add("@sCountry", SqlDbType.NVarChar).Value = sCountryID;
                            cmd.Parameters.Add("@sState", SqlDbType.NVarChar).Value = sStateID;

                            try
                            {
                                sqlConn.Open();
                                int nSuccess = cmd.ExecuteNonQuery();
                                if (nSuccess != 0)
                                {
                                    XtraMessageBox.Show("Data has been successfully registered!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearLocations();
                                    FillBusLocation();
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void ShowLocations(object sender, EventArgs e)
        {
            string sCommand = "SELECT loc.ID_Location AS 'ID', loc.Location AS 'LOCATION', st.state AS 'STATE', cty.country AS 'COUNTRY' " +
                                "FROM Locations loc " +
                                "LEFT JOIN Countries cty ON cty.pk_id = loc.Country " +
                                "LEFT JOIN States st ON st.pk_id = loc.State " +
                                "ORDER BY cty.country, st.state, loc.Location";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                MainViewModel.GetInstance().show = new frmShow("Locations", dt);
                MainViewModel.GetInstance().show.ShowDialog();         
            }
            else
            {
                XtraMessageBox.Show("No records found.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ReturnLocValues()
        {
            teLocation.Text = sLocation;
            cmbLocCountry.Text = sCountry;
            cmbLocState.Text = sState;            
        }

        private void ClearLocations(object sender, EventArgs e)
        {
            ClearLocations();
        }

        private void ClearLocations()
        {
            sLocID = null;
            sCountryID = string.Empty;
            sStateID = string.Empty;
            sLocation = string.Empty;
            cmbLocCountry.SelectedIndex = -1;
            cmbLocState.SelectedIndex = -1;
            teLocation.Text = string.Empty;
        }

        private void FillLocStatesCombo(string sCountry)
        {
            string sCommand = $"SELECT pk_id AS 'ID', state AS 'STATE' FROM States WHERE countryID = '{sCountry}'";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                cmbLocState.ValueMember = "ID";
                cmbLocState.DisplayMember = "STATE";
                cmbLocState.DataSource = dt;
                cmbLocState.SelectedIndex = -1;
            }
        }

        private void FillLocCountriesCombo()
        {
            string sCommand = "SELECT pk_id AS 'ID', country AS 'COUNTRY' FROM Countries";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                cmbLocCountry.ValueMember = "ID";
                cmbLocCountry.DisplayMember = "COUNTRY";
                cmbLocCountry.DataSource = dt;
                cmbLocCountry.SelectedIndex = -1;
            }
        }
               
        private void cmbLocCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                sCountryID = cmbLocCountry.SelectedValue.ToString();
                FillLocStatesCombo(sCountryID);
            }
            catch
            {
                sCountryID = "";
            }
        }        
        #endregion

        #region Payroll
        private void SavePayroll(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tePayroll.Text))
            {
                bool Exists = false;
                if (string.IsNullOrEmpty(sPayrollID))
                {
                    string sCommand = "SELECT ID_PayRoll FROM PayRoll WHERE PayRoll = @sPayroll";
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand(sCommand, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@sPayroll", SqlDbType.NVarChar).Value = tePayroll.Text.Trim();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    Exists = true;
                                }
                            }
                        }
                    }
                }

                if (Exists)
                {
                    XtraMessageBox.Show("This registry already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdatePayroll", sqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sID_Payroll", SqlDbType.NVarChar).Value = sPayrollID;
                            cmd.Parameters.Add("@sPayroll", SqlDbType.NVarChar).Value = tePayroll.Text.Trim();
                            cmd.Parameters.Add("@sDescription", SqlDbType.NVarChar).Value = mePayDescription.Text.Trim();
                            cmd.Parameters.Add("@sExchange", SqlDbType.NVarChar).Value = teExRate.Text.Trim();

                            try
                            {
                                sqlConn.Open();
                                int nSuccess = cmd.ExecuteNonQuery();
                                if (nSuccess != 0)
                                {
                                    XtraMessageBox.Show("Data has been successfully registered!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearPayroll();
                                    FillEmpPayroll();
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void ShowPayroll(object sender, EventArgs e)
        {
            string sCommand = "SELECT ID_PayRoll AS 'ID', PayRoll AS 'PAYROLL', Description AS 'DESCRIPTION', ExchangeRate AS 'EXCHANGE RATE' FROM PayRoll ORDER BY PayRoll";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                MainViewModel.GetInstance().show = new frmShow("Payroll", dt);
                MainViewModel.GetInstance().show.ShowDialog();                
            }
            else
            {
                XtraMessageBox.Show("No records found.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ReturnPayrollValues()
        {
            tePayroll.Text = sPayroll;
            teExRate.Text = sPayExchange;
            mePayDescription.Text = sDescription;
        }

        private void ClearPayroll(object sender, EventArgs e)
        {
            ClearPayroll();
        }

        private void ClearPayroll()
        {
            sPayrollID = null;
            tePayroll.Text = string.Empty;
            teExRate.Text = string.Empty;
            mePayDescription.Text = string.Empty;
        }
        #endregion

        #region User
        private void SaveUser(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtUser.Text))
            {
                bool Exists = false;
                if (string.IsNullOrEmpty(sUserID))
                {
                    string sCommand = "SELECT ID_User FROM Users WHERE Username = @sUser";
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand(sCommand, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@sUser", SqlDbType.NVarChar).Value = txtUser.Text.Trim();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    Exists = true;   
                                }
                            }
                        }
                    }
                }

                if (Exists)
                {
                    XtraMessageBox.Show("This registry already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (string.IsNullOrEmpty(sUserID) && string.IsNullOrEmpty(txtPassword.Text))
                    {
                        XtraMessageBox.Show("Password can not be blank", "Password!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        bool bValidPassword = true;
                        string pass = txtPassword.Text.Trim();
                        
                        if (!string.IsNullOrEmpty(pass))
                        {
                            bValidPassword = SecurePassword.IsSecure(pass);
                            pass = SecurePassword.Hash(pass);
                        }
                        else
                        {
                            pass = null;
                        }

                        string sRightParam = "";
                        try
                        {
                            sRightParam = cmbRightsUser.SelectedValue.ToString();
                        }
                        catch { }

                        if (bValidPassword)
                        {
                            using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                            {
                                using (SqlCommand cmd = new SqlCommand("UpdateUser", sqlConn))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.Add("@sID_User", SqlDbType.NVarChar).Value = sUserID;
                                    cmd.Parameters.Add("@sID_Employee", SqlDbType.NVarChar).Value = cmbUserEmployee.SelectedValue;
                                    cmd.Parameters.Add("@sUserName", SqlDbType.NVarChar).Value = txtUser.Text.Trim();
                                    cmd.Parameters.Add("@sPassword", SqlDbType.NVarChar).Value = pass;
                                    cmd.Parameters.Add("@bFirstTime", SqlDbType.Bit).Value = chkFirstTime.Checked;
                                    cmd.Parameters.Add("@bStatus", SqlDbType.Bit).Value = chkStatus.Checked;
                                    cmd.Parameters.Add("@sID_Right", SqlDbType.NVarChar).Value = sRightParam;

                                    try
                                    {
                                        sqlConn.Open();
                                        int nSuccess = cmd.ExecuteNonQuery();
                                        if (nSuccess != 0)
                                        {
                                            XtraMessageBox.Show("Data has been successfully registered!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            ClearUser();
                                            FillUserEmployeeCombo();
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                    }
                                }
                            }
                        }                                              
                    }
                }
            }
        }

        private void ShowUser(object sender, EventArgs e)
        {
            string sCommand = "SELECT us.ID_User AS 'ID', em.Name + ' ' + em.P_LastName + ' ' + em.M_LastName AS 'NAME', Username AS 'USERNAME', " +
                                "us.FirstTime AS 'FIRST TIME', us.Status AS 'ACTIVE', em.Pic AS 'PICTURE', rt.RightType AS 'RIGHT TYPE' " +
                                "FROM Users us " +
                                "LEFT JOIN Employees em ON em.ID_Employee = us.ID_Employee " +
                                "LEFT JOIN Rights_Type rt ON rt.ID_Right = us.ID_Right " +
                                "ORDER BY em.Name";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                MainViewModel.GetInstance().show = new frmShow("User", dt);
                MainViewModel.GetInstance().show.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("No records found.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ReturnUserValues()
        {            
            cmbUserEmployee.Text = sName;
            txtUser.Text = sUser;            
            chkFirstTime.Checked = bFirstTime;
            chkUserStatus.Checked = bStatus;
            cmbRightsUser.Text = sRight;
        }

        private void ClearUser(object sender, EventArgs e)
        {
            ClearUser();
        }

        private void ClearUser()
        {
            sUserID = null;
            sName = string.Empty;
            sUser = string.Empty;
            sPassword = string.Empty;
            bFirstTime = false;
            bStatus = false;
            cmbUserEmployee.SelectedIndex = -1;
            txtUser.Text = string.Empty;
            txtPassword.Text = string.Empty;
            chkFirstTime.Checked = false;
            chkUserStatus.Checked = false;
            cmbRightsUser.SelectedIndex = -1;
            picEmployee.Image = Resources.User;
        }

        private void FillUserEmployeeCombo()
        {
            string sCommand = "SELECT ID_Employee AS 'ID', Name + ' ' + P_LastName + ' ' + M_LastName AS 'NAME' FROM Employees ORDER BY No_EMP";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                cmbUserEmployee.ValueMember = "ID";
                cmbUserEmployee.DisplayMember = "NAME";
                cmbUserEmployee.DataSource = dt;
                cmbUserEmployee.SelectedIndex = -1;
            }
        }

        private void FillRightsCombo()
        {
            string sCommand = "SELECT ID_Right AS 'ID', RightType AS 'RIGHT TYPE' FROM Rights_Type ORDER BY RightType";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                cmbRightsUser.ValueMember = "ID";
                cmbRightsUser.DisplayMember = "RIGHT TYPE";
                cmbRightsUser.DataSource = dt;
                cmbRightsUser.SelectedIndex = -1;
            }
        }

        private void cmbUserEmployee_SelectedIndexChanged(object sender, EventArgs e)
        {
            string sCommand = "SELECT us.ID_User AS 'ID USER', us.Username AS 'USER', us.ID_Right AS 'ID Right', us.FirstTime AS 'FIRST TIME', us.Status AS 'STATUS', em.Pic AS 'PICTURE' " +
                                "FROM Employees em LEFT JOIN Users us ON us.ID_Employee = em.ID_Employee " +
                                $"WHERE em.ID_Employee = '{cmbUserEmployee.SelectedValue}'";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                sUserID = dt.Rows[0]["ID USER"].ToString();
                txtUser.Text = dt.Rows[0]["USER"].ToString();
                cmbRightsUser.SelectedValue = dt.Rows[0]["ID Right"].ToString();
                try
                {
                    chkFirstTime.Checked = Convert.ToBoolean(dt.Rows[0]["FIRST TIME"]);
                }
                catch
                {
                    chkFirstTime.Checked = false;
                }

                try
                {
                    chkUserStatus.Checked = Convert.ToBoolean(dt.Rows[0]["STATUS"]);
                }
                catch
                {
                    chkUserStatus.Checked = false;
                }

                string sPicPath = dt.Rows[0]["PICTURE"].ToString();
                sPicPath = string.Concat(Constants.sPath, sPicPath);
                try
                {
                    picUser.Image = Image.FromFile(sPicPath);
                }
                catch
                {
                    picUser.Image = Resources.User;
                }
            }
            else
            {
                ClearUser();
            }
        }
        #endregion

        #region Rights
        private void SaveRights(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtRightType.Text))
            {
                bool Exists = false;
                if (string.IsNullOrEmpty(sRightID))
                {
                    string sCommand = "SELECT ID_Right FROM Rights_Type WHERE RightType = @sRight";
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand(sCommand, sqlConn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Add("@sRight", SqlDbType.NVarChar).Value = txtRightType.Text.Trim();
                            using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                            {
                                DataTable dt = new DataTable();
                                da.Fill(dt);
                                if (dt.Rows.Count > 0)
                                {
                                    Exists = true;
                                }
                            }
                        }
                    }
                }

                if (Exists)
                {
                    XtraMessageBox.Show("This registry already exists", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    using (SqlConnection sqlConn = new SqlConnection(Constants.cn))
                    {
                        using (SqlCommand cmd = new SqlCommand("UpdateRights", sqlConn))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@sID_Right", SqlDbType.NVarChar).Value = sRightID;
                            cmd.Parameters.Add("@sRightType", SqlDbType.NVarChar).Value = txtRightType.Text.Trim();
                            cmd.Parameters.Add("@bEditing", SqlDbType.Bit).Value = chkEditing.Checked;
                            cmd.Parameters.Add("@bModify", SqlDbType.Bit).Value = chkModify.Checked;
                            cmd.Parameters.Add("@bOnlyRead", SqlDbType.Bit).Value = chkReadOnly.Checked;

                            try
                            {
                                sqlConn.Open();
                                int nSuccess = cmd.ExecuteNonQuery();
                                if (nSuccess != 0)
                                {
                                    XtraMessageBox.Show("Data has been successfully registered!", "Done!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    ClearRights();
                                }
                            }
                            catch (Exception ex)
                            {
                                XtraMessageBox.Show($"There has been an error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                }
            }
        }

        private void ShowRights(object sender, EventArgs e)
        {
            string sCommand = "SELECT ID_Right AS 'ID', RightType AS 'RIGHT TYPE', Editing AS 'EDITING', Modify AS 'MODIFY', OnlyRead AS 'READ ONLY' FROM Rights_Type ORDER BY RightType";
            DataTable dt = SQL.Read(sCommand);
            if (dt.Rows.Count > 0)
            {
                MainViewModel.GetInstance().show = new frmShow("Rights", dt);
                MainViewModel.GetInstance().show.ShowDialog();
            }
            else
            {
                XtraMessageBox.Show("No records found.", "Empty", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        public void ReturnRightsValues()
        {
            txtRightType.Text = sRight;
            chkEditing.Checked = bEditing;
            chkModify.Checked = bModify;
            chkReadOnly.Checked = bReadOnly;
        }

        private void ClearRights(object sender, EventArgs e)
        {
            ClearRights();
        }

        private void ClearRights()
        {
            sRightID = null;
            sRight = string.Empty;
            bEditing = false;
            bModify = false;
            bReadOnly = false;
            txtRightType.Text = string.Empty;
            chkEditing.Checked = false;
            chkModify.Checked = false;
            chkReadOnly.Checked = false;
            
        }        
        #endregion

        private void xTabInfo_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            switch (xTabInfo.SelectedTabPageIndex)
            {
                case 0:
                    Size = new Size(584, 534);
                    break;

                case 1:
                    Size = new Size(584, 460);
                    FillPosDepartment();
                    break;

                case 2:
                    Size = new Size(584, 460);
                    FillDepBusiness();
                    break;

                case 3:
                    Size = new Size(584, 460);
                    FillBusLocation();
                    break;

                case 4:
                    Size = new Size(584, 460);
                    FillLocCountriesCombo();
                    break;

                case 5:
                    Size = new Size(584, 460);
                    break;

                case 6:
                    Size = new Size(584, 460);
                    FillUserEmployeeCombo();
                    FillRightsCombo();
                    break;

                case 7:
                    Size = new Size(584, 460);                    
                    break;
            }
        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            MainViewModel.GetInstance().login.Close();
        }
    }
}