namespace Krystals4Application
{
    partial class NewExpansionDialog
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
            this.SetExpanderButton = new System.Windows.Forms.Button();
            this.OKBtn = new System.Windows.Forms.Button();
            this.CancelBtn = new System.Windows.Forms.Button();
            this.SetDensityInputButton = new System.Windows.Forms.Button();
            this.SetInputValuesButton = new System.Windows.Forms.Button();
            this.ExpanderLabel = new System.Windows.Forms.Label();
            this.DensityInputLabel = new System.Windows.Forms.Label();
            this.InputValuesLabel = new System.Windows.Forms.Label();
            this.ExpanderFilenameLabel = new System.Windows.Forms.Label();
            this.DensityInputFilenameLabel = new System.Windows.Forms.Label();
            this.PointsInputFilenameLabel = new System.Windows.Forms.Label();
            this.CompulsoryTextlabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // SetExpanderButton
            // 
            this.SetExpanderButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SetExpanderButton.Location = new System.Drawing.Point(231, 128);
            this.SetExpanderButton.Name = "SetExpanderButton";
            this.SetExpanderButton.Size = new System.Drawing.Size(60, 23);
            this.SetExpanderButton.TabIndex = 3;
            this.SetExpanderButton.Text = "Set...";
            this.SetExpanderButton.UseVisualStyleBackColor = true;
            this.SetExpanderButton.Click += new System.EventHandler(this.SetExpanderButton_Click);
            // 
            // OKBtn
            // 
            this.OKBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OKBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.OKBtn.Location = new System.Drawing.Point(116, 180);
            this.OKBtn.Name = "OKBtn";
            this.OKBtn.Size = new System.Drawing.Size(75, 23);
            this.OKBtn.TabIndex = 5;
            this.OKBtn.Text = "OK";
            this.OKBtn.UseVisualStyleBackColor = true;
            this.OKBtn.Click += new System.EventHandler(this.OKBtn_Click);
            // 
            // CancelBtn
            // 
            this.CancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelBtn.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CancelBtn.Location = new System.Drawing.Point(216, 180);
            this.CancelBtn.Name = "CancelBtn";
            this.CancelBtn.Size = new System.Drawing.Size(75, 23);
            this.CancelBtn.TabIndex = 6;
            this.CancelBtn.Text = "Cancel";
            this.CancelBtn.UseVisualStyleBackColor = true;
            this.CancelBtn.Click += new System.EventHandler(this.CancelBtn_Click);
            // 
            // SetDensityInputButton
            // 
            this.SetDensityInputButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SetDensityInputButton.Location = new System.Drawing.Point(231, 27);
            this.SetDensityInputButton.Name = "SetDensityInputButton";
            this.SetDensityInputButton.Size = new System.Drawing.Size(60, 23);
            this.SetDensityInputButton.TabIndex = 1;
            this.SetDensityInputButton.Text = "Set...";
            this.SetDensityInputButton.UseVisualStyleBackColor = true;
            this.SetDensityInputButton.Click += new System.EventHandler(this.SetDensityInputButton_Click);
            // 
            // SetInputValuesButton
            // 
            this.SetInputValuesButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.SetInputValuesButton.Location = new System.Drawing.Point(231, 57);
            this.SetInputValuesButton.Name = "SetInputValuesButton";
            this.SetInputValuesButton.Size = new System.Drawing.Size(60, 23);
            this.SetInputValuesButton.TabIndex = 2;
            this.SetInputValuesButton.Text = "Set...";
            this.SetInputValuesButton.UseVisualStyleBackColor = true;
            this.SetInputValuesButton.Click += new System.EventHandler(this.SetPointsInputButton_Click);
            // 
            // ExpanderLabel
            // 
            this.ExpanderLabel.AutoSize = true;
            this.ExpanderLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExpanderLabel.Location = new System.Drawing.Point(26, 133);
            this.ExpanderLabel.Name = "ExpanderLabel";
            this.ExpanderLabel.Size = new System.Drawing.Size(55, 13);
            this.ExpanderLabel.TabIndex = 9;
            this.ExpanderLabel.Text = "Expander:";
            // 
            // DensityInputLabel
            // 
            this.DensityInputLabel.AutoSize = true;
            this.DensityInputLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DensityInputLabel.Location = new System.Drawing.Point(26, 32);
            this.DensityInputLabel.Name = "DensityInputLabel";
            this.DensityInputLabel.Size = new System.Drawing.Size(71, 13);
            this.DensityInputLabel.TabIndex = 11;
            this.DensityInputLabel.Text = "Density input:";
            // 
            // InputValuesLabel
            // 
            this.InputValuesLabel.AutoSize = true;
            this.InputValuesLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.InputValuesLabel.Location = new System.Drawing.Point(26, 62);
            this.InputValuesLabel.Name = "InputValuesLabel";
            this.InputValuesLabel.Size = new System.Drawing.Size(65, 13);
            this.InputValuesLabel.TabIndex = 12;
            this.InputValuesLabel.Text = "Points input:";
            // 
            // ExpanderFilenameLabel
            // 
            this.ExpanderFilenameLabel.AutoSize = true;
            this.ExpanderFilenameLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.ExpanderFilenameLabel.Location = new System.Drawing.Point(96, 133);
            this.ExpanderFilenameLabel.Name = "ExpanderFilenameLabel";
            this.ExpanderFilenameLabel.Size = new System.Drawing.Size(104, 13);
            this.ExpanderFilenameLabel.TabIndex = 13;
            this.ExpanderFilenameLabel.Text = "to be edited (default)";
            // 
            // DensityInputFilenameLabel
            // 
            this.DensityInputFilenameLabel.AutoSize = true;
            this.DensityInputFilenameLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.DensityInputFilenameLabel.Location = new System.Drawing.Point(96, 32);
            this.DensityInputFilenameLabel.Name = "DensityInputFilenameLabel";
            this.DensityInputFilenameLabel.Size = new System.Drawing.Size(77, 13);
            this.DensityInputFilenameLabel.TabIndex = 15;
            this.DensityInputFilenameLabel.Text = "<unassigned>*";
            // 
            // PointsInputFilenameLabel
            // 
            this.PointsInputFilenameLabel.AutoSize = true;
            this.PointsInputFilenameLabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.PointsInputFilenameLabel.Location = new System.Drawing.Point(96, 62);
            this.PointsInputFilenameLabel.Name = "PointsInputFilenameLabel";
            this.PointsInputFilenameLabel.Size = new System.Drawing.Size(77, 13);
            this.PointsInputFilenameLabel.TabIndex = 16;
            this.PointsInputFilenameLabel.Text = "<unassigned>*";
            // 
            // CompulsoryTextlabel
            // 
            this.CompulsoryTextlabel.AutoSize = true;
            this.CompulsoryTextlabel.FlatStyle = System.Windows.Forms.FlatStyle.System;
            this.CompulsoryTextlabel.ForeColor = System.Drawing.Color.SlateGray;
            this.CompulsoryTextlabel.Location = new System.Drawing.Point(26, 91);
            this.CompulsoryTextlabel.Name = "CompulsoryTextlabel";
            this.CompulsoryTextlabel.Size = new System.Drawing.Size(163, 13);
            this.CompulsoryTextlabel.TabIndex = 17;
            this.CompulsoryTextlabel.Text = "* the above fields are compulsory";
            // 
            // NewExpansionDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlLight;
            this.ClientSize = new System.Drawing.Size(320, 229);
            this.Controls.Add(this.CompulsoryTextlabel);
            this.Controls.Add(this.PointsInputFilenameLabel);
            this.Controls.Add(this.DensityInputFilenameLabel);
            this.Controls.Add(this.ExpanderFilenameLabel);
            this.Controls.Add(this.InputValuesLabel);
            this.Controls.Add(this.DensityInputLabel);
            this.Controls.Add(this.ExpanderLabel);
            this.Controls.Add(this.SetInputValuesButton);
            this.Controls.Add(this.SetDensityInputButton);
            this.Controls.Add(this.CancelBtn);
            this.Controls.Add(this.OKBtn);
            this.Controls.Add(this.SetExpanderButton);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "NewExpansionDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "new expansion";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button SetExpanderButton;
        private System.Windows.Forms.Button OKBtn;
        private System.Windows.Forms.Button CancelBtn;
        private System.Windows.Forms.Button SetDensityInputButton;
        private System.Windows.Forms.Button SetInputValuesButton;
        private System.Windows.Forms.Label ExpanderLabel;
        private System.Windows.Forms.Label DensityInputLabel;
        private System.Windows.Forms.Label InputValuesLabel;
        private System.Windows.Forms.Label ExpanderFilenameLabel;
        private System.Windows.Forms.Label DensityInputFilenameLabel;
        private System.Windows.Forms.Label PointsInputFilenameLabel;
        private System.Windows.Forms.Label CompulsoryTextlabel;
    }
}