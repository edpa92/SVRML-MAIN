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
using System.Windows.Shapes;
using SVRML.Classes;

namespace SVRML
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        Vehiclecls veh = new Vehiclecls();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGridTab_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Application.Current.Resources["AdminID"] = 1;
            using (var data=new DataClasses1DataContext())
            {
                var vehicle = from v in data.Vehicles
                              where v.AdminId == (int)Application.Current.Resources["AdminID"]
                              select new { Vehicles= v.PlateNum};

                dataGridTab.ItemsSource = vehicle;
            }
        }

        private void buttonAddVehicle_Click(object sender, RoutedEventArgs e)
        {
            AddVehicleForm addv = new AddVehicleForm();
            addv.ShowDialog();
        }
    }
}
