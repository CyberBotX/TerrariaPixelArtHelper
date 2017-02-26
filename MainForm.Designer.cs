namespace TerrariaPixelArtHelper
{
	partial class MainForm
	{

		#region Windows Form Designer generated code
		private void InitializeComponent()
		{
			this.pnlInGame = new System.Windows.Forms.Panel();
			this.pnlMap = new System.Windows.Forms.Panel();
			this.flpColorToWall = new System.Windows.Forms.FlowLayoutPanel();
			this.MenuStrip = new System.Windows.Forms.MenuStrip();
			this.miFile = new System.Windows.Forms.ToolStripMenuItem();
			this.miOpenFromFile = new System.Windows.Forms.ToolStripMenuItem();
			this.miOpenFromClipboard = new System.Windows.Forms.ToolStripMenuItem();
			this.MenuSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.miExit = new System.Windows.Forms.ToolStripMenuItem();
			this.miView = new System.Windows.Forms.ToolStripMenuItem();
			this.miToggleGrid = new System.Windows.Forms.ToolStripMenuItem();
			this.miHelp = new System.Windows.Forms.ToolStripMenuItem();
			this.miAbout = new System.Windows.Forms.ToolStripMenuItem();
			this.StatusStrip = new System.Windows.Forms.StatusStrip();
			this.tsslImageDimensions = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslPipe = new System.Windows.Forms.ToolStripStatusLabel();
			this.tsslCurrentPixel = new System.Windows.Forms.ToolStripStatusLabel();
			this.tbWalls = new System.Windows.Forms.TextBox();
			this.tlpImages = new System.Windows.Forms.TableLayoutPanel();
			this.lblInGame = new System.Windows.Forms.Label();
			this.lblMap = new System.Windows.Forms.Label();
			this.tbColors = new System.Windows.Forms.TextBox();
			this.lblToMake = new System.Windows.Forms.Label();
			this.lblWalls = new System.Windows.Forms.Label();
			this.lblColors = new System.Windows.Forms.Label();
			this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.scImages = new System.Windows.Forms.SplitContainer();
			this.tlpNeeded = new System.Windows.Forms.TableLayoutPanel();
			this.pbInGameGrid = new System.Windows.Forms.PictureBox();
			this.pbInGame = new System.Windows.Forms.PictureBox();
			this.pbMapGrid = new System.Windows.Forms.PictureBox();
			this.pbMap = new System.Windows.Forms.PictureBox();
			this.pnlInGame.SuspendLayout();
			this.pnlMap.SuspendLayout();
			this.MenuStrip.SuspendLayout();
			this.StatusStrip.SuspendLayout();
			this.tlpImages.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.scImages)).BeginInit();
			this.scImages.Panel1.SuspendLayout();
			this.scImages.Panel2.SuspendLayout();
			this.scImages.SuspendLayout();
			this.tlpNeeded.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbInGameGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbInGame)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbMapGrid)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.pbMap)).BeginInit();
			this.SuspendLayout();
			// 
			// pnlInGame
			// 
			this.pnlInGame.AutoScroll = true;
			this.pnlInGame.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlInGame.Controls.Add(this.pbInGameGrid);
			this.pnlInGame.Controls.Add(this.pbInGame);
			this.pnlInGame.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlInGame.Location = new System.Drawing.Point(3, 16);
			this.pnlInGame.Name = "pnlInGame";
			this.pnlInGame.Size = new System.Drawing.Size(341, 317);
			this.pnlInGame.TabIndex = 1;
			// 
			// pnlMap
			// 
			this.pnlMap.AutoScroll = true;
			this.pnlMap.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlMap.Controls.Add(this.pbMapGrid);
			this.pnlMap.Controls.Add(this.pbMap);
			this.pnlMap.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlMap.Location = new System.Drawing.Point(350, 16);
			this.pnlMap.Name = "pnlMap";
			this.pnlMap.Size = new System.Drawing.Size(341, 317);
			this.pnlMap.TabIndex = 2;
			// 
			// flpColorToWall
			// 
			this.flpColorToWall.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
			this.flpColorToWall.AutoScroll = true;
			this.flpColorToWall.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.flpColorToWall.Location = new System.Drawing.Point(12, 27);
			this.flpColorToWall.Name = "flpColorToWall";
			this.flpColorToWall.Size = new System.Drawing.Size(374, 467);
			this.flpColorToWall.TabIndex = 3;
			// 
			// MenuStrip
			// 
			this.MenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile,
            this.miView,
            this.miHelp});
			this.MenuStrip.Location = new System.Drawing.Point(0, 0);
			this.MenuStrip.Name = "MenuStrip";
			this.MenuStrip.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
			this.MenuStrip.Size = new System.Drawing.Size(1098, 24);
			this.MenuStrip.TabIndex = 4;
			// 
			// miFile
			// 
			this.miFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miOpenFromFile,
            this.miOpenFromClipboard,
            this.MenuSeparator1,
            this.miExit});
			this.miFile.Name = "miFile";
			this.miFile.Size = new System.Drawing.Size(37, 20);
			this.miFile.Text = "&File";
			// 
			// miOpenFromFile
			// 
			this.miOpenFromFile.Name = "miOpenFromFile";
			this.miOpenFromFile.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
			this.miOpenFromFile.Size = new System.Drawing.Size(228, 22);
			this.miOpenFromFile.Text = "&Open from File...";
			this.miOpenFromFile.Click += new System.EventHandler(this.miOpenFromFile_Click);
			// 
			// miOpenFromClipboard
			// 
			this.miOpenFromClipboard.Name = "miOpenFromClipboard";
			this.miOpenFromClipboard.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
			this.miOpenFromClipboard.Size = new System.Drawing.Size(228, 22);
			this.miOpenFromClipboard.Text = "Open from &Clipboard";
			this.miOpenFromClipboard.Click += new System.EventHandler(this.miOpenFromClipboard_Click);
			// 
			// MenuSeparator1
			// 
			this.MenuSeparator1.Name = "MenuSeparator1";
			this.MenuSeparator1.Size = new System.Drawing.Size(225, 6);
			// 
			// miExit
			// 
			this.miExit.Name = "miExit";
			this.miExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
			this.miExit.Size = new System.Drawing.Size(228, 22);
			this.miExit.Text = "E&xit";
			this.miExit.Click += new System.EventHandler(this.miExit_Click);
			// 
			// miView
			// 
			this.miView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miToggleGrid});
			this.miView.Name = "miView";
			this.miView.Size = new System.Drawing.Size(44, 20);
			this.miView.Text = "&View";
			// 
			// miToggleGrid
			// 
			this.miToggleGrid.Checked = true;
			this.miToggleGrid.CheckState = System.Windows.Forms.CheckState.Checked;
			this.miToggleGrid.Name = "miToggleGrid";
			this.miToggleGrid.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.G)));
			this.miToggleGrid.Size = new System.Drawing.Size(177, 22);
			this.miToggleGrid.Text = "Toggle &Grid";
			this.miToggleGrid.Click += new System.EventHandler(this.miToggleGrid_Click);
			// 
			// miHelp
			// 
			this.miHelp.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miAbout});
			this.miHelp.Name = "miHelp";
			this.miHelp.Size = new System.Drawing.Size(44, 20);
			this.miHelp.Text = "&Help";
			// 
			// miAbout
			// 
			this.miAbout.Name = "miAbout";
			this.miAbout.Size = new System.Drawing.Size(107, 22);
			this.miAbout.Text = "&About";
			this.miAbout.Click += new System.EventHandler(this.miAbout_Click);
			// 
			// StatusStrip
			// 
			this.StatusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsslImageDimensions,
            this.tsslPipe,
            this.tsslCurrentPixel});
			this.StatusStrip.Location = new System.Drawing.Point(0, 496);
			this.StatusStrip.Name = "StatusStrip";
			this.StatusStrip.Size = new System.Drawing.Size(1098, 22);
			this.StatusStrip.TabIndex = 5;
			// 
			// tsslImageDimensions
			// 
			this.tsslImageDimensions.Name = "tsslImageDimensions";
			this.tsslImageDimensions.Size = new System.Drawing.Size(0, 17);
			// 
			// tsslPipe
			// 
			this.tsslPipe.Name = "tsslPipe";
			this.tsslPipe.Size = new System.Drawing.Size(10, 17);
			this.tsslPipe.Text = "|";
			this.tsslPipe.Visible = false;
			// 
			// tsslCurrentPixel
			// 
			this.tsslCurrentPixel.Name = "tsslCurrentPixel";
			this.tsslCurrentPixel.Size = new System.Drawing.Size(0, 17);
			// 
			// tbWalls
			// 
			this.tbWalls.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbWalls.Location = new System.Drawing.Point(3, 29);
			this.tbWalls.Multiline = true;
			this.tbWalls.Name = "tbWalls";
			this.tbWalls.ReadOnly = true;
			this.tbWalls.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbWalls.Size = new System.Drawing.Size(341, 94);
			this.tbWalls.TabIndex = 6;
			this.tbWalls.TabStop = false;
			// 
			// tlpImages
			// 
			this.tlpImages.ColumnCount = 2;
			this.tlpImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpImages.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpImages.Controls.Add(this.pnlInGame, 0, 1);
			this.tlpImages.Controls.Add(this.pnlMap, 1, 1);
			this.tlpImages.Controls.Add(this.lblInGame, 0, 0);
			this.tlpImages.Controls.Add(this.lblMap, 1, 0);
			this.tlpImages.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpImages.Location = new System.Drawing.Point(0, 0);
			this.tlpImages.Name = "tlpImages";
			this.tlpImages.RowCount = 2;
			this.tlpImages.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpImages.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tlpImages.Size = new System.Drawing.Size(694, 336);
			this.tlpImages.TabIndex = 7;
			// 
			// lblInGame
			// 
			this.lblInGame.AutoSize = true;
			this.lblInGame.Location = new System.Drawing.Point(3, 0);
			this.lblInGame.Name = "lblInGame";
			this.lblInGame.Size = new System.Drawing.Size(50, 13);
			this.lblInGame.TabIndex = 3;
			this.lblInGame.Text = "In-Game:";
			// 
			// lblMap
			// 
			this.lblMap.AutoSize = true;
			this.lblMap.Location = new System.Drawing.Point(350, 0);
			this.lblMap.Name = "lblMap";
			this.lblMap.Size = new System.Drawing.Size(31, 13);
			this.lblMap.TabIndex = 4;
			this.lblMap.Text = "Map:";
			// 
			// tbColors
			// 
			this.tbColors.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tbColors.Location = new System.Drawing.Point(350, 29);
			this.tbColors.Multiline = true;
			this.tbColors.Name = "tbColors";
			this.tbColors.ReadOnly = true;
			this.tbColors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.tbColors.Size = new System.Drawing.Size(341, 94);
			this.tbColors.TabIndex = 10;
			this.tbColors.TabStop = false;
			// 
			// lblToMake
			// 
			this.lblToMake.AutoSize = true;
			this.tlpNeeded.SetColumnSpan(this.lblToMake, 2);
			this.lblToMake.Location = new System.Drawing.Point(3, 0);
			this.lblToMake.Name = "lblToMake";
			this.lblToMake.Size = new System.Drawing.Size(159, 13);
			this.lblToMake.TabIndex = 7;
			this.lblToMake.Text = "To make the above, you\'ll need:";
			// 
			// lblWalls
			// 
			this.lblWalls.AutoSize = true;
			this.lblWalls.Location = new System.Drawing.Point(3, 13);
			this.lblWalls.Name = "lblWalls";
			this.lblWalls.Size = new System.Drawing.Size(36, 13);
			this.lblWalls.TabIndex = 8;
			this.lblWalls.Text = "Walls:";
			// 
			// lblColors
			// 
			this.lblColors.AutoSize = true;
			this.lblColors.Location = new System.Drawing.Point(350, 13);
			this.lblColors.Name = "lblColors";
			this.lblColors.Size = new System.Drawing.Size(39, 13);
			this.lblColors.TabIndex = 9;
			this.lblColors.Text = "Colors:";
			// 
			// OpenFileDialog
			// 
			this.OpenFileDialog.ReadOnlyChecked = true;
			this.OpenFileDialog.SupportMultiDottedExtensions = true;
			// 
			// scImages
			// 
			this.scImages.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.scImages.Location = new System.Drawing.Point(392, 27);
			this.scImages.Name = "scImages";
			this.scImages.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// scImages.Panel1
			// 
			this.scImages.Panel1.Controls.Add(this.tlpImages);
			// 
			// scImages.Panel2
			// 
			this.scImages.Panel2.Controls.Add(this.tlpNeeded);
			this.scImages.Size = new System.Drawing.Size(694, 466);
			this.scImages.SplitterDistance = 336;
			this.scImages.TabIndex = 8;
			// 
			// tlpNeeded
			// 
			this.tlpNeeded.ColumnCount = 2;
			this.tlpNeeded.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpNeeded.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tlpNeeded.Controls.Add(this.tbColors, 1, 2);
			this.tlpNeeded.Controls.Add(this.lblToMake, 0, 0);
			this.tlpNeeded.Controls.Add(this.lblWalls, 0, 1);
			this.tlpNeeded.Controls.Add(this.tbWalls, 0, 2);
			this.tlpNeeded.Controls.Add(this.lblColors, 1, 1);
			this.tlpNeeded.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tlpNeeded.Location = new System.Drawing.Point(0, 0);
			this.tlpNeeded.Name = "tlpNeeded";
			this.tlpNeeded.RowCount = 3;
			this.tlpNeeded.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNeeded.RowStyles.Add(new System.Windows.Forms.RowStyle());
			this.tlpNeeded.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tlpNeeded.Size = new System.Drawing.Size(694, 126);
			this.tlpNeeded.TabIndex = 0;
			// 
			// pbInGameGrid
			// 
			this.pbInGameGrid.BackColor = System.Drawing.Color.Transparent;
			this.pbInGameGrid.Enabled = false;
			this.pbInGameGrid.Location = new System.Drawing.Point(0, 0);
			this.pbInGameGrid.Name = "pbInGameGrid";
			this.pbInGameGrid.Size = new System.Drawing.Size(0, 0);
			this.pbInGameGrid.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbInGameGrid.TabIndex = 1;
			this.pbInGameGrid.TabStop = false;
			// 
			// pbInGame
			// 
			this.pbInGame.Location = new System.Drawing.Point(0, 0);
			this.pbInGame.Name = "pbInGame";
			this.pbInGame.Size = new System.Drawing.Size(0, 0);
			this.pbInGame.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbInGame.TabIndex = 0;
			this.pbInGame.TabStop = false;
			this.pbInGame.Click += new System.EventHandler(this.pictureBox_Click);
			this.pbInGame.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
			this.pbInGame.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
			this.pbInGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picutreBox_MouseMove);
			// 
			// pbMapGrid
			// 
			this.pbMapGrid.BackColor = System.Drawing.Color.Transparent;
			this.pbMapGrid.Enabled = false;
			this.pbMapGrid.Location = new System.Drawing.Point(0, 0);
			this.pbMapGrid.Name = "pbMapGrid";
			this.pbMapGrid.Size = new System.Drawing.Size(0, 0);
			this.pbMapGrid.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbMapGrid.TabIndex = 1;
			this.pbMapGrid.TabStop = false;
			// 
			// pbMap
			// 
			this.pbMap.Location = new System.Drawing.Point(0, 0);
			this.pbMap.Name = "pbMap";
			this.pbMap.Size = new System.Drawing.Size(0, 0);
			this.pbMap.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.pbMap.TabIndex = 0;
			this.pbMap.TabStop = false;
			this.pbMap.Click += new System.EventHandler(this.pictureBox_Click);
			this.pbMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox_Paint);
			this.pbMap.MouseLeave += new System.EventHandler(this.pictureBox_MouseLeave);
			this.pbMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.picutreBox_MouseMove);
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1098, 518);
			this.Controls.Add(this.scImages);
			this.Controls.Add(this.StatusStrip);
			this.Controls.Add(this.flpColorToWall);
			this.Controls.Add(this.MenuStrip);
			this.DoubleBuffered = true;
			this.MainMenuStrip = this.MenuStrip;
			this.Name = "MainForm";
			this.Text = "Terraria Pixel Art Helper";
			this.pnlInGame.ResumeLayout(false);
			this.pnlInGame.PerformLayout();
			this.pnlMap.ResumeLayout(false);
			this.pnlMap.PerformLayout();
			this.MenuStrip.ResumeLayout(false);
			this.MenuStrip.PerformLayout();
			this.StatusStrip.ResumeLayout(false);
			this.StatusStrip.PerformLayout();
			this.tlpImages.ResumeLayout(false);
			this.tlpImages.PerformLayout();
			this.scImages.Panel1.ResumeLayout(false);
			this.scImages.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.scImages)).EndInit();
			this.scImages.ResumeLayout(false);
			this.tlpNeeded.ResumeLayout(false);
			this.tlpNeeded.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.pbInGameGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbInGame)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbMapGrid)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.pbMap)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}
		#endregion
		private System.Windows.Forms.Panel pnlInGame;
		private System.Windows.Forms.PictureBox pbInGame;
		private System.Windows.Forms.Panel pnlMap;
		private System.Windows.Forms.PictureBox pbMap;
		private System.Windows.Forms.FlowLayoutPanel flpColorToWall;
		private System.Windows.Forms.MenuStrip MenuStrip;
		private System.Windows.Forms.ToolStripMenuItem miFile;
		private System.Windows.Forms.ToolStripMenuItem miOpenFromFile;
		private System.Windows.Forms.ToolStripMenuItem miExit;
		private System.Windows.Forms.ToolStripMenuItem miHelp;
		private System.Windows.Forms.ToolStripMenuItem miAbout;
		private System.Windows.Forms.ToolStripSeparator MenuSeparator1;
		private System.Windows.Forms.StatusStrip StatusStrip;
		private System.Windows.Forms.TextBox tbWalls;
		private System.Windows.Forms.TableLayoutPanel tlpImages;
		private System.Windows.Forms.OpenFileDialog OpenFileDialog;
		private System.Windows.Forms.ToolStripMenuItem miOpenFromClipboard;
		private System.Windows.Forms.ToolStripStatusLabel tsslCurrentPixel;
		private System.Windows.Forms.Label lblInGame;
		private System.Windows.Forms.Label lblMap;
		private System.Windows.Forms.TextBox tbColors;
		private System.Windows.Forms.Label lblToMake;
		private System.Windows.Forms.Label lblWalls;
		private System.Windows.Forms.Label lblColors;
		private System.Windows.Forms.PictureBox pbInGameGrid;
		private System.Windows.Forms.PictureBox pbMapGrid;
		private System.Windows.Forms.ToolStripMenuItem miView;
		private System.Windows.Forms.ToolStripMenuItem miToggleGrid;
		private System.Windows.Forms.ToolStripStatusLabel tsslImageDimensions;
		private System.Windows.Forms.ToolStripStatusLabel tsslPipe;
		private System.Windows.Forms.TableLayoutPanel tlpNeeded;
		private System.Windows.Forms.SplitContainer scImages;
	}
}

