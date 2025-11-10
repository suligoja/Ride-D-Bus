using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ride_D_Bus
{
    class Primerjava:IComparer<Karta>
    {

        public int Compare(Karta x, Karta y)
        {
            if (x.Vrednost < y.Vrednost)
                return -1;
            if (x.Vrednost > y.Vrednost)
                return 1;
            //barve primerja samo, če sta vrednosti enaki
            if (x.Barva < y.Barva)
                return -1;
            if (x.Barva > y.Barva)
                return 1;
            return 0;

        }
    }
}
