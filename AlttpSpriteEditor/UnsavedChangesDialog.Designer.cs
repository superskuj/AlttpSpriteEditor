namespace AlttpSpriteEditor
{
    partial class UnsavedChangesDialog
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
            this.UnsavedChangesLabel = new System.Windows.Forms.Label();
            this.SaveButton = new System.Windows.Forms.Button();
            this.DontSaveButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UnsavedChangesLabel
            // 
            this.UnsavedChangesLabel.AutoSize = true;
            this.UnsavedChangesLabel.Location = new System.Drawing.Point(12, 9);
            this.UnsavedChangesLabel.Name = "UnsavedChangesLabel";
            this.UnsavedChangesLabel.Size = new System.Drawing.Size(204, 13);
            this.UnsavedChangesLabel.TabIndex = 0;
            this.UnsavedChangesLabel.Text = "There are unsaved changes.  Save now?";
            // 
            // SaveButton
            // 
            this.SaveButton.DialogResult = System.Windows.Forms.DialogResult.Yes;
            this.SaveButton.Location = new System.Drawing.Point(60, 36);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(75, 23);
            this.SaveButton.TabIndex = 1;
            this.SaveButton.Text = "Save";
            this.SaveButton.UseVisualStyleBackColor = true;
            this.SaveButton.Click += new System.EventHandler(this.SaveButton_Click);
            // 
            // DontSaveButton
            // 
            this.DontSaveButton.DialogResult = System.Windows.Forms.DialogResult.No;
            this.DontSaveButton.Location = new System.Drawing.Point(141, 36);
            this.DontSaveButton.Name = "DontSaveButton";
            this.DontSaveButton.Size = new System.Drawing.Size(75, 23);
            this.DontSaveButton.TabIndex = 2;
            this.DontSaveButton.Text = "Don\'t Save";
            this.DontSaveButton.UseVisualStyleBackColor = true;
            this.DontSaveButton.Click += new System.EventHandler(this.DontSaveButton_Click);
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(222, 36);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 3;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            this.CancelButton.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // UnsavedChangesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(310, 69);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.DontSaveButton);
            this.Controls.Add(this.SaveButton);
            this.Controls.Add(this.UnsavedChangesLabel);
            this.Name = "UnsavedChangesDialog";
            this.Text = "Unsaved Changes";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UnsavedChangesLabel;
        private System.Windows.Forms.Button SaveButton;
        private System.Windows.Forms.Button DontSaveButton;
        private System.Windows.Forms.Button CancelButton;
    }
}