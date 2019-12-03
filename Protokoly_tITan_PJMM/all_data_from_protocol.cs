using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Protokoly_tITan_PJMM
{
    public static class all_data_from_protocol
    {
        public static string numer_zlecenia = "";
        public static string data_przyjecia = "";


        public static string nazwa_klienta = "";
        public static string adres_klienta = "";
        public static string kod_pocztowy = "";
        public static string prywatny_firma = ""; // radiobutton
        public static string nip_klienta = "";
        public static string numer_sluzbowy = "";
        public static string numer_prywatny = "";
        public static string email = "";


        public static string pc_laptop_telefon_drukarka = ""; // radiobutton
        public static string model = "";
        public static string serial_number = "";


        public static string opis = "";

       
        public static string czas_naprawy = "";
        public static string koszt_naprawy = "";
        public static string gotowka_karta_przelew = ""; // radiobutton

        public static bool flaga_nip = false;
        public static bool flaga_sluzbowy = false;

        // ----------- Flagi na poprawność z regexow --------------------------- ponizej -----------

        public static bool numer_zlecenia_poprawnosc = false;
        public static bool data_przyjecia_poprawnosc = false;


        public static bool kod_pocztowy_poprawnosc = false;
        public static bool nip_klienta_poprawnosc = false;
        public static bool numer_sluzbowy_poprawnosc = false;
        public static bool numer_prywatny_poprawnosc = false;
        public static bool email_poprawnosc = false;

        //public static bool czas_naprawy_poprawnosc = false;
        public static bool koszt_naprawy_poprawnosc = false;
    }
}
