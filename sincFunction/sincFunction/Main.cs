using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sincFunction
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private Bitmap DoubleWidth(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width * 2, original.Height * 2);
            Color[] ogPixels = new Color[8];
            Color currPixel, newPixel;
            int newRed, newGreen, newBlue;
            int curr = 0;

            // traverse every pixel
            for (int j = 0; j < original.Height; j++)
            {
                for (int i = 0; i < original.Width; i++)
                {
                    currPixel = original.GetPixel(i, j);
                    // get pixels i - 3  to i + 4
                    // store in ogPixels array
                    for (int offset = -3; offset < 5; offset++)
                    {
                        // check bounds
                        if (i + offset > 0 && i + offset < original.Width - 1)
                            ogPixels[curr] = original.GetPixel(i + offset, j);
                        else
                            // store current values if out of bounds
                            ogPixels[curr] = original.GetPixel(i, j);
                        curr++;
                    }
                    // reset curr
                    curr = 0;

                    // got new pixel
                    // calculate each R G B values
                    newRed = -10 * (ogPixels[0].R + ogPixels[7].R) + 14 * (ogPixels[1].R + ogPixels[6].R) - 23 * (ogPixels[2].R + ogPixels[5].R) + 69 * (ogPixels[3].R + ogPixels[4].R);
                    newRed = (newRed + 50) / 100;

                    newGreen = -10 * (ogPixels[0].G + ogPixels[7].G) + 14 * (ogPixels[1].G + ogPixels[6].G) - 23 * (ogPixels[2].G + ogPixels[5].G) + 69 * (ogPixels[3].G + ogPixels[4].G);
                    newGreen = (newGreen + 50) / 100;

                    newBlue = -10 * (ogPixels[0].B + ogPixels[7].B) + 14 * (ogPixels[1].B + ogPixels[6].B) - 23 * (ogPixels[2].B + ogPixels[5].B) + 69 * (ogPixels[3].B + ogPixels[4].B);
                    newBlue = (newBlue + 50) / 100;

                    if (newRed < 0)
                        newRed = Math.Abs(newRed);
                    else if (newRed > 255)
                        newRed = 255;

                    if (newGreen < 0)
                        newGreen = Math.Abs(newGreen);
                    else if (newGreen > 255)
                        newGreen = 255;

                    if (newBlue < 0)
                        newBlue = Math.Abs(newBlue);
                    else if (newBlue > 255)
                        newBlue = 255;

                    newPixel = Color.FromArgb(newRed, newGreen, newBlue);

                    // reset colors
                    newRed = 0;
                    newBlue = 0;
                    newGreen = 0;

                    // need to map old pixels and new pixels to new bitmap
                    // map old pixels to even number x pixels 0, 2, 4, . . .
                    newBitmap.SetPixel(2 * i, j, currPixel);

                    // map new pixels to odd number x pixels 1, 3, 5, . . .
                    newBitmap.SetPixel(2 * i + 1, j, newPixel);
                }
            }
                

            return newBitmap;
        }

        private Bitmap DoubleHeight(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width * 2, original.Height * 2);
            Color[] ogPixels = new Color[8];
            Color currPixel, newPixel;
            int newRed, newGreen, newBlue;
            int curr = 0;

            // traverse every pixel
            for (int i = 0; i < original.Width; i++)
            {
                for (int j = 0; j < original.Height; j++)
                {
                    currPixel = original.GetPixel(i, j);
                    // get pixels i - 3  to i + 4
                    // store in ogPixels array
                    for (int offset = -3; offset < 5; offset++)
                    {
                        // check bounds
                        if (j + offset > 0 && j + offset < original.Height - 1)
                            ogPixels[curr] = original.GetPixel(i, j + offset);
                        else
                            // store current values if out of bounds
                            ogPixels[curr] = original.GetPixel(i, j);
                        curr++;
                    }
                    // reset curr
                    curr = 0;

                    // got new pixel
                    // calculate each R G B values
                    newRed = -10 * (ogPixels[0].R + ogPixels[7].R) + 14 * (ogPixels[1].R + ogPixels[6].R) - 23 * (ogPixels[2].R + ogPixels[5].R) + 69 * (ogPixels[3].R + ogPixels[4].R);
                    newRed = (newRed + 50) / 100;

                    newGreen = -10 * (ogPixels[0].G + ogPixels[7].G) + 14 * (ogPixels[1].G + ogPixels[6].G) - 23 * (ogPixels[2].G + ogPixels[5].G) + 69 * (ogPixels[3].G + ogPixels[4].G);
                    newGreen = (newGreen + 50) / 100;

                    newBlue = -10 * (ogPixels[0].B + ogPixels[7].B) + 14 * (ogPixels[1].B + ogPixels[6].B) - 23 * (ogPixels[2].B + ogPixels[5].B) + 69 * (ogPixels[3].B + ogPixels[4].B);
                    newBlue = (newBlue + 50) / 100;

                    if (newRed < 0)
                        newRed = Math.Abs(newRed);
                    else if (newRed > 255)
                        newRed = 255;

                    if (newGreen < 0)
                        newGreen = Math.Abs(newGreen);
                    else if (newGreen > 255)
                        newGreen = 255;

                    if (newBlue < 0)
                        newBlue = Math.Abs(newBlue);
                    else if (newBlue > 255)
                        newBlue = 255;

                    newPixel = Color.FromArgb(newRed, newGreen, newBlue);

                    // reset colors
                    newRed = 0;
                    newBlue = 0;
                    newGreen = 0;

                    // need to map old pixels and new pixels to new bitmap
                    // map old pixels to even number x pixels 0, 2, 4, . . .
                    newBitmap.SetPixel(i, 2 * j, currPixel);

                    // map new pixels to odd number x pixels 1, 3, 5, . . .
                    newBitmap.SetPixel(i, 2 * j + 1, newPixel);
                }
            }


            return newBitmap;
        }

        private void Load_Image_Click(object sender, EventArgs e)
        {
            OpenFileDialog imagefileopen = new OpenFileDialog();
            imagefileopen.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png) | *.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (imagefileopen.ShowDialog() == DialogResult.OK)
            {
                imageBox.Image = new Bitmap(imagefileopen.FileName);
                imageBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        private void Double_Button_Click(object sender, EventArgs e)
        {
            Form Double = new Double();
            Bitmap imageInstance = (Bitmap)imageBox.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = DoubleWidth(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Double.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Double.Show();
            }

        }

        private void Quad_Button_Click(object sender, EventArgs e)
        {
            Form Quadruple = new Quadruple();
            Bitmap imageInstance = (Bitmap)imageBox.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = DoubleWidth(imageInstance);
                imageInstance1 = DoubleHeight(imageInstance1);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Quadruple.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Quadruple.Show();
            }
        }
    }
}
