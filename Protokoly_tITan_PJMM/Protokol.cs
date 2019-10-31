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



        public Protokol()
        {
            InitializeComponent();

            SendMessage(textBox_nazwa_klienta.Handle, EM_SETCUEBANNER, 0, "Nazwa (imię i nazwisko)");
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
            SendMessage(textBox_czas_naprawy.Handle, EM_SETCUEBANNER, 0, "Szacowany czas naprawy - dni");

            checkboxy_wszystkie_uszkodzenia();

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
            Save(bm, 1400, 1920 , 3000);
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

        // lista uszkodzen
        public void checkboxy_wszystkie_uszkodzenia()
        {
            if (!checkBox_pc.Checked && !checkBox_telefon.Checked && !checkBox_laptop.Checked && !checkBox_drukarka.Checked)
            {
                foreach(CheckBox szukaj in groupBox_uszkodzenia.Controls.OfType<CheckBox>())
                {
                        szukaj.Enabled = false;
                        szukaj.Checked = false;
                }
            }
        }

        private void checkBox_pc_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_pc.Checked)
            {
                checkBox_telefon.Enabled = false;
                checkBox_laptop.Enabled = false;
                checkBox_drukarka.Enabled = false;
                checkBox_touchpad.Enabled = false;
                checkBox_glowica.Enabled = false;

                checkBox_wyswietlacz.Enabled = true;
                checkBox_procesor.Enabled = true;
                checkBox_klawiatura.Enabled = true;
                checkBox_plyta.Enabled = true;
                checkBox_os.Enabled = true;
                checkBox_gniazda.Enabled = true;
                checkBox_zasilacz.Enabled = true;
                checkBox_ram.Enabled = true;
                checkBox_obudowa.Enabled = true;
            }
            else
            {
                checkBox_telefon.Enabled = true;
                checkBox_laptop.Enabled = true;
                checkBox_drukarka.Enabled = true;
                checkBox_touchpad.Enabled = true;
                checkBox_glowica.Enabled = true;

            }

            checkboxy_wszystkie_uszkodzenia();
            
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

                checkBox_wyswietlacz.Enabled = true;
                checkBox_procesor.Enabled = true;
                checkBox_klawiatura.Enabled = true;
                checkBox_plyta.Enabled = true;
                checkBox_os.Enabled = true;
                checkBox_gniazda.Enabled = true;
                checkBox_zasilacz.Enabled = true;
                checkBox_ram.Enabled = true;
                checkBox_dotyk.Enabled = true;
                checkBox_obudowa.Enabled = true;
            }
            else
            {
                checkBox_pc.Enabled = true;
                checkBox_laptop.Enabled = true;
                checkBox_drukarka.Enabled = true;
                checkBox_touchpad.Enabled = true;
                checkBox_glowica.Enabled = true;
            }

            checkboxy_wszystkie_uszkodzenia();
        }

        private void checkBox_laptop_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_laptop.Checked)
            {
                checkBox_telefon.Enabled = false;
                checkBox_pc.Enabled = false;
                checkBox_drukarka.Enabled = false;
                checkBox_glowica.Enabled = false;

                checkBox_wyswietlacz.Enabled = true;
                checkBox_procesor.Enabled = true;
                checkBox_touchpad.Enabled = true;
                checkBox_klawiatura.Enabled = true;
                checkBox_plyta.Enabled = true;
                checkBox_os.Enabled = true;
                checkBox_gniazda.Enabled = true;
                checkBox_zasilacz.Enabled = true;
                checkBox_ram.Enabled = true;
                checkBox_dotyk.Enabled = true;
                checkBox_obudowa.Enabled = true;
            }
            else
            {
                checkBox_telefon.Enabled = true;
                checkBox_pc.Enabled = true;
                checkBox_drukarka.Enabled = true;
                checkBox_glowica.Enabled = true;
            }

            checkboxy_wszystkie_uszkodzenia();
        }

        private void checkBox_drukarka_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_drukarka.Checked)
            {
                checkBox_telefon.Enabled = false;
                checkBox_laptop.Enabled = false;
                checkBox_pc.Enabled = false;
                checkBox_touchpad.Enabled = false;

                checkBox_wyswietlacz.Enabled = true;
                checkBox_procesor.Enabled = true;
                checkBox_plyta.Enabled = true;
                checkBox_os.Enabled = true;
                checkBox_gniazda.Enabled = true;
                checkBox_zasilacz.Enabled = true;
                checkBox_ram.Enabled = true;
                checkBox_glowica.Enabled = true;
                checkBox_dotyk.Enabled = true;
                checkBox_obudowa.Enabled = true;
            }
            else
            {
                checkBox_telefon.Enabled = true;
                checkBox_laptop.Enabled = true;
                checkBox_pc.Enabled = true;
                checkBox_touchpad.Enabled = true;
            }

            checkboxy_wszystkie_uszkodzenia();
        }
        // lista uszkodzen

        // uszkodzenia
        private void checkBox_wyswietlacz_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox_wyswietlacz.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_klawiatura_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_klawiatura.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_gniazda_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_gniazda.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_procesor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_procesor.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_plyta_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_plyta.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_zasilacz_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_zasilacz.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_touchpad_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_touchpad.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_os_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_os.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_ram_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ram.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_glowica_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_glowica.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_dotyk_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_dotyk.Checked)
            {

            }
            else
            {

            }
        }

        private void checkBox_obudowa_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_obudowa.Checked)
            {

            }
            else
            {

            }
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

        private void textBox_numer_zlecenia_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[0-9]{5}\/[0-9]{4}$");

            if(regex.Match(textBox_numer_zlecenia.Text).Success || textBox_numer_zlecenia.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_numer_zlecenia, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
            else
            {
                Border.SetHighlightColor(textBox_numer_zlecenia, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }

        private void textBox_data_przyjecia_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[0-9]{4}\-[0-9]{2}\-[0-9]{2}\ [0-9]{2}\:[0-9]{2}\:[0-9]{2}$");

            if (regex.Match(textBox_data_przyjecia.Text).Success || textBox_data_przyjecia.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_data_przyjecia, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
            else
            {
                Border.SetHighlightColor(textBox_data_przyjecia, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }

        private void textBox_nazwa_klienta_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_adres_klienta_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox_kod_pocztowy_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[0-9]{2}\-[0-9]{3}\ [A-ZĄĆĘŁŃÓŻŹ]{1}[a-ząćęłńóżź]{2,10}$");

            if (regex.Match(textBox_kod_pocztowy.Text).Success || textBox_kod_pocztowy.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_kod_pocztowy, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
            else
            {
                Border.SetHighlightColor(textBox_kod_pocztowy, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }

        private void textBox_nip_klienta_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[0-9]{3}\-[0-9]{3}\-[0-9]{2}\-[0-9]{2}$");

            if (regex.Match(textBox_nip_klienta.Text).Success || textBox_nip_klienta.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_nip_klienta, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
            else
            {
                Border.SetHighlightColor(textBox_nip_klienta, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }

        private void textBox_pesel_klienta_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[0-9]{11}$");

            if (regex.Match(textBox_pesel_klienta.Text).Success || textBox_pesel_klienta.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_pesel_klienta, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
            else
            {
                Border.SetHighlightColor(textBox_pesel_klienta, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }

        private void textBox_numer_sluzbowy_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[+]{0,1}[4]{0,1}[8]{0,1}[ ]{0,1}[0-9]{9}$");

            if (regex.Match(textBox_numer_sluzbowy.Text).Success || textBox_numer_sluzbowy.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_numer_sluzbowy, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
            else
            {
                Border.SetHighlightColor(textBox_numer_sluzbowy, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }

        private void textBox_numer_prywatny_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[+]{0,1}[4]{0,1}[8]{0,1}[ ]{0,1}[0-9]{9}$");

            if (regex.Match(textBox_numer_prywatny.Text).Success || textBox_numer_prywatny.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_numer_prywatny, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
            else
            {
                Border.SetHighlightColor(textBox_numer_prywatny, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }

        private void textBox_email_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[a-zA-Z0-9_.!#$%&'*+/=?^_`{|}~-]{3,30}@[a-z0-9]{2,10}\.[a-z]{2,5}$");

            if (regex.Match(textBox_email.Text).Success || textBox_email.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_email, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
            else
            {
                Border.SetHighlightColor(textBox_email, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }

        private void textBox_czas_naprawy_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[0-9]{1,2}$");

            if (regex.Match(textBox_czas_naprawy.Text).Success || textBox_czas_naprawy.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_czas_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
            else
            {
                Border.SetHighlightColor(textBox_czas_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }

        private void textBox_koszt_naprawy_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[0-9]{2,4}$");

            if (regex.Match(textBox_koszt_naprawy.Text).Success || textBox_koszt_naprawy.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_koszt_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.None);
            }
            else
            {
                Border.SetHighlightColor(textBox_koszt_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }
    }
}
