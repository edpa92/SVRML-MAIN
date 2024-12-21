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
using SVRML.Classes;

namespace SVRML
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Admincls adminobj = new Admincls();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            string uname = textBoxUsername.Text;
            string pw = textBoxPW.Password;

            if (string.IsNullOrWhiteSpace(uname) || string.IsNullOrWhiteSpace(pw))
            {
                MessageBox.Show("Invalid login.", "LOGIN", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            int isloggedin=adminobj.Login(uname, pw);
            if (isloggedin>0)
            {
                Application.Current.Resources["AdminID"] = isloggedin;
                textBoxUsername.Text = textBoxPW.Password = "";
                Dashboard db = new Dashboard();
                db.ShowDialog();

            }else

                MessageBox.Show("Invalid login.", "LOGIN", MessageBoxButton.OK, MessageBoxImage.Error);

        }
    }
}
