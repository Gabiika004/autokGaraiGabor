using Microsoft.VisualBasic;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {

        Database db = App.db;
        List<Auto> autok = App.autok;
        bool menuClosed = App.menuClosed;
        public MainWindow()
        {
            InitializeComponent();
            db = new Database();
            autok = db.getAllAuto();
            HomeGrid.Visibility = Visibility.Collapsed;
            Data.Content = new Home();

        }

        //Gomb a menühöz
        private void button_Click_Menu(object sender, RoutedEventArgs e)
        {
            if(menuClosed)
            {
                Storyboard openMenu = (Storyboard)button.FindResource("OpenMenu");
                openMenu.Begin();
            }
            else
            {
                Storyboard closeMenu = (Storyboard)button.FindResource("CloseMenu");
                closeMenu.Begin();
            }
            
            menuClosed = !menuClosed;
        }

        //Gomb az autók betöltésére
        private void Button_Click_Autok(object sender, RoutedEventArgs e)
        {
            HomeGrid.Visibility = Visibility.Collapsed;
            App.NavigateToPage("Home");

        }

        //Gomb új autó felvétekéhez
        private void Button_Click_New(object sender, RoutedEventArgs e)
        {
            HomeGrid.Visibility = Visibility.Collapsed;
            App.NavigateToPage("New");

        }

        //Gomb az autók törléséhez
        private void Button_Click_Delete(object sender, RoutedEventArgs e)
        {
            HomeGrid.Visibility = Visibility.Collapsed;
            App.NavigateToPage("Delete");
        }

        //Gomb a szűrési feltételek kiválasztásához
        private void Button_Click_Exit(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

    }
}
