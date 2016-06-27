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

namespace PCAudit
{
    /// <summary>
    /// Interaction logic for LoginToChangeControl.xaml
    /// </summary>
    public partial class LoginToChangeControl : Window
    {
        AuditManager am = new AuditManager();
        public LoginToChangeControl()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            var userinfo = am.ChangeControlLogin(txtUser.Text, txtPass.Text);

            if (userinfo == "yes")
            {
                ChangeControl cc = new ChangeControl();
                this.Hide();
                cc.Show();
            }
            else
            {
                MessageBox.Show("Incorrect Username or Password");
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
