using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace autokGaraiGabor
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        internal static bool menuClosed = false;
        internal static List<Auto> autok = new List<Auto>();
        internal static Database db = null;

        public static Dictionary<string, Uri> PageUris { get; } = new Dictionary<string, Uri>
    {
        { "Home", new Uri("Pages/Home.xaml", UriKind.Relative) },
        { "New", new Uri("Pages/New.xaml", UriKind.Relative) },
        { "Delete", new Uri("Pages/Delete.xaml", UriKind.Relative) }
        // További oldalak hozzáadása
    };

        public static void NavigateToPage(string pageName)
        {
            if (PageUris.TryGetValue(pageName, out Uri uri))
            {
                ((MainWindow)Application.Current.MainWindow).Data.Navigate(uri);
            }
        }

    }

}
