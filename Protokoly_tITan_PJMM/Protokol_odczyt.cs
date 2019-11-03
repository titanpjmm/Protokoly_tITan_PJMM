using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Protokoly_tITan_PJMM
{
    public partial class Protokol_odczyt : UserControl
    {
        private const int EM_SETCUEBANNER = 0x1501;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern Int32 SendMessage(IntPtr hWnd, int msg, int wParam, [MarshalAs(UnmanagedType.LPWStr)]string lParam);

        public Protokol_odczyt _instance;

        public Protokol_odczyt Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new Protokol_odczyt();
                return _instance;
            }
        }

        public Protokol_odczyt()
        {
            InitializeComponent();
            SendMessage(textBox_data_uplyw.Handle, EM_SETCUEBANNER, 0, "Termin realizacji");
            SendMessage(textBox_data_oddania.Handle, EM_SETCUEBANNER, 0, "Data przyjęcia sprzętu");
            SendMessage(textBox_numer_zlecenia.Handle, EM_SETCUEBANNER, 0, "Numer zlecenia");
            SendMessage(textBox_nazwa_klienta.Handle, EM_SETCUEBANNER, 0, "Nazwa klienta");
            SendMessage(textBox_numer_nip.Handle, EM_SETCUEBANNER, 0, "Numer NIP");

            textBox_data_uplyw.Enabled = false;
            textBox_data_oddania.Enabled = false;
            textBox_numer_zlecenia.Enabled = false;
            textBox_nazwa_klienta.Enabled = false;
            textBox_numer_nip.Enabled = false;
            groupBox_status_zgl.Enabled = false;

            richTextBox1.Visible = false;
            label_nr_zlecenia.Visible = false;
            label_nazwa_klienta.Visible = false;
            label_nip.Visible = false;
            label_I_linia.Visible = false;
            label_kod_miasto.Visible = false;
            richTextBox_opis.Visible = false;
            label_tel_sluzbowy.Visible = false;
            label_tel_prywatny.Visible = false;
            label_mail.Visible = false;
            label_termin_zlecenia.Visible = false;
            label_data_godzina_przyjecia.Visible = false;
            label_typ_urzadzenia.Visible = false;
            label_szacowany_koszt.Visible = false;
        }
        private void shut_all()
        {
            textBox_data_uplyw.Enabled = false;
            textBox_data_oddania.Enabled = false;
            textBox_numer_zlecenia.Enabled = false;
            textBox_nazwa_klienta.Enabled = false;
            textBox_numer_nip.Enabled = false;
            groupBox_status_zgl.Enabled = false;
        }
        private void Filtr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(comboBox_Filtr.SelectedItem.ToString() == "Data upływu terminu")
            {
                shut_all();
                textBox_data_uplyw.Enabled = true;
            }
            else if(comboBox_Filtr.SelectedItem.ToString() == "Data wpływu zlecenia")
            {
                shut_all();
                textBox_data_oddania.Enabled = true;
            }
            else if (comboBox_Filtr.SelectedItem.ToString() == "Numer zlecenia")
            {
                shut_all();
                textBox_numer_zlecenia.Enabled = true;
            }
            else if (comboBox_Filtr.SelectedItem.ToString() == "Numer NIP")
            {
                shut_all();
                textBox_numer_nip.Enabled = true;
            }
            else if (comboBox_Filtr.SelectedItem.ToString() == "Nazwa klienta")
            {
                shut_all(); 
                textBox_nazwa_klienta.Enabled = true;
            }
            else if (comboBox_Filtr.SelectedItem.ToString() == "Status zlecenia")
            {
                shut_all();
                groupBox_status_zgl.Enabled = true;
            }

            richTextBox1.Visible = true;
        }
    }
}
