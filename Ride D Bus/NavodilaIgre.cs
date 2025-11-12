using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Ride_D_Bus
{
    class NavodilaIgre
    {
        public void PrikažiNavodila()
        {
            string navodila =
                "=== NAVODILA ZA IGRO: RIDE THE BUS ===\n" +
                "1. Na začetku igralec vnese stavo.\n" +
                "2. Igra ima štiri runde, v vsaki mora igralec uganiti določeno lastnost naslednje karte:\n" +
                "   - 1. runda: Ugani, ali bo karta rdeča ali črna.\n" +
                "   - 2. runda: Ugani, ali bo karta višja ali nižja od prejšnje.\n" +
                "   - 3. runda: Ugani, ali bo tretja karta vmes ali izven prejšnjih dveh vrednosti.\n" +
                "   - 4. runda: Ugani, kateri simbol (piki, srca, karo, križ) bo naslednja karta.\n" +
                "3. Za vsako pravilno napoved se poveča bonus na začetno stavo.\n" +
                "4. Po vsaki rundi se lahko igralec odloči, ali bo nadaljeval ali vzel svoj trenutni dobitek.\n" +
                "5. Če igralec v kateri koli rundi zgreši, izgubi bonus in igra se konča.";

            MessageBox.Show(navodila, "Navodila igre");
        }
    }
}
