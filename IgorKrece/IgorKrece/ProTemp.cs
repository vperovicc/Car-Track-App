using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows;


namespace IgorKrece
{

    public class AddCommand : ICommand
    {
        private ProTemp pro;
        public AddCommand(ProTemp z)
        {
            pro = z;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            pro.Proizvodi.Add(new Proizvodjac() { Naziv = "X", ID = 0, Sediste="YY",Logo="ZZ" });
        }
    }

    public class ProTemp : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public bool Dodaj(Proizvodjac p)
        {
            foreach (Proizvodjac prpr in Proizvodi)
            {
                if (prpr.ID == p.ID)
                {
                    MessageBox.Show("Vec postoji Proizvodjac sa tim ID-om!");
                    return false;
                }
            }
            Proizvodi.Add(p);
            return true;
        }

        public void Izbrisi(Proizvodjac pp)
        {
            Proizvodi.Remove(pp);
        }


        private string ime;
        public string Ime
        {
            get
            {
                return ime;
            }
            set
            {
                if (ime != value)
                {
                    ime = value;
                    OnPropertyChanged("Ime");
                }
            }
        }

        private AddCommand add;
        public AddCommand Add
        {
            get
            {
                return add;
            }
            set
            {
                if (add != value)
                {
                    add = value;
                    OnPropertyChanged("Add");
                }
            }
        }

        public ObservableCollection<Proizvodjac> Proizvodi
        {
            get;
            set;
        }

        public ProTemp()
        {
            Ime = "";
            Proizvodi = new ObservableCollection<Proizvodjac>();
            Add = new AddCommand(this);
        }


        public Proizvodjac Pronadji(int ID)
        {
            foreach (Proizvodjac pr in Proizvodi)
            {
                if (pr.ID == ID)
                {
                    Proizvodi.Remove(pr);
                    return pr;
                }
            }
            return null;
        }

        public bool Proveri(int brojID)
        {
            foreach (Proizvodjac prpt in Proizvodi)
            {
                if (prpt.ID == brojID)
                {
                    MessageBox.Show("Vec postoji proizvodjac sa tim ID-em");
                    return false;
                }
            }
            return true;
        }

    }
}
