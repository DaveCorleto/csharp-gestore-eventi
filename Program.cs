using System.Security.Cryptography.X509Certificates;

namespace GestoreEventi
{
    using System;

    internal class Program
    {
        public static int LeggiNumerodiPostiDaPrenotare(string richiediNBiglietti)
        {
            Console.WriteLine(richiediNBiglietti);
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int numero))
                {
                    return numero;
                }
                else
                {
                    Console.WriteLine("Input non valido. Inserisci un numero intero valido.");
                }
            }
        }

        public static int LeggiNumerodiPostiDaDisdire(string richiediNBigliettiDaDisdire)
        {
            Console.WriteLine(richiediNBigliettiDaDisdire);
            while (true)
            {
                if (int.TryParse(Console.ReadLine(), out int numero))
                {
                    return numero;
                }
                else
                {
                    Console.WriteLine("Input non valido. Inserisci un numero intero valido.");
                }
            }
        }

        static void Main(string[] args)
        {
            //Istanziazione degli eventi 

            Evento BillieEilish2Giugno = new Evento("Billie Eilish al forum di Assago", new DateTime(2024, 6, 2), 6000);


            string descrizione = BillieEilish2Giugno.ToString();
            Console.WriteLine(descrizione);

            string richiediNBiglietti = "Vuoi prenotare uno o più biglietti? Inserisci qui il numero dei biglietti che vuoi prenotare";
            string richiediNBigliettiDaDisdire = "Vuoi disdire uno o più biglietti? Inserisci qui il numero dei biglietti che vuoi disdire";
            int postiDaPrenotare = LeggiNumerodiPostiDaPrenotare(richiediNBiglietti);

            BillieEilish2Giugno.ToString();

            BillieEilish2Giugno.PrenotaPosti(postiDaPrenotare);
            Console.WriteLine($"Congratulazioni, hai appena prenotato {postiDaPrenotare} posti per {BillieEilish2Giugno.Titolo}");
            Console.WriteLine($"Al momento per questo evento sono prenotati {BillieEilish2Giugno.PostiPrenotati} posti");
            int postiDaDisdire = LeggiNumerodiPostiDaDisdire(richiediNBigliettiDaDisdire);
            BillieEilish2Giugno.DisdiciPosti(postiDaDisdire);

            Console.WriteLine($"Hai appena disdetto {postiDaDisdire} posti per {BillieEilish2Giugno.Titolo}");
            Console.WriteLine($"Al momento per questo evento sono prenotati {BillieEilish2Giugno.PostiPrenotati} posti");


        }
    }


}
