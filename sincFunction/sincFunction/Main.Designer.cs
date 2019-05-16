namespace sincFunction
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
            this.Load_Image = new System.Windows.Forms.Button();
            this.imageBox = new System.Windows.Forms.PictureBox();
            this.Double_Button = new System.Windows.Forms.Button();
            this.Quad_Button = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Load_Image
            // 
            this.Load_Image.Location = new System.Drawing.Point(12, 12);
            this.Load_Image.Name = "Load_Image";
            this.Load_Image.Size = new System.Drawing.Size(75, 23);
            this.Load_Image.TabIndex = 0;
            this.Load_Image.Text = "Load";
            this.Load_Image.UseVisualStyleBackColor = true;
            this.Load_Image.Click += new System.EventHandler(this.Load_Image_Click);
            // 
            // imageBox
            // 
            this.imageBox.Location = new System.Drawing.Point(93, 12);
            this.imageBox.Name = "imageBox";
            this.imageBox.Size = new System.Drawing.Size(514, 617);
            this.imageBox.TabIndex = 1;
            this.imageBox.TabStop = false;
            // 
            // Double_Button
            // 
            this.Double_Button.Location = new System.Drawing.Point(612, 12);
            this.Double_Button.Name = "Double_Button";
            this.Double_Button.Size = new System.Drawing.Size(75, 23);
            this.Double_Button.TabIndex = 2;
            this.Double_Button.Text = "Double";
            this.Double_Button.UseVisualStyleBackColor = true;
            this.Double_Button.Click += new System.EventHandler(this.Double_Button_Click);
            // 
            // Quad_Button
            // 
            this.Quad_Button.Location = new System.Drawing.Point(613, 41);
            this.Quad_Button.Name = "Quad_Button";
            this.Quad_Button.Size = new System.Drawing.Size(75, 23);
            this.Quad_Button.TabIndex = 4;
            this.Quad_Button.Text = "Quadruple";
            this.Quad_Button.UseVisualStyleBackColor = true;
            this.Quad_Button.Click += new System.EventHandler(this.Quad_Button_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(698, 641);
            this.Controls.Add(this.Quad_Button);
            this.Controls.Add(this.Double_Button);
            this.Controls.Add(this.imageBox);
            this.Controls.Add(this.Load_Image);
            this.Name = "Main";
            this.Text = "Double or Quadruple Size of Image Using Sinc Function";
            ((System.ComponentModel.ISupportInitialize)(this.imageBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Load_Image;
        private System.Windows.Forms.PictureBox imageBox;
        private System.Windows.Forms.Button Double_Button;
        private System.Windows.Forms.Button Quad_Button;
    }
}

