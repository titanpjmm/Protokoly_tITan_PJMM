using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Syncfusion.Pdf;
using Syncfusion.Pdf.Graphics;
using Syncfusion.Pdf.Interactive;
using System.Drawing;
using System.Diagnostics;
using System.IO;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;

namespace Protokoly_tITan_PJMM
{
    public partial class Protokol : Form
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public static int Flaga_usercontrola = 0;
        public static bool Flaga_visible_ilosc_znakow = true;

        public void UserToFront()
        {
            if (Flaga_usercontrola == 0)
            {
                var protokol_glowny_User = new Protokol_glowna();

                if (!panel_protokol.Contains(protokol_glowny_User.Instance))
                {
                    panel_protokol.Controls.Add(protokol_glowny_User.Instance);
                    protokol_glowny_User.Instance.Dock = DockStyle.Fill;
                    protokol_glowny_User.Instance.BringToFront();
                }
                else
                {
                    protokol_glowny_User.Instance.BringToFront();
                }
            }

            else if (Flaga_usercontrola == 1)
            {
                var protokol_odczyt = new Protokol_odczyt();


                if (!panel_protokol.Contains(protokol_odczyt.Instance))
                {
                    panel_protokol.Controls.Add(protokol_odczyt.Instance);
                    protokol_odczyt.Instance.Dock = DockStyle.Fill;
                    protokol_odczyt.Instance.BringToFront();
                }
                else
                {
                    protokol_odczyt.Instance.BringToFront();
                }
            }
        }


        public Protokol()
        {
            InitializeComponent();
            UserToFront();

            ActiveControl = panel_protokol;
        }

        private void formularzToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flaga_usercontrola = 0;
            UserToFront();
        }

        private void formularzOdczytToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flaga_usercontrola = 1;
            UserToFront();
        }

        private void saveAsPDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreatePdf();
        }

        private void CreatePdf()
        {
            PdfDocument doc = new PdfDocument();
            PdfSection section = doc.Sections.Add();
            PdfPageBase page = doc.Pages.Add();
            PdfImage image = PdfImage.FromFile(@"C:\Users\rozek\Desktop\protokol.jpeg");
            //PdfImage image1 = PdfImage.FromImage();
            //Set image display location and size in PDF
             float widthFitRate = image.PhysicalDimension.Width / page.Size.Width;
             float heightFitRate = image.PhysicalDimension.Height / page.Size.Height;
             float fitRate = Math.Max(widthFitRate , heightFitRate);
             float fitWidth = image.PhysicalDimension.Width/ fitRate;
             float fitHeight = image.PhysicalDimension.Height/ fitRate;
             page.Graphics.DrawImage(image, -10, -10, fitWidth, fitHeight);

            doc.Save("image to pdf.pdf");
            doc.Close();
            System.Diagnostics.Process.Start("image to pdf.pdf");
        }

        // jpeg -- jpeg -- jpeg -- jpeg -- jpeg -- jpeg -- jpeg -- jpeg
        private Bitmap DrawControlToBitmap(Control control)
        {
            ActiveControl = null;
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle rect = control.RectangleToScreen(control.ClientRectangle);
            graphics.CopyFromScreen(rect.Location, Point.Empty, control.Size);
            return bitmap;
        }

        private void saveAsJPGToolStripMenuItem_Click(object sender, EventArgs e) // klikniecie w menustrip
        {     
            Bitmap bm = DrawControlToBitmap(panel_protokol);
            Save(bm, 3050, 4000 , 9000);
            UserToFront();
        }

        public void Save(Bitmap image, int maxWidth, int maxHeight, int quality)
        {
            // Get the image's original width and height
            int originalWidth = image.Width;
            int originalHeight = image.Height;

            // To preserve the aspect ratio
            float ratioX = (float)maxWidth / (float)originalWidth;
            float ratioY = (float)maxHeight / (float)originalHeight;
            //float ratio = Math.Min(ratioX, ratioY);

            // New width and height based on aspect ratio
            int newWidth = (int)(originalWidth * ratioX);
            int newHeight = (int)(originalHeight * ratioY);

            // Convert other formats (including CMYK) to RGB.
            Bitmap newImage = new Bitmap(newWidth, newHeight, PixelFormat.Format24bppRgb);

            // Draws the image in the specified size with quality mode set to HighQuality
            using (Graphics graphics = Graphics.FromImage(newImage))
            {
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.DrawImage(image, 0, 0, newWidth, newHeight);
            }

            // Get an ImageCodecInfo object that represents the JPEG codec.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Jpeg);

            // Create an Encoder object for the Quality parameter.
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a JPEG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;

            SaveFileDialog dialog = new SaveFileDialog
            {
                Filter = "JPEG (*.jpeg)|*.jpeg | BMP (*.bmp)|*.bmp | GIF (*.gif)|*.gif"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                newImage.Save(dialog.FileName, imageCodecInfo, encoderParameters);
            }
            
        }

        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }
        // jpeg -- jpeg -- jpeg -- jpeg -- jpeg -- jpeg -- jpeg -- jpeg

    }
}
