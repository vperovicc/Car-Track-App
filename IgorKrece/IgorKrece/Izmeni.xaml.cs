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

namespace IgorKrece
{
    /// <summary>
    /// Interaction logic for Izmeni.xaml
    /// </summary>
    public partial class Izmeni : Window
    {
        private Vozac driver;
        public Izmeni(Vozac selectedDriver)
        {
            InitializeComponent();
            driver = selectedDriver;

            // Populate the form fields with the driver's information
            PopulateFormFields();
        }
        private void PopulateFormFields()
        {
            // Set the TextBox values based on the selected driver
            txtIme.Text = driver.Ime;
            txtPrezime.Text = driver.Prezime;
            txtID.Text = driver.ID.ToString();
            txtTim.Text = driver.Tim;
            txtNacionalnost.Text = driver.Nacionalnost;
            txtBrojSasije.Text = driver.BrojSasije.ToString();
            txtBrojTrka.Text = driver.BrojTrka.ToString();
            txtBrojPobeda.Text = driver.BrojPobeda.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            // Update the selected driver's information based on the form fields
            driver.Ime = txtIme.Text;
            driver.Prezime = txtPrezime.Text;
            driver.ID = int.Parse(txtID.Text);
            driver.Tim = txtTim.Text;
            driver.Nacionalnost = txtNacionalnost.Text;
            driver.BrojSasije = int.Parse(txtBrojSasije.Text);
            driver.BrojTrka = int.Parse(txtBrojTrka.Text);
            driver.BrojPobeda = int.Parse(txtBrojPobeda.Text);
            // Close the form
            Close();
        }
    }


}
