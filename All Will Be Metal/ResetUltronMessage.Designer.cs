namespace All_Will_Be_Metal
{
    partial class ResetUltronMessage
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
            this.UltronConditionLabel = new System.Windows.Forms.Label();
            this.UltronResetOKButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // UltronConditionLabel
            // 
            this.UltronConditionLabel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.UltronConditionLabel.AutoSize = true;
            this.UltronConditionLabel.Font = new System.Drawing.Font("BadaBoom BB", 27.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UltronConditionLabel.ForeColor = System.Drawing.Color.White;
            this.UltronConditionLabel.Location = new System.Drawing.Point(80, 40);
            this.UltronConditionLabel.Name = "UltronConditionLabel";
            this.UltronConditionLabel.Size = new System.Drawing.Size(670, 280);
            this.UltronConditionLabel.TabIndex = 60;
            this.UltronConditionLabel.Text = "You have corrupted Ultron\'s Firmware.\r\n\r\nPleace him within range 3 of his current" +
    " location\r\n - or -\r\nrange 1 of a doomsday console he controls.\r\n\r\n\r\n";
            this.UltronConditionLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // UltronResetOKButton
            // 
            this.UltronResetOKButton.BackColor = System.Drawing.SystemColors.ControlText;
            this.UltronResetOKButton.Font = new System.Drawing.Font("BadaBoom BB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.UltronResetOKButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.UltronResetOKButton.Location = new System.Drawing.Point(354, 314);
            this.UltronResetOKButton.Name = "UltronResetOKButton";
            this.UltronResetOKButton.Size = new System.Drawing.Size(123, 70);
            this.UltronResetOKButton.TabIndex = 61;
            this.UltronResetOKButton.Text = "Done";
            this.UltronResetOKButton.UseVisualStyleBackColor = false;
            this.UltronResetOKButton.Click += new System.EventHandler(this.UltronResetOKButton_Click);
            // 
            // ResetUltronMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.UltronResetOKButton);
            this.Controls.Add(this.UltronConditionLabel);
            this.Name = "ResetUltronMessage";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label UltronConditionLabel;
        private System.Windows.Forms.Button UltronResetOKButton;
    }
}