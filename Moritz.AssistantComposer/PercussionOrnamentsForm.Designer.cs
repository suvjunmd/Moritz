

namespace Moritz.AssistantComposer
{
    partial class PercussionOrnamentsForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private global::System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if(disposing)
			{
				if(components != null)
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PercussionOrnamentsForm));
            this.OrnamentKrystalLabel = new System.Windows.Forms.Label();
            this.OrnamentKrystalNameLabel = new System.Windows.Forms.Label();
            this.GetOrnamentKrystalButton = new System.Windows.Forms.Button();
            this.ShowOrnamentKrystalStrandsButton = new System.Windows.Forms.Button();
            this.OrnamentsLevelTextBox = new System.Windows.Forms.TextBox();
            this.OrnamentsLevelLabel = new System.Windows.Forms.Label();
            this.OrnamentsLevelHelpLabel = new System.Windows.Forms.Label();
            this.ShowContainingPalletButton = new System.Windows.Forms.Button();
            this.ShowMainScoreFormButton = new System.Windows.Forms.Button();
            this.MainHelpLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // OrnamentKrystalLabel
            // 
            this.OrnamentKrystalLabel.AutoSize = true;
            this.OrnamentKrystalLabel.ForeColor = System.Drawing.Color.Brown;
            this.OrnamentKrystalLabel.Location = new System.Drawing.Point(77, 19);
            this.OrnamentKrystalLabel.Name = "OrnamentKrystalLabel";
            this.OrnamentKrystalLabel.Size = new System.Drawing.Size(87, 14);
            this.OrnamentKrystalLabel.TabIndex = 17;
            this.OrnamentKrystalLabel.Text = "ornament krystal";
            // 
            // OrnamentKrystalNameLabel
            // 
            this.OrnamentKrystalNameLabel.AutoSize = true;
            this.OrnamentKrystalNameLabel.BackColor = System.Drawing.Color.Transparent;
            this.OrnamentKrystalNameLabel.Location = new System.Drawing.Point(169, 19);
            this.OrnamentKrystalNameLabel.Name = "OrnamentKrystalNameLabel";
            this.OrnamentKrystalNameLabel.Size = new System.Drawing.Size(124, 14);
            this.OrnamentKrystalNameLabel.TabIndex = 31;
            this.OrnamentKrystalNameLabel.Text = "                                       ";
            // 
            // GetOrnamentKrystalButton
            // 
            this.GetOrnamentKrystalButton.Location = new System.Drawing.Point(341, 15);
            this.GetOrnamentKrystalButton.Name = "GetOrnamentKrystalButton";
            this.GetOrnamentKrystalButton.Size = new System.Drawing.Size(219, 22);
            this.GetOrnamentKrystalButton.TabIndex = 0;
            this.GetOrnamentKrystalButton.Text = "get ornament krystal from browser";
            this.GetOrnamentKrystalButton.UseVisualStyleBackColor = true;
            this.GetOrnamentKrystalButton.Click += new System.EventHandler(this.GetOrnamentKrystalButton_Click);
            // 
            // ShowOrnamentKrystalStrandsButton
            // 
            this.ShowOrnamentKrystalStrandsButton.Location = new System.Drawing.Point(568, 15);
            this.ShowOrnamentKrystalStrandsButton.Name = "ShowOrnamentKrystalStrandsButton";
            this.ShowOrnamentKrystalStrandsButton.Size = new System.Drawing.Size(97, 23);
            this.ShowOrnamentKrystalStrandsButton.TabIndex = 1;
            this.ShowOrnamentKrystalStrandsButton.Text = "show strands";
            this.ShowOrnamentKrystalStrandsButton.UseVisualStyleBackColor = true;
            this.ShowOrnamentKrystalStrandsButton.Click += new System.EventHandler(this.ShowOrnamentKrystalStrandsButton_Click);
            // 
            // OrnamentsLevelTextBox
            // 
            this.OrnamentsLevelTextBox.Location = new System.Drawing.Point(169, 45);
            this.OrnamentsLevelTextBox.Name = "OrnamentsLevelTextBox";
            this.OrnamentsLevelTextBox.Size = new System.Drawing.Size(26, 20);
            this.OrnamentsLevelTextBox.TabIndex = 2;
            this.OrnamentsLevelTextBox.TextChanged += new System.EventHandler(this.OrnamentsLevelTextBox_TextChanged);
            this.OrnamentsLevelTextBox.Leave += new System.EventHandler(this.OrnamentsLevelTextBox_Leave);
            // 
            // OrnamentsLevelLabel
            // 
            this.OrnamentsLevelLabel.AutoSize = true;
            this.OrnamentsLevelLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(60)))), ((int)(((byte)(60)))), ((int)(((byte)(60)))));
            this.OrnamentsLevelLabel.Location = new System.Drawing.Point(71, 48);
            this.OrnamentsLevelLabel.Name = "OrnamentsLevelLabel";
            this.OrnamentsLevelLabel.Size = new System.Drawing.Size(97, 14);
            this.OrnamentsLevelLabel.TabIndex = 18;
            this.OrnamentsLevelLabel.Text = "( ornaments level )";
            // 
            // OrnamentsLevelHelpLabel
            // 
            this.OrnamentsLevelHelpLabel.AutoSize = true;
            this.OrnamentsLevelHelpLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.OrnamentsLevelHelpLabel.Location = new System.Drawing.Point(198, 48);
            this.OrnamentsLevelHelpLabel.Name = "OrnamentsLevelHelpLabel";
            this.OrnamentsLevelHelpLabel.Size = new System.Drawing.Size(418, 14);
            this.OrnamentsLevelHelpLabel.TabIndex = 32;
            this.OrnamentsLevelHelpLabel.Text = "1 integer value (greater than 0, less than or equal to the level of the ornament " +
    "krystal)";
            // 
            // ShowContainingPalletButton
            // 
            this.ShowContainingPalletButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowContainingPalletButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(215)))));
            this.ShowContainingPalletButton.Font = new System.Drawing.Font("Arial", 8F);
            this.ShowContainingPalletButton.Location = new System.Drawing.Point(647, 206);
            this.ShowContainingPalletButton.Name = "ShowContainingPalletButton";
            this.ShowContainingPalletButton.Size = new System.Drawing.Size(157, 36);
            this.ShowContainingPalletButton.TabIndex = 3;
            this.ShowContainingPalletButton.Text = "show containing palette";
            this.ShowContainingPalletButton.UseVisualStyleBackColor = false;
            this.ShowContainingPalletButton.Click += new System.EventHandler(this.ShowContainingPaletteButton_Click);
            // 
            // ShowMainScoreFormButton
            // 
            this.ShowMainScoreFormButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.ShowMainScoreFormButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(225)))), ((int)(((byte)(215)))));
            this.ShowMainScoreFormButton.Font = new System.Drawing.Font("Arial", 8F);
            this.ShowMainScoreFormButton.Location = new System.Drawing.Point(647, 250);
            this.ShowMainScoreFormButton.Name = "ShowMainScoreFormButton";
            this.ShowMainScoreFormButton.Size = new System.Drawing.Size(157, 36);
            this.ShowMainScoreFormButton.TabIndex = 4;
            this.ShowMainScoreFormButton.Text = "show main score form";
            this.ShowMainScoreFormButton.UseVisualStyleBackColor = false;
            this.ShowMainScoreFormButton.Click += new System.EventHandler(this.ShowMainScoreFormButton_Click);
            // 
            // MainHelpLabel
            // 
            this.MainHelpLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.MainHelpLabel.AutoSize = true;
            this.MainHelpLabel.Font = new System.Drawing.Font("Arial", 8F);
            this.MainHelpLabel.ForeColor = System.Drawing.Color.RoyalBlue;
            this.MainHelpLabel.Location = new System.Drawing.Point(23, 206);
            this.MainHelpLabel.Name = "MainHelpLabel";
            this.MainHelpLabel.Size = new System.Drawing.Size(589, 126);
            this.MainHelpLabel.TabIndex = 46;
            this.MainHelpLabel.Text = resources.GetString("MainHelpLabel.Text");
            // 
            // PercussionOrnamentsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(255)))), ((int)(((byte)(245)))));
            this.ClientSize = new System.Drawing.Size(832, 347);
            this.ControlBox = false;
            this.Controls.Add(this.ShowMainScoreFormButton);
            this.Controls.Add(this.ShowContainingPalletButton);
            this.Controls.Add(this.OrnamentsLevelHelpLabel);
            this.Controls.Add(this.OrnamentsLevelTextBox);
            this.Controls.Add(this.ShowOrnamentKrystalStrandsButton);
            this.Controls.Add(this.GetOrnamentKrystalButton);
            this.Controls.Add(this.OrnamentKrystalNameLabel);
            this.Controls.Add(this.OrnamentKrystalLabel);
            this.Controls.Add(this.MainHelpLabel);
            this.Controls.Add(this.OrnamentsLevelLabel);
            this.Font = new System.Drawing.Font("Arial", 8F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Location = new System.Drawing.Point(280, 70);
            this.Name = "PercussionOrnamentsForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "  ";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.OrnamentSettingsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

        private System.Windows.Forms.Label OrnamentKrystalLabel;
        private System.Windows.Forms.Button GetOrnamentKrystalButton;
        private System.Windows.Forms.Button ShowOrnamentKrystalStrandsButton;

        private System.Windows.Forms.Label OrnamentsLevelLabel;
        private System.Windows.Forms.Label OrnamentsLevelHelpLabel;
        private System.Windows.Forms.Button ShowContainingPalletButton;
        private System.Windows.Forms.Button ShowMainScoreFormButton;
        private System.Windows.Forms.Label MainHelpLabel;
        public System.Windows.Forms.TextBox OrnamentsLevelTextBox;
        public System.Windows.Forms.Label OrnamentKrystalNameLabel;

    }
}

