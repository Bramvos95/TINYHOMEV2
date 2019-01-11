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
    public partial class LoginForm : Form
    {
        DatabaseConnectie dbVerbinding;

        public LoginForm()
        {
            InitializeComponent();
            dbVerbinding = new DatabaseConnectie();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login(txtGebruikersnaam.Text, txtWachtwoord.Text);
        }

        private void txtWachtwoord_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
            {
                Login(txtGebruikersnaam.Text, txtWachtwoord.Text);
            }
        }

        private void Login(string gebruikersnaam, string wachtwoord)
        {
            Gebruiker gebruiker = dbVerbinding.inloggen(gebruikersnaam, wachtwoord);
            if (gebruiker != null)
            {
                Tinyhome mainUI = new Tinyhome();
                this.Hide();
                mainUI.Show();
            }
            else
            {
                MessageBox.Show("Inloggen mislukt!");
            }
        }
    }
}
