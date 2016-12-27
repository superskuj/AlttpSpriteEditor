namespace AlttpSpriteEditor
{
    partial class ExtraStatsForm
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
            this.SharedSpriteStatsGroupBox = new System.Windows.Forms.GroupBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.SharedSpriteHealthLabel = new System.Windows.Forms.Label();
            this.SharedSpriteDamageTypeLabel = new System.Windows.Forms.Label();
            this.SharedSpriteStatsGroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // SharedSpriteStatsGroupBox
            // 
            this.SharedSpriteStatsGroupBox.Controls.Add(this.SharedSpriteDamageTypeLabel);
            this.SharedSpriteStatsGroupBox.Controls.Add(this.SharedSpriteHealthLabel);
            this.SharedSpriteStatsGroupBox.Location = new System.Drawing.Point(12, 259);
            this.SharedSpriteStatsGroupBox.Name = "SharedSpriteStatsGroupBox";
            this.SharedSpriteStatsGroupBox.Size = new System.Drawing.Size(265, 118);
            this.SharedSpriteStatsGroupBox.TabIndex = 0;
            this.SharedSpriteStatsGroupBox.TabStop = false;
            this.SharedSpriteStatsGroupBox.Text = "Shared Sprite: ??";
            // 
            // groupBox1
            // 
            this.groupBox1.Location = new System.Drawing.Point(12, 13);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(264, 171);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "groupBox1";
            // 
            // SharedSpriteHealthLabel
            // 
            this.SharedSpriteHealthLabel.AutoSize = true;
            this.SharedSpriteHealthLabel.Location = new System.Drawing.Point(7, 20);
            this.SharedSpriteHealthLabel.Name = "SharedSpriteHealthLabel";
            this.SharedSpriteHealthLabel.Size = new System.Drawing.Size(38, 13);
            this.SharedSpriteHealthLabel.TabIndex = 0;
            this.SharedSpriteHealthLabel.Text = "Health";
            // 
            // SharedSpriteDamageTypeLabel
            // 
            this.SharedSpriteDamageTypeLabel.AutoSize = true;
            this.SharedSpriteDamageTypeLabel.Location = new System.Drawing.Point(7, 47);
            this.SharedSpriteDamageTypeLabel.Name = "SharedSpriteDamageTypeLabel";
            this.SharedSpriteDamageTypeLabel.Size = new System.Drawing.Size(74, 13);
            this.SharedSpriteDamageTypeLabel.TabIndex = 1;
            this.SharedSpriteDamageTypeLabel.Text = "Damage Type";
            // 
            // ExtraStatsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(288, 389);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.SharedSpriteStatsGroupBox);
            this.Name = "ExtraStatsForm";
            this.Text = "ExtraStatsForm";
            this.SharedSpriteStatsGroupBox.ResumeLayout(false);
            this.SharedSpriteStatsGroupBox.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox SharedSpriteStatsGroupBox;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label SharedSpriteDamageTypeLabel;
        private System.Windows.Forms.Label SharedSpriteHealthLabel;
    }
}