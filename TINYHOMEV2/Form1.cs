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
        SerialMessenger sm;
        private string portName;
        private int baudRate;
        private char beginCHar;
        private char endChar;
        MessageBuilder ms;
        private Timer readMessageTimer;


        public string PortName { get => portName; set => portName = value; }
        public int BaudRate { get => baudRate; set => baudRate = value; }
        public char BeginCHar { get => beginCHar; set => beginCHar = value; }
        public char EndChar { get => endChar; set => endChar = value; }


        public Form1()
        {
            InitializeComponent();
            readMessageTimer = new Timer();
            readMessageTimer.Interval = 10;
            readMessageTimer.Tick += new EventHandler(ReadMessageTimer_Tick);
        }
      
        private void Form1_Resize(object sender, EventArgs e)
        {

        }


        public void Connect()
        {
            ms = new MessageBuilder(BeginCHar, EndChar);
            sm = new SerialMessenger(PortName, BaudRate, ms);

            try
            {
                sm.Connect();
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
            Instellingen instellingen = new Instellingen();
            instellingen.Show();
        }

        private void ReadMessageTimer_Tick(object sender, EventArgs e)
        {
            string[] messages = sm.ReadMessages();
            if (messages != null)
            {
                foreach (string message in messages)
                {
                    processReceivedMessage(message);
                }
            }
        }

        /// <summary>
        /// handle received messages
        /// </summary>
        /// <param name="message"></param>
        private void processReceivedMessage(string message)
        {
            MessageBox.Show(message);
            //if (message == "ARDUINO_CONTROL")
            //{
            //    whoIsInControlLabel.Text = "Arduino";
            //}
            //else if (message.StartsWith("RED_STATUS:"))
            //{
            //    int value = getParamValue(message);
            //    arduinoRedTextBox.Text = value.ToString();
            //    arduinoColorPanel.BackColor = Color.FromArgb(value, arduinoColorPanel.BackColor.G, arduinoColorPanel.BackColor.B);
            //}
            //else if (message.StartsWith("GREEN_STATUS:"))
            //{
            //    int value = getParamValue(message);
            //    arduinoGreenTextBox.Text = value.ToString();
            //    arduinoColorPanel.BackColor = Color.FromArgb(arduinoColorPanel.BackColor.R, value, arduinoColorPanel.BackColor.B);
            //}
            //else if (message.StartsWith("BLUE_STATUS:"))
            //{
            //    int value = getParamValue(message);
            //    arduinoBlueTextBox.Text = value.ToString();
            //    arduinoColorPanel.BackColor = Color.FromArgb(arduinoColorPanel.BackColor.R, arduinoColorPanel.BackColor.G, value);
            //}
        }
    }
}
