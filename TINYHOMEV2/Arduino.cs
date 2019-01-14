using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINYHOMEV2
{
    public class Arduino
    {
        //variabelen 
        private string poort;
        private int baudrate;
        private string commandbegin;
        private string commandend;
        private string naam;
        private int id;

        public string Poort { get => poort; set => poort = value; }
        public int Baudrate { get => baudrate; set => baudrate = value; }
        public string Commandbegin { get => commandbegin; set => commandbegin = value; }
        public string Commandend { get => commandend; set => commandend = value; }
        public string Naam { get => naam; set => naam = value; }
        public int Id { get => id; set => id = value; }

        public override string ToString() //wanneer de gegevens uit deze klasse worden opgevraagd wordt dit teruggestuurd:
        {
            return Id + " - " + Naam + " - " + Poort + " - " + Commandbegin + " - " + Commandend;
        }
    }
}
