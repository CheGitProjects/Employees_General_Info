
namespace Employees_General_Info.Views
{
    partial class frmRoles
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRoles));
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.btnDelete = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcRoles = new DevExpress.XtraGrid.GridControl();
            this.gvRoles = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.txtRole = new DevExpress.XtraEditors.TextEdit();
            this.rbFullControl = new System.Windows.Forms.RadioButton();
            this.rbRead = new System.Windows.Forms.RadioButton();
            this.rbWrite = new System.Windows.Forms.RadioButton();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcRoles)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoles)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRole.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.panelControl3);
            this.panelControl1.Controls.Add(this.panelControl2);
            this.panelControl1.Controls.Add(this.groupBox2);
            this.panelControl1.Controls.Add(this.groupBox1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelControl1.Location = new System.Drawing.Point(0, 0);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(586, 380);
            this.panelControl1.TabIndex = 1;
            // 
            // panelControl3
            // 
            this.panelControl3.Controls.Add(this.btnDelete);
            this.panelControl3.Controls.Add(this.btnClear);
            this.panelControl3.Controls.Add(this.btnSave);
            this.panelControl3.Location = new System.Drawing.Point(12, 312);
            this.panelControl3.Name = "panelControl3";
            this.panelControl3.Size = new System.Drawing.Size(269, 54);
            this.panelControl3.TabIndex = 4;
            // 
            // btnDelete
            // 
            this.btnDelete.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnDelete.ImageOptions.Image")));
            this.btnDelete.Location = new System.Drawing.Point(207, 9);
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnDelete.Size = new System.Drawing.Size(40, 40);
            this.btnDelete.TabIndex = 6;
            this.btnDelete.ToolTip = "Delete";
            // 
            // btnClear
            // 
            this.btnClear.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnClear.ImageOptions.Image")));
            this.btnClear.Location = new System.Drawing.Point(118, 9);
            this.btnClear.Name = "btnClear";
            this.btnClear.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnClear.Size = new System.Drawing.Size(40, 40);
            this.btnClear.TabIndex = 5;
            this.btnClear.ToolTip = "Clear";
            // 
            // btnSave
            // 
            this.btnSave.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("btnSave.ImageOptions.Image")));
            this.btnSave.Location = new System.Drawing.Point(25, 9);
            this.btnSave.Name = "btnSave";
            this.btnSave.PaintStyle = DevExpress.XtraEditors.Controls.PaintStyles.Light;
            this.btnSave.Size = new System.Drawing.Size(40, 40);
            this.btnSave.TabIndex = 4;
            this.btnSave.ToolTip = "Save";
            // 
            // panelControl2
            // 
            this.panelControl2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panelControl2.Controls.Add(this.gcRoles);
            this.panelControl2.Location = new System.Drawing.Point(298, 12);
            this.panelControl2.Name = "panelControl2";
            this.panelControl2.Size = new System.Drawing.Size(276, 356);
            this.panelControl2.TabIndex = 3;
            // 
            // gcRoles
            // 
            this.gcRoles.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcRoles.Location = new System.Drawing.Point(2, 2);
            this.gcRoles.MainView = this.gvRoles;
            this.gcRoles.Name = "gcRoles";
            this.gcRoles.Size = new System.Drawing.Size(272, 352);
            this.gcRoles.TabIndex = 0;
            this.gcRoles.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRoles});
            // 
            // gvRoles
            // 
            this.gvRoles.GridControl = this.gcRoles;
            this.gvRoles.Name = "gvRoles";
            this.gvRoles.OptionsBehavior.Editable = false;
            this.gvRoles.OptionsView.ShowGroupPanel = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rbWrite);
            this.groupBox2.Controls.Add(this.rbRead);
            this.groupBox2.Controls.Add(this.rbFullControl);
            this.groupBox2.Location = new System.Drawing.Point(12, 99);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(269, 174);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Permits";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txtRole);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(269, 67);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Role";
            // 
            // txtRole
            // 
            this.txtRole.Location = new System.Drawing.Point(25, 29);
            this.txtRole.Name = "txtRole";
            this.txtRole.Properties.CharacterCasing = System.Windows.Forms.CharacterCasing.Upper;
            this.txtRole.Size = new System.Drawing.Size(222, 20);
            this.txtRole.TabIndex = 0;
            // 
            // rbFullControl
            // 
            this.rbFullControl.AutoSize = true;
            this.rbFullControl.Location = new System.Drawing.Point(95, 31);
            this.rbFullControl.Name = "rbFullControl";
            this.rbFullControl.Size = new System.Drawing.Size(79, 17);
            this.rbFullControl.TabIndex = 1;
            this.rbFullControl.TabStop = true;
            this.rbFullControl.Text = "Full Control";
            this.rbFullControl.UseVisualStyleBackColor = true;
            // 
            // rbRead
            // 
            this.rbRead.AutoSize = true;
            this.rbRead.Location = new System.Drawing.Point(95, 84);
            this.rbRead.Name = "rbRead";
            this.rbRead.Size = new System.Drawing.Size(50, 17);
            this.rbRead.TabIndex = 1;
            this.rbRead.TabStop = true;
            this.rbRead.Text = "Read";
            this.rbRead.UseVisualStyleBackColor = true;
            // 
            // rbWrite
            // 
            this.rbWrite.AutoSize = true;
            this.rbWrite.Location = new System.Drawing.Point(95, 138);
            this.rbWrite.Name = "rbWrite";
            this.rbWrite.Size = new System.Drawing.Size(51, 17);
            this.rbWrite.TabIndex = 1;
            this.rbWrite.TabStop = true;
            this.rbWrite.Text = "Write";
            this.rbWrite.UseVisualStyleBackColor = true;
            // 
            // frmRoles
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(586, 380);
            this.Controls.Add(this.panelControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmRoles";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmRoles";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmRoles_FormClosing);
            this.Load += new System.EventHandler(this.frmRoles_Load);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcRoles)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRoles)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtRole.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.SimpleButton btnDelete;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gcRoles;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRoles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private DevExpress.XtraEditors.TextEdit txtRole;
        private System.Windows.Forms.RadioButton rbWrite;
        private System.Windows.Forms.RadioButton rbRead;
        private System.Windows.Forms.RadioButton rbFullControl;
    }
}