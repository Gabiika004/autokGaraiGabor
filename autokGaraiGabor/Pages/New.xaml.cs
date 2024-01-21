using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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

namespace autokGaraiGabor
{
    /// <summary>
    /// Interaction logic for New.xaml
    /// </summary>
    public partial class New : Page
    {
        Database db = App.db;
        List<Auto> autok = App.autok;
        public New()
        {
            InitializeComponent();
            db = new Database();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (string.IsNullOrEmpty(rendszam.Text) ||
                string.IsNullOrEmpty(marka.Text) ||
                string.IsNullOrEmpty(modell.Text) ||
                string.IsNullOrEmpty(gyartEv.Text) ||
                string.IsNullOrEmpty(forgalmiErv.Text) ||
                string.IsNullOrEmpty(vetelarEUR.Text) ||
                string.IsNullOrEmpty(kmAllas.Text) ||
                string.IsNullOrEmpty(hengerurtart.Text) ||
                string.IsNullOrEmpty(tomeg.Text) ||
                string.IsNullOrEmpty(teljesitmeny.Text))
            {
                MessageBox.Show("Kérlek töltsd ki az összes mezőt!", "Hiányzó adat", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                try
                {
                    // Rendszám ellenőrzése reguláris kifejezéssel
                    string rendszamPattern = @"^[A-Z]{3}-\d{3}$";
                    Regex regex = new Regex(rendszamPattern);
                    if (regex.IsMatch(rendszam.Text))
                    {
                        Auto newAuto = new Auto(
                            rendszam.Text,
                            marka.Text,
                            modell.Text,
                            Convert.ToInt16(gyartEv.Text),
                            Convert.ToDateTime(forgalmiErv.Text),
                            Convert.ToDecimal(vetelarEUR.Text),
                            Convert.ToInt32(kmAllas.Text),
                            Convert.ToInt32(hengerurtart.Text),
                            Convert.ToInt32(tomeg.Text),
                            Convert.ToInt32(teljesitmeny.Text)
                        );

                        db.addNewAuto(newAuto);
                        MessageBox.Show($"A(z) {newAuto.Rendszam} rendszámú autó sikeresen rögzítve!");
                        if (db.IsAutoExists(newAuto.Rendszam))
                        {
                            rendszam.Text = string.Empty;
                            marka.Text = string.Empty;
                            modell.Text = string.Empty;
                            forgalmiErv.Text = string.Empty;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Hibás rendszám formátum! Kérlek ellenőrizd az értéket. A helyes formátum: ABC-123", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                catch (FormatException)
                {
                    MessageBox.Show("Hibás formátumú adatok! Kérlek ellenőrizd az értékeket.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

        }

        private void VetelarEUR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SzamolAtvaltottErteket();
        }

        private void ValutaComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SzamolAtvaltottErteket();
        }

        private void SzamolAtvaltottErteket()
        {
            if (double.TryParse(vetelarEUR.Text, out double eurValue))
            {
                if (valutaComboBox.SelectedItem != null)
                {
                    string selectedValuta = ((ComboBoxItem)valutaComboBox.SelectedItem).Content.ToString();
                    double atvaltasFaktor = GetAtvaltasFaktor(selectedValuta);

                    double atvaltottErtek = eurValue * atvaltasFaktor;
                    atvaltottErtekTextBlock.Text = atvaltottErtek.ToString("F2");
                }
            }
        }

        private double GetAtvaltasFaktor(string valuta)
        {
            // Az átváltási faktorokat itt definiálhatod, például:
            switch (valuta)
            {
                case "HUF":
                    return 382.34; // Az átváltási faktor HUF-hoz
                case "USD":
                    return 1.12; // Az átváltási faktor USD-hez
                                 // További valuták és azok átváltási faktorai
                default:
                    return 1.0; // Alapértelmezett érték (nem történik átváltás)
            }
        }
        private void ValutaComboBox_Loaded(object sender, RoutedEventArgs e)
        {
            // Beállítjuk az alapértelmezett értéket "HUF"-ra
            valutaComboBox.SelectedItem = valutaComboBox.Items[0];
        }

    }
}
