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

namespace SVRML
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var data = new DataClasses1DataContext())
            {
                var remarks = from r in data.RepairMaintenanceLogs
                               select new {ID=r.RepairLog_ID, r.Repair_Date, r.Milage, r.Remarks };

                myListView.ItemsSource = remarks;
            }
        }
    }
}
