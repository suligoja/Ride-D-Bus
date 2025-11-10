using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ride_D_Bus
{
    internal class Igralec
    {
        private string ime; //ime igralca
        public string Ime
        {
            get { return ime; }
        }
        private Random random;
        private Kup roka; //karte, ki jih ima igralec v roki
        private TextBox textNaFormi; //vnosno polje za izpis rezultatov
        public Igralec(string i, Random r, TextBox t)
        {
            //Konstruktor inicializira atribute Igralca
            //v vnosno polje doda tekst "Joe se je pridružil igri" in prehod
            //v novo vrstico
            ime = i;
            random = r;
            roka = new Kup(new Karta[] { });
            textNaFormi = t;
            textNaFormi.Text += ime + " se priključi igri" + Environment.NewLine;
        }

        public IEnumerable<Vrednosti> IzločiKomplete()
        {
            //pregleda vseh 13 možnih vrednosti kart
            //ugotovi koliko kart s to vrednostjo ima igralec v roki
            // če ima 4 karte te vrednosti je to komplet, ki ga doda seznamu in karte
            //odstrani iz roke
            List<Vrednosti> kompleti = new List<Vrednosti>();
            for (int i = 1; i <= 13; i++)
            {
                Vrednosti v = (Vrednosti)i;
                int koliko = 0;
                for (int k = 0; k < roka.Count; k++)
                {
                    if (roka.Peek(k).Vrednost == v)
                        koliko++;
                }
                if (koliko == 4)
                {
                    kompleti.Add(v);
                    roka.PullOutValues(v);
                }
            }
            return kompleti;
        }
        public Vrednosti DobiNaključnoVrednost()
        {
            //vrne naključno vrednost, a mora biti iz roke igralca
            //če bo igralec računalnik bo izbral naključno vrednost za spraševanje
            Karta n = roka.Peek(random.Next(roka.Count));
                return n.Vrednost;
        }
        public Kup ImašKakšno(Vrednosti v)
        {
            //Nasprotnik te sprašuje ali imaš kakšno karto določene vrednosti
            // če jo imaš s pomočjo PullOutValues(3) odstrani karto iz svoje roke. Dodaj
            // vrstico v vnosno polje "Joe ima 3 šestice"
            Kup imam = roka.PullOutValues(v);
            textNaFormi.Text += ime + " ima " + imam.Count + " " + v + Environment.NewLine;
            return imam;
        }
        public void VprašajZaKarto(List<Igralec> i, int mojInd, Kup talon, Vrednosti v)
        {
            //Tu ti sprašuješ druge igralce ali imajo karto določene vrednosti
            textNaFormi.Text += ime + " sprašuje, če ima kdo " + v + Environment.NewLine;
            int daneKarteSkupaj = 0;
            for(int k =0; k< i.Count; k++)
            {
                if(k!= mojInd)
                {
                    Igralec ig = i[k];
                    Kup daneKarte = ig.ImašKakšno(v);
                    daneKarteSkupaj += daneKarte.Count;
                    while (daneKarte.Count > 0)
                        roka.Add(daneKarte.Deli());
                }
            }
            if(daneKarteSkupaj ==0 && talon.Count > 0)
            {
                textNaFormi.Text += ime + "mora potegniti iz talona." + Environment.NewLine;
                roka.Add(talon.Deli());
            }
        }
        public void VprašajZaKarto(List<Igralec> i, int mojIndeks, Kup talon)
        {
            if (talon.Count > 0) {
                if(roka.Count == 0)
                {               
 
                Vrednosti nak = DobiNaključnoVrednost();
                    VprašajZaKarto(i, mojIndeks, talon, nak);
                    }
            }
        }
        public int ŠtevecKart { get { return roka.Count; } }
        public void VzemiKarto(Karta k)
        {
            roka.Add(k);
        }
        public IEnumerable<string> Imena()
        {
            return roka.ImenaKart();
        }
        public Karta Peek(int št)
        {
            return roka.Peek(št);
        }
        public void SortRoka()
        {
            roka.Sort();
        }
    }
}
