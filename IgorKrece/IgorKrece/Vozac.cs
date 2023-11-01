using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgorKrece
{
    public class Vozac : INotifyPropertyChanged
    {
        public string ime;
        public string prezime;
        public int id;
        public string tim;
        public string nacionalnost;
        public int brojSasije;
        public int brojTrka;
        public int brojPobeda;
        public string slika;

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string v)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public string Slika
        {
            set {
                if (value != slika)
                {
                    slika = value;
                    this.NotifyPropertyChanged("Slika");
                }
            }
            get { return slika; }
        }




        public string Nacionalnost
        {
            get { return nacionalnost; }
            set {
                if (value != nacionalnost)
                {
                    nacionalnost = value;
                    this.NotifyPropertyChanged("Nacionalnost");
                }
            }
        }

        public int BrojSasije
        {
            get { return brojSasije; }
            set {
                if (value != brojSasije)
                {
                    brojSasije = value;
                    this.NotifyPropertyChanged("Broj Sasije");
                }
            }
        }

        public int BrojTrka
        {
            get { return brojTrka; }
            set {
                if (value != brojTrka)
                {
                    brojTrka = value;
                    this.NotifyPropertyChanged("broj Trka");
                }

            }
        }

        public int BrojPobeda
        {
            get { return brojPobeda; }
            set {
                if (value != brojPobeda)
                {
                    brojPobeda = value;
                    this.NotifyPropertyChanged("Broj Pobeda");
                }
            }
        }


        public string Prezime
        {
            get { return prezime; }
            set {
                if (value != prezime)
                {
                    prezime = value;
                    this.NotifyPropertyChanged("Prezime");
                }
            }
        }

        public string Ime
        {
            get { return ime; }
            set {
                if (value != ime)
                {
                    ime = value;
                    this.NotifyPropertyChanged("Ime");
                }
            }
        }

        public int ID
        {
            get { return id; }
            set {
                if (value != id)
                {
                    id = value;
                    this.NotifyPropertyChanged("Slika");
                }
            }
        }

        public string Tim
        {
            get { return tim; }
            set {
                if (value != tim)
                {
                    tim = value;
                    this.NotifyPropertyChanged("Tim");
                }
            }
        }

        public Vozac()
        {
            // Podrazumevani konstruktor bez parametara
        }

        public Vozac(int id,string ime,string prezime, string tim,string nac,int brS,int brT,int brP,string sl)
        {
            this.ime = ime;
            this.id = id;
            this.tim = tim;
            this.prezime = prezime;
            this.nacionalnost = nac;
            this.brojSasije = brS;
            this.brojPobeda = brP;
            this.brojTrka = brT;
            this.slika = sl;
        }
    }

}
