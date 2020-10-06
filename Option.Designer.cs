/*
 * Created by SharpDevelop.
 * User: Benoit Le Guern
 * Date: 25/03/2007
 * Time: 18:58
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
namespace VMI
{
	partial class Option
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
			this.btnValidate = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.gbxOption = new System.Windows.Forms.GroupBox();
			this.btnCustomerDir = new System.Windows.Forms.Button();
			this.lblCustomerDir = new System.Windows.Forms.Label();
			this.tbxCustomerDir = new System.Windows.Forms.TextBox();
			this.btnDsDsrpDir = new System.Windows.Forms.Button();
			this.lblDsDsrpDir = new System.Windows.Forms.Label();
			this.tbxDsDsrpDir = new System.Windows.Forms.TextBox();
			this.fbdDsDsrpDir = new System.Windows.Forms.FolderBrowserDialog();
			this.fbdCustomerDir = new System.Windows.Forms.FolderBrowserDialog();
			this.lblUser = new System.Windows.Forms.Label();
			this.tbxUser = new System.Windows.Forms.TextBox();
			this.gbxOption.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnValidate
			// 
			this.btnValidate.Location = new System.Drawing.Point(153, 186);
			this.btnValidate.Name = "btnValidate";
			this.btnValidate.Size = new System.Drawing.Size(105, 23);
			this.btnValidate.TabIndex = 0;
			this.btnValidate.Text = "Valider";
			this.btnValidate.UseVisualStyleBackColor = true;
			this.btnValidate.Click += new System.EventHandler(this.BtnValidateClick);
			// 
			// btnCancel
			// 
			this.btnCancel.Location = new System.Drawing.Point(264, 186);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(105, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Annuler";
			this.btnCancel.UseVisualStyleBackColor = true;
			this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
			// 
			// gbxOption
			// 
			this.gbxOption.Controls.Add(this.tbxUser);
			this.gbxOption.Controls.Add(this.lblUser);
			this.gbxOption.Controls.Add(this.btnCustomerDir);
			this.gbxOption.Controls.Add(this.lblCustomerDir);
			this.gbxOption.Controls.Add(this.tbxCustomerDir);
			this.gbxOption.Controls.Add(this.btnDsDsrpDir);
			this.gbxOption.Controls.Add(this.lblDsDsrpDir);
			this.gbxOption.Controls.Add(this.tbxDsDsrpDir);
			this.gbxOption.Location = new System.Drawing.Point(12, 12);
			this.gbxOption.Name = "gbxOption";
			this.gbxOption.Size = new System.Drawing.Size(357, 168);
			this.gbxOption.TabIndex = 2;
			this.gbxOption.TabStop = false;
			this.gbxOption.Text = "Liste des options";
			// 
			// btnCustomerDir
			// 
			this.btnCustomerDir.Location = new System.Drawing.Point(270, 87);
			this.btnCustomerDir.Name = "btnCustomerDir";
			this.btnCustomerDir.Size = new System.Drawing.Size(81, 23);
			this.btnCustomerDir.TabIndex = 5;
			this.btnCustomerDir.Text = "...";
			this.btnCustomerDir.UseVisualStyleBackColor = true;
			this.btnCustomerDir.Click += new System.EventHandler(this.BtnCustomerDirClick);
			// 
			// lblCustomerDir
			// 
			this.lblCustomerDir.Location = new System.Drawing.Point(7, 69);
			this.lblCustomerDir.Name = "lblCustomerDir";
			this.lblCustomerDir.Size = new System.Drawing.Size(178, 17);
			this.lblCustomerDir.TabIndex = 4;
			this.lblCustomerDir.Text = "Répertoire des bases client :";
			// 
			// tbxCustomerDir
			// 
			this.tbxCustomerDir.Location = new System.Drawing.Point(6, 89);
			this.tbxCustomerDir.Name = "tbxCustomerDir";
			this.tbxCustomerDir.ReadOnly = true;
			this.tbxCustomerDir.Size = new System.Drawing.Size(258, 20);
			this.tbxCustomerDir.TabIndex = 3;
			// 
			// btnDsDsrpDir
			// 
			this.btnDsDsrpDir.Location = new System.Drawing.Point(270, 40);
			this.btnDsDsrpDir.Name = "btnDsDsrpDir";
			this.btnDsDsrpDir.Size = new System.Drawing.Size(81, 23);
			this.btnDsDsrpDir.TabIndex = 2;
			this.btnDsDsrpDir.Text = "...";
			this.btnDsDsrpDir.UseVisualStyleBackColor = true;
			this.btnDsDsrpDir.Click += new System.EventHandler(this.BtnDsDsrpDirClick);
			// 
			// lblDsDsrpDir
			// 
			this.lblDsDsrpDir.Location = new System.Drawing.Point(7, 20);
			this.lblDsDsrpDir.Name = "lblDsDsrpDir";
			this.lblDsDsrpDir.Size = new System.Drawing.Size(178, 21);
			this.lblDsDsrpDir.TabIndex = 1;
			this.lblDsDsrpDir.Text = "Répertoire de DS/DSRP :";
			// 
			// tbxDsDsrpDir
			// 
			this.tbxDsDsrpDir.Location = new System.Drawing.Point(6, 41);
			this.tbxDsDsrpDir.Name = "tbxDsDsrpDir";
			this.tbxDsDsrpDir.ReadOnly = true;
			this.tbxDsDsrpDir.Size = new System.Drawing.Size(258, 20);
			this.tbxDsDsrpDir.TabIndex = 0;
			// 
			// lblUser
			// 
			this.lblUser.Location = new System.Drawing.Point(7, 117);
			this.lblUser.Name = "lblUser";
			this.lblUser.Size = new System.Drawing.Size(178, 17);
			this.lblUser.TabIndex = 6;
			this.lblUser.Text = "Nom d\'utilisateur :";
			// 
			// tbxUser
			// 
			this.tbxUser.Location = new System.Drawing.Point(6, 137);
			this.tbxUser.MaxLength = 10;
			this.tbxUser.Name = "tbxUser";
			this.tbxUser.Size = new System.Drawing.Size(345, 20);
			this.tbxUser.TabIndex = 7;
			// 
			// Option
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(381, 219);
			this.ControlBox = false;
			this.Controls.Add(this.gbxOption);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnValidate);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "Option";
			this.Text = "Options...";
			this.gbxOption.ResumeLayout(false);
			this.gbxOption.PerformLayout();
			this.ResumeLayout(false);
		}
		private System.Windows.Forms.Label lblUser;
		private System.Windows.Forms.TextBox tbxUser;
		private System.Windows.Forms.FolderBrowserDialog fbdCustomerDir;
		private System.Windows.Forms.FolderBrowserDialog fbdDsDsrpDir;
		private System.Windows.Forms.TextBox tbxDsDsrpDir;
		private System.Windows.Forms.Label lblDsDsrpDir;
		private System.Windows.Forms.Button btnDsDsrpDir;
		private System.Windows.Forms.TextBox tbxCustomerDir;
		private System.Windows.Forms.Label lblCustomerDir;
		private System.Windows.Forms.Button btnCustomerDir;
		private System.Windows.Forms.GroupBox gbxOption;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnValidate;
	}
}
