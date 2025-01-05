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
using System.IO;
using System.Data;
using Spire.Xls;
using OfficeOpenXml;
using System.Diagnostics;

namespace SVRML
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        string vehicleid = "";
        public ReportWindow(string vehicleid)
        {
            this.vehicleid = vehicleid;
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            if (comboBoxYears.SelectedIndex>0)
            {
                using (var data = new DataClasses1DataContext())
                {
                    var vehlog = from v in data.Vehicles
                                 join log in data.RepairMaintenanceLogs on v.Vehicle_ID equals log.Vehicle_ID
                                 join rt in data.RepairTypes on log.RepairLog_ID equals rt.RepairLogID
                                 where v.Vehicle_ID == Convert.ToInt32(this.vehicleid) && log.Repair_Date.Year==Convert.ToInt32(comboBoxYears.SelectedItem.ToString())
                                 select new { Vehicle = v.PlateNum + " " + v.Brand+" "+v.Model, Odometer = log.Milage + "km", v.LastLTORegistration, log.Repair_Date, RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_AirFilter) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") + (((bool)rt.OtherTypes) ? "Others repair, " : ""), log.Remarks };
                     
                   listViewReport.ItemsSource = vehlog;
                    dataGridReport.ItemsSource = vehlog;

                }
            }
            else
            {
                MessageBox.Show("Select Year first","Generate Report by Year",MessageBoxButton.OK,MessageBoxImage.Exclamation);
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var data=new DataClasses1DataContext())
            {
                var vehlog = from v in data.Vehicles
                             join log in data.RepairMaintenanceLogs on v.Vehicle_ID equals log.Vehicle_ID
                             join rt in data.RepairTypes on log.RepairLog_ID equals rt.RepairLogID
                             where v.Vehicle_ID == Convert.ToInt32(this.vehicleid)
                             select new { Vehicle = v.PlateNum + " " + v.Brand+" "+v.Model, Odometer = log.Milage + "km", v.LastLTORegistration, log.Repair_Date, RepairType = (((bool)rt.Replace_Drivebelt) ? "Repalce Drive Belt, " : "") + (((bool)rt.Replace_AirFilter) ? "Repalce Airfilter, " : "") + (((bool)rt.Replace_Breakpads) ? "Repalce Breakpads, " : "") + (((bool)rt.Replace_Tire) ? "Repalce Tire, " : "") + (((bool)rt.Change_Oil) ? "Change oil, " : "") + (((bool)rt.OtherTypes) ? "Others repair, " : ""), log.Remarks };
                if (vehlog != null)
                {
                    listViewReport.ItemsSource = vehlog;
                    dataGridReport.ItemsSource = vehlog;
                    labelVehicleReportPlate.Content = vehlog.FirstOrDefault().Vehicle;
                    labelVehicleReportLTO.Content = vehlog.FirstOrDefault().LastLTORegistration;
                }
                else
                {
                    MessageBox.Show("No data");
                }

                var years = (from y in vehlog
                            select new { Years = y.Repair_Date.Year}).ToList();
                for (int i = 0; i < years.Count; i++)
                {
                    comboBoxYears.Items.Add(years.ElementAt(i).Years);
                }
            }
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            try
            {

                using (var folderDialog = new System.Windows.Forms.FolderBrowserDialog())
                {
                    folderDialog.Description = "Select a folder Where to save/export the report";

                    System.Windows.Forms.DialogResult result = folderDialog.ShowDialog();

                    if (result == System.Windows.Forms.DialogResult.OK && !string.IsNullOrWhiteSpace(folderDialog.SelectedPath))
                    {
                        // Get the selected folder path
                        string folderPath = folderDialog.SelectedPath;
                        //// Convert DataGrid Items to DataTable
                        DataTable dataTable = ConvertDataGridToDataTable(dataGridReport);

                        // Export DataTable to Excel
                        string timestamp = DateTime.Now.ToString("yyyyMMdd_HHmmss");
                        string fullpathandfilename = $"{folderPath}/CarMaintenanceReport_{labelVehicleReportPlate.Content.ToString()}_{timestamp}.xlsx";
                        SaveDataTableToExcel(dataTable, fullpathandfilename);
                       MessageBoxResult isOpen= MessageBox.Show($"Data exported successfully!\n\nDo you want to open the file Now?","Export Report", MessageBoxButton.YesNo, MessageBoxImage.Question);
                        if (isOpen==MessageBoxResult.Yes)
                        {
                            Process.Start(new ProcessStartInfo
                            {
                                FileName = fullpathandfilename,
                                UseShellExecute = true // Ensures the default application is used
                            });
                        }
                    }
                }         
            }
            catch (Exception ex)
            {
                
                MessageBox.Show($"Error: {ex.Message}");
            }
        }



        private void SaveDataTableToExcel(DataTable dataTable, string filePath)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Set the license context

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("DataGrid Export");

                // Load DataTable into Worksheet
                worksheet.Cells["A1"].LoadFromDataTable(dataTable, true);

                // Save to file
                var file = new FileInfo(filePath);
                package.SaveAs(file);
            }
        }

        private DataTable ConvertDataGridToDataTable(DataGrid dataGrid)
        {
            var dataTable = new DataTable();

            // Add Columns
            foreach (var column in dataGrid.Columns)
            {
                dataTable.Columns.Add(column.Header.ToString());
            }

            // Add Rows
            foreach (var item in dataGrid.Items)
            {
                var row = dataTable.NewRow();
                for (int i = 0; i < dataGrid.Columns.Count; i++)
                {
                    row[i] = dataGrid.Columns[i].GetCellContent(item) as TextBlock != null
                         ? (dataGrid.Columns[i].GetCellContent(item) as TextBlock).Text
                         : "";
                }
                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
