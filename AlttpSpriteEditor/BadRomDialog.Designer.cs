namespace AlttpSpriteEditor
{
    partial class BadRomDialog
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
            this.BadRomLabel = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BadRomLabel
            // 
            this.BadRomLabel.AutoSize = true;
            this.BadRomLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BadRomLabel.Location = new System.Drawing.Point(62, 9);
            this.BadRomLabel.Name = "BadRomLabel";
            this.BadRomLabel.Size = new System.Drawing.Size(84, 16);
            this.BadRomLabel.TabIndex = 0;
            this.BadRomLabel.Text = "Invalid ROM!";
            // 
            // OkButton
            // 
            this.OkButton.Location = new System.Drawing.Point(65, 33);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 1;
            this.OkButton.Text = "Ok, sorry!";
            this.OkButton.UseVisualStyleBackColor = true;
            this.OkButton.Click += new System.EventHandler(this.OkButton_Click);
            // 
            // BadRomDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(215, 65);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.BadRomLabel);
            this.Name = "BadRomDialog";
            this.Text = "Error";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label BadRomLabel;
        private System.Windows.Forms.Button OkButton;
    }
}