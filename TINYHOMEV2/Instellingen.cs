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
    public partial class Instellingen : Form
    {
        DatabaseConnectie db;

        public Instellingen()
        {
            InitializeComponent();
            db = new DatabaseConnectie();
            listboxVullen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtBaudrate.Text != "" && txtCommandBegin.Text != "" && txtCommandEnd.Text != "" && txtComPoort.Text != "")
            {
                Arduino a = new Arduino();
                a.Baudrate = Convert.ToInt32(txtBaudrate.Text);
                a.Commandbegin = txtCommandBegin.Text;
                a.Commandend = txtCommandEnd.Text;
                a.Poort = txtComPoort.Text;
                a.Naam = txtVerbindingsNaam.Text;
                db.nieuweArduino(a); 
            }
            else if (listBox1.SelectedIndex != -1)
            {
                MessageBox.Show("Arduino gekozen.");
            }
            this.Hide();
        }

        private void listboxVullen()
        {
            listBox1.DataSource = db.arduinos();
        }
    }
}
