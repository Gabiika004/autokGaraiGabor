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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace autokGaraiGabor
{
    /// <summary>
    /// Interaction logic for Home.xaml
    /// </summary>
    public partial class Home : Page
    {

        Database db = App.db;
        List<Auto> autok = App.autok;
        private List<CheckBox> checkBoxes = new List<CheckBox>();

        public Home()
        {
            InitializeComponent();
            db = new Database();
            autok = db.getAllAuto();
            LoadCheckBoxes();
            updateAutokList();
        }


        // Függvény a szűréshez --> létrehozunk a márkák alapján CheckBoxokat
        private void LoadCheckBoxes()
        {
            foreach (string marka in autok.Select(x => x.Marka).Distinct())
            {
                CheckBox cb = new CheckBox();
                cb.Content = marka;
                cb.IsChecked = true;
                cb.Checked += CheckBox_Checked;
                cb.Unchecked += CheckBox_Checked;
                selector.Children.Add(cb);

                checkBoxes.Add(cb);
            }
        }

        private void updateAutokList()
        {
            if (listBoxDatas.SelectedIndex < 0)
            {
                listBoxDatas.Items.Clear();

                // Szűrés az ellenőrzött CheckBox-ok alapján
                var selectedBrands = selector.Children
                    .OfType<CheckBox>()
                    .Where(cb => cb.IsChecked == true)
                    .Select(cb => cb.Content.ToString());

                // Újra betöltjük az autók listát a szűrt márkák alapján
                var filteredAutok = autok.Where(auto => selectedBrands.Contains(auto.Marka)).ToList();

                foreach (var auto in filteredAutok)
                {
                    listBoxDatas.Items.Add(auto);
                }
            }
        }

        private void SelectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var checkBox in checkBoxes)
            {
                checkBox.IsChecked = true;
            }
        }

        private void DeselectAll_Click(object sender, RoutedEventArgs e)
        {
            foreach (var checkBox in checkBoxes)
            {
                checkBox.IsChecked = false;
            }
        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            updateAutokList();
        }


        private void listBoxDatas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listBoxDatas.SelectedItem != null)
            {
                var selectedAuto = (Auto)listBoxDatas.SelectedItem;
                OpenAutoView(selectedAuto);
            }
        }

        private void OpenAutoView(Auto auto)
        {
            // Letiltjuk az összes ablakot, kivéve az új ablakot
            foreach (Window window in Application.Current.Windows)
            {
                if (window != null && window.GetType() != typeof(AutoView))
                {
                    window.IsEnabled = false;
                }
            }

            AutoView autoView = new AutoView();
            autoView.SetAutoData(auto);

            // Az eseménykezelő hozzáadása az új ablak bezárásához
            autoView.Closed += (sender, e) =>
            {
                // Engedélyezzük az összes ablakot, amiket korábban letiltottunk
                foreach (Window window in Application.Current.Windows)
                {
                    if (window != null)
                    {
                        window.IsEnabled = true;
                    }
                }
            };

            autoView.Show();
        }
    }
}
