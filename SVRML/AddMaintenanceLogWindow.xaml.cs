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
    /// Interaction logic for AddMaintenanceLogWindow.xaml
    /// </summary>
    public partial class AddMaintenanceLogWindow : Window
    {
        RepairMaintenanceLogcls replog = new RepairMaintenanceLogcls();
        RepairTypecls rtobj = new RepairTypecls();

        int vehid = 0;
        int logid = 0;
        bool isEdit = false;

        public AddMaintenanceLogWindow(int vehid, int logid)
        {
            this.vehid = vehid;
            this.logid = logid;

            isEdit = (vehid==0&&logid>0);
            InitializeComponent();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (isEdit)
            {
                RepairMaintenanceLog rlog = replog.GetRepairLog(logid);
                dateTimePickerRepairDate.SelectedDate = rlog.Repair_Date;
                textBoxMilage.Text =rlog.Milage.ToString();
                textBoxRemarks.Text = rlog.Remarks;
                textBoxCost.Text = rtobj.GetRepType(logid).Cost.ToString();


                cbOil.IsChecked = rtobj.GetRepType(logid).Change_Oil;
                cbBreak.IsChecked= rtobj.GetRepType(logid).Replace_Breakpads;
                cbBelt.IsChecked = rtobj.GetRepType(logid).Replace_Drivebelt;
                cbTire.IsChecked = rtobj.GetRepType(logid).Replace_Tire;
                cbAir.IsChecked = rtobj.GetRepType(logid).Replace_Aircleaner;
            }

            btnSave.Content = (isEdit?"Save Edit":"Save");
            btnDelete.Visibility= (isEdit ? Visibility.Visible: Visibility.Hidden);

        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                if (!dateTimePickerRepairDate.SelectedDate.HasValue)
                {
                    MessageBox.Show("Select Date", "Add Maintenance Data", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                if (string.IsNullOrEmpty(textBoxMilage.Text))
                {
                    MessageBox.Show("Milage is Required", "Add Maintenance Data", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(textBoxCost.Text))
                {
                    MessageBox.Show("Cost is required", "Add Maintenance Data", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(textBoxRemarks.Text))
                {
                    MessageBox.Show("Remarks is required", "Add Maintenance Data", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int hascheckboxchecked = 0;
                
                if ((bool)cbOil.IsChecked)
                {
                    hascheckboxchecked++;
                }
                if ((bool)cbBreak.IsChecked)
                {
                    hascheckboxchecked++;
                }
                if ((bool)cbBelt.IsChecked)
                {
                    hascheckboxchecked++;
                }
                if ((bool)cbTire.IsChecked)
                {
                    hascheckboxchecked++;
                }
                if ((bool)cbAir.IsChecked)
                {
                    hascheckboxchecked++;
                }

                if (hascheckboxchecked==0)
                {
                    MessageBox.Show("Check any Checkbox that applicable to service being provided", "Add Maintenance Data", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                double cost = Convert.ToDouble(textBoxCost.Text.ToString());
                int milage = Convert.ToInt32(textBoxMilage.Text.ToString());
                DateTime repDate = (DateTime)dateTimePickerRepairDate.SelectedDate;
                string remarks = textBoxRemarks.Text.ToString();

                if (!isEdit)
                {
                    if (vehid > 0)
                    {
                        RepairMaintenanceLog log = new RepairMaintenanceLog();
                        log.Admin_ID = (int)Application.Current.Resources["AdminID"];
                        log.Milage = milage;
                        log.Remarks = remarks;
                        log.Repair_Date = repDate;
                        log.Vehicle_ID = vehid;
                        int logid = replog.AddRepairLog(log);

                        if (logid > 0)
                        {
                            RepairType rt = new RepairType();
                            rt.RepairLogID = logid;
                            rt.Replace_Aircleaner = cbAir.IsChecked;
                            rt.Replace_Breakpads = cbBreak.IsChecked;
                            rt.Replace_Drivebelt = cbBelt.IsChecked;
                            rt.Replace_Tire = cbTire.IsChecked;
                            rt.Change_Oil = cbOil.IsChecked;
                            rt.Cost = (decimal)cost;
                            rtobj.AddRepairType(rt);
                            MessageBox.Show("Repair Data Saved!", "Add Maintenance Data", MessageBoxButton.OK, MessageBoxImage.Information);
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("log ID is 0!", "Add Maintenance Data", MessageBoxButton.OK, MessageBoxImage.Error);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("No vehicle selected!", "Add Maintenance Data", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                else
                {
                    RepairMaintenanceLog log = new RepairMaintenanceLog();
                    log.RepairLog_ID = this.logid;
                    log.Milage = milage;
                    log.Remarks = remarks;
                    log.Repair_Date = repDate;
                    replog.EditRepairLog(log);

                    int typeid = 0;
                    using (var data=new DataClasses1DataContext())
                    {
                        typeid = data.RepairTypes.FirstOrDefault(r=>r.RepairLogID==this.logid).RepairType_ID;
                    }
                    RepairType rt = new RepairType();
                    rt.RepairType_ID = typeid;
                    rt.Change_Oil = cbOil.IsChecked;
                    rt.Replace_Breakpads = cbBreak.IsChecked;
                    rt.Replace_Drivebelt = cbBelt.IsChecked;
                    rt.Replace_Tire = cbTire.IsChecked;
                    rt.Replace_Aircleaner = cbAir.IsChecked;
                    rt.Cost = (decimal)cost;
                    rtobj.EditReptype(rt);
                    
                    MessageBox.Show("Repair Data Saved!", "Edit Maintenance Data", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }

            }
            catch (Exception)
            {
                MessageBox.Show("You have entered data with invalid format (Cost, Milage).", "Add Maintenance Data", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult isdelete = MessageBox.Show("Are you sure to delete?\n\n\nDeleting this Repair Maintenance Log will also deleting all the data associated with it.", "Delete Maintenance Log", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (isdelete == MessageBoxResult.Yes)
            {
                replog.DeleteRepairLog(this.logid);
                MessageBox.Show("Delete Success.","Maintenance Log Data", MessageBoxButton.OK, MessageBoxImage.None);
                this.Close();
            }
        }
    }
}
