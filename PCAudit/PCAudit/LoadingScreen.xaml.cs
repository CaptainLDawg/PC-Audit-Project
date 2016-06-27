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
using System.Threading;

namespace PCAudit
{
    /// <summary>
    /// Interaction logic for LoadingScreen.xaml
    /// </summary>
    public partial class LoadingScreen : Window
    {
        AuditManager am = new AuditManager();
        MainWindow mw = new MainWindow();
        public bool auditsoftware = false;

        public LoadingScreen()
        {
            InitializeComponent();
            WindowStartupLocation = System.Windows.WindowStartupLocation.CenterScreen;
        }
        async Task PutTaskDelay()
        {
            await Task.Delay(100);
        } 
        private async void Window_Loaded(object sender, RoutedEventArgs e)
        {
            await PutTaskDelay();
            Audit();
        }
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            e.Cancel = true;
        }
        private void Audit()
        {
            am.GetCompInfo();
            if (auditsoftware == true)
            {
                am.GetSoftwareInfo();
            }
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(MainWindow))
                {
                    (window as MainWindow).txtAuditDate.Text = Convert.ToString(System.DateTime.Now);
                    (window as MainWindow).txtManufacturer.Text = am.ManufacturerName;
                    (window as MainWindow).txtModel.Text = am.ModelName;
                    (window as MainWindow).txtComputerName.Text = am.ComputerName;
                    (window as MainWindow).txtOS.Text = am.OperatingSystem;
                    (window as MainWindow).txtOSArch.Text = am.OperatingArchitecture;
                    (window as MainWindow).txtServicePack.Text = am.ServicePack;
                    (window as MainWindow).txtSerialNumber.Text = am.SerialNumber;
                    (window as MainWindow).txtRam.Text = am.RamAmt.ToString() + " GB";
                    (window as MainWindow).txtNoProcessors.Text = am.NoProcessors.ToString();
                    (window as MainWindow).txtProcessor.Text = am.ProcessorName;
                    (window as MainWindow).txtHardDriveSize.Text = am.HardDrivesize.ToString() + " GB";
                    (window as MainWindow).txtFreeHardDrive.Text = am.FreeHardDrive.ToString() + " GB";
                    (window as MainWindow).cbIPAddress.SelectedIndex = 0;
                    (window as MainWindow).cbMacAddress.SelectedIndex = 0;
                    (window as MainWindow).MWRamAmt = am.RamAmt;
                    (window as MainWindow).MWNoProcessors = am.NoProcessors;
                    (window as MainWindow).MWHardDriveSize = am.HardDrivesize;
                    (window as MainWindow).MWFreeHardDrive = am.FreeHardDrive;
                    (window as MainWindow).btnAddDataBase.IsEnabled = true;
                }
                this.Hide();
            }
        }
    }
}
