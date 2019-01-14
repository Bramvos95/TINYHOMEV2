using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINYHOMEV2
{
    class Logboek
    {
        public void schrijfLog(string gebruiker, string handeling, string tijdstip)
        {
            try
            {
                StreamWriter writer = new StreamWriter(@"\\\\Mac\\Home\\Desktop\\logboek.txt", true); // writer wordt aangemaakt op het pad
                writer.WriteLine(gebruiker + " - " + handeling + " - " + tijdstip); // schrijf dit in het bestand
                writer.Close(); // writer wordt gestopt
            }
            catch (Exception exc) // Foutmelding wordt opgevangen en getoond in het output venter
            {
                Console.WriteLine(exc);
            }
        }
    }
}
