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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace gestione_mercato
{
    /// <summary>
    /// Logica di interazione per Acquisti.xaml
    /// </summary>
    public partial class Acquisti : Window
    {
        public Acquisti(ObservableCollection<Calciatore> c)
        {
            InitializeComponent();
            lstAcquisti.ItemsSource = c;
        }
    }
}
