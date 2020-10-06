/*
 * Created by SharpDevelop.
 * User: Benoit Le Guern
 * Date: 25/03/2007
 * Time: 18:36
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace VMI
{
	partial class MainForm
	{
		/// <summary>
		/// Designer variable used to keep track of non-visual components.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
		/// <summary>
		/// Disposes resources used by the form.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing) {
				if (components != null) {
					components.Dispose();
				}
			}
			base.Dispose(disposing);
		}
		
		/// <summary>
		/// This method is required for Windows Forms designer support.
		/// Do not change the method contents inside the source code editor. The Forms designer might
		/// not be able to load this method if it was changed manually.
		/// </summary>
		private void InitializeComponent()
		{
			this.gbxCustomer = new System.Windows.Forms.GroupBox();
			this.lblCustomer = new System.Windows.Forms.Label();
			this.cbxCustomer = new System.Windows.Forms.ComboBox();
			this.gbxDsDsrp = new System.Windows.Forms.GroupBox();
			this.btnDsrpInit = new System.Windows.Forms.Button();
			this.btnDsrpStart = new System.Windows.Forms.Button();
			this.btnDsStart = new System.Windows.Forms.Button();
			this.btnQuit = new System.Windows.Forms.Button();
			this.gbxExport = new System.Windows.Forms.GroupBox();
			this.btnExport = new System.Windows.Forms.Button();
			this.lbxExport = new System.Windows.Forms.ListBox();
			this.gbxOption = new System.Windows.Forms.GroupBox();
			this.btnOption = new System.Windows.Forms.Button();
			this.gbxMessage = new System.Windows.Forms.GroupBox();
			this.lblMessage = new System.Windows.Forms.Label();
			this.gbxImport = new System.Windows.Forms.GroupBox();
			this.btnImportOpen = new System.Windows.Forms.Button();
			this.gbxCustomer.SuspendLayout();
			this.gbxDsDsrp.SuspendLayout();
			this.gbxExport.SuspendLayout();
			this.gbxOption.SuspendLayout();
			this.gbxMessage.SuspendLayout();
			this.gbxImport.SuspendLayout();
			this.SuspendLayout();
			// 
			// gbxCustomer
			// 
			this.gbxCustomer.Controls.Add(this.lblCustomer);
			this.gbxCustomer.Controls.Add(this.cbxCustomer);
			this.gbxCustomer.Location = new System.Drawing.Point(12, 12);
			this.gbxCustomer.Name = "gbxCustomer";
			this.gbxCustomer.Size = new System.Drawing.Size(260, 54);
			this.gbxCustomer.TabIndex = 0;
			this.gbxCustomer.TabStop = false;
			this.gbxCustomer.Text = "Choix du client";
			// 
			// lblCustomer
			// 
			this.lblCustomer.Location = new System.Drawing.Point(6, 22);
			this.lblCustomer.Name = "lblCustomer";
			this.lblCustomer.Size = new System.Drawing.Size(40, 18);
			this.lblCustomer.TabIndex = 1;
			this.lblCustomer.Text = "Client :";
			// 
			// cbxCustomer
			// 
			this.cbxCustomer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbxCustomer.FormattingEnabled = true;
			this.cbxCustomer.Location = new System.Drawing.Point(52, 19);
			this.cbxCustomer.Name = "cbxCustomer";
			this.cbxCustomer.Size = new System.Drawing.Size(200, 21);
			this.cbxCustomer.TabIndex = 0;
			this.cbxCustomer.SelectedIndexChanged += new System.EventHandler(this.CbxCustomerSelectedIndexChanged);
			// 
			// gbxDsDsrp
			// 
			this.gbxDsDsrp.Controls.Add(this.btnDsrpInit);
			this.gbxDsDsrp.Controls.Add(this.btnDsrpStart);
			this.gbxDsDsrp.Controls.Add(this.btnDsStart);
			this.gbxDsDsrp.Location = new System.Drawing.Point(12, 72);
			this.gbxDsDsrp.Name = "gbxDsDsrp";
			this.gbxDsDsrp.Size = new System.Drawing.Size(195, 129);
			this.gbxDsDsrp.TabIndex = 1;
			this.gbxDsDsrp.TabStop = false;
			this.gbxDsDsrp.Text = "DS/DSRP";
			// 
			// btnDsrpInit
			// 
			this.btnDsrpInit.Location = new System.Drawing.Point(6, 19);
			this.btnDsrpInit.Name = "btnDsrpInit";
			this.btnDsrpInit.Size = new System.Drawing.Size(181, 23);
			this.btnDsrpInit.TabIndex = 2;
			this.btnDsrpInit.Text = "Initialisation";
			this.btnDsrpInit.UseVisualStyleBackColor = true;
			this.btnDsrpInit.Click += new System.EventHandler(this.BtnDsrpInitClick);
			// 
			// btnDsrpStart
			// 
			this.btnDsrpStart.Location = new System.Drawing.Point(6, 96);
			this.btnDsrpStart.Name = "btnDsrpStart";
			this.btnDsrpStart.Size = new System.Drawing.Size(181, 23);
			this.btnDsrpStart.TabIndex = 1;
			this.btnDsrpStart.Text = "Démarrer DSRP";
			this.btnDsrpStart.UseVisualStyleBackColor = true;
			this.btnDsrpStart.Click += new System.EventHandler(this.BtnDsrpStartClick);
			// 
			// btnDsStart
			// 
			this.btnDsStart.Location = new System.Drawing.Point(6, 67);
			this.btnDsStart.Name = "btnDsStart";
			this.btnDsStart.Size = new System.Drawing.Size(181, 23);
			this.btnDsStart.TabIndex = 0;
			this.btnDsStart.Text = "Démarrer DS";
			this.btnDsStart.UseVisualStyleBackColor = true;
			this.btnDsStart.Click += new System.EventHandler(this.BtnDsStartClick);
			// 
			// btnQuit
			// 
			this.btnQuit.Location = new System.Drawing.Point(55, 226);
			this.btnQuit.Name = "btnQuit";
			this.btnQuit.Size = new System.Drawing.Size(103, 39);
			this.btnQuit.TabIndex = 2;
			this.btnQuit.Text = "Quitter";
			this.btnQuit.UseVisualStyleBackColor = true;
			this.btnQuit.Click += new System.EventHandler(this.BtnQuitClick);
			// 
			// gbxExport
			// 
			this.gbxExport.Controls.Add(this.btnExport);
			this.gbxExport.Controls.Add(this.lbxExport);
			this.gbxExport.Location = new System.Drawing.Point(213, 129);
			this.gbxExport.Name = "gbxExport";
			this.gbxExport.Size = new System.Drawing.Size(195, 146);
			this.gbxExport.TabIndex = 2;
			this.gbxExport.TabStop = false;
			this.gbxExport.Text = "Exportation";
			// 
			// btnExport
			// 
			this.btnExport.Location = new System.Drawing.Point(8, 115);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(181, 23);
			this.btnExport.TabIndex = 3;
			this.btnExport.Text = "Exporter";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.BtnExportClick);
			// 
			// lbxExport
			// 
			this.lbxExport.FormattingEnabled = true;
			this.lbxExport.Location = new System.Drawing.Point(7, 21);
			this.lbxExport.Name = "lbxExport";
			this.lbxExport.Size = new System.Drawing.Size(181, 82);
			this.lbxExport.TabIndex = 0;
			this.lbxExport.SelectedIndexChanged += new System.EventHandler(this.LbxExportSelectedIndexChanged);
			// 
			// gbxOption
			// 
			this.gbxOption.Controls.Add(this.btnOption);
			this.gbxOption.Location = new System.Drawing.Point(283, 12);
			this.gbxOption.Name = "gbxOption";
			this.gbxOption.Size = new System.Drawing.Size(125, 54);
			this.gbxOption.TabIndex = 2;
			this.gbxOption.TabStop = false;
			this.gbxOption.Text = "Options";
			// 
			// btnOption
			// 
			this.btnOption.Location = new System.Drawing.Point(6, 18);
			this.btnOption.Name = "btnOption";
			this.btnOption.Size = new System.Drawing.Size(112, 23);
			this.btnOption.TabIndex = 0;
			this.btnOption.Text = "Options...";
			this.btnOption.UseVisualStyleBackColor = true;
			this.btnOption.Click += new System.EventHandler(this.BtnOptionClick);
			// 
			// gbxMessage
			// 
			this.gbxMessage.Controls.Add(this.lblMessage);
			this.gbxMessage.Location = new System.Drawing.Point(12, 281);
			this.gbxMessage.Name = "gbxMessage";
			this.gbxMessage.Size = new System.Drawing.Size(396, 97);
			this.gbxMessage.TabIndex = 4;
			this.gbxMessage.TabStop = false;
			this.gbxMessage.Text = "Message";
			// 
			// lblMessage
			// 
			this.lblMessage.ForeColor = System.Drawing.Color.Red;
			this.lblMessage.Location = new System.Drawing.Point(6, 16);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(383, 69);
			this.lblMessage.TabIndex = 0;
			// 
			// gbxImport
			// 
			this.gbxImport.Controls.Add(this.btnImportOpen);
			this.gbxImport.Location = new System.Drawing.Point(214, 73);
			this.gbxImport.Name = "gbxImport";
			this.gbxImport.Size = new System.Drawing.Size(200, 50);
			this.gbxImport.TabIndex = 5;
			this.gbxImport.TabStop = false;
			this.gbxImport.Text = "Importation";
			// 
			// btnImportOpen
			// 
			this.btnImportOpen.Location = new System.Drawing.Point(6, 18);
			this.btnImportOpen.Name = "btnImportOpen";
			this.btnImportOpen.Size = new System.Drawing.Size(181, 23);
			this.btnImportOpen.TabIndex = 3;
			this.btnImportOpen.Text = "Ouvrir le répertoire...";
			this.btnImportOpen.UseVisualStyleBackColor = true;
			this.btnImportOpen.Click += new System.EventHandler(this.BtnImportOpenClick);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(418, 392);
			this.Controls.Add(this.gbxImport);
			this.Controls.Add(this.gbxMessage);
			this.Controls.Add(this.gbxOption);
			this.Controls.Add(this.gbxExport);
			this.Controls.Add(this.btnQuit);
			this.Controls.Add(this.gbxDsDsrp);
			this.Controls.Add(this.gbxCustomer);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MaximizeBox = false;
			this.Name = "MainForm";
			this.Text = "VMI";
			this.gbxCustomer.ResumeLayout(false);
			this.gbxDsDsrp.ResumeLayout(false);
			this.gbxExport.ResumeLayout(false);
			this.gbxOption.ResumeLayout(false);
			this.gbxMessage.ResumeLayout(false);
			this.gbxImport.ResumeLayout(false);
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Button btnImportOpen;
		private System.Windows.Forms.GroupBox gbxImport;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Button btnDsStart;
		private System.Windows.Forms.GroupBox gbxMessage;
		private System.Windows.Forms.ComboBox cbxCustomer;
		private System.Windows.Forms.Label lblCustomer;
		private System.Windows.Forms.GroupBox gbxExport;
		private System.Windows.Forms.Button btnDsrpStart;
		private System.Windows.Forms.Button btnDsrpInit;
		private System.Windows.Forms.GroupBox gbxDsDsrp;
		private System.Windows.Forms.ListBox lbxExport;
		private System.Windows.Forms.GroupBox gbxOption;
		private System.Windows.Forms.GroupBox gbxCustomer;
		private System.Windows.Forms.Button btnOption;
		private System.Windows.Forms.Label lblMessage;
		private System.Windows.Forms.Button btnQuit;
	}
}
