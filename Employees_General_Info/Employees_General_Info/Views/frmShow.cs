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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
using Employees_General_Info.Properties;
using Employees_General_Info.Models;
using System.Diagnostics;

namespace Employees_General_Info.Views
{
    public partial class frmShow : DevExpress.XtraEditors.XtraForm
    {
        private string sCatalog;
        private DataTable dt;
        public frmShow(string sCatalog, DataTable dt)
        {
            InitializeComponent();
            btnExport.Click += new EventHandler(Export);
            this.sCatalog = sCatalog;
            this.dt = dt;
        }

        private void ShowData()
        {
            gcShow.DataSource = dt;
            gvShow.Columns[0].Visible = false;
            try
            {
                gvShow.Columns["PICTURE"].Visible = false;
            }
            catch { }
            gvShow.OptionsView.ColumnAutoWidth = false;
            gvShow.BestFitColumns();
            GridViewInfo viewInfo = gvShow.GetViewInfo() as GridViewInfo;
            if (viewInfo.ViewRects.ColumnTotalWidth < viewInfo.ViewRects.ColumnPanelWidth)
            {
                gvShow.OptionsView.ColumnAutoWidth = true;
            }
        }

        private void ReturnEmployeeData(GridView view)
        {
            MainViewModel.GetInstance().main.sEmployeeID = view.GetRowCellValue(view.FocusedRowHandle, "ID").ToString();
            MainViewModel.GetInstance().main.sNo_Emp = view.GetRowCellValue(view.FocusedRowHandle, "EMPLOYEE NUMBER").ToString();
            MainViewModel.GetInstance().main.sName = view.GetRowCellValue(view.FocusedRowHandle, "NAME").ToString();
            MainViewModel.GetInstance().main.sP_LastName = view.GetRowCellValue(view.FocusedRowHandle, "PATERNAL SURNAME").ToString();
            MainViewModel.GetInstance().main.sM_LastName = view.GetRowCellValue(view.FocusedRowHandle, "MATERNAL SURNAME").ToString();
            MainViewModel.GetInstance().main.sPosition = view.GetRowCellValue(view.FocusedRowHandle, "POSITION").ToString();
            MainViewModel.GetInstance().main.sEmail = view.GetRowCellValue(view.FocusedRowHandle, "EMAIL 1").ToString();
            MainViewModel.GetInstance().main.sEmail2 = view.GetRowCellValue(view.FocusedRowHandle, "EMAIL 2").ToString();
            MainViewModel.GetInstance().main.sEmail3 = view.GetRowCellValue(view.FocusedRowHandle, "EMAIL 3").ToString();
            MainViewModel.GetInstance().main.sPhone = view.GetRowCellValue(view.FocusedRowHandle, "PHONE").ToString();
            MainViewModel.GetInstance().main.sCellPhone = view.GetRowCellValue(view.FocusedRowHandle, "CELL PHONE").ToString();
            MainViewModel.GetInstance().main.sAD = view.GetRowCellValue(view.FocusedRowHandle, "ACTIVE DIRECTORY").ToString();
            MainViewModel.GetInstance().main.sPic = string.Concat(Constants.sPath, view.GetRowCellValue(view.FocusedRowHandle, "PIC").ToString());
            try
            {
                MainViewModel.GetInstance().main.nPayroll_Rate = Convert.ToDecimal(view.GetRowCellValue(view.FocusedRowHandle, "PAYROLL RATE"));
            }
            catch
            {
                MainViewModel.GetInstance().main.nPayroll_Rate = 0;
            }

            try
            {
                MainViewModel.GetInstance().main.bStatus = Convert.ToBoolean(view.GetRowCellValue(view.FocusedRowHandle, "ACTIVE"));
            }
            catch
            {
                MainViewModel.GetInstance().main.bStatus = false;
            }
            MainViewModel.GetInstance().main.ReturnEmployeeValues();            
            Close();
        }

        private void ReturnPositionData(GridView view)
        {
            MainViewModel.GetInstance().main.sPosID = view.GetRowCellValue(view.FocusedRowHandle, "ID").ToString();
            MainViewModel.GetInstance().main.sPosition = view.GetRowCellValue(view.FocusedRowHandle, "POSITION").ToString();
            MainViewModel.GetInstance().main.sDepartment = view.GetRowCellValue(view.FocusedRowHandle, "DEPARTMENT").ToString();
            MainViewModel.GetInstance().main.sDescription = view.GetRowCellValue(view.FocusedRowHandle, "DESCRIPTION").ToString();

            MainViewModel.GetInstance().main.ReturnPositionValues();
            Close();
        }

        private void ReturnDepartmentData(GridView view)
        {
            MainViewModel.GetInstance().main.sDepID = view.GetRowCellValue(view.FocusedRowHandle, "ID").ToString();
            MainViewModel.GetInstance().main.sDepartment = view.GetRowCellValue(view.FocusedRowHandle, "DEPARTMENT").ToString();
            MainViewModel.GetInstance().main.sBusiness = view.GetRowCellValue(view.FocusedRowHandle, "BUSINESS").ToString();
            MainViewModel.GetInstance().main.sDescription = view.GetRowCellValue(view.FocusedRowHandle, "DESCRIPTION").ToString();

            MainViewModel.GetInstance().main.ReturnDepartmentValues();
            Close();
        }

        private void ReturnBusinessData(GridView view)
        {
            MainViewModel.GetInstance().main.sBusID = view.GetRowCellValue(view.FocusedRowHandle, "ID").ToString();
            MainViewModel.GetInstance().main.sBusiness = view.GetRowCellValue(view.FocusedRowHandle, "BUSINESS").ToString();
            MainViewModel.GetInstance().main.sLocation = view.GetRowCellValue(view.FocusedRowHandle, "LOCATION").ToString();
            MainViewModel.GetInstance().main.sDescription = view.GetRowCellValue(view.FocusedRowHandle, "DESCRIPTION").ToString();

            MainViewModel.GetInstance().main.ReturnBusValues();
            Close();
        }

        private void ReturnLocationData(GridView view)
        {
            MainViewModel.GetInstance().main.sLocID = view.GetRowCellValue(view.FocusedRowHandle, "ID").ToString();
            MainViewModel.GetInstance().main.sLocation = view.GetRowCellValue(view.FocusedRowHandle, "LOCATION").ToString();
            MainViewModel.GetInstance().main.sState = view.GetRowCellValue(view.FocusedRowHandle, "STATE").ToString();
            MainViewModel.GetInstance().main.sCountry = view.GetRowCellValue(view.FocusedRowHandle, "COUNTRY").ToString();

            MainViewModel.GetInstance().main.ReturnLocValues();
            Close();
        }

        private void ReturnPayRollData(GridView view)
        {
            MainViewModel.GetInstance().main.sPayrollID = view.GetRowCellValue(view.FocusedRowHandle, "ID").ToString();
            MainViewModel.GetInstance().main.sPayroll = view.GetRowCellValue(view.FocusedRowHandle, "PAYROLL").ToString();
            MainViewModel.GetInstance().main.sPayExchange = view.GetRowCellValue(view.FocusedRowHandle, "EXCHANGE RATE").ToString();
            MainViewModel.GetInstance().main.sDescription = view.GetRowCellValue(view.FocusedRowHandle, "DESCRIPTION").ToString();
            MainViewModel.GetInstance().main.ReturnPayrollValues();            
            Close();
        }

        private void ReturnUserData(GridView view)
        {
            MainViewModel.GetInstance().main.sUserID = view.GetRowCellValue(view.FocusedRowHandle, "ID").ToString();
            MainViewModel.GetInstance().main.sName = view.GetRowCellValue(view.FocusedRowHandle, "NAME").ToString();
            MainViewModel.GetInstance().main.sUser = view.GetRowCellValue(view.FocusedRowHandle, "USERNAME").ToString();
            MainViewModel.GetInstance().main.bFirstTime = Convert.ToBoolean(view.GetRowCellValue(view.FocusedRowHandle, "FIRST TIME"));
            MainViewModel.GetInstance().main.bStatus = Convert.ToBoolean(view.GetRowCellValue(view.FocusedRowHandle, "ACTIVE"));
            MainViewModel.GetInstance().main.sPic = view.GetRowCellValue(view.FocusedRowHandle, "PICTURE").ToString();
            MainViewModel.GetInstance().main.sRight = view.GetRowCellValue(view.FocusedRowHandle, "RIGHT TYPE").ToString();

            MainViewModel.GetInstance().main.ReturnUserValues();
            Close();
        }

        private void ReturnRightsData(GridView view)
        {
            MainViewModel.GetInstance().main.sRightID = view.GetRowCellValue(view.FocusedRowHandle, "ID").ToString();
            MainViewModel.GetInstance().main.sRight = view.GetRowCellValue(view.FocusedRowHandle, "RIGHT TYPE").ToString();
            MainViewModel.GetInstance().main.bEditing = Convert.ToBoolean(view.GetRowCellValue(view.FocusedRowHandle, "EDITING"));
            MainViewModel.GetInstance().main.bModify = Convert.ToBoolean(view.GetRowCellValue(view.FocusedRowHandle, "MODIFY"));
            MainViewModel.GetInstance().main.bReadOnly = Convert.ToBoolean(view.GetRowCellValue(view.FocusedRowHandle, "READ ONLY"));
            MainViewModel.GetInstance().main.ReturnRightsValues();
            Close();
        }

        private void Export(object sender, EventArgs e)
        {            
            SaveFileDialog save = new SaveFileDialog()
            {
                Filter = "Excel File|*.xlsx"
            };
            if (save.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(save.FileName))
                {
                    gvShow.ExportToXlsx(save.FileName);
                    if (XtraMessageBox.Show($"File {save.FileName} has been successfully created,\nDo you want to open it?", "Exported", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        Process.Start(save.FileName);
                    }
                }
            }
        }

        private void frmShow_Load(object sender, EventArgs e)
        {
            Text = $"Show {sCatalog}";
            switch (sCatalog)
            {
                case "Employee":
                    Icon = Resources._124_1247409_employees_icon_png_virtual_assistant_icon_png;
                    break;

                case "Locations":
                    Icon = Resources.Loc1;
                    break;

                case "Business":
                    Icon = Resources.Business1;
                    break;

                case "Department":
                    Icon = Resources.Departments1;
                    break;

                case "Payroll":
                    Icon = Resources.Payroll;
                    break;

                case "User":
                    Icon = Resources.User1;
                    break;

                case "Rights":
                    Icon = Resources.user_permissions1;
                    break;
            }
            ShowData();
        }

        private void gvShow_DoubleClick(object sender, EventArgs e)
        {
            GridView view = sender as GridView;
            switch (sCatalog)
            {
                case "Employee":
                    ReturnEmployeeData(view);
                    break;

                case "Locations":
                    ReturnLocationData(view);
                    break;

                case "Business":
                    ReturnBusinessData(view);
                    break;

                case "Department":
                    ReturnDepartmentData(view);
                    break;

                case "Position":
                    ReturnPositionData(view);
                    break;

                case "Payroll":
                    ReturnPayRollData(view);
                    break;

                case "User":
                    ReturnUserData(view);
                    break;

                case "Rights":
                    ReturnRightsData(view);
                    break;
            }
        }
    }
}