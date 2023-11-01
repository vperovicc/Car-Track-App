using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IgorKrece
{
    public class Proizvodjac : INotifyPropertyChanged
    {
        private int id;
        private string naziv;
        private string sediste;
        private string logo;



        public Proizvodjac(int idd,string nn,string ss,string ll)
        {
            id=idd; naziv = nn; sediste = ss; logo = ll;   
        }
        public Proizvodjac()
        {

        }

        public int ID
        {
            get { return id; }
            set
            {
                if (id != value)
                {
                    id = value;
                    NotifyPropertyChanged(nameof(ID));
                }
            }
        }

        public string Naziv
        {
            get { return naziv; }
            set
            {
                if (naziv != value)
                {
                    naziv = value;
                    NotifyPropertyChanged(nameof(Naziv));
                }
            }
        }

        public string Sediste
        {
            get { return sediste; }
            set
            {
                if (sediste != value)
                {
                    sediste = value;
                    NotifyPropertyChanged(nameof(Sediste));
                }
            }
        }

        public string Logo
        {
            get { return logo; }
            set
            {
                if (logo != value)
                {
                    logo = value;
                    NotifyPropertyChanged(nameof(Logo));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void NotifyPropertyChanged(string v)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
    }
}
