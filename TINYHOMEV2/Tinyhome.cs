using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TINYHOMEV2
{
    public partial class Tinyhome : Form
    {
        // objecten
        private SerialMessenger sm;
        private MessageBuilder ms;
        private DatabaseConnectie db;
        private Logboek lg;
        private lamp lamp;
        private Timer readMessageTimer;

        // variabelen
        private string portName;
        private int baudRate;
        private char beginCHar;
        private char endChar;
        private decimal temperatuur;
        private decimal luchtvochtigheid;


        public string PortName { get => portName; set => portName = value; }
        public int BaudRate { get => baudRate; set => baudRate = value; }
        public char BeginCHar { get => beginCHar; set => beginCHar = value; }
        public char EndChar { get => endChar; set => endChar = value; }
        internal SerialMessenger Sm { get => sm; set => sm = value; }
        internal MessageBuilder Ms { get => ms; set => ms = value; }
        internal DatabaseConnectie Db { get => db; set => db = value; }
        public decimal Temperatuur { get => temperatuur; set => temperatuur = value; }
        public decimal Luchtvochtigheid { get => luchtvochtigheid; set => luchtvochtigheid = value; }
        internal Logboek Lg { get => lg; set => lg = value; }

        public Tinyhome()
        {
            InitializeComponent();
            readMessageTimer = new Timer();
            Db = new DatabaseConnectie();
            lamp = new lamp();
            Lg = new Logboek();
            readMessageTimer.Interval = 10;
            readMessageTimer.Tick += new EventHandler(ReadMessageTimer_Tick);
            readMessageTimer.Start();
            Connect(Db.Arduinolaatste());
            FillCheckBox();
            try
            {
                pbDag.Image = Image.FromFile("\\\\Mac\\Home\\Downloads\\dagweergave.png");
                label4.Text = "24.000 KW";
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc);
            }
        }

        public void Connect(Arduino arduino)
        {
            // nieuwe objecten worden aangemaakt
            Ms = new MessageBuilder(Convert.ToChar(arduino.Commandbegin), Convert.ToChar(arduino.Commandend));
            Sm = new SerialMessenger(arduino.Poort, arduino.Baudrate, Ms);

            try
            {
                Sm.Connect(); // de functie connect in de klasse serialmessenger wordt hier aangeroepen
                MessageBox.Show("Connected!"); // messagebox wordt getoond
            }
            catch(Exception exc) //foutmeldingen worden hier opgevangen en geschreven in het output venster
            {
                Console.WriteLine(exc);
            }
        }
        private void lblUitloggen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide(); // form wordt verborgen

            LoginForm lf = new LoginForm(); // nieuw loginform wordt aangemaakt
            lf.Show(); // loginform wordt getoond
        }

        private void btnInstellingen_Click(object sender, EventArgs e)
        {
            Instellingen instellingen = new Instellingen(this); //nieuw instellingen form wordt aangemaakt met dit form als object
            instellingen.Show(); // nieuwe instellinge form wordt getoond
        }

        private void ReadMessageTimer_Tick(object sender, EventArgs e) // hier wordt gecontroleerd of er een message binnen is gekomen 
        {
            string[] messages = Sm.ReadMessages();
            if (messages != null)
            {
                foreach (string message in messages)
                {
                    processReceivedMessage(message);
                }
            }
        }

        public void FillCheckBox() // waardes worden uit de database gehaald en het checkbox past zich daarop aan
        {     
            cbKeuken.Checked = db.FillCheckBox("Keuken") ? true : false; // als de functie db.fillcheckbox true teruggeeft dan is de checkbox checked, anders niet
            cbWoonkamer.Checked = db.FillCheckBox("Woonkamer") ? true : false;
            cbBadkamer.Checked = db.FillCheckBox("Badkamer") ? true : false;
            cbGarage.Checked = db.FillCheckBox("Garage") ? true : false;
            cbSlaapkamer.Checked = db.FillCheckBox("Slaapkamer") ? true : false;
        }


        private void processReceivedMessage(string message) // hier worden de binnengekregen berichten afgehandelt
        {
            if (message.StartsWith("TEMPERATUUR")){
                Decimal value = getParamValue(message); //de temperatuur wordt opgelagen
                lblGraden.Text = value.ToString(); // de temperatuur wordt in een label getoond
                Temperatuur = value;
            }else if (message.StartsWith("LUCHTVOCHTIGHEID")){
                Decimal value = getParamValue(message);
                lblLuchtvochtigheid.Text = value.ToString();
                Luchtvochtigheid = value;
            }
        }

        private void disconnect() // hiermee wordt de connectie met de arduino verbroken
        {
            try
            {
                readMessageTimer.Enabled = false;
                sm.Disconnect();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private Decimal getParamValue(string message) // hier wordt de waarde uit het bericht gehaad
        {
            int colonIndex = message.IndexOf(':');
            if (colonIndex != -1)
            {
                string param = message.Substring(colonIndex + 1);
                Decimal value;
                bool done = Decimal.TryParse(param, out value);
                if (done)
                {
                    return value;
                }
            }
            throw new ArgumentException("message contains no value parameter");
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e) //stuurt een commando naar de arduino om de lamp aan of uit te zetten
        {
            lamp.Naam = "Keuken";
            SetLed("SET_KITCHENLED:TRUE", "SET_KITCHENLED:FALSE", sender);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)//stuurt een commando naar de arduino om de lamp aan of uit te zetten
        {
            lamp.Naam = "Garage";
            SetLed("SET_GARAGELED:TRUE", "SET_GARAGELED:FALSE", sender);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)//stuurt een commando naar de arduino om de lamp aan of uit te zetten
        {
            lamp.Naam = "Woonkamer";
            SetLed("SET_LIVINGROOMLED:TRUE", "SET_LIVINGROOMLED:FALSE", sender);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)//stuurt een commando naar de arduino om de lamp aan of uit te zetten
        {
            lamp.Naam = "Slaapkamer";
            SetLed("SET_BEDROOMLED:TRUE", "SET_BEDROOMLED:FALSE", sender);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)//stuurt een commando naar de arduino om de lamp aan of uit te zetten
        {
            lamp.Naam = "Badkamer";
            SetLed("SET_BATHROOMLED:TRUE", "SET_BATHROOMLED:FALSE", sender);
        }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)//stuurt een commando naar de arduino om de lamp aan of uit te zetten
        {
            lamp.Naam = "Woonkamer_Stalamp";
            SetLed("SET_LIVINGROOMRGBLED:TRUE", "SET_LIVINGROOMRGBLED:FALSE", sender);
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)//stuurt een commando naar de arduino om de lamp aan of uit te zetten
        {
            lamp.Naam = "Slaapkamer_Stalamp";
            SetLed("SET_BEDROOMRGBLED:FALSE", "SET_BEDROOMRGBLED:TRUE", sender);
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)//stuurt een commando naar de arduino om het deurslot te openen of te sluiten
        {
            if (checkBox6.Checked == true)
            {
                Sm.SendMessage("SET_FRONTDOORLOCK:TRUE");
            }
            else
            {
                Sm.SendMessage("SET_FRONTDOORLOCK:FALSE");
            }
        }

        private void SetLed(string aan, string uit, object sender) // controleerd of het checkbox al checked was, zoja, dan zet de lamp uit. Anders zet de lamp aan.
        {
            if (((CheckBox)sender).Checked == true)
            {
                Sm.SendMessage(aan);
                lamp.Status = 1;
                Lg.schrijfLog("Admin:", aan, DateTime.Now.ToString("h:mm:ss tt")); // er wordt in het log geschreven wanneer deze actie heeft plaatsgevonden en door welke gebruiker
                Db.Updatelampen(lamp);
            }
            else
            {
                Sm.SendMessage(uit);
                lamp.Status = 0;
                Lg.schrijfLog("Admin:", uit, DateTime.Now.ToString("h:mm:ss tt"));// er wordt in het log geschreven wanneer deze actie heeft plaatsgevonden en door welke gebruiker
                Db.Updatelampen(lamp);
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            RGBControl rgb = new RGBControl(this, "RGBLED1");
            rgb.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            RGBControl rgb = new RGBControl(this, "RGBLED2");
            rgb.Show();
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            sm.SendMessage("SET_SHUTTERBEDROOM:TRUE"); // stuurt een commando om de rolluik te openen
            Lg.schrijfLog("Admin:", "Slaapkamer_rolluik:OPEN", DateTime.Now.ToString("h:mm:ss tt"));// er wordt in het log geschreven wanneer deze actie heeft plaatsgevonden en door welke gebruiker
        }

        private void button5_Click(object sender, EventArgs e)
        {
            sm.SendMessage("SET_SHUTTERBEDROOM:FALSE");// stuurt een commando om de rolluik te sluiten
            Lg.schrijfLog("Admin:", "Slaapkamer_rolluik:DICHT", DateTime.Now.ToString("h:mm:ss tt"));// er wordt in het log geschreven wanneer deze actie heeft plaatsgevonden en door welke gebruiker
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sm.SendMessage("SET_SHUTTERBATHROOM:TRUE");// stuurt een commando om de rolluik te openen
            Lg.schrijfLog("Admin:", "Badkamer_rolluik:OPEN", DateTime.Now.ToString("h:mm:ss tt"));
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sm.SendMessage("SET_SHUTTERBATHROOM:FALSE");// stuurt een commando om de rolluik te sluiten
            Lg.schrijfLog("Admin:", "Badkamer_rolluik:DICHT", DateTime.Now.ToString("h:mm:ss tt"));
        }

        private void label4_TextChanged(object sender, EventArgs e)
        {
            db.Thermostaat(Luchtvochtigheid, Temperatuur); // als de temperatuur of luchtvochtigheid wijzigt wordt deze in de database opgeslagen
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Camera camera = new Camera(); // nieuw form wordt aangemaakt
            Lg.schrijfLog("Admin:", "Camera sessie gestart", DateTime.Now.ToString("h:mm:ss tt")); // er wordt in het log geschreven wanneer deze actie heeft plaatsgevonden en door welke gebruiker
            camera.Show(); // form wordt getoond
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value = (int)(comboBox1.SelectedIndex); // er wordt opgeslagen welk item er in de combobox is geselecteerd

            // bij ieder item in de combobox horen andere gegevens
            try
            {
                if (value == 0)
                {
                    pbDag.Image = Image.FromFile("\\\\Mac\\Home\\Downloads\\dagweergave.png");
                    label4.Text = "24.000 KW";
                }
                else if (value == 1)
                {
                    pbDag.Image = Image.FromFile("\\\\Mac\\Home\\Downloads\\cropped-stroom-concept-1-1.jpg");
                    label4.Text = "94.000 KW";
                }
                else if (value == 2)
                {
                    pbDag.Image = Image.FromFile("\\\\Mac\\Home\\Downloads\\sticker-pas-op-_stroom.png");
                    label4.Text = "182.073 KW";
                }
                else if (value == 3)
                {
                    pbDag.Image = Image.FromFile("\\\\Mac\\Home\\Downloads\\Grafiek-zonnepanelen.jpg");
                    label4.Text = "939.811 KW";
                }
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
    }
}
