namespace TerrariaPixelArtHelper
{
	partial class ColorToWall
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

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblColor = new System.Windows.Forms.Label();
			this.pnlColor = new System.Windows.Forms.Panel();
			this.cbWallColor = new System.Windows.Forms.ComboBox();
			this.btnFindClosestColor = new System.Windows.Forms.Button();
			this.btnReset = new System.Windows.Forms.Button();
			this.lblNumberOfPixels = new System.Windows.Forms.Label();
			this.cbWall = new TerrariaPixelArtHelper.WallSelector();
			this.SuspendLayout();
			// 
			// lblColor
			// 
			this.lblColor.AutoSize = true;
			this.lblColor.Location = new System.Drawing.Point(4, 56);
			this.lblColor.MaximumSize = new System.Drawing.Size(103, 0);
			this.lblColor.MinimumSize = new System.Drawing.Size(103, 0);
			this.lblColor.Name = "lblColor";
			this.lblColor.Size = new System.Drawing.Size(103, 13);
			this.lblColor.TabIndex = 0;
			this.lblColor.Text = "(R, G, B, A)";
			this.lblColor.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlColor
			// 
			this.pnlColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlColor.Location = new System.Drawing.Point(18, 3);
			this.pnlColor.Name = "pnlColor";
			this.pnlColor.Size = new System.Drawing.Size(75, 50);
			this.pnlColor.TabIndex = 1;
			// 
			// cbWallColor
			// 
			this.cbWallColor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbWallColor.Enabled = false;
			this.cbWallColor.FormattingEnabled = true;
			this.cbWallColor.Location = new System.Drawing.Point(99, 31);
			this.cbWallColor.Name = "cbWallColor";
			this.cbWallColor.Size = new System.Drawing.Size(175, 21);
			this.cbWallColor.TabIndex = 3;
			this.cbWallColor.SelectedIndexChanged += new System.EventHandler(this.cbWallColor_SelectedIndexChanged);
			// 
			// btnFindClosestColor
			// 
			this.btnFindClosestColor.Location = new System.Drawing.Point(280, 3);
			this.btnFindClosestColor.Name = "btnFindClosestColor";
			this.btnFindClosestColor.Size = new System.Drawing.Size(61, 47);
			this.btnFindClosestColor.TabIndex = 4;
			this.btnFindClosestColor.Text = "Find Closest Color";
			this.btnFindClosestColor.UseVisualStyleBackColor = true;
			this.btnFindClosestColor.Click += new System.EventHandler(this.btnFindClosestColor_Click);
			// 
			// btnReset
			// 
			this.btnReset.Location = new System.Drawing.Point(280, 56);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(61, 23);
			this.btnReset.TabIndex = 5;
			this.btnReset.Text = "Reset";
			this.btnReset.UseVisualStyleBackColor = true;
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// lblNumberOfPixels
			// 
			this.lblNumberOfPixels.AutoSize = true;
			this.lblNumberOfPixels.Location = new System.Drawing.Point(113, 56);
			this.lblNumberOfPixels.Name = "lblNumberOfPixels";
			this.lblNumberOfPixels.Size = new System.Drawing.Size(113, 13);
			this.lblNumberOfPixels.TabIndex = 6;
			this.lblNumberOfPixels.Text = "Number of Pixels: XXX";
			// 
			// cbWall
			// 
			this.cbWall.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawVariable;
			this.cbWall.DropDownHeight = 200;
			this.cbWall.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cbWall.FormattingEnabled = true;
			this.cbWall.IntegralHeight = false;
			this.cbWall.Location = new System.Drawing.Point(99, 4);
			this.cbWall.Name = "cbWall";
			this.cbWall.Size = new System.Drawing.Size(175, 21);
			this.cbWall.TabIndex = 2;
			this.cbWall.SelectedIndexChanged += new System.EventHandler(this.cbWall_SelectedIndexChanged);
			// 
			// ColorToWall
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.Controls.Add(this.lblNumberOfPixels);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.btnFindClosestColor);
			this.Controls.Add(this.cbWallColor);
			this.Controls.Add(this.cbWall);
			this.Controls.Add(this.pnlColor);
			this.Controls.Add(this.lblColor);
			this.Name = "ColorToWall";
			this.Size = new System.Drawing.Size(347, 84);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblColor;
		private System.Windows.Forms.Panel pnlColor;
		private WallSelector cbWall;
		private System.Windows.Forms.ComboBox cbWallColor;
		private System.Windows.Forms.Button btnFindClosestColor;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Label lblNumberOfPixels;
	}
}
