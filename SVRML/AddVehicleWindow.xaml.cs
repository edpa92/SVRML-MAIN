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
    /// Interaction logic for AddVehicleWindow.xaml
    /// </summary>
    public partial class AddVehicleWindow : Window
    {
        Vehiclecls vehobj = new Vehiclecls();
        public AddVehicleWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBox1.Text.ToString() == "")
                {
                    MessageBox.Show("Plate No. is required.");
                    return;

                }

                if (textBox2.Text.ToString() == "")
                {
                    MessageBox.Show("Serial No. is required.");
                    return;

                }
                if (textBox4.Text.ToString() == "")
                {
                    MessageBox.Show("Cost is required.");
                    return;

                }
                if (comboBox1.SelectedIndex == -1)
                {
                    MessageBox.Show("Select a brand");

                    return;
                }
                if (comboBox2.SelectedIndex == -1)
                {
                    MessageBox.Show("Select Vehicle type.");
                    return;

                }
                if (!dateTimePicker1.SelectedDate.HasValue)
                {
                    MessageBox.Show("Select Date");
                    return;

                }
                

                double cost = Convert.ToDouble(textBox4.Text);
                Vehicle veh = new Vehicle();
                veh.AcquisitionCost = (decimal)cost;
                veh.AcquisitionDate = (DateTime)dateTimePicker1.SelectedDate;
                veh.AdminId = (int)Application.Current.Resources["AdminID"];
                veh.BrandModel = ((ComboBoxItem)comboBox1.Items.GetItemAt(comboBox1.SelectedIndex)).Content.ToString();
                veh.PlateNum = textBox1.Text;
                veh.SerialNum = textBox2.Text;
                veh.Type = ((ComboBoxItem)comboBox2.Items.GetItemAt(comboBox2.SelectedIndex)).Content.ToString();
                veh.LastLTORegistration= (DateTime)dateTimePicker2.SelectedDate;

                if (vehobj.AddVehicle(veh))
                {
                    MessageBox.Show("Vehicle saved.", "Save", MessageBoxButton.OK, MessageBoxImage.None);
                    IDashboard dashi = new Dashboard();
                    dashi.ReloadVehicle();
                    this.Close();
                }



            }
            catch (Exception)
            {

                MessageBox.Show("Invalid input.");

                return;
            }
        }

        private void textbox1_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
