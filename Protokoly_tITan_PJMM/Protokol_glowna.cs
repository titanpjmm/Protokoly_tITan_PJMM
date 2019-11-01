using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Imaging;
using Spire.Pdf;
using Spire.Pdf.Graphics;
using System.Drawing.Drawing2D;
using System.Text.RegularExpressions;
using DevComponents.WinForms.Drawing;
using System.Runtime.InteropServices;

namespace Protokoly_tITan_PJMM
{
    public partial class Protokol_glowna : UserControl
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public Protokol_glowna _instance;


        public Protokol_glowna Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Protokol_glowna();
                return _instance;
            }
        }


        public Protokol_glowna()
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
        }


        // lista uszkodzen
        public void checkboxy_wszystkie_uszkodzenia()
        {
            if (!checkBox_pc.Checked && !checkBox_telefon.Checked && !checkBox_laptop.Checked && !checkBox_drukarka.Checked)
            {
                foreach (CheckBox szukaj in groupBox_uszkodzenia.Controls.OfType<CheckBox>())
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
                checkBox_wyswietlacz.Enabled = false;

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
            if (checkBox_wyswietlacz.Checked)
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

            if (regex.Match(textBox_numer_zlecenia.Text).Success || textBox_numer_zlecenia.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_numer_zlecenia, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
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
                Border.SetHighlightColor(textBox_data_przyjecia, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
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
                Border.SetHighlightColor(textBox_kod_pocztowy, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
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
                Border.SetHighlightColor(textBox_nip_klienta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
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
                Border.SetHighlightColor(textBox_pesel_klienta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
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
                Border.SetHighlightColor(textBox_numer_sluzbowy, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
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
                Border.SetHighlightColor(textBox_numer_prywatny, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
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
                Border.SetHighlightColor(textBox_email, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
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
                Border.SetHighlightColor(textBox_czas_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
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
                Border.SetHighlightColor(textBox_koszt_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
            }
            else
            {
                Border.SetHighlightColor(textBox_koszt_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
            }
        }

    }
}
