namespace NewCanny
{
    partial class Main
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
            this.Load = new System.Windows.Forms.Button();
            this.picBox = new System.Windows.Forms.PictureBox();
            this.Canny = new System.Windows.Forms.Button();
            this.cannyBox = new System.Windows.Forms.PictureBox();
            this.Note = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cannyBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Load
            // 
            this.Load.Location = new System.Drawing.Point(12, 12);
            this.Load.Name = "Load";
            this.Load.Size = new System.Drawing.Size(75, 23);
            this.Load.TabIndex = 0;
            this.Load.Text = "Load";
            this.Load.UseVisualStyleBackColor = true;
            this.Load.Click += new System.EventHandler(this.Load_Click);
            // 
            // picBox
            // 
            this.picBox.Location = new System.Drawing.Point(93, 12);
            this.picBox.Name = "picBox";
            this.picBox.Size = new System.Drawing.Size(428, 559);
            this.picBox.TabIndex = 1;
            this.picBox.TabStop = false;
            // 
            // Canny
            // 
            this.Canny.Location = new System.Drawing.Point(527, 12);
            this.Canny.Name = "Canny";
            this.Canny.Size = new System.Drawing.Size(75, 23);
            this.Canny.TabIndex = 2;
            this.Canny.Text = "Canny";
            this.Canny.UseVisualStyleBackColor = true;
            this.Canny.Click += new System.EventHandler(this.Canny_Click);
            // 
            // cannyBox
            // 
            this.cannyBox.Location = new System.Drawing.Point(608, 12);
            this.cannyBox.Name = "cannyBox";
            this.cannyBox.Size = new System.Drawing.Size(428, 559);
            this.cannyBox.TabIndex = 3;
            this.cannyBox.TabStop = false;
            // 
            // Note
            // 
            this.Note.AutoSize = true;
            this.Note.Location = new System.Drawing.Point(605, 574);
            this.Note.Name = "Note";
            this.Note.Size = new System.Drawing.Size(365, 13);
            this.Note.TabIndex = 4;
            this.Note.Text = "*Note: Larger resolution images will take longer to smooth and detect edges.";
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1183, 638);
            this.Controls.Add(this.Note);
            this.Controls.Add(this.cannyBox);
            this.Controls.Add(this.Canny);
            this.Controls.Add(this.picBox);
            this.Controls.Add(this.Load);
            this.Name = "Main";
            this.Text = "Canny Edge Detection";
            ((System.ComponentModel.ISupportInitialize)(this.picBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cannyBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button Load;
        private System.Windows.Forms.PictureBox picBox;
        private System.Windows.Forms.Button Canny;
        private System.Windows.Forms.PictureBox cannyBox;
        private System.Windows.Forms.Label Note;
    }
}

