using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;
using LiczbyNaSlowaNET;
using LiczbyNaSlowaNET.Dictionaries.Currencies;

namespace Protokoly_tITan_PJMM
{
    public partial class Protokol_faktury : UserControl
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public Protokol_faktury _instance;

        public Protokol_faktury Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Protokol_faktury();
                return _instance;
            }
        }

        List<TextBox> lista_brutto = new List<TextBox>();
        List<TextBox> lista_netto = new List<TextBox>();
        List<TextBox> lista_kwota_vat = new List<TextBox>();
        List<TextBox> lista_ilosc = new List<TextBox>();
        bool flaga_wiersz2 = false;
        bool flaga_wiersz3 = false;
        bool flaga_wiersz4 = false;

        public Protokol_faktury()
        {
            InitializeComponent();

            

            groupBox_2.Visible = false;
            groupBox_3.Visible = false;
            groupBox_4.Visible = false;

            foreach(GroupBox szukaj in panel_faktury.Controls.OfType<GroupBox>())
            {
                foreach(TextBox szukaj_t in szukaj.Controls.OfType<TextBox>())
                {
                    szukaj_t.BorderStyle = BorderStyle.None;
                }

                foreach(GroupBox szukaj1 in szukaj.Controls.OfType<GroupBox>())
                {
                    foreach(TextBox szukaj2 in szukaj1.Controls.OfType<TextBox>())
                    {
                        szukaj2.BorderStyle = BorderStyle.None;
                    }
                }
            }
            SendMessage(textBox_nazwa_nabywcy.Handle, EM_SETCUEBANNER, 0, "Nazwa (imię i nazwisko) lub/i nazwa firmy");
            SendMessage(textBox_adres_nabywcy.Handle, EM_SETCUEBANNER, 0, "Adres nabywcy");
            SendMessage(textBox_osoba_przyjmujaca.Handle, EM_SETCUEBANNER, 0, "Osoba przyjmująca");
            //SendMessage(textBox_nip_nabywcy.Handle, EM_SETCUEBANNER, 0, "NIP");

            lista_brutto.Add(textBox_brutto);
            lista_brutto.Add(textBox_brutto2);
            lista_brutto.Add(textBox_brutto3);
            lista_brutto.Add(textBox_brutto4);

            lista_netto.Add(textBox_netto);
            lista_netto.Add(textBox_netto2);
            lista_netto.Add(textBox_netto3);
            lista_netto.Add(textBox_netto4);

            lista_kwota_vat.Add(textBox_kwotavat);
            lista_kwota_vat.Add(textBox_kwotavat2);
            lista_kwota_vat.Add(textBox_kwotavat3);
            lista_kwota_vat.Add(textBox_kwotavat4);

            lista_ilosc.Add(textBox_ilosc);
            lista_ilosc.Add(textBox_ilosc2);
            lista_ilosc.Add(textBox_ilosc3);
            lista_ilosc.Add(textBox_ilosc4);

            textBox_brutto.Tag = "";
            textBox_brutto2.Tag = "";
            textBox_brutto3.Tag = "";
            textBox_brutto4.Tag = "";

            textBox_ilosc.Tag = "";
            textBox_ilosc2.Tag = "";
            textBox_ilosc3.Tag = "";
            textBox_ilosc4.Tag = "";

            textBox_kwotavat.Tag = "";
            textBox_kwotavat2.Tag = "";
            textBox_kwotavat3.Tag = "";
            textBox_kwotavat4.Tag = "";

            textBox_netto.Tag = "";
            textBox_netto2.Tag = "";
            textBox_netto3.Tag = "";
            textBox_netto4.Tag = "";

            textBox_wiersz2.BorderStyle = BorderStyle.None;
            textBox_wiersz3.BorderStyle = BorderStyle.None;
            textBox_wiersz4.BorderStyle = BorderStyle.None;

            string last_invoice_id = "SELECT invoice_serial_number FROM invoices ORDER BY invoice_serial_number DESC LIMIT 1;";
            string id_string = "00000";

            comboBox_numer_protokolu.Visible = false;
            comboBox_numer_protokolu2.Visible = false;
            comboBox_numer_protokolu3.Visible = false;
            comboBox_numer_protokolu4.Visible = false;

            connection_and_methods.conn.Open();

            try
            {
                using (var cmdSel = new MySqlCommand(last_invoice_id, connection_and_methods.conn))
                {
                    var fetch = new DataTable();
                    var da = new MySqlDataAdapter(cmdSel);
                    da.Fill(fetch);

                    var reader = cmdSel.ExecuteReader();

                    while (reader.Read())
                    {
                        string id_5_numbers = (reader.GetInt32("invoice_serial_number") + 1).ToString();
                        invoice_form_data.serial = reader.GetInt32("invoice_serial_number");

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

                    label_faktury.Text = "FV/" + id_string + "/2019";
                    invoice_form_data.numer_faktury = label_faktury.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Warning");
            }

            string connection_from_protocols = "SELECT protocol_number FROM protocols WHERE isInvoice=0;";

            try
            {
                using (var cmdSel = new MySqlCommand(connection_from_protocols, connection_and_methods.conn))
                {
                    var fetch = new DataTable();
                    var da = new MySqlDataAdapter(cmdSel);
                    da.Fill(fetch);

                    var reader = cmdSel.ExecuteReader();

                    while (reader.Read())
                    {
                        comboBox_numer_protokolu.Items.Add(reader.GetString("protocol_number"));
                        comboBox_numer_protokolu2.Items.Add(reader.GetString("protocol_number"));
                        comboBox_numer_protokolu3.Items.Add(reader.GetString("protocol_number"));
                        comboBox_numer_protokolu4.Items.Add(reader.GetString("protocol_number"));
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Informacja");
            }

            connection_and_methods.conn.Close();

            label_data_wystawienia.Text = DateTime.Now.ToString("yyyy-MM-dd");
            panel_faktury.Refresh();

        }

        // -------------------------------------------------------------------

        private void textBox_netto_TextChanged(object sender, EventArgs e)
        {
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {

                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        else if (lista_netto[i].Tag.ToString() == "8")
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();

                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto.Text != string.Empty && textBox_vat.Text != string.Empty && textBox_ilosc.Text != string.Empty)
                    {
                        textBox_brutto.Text = decimal.Round((decimal.Parse(textBox_ilosc.Text) * (decimal.Parse(textBox_netto.Text) + decimal.Parse(textBox_netto.Text) * decimal.Parse(textBox_vat.Text) / 100)), 2).ToString();
                        textBox_kwotavat.Text = decimal.Round((decimal.Parse(textBox_ilosc.Text) * (decimal.Parse(textBox_netto.Text) * decimal.Parse(textBox_vat.Text) / 100)), 2).ToString();
                        invoice_form_data.brutto1 = textBox_brutto.Text;
                        invoice_form_data.wartosc_vat1 = textBox_kwotavat.Text;
                    }
                    else if (textBox_vat.Text == string.Empty || textBox_netto.Text == string.Empty || textBox_ilosc.Text == string.Empty)
                    {
                        textBox_brutto.Text = "";
                        textBox_kwotavat.Text = "";
                        invoice_form_data.brutto1 = "";
                        invoice_form_data.wartosc_vat1 = "";
                }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
            invoice_form_data.netto1 = textBox_netto.Text;
        }

        private void textBox_netto2_TextChanged(object sender, EventArgs e)
        {
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {

                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        else if (lista_netto[i].Tag.ToString() == "8")
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();

                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto2.Text != string.Empty && textBox_vat2.Text != string.Empty && textBox_ilosc2.Text != string.Empty)
                {
                    textBox_brutto2.Text = decimal.Round((decimal.Parse(textBox_ilosc2.Text) * (decimal.Parse(textBox_netto2.Text) + decimal.Parse(textBox_netto2.Text) * decimal.Parse(textBox_vat2.Text) / 100)),2).ToString();
                    textBox_kwotavat2.Text = decimal.Round((decimal.Parse(textBox_ilosc2.Text) * (decimal.Parse(textBox_netto2.Text) * decimal.Parse(textBox_vat2.Text) / 100)),2).ToString();
                    invoice_form_data.brutto2 = textBox_brutto2.Text;
                    invoice_form_data.wartosc_vat2 = textBox_kwotavat2.Text;
                }
                else if (textBox_vat2.Text == string.Empty || textBox_netto2.Text == string.Empty || textBox_ilosc2.Text == string.Empty)
                {
                    textBox_brutto2.Text = "";
                    textBox_kwotavat2.Text = "";
                    invoice_form_data.brutto2 = "";
                    invoice_form_data.wartosc_vat2 = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
            invoice_form_data.netto2 = textBox_netto2.Text;
        }

        private void textBox_netto3_TextChanged(object sender, EventArgs e)
        {
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {

                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        else if (lista_netto[i].Tag.ToString() == "8")
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                    }
                }
                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();

                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto3.Text != string.Empty && textBox_vat3.Text != string.Empty && textBox_ilosc3.Text != string.Empty)
                {
                    textBox_brutto3.Text = decimal.Round((decimal.Parse(textBox_ilosc3.Text) * (decimal.Parse(textBox_netto3.Text) + decimal.Parse(textBox_netto3.Text) * decimal.Parse(textBox_vat3.Text) / 100)),2).ToString();
                    textBox_kwotavat3.Text = decimal.Round((decimal.Parse(textBox_ilosc3.Text) * (decimal.Parse(textBox_netto3.Text) * decimal.Parse(textBox_vat3.Text) / 100)),2).ToString();
                    invoice_form_data.brutto3 = textBox_brutto3.Text;
                    invoice_form_data.wartosc_vat3 = textBox_kwotavat3.Text;
                }
                else if (textBox_vat3.Text == string.Empty || textBox_netto3.Text == string.Empty || textBox_ilosc3.Text == string.Empty)
                {
                    textBox_brutto3.Text = "";
                    textBox_kwotavat3.Text = "";
                    invoice_form_data.brutto3 = "";
                    invoice_form_data.wartosc_vat3 = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
            invoice_form_data.netto3 = textBox_netto3.Text;
        }

        private void textBox_netto4_TextChanged(object sender, EventArgs e)
        {
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {

                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        else if (lista_netto[i].Tag.ToString() == "8")
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();

                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto4.Text != string.Empty && textBox_vat4.Text != string.Empty && textBox_ilosc4.Text != string.Empty)
                    {
                        textBox_brutto4.Text = decimal.Round((decimal.Parse(textBox_ilosc4.Text) * (decimal.Parse(textBox_netto4.Text) + decimal.Parse(textBox_netto4.Text) * decimal.Parse(textBox_vat4.Text) / 100)), 2).ToString();
                        textBox_kwotavat4.Text = decimal.Round((decimal.Parse(textBox_ilosc4.Text) * (decimal.Parse(textBox_netto4.Text) * decimal.Parse(textBox_vat4.Text) / 100)), 2).ToString();
                        invoice_form_data.brutto4 = textBox_brutto4.Text;
                        invoice_form_data.wartosc_vat4 = textBox_kwotavat4.Text;
                    }
                    else if (textBox_vat4.Text == string.Empty || textBox_netto4.Text == string.Empty || textBox_ilosc4.Text == string.Empty)
                    {
                        textBox_brutto4.Text = "";
                        textBox_kwotavat4.Text = "";
                        invoice_form_data.brutto4 = "";
                        invoice_form_data.wartosc_vat4 = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
            invoice_form_data.netto4 = textBox_netto4.Text;
        }

        // ----------------------------------------------------------------------

        // ---------------------------------------------------------------------

        private void textBox_vat_TextChanged(object sender, EventArgs e)
        {
            textBox_netto.Tag = textBox_vat.Text;
            textBox_brutto.Tag = textBox_vat.Text;
            textBox_kwotavat.Tag = textBox_vat.Text;

            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {
                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                        {
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        }
                        else if (lista_netto[i].Tag.ToString() == "8")
                        {
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        }
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();
                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto.Text != string.Empty && textBox_vat.Text != string.Empty && textBox_ilosc.Text != string.Empty && (textBox_vat.Text == "8" || textBox_vat.Text == "23"))
                {
                    textBox_brutto.Text = decimal.Round((decimal.Parse(textBox_ilosc.Text) * (decimal.Parse(textBox_netto.Text) + decimal.Parse(textBox_netto.Text) * decimal.Parse(textBox_vat.Text) / 100)),2).ToString();
                    textBox_kwotavat.Text = decimal.Round((decimal.Parse(textBox_ilosc.Text) * (decimal.Parse(textBox_netto.Text) * decimal.Parse(textBox_vat.Text) / 100)),2).ToString();
                    invoice_form_data.brutto1 = textBox_brutto.Text;
                    invoice_form_data.wartosc_vat1 = textBox_kwotavat.Text;
                }
                else if (textBox_vat.Text == string.Empty || textBox_netto.Text == string.Empty || textBox_ilosc.Text == string.Empty || textBox_vat.Text != "8" || textBox_vat.Text != "23")
                {
                    textBox_brutto.Text = "";
                    textBox_kwotavat.Text = "";
                    invoice_form_data.brutto1 = "";
                    invoice_form_data.wartosc_vat1 = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
            invoice_form_data.procent_vat1 = textBox_vat.Text;

        }

        private void textBox_vat2_TextChanged(object sender, EventArgs e)
        {
            textBox_netto2.Tag = textBox_vat2.Text;
            textBox_brutto2.Tag = textBox_vat2.Text;
            textBox_kwotavat2.Tag = textBox_vat2.Text;
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {
                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                        {
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        }
                        else if (lista_netto[i].Tag.ToString() == "8")
                        {
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        }
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();
                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto2.Text != string.Empty && textBox_vat2.Text != string.Empty && textBox_ilosc2.Text != string.Empty && (textBox_vat2.Text == "8" || textBox_vat2.Text == "23"))
                {
                    textBox_brutto2.Text = decimal.Round((decimal.Parse(textBox_ilosc2.Text) * (decimal.Parse(textBox_netto2.Text) + decimal.Parse(textBox_netto2.Text) * decimal.Parse(textBox_vat2.Text) / 100)),2).ToString();
                    textBox_kwotavat2.Text = decimal.Round((decimal.Parse(textBox_ilosc2.Text) * (decimal.Parse(textBox_netto2.Text) * decimal.Parse(textBox_vat2.Text) / 100)),2).ToString();
                    invoice_form_data.brutto2 = textBox_brutto2.Text;
                    invoice_form_data.wartosc_vat2 = textBox_kwotavat2.Text;
                }
                else if (textBox_vat2.Text == string.Empty || textBox_netto2.Text == string.Empty || textBox_ilosc2.Text == string.Empty || textBox_vat2.Text != "8" || textBox_vat2.Text != "23")
                {
                    textBox_brutto2.Text = "";
                    textBox_kwotavat2.Text = "";
                    invoice_form_data.brutto2 = textBox_brutto2.Text;
                    invoice_form_data.wartosc_vat2 = textBox_kwotavat2.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
        }

        private void textBox_vat3_TextChanged(object sender, EventArgs e)
        {
            textBox_netto3.Tag = textBox_vat3.Text;
            textBox_brutto3.Tag = textBox_vat3.Text;
            textBox_kwotavat3.Tag = textBox_vat3.Text;
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {
                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                        {
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        }
                        else if (lista_netto[i].Tag.ToString() == "8")
                        {
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        }
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();
                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto3.Text != string.Empty && textBox_vat3.Text != string.Empty && textBox_ilosc3.Text != string.Empty && (textBox_vat3.Text == "8" || textBox_vat3.Text == "23"))
                {
                    textBox_brutto3.Text = decimal.Round((decimal.Parse(textBox_ilosc3.Text) * (decimal.Parse(textBox_netto3.Text) + decimal.Parse(textBox_netto3.Text) * decimal.Parse(textBox_vat3.Text) / 100)),2).ToString();
                    textBox_kwotavat3.Text = decimal.Round((decimal.Parse(textBox_ilosc3.Text) * (decimal.Parse(textBox_netto3.Text) * decimal.Parse(textBox_vat3.Text) / 100)),2).ToString();
                    invoice_form_data.brutto3 = textBox_brutto3.Text;
                    invoice_form_data.wartosc_vat3 = textBox_kwotavat3.Text;
                }
                else if (textBox_vat3.Text == string.Empty || textBox_netto3.Text == string.Empty || textBox_ilosc3.Text == string.Empty || textBox_vat3.Text != "8" || textBox_vat3.Text != "23")
                {
                    textBox_brutto3.Text = "";
                    textBox_kwotavat3.Text = "";
                    invoice_form_data.brutto3 = textBox_brutto3.Text;
                    invoice_form_data.wartosc_vat3 = textBox_kwotavat3.Text;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
        }

        private void textBox_vat4_TextChanged(object sender, EventArgs e)
        {
            textBox_netto4.Tag = textBox_vat4.Text;
            textBox_brutto4.Tag = textBox_vat4.Text;
            textBox_kwotavat4.Tag = textBox_vat4.Text;
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {
                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                        {
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        }
                        else if (lista_netto[i].Tag.ToString() == "8")
                        {
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        }
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();
                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto4.Text != string.Empty && textBox_vat4.Text != string.Empty && textBox_ilosc4.Text != string.Empty && (textBox_vat4.Text == "8" || textBox_vat4.Text == "23"))
                {
                    textBox_brutto4.Text = decimal.Round((decimal.Parse(textBox_ilosc4.Text) * (decimal.Parse(textBox_netto4.Text) + decimal.Parse(textBox_netto4.Text) * decimal.Parse(textBox_vat4.Text) / 100)),2).ToString();
                    textBox_kwotavat4.Text = decimal.Round((decimal.Parse(textBox_ilosc4.Text) * (decimal.Parse(textBox_netto4.Text) * decimal.Parse(textBox_vat4.Text) / 100)),2).ToString(); 
                }
                else if (textBox_vat4.Text == string.Empty || textBox_netto4.Text == string.Empty || textBox_ilosc4.Text == string.Empty || textBox_vat4.Text != "8" || textBox_vat4.Text != "23")
                {
                    textBox_brutto4.Text = "";
                    textBox_kwotavat4.Text = "";
                }
                invoice_form_data.brutto4 = textBox_brutto4.Text;
                invoice_form_data.wartosc_vat4 = textBox_kwotavat4.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
        }

        // -----------------------------------------------------------------------

        private void textBox_ilosc_TextChanged(object sender, EventArgs e)
        {
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {

                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if(lista_netto[i].Tag.ToString() == "23")
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        else if(lista_netto[i].Tag.ToString() == "8")
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();
                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto.Text != string.Empty && textBox_vat.Text != string.Empty && textBox_ilosc.Text != string.Empty)
                {
                    textBox_brutto.Text = decimal.Round((decimal.Parse(textBox_ilosc.Text) * (decimal.Parse(textBox_netto.Text) + decimal.Parse(textBox_netto.Text) * decimal.Parse(textBox_vat.Text) / 100)),2).ToString();
                    textBox_kwotavat.Text = decimal.Round((decimal.Parse(textBox_ilosc.Text) * (decimal.Parse(textBox_netto.Text) * decimal.Parse(textBox_vat.Text) / 100)),2).ToString();
                }
                else if (textBox_vat.Text == string.Empty || textBox_netto.Text == string.Empty || textBox_ilosc.Text == string.Empty)
                {
                    textBox_brutto.Text = "";
                    textBox_kwotavat.Text = "";
                }
                invoice_form_data.brutto1 = textBox_brutto.Text;
                invoice_form_data.wartosc_vat1 = textBox_kwotavat.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
            invoice_form_data.ilosc1 = textBox_ilosc.Text;
        }

        private void textBox_ilosc2_TextChanged(object sender, EventArgs e)
        {
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {

                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        else if (lista_netto[i].Tag.ToString() == "8")
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();
                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto2.Text != string.Empty && textBox_vat2.Text != string.Empty && textBox_ilosc2.Text != string.Empty)
                {
                    textBox_brutto2.Text = decimal.Round((decimal.Parse(textBox_ilosc2.Text) * (decimal.Parse(textBox_netto2.Text) + decimal.Parse(textBox_netto2.Text) * decimal.Parse(textBox_vat2.Text) / 100)),2).ToString();
                    textBox_kwotavat2.Text = decimal.Round((decimal.Parse(textBox_ilosc2.Text) * (decimal.Parse(textBox_netto2.Text) * decimal.Parse(textBox_vat2.Text) / 100)),2).ToString();
                }
                else if (textBox_vat2.Text == string.Empty || textBox_netto2.Text == string.Empty || textBox_ilosc2.Text == string.Empty)
                {
                    textBox_brutto2.Text = "";
                    textBox_kwotavat2.Text = "";
                }
                invoice_form_data.brutto2 = textBox_brutto2.Text;
                invoice_form_data.wartosc_vat2 = textBox_kwotavat2.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
        }

        private void textBox_ilosc3_TextChanged(object sender, EventArgs e)
        {
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {

                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        else if (lista_netto[i].Tag.ToString() == "8")
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();
                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;


                if (textBox_netto3.Text != string.Empty && textBox_vat3.Text != string.Empty && textBox_ilosc3.Text != string.Empty)
                {
                    textBox_brutto3.Text = decimal.Round((decimal.Parse(textBox_ilosc3.Text) * (decimal.Parse(textBox_netto3.Text) + decimal.Parse(textBox_netto3.Text) * decimal.Parse(textBox_vat3.Text) / 100)),2).ToString();
                    textBox_kwotavat3.Text = decimal.Round((decimal.Parse(textBox_ilosc3.Text) * (decimal.Parse(textBox_netto3.Text) * decimal.Parse(textBox_vat3.Text) / 100)),2).ToString();
                }
                else if (textBox_vat3.Text == string.Empty || textBox_netto3.Text == string.Empty || textBox_ilosc3.Text == string.Empty)
                {
                    textBox_brutto3.Text = "";
                    textBox_kwotavat3.Text = "";
                }
                invoice_form_data.brutto3 = textBox_brutto3.Text;
                invoice_form_data.wartosc_vat3 = textBox_kwotavat3.Text;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();
            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
        }

        private void textBox_ilosc4_TextChanged(object sender, EventArgs e)
        {
            decimal licz_23 = 0;
            decimal licz_8 = 0;
            try
            {

                for (int i = 0; i < lista_ilosc.Count; i++)
                {
                    if (lista_ilosc[i].Text != string.Empty && lista_netto[i].Text != string.Empty)
                    {
                        if (lista_netto[i].Tag.ToString() == "23")
                            licz_23 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                        else if (lista_netto[i].Tag.ToString() == "8")
                            licz_8 += decimal.Parse(lista_ilosc[i].Text) * decimal.Parse(lista_netto[i].Text);
                    }
                }

                textBox_netto_calosc_23.Text = decimal.Round(licz_23, 2).ToString();
                textBox_netto_calosc_8.Text = decimal.Round(licz_8, 2).ToString();
                invoice_form_data.netto_cale_23 = textBox_netto_calosc_23.Text;
                invoice_form_data.netto_cale_8 = textBox_netto_calosc_8.Text;

                if (textBox_netto4.Text != string.Empty && textBox_vat4.Text != string.Empty && textBox_ilosc4.Text != string.Empty)
                    {
                        textBox_brutto4.Text = decimal.Round((decimal.Parse(textBox_ilosc4.Text) * (decimal.Parse(textBox_netto4.Text) + decimal.Parse(textBox_netto4.Text) * decimal.Parse(textBox_vat4.Text) / 100)), 2).ToString();
                        textBox_kwotavat4.Text = decimal.Round((decimal.Parse(textBox_ilosc4.Text) * (decimal.Parse(textBox_netto4.Text) * decimal.Parse(textBox_vat4.Text) / 100)), 2).ToString();
                    }
                    else if (textBox_vat4.Text == string.Empty || textBox_netto4.Text == string.Empty || textBox_ilosc4.Text == string.Empty)
                    {
                        textBox_brutto4.Text = "";
                        textBox_kwotavat4.Text = "";
                    }
                    invoice_form_data.brutto4 = textBox_brutto4.Text;
                    invoice_form_data.wartosc_vat4 = textBox_kwotavat4.Text;
            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            textBox_brutto_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23 + licz_23, 2).ToString();
            textBox_brutto_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8 + licz_8, 2).ToString();

            textBox_vat_calosc_23.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_23.Text) / 100 * licz_23, 2).ToString();
            textBox_vat_calosc_8.Text = decimal.Round(decimal.Parse(textBox_stawka_vat_calosc_8.Text) / 100 * licz_8, 2).ToString();

            invoice_form_data.brutto_cale_23 = textBox_brutto_calosc_23.Text;
            invoice_form_data.brutto_cale_8 = textBox_brutto_calosc_8.Text;
            invoice_form_data.vat_cale_23 = textBox_vat_calosc_23.Text;
            invoice_form_data.vat_cale_8 = textBox_vat_calosc_8.Text;
        }

        private void textBox_brutto_calosc_23_TextChanged(object sender, EventArgs e)
        {
            try
            {
                var options = new NumberToTextOptions
                {
                    Stems = true,
                    Currency = Currency.PLN
                };

                if (textBox_brutto_calosc_8.Text != string.Empty)
                {
                    label_cala_kwota.Text = (decimal.Parse(textBox_brutto_calosc_23.Text) + decimal.Parse(textBox_brutto_calosc_8.Text)).ToString() + " PLN";
                    decimal calosc = decimal.Parse(textBox_brutto_calosc_23.Text) + decimal.Parse(textBox_brutto_calosc_8.Text);
                    string liczba_slownie = NumberToText.Convert((int)calosc, options);
                    label_zaplata_slownie_23.Text = "Słownie: " + liczba_slownie /*+ " PLN "*/ + " " + ((int)((calosc - (int)calosc) * 100)).ToString() + "/100";
                }
                else
                {
                    label_cala_kwota.Text = decimal.Parse(textBox_brutto_calosc_23.Text).ToString() + " PLN";
                    decimal calosc = decimal.Parse(textBox_brutto_calosc_23.Text);
                    string liczba_slownie = NumberToText.Convert((int)calosc, options);
                    label_zaplata_slownie_23.Text = "Słownie: " + liczba_slownie /*+ " PLN "*/ + " " + ((int)((calosc - (int)calosc) * 100)).ToString() + "/100";
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void textBox_brutto_calosc_8_TextChanged(object sender, EventArgs e)
        {
            var options = new NumberToTextOptions
            {
                Stems = true,
                Currency = Currency.PLN
            };

            if (textBox_brutto_calosc_23.Text != string.Empty)
            {
                label_cala_kwota.Text = (decimal.Parse(textBox_brutto_calosc_23.Text) + decimal.Parse(textBox_brutto_calosc_8.Text)).ToString() + " PLN";
                decimal calosc = decimal.Parse(textBox_brutto_calosc_23.Text) + decimal.Parse(textBox_brutto_calosc_8.Text);
                string liczba_slownie = NumberToText.Convert((int)calosc, options);
                label_zaplata_slownie_23.Text = "Słownie: " + liczba_slownie /*+ " PLN "*/ + " " + ((int)((calosc - (int)calosc) * 100)).ToString() + "/100";
            }
            else
            {
                label_cala_kwota.Text = decimal.Parse(textBox_brutto_calosc_8.Text).ToString() + " PLN";
                decimal calosc = decimal.Parse(textBox_brutto_calosc_8.Text);
                string liczba_slownie = NumberToText.Convert((int)calosc, options);
                label_zaplata_slownie_23.Text = "Słownie: " + liczba_slownie /*+ " PLN "*/ + " " + ((int)((calosc - (int)calosc) * 100)).ToString() + "/100";

            }
        }

        private void textBox_wiersz2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (flaga_wiersz2)
            {
                groupBox_2.Visible = false;
                flaga_wiersz2 = false;
                ActiveControl = label_nazwa_firmy_titan;
            }
            else
            {
                groupBox_2.Visible = true;
                flaga_wiersz2 = true;
                ActiveControl = label_nazwa_firmy_titan;
            }
            panel_faktury.Refresh();
        }

        private void textBox_wiersz3_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (flaga_wiersz3)
            {
                groupBox_3.Visible = false;
                flaga_wiersz3 = false;
                ActiveControl = label_nazwa_firmy_titan;
            }
            else
            {
                groupBox_3.Visible = true;
                flaga_wiersz3 = true;
                ActiveControl = label_nazwa_firmy_titan;
            }
            panel_faktury.Refresh();
        }

        private void textBox_wiersz4_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (flaga_wiersz4)
            {
                groupBox_4.Visible = false;
                flaga_wiersz4 = false;
                ActiveControl = label_nazwa_firmy_titan;
            }
            else
            {
                groupBox_4.Visible = true;
                flaga_wiersz4 = true;
                ActiveControl = label_nazwa_firmy_titan;
            }
            panel_faktury.Refresh();
        }

        private void panel_faktury_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = panel_faktury.CreateGraphics();
            Pen pioro = new Pen(Color.Black, 3);

            g.DrawRectangle(new Pen(Color.Black, 5), groupBox6_razem_zaplata_23.Location.X, groupBox6_razem_zaplata_23.Location.Y, groupBox6_razem_zaplata_23.Size.Width - 1, groupBox6_razem_zaplata_23.Size.Height - 1);

            g.DrawRectangle(pioro, groupBox_1.Location.X, groupBox_1.Location.Y , groupBox_1.Size.Width - 1, groupBox_1.Size.Height - 1);
            g.DrawRectangle(new Pen(Color.Black, 5), groupBox_wystawil.Location.X, groupBox_wystawil.Location.Y, groupBox_wystawil.Size.Width - 1, groupBox_wystawil.Height - 1);
            g.DrawRectangle(new Pen(Color.Black, 5), groupBox_odebral.Location.X, groupBox_odebral.Location.Y, groupBox_odebral.Size.Width - 1, groupBox_odebral.Size.Height - 1);

            if (flaga_wiersz2)
                g.DrawRectangle(pioro, groupBox_2.Location.X, groupBox_2.Location.Y, groupBox_2.Size.Width - 1, groupBox_2.Size.Height - 1);
            else
                g.DrawRectangle(new Pen(Color.White, 3), groupBox_2.Location.X, groupBox_2.Location.Y, groupBox_2.Size.Width - 1, groupBox_2.Size.Height);

            if(flaga_wiersz3)
                g.DrawRectangle(pioro, groupBox_3.Location.X, groupBox_3.Location.Y, groupBox_3.Size.Width - 1, groupBox_3.Size.Height - 1);
            else
                g.DrawRectangle(new Pen(Color.White, 3), groupBox_3.Location.X, groupBox_3.Location.Y, groupBox_3.Size.Width - 1, groupBox_3.Size.Height - 1);

            if(flaga_wiersz4)
                g.DrawRectangle(pioro, groupBox_4.Location.X, groupBox_4.Location.Y, groupBox_4.Size.Width - 1, groupBox_4.Size.Height - 1);
            else
                g.DrawRectangle(new Pen(Color.White, 3), groupBox_4.Location.X, groupBox_4.Location.Y, groupBox_4.Size.Width - 1, groupBox_4.Size.Height - 1);
        }

        private void panel_faktury_Click(object sender, EventArgs e)
        {
            ActiveControl = label_nazwa_firmy_titan;
            comboBox_numer_protokolu.Visible = false;
            comboBox_numer_protokolu2.Visible = false;
            comboBox_numer_protokolu3.Visible = false;
            comboBox_numer_protokolu4.Visible = false;
        }

        public static string NumberToWords(int number)
        {
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + NumberToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += NumberToWords(number / 1000000) + " milion ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += NumberToWords(number / 1000) + " tysiąc ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += NumberToWords(number / 100) + " sto ";
                number %= 100;
            }

            if (number > 0)
            {

                var unitsMap = new[] { "zero", "jeden", "dwa", "trzy", "cztery", "pięć", "sześć", "siedem", "osiem", "dziewięć", "dziesięć", "jedenaście", "dwanaście", "trzynaście", "czternaście", "piętnaście", "szesnaście", "siedemnaście", "osiemnaście", "dziewiętnaście" };
                var tensMap = new[] { "zero", "dziesięć", "dwadzieścia", "trzydzieści", "czterdzieści", "pięćdziesiąt", "sześćdziesiąt", "siedemdziesiąt", "osiemdziesiąt", "dziewięćdziesiąt" };

                if (number < 20)
                    words += unitsMap[number];
                else
                {
                    words += tensMap[number / 10];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[number % 10];
                }
            }

            return words;
        }

        private void textBox_nazwa_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.nazwa1 = textBox_nazwa.Text;
        }

        private void textBox_nazwa2_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.nazwa2 = textBox_nazwa2.Text;         
        }

        private void textBox_nazwa3_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.nazwa3 = textBox_nazwa3.Text;
        }

        private void textBox_nazwa4_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.nazwa4 = textBox_nazwa4.Text;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.numer_protokolu1 = textBox_zakonczenie_uslugi.Text;
        }

        private void textBox_pkwiu2_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.numer_protokolu2 = textBox_zakonczenie_uslugi2.Text;
        }

        private void textBox_pkwiu3_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.numer_protokolu3 = textBox_zakonczenie_uslugi3.Text;
        }

        private void textBox_pkwiu4_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.numer_protokolu4 = textBox_zakonczenie_uslugi4.Text;
        }

        private void textBox_nazwa_nabywcy_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.nabywca = textBox_nazwa_nabywcy.Text;
        }

        private void textBox_adres_nabywcy_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.adres = textBox_adres_nabywcy.Text;
        }

        private void textBox_nip_nabywcy_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.nip = textBox_nip_nabywcy.Text;
        }

        private void textBox_nazwa_Click(object sender, EventArgs e)
        {
            comboBox_numer_protokolu.Visible = true;
        }

        private void textBox_nazwa2_Click(object sender, EventArgs e)
        {
            comboBox_numer_protokolu2.Visible = true;
        }

        private void textBox_nazwa3_Click(object sender, EventArgs e)
        {
            comboBox_numer_protokolu3.Visible = true;
        }

        private void textBox_nazwa4_Click(object sender, EventArgs e)
        {
            comboBox_numer_protokolu4.Visible = true;
        }

        private void comboBox_numer_protokolu_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_nazwa.Text = comboBox_numer_protokolu.SelectedItem.ToString();
            comboBox_numer_protokolu.Visible = false;
            ActiveControl = textBox_zakonczenie_uslugi;
            textBox_zakonczenie_uslugi.Text = Data_wczytania_uslugi(textBox_nazwa.Text);
        }

        private void comboBox_numer_protokolu2_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_nazwa2.Text = comboBox_numer_protokolu2.SelectedItem.ToString();
            comboBox_numer_protokolu2.Visible = false;
            ActiveControl = textBox_zakonczenie_uslugi2;
            textBox_zakonczenie_uslugi2.Text = Data_wczytania_uslugi(textBox_nazwa2.Text);
        }

        private void comboBox_numer_protokolu3_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_nazwa3.Text = comboBox_numer_protokolu3.SelectedItem.ToString();
            comboBox_numer_protokolu3.Visible = false;
            ActiveControl = textBox_zakonczenie_uslugi3;
            textBox_zakonczenie_uslugi3.Text = Data_wczytania_uslugi(textBox_nazwa3.Text);
        }

        private void comboBox_numer_protokolu4_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox_nazwa4.Text = comboBox_numer_protokolu4.SelectedItem.ToString();
            comboBox_numer_protokolu4.Visible = false;
            ActiveControl = textBox_zakonczenie_uslugi4;
            textBox_zakonczenie_uslugi4.Text = Data_wczytania_uslugi(textBox_nazwa4.Text);
        }

        private string Data_wczytania_uslugi(string tekst)
        {
            string polaczenie = "SELECT protocol_achievmenet FROM protocols WHERE protocol_number='" + tekst +"';";
            string Return = "";
            
            connection_and_methods.conn.Open();
            try
            {
                using (var cmdSel = new MySqlCommand(polaczenie, connection_and_methods.conn))
                {
                    var fetch = new DataTable();
                    var da = new MySqlDataAdapter(cmdSel);
                    da.Fill(fetch);

                    var reader = cmdSel.ExecuteReader();

                    while (reader.Read())
                    {
                        Return = reader.GetString("protocol_achievmenet");
                    }
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Informacja");
            }
            connection_and_methods.conn.Close();
            Return = Convert.ToDateTime(Return).ToString("yyyy-MM-dd");

            return Return;
        }

        private void textBox_osoba_przyjmujaca_TextChanged(object sender, EventArgs e)
        {
            invoice_form_data.przyjmujacy = textBox_osoba_przyjmujaca.Text;
        }
        
    }


}
