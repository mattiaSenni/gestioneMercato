using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gestione_mercato
{
    public class Calciatore
    {
        private string _ruolo;
        public string Ruolo
        {
            get
            {
                return _ruolo;
            }
            set
            {
                string nome = value.ToLower();
                if (nome == "por" || nome == "dc" || nome == "td" || nome == "ts" || nome == "cc" || nome == "coc" || nome == "ed" || nome == "es" || nome == "att")
                {
                    _ruolo = nome;
                }
                else
                    throw new Exception("il ruolo non è valido");
            }
        }

        public string Nome { get; set; }

        private int _costo;
        public int Costo
        {
            get
            {
                return _costo;
            }
            set
            {
                if(value < 0)
                {
                    throw new Exception("costo non valido");
                }
                _costo = value;
            }
        }

        public bool Selezionato { get; set; }

        public bool Comprato
        {
            get;
            private set;
        }

        private int _aquisto;
        public int Aquisto 
        {
            get
            {
                if (Comprato)
                    return _aquisto;
                else
                    throw new Exception("il giocatore non è satato comprato");
            }
            set
            {
                if(value < 0)
                {
                    if (value == -1)
                        Comprato = false;
                    else
                        throw new Exception("prezzo non valido");
                }
                Comprato = true;
                _aquisto = value;
            }
        }

        public Calciatore(string n, string r, int c, bool s)
        {
            Nome = n; Ruolo = r; Costo = c; Selezionato = s;
        }

        public void ResettaAcquisto()
        {
            Comprato = false;
        }


        public string ScriviSuFile(char split)
        {
            if (Comprato)
                return Nome + split + Ruolo + split + Costo + split + Selezionato + split + Comprato + split + Aquisto;
            else
                return Nome + split + Ruolo + split + Costo + split + Selezionato + split + Comprato;
        }
    }
}
