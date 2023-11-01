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
    /// Interaction logic for NoviTrkac.xaml
    /// </summary>
    public partial class NoviTrkac : Window
    {
        public ObservableCollection<Vozac> vv;
        public NoviTrkac(ObservableCollection<Vozac> vo)
        {
            vv = vo;
            InitializeComponent();
        }

        private void Dodaj_Click(object sender, RoutedEventArgs e)
        {
            int ID = int.Parse(txtID.Text);
            int brojSes = int.Parse(txtBS.Text);
            int brojTrk = int.Parse(txtBT.Text);
            int brojPob = int.Parse(txtBP.Text);
            string Ime = txtIme.Text;
            string Prezime = txtPrezime.Text;
            string Tim = txtTim.Text;
            string Nac = txtNac.Text;

            Vozac voz = new Vozac(ID, Ime, Prezime, Tim, Nac, brojSes, brojTrk, brojPob, txtSlika.Text);

            foreach (Vozac vozic in vv)
            {
                if (vozic.ID == voz.ID)
                {
                    MessageBox.Show("Vec postoji vozac sa tim ID-em");
                    return;

                }
            }
            vv.Add(voz);
            MessageBox.Show("Uspesno dodavanje vozaca");
            StreamWriter w = new StreamWriter("kolaaa.txt", true);
            w.WriteLine($"{voz.ID},{voz.Ime},{voz.Prezime},{voz.Tim},{voz.Nacionalnost},{voz.BrojSasije},{voz.BrojTrka},{voz.BrojPobeda},{voz.Slika}");
            w.Flush();


        }
    }
}
