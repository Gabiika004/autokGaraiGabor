using MaterialDesignColors;
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
using System.Windows.Shapes;
using Xceed.Wpf.Toolkit;

namespace autokGaraiGabor
{
    /// <summary>
    /// Interaction logic for AutoView.xaml
    /// </summary>
    public partial class AutoView : Window
    {
        Database db = App.db;
        List<Auto> autok = App.autok;
        public event EventHandler AutoViewClosed;
        private bool markaChanged = false;
        private bool modellChanged = false;
        private bool gyartEvChanged = false;
        private bool forgalmiEvChanged = false;
        private bool vetelarEURChanged = false;
        private bool kmAllasChanged = false;
        private bool tomegChanged = false;
        private bool hengerurtartSliderChanged = false;
        private bool teljesitmenySliderChanged = false;

        public AutoView()
        {
            InitializeComponent();
            db = new Database();
            Closed += AutoView_Closed;
        }

        internal void SetAutoData(Auto auto)
        {
            marka.Text = auto.Marka;
            modell.Text = auto.Modell;
            gyartEv.Text = auto.GyartEv.ToString();
            forgalmiErv.SelectedDate = auto.ForgalmiErv;
            vetelarEUR.Text = auto.Vetelar.ToString();
            kmAllas.Text = auto.KmAllas.ToString();
            hengerurtartalomSlider.Value = auto.Hengerurtart;
            tomeg.Text = auto.Tomeg.ToString();
            teljesitmenySlider.Value = auto.Teljesitmeny;
            Title.Text = auto.Rendszam.ToString();
            markaChanged = false;
            modellChanged = false;
            forgalmiEvChanged = false;
            gyartEvChanged = false;
            vetelarEURChanged = false;
            kmAllasChanged = false;
            tomegChanged = false;
            hengerurtartSliderChanged = false;
            teljesitmenySliderChanged = false;

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            if (markaChanged || 
                modellChanged || 
                forgalmiEvChanged ||
                gyartEvChanged ||
                vetelarEURChanged ||
                kmAllasChanged ||
                tomegChanged ||
                hengerurtartSliderChanged ||
                teljesitmenySliderChanged )
            {
                if (string.IsNullOrEmpty(marka.Text) ||
              string.IsNullOrEmpty(modell.Text) ||
              string.IsNullOrEmpty(gyartEv.Text) ||
              string.IsNullOrEmpty(forgalmiErv.Text) ||
              string.IsNullOrEmpty(vetelarEUR.Text) ||
              string.IsNullOrEmpty(kmAllas.Text) ||
              string.IsNullOrEmpty(hengerurtart.Text) ||
              string.IsNullOrEmpty(tomeg.Text) ||
              string.IsNullOrEmpty(teljesitmeny.Text))
                {
                    Xceed.Wpf.Toolkit.MessageBox.Show("Kérlek töltsd ki az összes mezőt!", "Hiányzó adat", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
                else
                {
                    try
                    {
                        Auto updatedAuto = new Auto(
                            Title.Text,
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

                        db.UpdateAuto(updatedAuto);
                        markaChanged = false;
                        modellChanged = false;
                        forgalmiEvChanged = false;
                        gyartEvChanged = false;
                        vetelarEURChanged = false;
                        kmAllasChanged = false;
                        tomegChanged = false;
                        hengerurtartSliderChanged = false;
                        teljesitmenySliderChanged = false;
                        App.NavigateToPage("Home");
                        Close();

                    }
                    catch (FormatException)
                    {
                        System.Windows.MessageBox.Show("Hibás formátumú adatok! Kérlek ellenőrizd az értékeket.", "Hiba", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                System.Windows.MessageBox.Show("Nincs változás az adatokban.", "Nincs módosítás", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }
        private void VetelarEUR_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            SzamolAtvaltottErteket();
            vetelarEURChanged = true;
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
            
            switch (valuta)
            {
                case "HUF":
                    return 382.34; 
                case "USD":
                    return 1.12; 
                                
                default:
                    return 1.0; 
            }
        }
        private void ValutaComboBox_Loaded(object sender, RoutedEventArgs e)
        {       
            valutaComboBox.SelectedItem = valutaComboBox.Items[0];
        }

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            switch (textBox.Name)
            {
                case "marka":
                    markaChanged = true;
                    break;
                case "modell":
                    modellChanged = true;
                    break;                
            }
        }

        private void DatePicker_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            forgalmiEvChanged = true;
        }
        private void IntegerUpDown_ValueChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            IntegerUpDown integerUpDown = (IntegerUpDown)sender;
            switch (integerUpDown.Name)
            {
                case "gyartEv":
                    gyartEvChanged = true;
                    break;
                case "kmAllas":
                    kmAllasChanged = true;
                    break;
                case "tomeg":
                    tomegChanged = true;
                    break;
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = (Slider)sender;
            switch (slider.Name)
            {
                case "hengerurtartalomSlider":
                    hengerurtartSliderChanged = true;
                    break;
                case "teljesitmenySlider":
                    teljesitmenySliderChanged = true;
                    break;
            }

        }
        private void AutoView_Closed(object sender, EventArgs e)
        {
            
            App.NavigateToPage("Home");
        }
    }
}
