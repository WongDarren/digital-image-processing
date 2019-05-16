using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NewCanny
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        //=======================================
        // Make Bitmap Gray Scale
        //=======================================
        private Bitmap MakeGrayscale(Bitmap original)
        {
            try
            {
                Color originalColor;
                Color newColor;
                Bitmap newBitmap = new Bitmap(original.Width, original.Height);

                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        originalColor = original.GetPixel(i, j);
                        // create the gray scale version of each pixel
                        int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                        newColor = Color.FromArgb(grayScale, grayScale, grayScale);
                        newBitmap.SetPixel(i, j, newColor);
                    }
                return newBitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // Apply Smoothing
        //=======================================
        private Bitmap MakeSmooth(Bitmap original)
        {

            int runningSum = 0;
            int tempSum = 0;
            int xcoord;
            int ycoord;
            Color newColor;
            int[,] kernel = new int[5, 5] {
                    {1, 4, 7, 4, 1},
                    {4, 16, 26, 16, 4},
                    {7, 26, 41, 26, 7},
                    {4, 16, 26, 16, 4},
                    {1, 4, 7, 4, 1}
                };
            Color[,] pixels = new Color[5, 5];

            Bitmap newBitmap = new Bitmap(original.Width, original.Height);

            // traverse every pixel
            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    // for every pixel
                    // put surrounding pixels in 5x5 matrix
                    for (int x = -2; x < 3; x++)
                        for (int y = -2; y < 3; y++)
                        {
                            // offset xcoord and ycoord
                            xcoord = i + x;
                            ycoord = j + y;

                            // check if out of bounds
                            // reflect pixel value if out of bounds
                            if (xcoord < 0 || xcoord > original.Width - 1)
                                xcoord = i - x;
                            if (ycoord < 0 || ycoord > original.Height - 1)
                                ycoord = j - y;

                            // assign to 5x5 pixels color matrix
                            pixels[x + 2, y + 2] = original.GetPixel(xcoord, ycoord);
                        }
                    
                    // Multiply matrix values
                    // add to tempSum
                    for (int k = 0; k < 5; k++)
                        for (int l = 0; l < 5; l++)
                            tempSum += kernel[k, l] * pixels[k, l].R;
                    // divide by 273
                    runningSum = tempSum / 273;
                    // thats the new color
                    newColor = Color.FromArgb(runningSum, runningSum, runningSum);
                    // set the color to the bitmap
                    newBitmap.SetPixel(i, j, newColor);

                    // reset sum
                    tempSum = 0;
                    runningSum = 0;
                }
            return newBitmap;
        }

        //=======================================
        // Detect Edges
        //=======================================
        private Bitmap DetectEdge(Bitmap original)
        {
            Bitmap newBitmap = new Bitmap(original.Width, original.Height);
            int xleft;
            int xright;
            int ytop;
            int ybot;
            double gx;
            double gy;
            double tempAngle;
            Color color1, color2;
            double[,] magnitudes = new double[original.Width, original.Height];
            double[,] angles = new double[original.Width, original.Height];
            bool[,] isEdge = new bool[original.Width, original.Height];
            double maxMag = 0;

            // traverse every pixel
            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    xleft = i - 1;
                    xright = i + 1;
                    ytop = j - 1;
                    ybot = j + 1;

                    // check bounds
                    // if out of bounds, get reflected value
                    if (xleft < 0)
                        xleft = xright;
                    if (xright > original.Width - 1)
                        xright = xleft;
                    if (ytop < 0)
                        ytop = ybot;
                    if (ybot > original.Height - 1)
                        ybot = ytop;

                    // compute x gradient magnitude
                    color1 = original.GetPixel(xright, j);
                    color2 = original.GetPixel(xleft, j);
                    gx = (color1.R - color2.R) / 2;

                    // compute y gradient magnitude
                    color1 = original.GetPixel(i, ybot);
                    color2 = original.GetPixel(i, ytop);
                    gy = (color1.R - color2.R) / 2;

                    // store magnitudes
                    magnitudes[i, j] = Math.Abs(gx) + Math.Abs(gy);
                    if (magnitudes[i, j] > maxMag)
                        maxMag = magnitudes[i, j];

                    // need to round angles and then store angles
                    tempAngle = Math.Atan(gy / gx);
                    // convert from radians to degrees
                    tempAngle = tempAngle * 180 / Math.PI;

                    // θ in [0°, 22.5°] or [157.6°, 180°] maps to 0°.
                    if ((tempAngle >= 0 && tempAngle < 22.5) || (tempAngle > 157.5 && tempAngle <= 180) || (tempAngle <= 0 && tempAngle > -22.5) || (tempAngle < -157.5 && tempAngle >= -180))
                        tempAngle = 0.0;
                    // θ in [22.6°, 67.4°] maps to 45°.
                    else if ((tempAngle > 22.5 && tempAngle < 67.5) || (tempAngle < -22.5 && tempAngle > -67.5))
                        tempAngle = 45.0;
                    // θ in [67.5°, 112.5°] maps to 90°.
                    else if ((tempAngle > 67.5 && tempAngle < 112.5) || (tempAngle < -67.5 && tempAngle > -112.5))
                        tempAngle = 90.0;
                    // θ in [112.6°, 157.5°] maps to 135°.
                    else if ((tempAngle > 112.5 && tempAngle < 157.5) || (tempAngle < -112.5 && tempAngle > -157.5))
                        tempAngle = 135.0;

                    angles[i, j] = tempAngle;
                }

            // now need to check neighboring magnitudes of every pixel
            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    // if gradient angle is 0°, is edge if its gradient magnitude is greater than the magnitudes at pixels @ the left and right
                    // if gradient angle is 90°, is edge if its gradient magnitude is greater than the magnitudes at pixels @ the top and bot
                    // if gradient angle is 135°, is edge if its gradient magnitude is greater than the magnitudes at pixels @ the topleft and botright
                    // if gradient angle is 45°, is edge if its gradient magnitude is greater than the magnitudes at pixels @ the topright and botleft
                    if ((i - 1 < 0 || i + 1 > original.Width - 1 || j - 1 < 0 || j + 1 > original.Height - 1))
                    {
                        isEdge[i, j] = false;
                    }
                    else if (angles[i, j] == 0.0)
                    {
                        
                        if (magnitudes[i, j] > magnitudes[i - 1, j] && magnitudes[i, j] > magnitudes[i + 1, j])
                            isEdge[i, j] = true;
                        else
                            isEdge[i, j] = false;
                    }
                    else if (angles[i, j] == 90.0)
                    {
                        
                        if (magnitudes[i, j] > magnitudes[i, j - 1] && magnitudes[i, j] > magnitudes[i, j + 1])
                            isEdge[i, j] = true;
                        else
                            isEdge[i, j] = false; 
                    }
                    else if (angles[i, j] == 135.0)
                    {
                       
                        if (magnitudes[i, j] > magnitudes[i - 1, j - 1] && magnitudes[i, j] > magnitudes[i + 1, j + 1])
                            isEdge[i, j] = true;
                        else
                            isEdge[i, j] = false;
                    }
                    else if (angles[i, j] == 45.0)
                    {
                        if (magnitudes[i, j] > magnitudes[i + 1, j - 1] && magnitudes[i, j] > magnitudes[i - 1, j + 1])
                            isEdge[i, j] = true;
                        else
                            isEdge[i, j] = false;
                    }
                }

            double lowerThreshold = maxMag * 0.10;

            for (int i = 0; i < original.Width; i++)
                for (int j = 0; j < original.Height; j++)
                {
                    // if edge
                    if (isEdge[i, j] && magnitudes[i, j] > lowerThreshold)
                    {
                        // check pixels in the direction of the edge
                        // if either or both have 
                            // the same angle as [i, j]
                            // magnitude is > than lower threshold
                            // then mark as edge
                        if (angles[i, j] == 0.0)
                        {
                            if (angles[i, j] == angles[i - 1, j] || angles[i, j] == angles[i + 1, j])
                            {
                                if (magnitudes[i - 1, j] > lowerThreshold)
                                    newBitmap.SetPixel(i - 1, j, Color.White);
                                else
                                    newBitmap.SetPixel(i - 1, j, Color.Black);

                                if (magnitudes[i + 1, j] > lowerThreshold)
                                    newBitmap.SetPixel(i + 1, j, Color.White);
                                else
                                    newBitmap.SetPixel(i + 1, j, Color.Black);
                            }
                        }
                        else if (angles[i, j] == 90.0)
                        {
                            if (angles[i, j] == angles[i, j - 1] || angles[i, j] == angles[i, j + 1])
                            {
                                if (magnitudes[i, j - 1] > lowerThreshold)
                                    newBitmap.SetPixel(i, j - 1, Color.White);
                                else
                                    newBitmap.SetPixel(i, j - 1, Color.Black);

                                if (magnitudes[i, j + 1] > lowerThreshold)
                                    newBitmap.SetPixel(i, j + 1, Color.White);
                                else
                                    newBitmap.SetPixel(i, j + 1, Color.Black);
                            }
                        }
                        else if (angles[i, j] == 135.0)
                        {
                            if (angles[i, j] == angles[i - 1, j - 1] || angles[i, j] == angles[i + 1, j + 1])
                            {
                                if (magnitudes[i - 1, j - 1] > lowerThreshold)
                                    newBitmap.SetPixel(i - 1, j - 1, Color.White);
                                else
                                    newBitmap.SetPixel(i - 1, j - 1, Color.Black);

                                if (magnitudes[i + 1, j + 1] > lowerThreshold)
                                    newBitmap.SetPixel(i + 1, j + 1, Color.White);
                                else
                                    newBitmap.SetPixel(i + 1, j + 1, Color.Black);
                            }
                        }
                        else if (angles[i, j] == 45.0)
                        {
                            if (angles[i, j] == angles[i + 1, j - 1] || angles[i, j] == angles[i - 1, j + 1])
                            {
                                if (magnitudes[i + 1, j - 1] > lowerThreshold)
                                    newBitmap.SetPixel(i + 1, j - 1, Color.White);
                                else
                                    newBitmap.SetPixel(i + 1, j - 1, Color.Black);

                                if (magnitudes[i - 1, j + 1] > lowerThreshold)
                                    newBitmap.SetPixel(i - 1, j + 1, Color.White);
                                else
                                    newBitmap.SetPixel(i - 1, j + 1, Color.Black);
                            }
                        }
                    }
                    else
                        newBitmap.SetPixel(i, j, Color.Black);
                }

            return newBitmap;
        }

        //=======================================
        // Load Click
        //=======================================
        private void Load_Click(object sender, EventArgs e)
        {
            OpenFileDialog imagefileopen = new OpenFileDialog();
            imagefileopen.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png) | *.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (imagefileopen.ShowDialog() == DialogResult.OK)
            {
                picBox.Image = new Bitmap(imagefileopen.FileName);
                picBox.SizeMode = PictureBoxSizeMode.Zoom;
            }
        }

        //=======================================
        // Perform all steps to Canny Edge Detection
        //=======================================
        private void Canny_Click(object sender, EventArgs e)
        {
            Bitmap newBitmap = (Bitmap)picBox.Image;
            Bitmap newBitmap1 = new Bitmap(newBitmap.Width, newBitmap.Height);

            cannyBox.SizeMode = PictureBoxSizeMode.Zoom;
            newBitmap1 = MakeGrayscale(newBitmap);
            newBitmap1 = MakeSmooth(newBitmap1);
            newBitmap1 = DetectEdge(newBitmap1);

            cannyBox.Image = newBitmap1;
        }
    }
}
