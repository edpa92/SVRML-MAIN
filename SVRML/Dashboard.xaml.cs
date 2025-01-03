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
using System.ComponentModel;
using System.Threading;

namespace SVRML
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window, IDashboard
    {
        Vehiclecls veh = new Vehiclecls();
        RepairMaintenanceLogcls log = new RepairMaintenanceLogcls();
        private System.Windows.Forms.NotifyIcon _notifyIcon;
        List<string> cboItems = new List<string>();

        private BackgroundWorker _backgroundWorker;
        private CancellationTokenSource _cts;

        public Dashboard()
        {
            InitializeComponent();

            GetAllNotifs();
            StartBackgroundService();

                }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            GetAllVehicles();

            // Subscribe to the event *after* the window is loaded
           this.NotifyIconClicked += MainWindow_NotifyIconClicked;

        }

        private void buttonAddVehicle_Click(object sender, RoutedEventArgs e)
        {
            //AddRowWithButton($"Button {dynamicGrid.RowDefinitions.Count}");
            AddVehicleWindow addv = new AddVehicleWindow();
            addv.Closed += (s, args) => ReloadVehicle();
            addv.ShowDialog();
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrWhiteSpace(label2.Content.ToString()))
                {

                    if (comboBox1.SelectedIndex < 1)
                    {
                        MessageBox.Show("Select Car Brand", "Save Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (comboBox2.SelectedIndex < 1)
                    {
                        MessageBox.Show("Select Car Type", "Save Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    if (String.IsNullOrEmpty(txtPlateNo.Text.ToString()))
                    {
                        MessageBox.Show("Plate No. Required", "Save Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                    if (String.IsNullOrEmpty(txtSerialNo.Text.ToString()))
                    {
                        MessageBox.Show("Serial No. Required", "Save Edit", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    Vehicle v = new Vehicle();
                    v.Vehicle_ID = Convert.ToInt32(label2.Content.ToString());
                    v.AcquisitionDate = (DateTime)dtpAcq.SelectedDate;
                    v.LastLTORegistration = (DateTime)dtpLTO.SelectedDate;
                    v.AcquisitionCost = Convert.ToDecimal(txtCost.Text);
                    v.BrandModel = ((ComboBoxItem)comboBox1.Items.GetItemAt(comboBox1.SelectedIndex)).Content.ToString();
                    v.Type = ((ComboBoxItem)comboBox2.Items.GetItemAt(comboBox2.SelectedIndex)).Content.ToString();
                    v.PlateNum = txtPlateNo.Text;
                    v.SerialNum = txtSerialNo.Text;


                    if (veh.UpdateVehicle(v))
                    {
                        MessageBox.Show("Update details saved!", "Edit Vehicle", MessageBoxButton.OK, MessageBoxImage.None);
                        label.Content = v.PlateNum;
                        GetAllVehicles();
                    }

                }

            }
            catch (Exception)
            {
                MessageBox.Show("Invalid Format: Cost", "Save Edit", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void tabControl_SelectionChanged_1(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            AddMaintenanceLogWindow addm = new AddMaintenanceLogWindow(Convert.ToInt32(label2.Content.ToString()), 0);
            addm.Closed += (s, args) => GetAllRepairLogs(Convert.ToInt32(label2.Content.ToString()));
            addm.ShowDialog();
        }

        private void VehicleButton_Click(object sender, RoutedEventArgs e)
        {
            // Cast sender to Button to access its properties
            Button clickedButton = sender as Button;
            if (clickedButton != null)
            {

                for (int i = 0; i < dynamicGrid.Children.Count; i++)
                {
                    ((Button)dynamicGrid.Children[i]).FontWeight = FontWeights.Normal;
                    ((Button)dynamicGrid.Children[i]).Foreground = new SolidColorBrush(Colors.Black);
                    ((Button)dynamicGrid.Children[i]).Background = new SolidColorBrush(Colors.DarkGray);
                }
                clickedButton.FontWeight = FontWeights.Bold;
                clickedButton.Foreground = new SolidColorBrush(Colors.White);
                clickedButton.Background = new SolidColorBrush(Colors.DarkBlue);

                btnReport.IsEnabled = btnApplyFilter.IsEnabled = btnNewMaintenance.IsEnabled = btnNewSched.IsEnabled = buttonDeleteVehicle.IsEnabled = buttonSaveEdit.IsEnabled = true;

                GetThePlateNoOfSelectedVehicleButtonClick(clickedButton.Tag.ToString());
            }
        }

        private void dataGridMaintenanceLog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void dataGridMaintenanceLog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    // Get the clicked row item
            //    var selectedRow = dataGridMaintenanceLog.SelectedItem;
            //    if (selectedRow == null) return;

            //    // Example: Get value of a specific property (column)
            //    string columnName = "ID"; // Replace with the name of the property
            //    var cellValue = selectedRow.GetType().GetProperty(columnName)?.GetValue(selectedRow, null);
            //    AddMaintenanceLogWindow addm = new AddMaintenanceLogWindow(0, Convert.ToInt32(cellValue));
            //    addm.Closed += (s, args) => GetAllRepairLogs(Convert.ToInt32(label2.Content.ToString()));
            //    addm.ShowDialog();
            //}
            //catch (Exception)
            //{

            //    throw;
            //}


        }

        private void dataGridSchedMaintenance_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //try
            //{
            //    // Get the clicked row item
            //    var selectedRow = dataGridSchedMaintenance.SelectedItem;
            //    if (selectedRow == null) return;

            //    // Example: Get value of a specific property (column)
            //    string columnName = "ID"; // Replace with the name of the property
            //    var cellValue = selectedRow.GetType().GetProperty(columnName)?.GetValue(selectedRow, null);

            //    AddScheduleMaintenanceWindow sched = new AddScheduleMaintenanceWindow(0,(int)cellValue);
            //    sched.Closed += (s, args) => GetAllSched(Convert.ToInt32(label2.Content.ToString()));
            //    sched.ShowDialog();

            //}
            //catch (Exception)
            //{

            //    throw;
            //}
        }

        private void dataGridMaintenanceLog_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {

        }

        private void buttonDeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MessageBoxResult isdelete = MessageBox.Show("Deleting this Vehicle will also deleting all the data associated with it, such as Repair maintenance logs, Repair Schedule.", "Delete Vehicle", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (isdelete == MessageBoxResult.Yes)
                {

                    int vehicleid = Convert.ToInt32(label2.Content.ToString());
                    veh.DeleteVehicle(vehicleid);
                    MessageBox.Show("Vehicle deleted", "Delete Vehicle", MessageBoxButton.OK, MessageBoxImage.Information);
                    RefreshAll();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnNewSched_Click(object sender, RoutedEventArgs e)
        {
            AddScheduleMaintenanceWindow sched = new AddScheduleMaintenanceWindow(Convert.ToInt32(label2.Content.ToString()), 0);
            sched.Closed += (s, args) => GetAllSched(Convert.ToInt32(label2.Content.ToString()));
            sched.ShowDialog();
        }

        private void textBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SearchComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                PlaceholderText.Visibility = string.IsNullOrEmpty(SearchComboBox.Text) ? Visibility.Visible : Visibility.Collapsed;
                GetThePlateNoOfSelectedVehicleButtonClick(SearchComboBox.SelectedItem.ToString());
                btnReport.IsEnabled = btnApplyFilter.IsEnabled = btnNewMaintenance.IsEnabled = btnNewSched.IsEnabled = buttonDeleteVehicle.IsEnabled = buttonSaveEdit.IsEnabled = true;

            }
            catch (Exception)
            {
            }
        }

        private void SearchComboBox_TextInput(object sender, TextCompositionEventArgs e)
        {

            PlaceholderText.Visibility = string.IsNullOrEmpty(SearchComboBox.Text) ? Visibility.Visible : Visibility.Collapsed;

        }

        private void SearchComboBox_KeyDown(object sender, KeyEventArgs e)
        {

            PlaceholderText.Visibility = string.IsNullOrEmpty(SearchComboBox.Text) ? Visibility.Visible : Visibility.Collapsed;

        }

        private void SearchComboBox_KeyUp(object sender, KeyEventArgs e)
        {

            PlaceholderText.Visibility = string.IsNullOrEmpty(SearchComboBox.Text) ? Visibility.Visible : Visibility.Collapsed;
            string searchText = SearchComboBox.Text;
            if (searchText.Length > 1)
            {
                var filteredItems = cboItems
                    .Where(item => item.ToLower().Contains(searchText.ToLower()))
                    .ToList();

                SearchComboBox.ItemsSource = filteredItems;
                SearchComboBox.IsDropDownOpen = true;

                SearchComboBox.Text = searchText;
            }

        }

        private void btnApplyFilter_Click(object sender, RoutedEventArgs e)
        {
            if (!dtpFrom.SelectedDate.HasValue)
            {
                MessageBox.Show("Select atleast From Date, or including To Date for specific Date Range, you can also filter by Repair types.", "Rapair Maintenance Log Filter", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                using (var data = new DataClasses1DataContext())
                {
                    if (!string.IsNullOrEmpty(label2.Content.ToString()))
                    {
                        var vlogs = from l in data.RepairMaintenanceLogs
                                    join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                    where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && (l.Repair_Date >= dtpFrom.SelectedDate)
                                    select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };





                        if ((bool)cbBelt.IsChecked)
                        {
                            vlogs = from l in data.RepairMaintenanceLogs
                                    join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                    where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && (l.Repair_Date >= dtpFrom.SelectedDate)
                                    where (rt.Replace_Drivebelt == true)
                                    select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                        }

                        else if ((bool)cbBreak.IsChecked)
                        {
                            vlogs = from l in data.RepairMaintenanceLogs
                                    join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                    where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && (l.Repair_Date >= dtpFrom.SelectedDate)
                                    where (rt.Replace_Breakpads == true)
                                    select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                        }

                        else if ((bool)cbOil.IsChecked)
                        {
                            vlogs = from l in data.RepairMaintenanceLogs
                                    join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                    where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && (l.Repair_Date >= dtpFrom.SelectedDate)
                                    where (rt.Change_Oil == true)
                                    select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                        }
                        else if ((bool)cbTire.IsChecked)
                        {
                            vlogs = from l in data.RepairMaintenanceLogs
                                    join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                    where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && (l.Repair_Date >= dtpFrom.SelectedDate)
                                    where (rt.Replace_Tire == true)
                                    select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                        }
                        else if ((bool)cbAirfilter.IsChecked)
                        {
                            vlogs = from l in data.RepairMaintenanceLogs
                                    join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                    where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && (l.Repair_Date >= dtpFrom.SelectedDate)
                                    where (rt.Replace_Aircleaner == true)
                                    select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                        }


                        if (dtpTo.SelectedDate.HasValue)
                        {
                            vlogs = from l in data.RepairMaintenanceLogs
                                    join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                    where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && ((l.Repair_Date >= dtpFrom.SelectedDate) && (l.Repair_Date <= dtpTo.SelectedDate))
                                    select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                            if ((bool)cbBelt.IsChecked)
                            {
                                vlogs = from l in data.RepairMaintenanceLogs
                                        join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                        where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && ((l.Repair_Date >= dtpFrom.SelectedDate) && (l.Repair_Date <= dtpTo.SelectedDate))
                                        where (rt.Replace_Drivebelt == true)
                                        select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                            }

                            else if ((bool)cbBreak.IsChecked)
                            {
                                vlogs = from l in data.RepairMaintenanceLogs
                                        join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                        where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && ((l.Repair_Date >= dtpFrom.SelectedDate) && (l.Repair_Date <= dtpTo.SelectedDate))
                                        where (rt.Replace_Breakpads == true)
                                        select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                            }

                            else if ((bool)cbOil.IsChecked)
                            {
                                vlogs = from l in data.RepairMaintenanceLogs
                                        join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                        where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && ((l.Repair_Date >= dtpFrom.SelectedDate) && (l.Repair_Date <= dtpTo.SelectedDate))
                                        where (rt.Change_Oil == true)
                                        select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                            }
                            else if ((bool)cbTire.IsChecked)
                            {
                                vlogs = from l in data.RepairMaintenanceLogs
                                        join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                        where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && ((l.Repair_Date >= dtpFrom.SelectedDate) && (l.Repair_Date <= dtpTo.SelectedDate))
                                        where (rt.Replace_Tire == true)
                                        select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                            }
                            else if ((bool)cbAirfilter.IsChecked)
                            {
                                vlogs = from l in data.RepairMaintenanceLogs
                                        join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                                        where (l.Vehicle_ID == Convert.ToInt32(label2.Content.ToString())) && ((l.Repair_Date >= dtpFrom.SelectedDate) && (l.Repair_Date <= dtpTo.SelectedDate))
                                        where (rt.Replace_Aircleaner == true)
                                        select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                            }
                        }

                        //dataGridMaintenanceLog.ItemsSource = null;
                        //dataGridMaintenanceLog.ItemsSource = vlogs;
                        listView.ItemsSource = vlogs;
                        //dataGridMaintenanceLog.AutoGeneratedColumns += MyDataGridLog_AutoGeneratedColumns;
                    }
                }
            }
        }

        private void btnReport_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(label2.Content.ToString()))
            {
                ReportWindow rw = new ReportWindow(label2.Content.ToString());
                rw.ShowDialog();
            }
        }

        private void MenuExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {

        }

        private void listView_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Get the clicked row item
                var selectedRow = listView.SelectedItem;
                if (selectedRow == null) return;

                // Example: Get value of a specific property (column)
                string columnName = "ID"; // Replace with the name of the property
                var cellValue = selectedRow.GetType().GetProperty(columnName)?.GetValue(selectedRow, null);
                AddMaintenanceLogWindow addm = new AddMaintenanceLogWindow(0, Convert.ToInt32(cellValue));
                addm.Closed += (s, args) => GetAllRepairLogs(Convert.ToInt32(label2.Content.ToString()));
                addm.ShowDialog();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void listView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listViewSched_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void listViewSched_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                // Get the clicked row item
                var selectedRow = listViewSched.SelectedItem;
                if (selectedRow == null) return;

                // Example: Get value of a specific property (column)
                string columnName = "ID"; // Replace with the name of the property
                var cellValue = selectedRow.GetType().GetProperty(columnName)?.GetValue(selectedRow, null);

                AddScheduleMaintenanceWindow sched = new AddScheduleMaintenanceWindow(0, (int)cellValue);
                sched.Closed += (s, args) => GetAllSched(Convert.ToInt32(label2.Content.ToString()));
                sched.ShowDialog();

            }
            catch (Exception)
            {

                throw;
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            try
            {

                _notifyIcon.Dispose(); // Clean up
                base.OnClosed(e);

            }
            catch (Exception)
            {
            }
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            _cts.Cancel();
            _backgroundWorker.CancelAsync();
        }


        private void MenuItemSettigns_Click(object sender, RoutedEventArgs e)
        {
            SettingWindow sw = new SettingWindow();
            sw.ShowDialog();
        }


        private void NotifyIcon_Click(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //if (e.Button == System.Windows.Forms.MouseButtons.Left)
            //{
                // Handle left click (e.g., restore the window)
                this.WindowState = WindowState.Normal;
                this.Show();
                this.Activate(); // Bring to front

                // Switch to the desired tab
               tabControl.SelectedIndex = 3; // Index of the tab you want to open (zero-based)

            //    OnNotifyIconClicked(EventArgs.Empty);

            //}
            //else if (e.Button == System.Windows.Forms.MouseButtons.Right)
            //{
            //    //Handle Right click (Show Context Menu is done automatically if set)
            //    Console.WriteLine("Right Click");
            //}
        }
        public event EventHandler NotifyIconClicked;
        protected virtual void OnNotifyIconClicked(EventArgs e)
        {
            NotifyIconClicked?.Invoke(this, e);
        }

        private void NotifyIcon_DoubleClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            this.WindowState = WindowState.Normal;
            this.Show();
            this.Activate();
            OnNotifyIconClicked(EventArgs.Empty);
        }

        private void MainWindow_NotifyIconClicked(object sender, EventArgs e)
        {
            // Switch to the desired tab
            tabControl.SelectedIndex = 3; // Index of the tab you want to open (zero-based)
        }


        //Define Methods -------------------------------------------------------

        private void AddRowWithButton(string buttonText)
        {
            var stackPanel = new StackPanel { Orientation = Orientation.Horizontal };
            // Add the text
            var text = new TextBlock
            {
                Text = buttonText,
                VerticalAlignment = VerticalAlignment.Center,
                Margin = new Thickness(5, 0, 0, 0)
            };
            // Create an image for the button
            var icon = new Image
            {
                Source = new BitmapImage(new Uri("pack://application:,,,/icons/car-36.png")),
                Width = 24,
                Height = 24,
                Margin = new Thickness(0, 0, 0, 5),
            };
            stackPanel.Children.Add(icon);
            stackPanel.Children.Add(text);


            // Add a new row to the grid
            dynamicGrid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            // Create a new button
            Button newButton = new Button
            {
                Tag = buttonText,
                Margin = new Thickness(5),
                FontSize = 16,
                Padding = new Thickness(0, 50, 0, 50),
            };


            newButton.Content = stackPanel;
            newButton.Style = (Style)FindResource("RoundedButton2");
            newButton.Padding = new Thickness(20, 20, 20, 20);
            newButton.Background = new SolidColorBrush(Colors.DarkGray);

            // Attach the Click event handler
            newButton.Click += VehicleButton_Click;

            // Place the button in the newly added row
            int newRowIndex = dynamicGrid.RowDefinitions.Count - 1; // Get the index of the new row
            Grid.SetRow(newButton, newRowIndex);

            // Add the button to the grid
            dynamicGrid.Children.Add(newButton);
        }

        public void GetAllVehicles()
        {
            Application.Current.Resources["AdminID"] = 1;
            using (var data = new DataClasses1DataContext())
            {
                var vehicle = from v in data.Vehicles
                                  // where v.AdminId == (int)Application.Current.Resources["AdminID"]
                              select new { Vehicles = v.PlateNum };
                dynamicGrid.Children.Clear();
                for (int i = 0; i < vehicle.Count(); i++)
                {
                    string item = vehicle.ToList().ElementAt(i).Vehicles;
                    cboItems.Add(item);
                    AddRowWithButton(item);
                }

                //SearchComboBox.ItemsSource = cboItems;
            }
        }

        public void GetAllNotifs()
        {
            using (var data = new DataClasses1DataContext())
            {
                var notis = from n in data.NotificationTbls
                            select new { n.DateNotified, n.Details, n.SchedID };
                listViewNoti.ItemsSource = notis;
            }
        }

        public void GetAllRepairLogs(int vehicleid)
        {
            using (var data = new DataClasses1DataContext())
            {
                var vlogs = from l in data.RepairMaintenanceLogs
                            join rt in data.RepairTypes on l.RepairLog_ID equals rt.RepairLogID
                            where l.Vehicle_ID == vehicleid
                            select new { ID = l.RepairLog_ID, RepairDate = l.Repair_Date.ToLongDateString(), rt.Cost, Milage = l.Milage + "KM", RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_Aircleaner) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") };

                //dataGridMaintenanceLog.ItemsSource = vlogs;
                //dataGridMaintenanceLog.AutoGeneratedColumns += MyDataGridLog_AutoGeneratedColumns;
                //listView.ItemsSource = vlogs;
                //if (dataGridMaintenanceLog.Columns.Count > 0)
                //{
                //    dataGridMaintenanceLog.Columns[0].Visibility = Visibility.Collapsed;
                //}

                //listView.AutoGeneratedColumns += MyDataGridLog_AutoGeneratedColumns;
                listView.ItemsSource = vlogs;

            }
        }

        public void GetAllSched(int vehicleid)
        {
            using (var data = new DataClasses1DataContext())
            {
                var sched = from s in data.SchedMaintenances
                            where s.Vehicle_ID == vehicleid
                            select new { ID = s.SchedMain_ID, Details = s.Remarks, ScheduleIn = Helper.CalculateDifference(DateTime.Now, s.Sched_Date), ScheduleDate = s.Sched_Date.ToShortDateString() };// MainInterval_Days=s.MainInterval_Days+"Days", NextSchedule=s.NextSched_Date.Value.ToLongDateString(), s.Remarks };


                //dataGridSchedMaintenance.ItemsSource = sched;
                //dataGridSchedMaintenance.AutoGeneratedColumns += MyDataGridSched_AutoGeneratedColumns;

                //if (dataGridSchedMaintenance.Columns.Count > 0)
                //{
                //    dataGridSchedMaintenance.Columns[0].Visibility = Visibility.Collapsed;
                //}

                listViewSched.ItemsSource = sched;
            }
        }

        public void ReloadVehicle()
        {
            GetAllVehicles();
        }

        public void GetThePlateNoOfSelectedVehicleButtonClick(string buttontext)
        {

            Vehicle v = veh.GetVehicle(buttontext.ToString());
            if (v != null)
            {
                label.Content = $"{v.PlateNum} {v.BrandModel}";
                txtPlateNo.Text = v.PlateNum;
                txtCost.Text = v.AcquisitionCost.ToString();
                txtSerialNo.Text = v.SerialNum;
                label2.Content = v.Vehicle_ID.ToString();

                dtpAcq.SelectedDate = v.AcquisitionDate;
                dtpLTO.SelectedDate = v.LastLTORegistration;

                foreach (ComboBoxItem item in comboBox1.Items)
                {
                    if (item.Content.ToString() == v.BrandModel)
                    {
                        comboBox1.SelectedItem = item;
                        break; // Stop once we find the match
                    }
                }

                foreach (ComboBoxItem itemc2 in comboBox2.Items)
                {
                    if (itemc2.Content.ToString() == v.Type)
                    {
                        comboBox2.SelectedItem = itemc2;
                        break; // Stop once we find the match
                    }
                }

                GetAllRepairLogs(v.Vehicle_ID);
                GetAllSched(v.Vehicle_ID);
            }
            else
            {
                MessageBox.Show("Vehicle not exist!", "Vehicle", MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
        }

        private void RefreshAll()
        {
            GetAllVehicles();
            GetAllNotifs();
            label2.Content = string.Empty;
            label.Content = "Vehicle";
            comboBox1.SelectedItem = null;
            comboBox2.SelectedItem = null;
            txtPlateNo.Text = txtCost.Text = txtSerialNo.Text = "";
            btnReport.IsEnabled = btnApplyFilter.IsEnabled = btnNewMaintenance.IsEnabled = btnNewSched.IsEnabled = buttonDeleteVehicle.IsEnabled = buttonSaveEdit.IsEnabled = true;
            listView.ItemsSource = null;
        }




        // BackgroundWorker Methods------------------------------------------
        private void StartBackgroundService()
        {
            _cts = new CancellationTokenSource(); // For cancellation
            _backgroundWorker = new BackgroundWorker();
            _backgroundWorker.WorkerSupportsCancellation = true;
            _backgroundWorker.DoWork += BackgroundWorker_DoWork;
            _backgroundWorker.RunWorkerAsync();
        }
        
        private async void BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                while (!_cts.IsCancellationRequested) // Check for cancellation
                {
                    SchedMaintenanceVehicle sc = CheckCondition();
                    if (sc != null)
                    {
                        Console.WriteLine("COndition true, need to fire Noti");
                        // Run the task on the UI thread using Dispatcher.InvokeAsync
                        await Dispatcher.InvokeAsync(async () =>
                        {
                            await ExecuteTaskAsync(sc); // Your task here
                        });

                        //Optional: Add a delay after the task is executed to avoid continuous execution
                        await Task.Delay(TimeSpan.FromSeconds(5), _cts.Token); // Check for cancellation during delay
                    }
                    else
                    {
                        Console.WriteLine("COndition False, No need to fire Noti");
                        // Wait for a short period before checking the condition again
                        await Task.Delay(TimeSpan.FromSeconds(5), _cts.Token); // Check for cancellation during delay
                    }
                }
            }
            catch (OperationCanceledException)
            {
                // Handle cancellation gracefully
                Console.WriteLine("Background worker cancelled.");
            }
            catch (Exception ex)
            {
                // Handle other exceptions appropriately (e.g., logging)
                Dispatcher.Invoke(() => MessageBox.Show($"Error in background worker: {ex.Message}"));
            }
        }

        private SchedMaintenanceVehicle CheckCondition()
        {
            // Implement your condition checking logic here.
            // Example: Check a file, a database value, etc.
            // This example checks if the current second is divisible by 5.
            SchedMaintenanceVehicle scv = null;
            try
            {


                using (var data = new DataClasses1DataContext())
                {
                    int days = (int)data.Administrators.FirstOrDefault(a => a.AdminId == (int)Application.Current.Resources["AdminID"]).NoDaysNotifyBeforeSched;
                    var scheds = from s in data.SchedMaintenances
                                 select s;
                    foreach (SchedMaintenance item in scheds)
                    {
                        // Calculate the difference using TimeSpan
                        TimeSpan difference = item.Sched_Date - DateTime.Now;

                        // Get the number of days
                        int daysRemaining = difference.Days;
                        if (daysRemaining <= days && daysRemaining > 0)
                        {
                            var noti = data.NotificationTbls.Where(n => n.SchedID == item.SchedMain_ID).ToList();
                            if (noti != null && noti.Count == 0)
                            {
                                scv = new SchedMaintenanceVehicle();
                                Vehicle ve = data.Vehicles.FirstOrDefault(v => v.Vehicle_ID == item.Vehicle_ID);
                                scv.scheddate = item.Sched_Date.ToLongDateString();
                                scv.scheddetails = item.Remarks;
                                scv.schedid = item.SchedMain_ID;
                                scv.vehid = item.Vehicle_ID;
                                scv.vehplate = ve.PlateNum;
                                scv.vehmodelbrand = ve.BrandModel;
                                return scv;
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                return null;

                throw;
            }

            return null;

        }

        private async Task ExecuteTaskAsync(SchedMaintenanceVehicle schedv)
        {
            string details = $"Schedule Maitenance on {schedv.scheddate} with Remarks/Details: {schedv.scheddetails} for {schedv.vehplate} {schedv.vehmodelbrand}, Please prepare proccurement.";
            _notifyIcon = new System.Windows.Forms.NotifyIcon
            {
                Icon = new System.Drawing.Icon(System.Drawing.SystemIcons.Information, 40, 40),
                Visible = true,
                BalloonTipTitle = "SVRML Notification",
                BalloonTipText = details
            };

            // Add the click event handler
            _notifyIcon.MouseClick += NotifyIcon_Click;
            

            _notifyIcon.ShowBalloonTip(10000); // Show balloon for 3 seconds
            Console.WriteLine("Noti Fired");
            // This is the task that will be executed when the condition is met.
            // It MUST be awaited if it's an async method to prevent blocking.

            // Example: Update a UI element
            //  TextBlockStatus.Text = $"Task executed at: {DateTime.Now}";

           

            // Example: Call an external API
            // ... your API call code ... await ...

            using (var data = new DataClasses1DataContext())
            {
                NotificationTbl not = new NotificationTbl();
                not.DateNotified = DateTime.Now;
                not.Details = details;
                not.SchedID = schedv.schedid;
                data.NotificationTbls.InsertOnSubmit(not);
                data.SubmitChanges();
            }
            // Example: Perform a long-running operation (using Task.Delay to simulate)
            await Task.Delay(2000);
            //TextBlockStatus.Text += " - Task Completed";
            GetAllNotifs();
            tabControl.SelectedIndex = 3;
        }

        private void MenuItemCredentials_Click(object sender, RoutedEventArgs e)
        {
            AccountSettings acc = new AccountSettings();
            acc.ShowDialog();
        }

    }

    public class SchedMaintenanceVehicle{
        public int schedid { set; get; }
        public string scheddetails { set; get; }
        public string scheddate { set; get; }
        public int vehid { set; get; }
        public string vehplate { set; get; }
        public string vehmodelbrand { set; get; }
    }
}
