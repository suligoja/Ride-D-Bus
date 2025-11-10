using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ride_D_Bus
{
    enum Barve { Križ, Pik, Karo,Srce}
    enum Vrednosti { As=1,Dva,Tri,Štiri,Pet, Šest, Sedem,Osem,Devet,Deset,Janez,Dama, Kralj}
    class Karta
    {
        public Barve Barva { get; set; }
        public Vrednosti Vrednost { get; set; }
        public Karta(Barve b, Vrednosti v)
        {
            Barva = b;
            Vrednost = v;
        }
        public Karta() { }
        public string Ime
        {
            get { return Vrednost.ToString() + " " + Barva.ToString(); }
        }
        public override string ToString()
        {
            return Ime;
        }
    }
}
