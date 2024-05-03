using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestoreEventi
{
    public class ProgrammaEventi
    {
        public string Titolo { get; private set; }
        public List<Evento> Eventi { get; private set; }

        public ProgrammaEventi(string titolo)
        {
            Titolo = titolo;
            Eventi = new List<Evento>();
        }

        public void AggiungiEvento(Evento evento)
        {
            Eventi.Add(evento);
        }

        public List<Evento> EventiInData(DateTime data)
        {
            return Eventi.Where(e => e.Data.Date == data.Date).ToList();
        }

        public static string StampaListaEventi(List<Evento> listaEventi)
        {
            if (listaEventi == null || listaEventi.Count == 0)
            {
                return "Nessun evento presente.";
            }

            string result = string.Join("\n", listaEventi.Select(e => $"{e.Data.ToString("dd/MM/yyyy")} - {e.Titolo}"));
            return result;
        }

        public int ConteggioEventi()
        {
            return Eventi.Count;
        }

        public void SvuotaEventi()
        {
            Eventi.Clear();
        }

        public string RappresentazioneStringa()
        {
            string result = $"{Titolo}:\n";
            foreach (var evento in Eventi)
            {
                result += $"{evento.Data.ToString("dd/MM/yyyy")} - {evento.Titolo}\n";
            }
            return result;
        }
    }
}

