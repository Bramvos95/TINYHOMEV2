using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TINYHOMEV2
{
    class lamp
    {
        // variabelen
        private string naam;
        private int status;
        private int hex;

        public string Naam { get => naam; set => naam = value; }
        public int Status { get => status; set => status = value; }
        public int Hex { get => hex; set => hex = value; }
    }
}
