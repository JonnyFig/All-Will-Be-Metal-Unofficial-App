namespace All_Will_Be_Metal
{
    partial class ControlConfirmationBox
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
            this.YesControlButton = new System.Windows.Forms.Button();
            this.CancelControlButton = new System.Windows.Forms.Button();
            this.TextLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // YesControlButton
            // 
            this.YesControlButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.YesControlButton.Font = new System.Drawing.Font("BadaBoom BB", 11.25F);
            this.YesControlButton.ForeColor = System.Drawing.Color.White;
            this.YesControlButton.Location = new System.Drawing.Point(12, 95);
            this.YesControlButton.Name = "YesControlButton";
            this.YesControlButton.Size = new System.Drawing.Size(106, 46);
            this.YesControlButton.TabIndex = 0;
            this.YesControlButton.Text = "Yes";
            this.YesControlButton.UseVisualStyleBackColor = false;
            this.YesControlButton.Click += new System.EventHandler(this.YesControlButton_Click);
            // 
            // CancelControlButton
            // 
            this.CancelControlButton.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.CancelControlButton.Font = new System.Drawing.Font("BadaBoom BB", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CancelControlButton.ForeColor = System.Drawing.Color.White;
            this.CancelControlButton.Location = new System.Drawing.Point(226, 95);
            this.CancelControlButton.Name = "CancelControlButton";
            this.CancelControlButton.Size = new System.Drawing.Size(98, 46);
            this.CancelControlButton.TabIndex = 2;
            this.CancelControlButton.Text = "No";
            this.CancelControlButton.UseVisualStyleBackColor = false;
            this.CancelControlButton.Click += new System.EventHandler(this.CancelControlButton_Click);
            // 
            // TextLabel
            // 
            this.TextLabel.AutoSize = true;
            this.TextLabel.Font = new System.Drawing.Font("BadaBoom BB", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TextLabel.ForeColor = System.Drawing.Color.White;
            this.TextLabel.Location = new System.Drawing.Point(8, 36);
            this.TextLabel.Name = "TextLabel";
            this.TextLabel.Size = new System.Drawing.Size(321, 23);
            this.TextLabel.TabIndex = 3;
            this.TextLabel.Text = "Can The Crisis Team Capture this console?";
            // 
            // ControlConfirmationBox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(330, 153);
            this.Controls.Add(this.TextLabel);
            this.Controls.Add(this.CancelControlButton);
            this.Controls.Add(this.YesControlButton);
            this.Name = "ControlConfirmationBox";
            this.Text = "ControlConfirmationBox";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button YesControlButton;
        private System.Windows.Forms.Button CancelControlButton;
        private System.Windows.Forms.Label TextLabel;
    }
}