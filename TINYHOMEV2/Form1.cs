using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINYHOMEV2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
      
        private void Form1_Resize(object sender, EventArgs e)
        {

        }

        private void lblUitloggen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            LoginForm lf = new LoginForm();
            lf.Show();
        }

        private void btnInstellingen_Click(object sender, EventArgs e)
        {
            Instellingen instellingen = new Instellingen();
            instellingen.Show();
        }
    }
}
