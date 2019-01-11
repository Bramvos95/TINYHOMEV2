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
            ToHex();
        }

        private void greenSlider_Scroll(object sender, EventArgs e)
        {
            greenTextBox.Text = greenSlider.Value.ToString();
            ToHex();
        }

        private void blueSlider_Scroll(object sender, EventArgs e)
        {
            blueTextBox.Text = blueSlider.Value.ToString();
            ToHex();
        }
    
        private void ToHex()
        {
            MyColor = Color.FromArgb(redSlider.Value, greenSlider.Value, blueSlider.Value);
            hex = MyColor.R.ToString("X2") + MyColor.G.ToString("X2") + MyColor.B.ToString("X2");
            pcColorPanel.BackColor = MyColor;
            Sendmessage();
        }

        private void Sendmessage()
        {
            if(Sender == "RGBLED1"){
                parent.Sm.SendMessage("SET_LIVINGROOMRGBLED:" + Hex);
                parent.Db.UpdateRGB(Hex, "Woonkamer_Stalamp");

            }else if(Sender == "RGBLED2")
            {
                parent.Sm.SendMessage("SET_BEDROOMRGBLED:" + Hex);
                parent.Db.UpdateRGB(Hex, "Slaapkamer_Stalamp");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}
