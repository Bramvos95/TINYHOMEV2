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
        private SerialMessenger sm;
        private MessageBuilder ms;
        private DatabaseConnectie db;
        private lamp lamp;
        private string portName;
        private int baudRate;
        private char beginCHar;
        private char endChar;
        private decimal temperatuur;
        private decimal luchtvochtigheid;
        private Timer readMessageTimer;


        public string PortName { get => portName; set => portName = value; }
        public int BaudRate { get => baudRate; set => baudRate = value; }
        public char BeginCHar { get => beginCHar; set => beginCHar = value; }
        public char EndChar { get => endChar; set => endChar = value; }
        internal SerialMessenger Sm { get => sm; set => sm = value; }
        internal MessageBuilder Ms { get => ms; set => ms = value; }
        internal DatabaseConnectie Db { get => db; set => db = value; }
        public decimal Temperatuur { get => temperatuur; set => temperatuur = value; }
        public decimal Luchtvochtigheid { get => luchtvochtigheid; set => luchtvochtigheid = value; }

        public Tinyhome()
        {
            InitializeComponent();
            readMessageTimer = new Timer();
            Db = new DatabaseConnectie();
            lamp = new lamp();
            readMessageTimer.Interval = 10;
            readMessageTimer.Tick += new EventHandler(ReadMessageTimer_Tick);
            readMessageTimer.Start();
            Connect(Db.Arduinolaatste());
            FillCheckBox();
            pbDag.Image = Image.FromFile("\\\\Mac\\Home\\Downloads\\dagweergave.png");
            label4.Text = "24.000 KW";
        }

        public void Connect(Arduino arduino)
        {

            Ms = new MessageBuilder(Convert.ToChar(arduino.Commandbegin), Convert.ToChar(arduino.Commandend));
            Sm = new SerialMessenger(arduino.Poort, arduino.Baudrate, Ms);

            try
            {
                Sm.Connect();
                MessageBox.Show("Connected!");
            }
            catch(Exception exc)
            {
                Console.WriteLine(exc);
            }
        }
        private void lblUitloggen_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();

            LoginForm lf = new LoginForm();
            lf.Show();
        }

        private void btnInstellingen_Click(object sender, EventArgs e)
        {
            Instellingen instellingen = new Instellingen(this);
            instellingen.Show();
        }

        private void ReadMessageTimer_Tick(object sender, EventArgs e)
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

        public void FillCheckBox()
        {
            cbKeuken.Checked = db.FillCheckBox("Keuken") ? true : false;
            cbWoonkamer.Checked = db.FillCheckBox("Woonkamer") ? true : false;
            cbBadkamer.Checked = db.FillCheckBox("Badkamer") ? true : false;
            cbGarage.Checked = db.FillCheckBox("Garage") ? true : false;
            cbSlaapkamer.Checked = db.FillCheckBox("Slaapkamer") ? true : false;
        }


        private void processReceivedMessage(string message)
        {
            if (message.StartsWith("TEMPERATUUR")){
                Decimal value = getParamValue(message);
                lblGraden.Text = value.ToString();
                Temperatuur = value;
            }else if (message.StartsWith("LUCHTVOCHTIGHEID")){
                Decimal value = getParamValue(message);
                lblLuchtvochtigheid.Text = value.ToString();
                Luchtvochtigheid = value;
            }
        }

        private void disconnect()
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
        private Decimal getParamValue(string message)
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

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            lamp.Naam = "Keuken";
            SetLed("SET_KITCHENLED:TRUE", "SET_KITCHENLED:FALSE", sender);
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            lamp.Naam = "Garage";
            SetLed("SET_GARAGELED:TRUE", "SET_GARAGELED:FALSE", sender);
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            lamp.Naam = "Woonkamer";
            SetLed("SET_LIVINGROOMLED:TRUE", "SET_LIVINGROOMLED:FALSE", sender);
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            lamp.Naam = "Slaapkamer";
            SetLed("SET_BEDROOMLED:TRUE", "SET_BEDROOMLED:FALSE", sender);
        }

        private void checkBox5_CheckedChanged(object sender, EventArgs e)
        {
            lamp.Naam = "Badkamer";
            SetLed("SET_BATHROOMLED:TRUE", "SET_BATHROOMLED:FALSE", sender);
        }
        private void checkBox1_CheckedChanged_1(object sender, EventArgs e)
        {
            lamp.Naam = "Woonkamer_Stalamp";
            SetLed("SET_LIVINGROOMRGBLED:TRUE", "SET_LIVINGROOMRGBLED:FALSE", sender);
        }

        private void checkBox2_CheckedChanged_1(object sender, EventArgs e)
        {
            lamp.Naam = "Slaapkamer_Stalamp";
            SetLed("SET_BEDROOMRGBLED:FALSE", "SET_BEDROOMRGBLED:TRUE", sender);
        }
        private void checkBox6_CheckedChanged(object sender, EventArgs e)
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

        private void SetLed(string aan, string uit, object sender)
        {
            if (((CheckBox)sender).Checked == true)
            {
                Sm.SendMessage(aan);
                lamp.Status = 1;
                Db.Updatelampen(lamp);
            }
            else
            {
                Sm.SendMessage(uit);
                lamp.Status = 0;
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
            sm.SendMessage("SET_SHUTTERBEDROOM:TRUE");
        }
        
        private void button5_Click(object sender, EventArgs e)
        {
            sm.SendMessage("SET_SHUTTERBEDROOM:FALSE");
        }

        private void button6_Click(object sender, EventArgs e)
        {
            sm.SendMessage("SET_SHUTTERBATHROOM:TRUE");
        }
        
        private void button2_Click(object sender, EventArgs e)
        {
            sm.SendMessage("SET_SHUTTERBATHROOM:FALSE");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            sm.SendMessage("SET_GARAGEDOOR:TRUE");
        }
        
        private void button7_Click_1(object sender, EventArgs e)
        {
            sm.SendMessage("SET_GARAGEDOOR:FALSE");
        }

        private void label4_TextChanged(object sender, EventArgs e)
        {
            db.Thermostaat(Luchtvochtigheid, Temperatuur);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            Camera camera = new Camera();
            camera.Show();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int value = (int)(comboBox1.SelectedIndex);
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
    }
}
