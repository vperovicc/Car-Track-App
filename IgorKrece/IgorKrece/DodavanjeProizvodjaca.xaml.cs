using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Shapes;

namespace IgorKrece
{
    /// <summary>
    /// Interaction logic for DodavanjeProizvodjaca.xaml
    /// </summary>
    public partial class DodavanjeProizvodjaca : Window
    {
        public ObservableCollection<ProTemp> pro;
        public string Naziv = "";


        public DodavanjeProizvodjaca(ObservableCollection<ProTemp> zz)
        {
            pro = zz;
            InitializeComponent();

        }

        private void DodajProizvod_Click(object sender, RoutedEventArgs e)
        {
            if (!Int32.TryParse(proizvodID.Text, out int prID))
            {
                MessageBox.Show("ID mora biti broj!", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                string NNN = proizvodNaziv.Text;
                int ID = prID;
                string Sediste = proizvodSediste.Text;
                string Logo = proizvodLogo.Text;
                Proizvodjac pr = new Proizvodjac(ID, NNN, Sediste, Logo);
                bool dozvola = true;

                foreach (ProTemp pt in pro)
                {
                    dozvola = pt.Proveri(ID);
                    if (!dozvola)
                    {
                        dozvola = false;
                        break;
                    }
                }
                int brojac = 0;
                foreach (ProTemp pt in pro)
                {
                    
                    if (!dozvola)
                    {
                        MessageBox.Show("Neuspesno dododavanje");
                       
                        break;
                    }

                    if (pt.Ime.Equals(Naziv))
                    {
                        brojac++;
                        if (pt.Dodaj(pr))
                            MessageBox.Show("Uspesno dodavanje!");
                        StreamWriter w = new StreamWriter("proizvodjacii.txt", true);
                        w.WriteLine($"{pr.ID},{pr.Naziv},{pr.Sediste},{pr.Logo}");
                        w.Flush();
                    }
                   

                }
                if (brojac == 0 && dozvola)
                {
                    brojac = 0;
                    ProTemp pp = new ProTemp();
                    pp.Ime = NNN;
                    pp.Dodaj(pr);
                    pro.Add(pp);

                    MessageBox.Show("Uspesno dodavanje!");
                    StreamWriter w = new StreamWriter("proizvodjacii.txt", true);
                    w.WriteLine($"{pr.ID},{pr.Naziv},{pr.Sediste},{pr.Logo}");
                    w.Flush();

                }


            }


            this.Close();
        }

       
          

        
    }
}
