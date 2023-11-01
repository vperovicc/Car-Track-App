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
    /// Interaction logic for IzmeniProizvodjaca.xaml
    /// </summary>
    public partial class IzmeniProizvodjaca : Window
    {
        private ObservableCollection<ProTemp> pro;
        String Naziv="";
        private Proizvodjac selektovan;
        public IzmeniProizvodjaca(ObservableCollection<ProTemp> zz)
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
                string NNN = Naziv;
                int ID = prID;
                string Sediste = proizvodSediste.Text;
                string Logo = proizvodLogo.Text;
                Proizvodjac pr = new Proizvodjac(ID, NNN, Sediste, Logo);
                bool dozvola = true;

                foreach(ProTemp pt in pro)
                {
                    dozvola = pt.Proveri(ID);
                    if (!dozvola)
                    {
                        break;
                    }
                }

                foreach (ProTemp pt in pro)
                {
                    if (!dozvola)
                    {
                        MessageBox.Show("Neuspesno dododavanje");
                        break;
                    }

                    if (pt.Ime.Equals(Naziv))
                    {
                        if (pt.Dodaj(pr))
                            MessageBox.Show("Uspesno dodavanje!");
                        StreamWriter w = new StreamWriter("proizvodjacii.txt", true);
                        w.WriteLine($"{pr.ID},{pr.Naziv},{pr.Sediste},{pr.Logo}");
                        w.Flush();
                    }

                }


            }


            this.Close();
        }

        private void imena_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (imena.SelectedItem != null)
            {
                string selectedItemContent = imena.SelectedItem.ToString();
                int index = selectedItemContent.IndexOf(":"); // Pronalaženje pozicije znaka ":"
                if (index != -1)
                {
                    Naziv = selectedItemContent.Substring(index + 1).Trim(); // Izdvajanje dela nakon ":" i uklanjanje eventualnih praznih mesta

                }
            }

        }
    }
}
