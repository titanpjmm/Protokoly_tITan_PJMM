using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using MySql.Data.MySqlClient;
using System.Data;
using System.Runtime.InteropServices;

namespace Protokoly_tITan_PJMM
{
    public static class connection_and_methods
    {
        public static readonly string connection = "SERVER=77.79.251.52; DATABASE=pawelskie_protocols; UID=pawelskie_titan; PASSWORD=*9UCSn2g!jQ8a0;";
        public static readonly MySqlConnection conn = new MySqlConnection(connection);

        // general methods

        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA512.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2").ToLower());

            return sb.ToString().ToLower();
        }

        public static void Active_Control_textboxu(TextBox textBox)
        {
            textBox.Focus();
        }

        public static void Active_Control_comboboxa(ComboBox comboBox)
        {
            comboBox.Focus();
        }

        public static string SpacjaNaPustePole(string tekst) // metoda zamieniajaca spacje na puste pole sklejając cały tekst
        {
            char[] k = new char[tekst.Length];
            int dlugosc = tekst.Count();

            for (int i = 0; i < tekst.Length; i++)
            {
                k[i] = tekst[i];
            }

            for (int l = 0; l < dlugosc; l++)
            {
                if (k[l] == ' ')
                {
                    for (int j = l; j < dlugosc; j++)
                    {
                        if (j < dlugosc - 1)
                        {
                            k[j] = k[j + 1];
                        }
                        else if (j == dlugosc - 1)
                        {
                            Array.Resize(ref k, dlugosc - 1);
                            dlugosc -= 1;
                        }
                    }

                }

            }
            tekst = new string(k);
            return tekst;
        }
    }
}
