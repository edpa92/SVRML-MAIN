using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for SettingWindow.xaml
    /// </summary>
    public partial class SettingWindow : Window
    {
        public ObservableCollection<MyItem> MyItems { get; set; } = new ObservableCollection<MyItem>();

        public SettingWindow()
        {
            InitializeComponent();

            DataContext = this; // Important for data binding
            MyItems.Add(new MyItem { Id = 0, Name = "Select Days" });
            MyItems.Add(new MyItem { Id = 3, Name = "3 Days" });
            MyItems.Add(new MyItem { Id = 5, Name = "5 Days" });
            MyItems.Add(new MyItem { Id = 10, Name = "10 Days" });
            MyItems.Add(new MyItem { Id = 15, Name = "15 Days" });
            MyItems.Add(new MyItem { Id = 20, Name = "20 Days" });
            MyItems.Add(new MyItem { Id = 30, Name = "30 Days" });
        }

        private void buttonSaveSettings_Click(object sender, RoutedEventArgs e)
        {
            using (var data=new DataClasses1DataContext())
            {
                var admin = data.Administrators.FirstOrDefault(a=>a.AdminId== (int)Application.Current.Resources["AdminID"]);
                if (admin!=null && comboBoxDays.SelectedIndex>0)
                {
                    admin.NoDaysNotifyBeforeSched =Convert.ToInt32(comboBoxDays.SelectedValue);
                    data.SubmitChanges();
                    MessageBox.Show("Settings saved", "Setings", MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Select days first","Setings",MessageBoxButton.OK,MessageBoxImage.Exclamation);
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            using (var data =new DataClasses1DataContext())
            {
                comboBoxDays.SelectedValue = data.Administrators.FirstOrDefault(a=>a.AdminId==(int)Application.Current.Resources["AdminID"]).NoDaysNotifyBeforeSched;

            }
        }
    }
}

public class MyItem
{
    public int Id { get; set; }
    public string Name { get; set; }
}
