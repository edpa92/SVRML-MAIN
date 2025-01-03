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
    /// Interaction logic for AccountSettings.xaml
    /// </summary>
    public partial class AccountSettings : Window
    {
        public AccountSettings()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtCurrentPW.Password)|| string.IsNullOrEmpty(txtNewPW.Password)|| string.IsNullOrEmpty(txtNewPW2.Password))
                {
                    MessageBox.Show("All fields are required.");
                    return;
                }
                using (var data=new DataClasses1DataContext())
                {
                    var admin = data.Administrators.FirstOrDefault(a=>a.AdminId==(int) Application.Current.Resources["AdminID"]);

                    if (admin!=null)
                    {
                        if (admin.AdminPw!=txtCurrentPW.Password)
                        {
                            MessageBox.Show("Wrong current password");
                            return;
                        }
                        if (txtNewPW.Password != txtNewPW2.Password)
                        {
                            MessageBox.Show("New Password and Retyped password are not the same.");
                            return;
                        }

                        admin.AdminPw = txtNewPW2.Password;
                        data.SubmitChanges();
                        MessageBox.Show("Admin password has been changed.");
                        this.Close();
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
