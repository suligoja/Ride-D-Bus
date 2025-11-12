using System.Numerics;
using System.Text;

namespace Ride_D_Bus
{
    public partial class Form1 : Form
    {
        private Random random = new Random();
        private Kup kup;
        private Karta PrvaKarta;
        private Karta DrugaKarta;
        private int Rounda = 1;
        private int Bonus = 0;
        private int Denar = 10000;
        private int Bet;
        private System.Windows.Forms.Timer zapriTimer;
        private System.Windows.Forms.Timer resetKartTimer;
        private PictureBox[] vseKarte;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            txtDenar.Text = $"{Denar} €";
            zapriTimer = new System.Windows.Forms.Timer();
            zapriTimer.Interval = 3000; // 3 sekunde
            zapriTimer.Tick += ZapriTimer_Tick;

            resetKartTimer = new System.Windows.Forms.Timer();
            resetKartTimer.Interval = 1500; // 1,5 sekundi
            resetKartTimer.Tick += ResetKartTimer_Tick;

            // shrani reference na PictureBox-e
            vseKarte = new PictureBox[] { karta1, karta2, karta3, karta4 };

            foreach (var k in vseKarte)
            {
                k.Image = new Bitmap("karte/karta.jpg");
            }

        }
        private void ZapriTimer_Tick(object sender, EventArgs e)
        {
            zapriTimer.Stop();
            Application.Exit();
        }
        private void ResetKartTimer_Tick(object sender, EventArgs e)
        {
            resetKartTimer.Stop();

            foreach (var k in vseKarte)
            {
                k.Image = new Bitmap("karte/karta.jpg");
            }
            btnBet.Visible = true;
        }
        private void resetvse()
        {
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            Rounda = 1;
            Bonus = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Rounda == 1)
            {
                if (kup == null || kup.Count == 0)
                {
                    kup = new Kup();
                    kup.Mešaj();
                }

                PrvaKarta = kup.Deli();
                int vrednostIndex = (int)PrvaKarta.Vrednost;
                string slikakarte = $"{PrvaKarta.Barva}{vrednostIndex}";
                karta1.Image = new Bitmap($"karte/{slikakarte}.jpg");
                textBox2.Text = $"Karta: {PrvaKarta.Ime}{Environment.NewLine}";

                if (PrvaKarta.Barva == Barve.Karo || PrvaKarta.Barva == Barve.Srce)
                {
                    Rounda = 2;
                    Bonus = 2;
                    button1.Text = "Višje";
                    button2.Text = "Nižje";
                    textBox2.Text += $"Prehajamo v rundo 2!{Environment.NewLine}";
                }
                else
                {
                    textBox2.Text += "Karta ni rdeèa. poskusi ponovno" + Environment.NewLine;
                    resetvse();
                    resetKartTimer.Start();
                    PreveriDenar();

                }

                txtBet.Text = $" {Bet * Bonus} €";
            }
            else if (Rounda == 2)
            {
                if (kup == null || kup.Count == 0)
                {
                    kup = new Kup();
                    kup.Mešaj();
                }

                DrugaKarta = kup.Deli();
                int vrednostIndex = (int)DrugaKarta.Vrednost;
                string slikakarte = $"{DrugaKarta.Barva}{vrednostIndex}";
                karta2.Image = new Bitmap($"karte/{slikakarte}.jpg");
                textBox2.Text = $"Karta: {PrvaKarta.Ime}{Environment.NewLine}Nova karta: {DrugaKarta.Ime}.{Environment.NewLine}";


                // preverimo, èe je nova karta višja
                if (DrugaKarta.Vrednost > PrvaKarta.Vrednost)
                {
                    Rounda = 3;
                    Bonus = 3;
                    button1.Text = "Vmes";
                    button2.Text = "Izven";
                    textBox2.Text += $"Prehajamo v rundo 3!{Environment.NewLine}";
                }
                else
                {
                    textBox2.Text += "Karta ni višja. Poskusi ponovno" + Environment.NewLine;
                    resetvse();
                    resetKartTimer.Start();
                    PreveriDenar();

                }

                txtBet.Text = $" {Bet * Bonus} €";
            }
            else if (Rounda == 3)
            {
                if (kup == null || kup.Count == 0)
                {
                    kup = new Kup();
                    kup.Mešaj();
                }

                Karta TretjaKarta = kup.Deli();
                int vrednostIndex = (int)TretjaKarta.Vrednost;
                string slikakarte = $"{TretjaKarta.Barva}{vrednostIndex}";
                karta3.Image = new Bitmap($"karte/{slikakarte}.jpg");
                textBox2.Text = $"Karta: {TretjaKarta.Ime}{Environment.NewLine}";


                if ((PrvaKarta.Vrednost < TretjaKarta.Vrednost && TretjaKarta.Vrednost < DrugaKarta.Vrednost) ||
                    (PrvaKarta.Vrednost > TretjaKarta.Vrednost && TretjaKarta.Vrednost > DrugaKarta.Vrednost))
                {
                    Rounda = 4;
                    Bonus = 4;
                    button1.Text = "Pik";
                    button2.Text = "Križ";
                    button3.Visible = true;
                    button4.Visible = true;
                    textBox2.Text += $"Prehajamo v rundo 4!{Environment.NewLine}";
                }
                else
                {
                    textBox2.Text += "Karta ni vmes. Poskusi ponovno" + Environment.NewLine;
                    resetvse();
                    resetKartTimer.Start();
                    PreveriDenar();

                }

                txtBet.Text = $" {Bet * Bonus} €";
            }
            else if (Rounda == 4)
            {
                if (kup == null || kup.Count == 0)
                {
                    kup = new Kup();
                    kup.Mešaj();
                }

                Karta ÈetrtaKarta = kup.Deli();
                int vrednostIndex = (int)ÈetrtaKarta.Vrednost;
                string slikakarte = $"{ÈetrtaKarta.Barva}{vrednostIndex}";
                karta4.Image = new Bitmap($"karte/{slikakarte}.jpg");
                textBox2.Text = $"Karta: {ÈetrtaKarta.Ime}{Environment.NewLine}";

                if (ÈetrtaKarta.Barva == Barve.Pik)
                {
                    Bonus = 20;
                    int dobitek = Bet * Bonus;
                    Denar += dobitek;
                    txtDenar.Text = $"{Denar} €";

                    textBox2.Text += $"Èestitke! Prejeli ste {dobitek} €{Environment.NewLine}";

                    // reset po izplaèilu
                    resetvse();
                    resetKartTimer.Start();
                }
                else
                {
                    textBox2.Text += "Žal, niste zmagali. Poskusi ponovno." + Environment.NewLine;
                    resetvse();
                    resetKartTimer.Start();
                    PreveriDenar();

                }

                txtBet.Text = $" {Bet * Bonus} €";
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (Rounda == 1)
            {
                if (kup == null || kup.Count == 0)
                {
                    kup = new Kup();
                    kup.Mešaj();
                }

                PrvaKarta = kup.Deli();
                int vrednostIndex = (int)PrvaKarta.Vrednost;
                string slikakarte = $"{PrvaKarta.Barva}{vrednostIndex}";
                karta1.Image = new Bitmap($"karte/{slikakarte}.jpg");
                textBox2.Text = $"Karta: {PrvaKarta.Ime}{Environment.NewLine}";

                if (PrvaKarta.Barva == Barve.Pik || PrvaKarta.Barva == Barve.Križ)
                {
                    Rounda = 2;
                    Bonus = 2;
                    button1.Text = "Višje";
                    button2.Text = "Nižje";
                    textBox2.Text += $"Prehajamo v rundo 2!{Environment.NewLine}";
                }
                else
                {
                    textBox2.Text += "Karta ni èrna. poskusi ponovno" + Environment.NewLine;
                    resetvse();
                    resetKartTimer.Start();
                    PreveriDenar();

                }

                txtBet.Text = $" {Bet * Bonus} €";
            }
            else if (Rounda == 2)
            {
                if (kup == null || kup.Count == 0)
                {
                    kup = new Kup();
                    kup.Mešaj();
                }

                DrugaKarta = kup.Deli();
                int vrednostIndex = (int)DrugaKarta.Vrednost;
                string slikakarte = $"{DrugaKarta.Barva}{vrednostIndex}";
                karta2.Image = new Bitmap($"karte/{slikakarte}.jpg");
                textBox2.Text = $"Karta: {PrvaKarta.Ime}{Environment.NewLine}Nova karta: {DrugaKarta.Ime}.{Environment.NewLine}";

                // preverimo, èe je nova karta višja
                if (DrugaKarta.Vrednost < PrvaKarta.Vrednost)
                {
                    Rounda = 3;
                    Bonus = 3;
                    button1.Text = "Vmes";
                    button2.Text = "Izven";
                    textBox2.Text += $"Prehajamo v rundo 3!{Environment.NewLine}";
                }
                else
                {
                    textBox2.Text += "Karta ni Nižja. Poskusi ponovno" + Environment.NewLine;
                    resetvse();
                    resetKartTimer.Start();
                    PreveriDenar();

                }

                txtBet.Text = $" {Bet * Bonus} €";
            }
            else if (Rounda == 3)
            {
                if (kup == null || kup.Count == 0)
                {
                    kup = new Kup();
                    kup.Mešaj();
                }

                Karta TretjaKarta = kup.Deli();
                int vrednostIndex = (int)TretjaKarta.Vrednost;
                string slikakarte = $"{TretjaKarta.Barva}{vrednostIndex}";
                karta3.Image = new Bitmap($"karte/{slikakarte}.jpg");
                textBox2.Text = $"Karta: {TretjaKarta.Ime}{Environment.NewLine}";

                int prva = (int)PrvaKarta.Vrednost;
                int druga = (int)DrugaKarta.Vrednost;
                int tretja = (int)TretjaKarta.Vrednost;

                if (tretja < Math.Min(prva, druga) || tretja > Math.Max(prva, druga))
                {
                    Rounda = 4;
                    Bonus = 4;
                    button1.Text = "Pik";
                    button2.Text = "Križ";
                    button3.Visible = true;
                    button4.Visible = true;
                    textBox2.Text += $"Prehajamo v rundo 4!{Environment.NewLine}";
                }
                else
                {
                    textBox2.Text += "Karta ni izven. Poskusi ponovno" + Environment.NewLine;
                    resetvse();
                    resetKartTimer.Start();
                    PreveriDenar();

                }

                txtBet.Text = $" {Bet * Bonus} €";
            }
            else if (Rounda == 4)
            {
                if (kup == null || kup.Count == 0)
                {
                    kup = new Kup();
                    kup.Mešaj();
                }

                Karta ÈetrtaKarta = kup.Deli();
                int vrednostIndex = (int)ÈetrtaKarta.Vrednost;
                string slikakarte = $"{ÈetrtaKarta.Barva}{vrednostIndex}";
                karta4.Image = new Bitmap($"karte/{slikakarte}.jpg");
                textBox2.Text = $"Karta: {ÈetrtaKarta.Ime}{Environment.NewLine}";

                if (ÈetrtaKarta.Barva == Barve.Križ)
                {
                    Bonus = 20;
                    int dobitek = Bet * Bonus;
                    Denar += dobitek;
                    txtDenar.Text = $"{Denar} €";

                    textBox2.Text += $"Èestitke! Prejeli ste {dobitek} €{Environment.NewLine}";

                    resetvse();
                    resetKartTimer.Start();
                }
                else
                {
                    textBox2.Text += "Žal, niste zmagali. Poskusi ponovno." + Environment.NewLine;
                    resetvse();
                    resetKartTimer.Start();
                    PreveriDenar();

                }

                txtBet.Text = $" {Bet * Bonus} €";
            }
        }

        private void btnOut_Click(object sender, EventArgs e)
        {
            // izraèun dobitka (trenutni Bet * Bonus)
            int dobitek = Bet * Bonus;

            Denar += dobitek;
            txtDenar.Text = $"{Denar} €";

            // prikaži sporoèilo
            textBox2.Text = $"Igra zakljuèena! Pobral si {dobitek} €.{Environment.NewLine}";
            Rounda = 1;
            Bonus = 1;
            Bet = 0;

            // posodobi prikaz
            txtBet.Text = "Stava";

            // skrij vse gumbe
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            btnBet.Visible = true;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Rounda == 4)
            {
                if (kup == null || kup.Count == 0)
                {
                    kup = new Kup();
                    kup.Mešaj();
                }

                Karta ÈetrtaKarta = kup.Deli();
                int vrednostIndex = (int)ÈetrtaKarta.Vrednost;
                string slikakarte = $"{ÈetrtaKarta.Barva}{vrednostIndex}";
                karta4.Image = new Bitmap($"karte/{slikakarte}.jpg");
                textBox2.Text = $"Karta: {ÈetrtaKarta.Ime}{Environment.NewLine}";

                if (ÈetrtaKarta.Barva == Barve.Karo)
                {
                    Bonus = 20;
                    int dobitek = Bet * Bonus;
                    Denar += dobitek;
                    txtDenar.Text = $"{Denar} €";

                    textBox2.Text += $"Èestitke! Prejeli ste {dobitek} €{Environment.NewLine}";

                    resetvse();
                    resetKartTimer.Start();
                }
                else
                {
                    textBox2.Text += "Žal, niste zmagali. Poskusi ponovno." + Environment.NewLine;
                    resetvse();
                    resetKartTimer.Start();
                    PreveriDenar();

                }

                txtBet.Text = $" {Bet * Bonus} €";
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (Rounda == 4)
            {
                if (kup == null || kup.Count == 0)
                {
                    kup = new Kup();
                    kup.Mešaj();
                }

                Karta ÈetrtaKarta = kup.Deli();
                int vrednostIndex = (int)ÈetrtaKarta.Vrednost;
                string slikakarte = $"{ÈetrtaKarta.Barva}{vrednostIndex}";
                karta4.Image = new Bitmap($"karte/{slikakarte}.jpg");
                textBox2.Text = $"Karta: {ÈetrtaKarta.Ime}{Environment.NewLine}";

                if (ÈetrtaKarta.Barva == Barve.Srce)
                {
                    Bonus = 20;
                    int dobitek = Bet * Bonus;
                    Denar += dobitek;
                    txtDenar.Text = $"{Denar} €";

                    textBox2.Text += $"Èestitke! Prejeli ste {dobitek} €{Environment.NewLine}";

                    resetvse();
                    resetKartTimer.Start();
                }
                else
                {
                    textBox2.Text += "Žal, niste zmagali. Poskusi ponovno." + Environment.NewLine;
                    resetvse();
                    resetKartTimer.Start();
                    PreveriDenar();

                }

                txtBet.Text = $" {Bet * Bonus} €";
            }
        }

        private void btnBet_Click(object sender, EventArgs e)
        {
            karta1.Image = new Bitmap($"karte/karta.jpg");
            karta2.Image = new Bitmap($"karte/karta.jpg");
            karta3.Image = new Bitmap($"karte/karta.jpg");
            karta4.Image = new Bitmap($"karte/karta.jpg");
            // preverimo, èe je vnos veljaven
            if (int.TryParse(textBox1.Text, out int vpisanBet))
            {
                if (vpisanBet <= 0)
                {
                    textBox2.Text = "Vnesi znesek veèji od 0." + Environment.NewLine;
                    return;
                }

                if (vpisanBet > Denar)
                {
                    textBox2.Text = "Nimaš dovolj denarja za to stavo!" + Environment.NewLine;
                    return;
                }

                // nastavimo stavo
                Bet = vpisanBet;
                Denar -= Bet;

                // posodobimo prikaz
                txtDenar.Text = $"{Denar} €";
                txtBet.Text = $"{Bet} €";

                textBox2.Text = $"Stavil si {Bet} €.{Environment.NewLine}";

                // omogoèimo gumbe za igro
                button1.Visible = true;
                button2.Visible = true;
                button3.Visible = false;
                button4.Visible = false;
                btnBet.Visible = false;

                // ponastavimo igro na prvo rundo
                Rounda = 1;
                Bonus = 0;
                button1.Text = "Rdeèa";
                button2.Text = "Èrna";
            }
            else
            {
                textBox2.Text = "Prosim, vnesi veljavno številko stave." + Environment.NewLine;
            }
        }
        private void PreveriDenar()
        {
            if (Denar <= 0)
            {
                textBox2.Text = "Ups... brez denarja ni zabave! Igra je zate konec." + Environment.NewLine;

                // onemogoèi vse gumbe
                button1.Visible = false;
                button2.Visible = false;
                button3.Visible = false;
                button4.Visible = false;
                btnBet.Enabled = false;
                btnOut.Enabled = false;

                txtBet.Text = "Stava";
                zapriTimer.Start();
            }
        }

        private void BtnNavodila_Click(object sender, EventArgs e)
        {
            NavodilaIgre navodila = new NavodilaIgre();
            navodila.PrikažiNavodila();
        }
    }
}
