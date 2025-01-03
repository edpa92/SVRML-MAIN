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
    /// Interaction logic for AddScheduleMaintenanceWindow.xaml
    /// </summary>
    public partial class AddScheduleMaintenanceWindow : Window
    {
        SchedMaintenancecls sched = new SchedMaintenancecls();
        private int vehicleid = 0;
        private int schedid = 0;
        private bool isEdit = false;
        public AddScheduleMaintenanceWindow(int vehicleid, int schedid)
        {
            this.vehicleid = vehicleid;
            this.schedid = schedid;
            if (vehicleid==0&&schedid>0)
            {
                isEdit = true;
            }
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!dateTimePickerSched.SelectedDate.HasValue)
                {
                    MessageBox.Show("Schedule Date Required.", "Add Maintenance Schedule", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }
                if (string.IsNullOrEmpty(textBoxInterval.Text))
                {
                    MessageBox.Show("Interval days required.", "Add Maintenance Schedule", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                if (string.IsNullOrEmpty(textBoxRemark.Text))
                {
                    MessageBox.Show("Remarks are required.", "Add Maintenance Schedule", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

                int intervaldays= Convert.ToInt32(textBoxInterval.Text.ToString());

                SchedMaintenance sc = new SchedMaintenance();
                sc.MainInterval_Days = intervaldays;
                sc.NextSched_Date = dateTimePickerNextSched.SelectedDate;
                sc.Remarks = textBoxRemark.Text;
                sc.Sched_Date = (DateTime)dateTimePickerSched.SelectedDate;
                if (!isEdit)
                {
                    sc.Vehicle_ID = this.vehicleid;
                    sc.Admin_ID = (int)Application.Current.Resources["AdminID"];
                    sched.CreateSchedMaintenance(sc);
                }
                else
                {
                    sc.SchedMain_ID = schedid;
                    sched.UpdateSched(sc);
                }

                MessageBox.Show("Maintenance Schedule saved.",(isEdit?"Edit":"Add")+" Maintenance Schedule",MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();

            }
            catch (Exception)
            {

                MessageBox.Show("You have entered invalid format- Interval days.", "Add Maintenance Schedule", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                
            }
        }

        private void textBoxInterval_TextChanged(object sender, TextChangedEventArgs e)
        {
            ComputeNextSxhedDate();
        }

        private void ComputeNextSxhedDate()
        {
            try
            {
                int days = Convert.ToInt32(textBoxInterval.Text);
                DateTime schedDate = (DateTime)dateTimePickerSched.SelectedDate;
                DateTime next = schedDate.AddDays(days);
                dateTimePickerNextSched.SelectedDate = next;
            }
            catch (Exception)
            {

            }
        }

        private void dateTimePickerSched_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            ComputeNextSxhedDate();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isEdit)
                {
                    AddSchedWindow.Title = "Update Maintenance Schedule";
                    using (var data=new DataClasses1DataContext())
                    {
                        var sched = data.SchedMaintenances.SingleOrDefault(s=>s.SchedMain_ID==this.schedid);
                        if (sched!=null)
                        {
                            dateTimePickerSched.SelectedDate = sched.Sched_Date;
                            textBoxInterval.Text = sched.MainInterval_Days.ToString();
                            textBoxRemark.Text = sched.Remarks;

                            btnSaveSched.Content = "Save Edit";
                            btnDeleteSched.Visibility = Visibility.Visible;

                        }
                    }
                }
                else
                {
                    AddSchedWindow.Title = "Add Maintenance Schedule";
                    btnSaveSched.Content = "Save";
                    btnDeleteSched.Visibility = Visibility.Hidden;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private void btnDeleteSched_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (this.schedid>0)
                {
                    MessageBoxResult delResult = MessageBox.Show("Are you sure you want to delete this schedule? this cannot be undone.","Delete Schedule",MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (delResult==MessageBoxResult.Yes)
                    {
                        if (sched.DeleteSched(this.schedid))
                        {
                            MessageBox.Show("Schedule Deleted.","Delete",MessageBoxButton.OK,MessageBoxImage.Information);
                            this.Close();
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
