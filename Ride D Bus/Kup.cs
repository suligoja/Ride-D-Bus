using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ride_D_Bus
{
    class Kup //lahko so vse karte ali karte, ki jih ima uporabnik v roki
    {
        List<Karta> karte;
        Random r = new Random();
        public Kup()
        {
            //konstruktor kupa - vse karte
            karte = new List<Karta>();
            for (int b=0;b<=3;b++)
                for (int v=1;v<=13;v++)
                    karte.Add(new Karta((Barve)b,(Vrednosti)v));
        }
        public Kup(IEnumerable<Karta> začetek)
        {
            //konstruktor - nekatere karte , v roki igralce
            karte = new List<Karta>(začetek);
        }
        public void Add(Karta novaKarta)
        {
            karte.Add(novaKarta);
        }
        public int Count
        { get { return karte.Count; } }
        public void Sort()
        {
            karte.Sort(new Primerjava());
        }
        public Karta Deli(int indeks)
        {
            Karta zaDelitev = karte[indeks];
            karte.RemoveAt(indeks);
            return zaDelitev;
        }
        public void Mešaj()
        {
            List<Karta> noveKarte = new List<Karta>();
            while (karte.Count > 0)
            { 
                int zaPremik=r.Next(karte.Count);
                noveKarte.Add(karte[zaPremik]);
                karte.RemoveAt(zaPremik);
            }
            karte = noveKarte;
        }
        public IEnumerable<string> ImenaKart() {
            string[] imena = new string[karte.Count];
            for (int i = 0; i < karte.Count; i++)
                imena[i] = karte[i].Ime;
            return imena;
        }
        public Karta Peek(int št)
        {
            //pogledam karto v kupu, ne da bi jo razdelila
            return karte[št];
        }
        public Karta Deli()
        {
            //vrne karto z vrha kupa
            return Deli(0);
        }
        public bool ContainsValue(Vrednosti v)
        {
            //pregleda ali je v kupu karta z dano vrednostjo--imaš 6? 
            foreach (Karta k in karte)
                if (k.Vrednost == v)
                    return true;
             return false;
        }
        public Kup PullOutValues(Vrednosti v)
        {
            //Uporabljali bomo za odlaganje kompleta kart. Pregelda vse karte, ki so v
            //kupu- roki igralca in izloči tiste, ki imajo določeno vrednost
            Kup rezultat = new Kup(new Karta[] { });
            for (int i = karte.Count - 1; i >= 0; i--)
                if (karte[i].Vrednost == v)
                    rezultat.Add(Deli(i));
            return rezultat;
        }
        public bool JeKomplet(Vrednosti v)
        {
            //preveri ali imamo 4 karte iste vrednosti v kupu-roki
            int št = 0;
            foreach (Karta k in karte)
                if (k.Vrednost == v)
                    št++;
            if (št == 4)
                return true;
            return false;
        }
    }
}
