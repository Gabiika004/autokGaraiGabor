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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace autokGaraiGabor
{
    /// <summary>
    /// Interaction logic for Delete.xaml
    /// </summary>
    public partial class Delete : Page
    {
        Database db = App.db;
        List<Auto> autok = App.autok;
        private List<CheckBox> checkBoxes = new List<CheckBox>();
        public Delete()
        {
            InitializeComponent();
            db = new Database();
            autok = db.getAllAuto();
            LoadByBrand();
            updateAutokList();
        }

        // Függvény a szűréshez --> létrehozunk a márkák alapján CheckBoxokat
        private void LoadByBrand()
        {
            foreach(string marka in autok.Select(x => x.Marka).Distinct())
            {
                CheckBox cb = new CheckBox();
                cb.Content = marka;
                cb.IsChecked = true;
                cb.Checked += CheckBox_Checked;
                cb.Unchecked += CheckBox_Checked; 
                selector.Children.Add(cb);

                // Hozzáadás a checkBoxes listához
                checkBoxes.Add(cb);
            }
        }
        private void updateAutokList()
        {
            if (listViewDatas.SelectedIndex < 0)
            {
                listViewDatas.Items.Clear();

                // Szűrés az ellenőrzött CheckBox-ok alapján
                var selectedBrands = selector.Children
                    .OfType<CheckBox>()
                    .Where(cb => cb.IsChecked == true)
                    .Select(cb => cb.Content.ToString());

                // Újra betöltjük az autók listát a szűrt márkák alapján
                var filteredAutok = autok.Where(auto => selectedBrands.Contains(auto.Marka)).ToList();

                foreach (var auto in filteredAutok)
                {
                    listViewDatas.Items.Add(auto);

                    
                }
            }
        }

        private void listBoxDatas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (Auto selectedItem in e.AddedItems)
            {
                selectedItem.IsSelected = true;
            }

            foreach (Auto removedItem in e.RemovedItems)
            {
                removedItem.IsSelected = false;
            }
        }

        private void SelectAllData_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listViewDatas.Items)
            {
                if (item is Auto auto)
                {
                    auto.IsSelected = true;
                }
            }
        }

        private void DeselectAllData_Click(object sender, RoutedEventArgs e)
        {
            foreach (var item in listViewDatas.Items)
            {
                if (item is Auto auto)
                {
                    auto.IsSelected = false;
                }
            }
        }

        private void SelectAllCheckBox_Click(object sender, RoutedEventArgs e)
        {
            foreach (var checkBox in checkBoxes)
            {
                checkBox.IsChecked = true;
            }
        }

        private void DeselectAllCheckBox_Click(object sender, RoutedEventArgs e)
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

        private void DeleteSelectedData_Click(object sender, RoutedEventArgs e)
        {
            var selectedAutos = autok.Where(auto => auto.IsSelected).ToList();

            foreach (var auto in selectedAutos)
            {
               
                db.DeleteSelectedAuto(auto);

  
                autok.Remove(auto);
            }

            updateAutokList();

        }

        private void listBoxDatas_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (listViewDatas.SelectedItem != null)
            {
                var selectedAuto = (Auto)listViewDatas.SelectedItem;
                OpenAutoView(selectedAuto);
            }
        }

        private void OpenAutoView(Auto auto)
        {
            AutoView autoView = new AutoView();
            autoView.SetAutoData(auto);
            autoView.Show();
        }
    }
}
