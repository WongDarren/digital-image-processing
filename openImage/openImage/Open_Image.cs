//=====================================
// Darren Wong
// 5002845323
// Assignment 1
// CS 469
//=====================================

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace openImage
{
    public partial class Open_Image : Form
    {
        public Open_Image()
        {
            InitializeComponent();
        }

        //=======================================
        // Load Image Click Event
        //=======================================
        private void Load_Image_Click(object sender, EventArgs e)
        {
            OpenFileDialog imagefileopen = new OpenFileDialog();
            imagefileopen.Filter = "Image Files(*.jpg; *.jpeg; *.gif; *.bmp; *.png) | *.jpg; *.jpeg; *.gif; *.bmp; *.png";
            if (imagefileopen.ShowDialog() == DialogResult.OK)
            {
                OpenImageDisplay.Image = new Bitmap(imagefileopen.FileName);
                OpenImageDisplay.Size = OpenImageDisplay.Image.Size;
            }
        }

        #region Gray

        //=======================================
        // Make Bitmap Gray Scale
        //=======================================
        private Bitmap MakeGrayscale(Bitmap original)
        {
            try
            {
                Bitmap newBitmap = new Bitmap(original.Width, original.Height);
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the gray scale version of each pixel
                        int grayScale = (int)((originalColor.R * 0.3) + (originalColor.G * 0.59) + (originalColor.B * 0.11));
                        Color newColor = Color.FromArgb(grayScale, grayScale, grayScale);
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
        // Gray Scale Click Event
        //=======================================
        private void Gray_Scale_Click(object sender, EventArgs e)
        {
            Form Gray_Scale = new Gray_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeGrayscale(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Gray_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Gray_Scale.Show();
            }
        }

        #endregion

        #region Red

        //=======================================
        // Make Bitmap Red Scale
        //=======================================
        private Bitmap MakeRed(Bitmap original)
        {
            try
            {
                Bitmap redBitmap = new Bitmap(original.Width, original.Height);
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the red scale version of each pixel
                        Color redColor = Color.FromArgb(originalColor.R, originalColor.R, originalColor.R);
                        redBitmap.SetPixel(i, j, redColor);
                    }
                return redBitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }

        }

        //=======================================
        // Red Scale Click Event
        //=======================================
        private void Red_Scale_Click(object sender, EventArgs e)
        {
            Form Red_Scale = new Red_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeRed(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Red_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Red_Scale.Show();
            }
        }
        #endregion

        #region Green

        //=======================================
        // Make Bitmap Green Scale
        //=======================================
        private Bitmap MakeGreen(Bitmap original)
        {
            try
            {
                Bitmap greenBitmap = new Bitmap(original.Width, original.Height);
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the green scale version of each pixel
                        Color greenColor = Color.FromArgb(originalColor.G, originalColor.G, originalColor.G);
                        greenBitmap.SetPixel(i, j, greenColor);
                    }
                return greenBitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }


        //=======================================
        // Green Scale Click Event
        //=======================================
        private void Green_Scale_Click(object sender, EventArgs e)
        {
            Form Green_Scale = new Green_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeGreen(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Green_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Green_Scale.Show();
            }
        }
        #endregion

        #region Blue

        //=======================================
        // Make Bitmap Blue Scale
        //=======================================
        private Bitmap MakeBlue(Bitmap original)
        {
            try
            {
                Bitmap blueBitmap = new Bitmap(original.Width, original.Height);
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the blue scale version of each pixel
                        Color blueColor = Color.FromArgb(originalColor.B, originalColor.B, originalColor.B);
                        blueBitmap.SetPixel(i, j, blueColor);
                    }
                return blueBitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }


        //=======================================
        // Blue Scale Click Event
        //=======================================
        private void Blue_Scale_Click(object sender, EventArgs e)
        {
            Form Blue_Scale = new Blue_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeBlue(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Blue_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Blue_Scale.Show();
            }
        }
        #endregion

        #region Y

        //=======================================
        // Make Bitmap Y Scale
        //=======================================
        private Bitmap MakeYScale(Bitmap original)
        {
            try
            {
                Bitmap y_Bitmap = new Bitmap(original.Width, original.Height);
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the Y scale version of each pixel
                        // Y = 0.299 * R + 0.587 * G + 0.114 * B
                        int calc = (int)((originalColor.R * 0.299) + (originalColor.G * 0.587) + (originalColor.B * 0.114));
                        Color yColor = Color.FromArgb(calc, calc, calc);
                        y_Bitmap.SetPixel(i, j, yColor);
                    }
                return y_Bitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // Y Scale Click Event
        //=======================================
        private void Y_Scale_Click(object sender, EventArgs e)
        {
            Form Y_Scale = new Y_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeYScale(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Y_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Y_Scale.Show();
            }
        }

        private void Y_Scale_1_Click(object sender, EventArgs e)
        {
            Form Y_Scale = new Y_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeYScale(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Y_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Y_Scale.Show();
            }
        }

        private void Y_Scale_2_Click(object sender, EventArgs e)
        {
            Form Y_Scale = new Y_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeYScale(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Y_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Y_Scale.Show();
            }
        }
        #endregion

        #region I

        //=======================================
        // Make Bitmap I Scale
        //=======================================
        private Bitmap MakeIScale(Bitmap original)
        {
            try
            {
                Bitmap i_Bitmap = new Bitmap(original.Width, original.Height);

                // Go through every pixel
                // to find the smallest negative number
                // make it the offset
                int offsetColor = 0;
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        int calc = (int)((originalColor.R * 0.596) - (originalColor.G * 0.275) - (originalColor.B * 0.321));

                        if (calc < offsetColor)
                            offsetColor = calc;
                    }

                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        Color originalColor = original.GetPixel(i, j);

                        // create the I scale version of each pixel
                        // I = 0.596 * R - 0.275 * G - 0.321 * B
                        int calc = (int)((originalColor.R * 0.596) - (originalColor.G * 0.275) - (originalColor.B * 0.321));

                        calc -= offsetColor;

                        Color iColor = Color.FromArgb(calc, calc, calc);
                        i_Bitmap.SetPixel(i, j, iColor);
                    }
                return i_Bitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // I Scale Click Event
        //=======================================
        private void I_Scale_Click(object sender, EventArgs e)
        {
            Form I_Scale = new I_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeIScale(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                I_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                I_Scale.Show();
            }
        }
        #endregion

        #region Q

        //=======================================
        // Make Bitmap Q Scale
        //=======================================
        private Bitmap MakeQScale(Bitmap original)
        {
            try
            {
                Bitmap q_Bitmap = new Bitmap(original.Width, original.Height);

                // Go through every pixel
                // to find the smallest negative number
                // make it the offset
                int offsetColor = 0;
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        int calc = (int)((originalColor.R * 0.212) - (originalColor.G * 0.523) - (originalColor.B * 0.311));

                        if (calc < offsetColor)
                            offsetColor = calc;
                    }

                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the Q scale version of each pixel
                        // Q = 0.212 * R - 0.523 * G - 0.311 * B
                        int calc = (int)((originalColor.R * 0.212) - (originalColor.G * 0.523) - (originalColor.B * 0.311));

                        calc -= offsetColor;

                        Color qColor = Color.FromArgb(calc, calc, calc);
                        q_Bitmap.SetPixel(i, j, qColor);
                    }
                return q_Bitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // Q Scale Click Event
        //=======================================
        private void Q_Scale_Click(object sender, EventArgs e)
        {
            Form Q_Scale = new Q_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeQScale(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Q_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Q_Scale.Show();
            }
        }
        #endregion

        #region Cr

        //=======================================
        // Make Bitmap Cr Scale
        //=======================================
        private Bitmap MakeCr(Bitmap original)
        {
            try
            {
                Bitmap Cr_Bitmap = new Bitmap(original.Width, original.Height);

                // Go through every pixel
                // to find the smallest negative number
                // make it the offset
                int offsetColor = 0;
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        int calc = (int)((originalColor.R * 0.5) - (originalColor.G * 0.419) + (originalColor.B * 0.081));

                        if (calc < offsetColor)
                            offsetColor = calc;
                    }

                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the Cr scale version of each pixel
                        // Cr = 0.5 * R - 0.419 * G - 0.081 * B
                        int calc = (int)((originalColor.R * 0.5) - (originalColor.G * 0.419) + (originalColor.B * 0.081));

                        calc -= offsetColor;

                        Color CrColor = Color.FromArgb(calc, calc, calc);
                        Cr_Bitmap.SetPixel(i, j, CrColor);
                    }
                return Cr_Bitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // Cr Scale Click Event
        //=======================================
        private void Cr_Scale_Click(object sender, EventArgs e)
        {
            Form Cr_Scale = new Cr_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeCr(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Cr_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Cr_Scale.Show();
            }
        }
        #endregion

        #region Cb

        //=======================================
        // Make Bitmap Cb Scale
        //=======================================
        private Bitmap MakeCb(Bitmap original)
        {
            try
            {
                Bitmap Cb_Bitmap = new Bitmap(original.Width, original.Height);

                // Go through every pixel
                // to find the smallest negative number
                // make it the offset
                int offsetColor = 0;
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        int calc = (int)((originalColor.R * -0.169) - (originalColor.G * 0.331) + (originalColor.B * 0.5));

                        if (calc < offsetColor)
                            offsetColor = calc;
                    }

                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the Cb scale version of each pixel
                        // Cb = -0.169 * R - 0.331 * G + 0.5 * B
                        int calc = (int)((originalColor.R * -0.169) - (originalColor.G * 0.331) + (originalColor.B * 0.5));

                        calc -= offsetColor;

                        Color CrColor = Color.FromArgb(calc, calc, calc);
                        Cb_Bitmap.SetPixel(i, j, CrColor);
                    }
                return Cb_Bitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // Cb Scale Click Event
        //=======================================
        private void Cb_Scale_Click(object sender, EventArgs e)
        {
            Form Cb_Scale = new Cb_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeCb(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Cb_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Cb_Scale.Show();
            }
        }
        #endregion

        #region U

        //=======================================
        // Make Bitmap U Scale
        //=======================================
        private Bitmap MakeU(Bitmap original)
        {
            try
            {
                Bitmap U_Bitmap = new Bitmap(original.Width, original.Height);

                // Go through every pixel
                // to find the smallest negative number
                // make it the offset
                int offsetColor = 0;
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        int calc = (int)((originalColor.R * -0.147) - (originalColor.G * 0.289) + (originalColor.B * 0.437));

                        if (calc < offsetColor)
                            offsetColor = calc;
                    }

                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the U scale version of each pixel
                        // U = -0.147 * R - 0.289 * G + 0.437 * B
                        int calc = (int)((originalColor.R * -0.147) - (originalColor.G * 0.289) + (originalColor.B * 0.437));

                        calc -= offsetColor;

                        Color uColor = Color.FromArgb(calc, calc, calc);
                        U_Bitmap.SetPixel(i, j, uColor);
                    }
                return U_Bitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // U Scale Click Event
        //=======================================
        private void U_Scale_Click(object sender, EventArgs e)
        {
            Form U_Scale = new U_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeU(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                U_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                U_Scale.Show();
            }
        }
        #endregion

        #region V

        //=======================================
        // Make Bitmap V Scale
        //=======================================
        private Bitmap MakeV(Bitmap original)
        {

            try
            {
                Bitmap V_Bitmap = new Bitmap(original.Width, original.Height);

                // Go through every pixel
                // to find the smallest negative number
                // make it the offset
                int offsetColor = 0;
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        int calc = (int)((originalColor.R * 0.617) - (originalColor.G * 0.515) - (originalColor.B * 0.102));

                        if (calc < offsetColor)
                            offsetColor = calc;
                    }

                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the V scale version of each pixel
                        // V = 0.617 * R - 0.515 * G - 0.102 * B
                        int calc = (int)((originalColor.R * 0.617) - (originalColor.G * 0.515) - (originalColor.B * 0.102));

                        calc -= offsetColor;

                        Color vColor = Color.FromArgb(calc, calc, calc);
                        V_Bitmap.SetPixel(i, j, vColor);
                    }
                return V_Bitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // V Scale Click Event
        //=======================================
        private void V_Scale_Click(object sender, EventArgs e)
        {
            Form V_Scale = new V_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeV(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                V_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                V_Scale.Show();
            }
        }
        #endregion

        #region Cyan

        //=======================================
        // Make Bitmap Cyan Scale
        //=======================================
        private Bitmap MakeCyan(Bitmap original)
        {
            try
            {
                Bitmap Cyan_Bitmap = new Bitmap(original.Width, original.Height);
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the cyan scale version of each pixel
                        // Cyan = 255 - Red;
                        int calc = 255 - originalColor.R;
                        Color cyanColor = Color.FromArgb(calc, calc, calc);
                        Cyan_Bitmap.SetPixel(i, j, cyanColor);
                    }
                return Cyan_Bitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // Cyan Scale Click Event
        //=======================================
        private void Cyan_Scale_Click(object sender, EventArgs e)
        {
            Form Cyan_Scale = new Cyan_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeCyan(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Cyan_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Cyan_Scale.Show();
            }
        }
        #endregion

        #region Magenta

        //=======================================
        // Make Bitmap Mangeta Scale
        //=======================================
        private Bitmap MakeMagenta(Bitmap original)
        {
            try
            {
                Bitmap Magenta_Bitmap = new Bitmap(original.Width, original.Height);
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the magenta scale version of each pixel
                        // Magenta = 255 - Green;
                        int calc = 255 - originalColor.G;
                        Color magentaColor = Color.FromArgb(calc, calc, calc);
                        Magenta_Bitmap.SetPixel(i, j, magentaColor);
                    }
                return Magenta_Bitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // Magenta Scale Click Event
        //=======================================
        private void Magenta_Scale_Click(object sender, EventArgs e)
        {
            Form Magenta_Scale = new Magenta_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeMagenta(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Magenta_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Magenta_Scale.Show();
            }
        }
        #endregion

        #region Yellow

        //=======================================
        // Make Bitmap Yellow Scale
        //=======================================
        private Bitmap MakeYellow(Bitmap original)
        {
            try
            {
                Bitmap Yellow_Bitmap = new Bitmap(original.Width, original.Height);
                for (int i = 0; i < original.Width; i++)
                    for (int j = 0; j < original.Height; j++)
                    {
                        // get the pixels from the original image
                        Color originalColor = original.GetPixel(i, j);
                        // create the yellow scale version of each pixel
                        // Yellow = 255 - Blue;
                        int calc = 255 - originalColor.B;
                        Color yellowColor = Color.FromArgb(calc, calc, calc);
                        Yellow_Bitmap.SetPixel(i, j, yellowColor);
                    }
                return Yellow_Bitmap;
            }
            catch
            {
                throw new NotImplementedException();
            }
        }

        //=======================================
        // Yellow Scale Click Event
        //=======================================
        private void Yellow_Scale_Click(object sender, EventArgs e)
        {
            Form Yellow_Scale = new Yellow_Scale();
            Bitmap imageInstance = (Bitmap)OpenImageDisplay.Image;
            Bitmap imageInstance1 = new Bitmap(imageInstance.Width, imageInstance.Height);
            if (imageInstance != null)
            {
                imageInstance1 = MakeYellow(imageInstance);
                PictureBox tempPict = new PictureBox();
                tempPict.Size = imageInstance1.Size;
                Yellow_Scale.Controls.Add(tempPict);
                tempPict.Image = imageInstance1;
                Yellow_Scale.Show();
            }
        }
        #endregion

        private void Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
