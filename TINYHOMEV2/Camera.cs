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
            axWindowsMediaPlayer1.URL = "\\\\Mac\\Home\\Desktop\\video.mp4";
            axWindowsMediaPlayer1.settings.autoStart = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
