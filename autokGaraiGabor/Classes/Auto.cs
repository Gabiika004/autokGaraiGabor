using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace autokGaraiGabor
{
    internal class Auto : INotifyPropertyChanged
    {
        private string rendszam;
        private string marka;
        private string modell;
        private Int16 gyartEv;
        private DateTime forgalmiErv;
        private decimal vetelar;
        private int kmAllas;
        private int hengerurtart;
        private int tomeg;
        private int teljesitmeny;

        private bool isSelected;

        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                if (isSelected != value)
                {
                    isSelected = value;
                    OnPropertyChanged(nameof(IsSelected));
                }
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public string Rendszam { get => rendszam; set => rendszam = value; }
        public string Marka { get => marka; set => marka = value; }
        public string Modell { get => modell; set => modell = value; }
        public Int16 GyartEv { get => gyartEv; set => gyartEv = value; }
        public DateTime ForgalmiErv { get => forgalmiErv; set => forgalmiErv = value; }
        public decimal Vetelar { get => vetelar; set => vetelar = value; }
        public int KmAllas { get => kmAllas; set => kmAllas = value; }
        public int Hengerurtart { get => hengerurtart; set => hengerurtart = value; }
        public int Tomeg { get => tomeg; set => tomeg = value; }
        public int Teljesitmeny { get => teljesitmeny; set => teljesitmeny = value; }

        public Auto(string rendszam, string marka, string modell, Int16 gyartEv, DateTime forgalmiErv, decimal vetelar, int kmAllas, int hengerurtart, int tomeg, int teljesitmeny)
        {
            Rendszam = rendszam;
            Marka = marka;
            Modell = modell;
            GyartEv = gyartEv;
            ForgalmiErv = forgalmiErv;
            Vetelar = vetelar;
            KmAllas = kmAllas;
            Hengerurtart = hengerurtart;
            Tomeg = tomeg;
            Teljesitmeny = teljesitmeny;
        }

        public override string? ToString()
        {
            return $"{this.marka} {this.modell} {this.gyartEv} {this.rendszam}";
        }
    }


}
