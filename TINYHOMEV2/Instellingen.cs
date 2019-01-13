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
        Tinyhome parent;

        public Instellingen(Tinyhome parent)
        {
            InitializeComponent();
            db = new DatabaseConnectie();
            this.parent = parent;
            listboxVullen();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(txtBaudrate.Text != "" && txtCommandBegin.Text != "" && txtCommandEnd.Text != "" && txtComPoort.Text != "")
            {
                Arduino a = new Arduino();
                try
                {
                    a.Baudrate = Convert.ToInt32(txtBaudrate.Text);
                    a.Commandbegin = txtCommandBegin.Text;
                    a.Commandend = txtCommandEnd.Text;
                    a.Poort = txtComPoort.Text;
                    a.Naam = txtVerbindingsNaam.Text;
                    db.nieuweArduino(a);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc);
                }
                finally
                {
                    parent.Connect(a);
                }
            }
            else if (listBox1.SelectedIndex != -1)
            {
                Arduino a =  (Arduino) listBox1.SelectedValue;
                parent.Connect(a);
            }
            this.Hide();
        }

        private void listboxVullen()
        {
            listBox1.DataSource = db.arduinos();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                Gebruiker gb = new Gebruiker();
                gb.Naam = textBox1.Text;
                db.GebruikerToevoegen(gb, textBox2.Text);
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
