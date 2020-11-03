namespace Employees_General_Info.Views
{
    partial class frmShow
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmShow));
            this.panel1 = new System.Windows.Forms.Panel();
            this.gcShow = new DevExpress.XtraGrid.GridControl();
            this.gvShow = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcShow)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvShow)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.gcShow);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(940, 460);
            this.panel1.TabIndex = 0;
            // 
            // gcShow
            // 
            this.gcShow.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcShow.Location = new System.Drawing.Point(0, 0);
            this.gcShow.MainView = this.gvShow;
            this.gcShow.Name = "gcShow";
            this.gcShow.Size = new System.Drawing.Size(936, 456);
            this.gcShow.TabIndex = 0;
            this.gcShow.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvShow});
            // 
            // gvShow
            // 
            this.gvShow.GridControl = this.gcShow;
            this.gvShow.Name = "gvShow";
            this.gvShow.OptionsBehavior.Editable = false;
            this.gvShow.OptionsFind.AlwaysVisible = true;
            this.gvShow.OptionsView.ShowGroupPanel = false;
            this.gvShow.DoubleClick += new System.EventHandler(this.gvShow_DoubleClick);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("simpleButton1.ImageOptions.Image")));
            this.btnExport.Location = new System.Drawing.Point(893, 3);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(40, 40);
            this.btnExport.TabIndex = 1;
            // 
            // frmShow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(940, 460);
            this.Controls.Add(this.panel1);
            this.Name = "frmShow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Show";
            this.Load += new System.EventHandler(this.frmShow_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcShow)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvShow)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraGrid.GridControl gcShow;
        private DevExpress.XtraGrid.Views.Grid.GridView gvShow;
        private DevExpress.XtraEditors.SimpleButton btnExport;
    }
}