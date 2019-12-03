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
using MySql.Data.MySqlClient;

namespace Protokoly_tITan_PJMM
{
    public partial class Protokol_glowna : UserControl
    {
        private const int EM_SETCUEBANNER = 0x1501;
        private const int zmienna = 0;

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
            SendMessage(textBox_data_przyjecia.Handle, EM_SETCUEBANNER, 0, "yyyy - MM - dd hh: mm: ss");
            SendMessage(textBox_numer_zlecenia.Handle, EM_SETCUEBANNER, 0, "00000/yyyy");
            SendMessage(textBox_koszt_naprawy.Handle, EM_SETCUEBANNER, 0, "Szacowany koszt naprawy - PLN");
            SendMessage(textBox_model.Handle, EM_SETCUEBANNER, 0, "Model urządzenia");
            SendMessage(textBox_serial_number.Handle, EM_SETCUEBANNER, 0, "Numer seryjny urządzenia");

            textBox_nip_klienta.Visible = false;
            textBox_numer_sluzbowy.Visible = false;
            label8.Visible = false;

            textBox_data_przyjecia.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            all_data_from_protocol.data_przyjecia = textBox_data_przyjecia.Text;
            all_data_from_protocol.data_przyjecia_poprawnosc = true;

            string sql_query = "SELECT protocol_id FROM protocols ORDER BY protocol_id DESC LIMIT 1;";
            string id_string = "00000";

            try
            {
                connection_and_methods.conn.Open();

                

            
                using (var cmdSel = new MySqlCommand(sql_query, connection_and_methods.conn))
                {
                    var fetch = new DataTable();
                    var da = new MySqlDataAdapter(cmdSel);
                    da.Fill(fetch);

                    var reader = cmdSel.ExecuteReader();

                    while (reader.Read())
                    {
                        string id_5_numbers = (reader.GetInt32("protocol_id") + 1).ToString();

                        if (id_5_numbers.Count() == 1)
                        {
                            id_string = "0000" + id_5_numbers;
                        }
                        else if (id_5_numbers.Count() == 2)
                        {
                            id_string = "000" + id_5_numbers;
                        }
                        else if (id_5_numbers.Count() == 3)
                        {
                            id_string = "00" + id_5_numbers;
                        }
                        else if (id_5_numbers.Count() == 4)
                        {
                            id_string = "0" + id_5_numbers;
                        }
                        else if (id_5_numbers.Count() == 5)
                        {
                            id_string = id_5_numbers;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            connection_and_methods.conn.Close();

            textBox_numer_zlecenia.Text = id_string + "/" + DateTime.Now.ToString("yyyy");
            all_data_from_protocol.numer_zlecenia = textBox_numer_zlecenia.Text;
            all_data_from_protocol.numer_zlecenia_poprawnosc = true;
            all_data_from_protocol.email_poprawnosc = true;
            dateTimePicker_czas_realizacji.MinDate = DateTime.Now;
            all_data_from_protocol.czas_naprawy = dateTimePicker_czas_realizacji.Value.ToString("yyyy-MM-dd");

            checkboxy_wszystkie_uszkodzenia();
        }


        // lista uszkodzen
        public void checkboxy_wszystkie_uszkodzenia()
        {
            if (!radioButton_pc.Checked && !radioButton_telefon.Checked && !radioButton_laptop.Checked && !radioButton_drukarka.Checked)
            {
                foreach (CheckBox szukaj in groupBox_uszkodzenia.Controls.OfType<CheckBox>())
                {
                    szukaj.Enabled = false;
                    szukaj.Checked = false;
                }
            }
        }

        private void radioButton_pc_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_pc.Checked == true)
            {
                all_data_from_protocol.pc_laptop_telefon_drukarka = "pc";

                checkBox_touchpad.Enabled = false;
                checkBox_glowica.Enabled = false;
                checkBox_wyswietlacz.Enabled = false;
                checkBox_klawiatura.Enabled = false;

                checkBox_procesor.Enabled = true;
                checkBox_plyta.Enabled = true;
                checkBox_os.Enabled = true;
                checkBox_gniazda.Enabled = true;
                checkBox_zasilacz.Enabled = true;
                checkBox_ram.Enabled = true;
                checkBox_obudowa.Enabled = true;
            }
            checkboxy_wszystkie_uszkodzenia();
        }

        private void radioButton_telefon_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_telefon.Checked)
            {
                all_data_from_protocol.pc_laptop_telefon_drukarka = "telefon";

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
            checkboxy_wszystkie_uszkodzenia();
        }

        private void radioButton_laptop_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_laptop.Checked)
            {
                all_data_from_protocol.pc_laptop_telefon_drukarka = "laptop";

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
            checkboxy_wszystkie_uszkodzenia();
        }

        private void radioButton_drukarka_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_drukarka.Checked)
            {
                all_data_from_protocol.pc_laptop_telefon_drukarka = "drukarka";

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
            checkboxy_wszystkie_uszkodzenia();
        }

        private void radioButton_inne_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton_inne.Checked)
            {
                all_data_from_protocol.pc_laptop_telefon_drukarka = "inne";
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
        }

        private void checkBox_klawiatura_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_klawiatura.Checked)
            {

            }
        }

        private void checkBox_gniazda_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_gniazda.Checked)
            {

            }
        }

        private void checkBox_procesor_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_procesor.Checked)
            {

            }
        }

        private void checkBox_plyta_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_plyta.Checked)
            {

            }
        }

        private void checkBox_zasilacz_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_zasilacz.Checked)
            {

            }
        }

        private void checkBox_touchpad_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_touchpad.Checked)
            {

            }
        }

        private void checkBox_os_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_os.Checked)
            {

            }
        }

        private void checkBox_ram_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_ram.Checked)
            {

            }
        }

        private void checkBox_glowica_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_glowica.Checked)
            {

            }
        }

        private void checkBox_dotyk_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_dotyk.Checked)
            {

            }
        }

        private void checkBox_obudowa_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_obudowa.Checked)
            {

            }
        }
        // uszkodzenia

        private void radioButton_gotowka_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_gotowka.Checked)
            {
                all_data_from_protocol.gotowka_karta_przelew = "1";
            }

        }

        private void radioButton_karta_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_karta.Checked)
            {
                all_data_from_protocol.gotowka_karta_przelew = "2";
            }
        }

        private void radioButton_przelew_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton_przelew.Checked)
            {
                all_data_from_protocol.gotowka_karta_przelew = "3";
            }
        }

        // rodzaj platnosci

        private void textBox_numer_zlecenia_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]{5}\/[0-9]{4}$");

            if (regex.Match(textBox_numer_zlecenia.Text).Success || textBox_numer_zlecenia.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_numer_zlecenia, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
                all_data_from_protocol.numer_zlecenia_poprawnosc = true;
            }
            else
            {
                Border.SetHighlightColor(textBox_numer_zlecenia, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
                all_data_from_protocol.numer_zlecenia_poprawnosc = false;
            }

            all_data_from_protocol.numer_zlecenia = textBox_numer_zlecenia.Text;

            if(textBox_numer_zlecenia.Text == string.Empty)
            {
                all_data_from_protocol.numer_zlecenia_poprawnosc = false;
            }
        }

        private void textBox_data_przyjecia_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]{4}\-[0-9]{2}\-[0-9]{2}\ [0-9]{2}\:[0-9]{2}\:[0-9]{2}$");

            if (regex.Match(textBox_data_przyjecia.Text).Success || textBox_data_przyjecia.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_data_przyjecia, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
                all_data_from_protocol.data_przyjecia_poprawnosc = true;
            }
            else
            {
                Border.SetHighlightColor(textBox_data_przyjecia, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
                all_data_from_protocol.data_przyjecia_poprawnosc = false;
            }

            all_data_from_protocol.data_przyjecia = textBox_data_przyjecia.Text;

            if(textBox_data_przyjecia.Text == string.Empty)
            {
                all_data_from_protocol.data_przyjecia_poprawnosc = false;
            }
        }

        private void textBox_nazwa_klienta_TextChanged(object sender, EventArgs e)
        {
            all_data_from_protocol.nazwa_klienta = textBox_nazwa_klienta.Text;
        }

        private void textBox_adres_klienta_TextChanged(object sender, EventArgs e)
        {
            all_data_from_protocol.adres_klienta = textBox_adres_klienta.Text;
        }

        private void textBox_kod_pocztowy_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]{2}\-[0-9]{3}\ [A-ZĄĆĘŁŃÓŻŹ]{1}[a-ząćęłńóżź]{2,10}$");

            if (regex.Match(textBox_kod_pocztowy.Text).Success || textBox_kod_pocztowy.Text == string.Empty)
            { 
                Border.SetHighlightColor(textBox_kod_pocztowy, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
                all_data_from_protocol.kod_pocztowy_poprawnosc = true;
            }
            else
            {
                if(all_data_from_protocol.koszt_naprawy_poprawnosc)
                    Border.SetHighlightColor(textBox_kod_pocztowy, DevComponents.DotNetBar.Validator.eHighlightColor.Red);

                all_data_from_protocol.kod_pocztowy_poprawnosc = false;
            }

            all_data_from_protocol.kod_pocztowy = textBox_kod_pocztowy.Text;

            if(textBox_kod_pocztowy.Text == string.Empty)
            {
                all_data_from_protocol.kod_pocztowy_poprawnosc = false;
            }
        }

        private void textBox_nip_klienta_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[0-9]{3}\-[0-9]{3}\-[0-9]{2}\-[0-9]{2}$");

            if (regex.Match(textBox_nip_klienta.Text).Success || textBox_nip_klienta.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_nip_klienta, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
                all_data_from_protocol.nip_klienta_poprawnosc = true;
            }
            else
            {
                Border.SetHighlightColor(textBox_nip_klienta, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
                all_data_from_protocol.nip_klienta_poprawnosc = false;
            }

            all_data_from_protocol.nip_klienta = textBox_nip_klienta.Text;

            if(textBox_nip_klienta.Text == string.Empty)
            {
                all_data_from_protocol.nip_klienta_poprawnosc = false;
            }
        }

        private void textBox_numer_sluzbowy_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[+]{0,1}[4]{0,1}[8]{0,1}[ ]{0,1}[0-9]{9}$");

            if (regex.Match(textBox_numer_sluzbowy.Text).Success || textBox_numer_sluzbowy.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_numer_sluzbowy, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
                all_data_from_protocol.numer_sluzbowy_poprawnosc = true;
            }
            else
            {
                Border.SetHighlightColor(textBox_numer_sluzbowy, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
                all_data_from_protocol.numer_sluzbowy_poprawnosc = false;
            }

            all_data_from_protocol.numer_sluzbowy = textBox_numer_sluzbowy.Text;

            if(textBox_numer_sluzbowy.Text == string.Empty)
            {
                all_data_from_protocol.numer_sluzbowy_poprawnosc = false;
            }
        }

        private void textBox_numer_prywatny_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[+]{0,1}[4]{0,1}[8]{0,1}[ ]{0,1}[0-9]{9}$");

            if (regex.Match(textBox_numer_prywatny.Text).Success || textBox_numer_prywatny.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_numer_prywatny, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
                all_data_from_protocol.numer_prywatny_poprawnosc = true;
            }
            else
            {
                Border.SetHighlightColor(textBox_numer_prywatny, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
                all_data_from_protocol.numer_prywatny_poprawnosc = false;
            }

            all_data_from_protocol.numer_prywatny = textBox_numer_prywatny.Text;

            if(textBox_numer_prywatny.Text == string.Empty)
            {
                all_data_from_protocol.numer_prywatny_poprawnosc = false;
            }
        }

        private void textBox_email_TextChanged(object sender, EventArgs e)
        {
            Regex regex = new Regex(@"^[a-zA-Z0-9_.!#$%&'*+/=?^_`{|}~-]{3,30}@[a-z0-9]{2,10}\.[a-z]{2,5}$");

            if (regex.Match(textBox_email.Text).Success || textBox_email.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_email, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
                all_data_from_protocol.email_poprawnosc = true;
            }
            else
            {
                Border.SetHighlightColor(textBox_email, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
                all_data_from_protocol.email_poprawnosc = false;
            }

            all_data_from_protocol.email = textBox_email.Text;

        }

        private void textBox_model_TextChanged(object sender, EventArgs e)
        {
            all_data_from_protocol.model = textBox_model.Text;
        }

        private void textBox_serial_number_TextChanged(object sender, EventArgs e)
        {
            all_data_from_protocol.serial_number = textBox_serial_number.Text;
        }

        /*private void textBox_czas_naprawy_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[0-9]{1,2}$");

            if (regex.Match(textBox_czas_naprawy.Text).Success || textBox_czas_naprawy.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_czas_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
                all_data_from_protocol.czas_naprawy_poprawnosc = true;
            }
            else
            {
                Border.SetHighlightColor(textBox_czas_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
                all_data_from_protocol.czas_naprawy_poprawnosc = false;
            }

            all_data_from_protocol.czas_naprawy = textBox_czas_naprawy.Text;

            if(textBox_czas_naprawy.Text == string.Empty)
            {
                all_data_from_protocol.czas_naprawy_poprawnosc = false;
            }
        }*/

        private void textBox_koszt_naprawy_TextChanged(object sender, EventArgs e)
        {
            var regex = new Regex(@"^[0-9]{2,4}$");

            if (regex.Match(textBox_koszt_naprawy.Text).Success || textBox_koszt_naprawy.Text == string.Empty)
            {
                Border.SetHighlightColor(textBox_koszt_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.Blue);
                all_data_from_protocol.koszt_naprawy_poprawnosc = true;
            }
            else
            {
                Border.SetHighlightColor(textBox_koszt_naprawy, DevComponents.DotNetBar.Validator.eHighlightColor.Red);
                all_data_from_protocol.koszt_naprawy_poprawnosc = false;
            }

            all_data_from_protocol.koszt_naprawy = textBox_koszt_naprawy.Text;

            if(textBox_koszt_naprawy.Text == string.Empty)
            {
                all_data_from_protocol.koszt_naprawy_poprawnosc = false;
            }
        }

        private void radioButton_prywatna_CheckedChanged(object sender, EventArgs e)   // radiobutton w jednym groupboxie działają preciwstawnie to znaczy, że tylko jeden może być "true"
        {                                                                              // dzięki temu możemy ograniczyć ilość kodu na logikę
            if(radioButton_prywatna.Checked)
            {
                textBox_nip_klienta.Visible = false;
                all_data_from_protocol.nip_klienta = "";
                all_data_from_protocol.prywatny_firma = "Osoba prywatna";
                all_data_from_protocol.flaga_nip = false;
                all_data_from_protocol.flaga_sluzbowy = false;
                textBox_numer_sluzbowy.Visible = false;
                label8.Visible = false;
            }
        }

        private void radioButton_firma_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton_firma.Checked)
            {
                textBox_nip_klienta.Visible = true;
                textBox_numer_sluzbowy.Visible = true;
                all_data_from_protocol.prywatny_firma = "Firma";
                all_data_from_protocol.flaga_nip = true;
                all_data_from_protocol.flaga_sluzbowy = true;
                label8.Visible = true;
            }
        }

        private void richTextBox_opis_TextChanged(object sender, EventArgs e)
        {
            all_data_from_protocol.opis = richTextBox_opis.Text;
        }

        private void dateTimePicker_czas_realizacji_ValueChanged(object sender, EventArgs e)
        {
            all_data_from_protocol.czas_naprawy = dateTimePicker_czas_realizacji.Value.ToString("yyyy-MM-dd");
        }
    }


}
