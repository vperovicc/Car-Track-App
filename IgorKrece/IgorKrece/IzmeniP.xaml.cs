using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for IzmeniP.xaml
    /// </summary>
    public partial class IzmeniP : Window
    {

        private Proizvodjac pro;
        String Naziv = "";
        private ObservableCollection<ProTemp> MOD;
        private string staro;
        public IzmeniP(Proizvodjac selec,ObservableCollection<ProTemp> Modeli)
        {
            InitializeComponent();
            MOD = Modeli;
            pro = selec;

            // Populate the form fields with the driver's information
            PopulateFormFields();
        }
        private void PopulateFormFields()
        {
            // Set the TextBox values based on the selected driver
            proizvodID.Text = pro.ID.ToString();
            staro = pro.Naziv;
            proizvodLogo.Text = pro.Logo;
            proizvodSediste.Text = pro.Sediste;
            proizvodNaziv.Text = pro.Naziv;
            



        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Update the selected driver's information based on the form fields
            pro.Naziv = proizvodNaziv.Text;
            pro.Logo = proizvodLogo.Text;
            pro.ID = int.Parse(proizvodID.Text);
            pro.Sediste = proizvodSediste.Text;

            int brojac = 0;

            if (staro != pro.Naziv)
            {
                
                foreach(ProTemp pt in MOD)
                {
                    if (pt.Ime.Equals(staro))
                    {
                        brojac++;
                        pt.Izbrisi(pro);

                    }else if (pt.Ime.Equals(pro.Naziv))
                    {
                        pt.Dodaj(pro);
                    }
                }
            }

            if (brojac == 0)
            {
                ProTemp novi = new ProTemp();
                novi.Ime = pro.Naziv;
                novi.Dodaj(pro);
                MOD.Add(novi);
                brojac = 0;
            }
            
            
            // Close the form
            Close();
        }

        
           
    }
}
