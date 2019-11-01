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
        }
    }
}
