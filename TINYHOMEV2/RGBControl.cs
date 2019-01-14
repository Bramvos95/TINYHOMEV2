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
    public partial class RGBControl : Form
    {
        private Color myColor;
        private string hex;
        private string sender;

        public Color MyColor { get => myColor; set => myColor = value; }
        public string Hex { get => hex; set => hex = value; }
        public string Sender { get => sender; set => sender = value; }

        Tinyhome parent;

        public RGBControl(Tinyhome parent, string sender)
        {
            InitializeComponent();
            this.parent = parent;
            Sender = sender;
        }

        private void redSlider_Scroll(object sender, EventArgs e) // als de slider van waarde verandert wordt er een commando gestuurd met de juiste kleur
        {
            redTextBox.Text = redSlider.Value.ToString();
            int r = redSlider.Value;
            Sendmessage("rood", r);
            ToHex();
        }

        private void greenSlider_Scroll(object sender, EventArgs e)
        {
            greenTextBox.Text = greenSlider.Value.ToString();// als de slider van waarde verandert wordt er een commando gestuurd met de juiste kleur
            int g = greenSlider.Value;
            Sendmessage("groen", g);
            ToHex();
        }

        private void blueSlider_Scroll(object sender, EventArgs e) // als de slider van waarde verandert wordt er een commando gestuurd met de juiste kleur
        {
            blueTextBox.Text = blueSlider.Value.ToString();
            int b = greenSlider.Value;
            Sendmessage("blauw", b);
            ToHex();
        }
    
        private void ToHex()
        {
            MyColor = Color.FromArgb(redSlider.Value, greenSlider.Value, blueSlider.Value);
            pcColorPanel.BackColor = MyColor;
        }

        private void Sendmessage(string kleur, int a) // hier wordt gecontroleeerd welke lamp er bediend moet worden en welke kleu er verzonden moet worden
        {
            if (Sender == "RGBLED1") 
            {
                if (kleur == "rood")
                {
                    parent.Sm.SendMessage("SET_LIVINGROOMRGBLEDRED:" + a);
                    parent.Lg.schrijfLog("Admin:", "WoonkamerRGB-Rood:" + a.ToString(), DateTime.Now.ToString("h:mm:ss tt"));
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
                if (kleur == "groen")
                {
                    parent.Sm.SendMessage("SET_LIVINGROOMRGBLEDGREEN:" + a);
                    parent.Lg.schrijfLog("Admin:", "WoonkamerRGB-Groen:" + a.ToString(), DateTime.Now.ToString("h:mm:ss tt"));
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
                if (kleur == "blauw")
                {
                    parent.Sm.SendMessage("SET_LIVINGROOMRGBLEDBLUE:" + a);
                    parent.Lg.schrijfLog("Admin:", "WoonkamerRGB-Blauw:" + a.ToString(), DateTime.Now.ToString("h:mm:ss tt"));
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
            }
            else if (Sender == "RGBLED2")
            {
                if (kleur == "rood")
                {
                    parent.Sm.SendMessage("SET_BEDROOMRGBLEDRED:" + a);
                    parent.Lg.schrijfLog("Admin:", "SlaapkamerRGB-Rood:" + a.ToString(), DateTime.Now.ToString("h:mm:ss tt"));
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
                if (kleur == "groen")
                {
                    parent.Sm.SendMessage("SET_BEDROOMRGBLEDGREEN:" + a);
                    parent.Lg.schrijfLog("Admin:", "SlaapkamerRGB-Groen:" + a.ToString(), DateTime.Now.ToString("h:mm:ss tt"));
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
                if (kleur == "blauw")
                {
                    parent.Sm.SendMessage("SET_BEDROOMRGBLEDBLUE:" + a);
                    parent.Lg.schrijfLog("Admin:", "SlaapkamerRGB-Blauw:" + a.ToString(), DateTime.Now.ToString("h:mm:ss tt"));
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); //het form wordt verborgen
        }
    }
}
