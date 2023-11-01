using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.DirectoryServices;
using System.IO;
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

namespace IgorKrece
{
    /// <summary>
    /// Interaction logic for DodajVozaca.xaml
    /// </summary>
    /// 
    
    public partial class DodajVozaca : Window
    {
        private ObservableCollection<Vozac> vozaci;
        public DodajVozaca(ObservableCollection<Vozac> vozaci)
        {
            InitializeComponent();
            this.vozaci = vozaci;
        }

        private void btnDodaj_Click(object sender, RoutedEventArgs e)
        {



        }
    }
}
