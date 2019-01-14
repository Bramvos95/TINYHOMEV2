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
            dbVerbinding = new DatabaseConnectie(); // nieuw object van de klasse databaseconnectie wordt gemaakt
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            Login(txtGebruikersnaam.Text, txtWachtwoord.Text); // de functie login wordt aangeroepen en de waardes van de tekstvelden worden meegegeven
        }

        private void txtWachtwoord_KeyPress(object sender, KeyPressEventArgs e) 
        {
            if (e.KeyChar == (char)Keys.Enter) // als er op enter geklikt wordt terwijl er getypt wordt in het wachtwoord tekstvak dan ...
            {
                Login(txtGebruikersnaam.Text, txtWachtwoord.Text); // de functie login wordt aangeroepen en de waardes van de tekstvelden worden meegegeven
            }
        }

        private void Login(string gebruikersnaam, string wachtwoord)
        {
            Gebruiker gebruiker = dbVerbinding.inloggen(gebruikersnaam, wachtwoord); // gebruiker wordt gecontroleerd in de database
            if (gebruiker != null) // als de gebruiker bestaat, wordt het hoofdform geopend
            {
                Tinyhome mainUI = new Tinyhome();
                this.Hide();
                mainUI.Show();
            }
            else // als de gebruiker niet voorkomt in de database dan wordt het bericht getoond "inloggen mislukt"
            {
                MessageBox.Show("Inloggen mislukt!"); 
            }
        }
    }
}
