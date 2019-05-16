
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace assignment2
{
    public partial class GetRGB : Form
    {
        public GetRGB()
        {
            InitializeComponent();
        }

        private void GetRGB_Load(object sender, EventArgs e)
        {
            Bitmap bm = new Bitmap(imgBox.Width, imgBox.Height);
            using (Graphics g = Graphics.FromImage(bm))
            {
                g.Clear(Color.White);
                imgBox.Image = bm;
            }
        }

        private void loadButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog imagefileopen = new OpenFileDialog();
            imagefileopen.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png) | *.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (imagefileopen.ShowDialog() == DialogResult.OK)
            {
                imgBox.Image = new Bitmap(imagefileopen.FileName);
                imgBox.Size = imgBox.Image.Size;
            }
        }


        private void imgBox_MouseMove(object sender, MouseEventArgs e)
        {
            // Array to hold all text boxes
            TextBox[] tbs = {
                textBox00, textBox01, textBox02, textBox03, textBox04,
                textBox10, textBox11, textBox12, textBox13, textBox14,
                textBox20, textBox21, textBox22, textBox23, textBox24,
                textBox30, textBox31, textBox32, textBox33, textBox34,
                textBox40, textBox41, textBox42, textBox43, textBox44,
            };

            // Create bitmap
            Bitmap myBitmap = new Bitmap(imgBox.Image);

            
            int xcoord;         // x coordinate of cursor
            int ycoord;         // y coordinate of cursor
            Color color;        // color variable
            int boxNum = 0;     // box position in array, start at 0

            for (int i = -2; i < 3; i++)
            // loop through x coordinates
            {
                xcoord = e.X + i;       // x coordinate that is i pixels away from cursor

                // check x coordinate bounds
                if (xcoord <= 0)
                    xcoord = 0;
                if (xcoord >= imgBox.Width)
                    xcoord = imgBox.Width - 1;

                for (int j = -2; j < 3; j++)
                // loop through y coordinates
                {
                    ycoord = e.Y + j;   // y coordinate that is j pixels away from cursor

                    // check y coordinate bounds
                    if (ycoord <= 0)
                        ycoord = 0;
                    if (ycoord >= imgBox.Height)
                        ycoord = imgBox.Height - 1;

                    // get the color of the pixel
                    color = myBitmap.GetPixel(xcoord, ycoord);

                    // set textbox background color
                    tbs[boxNum].BackColor = Color.FromArgb(color.R, color.G, color.B);

                    // clear the textbox of previous text
                    tbs[boxNum].Clear();

                    // RGB values of coordinates in textbox
                    tbs[boxNum].AppendText(Environment.NewLine);
                    tbs[boxNum].AppendText("R: " + color.R + Environment.NewLine);
                    tbs[boxNum].AppendText("G: " + color.G + Environment.NewLine);
                    tbs[boxNum].AppendText("B: " + color.B + Environment.NewLine);

                    // Increment to next text box in array
                    boxNum++;
                }
            }
        }  
    }
}
