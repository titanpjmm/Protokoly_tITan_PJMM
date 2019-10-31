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

namespace Protokoly_tITan_PJMM
{
    public partial class Protokol : Form
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        string text_rich_text_boxa = "";



        public Protokol()
        {
            InitializeComponent();
            SendMessage(textBox_nazwa_klienta.Handle, EM_SETCUEBANNER, 0, "Nazwa (imię nazwisko)");
            SendMessage(textBox_adres_klienta.Handle, EM_SETCUEBANNER, 0, "Adres");
            SendMessage(textBox_kod_pocztowy.Handle, EM_SETCUEBANNER, 0, "Kod pocztowy");
            SendMessage(textBox_numer_sluzbowy.Handle, EM_SETCUEBANNER, 0, "Służbowy numer telefonu");
            SendMessage(textBox_numer_prywatny.Handle, EM_SETCUEBANNER, 0, "Prywatny numer telefonu");
            SendMessage(textBox_email.Handle, EM_SETCUEBANNER, 0, "Adres e-mail");
            SendMessage(textBox_nip_klienta.Handle, EM_SETCUEBANNER, 0, "Numer NIP 000-000-00-00");
            SendMessage(textBox_pesel_klienta.Handle, EM_SETCUEBANNER, 0, "Pesel");
            SendMessage(textBox_data_przyjecia.Handle, EM_SETCUEBANNER, 0, "yyyy - MM - dd hh: mm: ss");
            SendMessage(textBox_numer_zlecenia.Handle, EM_SETCUEBANNER, 0, "00000/yyyy");
            SendMessage(textBox_koszt_naprawy.Handle, EM_SETCUEBANNER, 0, "Szacowany koszt naprawy - PLN");
            SendMessage(textBox_czas_naprawy.Handle, EM_SETCUEBANNER, 0, "Szacowany czas - dni");
            ActiveControl = label_titan;
            
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
        private static Bitmap DrawControlToBitmap(Control control)
        {
            Bitmap bitmap = new Bitmap(control.Width, control.Height);
            Graphics graphics = Graphics.FromImage(bitmap);
            Rectangle rect = control.RectangleToScreen(control.ClientRectangle);
            graphics.CopyFromScreen(rect.Location, Point.Empty, control.Size);
            return bitmap;
        }

        private void saveAsJPGToolStripMenuItem_Click(object sender, EventArgs e) // klikniecie w menustrip
        {
            Bitmap bm = DrawControlToBitmap(panel_protokol);
            Save(bm, 1400 ,2000 , 3000);
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
        // jpeg -- jpeg -- jpeg -- jpeg -- jpeg -- jpeg -- jpeg -- jpeg

        private ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        private void RichTextBox_TextChanged(object sender, EventArgs e)
        {
            var richtextbox = sender as RichTextBox;
            text_rich_text_boxa = richtextbox.Text;
        }

        // lista uszkodzen
        private void checkBox_pc_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_pc.Checked)
            {
                checkBox_telefon.Enabled = false;
                checkBox_laptop.Enabled = false;
                checkBox_drukarka.Enabled = false;
                checkBox_touchpad.Enabled = false;
                checkBox_glowica.Enabled = false;
            }
            else
            {
                checkBox_telefon.Enabled = true;
                checkBox_laptop.Enabled = true;
                checkBox_drukarka.Enabled = true;
                checkBox_touchpad.Enabled = true;
                checkBox_glowica.Enabled = true;
            }
        }

        private void checkBox_telefon_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_telefon.Checked)
            {
                checkBox_pc.Enabled = false;
                checkBox_laptop.Enabled = false;
                checkBox_drukarka.Enabled = false;
                checkBox_touchpad.Enabled = false;
                checkBox_glowica.Enabled = false;
            }
            else
            {
                checkBox_pc.Enabled = true;
                checkBox_laptop.Enabled = true;
                checkBox_drukarka.Enabled = true;
                checkBox_touchpad.Enabled = true;
                checkBox_glowica.Enabled = true;
            }
        }

        private void checkBox_laptop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_laptop.Checked)
            {
                checkBox_telefon.Enabled = false;
                checkBox_pc.Enabled = false;
                checkBox_drukarka.Enabled = false;
                checkBox_glowica.Enabled = false;
            }
            else
            {
                checkBox_telefon.Enabled = true;
                checkBox_pc.Enabled = true;
                checkBox_drukarka.Enabled = true;
                checkBox_glowica.Enabled = true;
            }
        }

        private void checkBox_drukarka_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_drukarka.Checked)
            {
                checkBox_telefon.Enabled = false;
                checkBox_laptop.Enabled = false;
                checkBox_pc.Enabled = false;
                checkBox_touchpad.Enabled = false;
            }
            else
            {
                checkBox_telefon.Enabled = true;
                checkBox_laptop.Enabled = true;
                checkBox_pc.Enabled = true;
                checkBox_touchpad.Enabled = true;
            }
        }
        // lista uszkodzen

        // uszkodzenia
        private void checkBox_wyswietlacz_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_klawiatura_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_gniazda_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_procesor_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_plyta_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_zasilacz_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_touchpad_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_os_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void checkBox_ram_CheckedChanged(object sender, EventArgs e)
        {

        }
        // uszkodzenia

        private void checkBox_gotowka_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_gotowka.Checked)
            {
                checkBox_karta.Enabled = false;
                checkBox_przelew.Enabled = false;
            }
            else
            {
                checkBox_karta.Enabled = true;
                checkBox_przelew.Enabled = true;
            }
        }

        private void checkBox_karta_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_karta.Checked)
            {
                checkBox_gotowka.Enabled = false;
                checkBox_przelew.Enabled = false;
            }
            else
            {
                checkBox_gotowka.Enabled = true;
                checkBox_przelew.Enabled = true;
            }
        }

        private void checkBox_przelew_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_przelew.Checked)
            {
                checkBox_gotowka.Enabled = false;
                checkBox_karta.Enabled = false;
            }
            else
            {
                checkBox_gotowka.Enabled = true;
                checkBox_karta.Enabled = true;
            }
        }
        // rodzaj platnosci
    }
}
