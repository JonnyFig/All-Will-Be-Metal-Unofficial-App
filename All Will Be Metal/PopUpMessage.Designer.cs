namespace All_Will_Be_Metal
{
    partial class PopUpMessage
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
            this.NoButton = new System.Windows.Forms.Button();
            this.YesButton = new System.Windows.Forms.Button();
            this.OKButton = new System.Windows.Forms.Button();
            this.QuestionTextbox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // NoButton
            // 
            this.NoButton.BackColor = System.Drawing.SystemColors.ControlText;
            this.NoButton.Font = new System.Drawing.Font("BadaBoom BB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NoButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.NoButton.Location = new System.Drawing.Point(648, 372);
            this.NoButton.Name = "NoButton";
            this.NoButton.Size = new System.Drawing.Size(123, 70);
            this.NoButton.TabIndex = 60;
            this.NoButton.Text = "No";
            this.NoButton.UseVisualStyleBackColor = false;
            this.NoButton.Click += new System.EventHandler(this.NoButton_Click);
            this.NoButton.MouseEnter += new System.EventHandler(this.MouseOver_Handler);
            this.NoButton.MouseLeave += new System.EventHandler(this.MouseLeave_Handler);
            // 
            // YesButton
            // 
            this.YesButton.BackColor = System.Drawing.SystemColors.ControlText;
            this.YesButton.Font = new System.Drawing.Font("BadaBoom BB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.YesButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.YesButton.Location = new System.Drawing.Point(155, 372);
            this.YesButton.Name = "YesButton";
            this.YesButton.Size = new System.Drawing.Size(123, 70);
            this.YesButton.TabIndex = 62;
            this.YesButton.Text = "Yes";
            this.YesButton.UseVisualStyleBackColor = false;
            this.YesButton.Click += new System.EventHandler(this.YesButton_Click);
            this.YesButton.MouseEnter += new System.EventHandler(this.MouseOver_Handler);
            this.YesButton.MouseLeave += new System.EventHandler(this.MouseLeave_Handler);
            // 
            // OKButton
            // 
            this.OKButton.BackColor = System.Drawing.SystemColors.ControlText;
            this.OKButton.Font = new System.Drawing.Font("BadaBoom BB", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.OKButton.ForeColor = System.Drawing.SystemColors.ControlLight;
            this.OKButton.Location = new System.Drawing.Point(405, 372);
            this.OKButton.Name = "OKButton";
            this.OKButton.Size = new System.Drawing.Size(123, 70);
            this.OKButton.TabIndex = 63;
            this.OKButton.Text = "OK";
            this.OKButton.UseVisualStyleBackColor = false;
            this.OKButton.Click += new System.EventHandler(this.OKButton_Click);
            this.OKButton.MouseEnter += new System.EventHandler(this.MouseOver_Handler);
            this.OKButton.MouseLeave += new System.EventHandler(this.MouseLeave_Handler);
            // 
            // QuestionTextbox
            // 
            this.QuestionTextbox.BackColor = System.Drawing.Color.Black;
            this.QuestionTextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.QuestionTextbox.Font = new System.Drawing.Font("BadaBoom BB", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.QuestionTextbox.ForeColor = System.Drawing.Color.White;
            this.QuestionTextbox.Location = new System.Drawing.Point(96, 12);
            this.QuestionTextbox.Multiline = true;
            this.QuestionTextbox.Name = "QuestionTextbox";
            this.QuestionTextbox.Size = new System.Drawing.Size(733, 335);
            this.QuestionTextbox.TabIndex = 64;
            this.QuestionTextbox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // PopUpMessage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(922, 468);
            this.ControlBox = false;
            this.Controls.Add(this.QuestionTextbox);
            this.Controls.Add(this.OKButton);
            this.Controls.Add(this.YesButton);
            this.Controls.Add(this.NoButton);
            this.Name = "PopUpMessage";
            this.Load += new System.EventHandler(this.PopUpMessage_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Button NoButton;
        private System.Windows.Forms.Button YesButton;
        private System.Windows.Forms.Button OKButton;
        private System.Windows.Forms.TextBox QuestionTextbox;
    }
}