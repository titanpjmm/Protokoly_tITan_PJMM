﻿using System;
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
using MySql.Data.MySqlClient;
using RawPrint;
using System.Drawing.Printing;
using System.Threading;

namespace Protokoly_tITan_PJMM
{
    public partial class Protokol : Form
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public static int Flaga_usercontrola = 0;
        public static bool Flaga_visible_ilosc_znakow = true;
        Thread thread;
        Thread thread1;
        Thread thread2;
        bool data_valid = true;
        

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

            else if(Flaga_usercontrola == 2)
            {
                //label_oryginal.Visible = true;
                var protokol_faktury = new Protokol_faktury();
                

                if (!panel_protokol.Contains(protokol_faktury.Instance))
                {
                    panel_protokol.Controls.Add(protokol_faktury.Instance);
                    protokol_faktury.Instance.Dock = DockStyle.Fill;
                    protokol_faktury.Instance.BringToFront();
                }
                else
                {
                    protokol_faktury.Instance.BringToFront();
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

        private void formularzFakturyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Flaga_usercontrola = 2;
            UserToFront();
        }


        // png - to - pdf - using - PDFCreator - 
        public Bitmap DrawControlToBitmap(Control control)
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
            if (Flaga_usercontrola == 0)
            {
                var list_of_all_data = new List<string>
            {
                all_data_from_protocol.numer_zlecenia,
                all_data_from_protocol.data_przyjecia,
                all_data_from_protocol.nazwa_klienta,
                all_data_from_protocol.adres_klienta,
                all_data_from_protocol.kod_pocztowy,
                all_data_from_protocol.prywatny_firma,
                all_data_from_protocol.numer_prywatny,
                //all_data_from_protocol.email,
                all_data_from_protocol.pc_laptop_telefon_drukarka,
                all_data_from_protocol.model,
                all_data_from_protocol.serial_number,
                all_data_from_protocol.opis,
                //all_data_from_protocol.czas_naprawy,
                all_data_from_protocol.koszt_naprawy,
                all_data_from_protocol.gotowka_karta_przelew
            };

                var list_of_all_data_popriety = new List<bool>
            {
                all_data_from_protocol.numer_zlecenia_poprawnosc,
                all_data_from_protocol.data_przyjecia_poprawnosc,
                all_data_from_protocol.kod_pocztowy_poprawnosc,
                //all_data_from_protocol.numer_sluzbowy_poprawnosc,
                all_data_from_protocol.numer_prywatny_poprawnosc,
                all_data_from_protocol.email_poprawnosc,
                //all_data_from_protocol.czas_naprawy_poprawnosc,
                all_data_from_protocol.koszt_naprawy_poprawnosc
             };

                bool go_to_database_flag = true;

                if (all_data_from_protocol.nip_klienta != string.Empty && all_data_from_protocol.numer_sluzbowy != string.Empty)
                {
                    if (all_data_from_protocol.nip_klienta_poprawnosc && all_data_from_protocol.numer_sluzbowy_poprawnosc)
                    {
                        go_to_database_flag = true;
                    }
                    else
                    {
                        go_to_database_flag = false;
                    }
                }
                else
                {
                    if (!all_data_from_protocol.flaga_nip && !all_data_from_protocol.flaga_sluzbowy)
                    {
                        go_to_database_flag = true;
                    }
                    else
                    {
                        go_to_database_flag = false;
                    }
                }

                foreach (string szukaj in list_of_all_data)
                {
                    if (szukaj == string.Empty)
                    {
                        go_to_database_flag = false;
                        break;
                    }
                }

                foreach (bool szukaj in list_of_all_data_popriety)
                {
                    if (szukaj == false)
                    {
                        go_to_database_flag = false;
                        break;
                    }
                }

                if (go_to_database_flag)
                {
                    Bitmap bm = DrawControlToBitmap(panel_protokol);
                    Save(bm, 3600, 4000, 1000000);

                    try
                    {
                        connection_and_methods.conn.Open();
                        string sql_query = "";

                        if (all_data_from_protocol.flaga_nip && all_data_from_protocol.flaga_sluzbowy)
                        {
                            sql_query = "INSERT INTO clients (client_name_surname, client_postal_city, client_street_house_number, client_phone_number, client_phone_job, client_email, client_nip)" +
                                        "VALUES(" + "'" + all_data_from_protocol.nazwa_klienta + "','" + all_data_from_protocol.kod_pocztowy + "','" + all_data_from_protocol.adres_klienta + "','"
                                        + all_data_from_protocol.numer_prywatny + "','" + all_data_from_protocol.numer_sluzbowy + "','" + all_data_from_protocol.email
                                        + "','" + all_data_from_protocol.nip_klienta + "'" + ");";

                            if (all_data_from_protocol.email == string.Empty)
                            {
                                sql_query = "INSERT INTO clients (client_name_surname, client_postal_city, client_street_house_number, client_phone_number, client_phone_job, client_nip) " +
                                            "VALUES(" + "'" + all_data_from_protocol.nazwa_klienta + "','" + all_data_from_protocol.kod_pocztowy + "','" + all_data_from_protocol.adres_klienta + "','"
                                            + all_data_from_protocol.numer_prywatny + "','" + all_data_from_protocol.numer_sluzbowy + "','" + all_data_from_protocol.nip_klienta + "'" + ");";
                            }

                            try
                            {
                                using (var cmdSel = new MySqlCommand(sql_query, connection_and_methods.conn))
                                {
                                    var fetch = new DataTable();
                                    var da = new MySqlDataAdapter(cmdSel);
                                    da.Fill(fetch);
                                }
                                MessageBox.Show("Dane prawidłowo wprowadzone do tabeli clientów.", "Information");

                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Warning");
                            }
                        }
                        else
                        {
                            sql_query = "INSERT INTO clients (client_name_surname, client_postal_city, client_street_house_number, client_phone_number, client_email)" +
                                        " VALUES(" + "'" + all_data_from_protocol.nazwa_klienta + "','" + all_data_from_protocol.kod_pocztowy + "','" + all_data_from_protocol.adres_klienta + "','"
                                        + all_data_from_protocol.numer_prywatny + "','" + all_data_from_protocol.email + "'" + ");"; ;

                            if (all_data_from_protocol.email == string.Empty)
                            {
                                sql_query = "INSERT INTO clients (client_name_surname, client_postal_city, client_street_house_number, client_phone_number)" +
                                    " VALUES(" + "'" + all_data_from_protocol.nazwa_klienta + "','" + all_data_from_protocol.kod_pocztowy + "','" + all_data_from_protocol.adres_klienta + "','"
                                    + all_data_from_protocol.numer_prywatny + "'" + ");";
                            }

                            try
                            {
                                using (var cmdSel = new MySqlCommand(sql_query, connection_and_methods.conn))
                                {
                                    var fetch = new DataTable();
                                    var da = new MySqlDataAdapter(cmdSel);
                                    da.Fill(fetch);
                                }
                                MessageBox.Show("Dane prawidłowo wprowadzone do tabeli clientów.", "Information");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Warning");
                            }
                        }

                        sql_query = "SELECT client_id FROM clients ORDER BY client_id DESC LIMIT 1";
                        string client_id = "";

                        try
                        {
                            using (var cmdSel = new MySqlCommand(sql_query, connection_and_methods.conn))
                            {
                                var fetch = new DataTable();
                                var da = new MySqlDataAdapter(cmdSel);
                                da.Fill(fetch);

                                var reader = cmdSel.ExecuteReader();

                                while (reader.Read())
                                {
                                    client_id = reader.GetInt32("client_id").ToString();
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Warning");
                        }

                        sql_query = "INSERT INTO protocols(protocol_number, client_id, failure_desc, serial_number, device_model, service_cost, payment_type, payment_service_state, protocol_add_time, protocol_achievmenet)" +
                                    " VALUES(" + "'" + all_data_from_protocol.numer_zlecenia + "','" + client_id + "','" + all_data_from_protocol.opis + "','"
                                    + all_data_from_protocol.serial_number + "','" + all_data_from_protocol.model + "','" + int.Parse(all_data_from_protocol.koszt_naprawy) + "','"
                                    + int.Parse(all_data_from_protocol.gotowka_karta_przelew) + "','" + 1 + "','" + all_data_from_protocol.data_przyjecia + "','" + all_data_from_protocol.czas_naprawy + "'" + ");";

                        try
                        {
                            using (var cmdSel = new MySqlCommand(sql_query, connection_and_methods.conn))
                            {
                                var fetch = new DataTable();
                                var da = new MySqlDataAdapter(cmdSel);
                                da.Fill(fetch);
                            }
                            MessageBox.Show("Dane prawidłowo wprowadzone do tabeli protokołów.", "Information");
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Warning");
                        }

                        connection_and_methods.conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Warning");
                    }

                    all_data_from_protocol.numer_zlecenia = "";
                    all_data_from_protocol.data_przyjecia = "";
                    all_data_from_protocol.nazwa_klienta = "";
                    all_data_from_protocol.adres_klienta = "";
                    all_data_from_protocol.kod_pocztowy = "";
                    all_data_from_protocol.prywatny_firma = "";
                    all_data_from_protocol.nip_klienta = "";
                    all_data_from_protocol.numer_sluzbowy = "";
                    all_data_from_protocol.numer_prywatny = "";
                    all_data_from_protocol.email = "";
                    all_data_from_protocol.pc_laptop_telefon_drukarka = "";
                    all_data_from_protocol.model = "";
                    all_data_from_protocol.serial_number = "";
                    all_data_from_protocol.opis = "";
                    //all_data_from_protocol.czas_naprawy = "";
                    all_data_from_protocol.koszt_naprawy = "";
                    all_data_from_protocol.gotowka_karta_przelew = "";
                    all_data_from_protocol.flaga_nip = false;
                    all_data_from_protocol.flaga_sluzbowy = false;

                    all_data_from_protocol.numer_zlecenia_poprawnosc = false;
                    all_data_from_protocol.data_przyjecia_poprawnosc = false;
                    all_data_from_protocol.kod_pocztowy_poprawnosc = false;
                    all_data_from_protocol.nip_klienta_poprawnosc = false;
                    all_data_from_protocol.numer_sluzbowy_poprawnosc = false;
                    all_data_from_protocol.numer_prywatny_poprawnosc = false;
                    all_data_from_protocol.email_poprawnosc = false;
                    //all_data_from_protocol.czas_naprawy_poprawnosc = false;
                    all_data_from_protocol.koszt_naprawy_poprawnosc = false;
                    all_data_from_protocol.nip_klienta_poprawnosc = false;

                    UserToFront();
                }
                else
                {
                    MessageBox.Show("Nie wszystkie dane zostały wpisane lub zostały niepoprawnie wpisane", "Warning");
                }
            }
            else if (Flaga_usercontrola == 2)
            {
                ActiveControl = label_copyright;
                //bool data_valid = true;
                data_valid = true;

                var list_invoice_data = new List<string>
                {
                    invoice_form_data.numer_faktury,
                    invoice_form_data.adres,
                    invoice_form_data.ilosc1,
                    invoice_form_data.nabywca,
                    invoice_form_data.nazwa1,
                    invoice_form_data.netto1,
                    invoice_form_data.nip,
                    invoice_form_data.numer_protokolu1,
                    invoice_form_data.procent_vat1
               };

                var invoice_nazwy = new List<string>
                   {
                        invoice_form_data.nazwa2,
                        invoice_form_data.nazwa3,
                        invoice_form_data.nazwa4
                   };

                foreach (string szukaj in list_invoice_data)
                {
                    if (szukaj == string.Empty)
                    {
                        data_valid = false;
                    }
                }

                connection_and_methods.conn.Open();

                string sql = "SELECT client_id FROM clients WHERE client_name_surname = " + "'" + invoice_form_data.nabywca + "'" + ";";
                string id_klienta = "";

                try
                {
                    using (var cmdSel = new MySqlCommand(sql, connection_and_methods.conn))
                    {
                        var fetch = new DataTable();
                        var da = new MySqlDataAdapter(cmdSel);
                        da.Fill(fetch);

                        var reader = cmdSel.ExecuteReader();

                        while (reader.Read())
                        {
                            id_klienta = reader.GetInt32("client_id").ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Warning");
                }

                if (id_klienta == string.Empty)
                {
                    data_valid = false;
                }
                else
                {
                    string sql2 = "SELECT client_nip, client_postal_city FROM clients WHERE client_id = " + "'" + id_klienta + "'" + ";";
                    string nip = "";
                    string adres = "";

                    try
                    {
                        using (var cmdSel = new MySqlCommand(sql2, connection_and_methods.conn))
                        {
                            var fetch = new DataTable();
                            var da = new MySqlDataAdapter(cmdSel);
                            da.Fill(fetch);

                            var reader = cmdSel.ExecuteReader();

                            while (reader.Read())
                            {
                                nip = reader.GetString("client_nip");
                                adres = reader.GetString("client_postal_city");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Warning");
                    }

                    if (nip != invoice_form_data.nip)
                    {
                        data_valid = false;
                    }

                }

                connection_and_methods.conn.Close();

                if (data_valid == true)
                {
                    int i = 0;
                    string[] protocols = new string[4];
                    string insert_invoice = "";
                    string protocols_string = "";

                    string update_protocol_status_1 = "";
                    string update_protocol_status_2 = "";
                    string update_protocol_status_3 = "";
                    string update_protocol_status_4 = "";

                    if (invoice_form_data.nazwa1 != string.Empty && i == 0)
                    {
                        string nazwa_1 = "";
                        for (int j = 0; j < 10; j++)
                        {
                            nazwa_1 += invoice_form_data.nazwa1[j];
                        }
                        protocols[i] = nazwa_1;
                        protocols_string += protocols[i];
                        update_protocol_status_1 = "UPDATE protocols SET isInvoice = 1 WHERE protocol_number = '" + protocols[i] + "';";
                        i++;
                    }

                    if (invoice_form_data.nazwa2 != string.Empty && i == 1)
                    {
                        string nazwa_2 = "";
                        for (int j = 0; j < 10; j++)
                        {
                            nazwa_2 += invoice_form_data.nazwa2[j];
                        }
                        protocols[i] = nazwa_2;
                        protocols_string += protocols[i];
                        update_protocol_status_2 = "UPDATE protocols SET isInvoice = 1 WHERE protocol_number = '" + protocols[i] + "';";
                        i++;
                    }

                    if (invoice_form_data.nazwa3 != string.Empty && i == 2)
                    {
                        string nazwa_3 = "";
                        for (int j = 0; j < 10; j++)
                        {
                            nazwa_3 += invoice_form_data.nazwa3[j];
                        }
                        protocols[i] = nazwa_3;
                        protocols_string += protocols[i];
                        update_protocol_status_3 = "UPDATE protocols SET isInvoice = 1 WHERE protocol_number = '" + protocols[i] + "';";
                        i++;
                    }

                    if (invoice_form_data.nazwa4 != string.Empty)
                    {
                        string nazwa_4 = "";
                        for (int j = 0; j < 10; j++)
                        {
                            nazwa_4 += invoice_form_data.nazwa4[j];
                        }
                        protocols[i] = nazwa_4;
                        protocols_string += protocols[i];
                        update_protocol_status_4 = "UPDATE protocols SET isInvoice = 1 WHERE protocol_number = '" + protocols[i] + "';";
                    }

                    string pkwiu = invoice_form_data.numer_protokolu1;


                    if (invoice_form_data.przyjmujacy != string.Empty)
                    {
                        insert_invoice = "INSERT INTO invoices (invoice_serial_number, invoice_number, fk_client_id, invoice_entity, invoice_date_end, invoice_protocols)" +
                        " VALUES(" + (invoice_form_data.serial + 1) + ",'" + invoice_form_data.numer_faktury + "','" + id_klienta + "','" + invoice_form_data.przyjmujacy + "','" + pkwiu + "','" + protocols_string + "');";
                    }
                    else
                    {
                        insert_invoice = "INSERT INTO invoices (invoice_serial_number, invoice_number, fk_client_id, invoice_date_end, invoice_protocols)" +
                        " VALUES(" + (invoice_form_data.serial + 1) + ",'" + invoice_form_data.numer_faktury + "','" + id_klienta + "','" + pkwiu + "','" + protocols_string + "');";
                    }


                    connection_and_methods.conn.Open();

                    try
                    {
                        using (var cmdSel = new MySqlCommand(insert_invoice, connection_and_methods.conn))
                        {
                            var fetch = new DataTable();
                            var da = new MySqlDataAdapter(cmdSel);
                            da.Fill(fetch);
                        }

                        if (update_protocol_status_1 != string.Empty)
                        {
                            try
                            {
                                using (var cmdSel = new MySqlCommand(update_protocol_status_1, connection_and_methods.conn))
                                {
                                    var fetch = new DataTable();
                                    var da = new MySqlDataAdapter(cmdSel);
                                    da.Fill(fetch);
                                }
                                //MessageBox.Show("Zaktualizowano stan protokołu numer " + protocols[0], "Information");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Warning");
                            }
                        }

                        if (update_protocol_status_2 != string.Empty)
                        {
                            try
                            {
                                using (var cmdSel = new MySqlCommand(update_protocol_status_2, connection_and_methods.conn))
                                {
                                    var fetch = new DataTable();
                                    var da = new MySqlDataAdapter(cmdSel);
                                    da.Fill(fetch);
                                }
                                //MessageBox.Show("Zaktualizowano stan protokołu numer " + protocols[1], "Information");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Warning");
                            }
                        }

                        if (update_protocol_status_3 != string.Empty)
                        {
                            try
                            {
                                using (var cmdSel = new MySqlCommand(update_protocol_status_3, connection_and_methods.conn))
                                {
                                    var fetch = new DataTable();
                                    var da = new MySqlDataAdapter(cmdSel);
                                    da.Fill(fetch);
                                }
                                //MessageBox.Show("Zaktualizowano stan protokołu numer " + protocols[2], "Information");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Warning");
                            }
                        }

                        if (update_protocol_status_4 != string.Empty)
                        {
                            try
                            {
                                using (var cmdSel = new MySqlCommand(update_protocol_status_4, connection_and_methods.conn))
                                {
                                    var fetch = new DataTable();
                                    var da = new MySqlDataAdapter(cmdSel);
                                    da.Fill(fetch);
                                }
                                //MessageBox.Show("Zaktualizowano stan protokołu numer " + protocols[3], "Information");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Warning");
                            }
                        }

                        //MessageBox.Show("Dane prawidłowo wprowadzone do tabeli faktur.", "Information");
                        connection_and_methods.conn.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Warning");
                        connection_and_methods.conn.Close();
                    }
                }
                else
                {
                    MessageBox.Show("Nie wprowadziłeś wymaganych danych / bądź są one niepoprawne");
                }

                /*Thread.Sleep(1000);
                metoda_oryginal_kopia();
                Thread.Sleep(1000);
                label_kopia.Visible = true;
                label_kopia.Refresh();
                label_oryginal.Visible = false;
                label_oryginal.Refresh();*/
                metoda_oryginal_kopia();
                //Thread.Sleep(1000);
                UserToFront();
            }       
        }

        private void metoda_oryginal_kopia()
        {
            Bitmap bm = DrawControlToBitmap(panel_protokol);
            Save(bm, 3600, 4000, 1000000);
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

            // Get an ImageCodecInfo object that represents the PNG codec.
            ImageCodecInfo imageCodecInfo = this.GetEncoderInfo(ImageFormat.Png);

            // Create an Encoder object for the Quality parameter.
            System.Drawing.Imaging.Encoder encoder = System.Drawing.Imaging.Encoder.Quality;

            // Create an EncoderParameters object. 
            EncoderParameters encoderParameters = new EncoderParameters(1);

            // Save the image as a PNG file with quality level.
            EncoderParameter encoderParameter = new EncoderParameter(encoder, quality);
            encoderParameters.Param[0] = encoderParameter;

            /*SaveFileDialog dialog = new SaveFileDialog
            {
                //Filter = "JPEG (*.jpeg)|*.jpeg | BMP (*.bmp)|*.bmp | GIF (*.gif)|*.gif"
                //Filter = "PNG (*.png)|*.png"
            };
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                newImage.Save(dialog.FileName, imageCodecInfo, encoderParameters);
            }*/

            if (Flaga_usercontrola == 2)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    newImage.Save(stream, imageCodecInfo, encoderParameters);

                    try
                    {
                        PrintDocument pd = new PrintDocument();
                        Margins margins = new Margins(0, 0, 120, 120);
                        pd.DefaultPageSettings.Margins = margins;
                        pd.PrintPage += (sender, args) =>
                        {
                            Image i = Image.FromStream(stream);
                            args.Graphics.DrawImage(i, args.MarginBounds);
                        };

                        pd.PrinterSettings.PrinterName = "PDFCreator";
                        pd.DocumentName = "faktura_" + invoice_form_data.numer_faktury;

                        pd.Print();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            else if(Flaga_usercontrola == 0)
            {
                using (MemoryStream stream = new MemoryStream())
                {
                    newImage.Save(stream, imageCodecInfo, encoderParameters);

                    try
                    {
                        PrintDocument pd = new PrintDocument();
                        Margins margins = new Margins(0, 0, 120, 120);
                        pd.DefaultPageSettings.Margins = margins;
                        pd.PrintPage += (sender, args) =>
                        {
                            Image i = Image.FromStream(stream);
                            args.Graphics.DrawImage(i, args.MarginBounds);
                        };

                        pd.PrinterSettings.PrinterName = "PDFCreator";
                        pd.DocumentName = "protokół_" + all_data_from_protocol.numer_zlecenia.Replace("/","-");
                        pd.Print();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
            
        }

        public ImageCodecInfo GetEncoderInfo(ImageFormat format)
        {
            return ImageCodecInfo.GetImageDecoders().SingleOrDefault(c => c.FormatID == format.Guid);
        }

        // // png - to - pdf - using - PDFCreator - 

        private void Protokol_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Bisque, 16), this.DisplayRectangle);
        }


    }

    public class myPanel : Panel
    {
        protected override void OnCreateControl()
        {
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            this.AutoScrollMinSize = new Size(100, 100);
            base.OnCreateControl();
        }
    }
}
