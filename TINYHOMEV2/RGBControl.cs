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

        private void redSlider_Scroll(object sender, EventArgs e)
        {
            redTextBox.Text = redSlider.Value.ToString();
            int r = redSlider.Value;
            Sendmessage("rood", r);
            ToHex();
        }

        private void greenSlider_Scroll(object sender, EventArgs e)
        {
            greenTextBox.Text = greenSlider.Value.ToString();
            int g = greenSlider.Value;
            Sendmessage("groen", g);
            ToHex();
        }

        private void blueSlider_Scroll(object sender, EventArgs e)
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

        private void Sendmessage(string kleur, int a)
        {
            if (Sender == "RGBLED1")
            {
                if (kleur == "rood")
                {
                    parent.Sm.SendMessage("SET_LIVINGROOMRGBLEDRED:" + a);
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
                if (kleur == "groen")
                {
                    parent.Sm.SendMessage("SET_LIVINGROOMRGBLEDGREEN:" + a);
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
                if (kleur == "blauw")
                {
                    parent.Sm.SendMessage("SET_LIVINGROOMRGBLEDBLUE:" + a);
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
            }
            else if (Sender == "RGBLED2")
            {
                if (kleur == "rood")
                {
                    parent.Sm.SendMessage("SET_BEDROOMRGBLEDRED:" + a);
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
                if (kleur == "groen")
                {
                    parent.Sm.SendMessage("SET_BEDROOMRGBLEDGREEN:" + a);
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
                if (kleur == "blauw")
                {
                    parent.Sm.SendMessage("SET_BEDROOMRGBLEDBLUE:" + a);
                    parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
