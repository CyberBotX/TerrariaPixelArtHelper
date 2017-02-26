namespace TerrariaPixelArtHelper
{
	partial class FindClosestColor
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
			this.btnOK = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.pnlOriginalColor = new System.Windows.Forms.Panel();
			this.lblOriginalColor = new System.Windows.Forms.Label();
			this.pnlSelectedColor = new System.Windows.Forms.Panel();
			this.lblSelectedColor = new System.Windows.Forms.Label();
			this.lbColors = new System.Windows.Forms.ListBox();
			this.pnlWall = new System.Windows.Forms.Panel();
			this.lblOriginal = new System.Windows.Forms.Label();
			this.lblSelected = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnOK
			// 
			this.btnOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(125, 191);
			this.btnOK.Name = "btnOK";
			this.btnOK.Size = new System.Drawing.Size(75, 23);
			this.btnOK.TabIndex = 0;
			this.btnOK.Text = "OK";
			this.btnOK.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(206, 191);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 1;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// pnlOriginalColor
			// 
			this.pnlOriginalColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlOriginalColor.Location = new System.Drawing.Point(22, 25);
			this.pnlOriginalColor.Name = "pnlOriginalColor";
			this.pnlOriginalColor.Size = new System.Drawing.Size(75, 50);
			this.pnlOriginalColor.TabIndex = 3;
			// 
			// lblOriginalColor
			// 
			this.lblOriginalColor.AutoSize = true;
			this.lblOriginalColor.Location = new System.Drawing.Point(8, 78);
			this.lblOriginalColor.MaximumSize = new System.Drawing.Size(103, 0);
			this.lblOriginalColor.MinimumSize = new System.Drawing.Size(103, 0);
			this.lblOriginalColor.Name = "lblOriginalColor";
			this.lblOriginalColor.Size = new System.Drawing.Size(103, 13);
			this.lblOriginalColor.TabIndex = 2;
			this.lblOriginalColor.Text = "(R, G, B, A)";
			this.lblOriginalColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlSelectedColor
			// 
			this.pnlSelectedColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlSelectedColor.Location = new System.Drawing.Point(22, 112);
			this.pnlSelectedColor.Name = "pnlSelectedColor";
			this.pnlSelectedColor.Size = new System.Drawing.Size(75, 50);
			this.pnlSelectedColor.TabIndex = 5;
			// 
			// lblSelectedColor
			// 
			this.lblSelectedColor.AutoSize = true;
			this.lblSelectedColor.Location = new System.Drawing.Point(8, 165);
			this.lblSelectedColor.MaximumSize = new System.Drawing.Size(103, 0);
			this.lblSelectedColor.MinimumSize = new System.Drawing.Size(103, 0);
			this.lblSelectedColor.Name = "lblSelectedColor";
			this.lblSelectedColor.Size = new System.Drawing.Size(103, 13);
			this.lblSelectedColor.TabIndex = 4;
			this.lblSelectedColor.Text = "(R, G, B, A)";
			this.lblSelectedColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbColors
			// 
			this.lbColors.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lbColors.DisplayMember = "DisplayMember";
			this.lbColors.FormattingEnabled = true;
			this.lbColors.Location = new System.Drawing.Point(125, 12);
			this.lbColors.Name = "lbColors";
			this.lbColors.Size = new System.Drawing.Size(156, 173);
			this.lbColors.TabIndex = 6;
			this.lbColors.SelectedIndexChanged += new System.EventHandler(this.lbColors_SelectedIndexChanged);
			// 
			// pnlWall
			// 
			this.pnlWall.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlWall.Location = new System.Drawing.Point(4, 112);
			this.pnlWall.Name = "pnlWall";
			this.pnlWall.Size = new System.Drawing.Size(16, 16);
			this.pnlWall.TabIndex = 7;
			// 
			// lblOriginal
			// 
			this.lblOriginal.AutoSize = true;
			this.lblOriginal.Location = new System.Drawing.Point(37, 9);
			this.lblOriginal.Name = "lblOriginal";
			this.lblOriginal.Size = new System.Drawing.Size(45, 13);
			this.lblOriginal.TabIndex = 8;
			this.lblOriginal.Text = "Original:";
			// 
			// lblSelected
			// 
			this.lblSelected.AutoSize = true;
			this.lblSelected.Location = new System.Drawing.Point(33, 96);
			this.lblSelected.Name = "lblSelected";
			this.lblSelected.Size = new System.Drawing.Size(52, 13);
			this.lblSelected.TabIndex = 9;
			this.lblSelected.Text = "Selected:";
			// 
			// FindClosestColor
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(293, 226);
			this.Controls.Add(this.lblSelected);
			this.Controls.Add(this.lblOriginal);
			this.Controls.Add(this.pnlWall);
			this.Controls.Add(this.lbColors);
			this.Controls.Add(this.pnlSelectedColor);
			this.Controls.Add(this.lblSelectedColor);
			this.Controls.Add(this.pnlOriginalColor);
			this.Controls.Add(this.lblOriginalColor);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.DoubleBuffered = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FindClosestColor";
			this.ShowInTaskbar = false;
			this.Text = "Find Closest Color";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Panel pnlOriginalColor;
		private System.Windows.Forms.Label lblOriginalColor;
		private System.Windows.Forms.Panel pnlSelectedColor;
		private System.Windows.Forms.Label lblSelectedColor;
		private System.Windows.Forms.ListBox lbColors;
		private System.Windows.Forms.Panel pnlWall;
		private System.Windows.Forms.Label lblOriginal;
		private System.Windows.Forms.Label lblSelected;
	}
}
