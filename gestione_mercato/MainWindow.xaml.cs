using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.Collections.ObjectModel;

namespace gestione_mercato
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LeggiFile();
            InizializzaCmbRuoloAG();
        }
        private ListaCalciatori _calciatori;

        private void InizializzaCmbRuoloAG()
        {
            string[] s = { "por", "dc", "td", "ts", "cc", "ed", "es", "coc", "att" };
            cmbRuoloAG.ItemsSource = s;
        }

        public void LeggiFile()
        {
            _calciatori = new ListaCalciatori();
            string[] file = File.ReadAllLines("giocatori.txt");
            for(int i = 0; i < file.Length; i++)
            {
                string[] riga = file[i].Split('|');
                
                Calciatore c = new Calciatore(riga[0], riga[1], int.Parse(riga[2]), bool.Parse(riga[3]));
                if (bool.Parse(riga[4]))
                {                    
                    c.Aquisto = int.Parse(riga[5]);
                }
                if (c.Selezionato)
                    _calciatori.CalciatoriSelezioanti.Add(c);
                else if (c.Comprato)
                    _calciatori.CalciatoriAcquistati.Add(c);
                else
                    _calciatori.CalciatoriNonAcquistati.Add(c);
            }
            _calciatori.OrdinaList();
            RiempiList();
            
        }

        private void RiempiList()
        {
            lstCalciatori.ItemsSource = _calciatori.CalciatoriNonAcquistati;
            lstCalciatoriSelezionati.ItemsSource = _calciatori.CalciatoriSelezioanti;
        }

        private void lstCalciatori_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(lstCalciatori.SelectedIndex != -1)
            {
                btnAquista.IsEnabled = true;
                btnRimuovi.IsEnabled = true;
                lstCalciatoriSelezionati.SelectedIndex = -1;
            }
            else
            {
                btnAquista.IsEnabled = false;
                btnRimuovi.IsEnabled = false;
            }
        }

        private void lstCalciatoriSelezionati_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstCalciatoriSelezionati.SelectedIndex != -1)
            {
                lstCalciatori.SelectedIndex = -1;
                btnRimuoviAquisto.IsEnabled = true;
                btnAcquistaGiocatore.IsEnabled = true;
            }
            else
            {
                btnRimuoviAquisto.IsEnabled = false;
                btnAcquistaGiocatore.IsEnabled = false;
            }
        }

        private void btnAquista_Click(object sender, RoutedEventArgs e)
        {
            _calciatori.Seleziona((Calciatore)lstCalciatori.SelectedItem);
            ScriviFile(_calciatori.ScriviSuFile('|'));
        }

        private void btnRimuovi_Click(object sender, RoutedEventArgs e)
        {
            _calciatori.CalciatoriNonAcquistati.Remove((Calciatore)lstCalciatori.SelectedItem);
            ScriviFile(_calciatori.ScriviSuFile('|'));
        }

        private void btnRimuoviAquisto_Click(object sender, RoutedEventArgs e)
        {
            _calciatori.RipristinaSelezione((Calciatore)lstCalciatoriSelezionati.SelectedItem);
            ScriviFile(_calciatori.ScriviSuFile('|'));
        }

        private void btnAcquistaGiocatore_Click(object sender, RoutedEventArgs e)
        {
            grdHome.Visibility = Visibility.Hidden;
            grdPrezzo.Visibility = Visibility.Visible;
            Calciatore c = (Calciatore)lstCalciatoriSelezionati.SelectedItem;
            lblTestoAcquisto.Content = "a quanto hai acquistato il calciatore " + c.Nome;

        }

        private void btnAcquistoDefinitivo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Calciatore c = (Calciatore)lstCalciatoriSelezionati.SelectedItem;
                _calciatori.Acquista(c, int.Parse(txtPrezzo.Text));
                ScriviFile(_calciatori.ScriviSuFile('|'));
                grdPrezzo.Visibility = Visibility.Hidden;
                grdHome.Visibility = Visibility.Visible;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }

        private void ScriviFile(string[] a)
        {
            _calciatori.OrdinaList();
            File.WriteAllLines("giocatori.txt", a);
        }

        private void btnVediAcquisti_Click(object sender, RoutedEventArgs e)
        {
            Acquisti a = new Acquisti(_calciatori.CalciatoriAcquistati);
            a.Show();
        }

        private void btnIndietroAcquista_Click(object sender, RoutedEventArgs e)
        {
            grdHome.Visibility = Visibility.Visible;
            grdPrezzo.Visibility = Visibility.Hidden;
        }

        private void btnIndietroAG_Click(object sender, RoutedEventArgs e)
        {
            grdHome.Visibility = Visibility.Visible;
            GrdAggiungi.Visibility = Visibility.Hidden;
        }


        private void btnAggiungiCalciatore_Click(object sender, RoutedEventArgs e)
        {
            grdHome.Visibility = Visibility.Hidden;
            GrdAggiungi.Visibility = Visibility.Visible;
        }

        private void btnAggiungiAG_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Calciatore c = new Calciatore(txtNomeAG.Text, (string)cmbRuoloAG.SelectedItem, int.Parse(txtPrezzoAG.Text), false);
                if (_calciatori.CalciatoriAcquistati.Contains(c) || _calciatori.CalciatoriNonAcquistati.Contains(c) || _calciatori.CalciatoriSelezioanti.Contains(c))
                    throw new Exception("calciatore esistente");
                _calciatori.CalciatoriNonAcquistati.Add(c);
                ScriviFile(_calciatori.ScriviSuFile('|'));
                grdHome.Visibility = Visibility.Visible;
                GrdAggiungi.Visibility = Visibility.Hidden;
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRipristinaRA_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if(lstAcquistatiRA.SelectedIndex != -1)
                {
                    _calciatori.RipristinaAcquisto((Calciatore)lstAcquistatiRA.SelectedItem);
                    ScriviFile(_calciatori.ScriviSuFile('|'));
                    grdRipristinaAcquisto.Visibility = Visibility.Visible;
                    grdHome.Visibility = Visibility.Hidden;
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRipristinaAcquisto_Click(object sender, RoutedEventArgs e)
        {
            grdRipristinaAcquisto.Visibility = Visibility.Visible;
            grdHome.Visibility = Visibility.Hidden;
            lstAcquistatiRA.ItemsSource = _calciatori.CalciatoriAcquistati;
        }

        private void btnIndietroRA_Click(object sender, RoutedEventArgs e)
        {
            grdRipristinaAcquisto.Visibility = Visibility.Hidden;
            grdHome.Visibility = Visibility.Visible;
        }
    }
}
