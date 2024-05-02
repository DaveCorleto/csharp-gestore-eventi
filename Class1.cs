using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
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
            get { return _data; }
            set
            {
                if (value < DateTime.Now.Date)
                {
                    throw new ArgumentException("Mi dispiace, non è possibile inserire una data passata. " +
                        "Inserisci almeno la data di domani.", nameof(Data));
                }
                _data = value;
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
    }

}
