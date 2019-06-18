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

namespace _01_RestauranteLasBrasas
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonAbrirMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCerrarMenu.Visibility = Visibility.Visible;
            ButtonAbrirMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCerrarMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCerrarMenu.Visibility = Visibility.Collapsed;
            ButtonAbrirMenu.Visibility = Visibility.Visible;
        }
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UserControl usc = null;
            GridMain.Children.Clear();

            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemEmpleados":
                    usc = new UserControlEmpleados();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemFacturacion":
                    usc = new UserControlFacturacion();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemHistorial":
                    usc = new UserControlHistorial();
                    GridMain.Children.Add(usc);
                    break;
                case "ItemInventario":
                    usc = new UserControlInventario();
                    GridMain.Children.Add(usc);
                    break;
                default:
                    break;
            }
        }
        private void BtnSalir_Click(object sender, RoutedEventArgs e)
        {
            App.Current.Shutdown();
        }
    }
}

