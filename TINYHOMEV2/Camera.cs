using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINYHOMEV2
{
    public partial class Camera : Form
    {
        public Camera()
        {
            InitializeComponent();
            try
            {
                axWindowsMediaPlayer1.settings.volume = 0; //geluid van de video wordt op 0 gezet
                axWindowsMediaPlayer1.uiMode = "none"; // de bedieningsknoppen worden verborgen
                axWindowsMediaPlayer1.URL = "\\\\Mac\\Home\\Desktop\\video.mp4"; // dit is de video die afgespeeld moet worden
                axWindowsMediaPlayer1.settings.autoStart = true; // video moet direct starten
                
            }
            catch(Exception exc) // foutmeldingen worden hier opgevangen en in het output venster geschreven
            {
                Console.WriteLine(exc);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop(); // stop de video
            this.Hide(); // form wordt verborgen
        }
    }
}
