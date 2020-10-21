using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace gestione_mercato
{
    public class ListaCalciatori
    {
        public ObservableCollection<Calciatore> CalciatoriNonAcquistati { get; set; }
        public ObservableCollection<Calciatore> CalciatoriSelezioanti { get; set; }
        public ObservableCollection<Calciatore> CalciatoriAcquistati { get; set; }

        public ListaCalciatori()
        {
            CalciatoriNonAcquistati = new ObservableCollection<Calciatore>();
            CalciatoriSelezioanti = new ObservableCollection<Calciatore>();
            CalciatoriAcquistati = new ObservableCollection<Calciatore>();
        }

        public ListaCalciatori(ObservableCollection<Calciatore> cn, ObservableCollection<Calciatore> cs, ObservableCollection<Calciatore> ca)
        {
            CalciatoriNonAcquistati = cn;
            CalciatoriSelezioanti = cs;
            CalciatoriAcquistati = ca;
        }

        public void Seleziona(Calciatore c)
        {
            CalciatoriNonAcquistati.Remove(c);
            c.Selezionato = true;
            CalciatoriSelezioanti.Add(c);
        }

        public void RipristinaSelezione(Calciatore c)
        {
            CalciatoriNonAcquistati.Add(c);
            c.Selezionato = false;
            CalciatoriSelezioanti.Remove(c);
        }

        public void Acquista(Calciatore c, int p)
        {
            CalciatoriSelezioanti.Remove(c);            
            c.Aquisto = p;
            CalciatoriAcquistati.Add(c);
        }

        public void RipristinaAcquisto(Calciatore c)
        {
            CalciatoriAcquistati.Remove(c);
            c.Aquisto = -1;
            CalciatoriSelezioanti.Add(c);
        }

        public string[] ScriviSuFile(char split)
        {
            string[] toReturn = new string[CalciatoriSelezioanti.Count() + CalciatoriNonAcquistati.Count() + CalciatoriAcquistati.Count()];
            for (int i = 0; i < CalciatoriSelezioanti.Count(); i++)
            {
                toReturn[i] = CalciatoriSelezioanti[i].ScriviSuFile(split);
            }
            for (int i = 0; i < CalciatoriNonAcquistati.Count(); i++)
            {
                toReturn[CalciatoriSelezioanti.Count() + i ] = CalciatoriNonAcquistati[i].ScriviSuFile(split);
            }
            for (int i = 0; i < CalciatoriAcquistati.Count(); i++)
            {
                toReturn[CalciatoriSelezioanti.Count() + CalciatoriNonAcquistati.Count() + i ] = CalciatoriAcquistati[i].ScriviSuFile(split);
            }
            return toReturn;
        }

        public void OrdinaList()
        {
            string[] p = { "por", "dc", "td", "ts", "cc", "ed", "es", "coc", "att" };
            ObservableCollection<Calciatore> na = new ObservableCollection<Calciatore>();
            ObservableCollection<Calciatore> a = new ObservableCollection<Calciatore>();
            ObservableCollection<Calciatore> s = new ObservableCollection<Calciatore>();
            for (int i = 0; i < p.Length; i++)
            {
                foreach(Calciatore c in CalciatoriNonAcquistati)
                {
                    if (c.Ruolo == p[i])
                        na.Add(c);
                }
                foreach(Calciatore c in CalciatoriAcquistati)
                {
                    if (c.Ruolo == p[i])
                        a.Add(c);
                }
                foreach(Calciatore c in CalciatoriSelezioanti)
                {
                    if (c.Ruolo == p[i])
                        s.Add(c);
                }
            }

            CalciatoriNonAcquistati = na;
            CalciatoriAcquistati = a;
            CalciatoriSelezioanti = s;
        }



    }
}
