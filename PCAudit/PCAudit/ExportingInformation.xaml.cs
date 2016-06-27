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
using System.Windows.Threading;
using System.ComponentModel;

namespace PCAudit
{
    /// <summary>
    /// Interaction logic for ExportingInformation.xaml
    /// </summary>
    public partial class ExportingInformation : Window
    {
        public ExportingInformation()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        AuditManager am = new AuditManager();
        DispatcherTimer dispatcherTimer = new DispatcherTimer();
        public string Softcomputername = "";
        public Boolean ExportingToDatabase = false;

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
        }
        async Task PutTaskDelay()
        {
            await Task.Delay(100);
        } 
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await PutTaskDelay();
            if (ExportingToDatabase == true)
            {
                AuditSoftware();
            }

        }
        private void AuditSoftware()
        {
            dispatcherTimer.Stop();
            am.GetSoftwareInfoAndAudit(Softcomputername);
            ExportingToDatabase = false;
            this.Hide();
        }

    }
}
