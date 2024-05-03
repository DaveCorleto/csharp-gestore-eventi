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


        public static Evento CreaEventoDaInput(ProgrammaEventi programma)
        {
            string titolo = null;
            DateTime data = DateTime.MinValue;
            int capienzaMassima = 0;

            Console.WriteLine("Inserisci il nome dell'evento che vuoi creare: ");
            titolo = Console.ReadLine();
            if (string.IsNullOrEmpty(titolo))
            {
                throw new ArgumentException("Il titolo dell'evento non può essere nullo...a meno che non si tratti di una MasterClass sul Minimalismo");
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Inserisci numero massimo di partecipanti all'evento: ");
                    capienzaMassima = LeggiEConvertiNumero();

                    // Se si arriva a questo punto senza eccezioni, usciamo dal ciclo
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore durante l'inserimento della capienza massima: {ex.Message}");
                }
            }

            while (true)
            {
                try
                {
                    Console.WriteLine("Inserisci la data dell'evento (formato dd/MM/yyyy): ");
                    data = LeggiEConvertiData();

                    // Se si arriva a questo punto senza eccezioni, usciamo dal ciclo
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore durante l'inserimento della data: {ex.Message}");
                }
            }

            Evento nuovoEvento = new Evento(titolo, data, capienzaMassima);

            programma.AggiungiEvento(nuovoEvento);

            Console.WriteLine($"Congratulazioni! Hai creato l'evento:\n" +
                              $"Titolo: {nuovoEvento.Titolo}\n" +
                              $"Data: {nuovoEvento.Data.ToString("dd/MM/yyyy")}\n" +
                              $"Capienza Massima: {nuovoEvento.CapienzaMassima}");

            return nuovoEvento;
        }



        public static DateTime LeggiEConvertiData()
        {
            while (true)
            {
                Console.Write("Data evento (formato dd/MM/yyyy): ");
                if (DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime data))
                {
                    return data;
                }
                else
                {
                    Console.WriteLine("La data che hai fornito ha un formato non valido. Riprova.");
                }
            }
        }

        public static int LeggiEConvertiNumero()
        {
            while (true)
            {
                Console.Write(" ");
                if (int.TryParse(Console.ReadLine(), out int numero) && numero > 0)
                {
                    return numero;
                }
                else
                {
                    Console.WriteLine("Inserisci un numero intero maggiore di zero.");
                }
            }
        }


        static void Main(string[] args)
        {

            //Istanziazione degli eventi standard. Come prova

            //Evento BillieEilish2Giugno = new Evento("Billie Eilish al forum di Assago", new DateTime(2024, 6, 2), 6000);


            //string descrizione = BillieEilish2Giugno.ToString();
            //Console.WriteLine(descrizione);

            //string richiediNBiglietti = "Vuoi prenotare uno o più biglietti? Inserisci qui il numero dei biglietti che vuoi prenotare";
            //string richiediNBigliettiDaDisdire = "Vuoi disdire uno o più biglietti? Inserisci qui il numero dei biglietti che vuoi disdire";
            //int postiDaPrenotare = LeggiNumerodiPostiDaPrenotare(richiediNBiglietti);

            //BillieEilish2Giugno.ToString();

            //BillieEilish2Giugno.PrenotaPosti(postiDaPrenotare);
            //Console.WriteLine($"Congratulazioni, hai appena prenotato {postiDaPrenotare} posti per {BillieEilish2Giugno.Titolo}");
            //Console.WriteLine($"Al momento per questo evento sono prenotati {BillieEilish2Giugno.PostiPrenotati} posti");
            //int postiDaDisdire = LeggiNumerodiPostiDaDisdire(richiediNBigliettiDaDisdire);
            //BillieEilish2Giugno.DisdiciPosti(postiDaDisdire);

            //Console.WriteLine($"Hai appena disdetto {postiDaDisdire} posti per {BillieEilish2Giugno.Titolo}");
            //Console.WriteLine($"Al momento per questo evento sono prenotati {BillieEilish2Giugno.PostiPrenotati} posti");

            //Istanzio il programma 
            ProgrammaEventi programma = new ProgrammaEventi("Programma Eventi");

            Evento ANightWithOphra = CreaEventoDaInput(programma);

            Console.WriteLine(ANightWithOphra.ToString());

            Console.WriteLine("Vuoi prenotare dei posti per questo evento?");
            ANightWithOphra.PrenotaPostiDaInput();

            ANightWithOphra.CalcolaPostiDisponibili();
            ANightWithOphra.DisdiciPostiDaInput();



        }
    }


}
