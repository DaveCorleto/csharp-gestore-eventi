using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.ConstrainedExecution;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace GestoreEventi
{

    internal class Evento 
    {
        private string _titolo;
        private DateTime _data;
        private int _capienzaMassima;
        private int _postiPrenotati;

        public string Titolo
        {
            get { return _titolo; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Il titolo non può essere vuoto o composto solo da spazi.", nameof(Titolo));
                }
                _titolo = value;
            }
        }

        public DateTime Data
        {
            get { return _data.Date; }
            set
            {
                if (value < DateTime.Now.Date)
                {
                    throw new ArgumentException("Mi dispiace, non è possibile inserire una data passata. " +
                        "Inserisci almeno la data di domani.", nameof(Data));
                }
                _data = value.Date;
            }
        }

        public int CapienzaMassima
        {
            get { return _capienzaMassima; }
            private set
            {
                _capienzaMassima = Math.Max(value, 0); 
            }
        }

        public int PostiPrenotati
        {
            get { return _postiPrenotati; }
            private set { _postiPrenotati = value; }
        }

        
        public Evento(string titolo, DateTime data, int capienzaMassima)
        {
            Titolo = titolo;
            Data = data;
            CapienzaMassima = capienzaMassima;
            PostiPrenotati = 0; 
        }
        //        Vanno inoltre implementati dei metodi public che svolgono le seguenti funzioni:

        //1. PrenotaPosti: aggiunge i posti passati come parametro ai posti prenotati.Se l’evento è già passato o non ha posti o non ha più posti disponibili deve sollevare un’eccezione.
        //2. DisdiciPosti: riduce del i posti prenotati del numero di posti indicati come parametro.Se l’evento è già passato o non ci sono i posti da disdire sufficienti, deve sollevare un’eccezione.
        //3. l’override del metodo ToString() in modo che venga restituita una stringa contenente: data formattata – titolo
        //Per formattare la data correttamente usate nomeVariabile.ToString("dd/MM/yyyy"); applicata alla vostra variabile DateTime.
        public void CalcolaPostiDisponibili()
        {
            int postiDisponibili = CapienzaMassima - PostiPrenotati;
            Console.WriteLine($"I posti disponibili per questo evento sono: {postiDisponibili}");
        }

        public void PrenotaPosti(int postiDaPrenotare)
        {
            if (postiDaPrenotare > CapienzaMassima - PostiPrenotati)
            {
                throw new ArgumentException("Non ci sono abbastanza posti disponibili.");
            }
            if (postiDaPrenotare < 0) 
            {
                throw new ArgumentException("Seleziona un numero positivo di biglietti da prenotare per favore...fai il serio");
            }
            if (Data < DateTime.Now.Date)
            {
                throw new InvalidOperationException("Impossibile prenotare posti per un evento passato.");
            }

            PostiPrenotati += postiDaPrenotare;
        }
        public void PrenotaPostiDaInput()
        {
            Console.WriteLine("Vuoi prenotare dei posti per questo evento? (SI/NO)");
            string risposta = Console.ReadLine().Trim().ToUpper();

            if (risposta == "SI" || risposta == "S")
            {
                Console.Write("Inserisci il numero di posti da prenotare: ");
                if (int.TryParse(Console.ReadLine(), out int postiDaPrenotare))
                {
                    if (postiDaPrenotare > CapienzaMassima - PostiPrenotati)
                    {
                        throw new ArgumentException("Spiaze...Non ci sono abbastanza posti.");
                    }
                    if (postiDaPrenotare < 0)
                    {
                        throw new ArgumentException("Inserisci un numero positivo di biglietti da prenotare.");
                    }
                    if (Data < DateTime.Now.Date)
                    {
                        throw new InvalidOperationException("Impossibile prenotare posti per un evento passato.");
                    }

                    PostiPrenotati += postiDaPrenotare;
                    Console.WriteLine($"Hai prenotato {postiDaPrenotare} posti per l'evento.");
                }
                else
                {
                    throw new ArgumentException("Inserisci un numero valido di posti da prenotare.");
                }
            }
            else if (risposta == "NO" || risposta == "N")
            {
                Console.WriteLine("Hai scelto di non prenotare posti per questo evento.");
            }
            else
            {
                Console.WriteLine("Inserisci o 'SI' o 'NO'.");
            }
        }


        public void DisdiciPosti(int postiDaDisdire)
        {
            if (postiDaDisdire > PostiPrenotati)
            {
                throw new ArgumentException("Non puoi disdire più posti di quelli che sono stati prenotati");
            }
            if (PostiPrenotati < 0)
            {
                throw new ArgumentException("Seleziona un numero positivo di biglietti da disdire per favore...fai il serio");
            }
            PostiPrenotati -= postiDaDisdire;
            if (Data < DateTime.Now.Date)
            {
                throw new InvalidOperationException("Impossibile disdire posti per un evento passato.");
            }
        }

        public void DisdiciPostiDaInput()
        {
            string risposta;
            bool rispostaValida = false;

            while (!rispostaValida)
            {
                Console.WriteLine("Desideri disdire dei posti? (SI/NO)");
                risposta = Console.ReadLine().ToUpper();

                if (risposta == "SI" || risposta == "S")
                {
                    Console.WriteLine("Quanti posti vuoi disdire?");
                    int postiDaDisdire;

                    while (!int.TryParse(Console.ReadLine(), out postiDaDisdire) || postiDaDisdire <= 0)
                    {
                        Console.WriteLine("Inserisci un numero intero maggiore di zero.");
                    }

                    DisdiciPosti(postiDaDisdire);
                    Console.WriteLine($"Hai disdetto {postiDaDisdire} posti");

                    int postiDisponibili = CapienzaMassima - PostiPrenotati;
                    Console.WriteLine($"Sono disponibili ancora {postiDisponibili} posti");

                    rispostaValida = true; // Esce dal ciclo
                }
                else if (risposta == "NO" || risposta == "N")
                {
                    Console.WriteLine("Hai scelto di non disdire nessun posto");
                    rispostaValida = true;
                }
                else
                {
                    Console.WriteLine("Risposta non valida. Inserisci 'SI' o 'NO'.");
                }
            }
        }



        //Sovrascrivo il ToString per ottenere una formattazione migliore 

        public override string ToString()
        {        
            string dataFormat = _data.ToString("dd/MM/yyyy");
            return $"{dataFormat} - {Titolo}";
        }


    }
        

        
}
         
        
